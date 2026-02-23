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

// Set global variable to track totals of all accounts. Also causes race conditions.
// double expected_total = NUM_ACCOUNTS * INITIAL_BALANCE;

//TODO 1: Implement complete transfer function
// Use the example above as reference
// Add balance checking ( sufficient funds ?)
// Add error handling
void transfer(int from_id, int to_id, double amount, int teller_id) {

	pthread_mutex_lock(&accounts[from_id].lock);

        printf("Thread %d: Locked account %d \n" , teller_id , from_id ) ;

        // Simulate processing delay
        usleep (100);

        // Try to lock destination account
        printf ( "Thread %d : Waiting for account %d \n" , teller_id , to_id ) ;

        pthread_mutex_lock(&accounts[to_id].lock) ; // DEADLOCK HERE !

        // Transfer ( never reached if deadlocked )
	if ( (accounts[from_id].balance - amount) < 0) {
		printf("Thread %d: Insufficient funds to transfer $%.2f from Account %d\n",
			teller_id, amount, from_id);
	}
	else {
        	accounts[from_id].balance -= amount ;
	        accounts[to_id].balance += amount ;
	}

        pthread_mutex_unlock(&accounts[to_id].lock) ;
        pthread_mutex_unlock(&accounts[from_id].lock) ;


} // End transfer method

void* teller_thread(void* arg) {
	int teller_id = *(int*) arg ; // GIVEN : Extract thread ID

	unsigned int seed = time(NULL)^pthread_self();

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {

		double amount = rand_r(&seed) % 100 + 1;

		if (teller_id == 1) {
			transfer(0, 1, amount, teller_id);
		}
		else {
			transfer(1, 0, amount, teller_id);
		}

	} // End for loop

	return NULL ;
} // End teller thread method


void cleanup_mutexes () {
	for ( int i = 0; i < NUM_ACCOUNTS; i++) {
		pthread_mutex_destroy(&accounts[i].lock);
    	}
}

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





int main () {

	struct timespec start, end;

	clock_gettime(CLOCK_MONOTONIC , &start);

	printf ( "=== Phase 3: Deadlock Conditions Demo ===\n\n" ) ;

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
		// Cycle through threads. tryjoin unfinished threads. Mark as finished if successful.
		for (int i = 0; i < NUM_THREADS; i++) {
			if (threads_finished[i]) {				// If the thread has already been marked finished then skip it.
				continue;
			}else {
				if (pthread_tryjoin_np(threads[i],NULL) == 0) {	// if tryjoin is successful
					threads_finished[i] = 1;		// Mark as finished in array
					threads_finished_count++;		// Increment cound of finished threads
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
	}
	return 0; // Will never be reached
} // End Main
