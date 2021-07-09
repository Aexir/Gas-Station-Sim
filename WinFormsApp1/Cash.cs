using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public class Cash
    {
        private Point position;
        Random rand = new Random();
        private int id;
        public Label cashLbl = new Label();
        public TextBox txt = new TextBox();
        Panel panel = new Panel();
        private bool inUse;

        public void cashierAction()
        {

        }

        public Cash(int id, Panel resetPanel)
        {
            this.id = id;
            this.panel = resetPanel;
            freeCashiers[id] = true;
            inUse = false;

            
         //   cashLbl.Location = cashLocations[id];
            txt.Location = cashLocations[id];
            cashLbl.Location = new Point(cashLocations[id].X, cashLocations[id].Y - 25);
            cashLbl.BackColor = Color.LightGray;

            cashLbl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cashLbl.Text = "Kasa nr " + (id + 1);

            resetPanel.Controls.Add(cashLbl);
            resetPanel.Controls.Add(txt);
        }
    }
}
