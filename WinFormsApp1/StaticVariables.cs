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

        public static int pbCars = 0;
        public static int onCars = 0;


        //SEMAPHORES
        public static Semaphore mutex = new Semaphore(1, 1); //zliczanie samochodow 


        public static Semaphore stationEnterance = new Semaphore(1, 1);

        //INFO
        public static Point enterance = new Point(0, 900);
        public static Point[] cashLocations;
        public static Point[] distributorLocations;
        public static bool[] freeDistributors;
    }
}
