using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public class Distributor
    {
        private Random rand = new Random();
        private int id, onTank, pbTank, maxOnTank, maxPbTank;
        private double amount;
        private Label dst = new Label();
        private Label onLBL = new Label();
        private Label pbLbl = new Label();
        private TextBox txt = new TextBox();
        private ProgressBar pb = new ProgressBar();
        private ProgressBar on = new ProgressBar();
        private Panel panel = new Panel();

        public double getAmount()
        {
            return amount;
        }

        public int getId()
        {
            return id;
        }
        public void resetDistributor()
        {
            panel.Invoke(new Action(delegate ()
            {
                this.amount = 0;
                txt.Text = "0.00";
            }));
        }

        public Distributor(int id, Panel resetPanel)
        {
            this.id = id;
            this.panel = resetPanel;

            maxOnTank = onTank = 100;
            maxPbTank = pbTank = 100;

            freeDistributors[id] = true;
            freePbDistributors[id] = true;
            freeOnDistributors[id] = true;

            //PB
            pb.Location = distributorLocations[id];
            pb.Width = 50;
            pb.Height = 10;
            pb.Maximum = pbTank;
            pb.Value = pbTank;

            //ON
            on.Location = new Point(distributorLocations[id].X + pb.Width + 5, distributorLocations[id].Y);
            on.Width = 50;
            on.Height = 10;
            on.Maximum = onTank;
            on.Value = onTank;
            //TEXT
            txt.Text = "0.00";
            txt.TextAlign = HorizontalAlignment.Center;
            txt.Multiline = true;
            txt.Height = 50;
            txt.Width = 105;
            txt.Location = new Point(distributorLocations[id].X, distributorLocations[id].Y - txt.Height - on.Height + 10); ;
            //PB
            pbLbl.Text = "PB";
            pbLbl.BackColor = Color.LightGreen;
            pbLbl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            pbLbl.TextAlign = ContentAlignment.MiddleCenter;
            pbLbl.Height = 25;
            pbLbl.Width = 50;
            pbLbl.Location = new Point(distributorLocations[id].X, distributorLocations[id].Y - txt.Height - on.Height - pbLbl.Height + 7);
            //ON
            onLBL.Text = "ON";
            onLBL.BackColor = Color.LightYellow;
            onLBL.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            onLBL.TextAlign = ContentAlignment.MiddleCenter;
            onLBL.Height = 25;
            onLBL.Width = 50;
            onLBL.Location = new Point(distributorLocations[id].X + pbLbl.Width + 5, distributorLocations[id].Y - txt.Height - on.Height - pbLbl.Height + 7);
            //TXT
            dst.Text = "Dystrybutor nr " + (id + 1);
            dst.BackColor = Color.LightGray;
            dst.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dst.TextAlign = ContentAlignment.MiddleCenter;
            //dst.AutoSize = true;
            dst.Height = 30;
            dst.Width = 105;
            dst.Location = new Point(distributorLocations[id].X, distributorLocations[id].Y - txt.Height - on.Height - pbLbl.Height - 25);



            resetPanel.Controls.Add(dst);
            resetPanel.Controls.Add(onLBL);
            resetPanel.Controls.Add(pbLbl);
            resetPanel.Controls.Add(txt);
            resetPanel.Controls.Add(on);
            resetPanel.Controls.Add(pb);
        }

        public void distributorAction()
        {
            while (true)
            {
                refCount.Wait();
                needRefuel();
                refCount.Release();

                dstSem[id].Wait();
                int carId = distribCarId[id];

                carFueling(carId);
                carSem[carId].Release();

            }
        }


        public void needRefuel()
        {
            int needRef = 0;
            for (int i = 0; i < maxDistributors; i++)
            {
                if (freePbDistributors[i] == false)
                {
                    needRef++;
                }
                if (freeOnDistributors[i] == false)
                {
                    needRef++;
                }
            }
            if (needRef >= maxDistributors / 2)
            {
                refueling = true;
            }
        }

        public void carFueling(int carId)
        {
            int carTankSize = cars[carId].getTankSize();
            int carFuelType = cars[carId].getFuelType();

            for (int i = 0; i < carTankSize; i++)
            {
                if (carFuelType == 0) //ON
                {
                    if (onTank == 0)
                    {
                        panel.Invoke(new Action(delegate ()
                        {
                            onLBL.BackColor = Color.Red;
                        }));
                        freeOnDistributors[id] = false;
                        break;
                    }
                    panel.Invoke(new Action(delegate ()
                    {
                        on.Value -= 1;
                        onTank -= 1;
                        amount = Math.Round(4.30 * i, 2);
                        txt.Text = "Ilosc: " + i + Environment.NewLine + "Koszt: " + amount;
                    }));

                }
                else //PB
                {
                    if (pbTank == 0)
                    {
                        panel.Invoke(new Action(delegate ()
                        {
                            pbLbl.BackColor = Color.Red;
                        }));
                        freePbDistributors[id] = false;
                        break;
                    }
                    panel.Invoke(new Action(delegate ()
                    {
                        pb.Value -= 1;
                        pbTank -= 1;
                        amount = Math.Round(4.30 * i, 2);
                        txt.Text = "Ilosc: " + i + Environment.NewLine + "Koszt: " + amount;
                    }));

                }
                Thread.Sleep(rand.Next(carTankSize));
            }
        }

        public void refuel()
        {
            for (int k = 0; k < maxDistributors; k++)
            {
                panel.Invoke(new Action(delegate ()
                {
                    txt.Text = "Uzupelnianie";
                }));


                for (int j = 0; j < maxOnTank; j++)
                {
                    if (on.Value == maxOnTank) break;
                    panel.Invoke(new Action(delegate ()
                    {

                        if (on.Value <= 99)
                        {
                            onTank += 1;
                        }
                        on.Value += 1;
                    }));

                    Thread.Sleep(1);
                }
                for (int j = 0; j < maxPbTank; j++)
                {
                    if (pb.Value == maxPbTank) break;

                    panel.Invoke(new Action(delegate ()
                    {
                        if (pb.Value <= 99)
                        {
                            pb.Value += 1;
                        }
                        pbTank += 1;
                    }));
                    Thread.Sleep(1);
                }

                panel.Invoke(new Action(delegate ()
                {
                    txt.Text = "0.00";
                    pbLbl.BackColor = Color.LightGreen;
                    onLBL.BackColor = Color.LightYellow;
                }));


            }
            Thread.Sleep(10);
        }
    }
}