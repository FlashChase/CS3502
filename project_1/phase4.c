// Chase Connell
// CS3502
// Section W03

# define _GNU_SOURCE
# include <pthread.h>
# include <stdio.h>
# include <stdlib.h>
# include <time.h>
# include <unistd.h>
# include <stdbool.h>
# include <stdatomic.h>
# include <string.h>

// Configuration - experiment with different values !
# define NUM_ACCOUNTS 2
# define NUM_THREADS 4
# define TRANSACTIONS_PER_THREAD 10
# define INITIAL_BALANCE 1000.0

atomic_long acquire_attempt = 0;
atomic_long acquire_success = 0;
_Atomic time_t last_progress;

int sleep_time = 0;

// Account data structure ( GIVEN )
typedef struct {
	int account_id ;
	double balance ;
	int transaction_count ;
	pthread_mutex_t lock; // NEW: Mutex for this account
} Account ;

// Struct to hold info to pass to threads
typedef struct {
	int thread_id;
	int method;	// Deadlock resolution method
} thread_info;

// Global shared array - THIS CAUSES RACE CONDITIONS !
Account accounts[NUM_ACCOUNTS];


void* teller_thread(void* arg);
void cleanup_mutexes(void);

/* STRATEGY 1: Lock Ordering ( RECOMMENDED )
*
* ALGORITHM :
* To prevent circular wait , always acquire locks in consistent order
*
* Step 1: Identify which account ID is lower
* Step 2: Lock lower ID first
* Step 3: Lock higher ID second
* Step 4: Perform transfer
* Step 5: Unlock in reverse order
*
* WHY THIS WORKS :
* - Thread 1: transfer (0 ,1) locks 0 then 1
* - Thread 2: transfer (1 ,0) locks 0 then 1 ( SAME ORDER !)
* - No circular wait possible
*
* WHICH COFFMAN CONDITION DOES THIS BREAK ?
* Answer in your report !
*/

// TODO : Implement safe_transfer_ordered ( from , to , amount )
// Use the algorithm description above
// Hint : int first = ( from < to ) ? from : to ;
void safe_transfer_ordered ( int from_id, int to_id, double amount, int teller_id ) {

	// Handle invalid account number
        if (from_id < 0 || from_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", from_id);
                return;
        }
         else if (to_id < 0 || to_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", to_id);
                return;
        }

	int first  = (from_id < to_id) ? from_id : to_id;	// Set first to lower id
	int second = (from_id < to_id) ? to_id : from_id;	// Set second to lower id

	acquire_attempt++;
	int mtxret = pthread_mutex_lock(&accounts[first].lock);		// Lock the first mutex
	if (mtxret != 0) {
               	pthread_mutex_unlock(&accounts[from_id].lock);  // Release first lock if second fails
               	printf("Failure attempting to obtain Account %d mutex. Error: %s\n", first, strerror(mtxret));
                return;
	}

	acquire_success++;

	if (sleep_time != 0) {
                usleep(sleep_time);				// Add a small sleep to try to induce deadlock
	}

	acquire_attempt++;
	mtxret = pthread_mutex_lock(&accounts[second].lock);		// Acquire second lock
	if (mtxret != 0) {
                pthread_mutex_unlock(&accounts[first].lock);  // Release first lock if second fails
                printf("Failure attempting to obtain Account %d mutex. Error: %s\n", second, strerror(mtxret));
                return;
        }

	// Complete transactions
        if ( (accounts[from_id].balance - amount) < 0)	// If funds are insufficient
        {
        	pthread_mutex_unlock(&accounts[second].lock);  // Release second lock

                pthread_mutex_unlock(&accounts[first].lock);  // Release first lock

		// Output insufficient funds message after releasing locks to decrease time in critical section
                printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
                         teller_id, amount, from_id);
        }
        else // If funds are sufficient. Complete transaction and release locks.
        {
                accounts[from_id].balance -= amount;    // Subtract amount from first  account
                accounts[to_id].balance += amount;      // Add amount to second account

                accounts[from_id].transaction_count++;  // Increment transaction counters
                accounts[to_id].transaction_count++;	// Increment transaction counters

                pthread_mutex_unlock(&accounts[second].lock);  // Release second lock
                pthread_mutex_unlock(&accounts[first].lock);  // Release first lock

		// Output transaction message after releasing locks to decrease time in critical section
                printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                teller_id, amount, from_id, to_id);
        }
	acquire_success++;

	// Set progess time
        last_progress = time(NULL);

} // End safe_transfer_ordered

