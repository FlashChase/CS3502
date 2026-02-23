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

// Configuration - experiment with different values !
# define NUM_ACCOUNTS 4
# define NUM_THREADS 8
# define TRANSACTIONS_PER_THREAD 10
# define INITIAL_BALANCE 1000.0


// Account data structure ( GIVEN )
typedef struct {
	int account_id ;
	double balance ;
	int transaction_count ;
	pthread_mutex_t lock; // NEW: Mutex for this account
} Account ;

// Global shared array - THIS CAUSES RACE CONDITIONS !
Account accounts[NUM_ACCOUNTS];


int method = 9;

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


	int first  = (from_id < to_id) ? from_id : to_id;
	int second = (from_id < to_id) ? to_id : from_id;
	bool same_id = (from_id == to_id);

	pthread_mutex_lock(&accounts[first].lock);
	if (!same_id)
	{
		pthread_mutex_lock(&accounts[second].lock);
	}

	// Check for insufficient funds. Release locks and print error message if true
        if ( (accounts[from_id].balance - amount) < 0)
        {
		if (!same_id)
		{
                	pthread_mutex_unlock(&accounts[second].lock);  // Release second lock
		}

                pthread_mutex_unlock(&accounts[first].lock);  // Release first lock


                printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
                         teller_id, amount, from_id);
        }
        else // If funds are sufficient. Complete transaction and release locks.
        {
                accounts[from_id].balance -= amount;    // Subtract amount from first  account
                accounts[to_id].balance += amount;      // Add amount to second account

                accounts[from_id].transaction_count++;  // Increment transaction counters
                accounts[to_id].transaction_count++;

		if (!same_id) 
                {
                        pthread_mutex_unlock(&accounts[second].lock);  // Release second lock
                }

                pthread_mutex_unlock(&accounts[first].lock);  // Release first lock

                printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                teller_id, amount, from_id, to_id);
        }
}

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
void safe_transfer_timeout (int from_id, int to_id, double amount, int teller_id) {

	bool success = false;		// Used for sentinel

	struct timespec abs_timeout;	// Used for timedlock()

	while (!success) {

		// Try to acquire first lock
		while (!success) {

			clock_gettime(CLOCK_REALTIME, &abs_timeout);	// Set current time
			abs_timeout.tv_sec += 2;			// Add 2 seconds

			// Used timedlock to wait 2 seconds for the lock to open
			if (pthread_mutex_timedlock(&accounts[from_id].lock, &abs_timeout) != 0) {
				usleep(100);
			}else {
				break;	// break out of first loop if the lock is acquired
			}
		}


		if (from_id == to_id) { // If same accounts are selected (Only need 1 lock)

			// Check for insufficient funds and print message if insufficient
                        if ( (accounts[from_id].balance - amount) < 0)
                        {
				pthread_mutex_unlock(&accounts[from_id].lock);	// Release lock to end critical section

                                printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
                                         teller_id, amount, from_id);
                        }
                        else // If funds are sufficient
                        {
                                accounts[from_id].balance -= amount;    // Subtract amount from first  account
                                accounts[to_id].balance += amount;      // Add amount to second account

                                accounts[from_id].transaction_count++;  // Increment transaction count for from account
                                accounts[to_id].transaction_count++;    // Increment transaction count in to_account

                                pthread_mutex_unlock(&accounts[from_id].lock);  // release lock to end critical section

                                printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                                        teller_id, amount, from_id, to_id);
                        }

			success = true;		// Set sentinel flag
		}
		else
		{
			// Try to get second mutex
			clock_gettime(CLOCK_REALTIME, &abs_timeout);
			abs_timeout.tv_sec += 2;

			if (pthread_mutex_timedlock(&accounts[to_id].lock, &abs_timeout) != 0)	// if failed to get mutex
			{
				pthread_mutex_unlock(&accounts[from_id].lock);		// Release first mutex if failed to get second
				usleep(100);						// Sleep to give time for another thread to grab first mutex
			}
			else	// If have both mutexes
			{
				// Check for insufficient funds. Release locks and print error message if true
                                if ( (accounts[from_id].balance - amount) < 0)
                                {

					pthread_mutex_unlock(&accounts[to_id].lock);    // Release second lock
	                                pthread_mutex_unlock(&accounts[from_id].lock);  // Release first lock


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

					printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
                                        teller_id, amount, from_id, to_id);

				}
				success = true;					// Flag Sentinel
			}
		}

	}
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

	bool success = false;	// Use as while sentinel

	while (!success) {

		// Take first mutex lock
		while (1) {
			if (pthread_mutex_trylock(&accounts[from_id].lock) != 0) {
				unsigned int seed = (time(NULL) ^ pthread_self()) % 200;
				usleep(rand_r(&seed));
			}else {
				break; // Break out of loop when lock is obtained
			}
		}

		if (from_id == to_id) {	// If transferring too and from the same account (for some reason)

			// Check for insufficient funds and print message if insufficient
                        if ( (accounts[from_id].balance - amount) < 0)
                        {
                                printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
                                         teller_id, amount, from_id);
                                success = true;
				pthread_mutex_unlock(&accounts[from_id].lock);
                        }
                        else // If funds are sufficient
                        {
                                accounts[from_id].balance -= amount;    // Subtract amount from first  account
                                accounts[to_id].balance += amount;      // Add amount to second account

				accounts[from_id].transaction_count++;	// Increment transaction count for from account
				accounts[to_id].transaction_count++;	// Increment transaction count in to_account

				success = true;				// Set flag variable
				pthread_mutex_unlock(&accounts[from_id].lock);	// release lock

				printf("Teller %d transferred $%.2f from Account %d to Account %d\n",
					teller_id, amount, from_id, to_id);
                        }

		}
		else
		{
			//  Try to take second mutex
			if (pthread_mutex_trylock(&accounts[to_id].lock) != 0) {
				// If second mutex is locked, then release the first one
				pthread_mutex_unlock(&accounts[from_id].lock);

				 // Give a little time for another thread to grab the mutex (Avoid livelock)
				unsigned int seed = (time(NULL) ^ pthread_self()) % 200;
                                usleep(rand_r(&seed));

			}
			else // If obtained both mutexes
			{
				// Check for sufficient funds and print message if true
				if ( (accounts[from_id].balance - amount) < 0)
				{
					printf("Teller %d: Transaction cancelled: Insufficient funds to withdraw $%.2f from account %d\n",
						 teller_id, amount, from_id);
					success = true;
				}
				else // If funds are sufficient
				{
					accounts[from_id].balance -= amount;	// Subtract amount from first  account
					accounts[to_id].balance += amount;	// Add amount to second account

					accounts[from_id].transaction_count++;	// Increment transaction counters
					accounts[to_id].transaction_count++;
				}

				pthread_mutex_unlock(&accounts[from_id].lock);
				pthread_mutex_unlock(&accounts[to_id].lock);
				success = true;
			}
		}

		usleep(1000);
	} // End while loop
} // End safe_transfer_timeout method


