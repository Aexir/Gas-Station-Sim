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

        public static int nCars = 0; //current customers on station

        //SEMAPHORES
        public static Semaphore stationEnterance = new Semaphore(1, 1);
        public static Semaphore mutex = new Semaphore(1, 1);

        //INFO
        public static Point enterance = new Point(0, 400);
        public static Point[] cashiers = new Point[maxCashiers];
        public static Point[] distributor = new Point[maxDistributors];
        public static bool[] freeDistributors = new bool[maxDistributors];
    }
}
