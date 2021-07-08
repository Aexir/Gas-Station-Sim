using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public class Distributor
    {

        private Point position;
        Random rand = new Random();
        private int id, onTank, pbTank;
        public Label dst = new Label();
        public Label onLBL = new Label();
        public Label pbLbl = new Label();
        public TextBox txt = new TextBox();
        public ProgressBar pb = new ProgressBar();
        public ProgressBar on = new ProgressBar();
        Panel panel = new Panel();
        private bool inUse;



        public Distributor(int id, Panel resetPanel)
        {
            this.id = id;
            this.panel = resetPanel;

            onTank = 1000;
            pbTank = 1000;
            inUse = false;


            position = distributorLocations[id];

            //PB
            pb.Location = distributorLocations[id];
            pb.Width = 50;
            pb.Height = 10;
            pb.Maximum = 1000;
            pb.Value = pbTank;

            //ON
            on.Location = new Point(distributorLocations[id].X + pb.Width + 5, distributorLocations[id].Y);
            on.Width = 50;
            on.Height = 10;
            on.Maximum = 1000;
            on.Value = onTank;
            //TEXT
            txt.Text = "0";
            txt.TextAlign = HorizontalAlignment.Center;
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

        public void createDistributor()
        {

        }

        public Point getLocation()
        {
            return new Point(position.X, position.Y);
        }

        public bool isInUse()
        {
            return inUse;
        }

        public void use()
        {
            inUse = true;
        }

        public void leave()
        {
            inUse = false;
        }


    }
}