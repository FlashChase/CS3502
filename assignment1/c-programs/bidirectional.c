#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <sys/types.h>
#include <sys/wait.h>


int main() {

	int pipe_pw[2]; // Parent write
	int pipe_cw[2]; // Child write
	pid_t pid;
	char buffer[100];
	char *message = "Clean your room";

	// Create two pipes and check for errors
	if (pipe(pipe_pw) < 0 || pipe(pipe_cw) < 0) {
		perror("Pipe failed");
		exit(1);
	}

	// Fork and check for errors
	pid = fork();
	if (pid == -1) {
		perror("Fork failed");
	}

	if (pid == 0) { // Child

		close(pipe_pw[1]); // Child will not use these
		close(pipe_cw[0]);

		// Read from the buffer and store the result of read
		ssize_t c_read_result = read(pipe_pw[0], buffer, sizeof(buffer));

		// If buffer contained bytes
		// print confirmation message and send return message to parent
		if ( c_read_result > 0 ) {
			printf("Child received message: %s\n", buffer);
			message = "I received my parents message";
			write(pipe_cw[1], message, strlen(message) + 1);
		}
		// If read returned 0, then write end was closed to early.
		else if ( c_read_result == 0) {
			message = "pipe_pw[1] closed too early";
			write(pipe_cw[1], message, strlen(message) + 1);
		}
		// If read result returned < 0 then there is an error. Write message to stderr.
		else {
			message = "Child read error";
			write(STDERR_FILENO, message, strlen(message) + 1);
		}

		// Child is finished
		// Close the pipes it used
		close(pipe_pw[0]);
		close(pipe_cw[1]);
	}
	else { // Parent

		// Close the pipes it won't use
		close(pipe_pw[0]);
		close(pipe_cw[1]);

		// Write message to child
		write(pipe_pw[1], message, strlen(message) + 1);

		// Read pipe_cw to get message from child. Store return from read.
		ssize_t p_read_result = read(pipe_cw[0], buffer, sizeof(buffer));

		// If read returned > 0 then message received. Print message.
		if (p_read_result > 0) {
			printf("Message received from child: %s\n", buffer);
		}

		// If read returned 0 then child's writer closed before sending message
		else if (p_read_result == 0) {
			printf("pipe_cw[1] closed too early");
		}

		// If read returned < 0 (-1) there is an error. Write error to stderr.
		else {
			message = "Parent read error\n";
			write(STDERR_FILENO, message, strlen(message) + 1);
		}

		// Parent if finished. Close the pipes it used.
		close(pipe_pw[1]);
		close(pipe_cw[0]);

		// Wait for child to finish
		wait(NULL);
	}

	return 0;
}
