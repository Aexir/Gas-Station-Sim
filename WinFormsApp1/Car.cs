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
        private int tankSize, fuelType, id, distrib;
        Panel panel = new Panel();

        public Car(int id, Simulation s, Panel resetPanel)
        {
            sim = s;
            panel = resetPanel;
            this.id = id;
            vehicle.BackColor = Color.Red;
            vehicle.BringToFront();

            panel.Controls.Add(vehicle);
        }

        public int getTankSize()
        {
            return tankSize;
        }

        public int getFuelType()
        {
            return fuelType;
        }

        public void carAction()
        {
            sim.Invoke(new Action(delegate ()
            {
                spawnCar();
            }));
            Thread.Sleep(rand.Next(500, 2000));

            mutex.WaitOne();
            Thread.Sleep(300);
            if (nCars < maxCars)
            {
                nCars++;
                mutex.Release();

                move(new Point(position.X, sim.Height / 2)); //dojazd do bramy
                move(new Point(position.X + 50, position.Y)); //wjazd

                chosingDistributor.WaitOne();

                distrib = getFreeDistributor();
                distribCarId[distrib] = id;

                chosingDistributor.Release();

                move(new Point(distributorLocations[distrib].X, distributorLocations[distrib].Y+15)); //dojazd do dystrybutora
                if (fuelType == 0)
                {
                    move(new Point(position.X + 60, position.Y));
                }
                dstSem[distrib].Release();
                carSem[id].WaitOne();

                chosingChashier.WaitOne();

                chosingChashier.Release();



                move(new Point(distributorLocations[distrib].X+200, position.Y)); //odjazd 


                //moveToEmptyCash()

                Point x = cashLoc();
                move(new Point(x.X, x.Y + 15));
                move(new Point(x.X+150, x.Y + 15));


                //fuelingDistributor[]
                // moveToExit();

                move(new Point(sim.Width));
                nCars--;
            }
            else
            {
                mutex.Release();
                move(new Point(position.X, sim.Height));
            }
            if (fuelType == 0)
            {
                onCars--;
            }
            else
            {
                pbCars--;
            }
            carAction();

        }

        public Point getPoint()
        {
            return new Point(position.X, position.Y);
        }

        public void spawnCar()
        {
            while (sim == null) {; }
            position = new Point(20, 0);
            fuelType = rand.Next(2);
            vehicle.Height = 40;
            vehicle.Width = 20;
            vehicle.TextAlign = ContentAlignment.BottomLeft;
            tankSize = rand.Next(30, 100);

            if (fuelType == 0)
            {
                vehicle.Text = "ON";
                vehicle.BackColor = Color.Yellow;
                onCars++;
            }
            else
            {
                vehicle.Text = "PB";
                vehicle.BackColor = Color.LightGreen;
                pbCars++;
            }
        }

        public void move(Point destination)
        {

            while (position.Y > destination.Y)
            {
                position.Y -= 1;
                sim.Invoke(new Action(delegate ()
                {
                    vehicle.Height = 40;
                    vehicle.Width = 20;
                }));
                Thread.Sleep(1);
            }



            while (position.Y < destination.Y)
            {
                position.Y += 1;

                sim.Invoke(new Action(delegate ()
                {
                    vehicle.Height = 40;
                    vehicle.Width = 20;
                }));
                Thread.Sleep(1);
            }

            while (position.X < destination.X)
            {
                position.X += 1;
                sim.Invoke(new Action(delegate ()
                {
                    vehicle.Height = 20;
                    vehicle.Width = 40;
                }));
                Thread.Sleep(1);
            }

            while (position.X > destination.X)
            {
                sim.Invoke(new Action(delegate ()
                {
                    position.X -= 1;
                    vehicle.Height = 20;
                    vehicle.Width = 40;
                }));
                Thread.Sleep(1);
            }
        }

        public int getFreeDistributor()
        {
            int i = 0;
            bool exist = false;
            while (!exist)
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

        public Point distLoc()
        {
            int i = rand.Next(0, maxDistributors);
            return distributorLocations[i];
        }
        
        public Point cashLoc()
        {
            int i = rand.Next(0, maxCashiers);
            return cashLocations[i];
        }


        public int getFreeCashier()
        {
            int i = 0;
            bool exists = false;
            while (!exists)
            {

            }
            return i;
        }
    }
}