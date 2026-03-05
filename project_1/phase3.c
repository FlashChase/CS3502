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
# include <string.h>

// Configuration - experiment with different values !
# define NUM_ACCOUNTS 2
# define NUM_THREADS 2
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

_Atomic time_t last_progress;

// GIVEN : Conceptual example showing HOW deadlock occurs
void transfer_deadlock_example ( int from_id , int to_id , double amount, int teller_id ) {

	// This code WILL cause deadlock !
	// Lock source account
	pthread_mutex_lock(&accounts[from_id].lock) ;
	printf("Thread %ld: Locked account %d \n" , pthread_self() , from_id ) ;

	// Simulate processing delay
	usleep (100);

	// Try to lock destination account
	printf ( "Thread %ld : Waiting for account %d / n" , pthread_self() , to_id ) ;
	pthread_mutex_lock(&accounts[to_id].lock) ; // DEADLOCK HERE !

	// Transfer ( never reached if deadlocked )
	accounts[from_id].balance -= amount ;
	accounts[to_id].balance += amount ;

	pthread_mutex_unlock(&accounts[to_id].lock) ;
	pthread_mutex_unlock(&accounts[from_id].lock) ;
}

//TODO 1: Implement complete transfer function
// Use the example above as reference
// Add balance checking ( sufficient funds ?)
// Add error handling
void transfer(int from_id, int to_id, double amount, int teller_id) {

	// Handle invalid account number
	if (from_id < 0 || from_id >= NUM_ACCOUNTS) {
		printf("%d is not a valid account number.\n", from_id);
		return;
	}
	 else if (to_id < 0 || to_id >= NUM_ACCOUNTS) {
		printf("%d is not a valid account number.\n", to_id);
		return;
	}

	// Acquire first lock
	int mtxret = pthread_mutex_lock(&accounts[from_id].lock);
	// Handle error return
	if (mtxret != 0) {
		printf("Failure attempting to obtain Account %d mutex. Error: %s\n", from_id, strerror(mtxret));
		return;
	}
	// Output aquisition message
        printf("Thread %d: Locked account %d \n" , teller_id , from_id ) ;

        // Simulate processing delay
        usleep (100);

        // Try to lock destination account
        printf ( "Thread %d : Waiting for account %d \n" , teller_id , to_id ) ;

	// Try to acquire second lock.
	// At some point another thread (or this thread) will be holding this lock and waiting on the first lock this thread is holding
        mtxret = pthread_mutex_lock(&accounts[to_id].lock) ; // DEADLOCK HERE !
	if (mtxret != 0) {
		pthread_mutex_unlock(&accounts[from_id].lock);	// Release first lock if second fails
                printf("Failure attempting to obtain Account %d mutex. Error: %s\n", to_id, strerror(mtxret));
		return;
	}

        // Transfer ( never reached if deadlocked )
	if ( (accounts[from_id].balance - amount) < 0) {	// Check for insufficient funds
		// Print insufficient funds message
		printf("Thread %d: Insufficient funds to transfer $%.2f from Account %d\n",
			teller_id, amount, from_id);
	}
	else {
        	accounts[from_id].balance -= amount ;	// Update from account balance
	        accounts[to_id].balance += amount ;	// Update to account balance

		accounts[from_id].transaction_count++;	// Update from account transaction count
		accounts[to_id].transaction_count++;	// Update to account transaction count
	}

	// Release locks in reverse order
        pthread_mutex_unlock(&accounts[to_id].lock) ;	// Release to account lock
        pthread_mutex_unlock(&accounts[from_id].lock) ;	// Release from account lock

	// Update progress timer
	last_progress = time(NULL);

} // End transfer method

void* teller_thread(void* arg) {
	int teller_id = *(int*) arg ; // GIVEN : Extract thread ID

	unsigned int seed = time(NULL)^pthread_self();

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {

		// Determine random amount
		double amount = rand_r(&seed) % 100 + 1;

		// TODO 2: Create threads that will deadlock
		// Set transfers in way that will cause deadlock
		if (teller_id == 1) {
			transfer(0, 1, amount, teller_id);	// Teller one always try to aquire lock 0 first and will wait on lock 1 second
		}
		else {
			transfer(1, 0, amount, teller_id);	// Teller two will always try to acquire lock 1 first then lock two
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






int main () {

	printf ( "=== Phase 3: Deadlock Conditions Demo ===\n\n" ) ;

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


	// Create array to hold threads
	pthread_t threads[NUM_THREADS];

	// Create int array of thread ids
	int thread_ids[NUM_THREADS];

	// Create threads
	for (int i = 0; i < NUM_THREADS; i++) {
		thread_ids[i] = i;
		pthread_create(&threads[i], NULL, teller_thread, &thread_ids[i]);
	}

	// TODO 3: Implement deadlock detection
	// Add timeout counter in main()
	// If no progress for 5 seconds, report suspected deadlock

	last_progress = time(NULL);

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

		// Cycle through threads. tryjoin unfinished threads. Mark as finished if successful.
		for (int i = 0; i < NUM_THREADS; i++) {
			if (threads_finished[i]) {		// If the thread has already been marked finished then skip it.
				continue;
			}else {
				if (pthread_tryjoin_np(threads[i],NULL) == 0) {	// if tryjoin is successful
					threads_finished[i] = 1;		// Mark as finished in array
					threads_finished_count++;		// Increment count of finished threads
				}
			}
		}

		// Check if all threads are finished
		if (threads_finished_count == NUM_THREADS)			// Compare finished thread count to total number of threads
		{
			finished = true;					// Mark all threads finished if true
		}
		else								// If all threads are not finished
		{
			if (time(NULL) - last_progress > 5)			// Check to see if 5 seconds have passed
			{
				printf("\nDEADLOCK DETECTED!\n");			// If true output deadlock detection message
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
	}
	return 0; // Will never be reached because of deadlock
} // End Main
