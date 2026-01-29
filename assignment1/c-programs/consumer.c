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
	// IF SIGINT signal received mark shutdown flag & exit. (Output message already sent from producer)
	if (sig == SIGINT) {
		// char sig_message[] = " User initiated shutdown...\n";
		// write(STDERR_FILENO, sig_message, sizeof(sig_message) - 1);
		shutdown_flag = 1;
		exit(1);
	}
	// IF SIGUSR1 signal received, flag it
	else if (sig == SIGUSR1) {
	sigusr1_flag = 1;
	}
}


int main(int argc, char *argv[]) {

	int opt;                    // Parse parameters
	int verbose;                // Flag if user entered verbose mode (print lines read)
	int line_limit = -2;        // Tracks user entered limit of total lines to read
	int is_line_limit = 0;      // Flags if a limit exists for total lines to read
	int line_count = 0;         // Tracks total lines read from stdin
	int char_count = 0;         // Tracks total chars read from stdin
	int sentinel = 0;           // Ends while loop
	ssize_t line_bytes;         // Stores number of characters (bytes) on each line
	char *line = NULL;          // Pointer for getline()
	size_t buf = 0;             // Allocate memory for getline()

	struct sigaction sa;
	sa.sa_handler = handle_sigint;
	sigemptyset(&sa.sa_mask);
	sa.sa_flags = SA_RESTART;	// Had to change this from 0 to SA_RESTART to reliably call SIGUSR1 multiple times
	sigaction(SIGINT, &sa, NULL);	// Send SIGINT to sig handler method
	sigaction(SIGUSR1, &sa, NULL);	// Send SIGUSR1 to sig handler method

	while ((opt = getopt(argc, argv, "n:v")) != -1) {
		switch (opt) {
			case 'n':
				line_limit = argc;     // Get user input line limit
				is_line_limit = 1;     // Flag that line limit exists
				break;

			case 'v':
				verbose = 1;           // Flag verbose mode selected
				break;

			default:
				exit(1);
		} // End switch
	} // End while

	// While SIGINT signal not received and the sentinel has not been flagged
	while (!shutdown_flag && !sentinel) {
		// If SIGUSR1 is flagged, reset flag, Print line and character count
		if (sigusr1_flag) {
			sigusr1_flag = 0;
			fprintf(stderr, "Lines: %d | Characters: %d\n", line_count, char_count);
		}

		if (is_line_limit) {                    // If a line limit exists
			if (line_count >= line_limit - 1) { // If line counter equals line limit
				sentinel = 1;                   // Line limit reached, flag sentinel
			}
		}

		// Read line of stdin and save char count(bytes)
		line_bytes = getline(&line, &buf, stdin);

		// If stdin reads a valid line (getline returns -1 for eof)
		if (line_bytes != -1) {
			line_count++;                       // Increment line count
			char_count += line_bytes;           // Add char count of line

			if (verbose) {
				printf("%s", line);             // If verbose option entered
			}
		}
		// If stdin is empty, flag sentinel, print final line and character count
		// Count includes newline characters. Must subtract line_count from char_count if you don't want them included
		else {
			sentinel = 1;
			fprintf(stderr, "Lines: %d | Characters: %d\n", line_count, char_count);
		}
	} // End sentinel loop
	return 0;
} // End main
