#include <mpi.h>
#include <stdio.h>

int main(int argc, char **argv)
{
    // Initialize the MPI environment
    MPI_Init(&argc, &argv);
    // Get the number of processes
    int numprocs;
    MPI_Comm_size(MPI_COMM_WORLD, &numprocs);
    // Get the rank of the process
    int rank;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    // Get the name of the processor
    char processor_name[MPI_MAX_PROCESSOR_NAME];
    int name_len;
    MPI_Get_processor_name(processor_name, &name_len);
    // Print off a hello world message
    printf("Start MPI. Executor=%s, Rank=%d of %d processors\n",
           processor_name, rank, numprocs);
    // Finalize the MPI environment.
    MPI_Finalize();
}