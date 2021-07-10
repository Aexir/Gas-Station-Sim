using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public static List<int> carsInQueue = new List<int>();
        public static List<int> carsOut = new List<int>();
        public static List<int> carFueling = new List<int>();

        //SEMAPHORES
        public static SemaphoreSlim mutex = new SemaphoreSlim(1, 1); //zliczanie samochodow 

        public static SemaphoreSlim[] carSem = new SemaphoreSlim[20];
        public static SemaphoreSlim[] dstSem = new SemaphoreSlim[20];
        public static SemaphoreSlim[] cashSem = new SemaphoreSlim[20];

        public static SemaphoreSlim[] paySem = new SemaphoreSlim[20];

        public static SemaphoreSlim chosingDistributor = new SemaphoreSlim(1, 1);
        public static SemaphoreSlim chosingChashier = new SemaphoreSlim(1, 1);

        public static Mutex refuelingMutex = new Mutex();
        public static Mutex counter = new Mutex();

        //INFO
        public static Point enterance = new Point(0, 900);
        public static Point[] cashLocations;
        public static Point[] distributorLocations;

        public static bool[] freeDistributors = new bool[20];
        public static bool[] freeOnDistributors = new bool[20];
        public static bool[] freePbDistributors = new bool[20];

        public static bool[] freeCashiers = new bool[20];

        public static int[] distribCarId = new int[20];
        public static int[] cashDistribId = new int[20];

        public static bool refueling = false;

        public StaticVariables()
        {
            for (int i = 0; i < 20; i++)
            {
                carSem[i] = new SemaphoreSlim(0, 20);
                dstSem[i] = new SemaphoreSlim(0, 20);
                cashSem[i] = new SemaphoreSlim(0, 20);
                paySem[i] = new SemaphoreSlim(0, 20);
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
