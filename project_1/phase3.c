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

// Set global variable to track totals of all accounts. Also causes race conditions.
double expected_total = NUM_ACCOUNTS * INITIAL_BALANCE;

// GIVEN : Example deposit function WITH race condition
void deposit_safe ( int account_id , double amount ) {
	// Acquire lock BEFORE accessing shared data
	pthread_mutex_lock(&accounts[account_id].lock);

	// ===== CRITICAL SECTION =====
	// Only ONE thread can execute this at a time for this account
	accounts[account_id].balance += amount;
	accounts[account_id].transaction_count++;
	expected_total += amount;
	// ============================

	// Release lock AFTER modifying shared data
	pthread_mutex_unlock(&accounts[account_id].lock);
}

// TODO 1: Implement withdrawal_safe () with mutex protection
// Reference : Follow the pattern of deposit_safe () above
// Remember : lock BEFORE accessing data , unlock AFTER
void withdrawal_safe ( int account_id , double amount ) {
	// YOUR CODE HERE
	// Hint : pthread_mutex_lock
	// Hint : Modify balance
	// Hint : pthread_mutex_unlock
	pthread_mutex_lock(&accounts[account_id].lock);

	accounts[account_id].balance -= amount;
	accounts[account_id].transaction_count++;
	expected_total -= amount;

	pthread_mutex_unlock(&accounts[account_id].lock);
}


// TODO 2: Update teller_thread to use safe functions
// Change : deposit_unsafe -> deposit_safe
// Change : withdrawal_unsafe -> withdrawal_safe
void* teller_thread(void* arg) {
	int teller_id = *(int*) arg ; // GIVEN : Extract thread ID

	// TODO 2 a : Initialize thread - safe random seed
	// Reference : Section 7.2 " Random Numbers per Thread "
	// Hint : unsigned int seed = time ( NULL ) ^ pthread_self () ;

	unsigned int seed = time(NULL)^pthread_self();

	for ( int i = 0; i < TRANSACTIONS_PER_THREAD ; i ++) {
		// TODO 2 b : Randomly select an account (0 to NUM_ACCOUNTS -1)
		// Hint : Use rand_r (& seed ) % NUM_ACCOUNTS
		int account_idx = rand_r(&seed) % NUM_ACCOUNTS;

		// TODO 2 c : Generate random amount (1 -100)
		double amount = rand_r(&seed) % 100 + 1;

		// TODO 2 d : Randomly choose deposit (1) or withdrawal (0)
		// Hint : rand_r (& seed ) % 2
		int operation = rand_r(&seed) % 2;

		// TODO 2 e : Call appropriate function
		if ( operation == 1) {
			deposit_safe ( account_idx , amount ) ;
			printf ("Teller %d: Deposited $%.2f to Account %d \n" ,
				teller_id , amount , account_idx ) ;
		} else {
			// YOUR CODE HERE - call withdrawal_unsafe
			withdrawal_safe ( account_idx , amount ) ;
			printf ("Teller %d: Withdrew $%.2f to Account %d \n" ,
				teller_id , amount , account_idx);
		}
	} // End for loop

	return NULL ;
} // End teller thread method


void cleanup_mutexes () {
	for ( int i = 0; i < NUM_ACCOUNTS; i++) {
		pthread_mutex_destroy(&accounts[i].lock);
    	}
}

int main () {
	// TODO 3: Add performance timing
	// Reference : Section 7.2 " Performance Measurement "
	// Hint : Use clock_gettime ( CLOCK_MONOTONIC , & start );
	struct timespec start, end;
	clock_gettime(CLOCK_MONOTONIC , &start);
	printf ( "=== Phase 2: Race Conditions Demo ===\n\n" ) ;
	// TODO 3 a : Initialize all accounts
	// Hint : Loop through accounts array
	// Set : account_id = i , balance = INITIAL_BALANCE , transaction_count = 0
	for (int i = 0; i < NUM_ACCOUNTS; i++) {
		accounts[i].account_id = i;
		accounts[i].balance = INITIAL_BALANCE;
		accounts[i].transaction_count = 0;

		// Initialize the mutex
		pthread_mutex_init(&accounts[i].lock, NULL);
	}

	// Display initial state ( GIVEN )
	printf ("Initial State :\n" ) ;

	for ( int i = 0; i < NUM_ACCOUNTS ; i ++) {
		printf ("Account %d : $%.2f \n" , i , accounts[i].balance) ;
	}

	// TODO 3 b : Calculate expected final balance
	// Question : With random deposits / withdrawals , what should total be ?
	// Hint : Total money in system should remain constant !


	// TODO 3 c : Create thread and thread ID arrays
	// Reference : man pthread_create for pthread_t type

	pthread_t threads[NUM_THREADS];

	int thread_ids[NUM_THREADS]; // GIVEN : Separate array for IDs

	// TODO 3 d : Create all threads
	// Reference : man pthread_create
	// Caution : See Appendix A .2 warning about passing & i in loop !

	for ( int i = 0; i < NUM_THREADS ; i ++) {
		thread_ids[i] = i ; // GIVEN : Store ID persistently
		// YOUR pthread_create CODE HERE
		// Format : pthread_create (& threads [ i ] , NULL , teller_thread , & thread_ids [ i ]) ;
		pthread_create(&threads[i], NULL, teller_thread, &thread_ids[i] );
	}

	// Reference : man pthread_join
	// Question : What happens if you skip this step ?
	for ( int i = 0; i < NUM_THREADS ; i ++) {
		// YOUR pthread_join CODE HERE
		pthread_join(threads[i], NULL);
	}

	// TODO 4: Add mutex cleanup in main ()
	// Reference : man pthread_mutex_destroy
	// Important : Destroy mutexes AFTER all threads complete !
	cleanup_mutexes();

	// TODO 3 f : Calculate and display results
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
	// TODO 3 g : Add race condition detection message
	// If expected != actual , print " RACE CONDITION DETECTED !"
	// Instruct user to run multiple times
	if (actual_total != expected_total) {
		printf("RACE CONDITION DETECTED!\n");
	}
	else {
		printf("NO RACE CONDITION DETECTED.\n");
	}

	clock_gettime(CLOCK_MONOTONIC, &end);
	double elapsed = (end.tv_sec - start.tv_sec) + (end.tv_nsec - start.tv_nsec) /1e9;
	printf("Time: %.4f seconds\n", elapsed);
	return 0;
} // End Main
