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

        public enum Status
        {
            EMPTY,
            LOCKED,
            IN_USE
        }


        //BASIC
        public static int maxCars = 10;
        public static int maxCashiers = 5;
        public static int maxDistributors = 5;

        public static int nCars = 0; 

        public static int pbCars = 0;
        public static int onCars = 0;


        //SEMAPHORES
        public static Semaphore mutex = new Semaphore(1, 1); //zliczanie samochodow 

        public static Semaphore[] carSem = new Semaphore[20];
        public static Semaphore[] dstSem = new Semaphore[20];
        public static Semaphore[] cashSem = new Semaphore[20];

        public static Semaphore chosingDistributor = new Semaphore(1, 1);
        public static Semaphore chosingChashier = new Semaphore(1, 1);
        public static Semaphore stationEnterance = new Semaphore(1, 1);

        public static Semaphore[] fuelingDistributor = new Semaphore[20];

        //INFO
        public static Point enterance = new Point(0, 900);
        public static Point[] cashLocations;
        public static Point[] distributorLocations;

        public static bool[] freeDistributors = new bool[20];
        public static bool[] freeCashiers = new bool[20];

        public static int[] distribCarId = new int[20];
        public static int[] cashDistribId = new int[20];

        public StaticVariables()
        {
            for (int i = 0; i < 20; i++)
            {
                carSem[i] = new Semaphore(0, 20);
                dstSem[i] = new Semaphore(0, 20);
                cashSem[i] = new Semaphore(0, 20);

                fuelingDistributor[i] = new Semaphore(0, 20);
            }

            for (int i = 0; i < maxDistributors; i++)
            {
                freeDistributors[i] = false;
            }
            for (int i = 0; i < maxCashiers; i++)
            {
                freeCashiers[i] = false;
            }
        }
    }
}
