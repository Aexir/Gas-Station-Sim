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
        public Car[] cars;
        public Distributor[] distributors;
        
        public Thread[] carThreads;
        public Thread[] distributorThreads;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Simulation()
        {
            InitializeComponent();
        }


        private void InitializeData()
        {
            nCars = 0;
            cars = new Car[maxCars+5];
            distributors = new Distributor[maxDistributors];


            distributorLocations[0] = new Point(this.Width/3, this.Height-50);
            distributorLocations[1] = new Point(this.Width/3, this.Height -200);
            distributorLocations[2] = new Point(this.Width/3, this.Height-350);
            distributorLocations[3] = new Point(this.Width / 3, this.Height -500);
            distributorLocations[4] = new Point(this.Width / 3, this.Height - 650);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < maxCars+5; i++)
            {
                cars[i].vehicle.Location = cars[i].getPoint();
            }
            textBox1.Text = "PB = " + pbCars + Environment.NewLine + "On = " + onCars + Environment.NewLine + "Cars: " + nCars + "/" + maxCars;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            InitializeData();
            carThreads = new Thread[maxCars+5];
            distributorThreads = new Thread[maxDistributors];

            for (int i = 0; i < maxCars+5; i++)
            {
                cars[i] = new Car(this, resetPanel);
            }

            for (int i = 0; i < maxDistributors; i++)
            {
                distributors[i] = new Distributor(i, resetPanel);
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