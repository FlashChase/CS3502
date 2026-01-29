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
        char sig_message[] = " User initiated shutdown...\n";
        write(STDERR_FILENO, sig_message, sizeof(sig_message)-1);
        shutdown_flag = 1;
    }
    else if (sig == SIGUSR1) {
        sigusr1_flag = 1;
    }
}

int main(int argc, char *argv[]) {
	FILE *read_file;
	int opt;
	int buffer_size = 4096;
	char *filename;
	int filename_input = 0;

	struct sigaction sa;
	sa.sa_handler = handle_sigint;
	sigemptyset(&sa.sa_mask);
	sa.sa_flags = SA_RESTART;
	sigaction(SIGINT, &sa, NULL);
	sigaction(SIGUSR1, &sa, NULL);

	while ((opt = getopt(argc, argv, "f:b:")) != -1) {
        	switch (opt) {
            		case 'f':
                		filename = optarg; // optarg contains the argument value
                		filename_input = 1;
                		break;
            		case 'b':
                		buffer_size = atoi(optarg);
				break;
			default:
                		fprintf(stderr, "Usage: %s [-f file] [-b size]\n", argv[0]);
                		exit(1);
		} // End switch
	} // End getopt while

	while (!shutdown_flag) {
		if (sigusr1_flag) {
			kill(-getpgrp(), SIGUSR1);
			sigusr1_flag = 0;
		}

		char buffer[buffer_size];

		if (filename_input) {
			read_file = fopen(filename, "r");

			if (read_file == NULL) {
				printf("Error reading from file\n");
				filename_input = -1;
			}
			else {
				while ((fgets(buffer, sizeof(buffer), read_file)) != NULL) {
					fprintf(stdout, "%s", buffer);
				}
			}
		}

		if (!filename_input) {
			printf("Enter Input: ");
			while ((fgets(buffer, sizeof(buffer), read_file)) != NULL) {
				fprintf(stdout, "%s", buffer);
			}
		}

		return 0; // <---COMMENT THIS OUT TO TEST SIGINT & SIGUSR1
	} // End main while loop (shutdown_flag)

} // End Main
