using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public class Car
    {
        private Simulation sim;
        private Point position;
        public Label vehicle = new Label();
        Random rand = new Random();
        private int tankSize, fuelType, id;
        Panel panel = new Panel();

        public Car(Simulation s, Panel resetPanel)
        {
            sim = s;
            panel = resetPanel;

            vehicle.BackColor = Color.Red;
            vehicle.BringToFront();

            panel.Controls.Add(vehicle);
        }


        public void carAction()
        {
            spawnCar();
            mutex.WaitOne();
            if (nCars < maxCars)
            {
                nCars++;
                mutex.Release();
                // moveToQueue();
                Console.WriteLine("Wejszlo");
                move(new Point(position.X, sim.Height));



                // moveToExit();
            }
            else
            {
                move(new Point(position.X, sim.Height));
                mutex.Release();
            }
            Thread.Sleep(rand.Next(1000, 2000));
            carAction();
        }

        public Point getPoint()
        {
            return new Point(position.X, position.Y);
        }

        public void spawnCar()
        {
            position = new Point(20, 0);
            fuelType = rand.Next(1);
            vehicle.Height = 50;
            vehicle.Width = 25;
            if (fuelType == 0)
            {
                vehicle.Text = "ON";
                vehicle.BackColor = Color.Yellow;
            }
            else
            {
                vehicle.Text = "PB";
                vehicle.BackColor = Color.LightGreen;
            }
        }

        public void move(Point destination)
        {
            while (position.Y > destination.Y)
            {
                position.Y -= 1;
                Thread.Sleep(1);
            }
            while (position.Y < destination.Y)
            {
                position.Y += 1;
                Thread.Sleep(1);
            }
            while (position.X < destination.X)
            {
                position.X += 1;
                Thread.Sleep(1);
            }
            while (position.X > destination.X)
            {
                position.X -= 1;
                Thread.Sleep(1);
            }
        }

        public int getFreeDistributor()
        {
            int i = 0;
            while (true)
            {
                for (i = 0; i < maxDistributors; i++)
                {
                    if (freeDistributors[i] == true)
                    {
                        freeDistributors[i] = false;
                        return i;
                    }
                }
            }
            return i;
        }

        public int getFreeCashier()
        {
            int i = 0;
            bool exists;
            return i;
        }
    }
}