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
    public partial class Simulation : Form
    {
        public Car[] cars;
        public Thread[] carThreads;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Simulation()
        {
            InitializeComponent();
        }


        private void InitializeData()
        {
            nCars = 0;
            cars = new Car[maxCars];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < maxCars; i++)
            {
                cars[i].vehicle.Location = cars[i].getPoint();
            }
            textBox1.Text = "PB = " + pbCars + "\nOn = " + onCars;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            nCars = 0;
            btnStart.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            InitializeData();
            carThreads = new Thread[maxCars];

            for (int i = 0; i < maxCars; i++)
            {
                cars[i] = new Car(this, resetPanel);
            }

            for (int i = 0; i < maxCars; i++)
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
    }
}