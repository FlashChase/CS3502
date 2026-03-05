CC = gcc
CFLAGS = -Wall -pthread

all: phase1 phase2 phase3 phase4

phase1: phase1.c
	$(CC) $(CFLAGS) phase1.c -o phase1

phase2: phase2.c
	$(CC) $(CFLAGS) phase2.c -o phase2

phase3: phase3.c
	$(CC) $(CFLAGS) phase3.c -o phase3

phase4: phase4.c
	$(CC) $(CFLAGS) phase4.c -o phase4

clean:
	rm -f phase1 phase2 phase3 phase4

# In case the above is not correct
# gcc -Wall -pthread phase1.c -o phase1
# gcc -Wall -pthread phase2.c -o phase2
# gcc -Wall -pthread phase3.c -o phase3
# gcc -Wall -pthread phase4.c -o phase4