/* STRATEGY 2: Timeout Mechanism
*
* ALGORITHM :
* Try to acquire locks with timeout ; release and retry if timeout
*
* Step 1: pthread_mutex_timedlock on first account (2 sec timeout )
* Step 2: If timeout , return and retry
* Step 3: pthread_mutex_timedlock on second account (2 sec timeout )
* Step 4: If timeout , release first lock and retry
* Step 5: If both acquired , perform transfer
*
* REFERENCE : man pthread_mutex_timedlock
*/
// TODO : Implement safe_transfer_timeout ( from , to , amount )
void safe_transfer_timedlock (int from_id, int to_id, double amount, int teller_id) {

	// Handle invalid account number
        if (from_id < 0 || from_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", from_id);
                return;
        }
         else if (to_id < 0 || to_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", to_id);
                return;
        }


	bool success = false;		// Used for sentinel

	struct timespec abs_timeout;	// Used for timedlock()

	while (!success) {

		// Try to acquire first lock
		while (1) {	// Repeat until first lock is acquired

			clock_gettime(CLOCK_REALTIME, &abs_timeout);	// Set current time
			abs_timeout.tv_sec += 2;			// Add 2 seconds

			acquire_attempt++;
			// Used timedlock to wait 2 seconds for the lock to open
			if (pthread_mutex_timedlock(&accounts[from_id].lock, &abs_timeout) != 0) {	// If didn't acquire lock
				unsigned int seed = (time(NULL) ^ pthread_self());              // Get rando>
                                usleep(rand_r(&seed) % 200);               		// Sleep random time
			}
			else
			{
				acquire_success++;
				break;	// break out of first loop if the lock is acquired
			}
		}


		if (sleep_time != 0) {
			usleep(sleep_time);                  // Add a small sleep to try to induce deadlock
	        }
		// Try to get second mutex
		clock_gettime(CLOCK_REALTIME, &abs_timeout);	// Set abs_timeout to the current time
		abs_timeout.tv_sec += 2;			// Add two seconds to it

		acquire_attempt++;
		if (pthread_mutex_timedlock(&accounts[to_id].lock, &abs_timeout) != 0)	// if failed to get mutex
		{
			pthread_mutex_unlock(&accounts[from_id].lock);		// Release first mutex if
			unsigned int seed = (time(NULL) ^ pthread_self());      // Get rando>
                        usleep(rand_r(&seed) % 200);              		// Sleep randcm time 
		}
		else	// If have both mutexes
		{
			// Complete transactions
                        if ( (accounts[from_id].balance - amount) < 0)		// If funds are insufficient
                        {

				pthread_mutex_unlock(&accounts[to_id].lock);    // Release second lock
                                pthread_mutex_unlock(&accounts[from_id].lock);  // Release first lock

				// Print insufficient funds message after unlocking mutex to decrease time in critical section
                                printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
                                         teller_id, amount, from_id);
                        }
                        else // If funds are sufficient. Complete transaction and release locks.
                        {
                                accounts[from_id].balance -= amount;    // Subtract amount from first  account
                                accounts[to_id].balance += amount;      // Add amount to second account

                                accounts[from_id].transaction_count++;  // Increment transaction counters
                                accounts[to_id].transaction_count++;

				pthread_mutex_unlock(&accounts[to_id].lock);	// Release second lock
				pthread_mutex_unlock(&accounts[from_id].lock);	// Release first lock

				// Print transaction message after releasing mutex to decrease time in critical section
				printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                                teller_id, amount, from_id, to_id);

			}

			acquire_success++;
			success = true;	// Flag Sentinel to end loop and method
		}
	}
	// Set progess time
        last_progress = time(NULL);
}


