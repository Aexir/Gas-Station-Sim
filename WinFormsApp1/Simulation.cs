﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public partial class Simulation : Form
    {


        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Simulation()
        {
            InitializeComponent();
        }


        private void InitializeData()
        {
            nCars = 0;
            StaticVariables vars = new StaticVariables();
            cashLocations = new Point[maxCashiers];
            distributorLocations = new Point[maxDistributors];
            freeDistributors = new bool[maxDistributors];


            cars = new Car[maxCars+5];
            distributors = new Distributor[maxDistributors];
            cashiers = new Cash[maxCashiers];

            for (int i = 0; i < maxDistributors; i++)
            {
                if ( i== 0)distributorLocations[0] = new Point(this.Width / 3, this.Height - 50);
                else
                {
                    distributorLocations[i] = new Point(this.Width / 3, this.Height - 50 - 150 * i);
                }
            }
           
            for (int i = 0; i < maxCashiers; i++)
            {
                if (i == 0) cashLocations[0] = new Point(this.Width/2 + 300, this.Height - 50);
                else
                {
                    cashLocations[i] = new Point(this.Width/2 + 300, this.Height - 50 - 100 * i);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < maxCars+5; i++)
            {
                cars[i].vehicle.Location = cars[i].getPoint();
            }
            textBox1.Text = "PB = " + pbCars + Environment.NewLine + "On = " + onCars + Environment.NewLine + "Cars: " + nCars + "/" + maxCars
                + Environment.NewLine + "W: ";
            for (int i = 0; i < carsInQueue.Count; i++)
            {
                textBox1.Text += carsInQueue[i] + " ";
            }
            textBox1.Text += Environment.NewLine + "O: ";
            for (int i = 0; i < carsOut.Count; i++)
            {
                textBox1.Text += carsOut[i]+ " ";
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            InitializeData();
            carThreads = new Thread[maxCars+5];
            distributorThreads = new Thread[maxDistributors];
            cashierThreads = new Thread[maxCashiers];

            for (int i = 0; i < maxDistributors; i++)
            {
                distributors[i] = new Distributor(i, resetPanel);
            }
            for (int i = 0; i < maxCashiers; i++)
            {
                cashiers[i] = new Cash(i, resetPanel);
            }
            for (int i = 0; i < maxCars+5; i++)
            {
                cars[i] = new Car(i, this, resetPanel);
            }

            for (int i = 0; i < maxDistributors; i++)
            {
                distributorThreads[i] = new Thread(new ThreadStart(distributors[i].distributorAction));
                distributorThreads[i].IsBackground = true;
            }

            for (int i = 0; i < maxCashiers; i++)
            {
                cashierThreads[i] = new Thread(new ThreadStart(cashiers[i].cashierAction));
                cashierThreads[i].IsBackground = true;
            }

            for (int i = 0; i < maxCars+5; i++)
            {
                carThreads[i] = new Thread(new ThreadStart(cars[i].carAction));
                carThreads[i].IsBackground = true;
            }


            timer.Interval = 10;
            timer.Tick += timer1_Tick;
            timer.Start();
            timer.Enabled = true;
            foreach (Thread thread in carThreads)
            {
                thread.Start();
            }

            foreach (Thread thread in distributorThreads)
            {
                thread.Start();
            }

            foreach (Thread thread in cashierThreads)
            {
                thread.Start();
            }
        }

        private void onClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Location.ToString());
        }

        private void onClose(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}