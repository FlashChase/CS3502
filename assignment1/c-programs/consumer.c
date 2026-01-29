#include <stddef.h>
#include <getopt.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <signal.h>
#include <unistd.h>

volatile sig_atomic_t shutdown_flag = 0;
volatile sig_atomic_t sigusr1_flag = 0;

void handle_sigint(int sig) {
    if (sig == SIGINT) {
       // char sig_message[] = " User initiated shutdown...\n";
       // write(STDERR_FILENO, sig_message, sizeof(sig_message) - 1);
        shutdown_flag = 1;
    }
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

	struct sigaction sa;
	sa.sa_handler = handle_sigint;
	sigemptyset(&sa.sa_mask);
	sa.sa_flags = SA_RESTART;
	sigaction(SIGINT, &sa, NULL);
	sigaction(SIGUSR1, &sa, NULL);

	while (!shutdown_flag && !sentinel) {
		if (sigusr1_flag) {
			sigusr1_flag = 0;
			fprintf(stderr, "Lines: %d | Characters: %d\n", line_count, char_count);
		}

		if (is_line_limit) {                    // If a line limit exists
			if (line_count >= line_limit - 1) { // If line counter equals line limit
				sentinel = 1;                   // Line limit flag sentinel
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
		else {
			sentinel = 1;                       // If stdin is empty
			fprintf(stderr, "Lines: %d | Characters: %d\n", line_count, char_count);
		}
	} // End sentinel loop
	return 0;
} // End main