/* STRATEGY 3: Try - Lock with Backoff
*
* ALGORITHM :
* Use non - blocking trylock ; back off and retry if fails
* Step 1: pthread_mutex_trylock on first account
* Step 2: If fails , usleep ( random time ) and retry
* Step 3: pthread_mutex_trylock on second account
* Step 4: If fails , release first , usleep , retry
*
* REFERENCE : man pthread_mutex_trylock
*/
// TODO : Implement safe_transfer_trylock ( from , to , amount )
void safe_transfer_trylock(int from_id, int to_id, double amount, int teller_id) {

	// Handle invalid account number
        if (from_id < 0 || from_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", from_id);
                return;
        }
         else if (to_id < 0 || to_id >= NUM_ACCOUNTS) {
                printf("%d is not a valid account number.\n", to_id);
                return;
        }

	bool success = false;	// Use as while sentinel

	while (!success) {

		// Take first mutex lock
		while (1) {	// While haven't obtained the first lock

			acquire_attempt++;
			if (pthread_mutex_trylock(&accounts[from_id].lock) != 0) {		// If failed to obtain the lock
				unsigned int seed = (time(NULL) ^ pthread_self());		// Get random seed
				usleep(rand_r(&seed) % 200);					// Sleep before retry
			}
			else									// If lock is obtained
			{
				if (sleep_time != 0)
			        {
			                usleep(sleep_time);                             // Add a small sleep to try to induce deadlock
			        }

				acquire_success++;
				break; 								// Break out of inner loop
			}
		}

		acquire_attempt++;
		//  Try to take second mutex
		if (pthread_mutex_trylock(&accounts[to_id].lock) != 0) {		// If failed to get the second lock

			pthread_mutex_unlock(&accounts[from_id].lock);			// Release the first lock

			// Give a little time for another thread to grab the mutex (Avoid livelock)
			unsigned int seed = (time(NULL) ^ pthread_self());
                        usleep(rand_r(&seed) % 200);

		}
		else // If obtained both mutexes
		{
			// Complete transaction
			if ( (accounts[from_id].balance - amount) < 0)	// If funds are insufficient
			{
				pthread_mutex_unlock(&accounts[to_id].lock);	// Release second lock
                                pthread_mutex_unlock(&accounts[from_id].lock);	// Release first lock

				acquire_success++;
	                        success = true;					// Set flag to end method

				// Print insufficient funds message after unlocking mutex to decrease time in critical section
				printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
					 teller_id, amount, from_id);
			}
			else // If funds are sufficient
			{
				accounts[from_id].balance -= amount;	// Subtract amount from first  account
				accounts[to_id].balance += amount;	// Add amount to second account

				accounts[from_id].transaction_count++;	// Increment transaction counters
				accounts[to_id].transaction_count++;

				pthread_mutex_unlock(&accounts[to_id].lock);	// Release second lock
				pthread_mutex_unlock(&accounts[from_id].lock);	// Release first lock

				acquire_success++;				// Increment success counter
				success = true;					// Set flag to end method

				// Print transaction message after releasing mutex to decrease time in critical section
	                        printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                                        teller_id, amount, from_id, to_id);
			}
		}
	} // End while loop

	// Set progess time
        last_progress = time(NULL);
} // End safe_transfer_timeout method


void test_harness() {

	printf("TEST HARNESS RUNNING\n");

	struct timespec start, end;                     // Create two timespec structs

	double time_elapsed, ordered_time, timedlock_time, trylock_time;

	int ordered_attempts, ordered_success, ordered_max_retries,
	    timedlock_attempts, timedlock_success, timedlock_max_retries,
	    trylock_attempts, trylock_success, trylock_max_retries,
	    max_retries;

	// Create array to hold threads
        pthread_t threads[NUM_THREADS];

        // Create array of Thread info structs
        thread_info threads_struct[NUM_THREADS];


	for (int method = 1; method < 4; method++)
	{
		time_elapsed = 0; 	// Reset elapsed time for each method

		acquire_attempt = 0;	// Reset mutex statistics between methods
		acquire_success = 0;
		max_retries = 0;

		for (int iter = 0; iter < 10; iter++)
		{
			// Initialize accounts
			for (int i = 0; i < NUM_ACCOUNTS; i++) {
	                	accounts[i].account_id = i;
	        	        accounts[i].balance = INITIAL_BALANCE;
		                accounts[i].transaction_count = 0;

	                	// Initialize the mutex
	        	        pthread_mutex_init(&accounts[i].lock, NULL);
	 	       }

			clock_gettime(CLOCK_MONOTONIC , &start);        // Set start equal to current time

			// Create threads
		        for (int i = 0; i < NUM_THREADS; i++)
			{
	        	        threads_struct[i].thread_id = i;        // Set the thread id
	                	threads_struct[i].method = method;      // Set the method to be used
		                pthread_create(&threads[i], NULL, teller_thread, &threads_struct[i]);   // Create the thread and pass it the stru>
	        	}

			// Join threads
		        for ( int i = 0; i < NUM_THREADS ; i ++) {
                		pthread_join(threads[i], NULL);
		        }

		        clock_gettime(CLOCK_MONOTONIC , &end);        // Set end equal to current time

			time_elapsed += (end.tv_sec - start.tv_sec) + (end.tv_nsec - start.tv_nsec) /1e9;

			if (acquire_attempt > max_retries)
			{
				max_retries = acquire_attempt - acquire_success;
			}

		}

		if (method == 1)
		{
			ordered_time = time_elapsed;
			ordered_attempts = acquire_attempt;
			ordered_success = acquire_success;
			ordered_max_retries = max_retries;
		}
		else if (method == 2)
		{
			timedlock_time = time_elapsed;
			timedlock_attempts = acquire_attempt;
			timedlock_success = acquire_success;
			timedlock_max_retries = max_retries;
		}
		else
		{
			trylock_time = time_elapsed;
			trylock_attempts = acquire_attempt;
			trylock_success = acquire_success;
			trylock_max_retries = max_retries;
		}

		cleanup_mutexes();
	}

	printf("\n=============DEADLOCK METHOD STATISTICS==============\n");

	printf("\nSHARED STATISTICS\nThread Count: %d\nNumber of Accounts: %d\nTransaction Count per Thread: %d\n",
		NUM_THREADS, NUM_ACCOUNTS, TRANSACTIONS_PER_THREAD);

	printf("\nLOCK ORDERING STATISTICS\nAverage time: %.4f\nMax Retry Count: %d\nLock Acquisition Success Rate: %.2f\n",
		(ordered_time / 10), ordered_max_retries, ((double)ordered_success / ordered_attempts * 100));

	printf("\nTIMED LOCK STATISTICS\nAverage time: %.4f\nMax Retry Count: %d\nLock Acquisition Success Rate: %.2f\n",
                (timedlock_time / 10), timedlock_max_retries, ((double)timedlock_success / timedlock_attempts * 100));

	printf("\nTRYLOCK STATISTICS\nAverage time: %.4f\nMax Retry Count: %d\nLock Acquisition Success Rate: %.2f\n",
                (trylock_time / 10), trylock_max_retries, ((double)trylock_success / trylock_attempts * 100));


	exit(0);

} // End test_harness method


