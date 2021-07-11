using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace WinFormsApp1
{
    public class StaticVariables
    {
        //CORE
        public static Car[] cars;
        public static Distributor[] distributors;
        public static Cash[] cashiers;

        public static Thread[] carThreads;
        public static Thread[] distributorThreads;
        public static Thread[] cashierThreads;


        //BASIC
        public static int maxCars = 10;
        public static int maxCashiers = 5;
        public static int maxDistributors = 5;

        public static int nCars = 0;
        public static int pbCars = 0;
        public static int onCars = 0;

        public static bool refueling = false;

        public static LinkedList<int> carsInQueue = new();
        public static LinkedList<int> carsPaying = new();
        public static LinkedList<int> carsRefueling = new();

        //SEMAPHORES
        public static SemaphoreSlim enterLeaveStation = new(1, 1); //zliczanie samochodow 

        public static SemaphoreSlim[] carSem = new SemaphoreSlim[20]; 
        public static SemaphoreSlim[] dstSem = new SemaphoreSlim[10];
        public static SemaphoreSlim[] cashSem = new SemaphoreSlim[10];

        public static SemaphoreSlim chosingDistributor = new(1, 1); 
        public static SemaphoreSlim chosingChashier = new(1, 1);

        public static SemaphoreSlim refCount = new(1, 1);
        public static SemaphoreSlim refuelMutex = new(1, 1);


        //INFO
        public static Point[] cashLocations;
        public static Point[] distributorLocations;

        public static bool[] freeDistributors = new bool[4];
        public static bool[] freeOnDistributors = new bool[4];
        public static bool[] freePbDistributors = new bool[4];

        public static bool[] freeCashiers = new bool[6];

        public static int[] distribCarId = new int[20];
        public static int[] cashDistribId = new int[10];

        public StaticVariables()
        {
            for (int i = 0; i < 20; i++)
            {
                carSem[i] = new (0, 1);
            }

            for (int i = 0; i < 10; i++)
            {
                dstSem[i] = new (0, 1);
                cashSem[i] = new (0, 1);
            }

            for (int i = 0; i < maxDistributors; i++)
            {
                freeDistributors[i] = false;
                freePbDistributors[i] = false;
                freeOnDistributors[i] = false;
            }
            for (int i = 0; i < maxCashiers; i++)
            {
                freeCashiers[i] = false;
            }
        }
    }
}
