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
    public partial class Distributor : UserControl
    {

        private Point position;
        Random rand = new Random();
        private int id, onTank, pbTank;
        private Label dst = new Label();
        private TextBox txt = new TextBox();
        private ProgressBar pb = new ProgressBar();
        private ProgressBar on = new ProgressBar();
        private PictureBox pi = new PictureBox();
        Panel panel = new Panel();
        private bool inUse;



        public Distributor(int id, Panel resetPanel)
        {
          //  InitializeComponent();

            this.id = id;
            this.panel = resetPanel;

            onTank = 1000;
            onTank = 1000;
            inUse = false;

            Image  imng = Image.FromFile(@"C:\Users\mdabk\Desktop\grafika\instrybutor.png");
            pi.Image = imng;
         //   txt.BringToFront();
          //  pb.BringToFront();
           // on.BringToFront();
            position = distributorLocations[id];

            pi.BringToFront();
            resetPanel.Controls.Add(pi);
            resetPanel.Controls.Add(dst);
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
