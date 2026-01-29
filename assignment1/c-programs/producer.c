#include <stddef.h>
#include <getopt.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <signal.h>
#include <unistd.h>

volatile sig_atomic_t shutdown_flag = 0;	// Used to flag SIGINT
volatile sig_atomic_t sigusr1_flag = 0;		// Used to flag SIGUSR1

// Handles SIGINT and SIGUSR1 Signals
void handle_sigint(int sig) {

	// If SIGINT signal sent, Output custom message to stderr, mark shutdown flag, and exit.
	if (sig == SIGINT) {
		char sig_message[] = " User initiated shutdown...\n";
		write(STDERR_FILENO, sig_message, sizeof(sig_message)-1);
		shutdown_flag = 1;
		exit(1);
	}
	// If SIGUSR1 signal received, flag it.
	else if (sig == SIGUSR1) {
	sigusr1_flag = 1;
	}
}

int main(int argc, char *argv[]) {
	FILE *read_file;
	int opt;
	int buffer_size = 4096;	// Default buffer size
	char *filename;
	int filename_input = 0;	// Flag used for if filename is entered or not

	struct sigaction sa;
	sa.sa_handler = handle_sigint;
	sigemptyset(&sa.sa_mask);
	sa.sa_flags = SA_RESTART;	// Had to change this from 0 to SA_RESTART to reliably use SIGUSR1
	sigaction(SIGINT, &sa, NULL);	// Send SIGINT to sig handler method
	sigaction(SIGUSR1, &sa, NULL);	// Send SIGUSR1 to sig handler method

	while ((opt = getopt(argc, argv, "f:b:")) != -1) {
        	switch (opt) {
            		case 'f':
                		filename = optarg; // optarg contains the argument value
                		filename_input = 1;// flag as file name entered
                		break;
            		case 'b':
                		buffer_size = atoi(optarg); // Save user input buffer size
				break;
			default:
                		fprintf(stderr, "Usage: %s [-f file] [-b size]\n", argv[0]);
                		exit(1);
		} // End switch
	} // End getopt while

	// While loop is really just for testing SIGINT & SIGUSR1. Comment out the return to get an infinite loop until SIGINT
	while (!shutdown_flag) {

		// Test if SIGUSR1 is flagged
		if (sigusr1_flag) {
			kill(-getpgrp(), SIGUSR1);	// Send SIGUSR1 signal to process group (to pass to child)
			sigusr1_flag = 0;		// Reset SIGUSR1 flag
		}

		char buffer[buffer_size];	// Create buffer using default or user input buffer size

		// If user input a file name
		if (filename_input) {
			// Open file in read mode
			read_file = fopen(filename, "r");

			// If read fails, Print error message, flag as no filename entered
			if (read_file == NULL) {
				printf("Error reading from file\n");
				filename_input = -1;
			}
			// If file open was successful
			else {
				// Read buffer sized sections and print to stdout until eof
				while ((fgets(buffer, sizeof(buffer), read_file)) != NULL) {
					fprintf(stdout, "%s", buffer);
				}
			}
		}
		// If no filename input, allow manual input
		if (!filename_input) {
			printf("Enter Input: ");
			while ((fgets(buffer, sizeof(buffer), read_file)) != NULL) {
				fprintf(stdout, "%s", buffer);
			}
		}

		return 0; // <---COMMENT THIS OUT TO TEST SIGINT & SIGUSR1
	} // End main while loop (shutdown_flag)

} // End Main