// TODO : Create comparison test harness
// Test all strategies with same workload
// Measure : success rate , average time , max retry count
void test_harness


void* teller_thread(void* arg) {
	int teller_id = *(int*) arg ; // GIVEN : Extract thread ID

	unsigned int seed = time(NULL)^pthread_self();

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {

		int account_from = rand_r(&seed) % NUM_ACCOUNTS;

		int account_to = rand_r(&seed) % NUM_ACCOUNTS;

		double amount = rand_r(&seed) % 100 + 1;

		if (method = 1)
		{
			safe_transfer_ordered(account_from, account_to, amount, teller_id);
		}
		else if (method == 2)
		{
			safe_transfer_trylock(account_from, account_to, amount, teller_id);
		}
		else
		{
			safe_transfer_trylock(account_from, account_to, amount, teller_id);
		}
	} // End for loop

	return NULL ;
} // End teller thread method


void cleanup_mutexes () {
	for ( int i = 0; i < NUM_ACCOUNTS; i++) {
		pthread_mutex_destroy(&accounts[i].lock);
    	}
}

//------------MAIN---------------//
int main (int argc, char *argv[])
{
	method = atoi(argv[1]);

	struct timespec start, end;

	clock_gettime(CLOCK_MONOTONIC , &start);

	printf ( "=== Phase 4: Deadlock Resolutions Demo ===\n\n" ) ;

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

        // Create int array of thread ids
        int thread_ids[NUM_THREADS];

        // Create threads
        for (int i = 0; i < NUM_THREADS; i++) {
                thread_ids[i] = i;
                pthread_create(&threads[i], NULL, teller_thread, &thread_ids[i]);
        }

        // Create array to keep track of numbers of finished threads
        int threads_finished[NUM_THREADS];
        for (int i = 0; i < NUM_THREADS; i++) {
                threads_finished[i] = 0;
        }

        // Mark time of when threads were started
        time_t thread_start_time = time(NULL);

        int threads_finished_count = 0;
        bool finished = false;

	while (!finished) {
                // Cycle through threads. tryjoin unfinished threads. Mark as finished if s>
                for (int i = 0; i < NUM_THREADS; i++) {
                        if (threads_finished[i]) {                              // If the t>
                                continue;
                        }else {
                                if (pthread_tryjoin_np(threads[i],NULL) == 0) { // if tryjo>
                                        threads_finished[i] = 1;                // Mark as >
                                        threads_finished_count++;               // Incremen>
                                }
                        }
                }

                if (threads_finished_count == NUM_THREADS) {
                        finished = true;
                }else {
                        if (time(NULL) - thread_start_time > 5) {
                                printf("DEADLOCK DETECTED!\n");
                                printf("Threads involved: ");
                                for (int i = 0; i < NUM_THREADS; i++) {
                                        if (threads_finished[i] == 0) {
                                                printf("Thread %d, ", i);
                                        }
                                }
                                printf("\nKilling threads and exiting program\n");
                                for (int i = 0; i < NUM_THREADS; i++) {
                                        if (threads_finished[i] == 1) {
                                                continue;
                                        }else {
                                                pthread_cancel(threads[i]);
                                        }
                                }
                                cleanup_mutexes();
				return(1);
                        }
                }
        } // End While loop

	// Destroy Mutexes
	cleanup_mutexes();

	printf ( "\n=== Final Results ===\n" ) ;

	double actual_total = 0.0;

	for (int i = 0; i < NUM_ACCOUNTS ; i ++) {

		printf ("Account %d : $%.2f (%d transactions ) \n" ,
		i , accounts[i].balance , accounts[i].transaction_count);
		actual_total += accounts[i].balance ;
	}

	printf ("\nExpected total: $%.2f \n" , expected_total ) ;
	printf ("Actual total: $%.2f \n" , actual_total ) ;
	printf ("Difference: $%.2f \n" , actual_total - expected_total ) ;


	if (actual_total != expected_total) {
		printf("RACE CONDITION DETECTED!\n");
	}
	else {
		printf("NO RACE CONDITION DETECTED.\n");
	}

	clock_gettime(CLOCK_MONOTONIC, &end);

	// Calculate and print program time
	double elapsed = (end.tv_sec - start.tv_sec) + (end.tv_nsec - start.tv_nsec) /1e9;

	printf("Time: %.4f seconds\n", elapsed);

	return 0;
} // End Main
