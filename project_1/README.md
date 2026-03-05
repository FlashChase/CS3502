# Project 1 Multi-threaded Banking System

## The project consists of four phases

## Phase 1 is designed to show race conditions. 
Actual balance and Expected balance are expected to be wrong.
Run multiple times to see different results.

## Phase 2 introduces mutual exclusion and performance timing.
Notice the actual vs expected balance are now the same.

## Phase 3 is designed to introduce and detect deadlocks.
Phase 3 is not guaranteed to deadlock, but it will most times.
If no progress has been made by any thread for five seconds, the program will end.

## Phase 4 implements deadlock resolution methods.
Phase 4 also implements a test harness to run the program ten times
with each method and display the average results.


## Phase 4 can also accept two arguments
The first argument selects the deadlock resolution method or test harness
The value must be an integer
1 = Lock Ordering (default)
2 = Timed Lock
3 = Try Lock
5 = Test Harness

The second argument selects how long to sleep in microseconds after obtaining the first mutex
This is designed to introduce collisions
 WARNING! With default number of accounts, threads, and transactions,
timed lock will take approximately 7 to 10 minutes to complete.

Example: ./phase4 5 1 (Will run the test harness with 1 microsecond sleep after acquiring first mutex.)

## Author
Chase Connell
