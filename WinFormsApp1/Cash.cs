using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp1.StaticVariables;

namespace WinFormsApp1
{
    public class Cash
    {
        private Random rand = new Random();
        private int id;
        public Label cashLbl = new Label();
        public TextBox txt = new TextBox();
        Panel panel = new Panel();

        public void cashierAction()
        {
            while (true) 
            {
                cashSem[id].Wait();
                int distribId = cashDistribId[id];
                double amount = distributors[distribId].getAmount();
                int carId = distribCarId[distribId];
                panel.Invoke(new Action(delegate ()
                {
                    txt.Text = "Kwota: " + amount;
                }));


                carSem[carId].Release();
                paySem[id].Wait();

                Thread.Sleep(rand.Next(1000, 2000));
                panel.Invoke(new Action(delegate ()
                 {
                     txt.Text = "OTWARTE";
                  }));
                carSem[carId].Release();

                distributors[cashDistribId[id]].reset();
                freeCashiers[id] = true;


                freeDistributors[distribId] = true;

                //if (station)

            }
        }

        public Cash(int id, Panel resetPanel)
        {
            this.id = id;
            this.panel = resetPanel;
            freeCashiers[id] = true;

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
