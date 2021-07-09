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
        public  Label vehicle = new Label();
        private Random rand = new Random();
        private int tankSize, fuelType, id, distrib;
        private Panel panel = new Panel();

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

           // refuelingStation[id].Wait();
            mutex.Wait();
            Thread.Sleep(rand.Next(500, 1000));

            Thread.Sleep(300);
            if (nCars < maxCars && refueling == false)
            {
                nCars++;
                carsInQueue.Add(id);
                if (fuelType == 0)
                {
                    onCars++;
                }
                else
                {
                    pbCars++;
                }
                mutex.Release();

                move(new Point(position.X, sim.Height / 2)); //dojazd do bramy
                move(new Point(position.X + 50, position.Y)); //wjazd

                carsInQueue.Remove(id);

                //Wybor wolnego dystrybutora
                chosingDistributor.Wait();
                distrib = getFreeDistributor();

                distribCarId[distrib] = id;
                chosingDistributor.Release();

                //dojazd do dystrybutora
                move(new Point(distributorLocations[distrib].X, distributorLocations[distrib].Y+15)); 
                if (fuelType == 0)
                {
                    move(new Point(position.X + 60, position.Y));
                }

                //tankowanie
                dstSem[distrib].Release(); 
                carSem[id].Wait();

                carsOut.Add(id);
                //wybor wolnej kasy
                chosingChashier.Wait();
                int cash = getFreeCashier();
                cashDistribId[cash] = distrib;
                chosingChashier.Release();

                //dojazd do kasy
                move(new Point(position.X+200, position.Y)); 
                move(new Point(cashLocations[cash].X, cashLocations[cash].Y + 15));


                //placenie
                cashSem[cash].Release();
                carSem[id].Wait();
                paySem[cash].Release();
                carSem[id].Wait();
                //wyjazd z budowy
                move(new Point(position.X+150, position.Y));
                move(new Point(sim.Width));
                carsOut.Remove(id);
                nCars--;
                if (fuelType == 0)
                {
                    onCars--;
                }
                else
                {
                    pbCars--;
                }
            }
            else
            {
                if (nCars == 0 && refueling == true)
                {
                    foreach (Distributor ddd in distributors)
                    {
                        ddd.refuel();
                        freeDistributors[ddd.getId()] = true;
                        freeOnDistributors[ddd.getId()] = true;
                        freePbDistributors[ddd.getId()] = true;

                        refuelingMutex.Wait();
                        refueling = false;
                        refuelingMutex.Release();
                    }
                }
                mutex.Release();
                move(new Point(position.X, sim.Height));
            }
            carAction();
        }

        public Point getPoint()
        {
            return new Point(position.X, position.Y);
        }

        public void spawnCar()
        {
            position = new Point(20, 0);
            fuelType = rand.Next(2);
            vehicle.Height = 40;
            vehicle.Width = 20;
            vehicle.TextAlign = ContentAlignment.BottomLeft;
            tankSize = rand.Next(30, 100);

            if (fuelType == 0)
            {
                vehicle.Text = "ON " +id;
                vehicle.BackColor = Color.Yellow;
            }
            else
            {
                vehicle.Text = "PB " + id;
                vehicle.BackColor = Color.LightGreen;
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
            while (true)
            {
                int i;
                for (i = 0; i < maxDistributors; i++)
                {
                    if (freeDistributors[i] == true)
                    {
                        if (fuelType == 0)
                        {
                            if (freeOnDistributors[i] == true)
                            {
                                freeDistributors[i] = false;
                                return i;
                            }
                        }
                        else
                        {
                            if (freePbDistributors[i] == true)
                            {
                                freeDistributors[i] = false;
                                return i;
                            }
                        }
                    }
                }
            }
        }

        public int getFreeCashier()
        {
            while (true)
            {
                int i;
                for (i = 0; i < maxCashiers; i++)
                {
                    if (freeCashiers[i] == true)
                    {
                        freeCashiers[i] = false;
                        return i;
                    }
                }
            }
        }
    }
}