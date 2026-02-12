// Chase Connell
// CS3502
// Section W03

// ============================================
// consumer.c - Consumer process starter
// ============================================
#include "buffer.h"

// Global variables for cleanup
shared_buffer_t* buffer = NULL;
sem_t* mutex = NULL;
sem_t* empty = NULL;
sem_t* full = NULL;
int shm_id = -1;

void cleanup() {
    // Detach shared memory
    if (buffer != NULL) {
        shmdt(buffer);
    }
    
    // Close semaphores
    if (mutex != SEM_FAILED) sem_close(mutex);
    if (empty != SEM_FAILED) sem_close(empty);
    if (full != SEM_FAILED) sem_close(full);
}

void signal_handler(int sig) {
    printf("\nConsumer: Caught signal %d, cleaning up...\n", sig);
    cleanup();
    exit(0);
}

int main(int argc, char* argv[]) {
    if (argc != 3) {
        fprintf(stderr, "Usage: %s <consumer_id> <num_items>\n", argv[0]);
        exit(1);
    }
    
    int consumer_id = atoi(argv[1]);
    int num_items = atoi(argv[2]);
    
    // Set up signal handlers
    signal(SIGINT, signal_handler);
    signal(SIGTERM, signal_handler);
    
    // Seed random number generator
    srand(time(NULL) + consumer_id * 100);
    
    // TODO: Attach to shared memory
    int shm_id = shmget(SHM_KEY, sizeof(shared_buffer_t), IPC_CREAT | 0666);
    if (shm_id == -1) {
	perror("shmeget failed in consumer");
	exit(1);
    }

    buffer = shmat(shm_id, NULL, 0);

    // TODO: Open semaphores (don't use O_CREAT - producer creates them)
    full = sem_open("/full_sem", 0);
    empty = sem_open("/empty_sem", 0);
    mutex = sem_open("/mutex_sem", 0);

    printf("Consumer %d: Starting to consume %d items\n", consumer_id, num_items);
    
    // TODO: Main consumption loop
    for (int i = 0; i < num_items; i++) {

        // TODO: Wait for full slot
        sem_wait(full);

        // TODO: Enter critical section
        sem_wait(mutex);

        // TODO: Remove item from buffer
        item_t collected_item = buffer->buffer[buffer->tail];	// Pointer to tail of buffer and variable equal to it

	printf("Consumer %d: Consumed value %d from Producer %d\n", consumer_id, collected_item.value, collected_item.producer_id);

	buffer->tail = (buffer->tail + 1) % BUFFER_SIZE;	// Move the tail of buffer to the next item (Or wrap around if necessary)

        // TODO: Exit critical section
        sem_post(mutex); 
        // TODO: Signal empty slot
        sem_post(empty);
        // Simulate consumption time
        usleep(rand() % 100000);
    }
    
    printf("Consumer %d: Finished consuming %d items\n", consumer_id, num_items);
    cleanup();
    return 0;
}