void* teller_thread(void* arg) {
	thread_info *info = arg ;

	int teller_id = info->thread_id;
	int method = info->method;

	unsigned int seed = time(NULL)^pthread_self();

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {

		int account_from = rand_r(&seed) % NUM_ACCOUNTS;

		int account_to = rand_r(&seed) % NUM_ACCOUNTS;
		while (account_to == account_from) {
			account_to = rand_r(&seed) % NUM_ACCOUNTS;
		}

		double amount = rand_r(&seed) % 100 + 1;

		if (method == 1)
		{
			safe_transfer_ordered(account_from, account_to, amount, teller_id);
		}
		else if (method == 2)
		{
			safe_transfer_timedlock(account_from, account_to, amount, teller_id);
		}
		else
		{
			safe_transfer_trylock(account_from, account_to, amount, teller_id);
		}
	} // End for loop

	return NULL ;
} // End teller thread method

// Destroy mutexes
void cleanup_mutexes () {
	for ( int i = 0; i < NUM_ACCOUNTS; i++) {
		pthread_mutex_destroy(&accounts[i].lock);
    	}
}

//------------MAIN---------------//
int main (int argc, char *argv[])
{

	// Which deadlock resolution method
	int method;

	// Set progess time
	last_progress = time(NULL);

	if (argc >= 2)	// If there were at least two arguments entered
	{
		method = atoi(argv[1]);

		if (argc == 3)
		{
			if (atoi(argv[2]) >= 0)
			{
				sleep_time = atoi(argv[2]);
			}
		}
		if (method == 5)
		{
			test_harness();
		}
		else if (method != 1 && method != 2 && method != 3)	// If arg1 is not 1, 2 or 3
		{
			method = 1;				// Set it to 1
		}
	}
	else
	{
		method = 1;					// Default method to 1
		sleep_time = 0;
	}

	struct timespec start, end;			// Create two timespec structs

	printf ( "=== Phase 4: Deadlock Resolutions Demo ===\n" ) ;

	// Print which method the program will use
	switch (method)
	{
		case 1:
			printf("Using Lock Ordering\n\n");
			break;
		case 2:
			printf("Using Timed Lock\n\n");
			break;
		case 3:
			printf("Using Try Lock\n\n");
			break;
	}

	// Initialize accounts
	for (int i = 0; i < NUM_ACCOUNTS; i++) {
		accounts[i].account_id = i;
		accounts[i].balance = INITIAL_BALANCE;
		accounts[i].transaction_count = 0;

		// Initialize the mutex
		pthread_mutex_init(&accounts[i].lock, NULL);
	}

	// Display initial state
	printf ("Initial State :\n" ) ;

	for ( int i = 0; i < NUM_ACCOUNTS ; i ++) {
		printf ("Account %d : $%.2f \n" , i , accounts[i].balance) ;
	}

	// Calculate expected end total
	double expected_total = INITIAL_BALANCE * NUM_ACCOUNTS;


	// Create array to hold threads
        pthread_t threads[NUM_THREADS];

        // Create array of Thread info structs
        thread_info threads_struct[NUM_THREADS];

	clock_gettime(CLOCK_MONOTONIC , &start);	// Set start equal to current time

        // Create threads
        for (int i = 0; i < NUM_THREADS; i++) {
                threads_struct[i].thread_id = i;	// Set the thread id
		threads_struct[i].method = method;	// Set the method to be used
                pthread_create(&threads[i], NULL, teller_thread, &threads_struct[i]);	// Create the thread and pass it the struct info
        }

        // Create array to keep track of numbers of finished threads
        int threads_finished[NUM_THREADS];
        for (int i = 0; i < NUM_THREADS; i++) {
                threads_finished[i] = 0;
        }

	// Counter for finished threads
        int threads_finished_count = 0;

	// Sentinel to tell if all threads are finished
        bool finished = false;

	// Run while all threads aren't finished (or 5 seconds passes)
	while (!finished) {

                // Cycle through threads. tryjoin unfinished threads. Mark as finished if s>
                for (int i = 0; i < NUM_THREADS; i++) {
                        if (threads_finished[i]) {                              // If the thread has already been marked finished the skip it
                                continue;
                        }else {
                                if (pthread_tryjoin_np(threads[i],NULL) == 0) { // if tryjoin is successful
                                        threads_finished[i] = 1;                // Mark as finished in array
                                        threads_finished_count++;               // Increment count of finished threads
                                }
                        }
                }

		// Check if all threads are finished
                if (threads_finished_count == NUM_THREADS)			// Compare finished thread count to total number of threads
		{
                        finished = true;					// Flag all threads finished if true
                }
		else								// If all threads are not finished
		{
                        if (time(NULL) - last_progress > 5)			// Check to see if 5 seconds has passed
			{
                                printf("DEADLOCK DETECTED!\n");			// If true, output deadlock detection message
                                printf("Threads involved: ");
                                for (int i = 0; i < NUM_THREADS; i++) {		// Cycle through thread array and print their ID if they are not finished
                                        if (threads_finished[i] == 0) {
                                                printf("Thread %d, ", i);
                                        }
                                }
                                printf("\nKilling threads and exiting program\n");	// Output program exit message
                                for (int i = 0; i < NUM_THREADS; i++) {			// Cycle through threads
                                        if (threads_finished[i] == 1) {			// Check if finished
                                                continue;
                                        }else {
                                                pthread_cancel(threads[i]);		// Kill them if they are not finished
                                        }
                                }
                                cleanup_mutexes();					// Destroy mutexes
				return(1);						// Exit program with error
                        }
                }
        } // End deadlock detection while loop

	// Set end equal to current time
        clock_gettime(CLOCK_MONOTONIC, &end);

	// Destroy Mutexes
	cleanup_mutexes();

	printf ( "\n=== Final Results ===\n" ) ;

	double actual_total = 0.0;

	// Cycle through accounts and get actual total balance
	for (int i = 0; i < NUM_ACCOUNTS ; i ++) {

		printf ("Account %d : $%.2f (%d transactions ) \n" ,
		i , accounts[i].balance , accounts[i].transaction_count);
		actual_total += accounts[i].balance ;
	}

	// Output results
	printf ("\nExpected total: $%.2f \n" , expected_total ) ;
	printf ("Actual total: $%.2f \n" , actual_total ) ;
	printf ("Difference: $%.2f \n" , actual_total - expected_total ) ;

	// Output Race condition detection message
	if (actual_total != expected_total) {
		printf("\nRACE CONDITION DETECTED!\n");
	}
	else {
		printf("\nNO RACE CONDITION DETECTED.\n");
	}

	// Calculate and display elapsed time in seconds
        // End seconds - Start seconds + {(End nanoseconds - Start nanoseconds) converted to seconds}
	double elapsed = (end.tv_sec - start.tv_sec) + (end.tv_nsec - start.tv_nsec) /1e9;

	printf("\nTime: %.4f seconds\n", elapsed);

	double success_rate = (double)acquire_success / (double)acquire_attempt * 100;
	printf("\nLock acquisition attempts: %ld\nLock acquisition successes: %ld\nSuccess rate: %.2f%%\n",
		acquire_attempt, acquire_success, success_rate);

	return 0;
} // End Main
