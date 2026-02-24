// Chase Connell
// CS3502
// Section W03

# include <pthread.h>
# include <stdio.h>
# include <stdlib.h>
# include <time.h>
# include <unistd.h>

// Configuration - experiment with different values !
# define NUM_ACCOUNTS 2
# define NUM_THREADS 4
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

// GIVEN : Example deposit function WITH race condition
void deposit_safe ( int account_id , double amount ) {
	// Acquire lock BEFORE accessing shared data
	pthread_mutex_lock(&accounts[account_id].lock);

	// ===== CRITICAL SECTION =====
	// Only ONE thread can execute this at a time for this account
	accounts[account_id].balance += amount;
	accounts[account_id].transaction_count++;
	// ============================

	// Release lock AFTER modifying shared data
	pthread_mutex_unlock(&accounts[account_id].lock);
}

// TODO 1: Implement withdrawal_safe () with mutex protection
void withdrawal_safe ( int account_id , double amount ) {

	// Hint : pthread_mutex_lock
	// Hint : Modify balance
	// Hint : pthread_mutex_unlock
	pthread_mutex_lock(&accounts[account_id].lock);		// Acquire lock

	accounts[account_id].balance -= amount;			// Update balance
	accounts[account_id].transaction_count++;		// Update transaction count

	pthread_mutex_unlock(&accounts[account_id].lock);	// Release lock
}


// TODO 2: Update teller_thread to use safe functions
// Change : deposit_unsafe -> deposit_safe
// Change : withdrawal_unsafe -> withdrawal_safe
void* teller_thread(void* arg) {
	int teller_id = *(int*) arg ; // GIVEN : Extract thread ID

	unsigned int seed = time(NULL)^pthread_self();			// Creat seed for random

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {

		int account_idx = rand_r(&seed) % NUM_ACCOUNTS;		// Determine account 1
		int account_idx2 = rand_r(&seed) % NUM_ACCOUNTS;	// Determine account 2

		double amount = rand_r(&seed) % 100 + 1;		// Determine transaction amount

		// int operation = rand_r(&seed) % 2;	// Not needed for transfers

		// Call updated deposit method
		deposit_safe ( account_idx , amount ) ;
		printf ("Teller %d: Deposited $%.2f to Account %d \n" ,
			teller_id , amount , account_idx ) ;

		// Call updated withdrawal method
		withdrawal_safe ( account_idx2 , amount ) ;
		printf ("Teller %d: Withdrew $%.2f from Account %d \n" ,
			teller_id , amount , account_idx2);

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
	// TODO 3: Add performance timing
	// Reference : Section 7.2 " Performance Measurement "
	// Hint : Use clock_gettime ( CLOCK_MONOTONIC , & start );

	struct timespec start, end;			// Create two timespec structs
	clock_gettime(CLOCK_MONOTONIC , &start);	// Assign current time to start


	printf ( "=== Phase 2: Race Conditions Demo ===\n\n" ) ;

	// Initialize Accounts
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

	double expected_total = NUM_ACCOUNTS * INITIAL_BALANCE;	// Set expected end balance (all transactions are transfers)

	pthread_t threads[NUM_THREADS];	// Create array to hold threads

	int thread_ids[NUM_THREADS];	// Create array to hold thread ID's

	// Create Threads
	for ( int i = 0; i < NUM_THREADS ; i ++) {
		thread_ids[i] = i ; // Store ID persistently
		pthread_create(&threads[i], NULL, teller_thread, &thread_ids[i] );
	}

	// Join threads
	for ( int i = 0; i < NUM_THREADS ; i ++) {
		pthread_join(threads[i], NULL);
	}

	// TODO 4: Add mutex cleanup in main ()
	// Reference : man pthread_mutex_destroy
	// Important : Destroy mutexes AFTER all threads complete !
	cleanup_mutexes();

	printf ( "\n=== Final Results ===\n" ) ;

	double actual_total = 0.0;

	// Calculate and display final results
	for (int i = 0; i < NUM_ACCOUNTS ; i ++) {

		printf ("Account %d : $%.2f (%d transactions ) \n" ,
		i , accounts[i].balance , accounts[i].transaction_count);
		actual_total += accounts[i].balance ;
	}
	printf ("\nExpected total: $%.2f \n" , expected_total ) ;
	printf ("Actual total: $%.2f \n" , actual_total ) ;
	printf ("Difference: $%.2f \n" , actual_total - expected_total ) ;

	// Test for Race Condition
	if (actual_total != expected_total) {
		printf("\nRACE CONDITION DETECTED!\n");
	}
	else {
		printf("\nNO RACE CONDITION DETECTED.\n");
	}

	// TODO 3: Performance measurement

	clock_gettime(CLOCK_MONOTONIC, &end);	// Set end to current time

	// Calculate and display elapsed time in seconds
	// End seconds - Start seconds + {(End nanoseconds - Start nanoseconds) converted to seconds}
	double elapsed = (end.tv_sec - start.tv_sec) +( (end.tv_nsec - start.tv_nsec) /1e9 );
	printf("\nTime: %.4f seconds\n", elapsed);

	return 0;
} // End Main
