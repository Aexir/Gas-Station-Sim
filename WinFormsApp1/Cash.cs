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
        private Point position;
        Random rand = new Random();
        private int id;
        public Label cashLbl = new Label();
        public TextBox txt = new TextBox();
        Panel panel = new Panel();

        public void cashierAction()
        {
            while (true) 
            {
                cashSem[id].Wait();   
                double amount = distributors[cashDistribId[id]].getAmount();
                int carId = distribCarId[cashDistribId[id]];
                panel.Invoke(new Action(delegate ()
                {
                    txt.Text = "Kwota: " + amount;
                }));
                Thread.Sleep(rand.Next(1000, 2000));

                freeDistributors[cashDistribId[id]] = true;
                freeCashiers[id] = true;


                carSem[carId].Release();

                nCars--;
                if (cars[carId].getFuelType() == 0)
                {
                    onCars--;
                }
                else
                {
                    pbCars--;
                }
                //panel.Invoke(new Action(delegate ()
                // {
                //     txt.Text = "OTWARTE";
                //  }));
                //distributors[cashDistribId[id]].reset();


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
