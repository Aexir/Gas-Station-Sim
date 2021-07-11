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
        private Random rand = new Random();
        private int tankSize, fuelType, id;
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
            spawnCar();

            mutex.Wait();

            Thread.Sleep(rand.Next(500, 1000));

            if (!refueling)
            {
                if (nCars < maxCars)
                {
                    increaseCarCounter();
                    carsInQueue.AddFirst(id);
                    mutex.Release();

                    moveToEnterance();
                    moveToQueue();

                    chosingDistributor.Wait();
                    int distrib = getDistributor();
                    if (distrib == maxDistributors)
                    {
                        decreaseCarCounter();
                        carsInQueue.Remove(id);
                        chosingDistributor.Release();
                        driveAway();
                    }
                    else
                    {
                        distribCarId[distrib] = id;
                        carsInQueue.Remove(id);
                        carsRefueling.AddFirst(id);

                        chosingDistributor.Release();

                        moveToDistriutor(distrib); //dojazd do dystrybutora

                        dstSem[distrib].Release(); //tankowanie

                        carSem[id].Wait(); //po tankowaniu

                        chosingChashier.Wait(); //
                        carsRefueling.Remove(id);

                        carsPaying.AddFirst(id);

                        int cashier = getFreeCashier();

                        cashDistribId[cashier] = distrib;
                        chosingChashier.Release();
                        //dojazd do kasy
                        move(new Point(position.X + 200, position.Y));
                        move(new Point(cashLocations[cashier].X, cashLocations[cashier].Y + 15));


                        //placenie
                        cashSem[cashier].Release();
                        carSem[id].Wait();
                        cashSem[cashier].Release();
                        carSem[id].Wait();


                        leaveStation();   //wyjazd z budowy
                        carsPaying.Remove(id);
                        decreaseCarCounter();
                    }
                }
                else
                {
                    mutex.Release();
                    move(new Point(position.X, sim.Height));
                }
            }
            else
            {
                mutex.Release();
                move(new Point(position.X, sim.Height));
                if (nCars == 0)
                {
                    refuelMutex.Wait();
                    for (int i = 0; i < maxDistributors; i++)
                    {
                        distributors[i].refuel();
                        freeDistributors[i] = true;
                        freeOnDistributors[i] = true;
                        freePbDistributors[i] = true;
                    }
                    refuelMutex.Release();
                    refueling = false;
                }
            }
            carAction();
        }

        public void leaveStation()
        {
            move(new Point(position.X + 150, position.Y));              
            move(new Point(sim.Width));
            move(new Point(position.X + 100, position.Y));
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

        public void moveToDistriutor(int station)
        {
            if (fuelType == 0)
            {
                move(new Point(distributorLocations[station].X + 50, distributorLocations[station].Y + 15));
            }
            else
            {
                move(new Point(distributorLocations[station].X, distributorLocations[station].Y + 15));
            }

        }
        public Point getPoint()
        {
            return new Point(position.X, position.Y);
        }

        public void spawnCar()
        {
            sim.Invoke(new Action(delegate ()
            {
                position = new Point(20, 0);
                fuelType = rand.Next(2);

                vehicle.Height = 40;
                vehicle.Width = 20;
                vehicle.TextAlign = ContentAlignment.BottomLeft;
                tankSize = rand.Next(30, 100);

                if (fuelType == 0)
                {
                    vehicle.Text = "ON " + id;
                    vehicle.BackColor = Color.Yellow;
                }
                else
                {
                    vehicle.Text = "PB " + id;
                    vehicle.BackColor = Color.LightGreen;
                }
            }));
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

        public void moveToQueue()
        {
            move(new Point(position.X + 100, position.Y));
        }

        public void moveToEnterance()
        {
            move(new Point(position.X, sim.Height / 2));
        }

        public void driveAway()
        {
            sim.Invoke(new Action(delegate ()
            {
                vehicle.BackColor = Color.DarkOrange;
            }));

            move(new Point(distributorLocations[maxDistributors-1].X, distributorLocations[maxDistributors-1].Y + 15));
            move(new Point(position.X + 200, position.Y));
            move(new Point(position.X + 200, position.Y));
            move(new Point(cashLocations[maxCashiers-1].X, cashLocations[maxCashiers-1].Y + 15));
            move(new Point(position.X + 150, position.Y));
            move(new Point(sim.Width));
        }

        public void moveToCashier(int cashId)
        {
            move(new Point(position.X + 300, position.Y));
            move(new Point(cashLocations[cashId].X, cashLocations[cashId].Y - 15));
        }
        public void increaseCarCounter()
        {
            nCars++;
            if (fuelType == 0)
            {
                onCars++;
            }
            else
            {
                pbCars++;
            }
        }

        public void decreaseCarCounter()
        {
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

        public int getDistributor()
        {
            while (true)
            {
                for (int i = 0; i < maxDistributors; i++)
                {
                    if (!refueling)
                    {
                        if (freeDistributors[i] == true)
                        {
                            if (fuelType == 0 && freeOnDistributors[i] == true)
                            {
                                freeDistributors[i] = false;
                                return i;
                            }
                            if (fuelType != 0 && freePbDistributors[i] == true)
                            {
                                freeDistributors[i] = false;
                                return i;
                            }
                        }
                    }
                    else
                    {
                        return maxDistributors;
                    }
                }
            }
        }
    }
}