using System;
using System.Drawing;
using System.Threading;
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

                updateValue(amount); //wyswietlanie kwoty na kasie

                carSem[carId].Release();
                cashSem[id].Wait();

                Thread.Sleep(rand.Next(1000, 2000));

                reset();

                carSem[carId].Release();
                distributors[cashDistribId[id]].resetDistributor();
                freeCashiers[id] = true;
                freeDistributors[distribId] = true;
            }
        }

        public Cash(int id, Panel resetPanel)
        {
            this.id = id;
            this.panel = resetPanel;
            freeCashiers[id] = true;

            txt.Location = cashLocations[id];
            cashLbl.Location = new Point(cashLocations[id].X, cashLocations[id].Y - 35);
            cashLbl.BackColor = Color.LightGray;

            cashLbl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            cashLbl.Text = "Kasa nr " + (id + 1);

            resetPanel.Controls.Add(cashLbl);
            resetPanel.Controls.Add(txt);
        }

        public void reset()
        {
            panel.Invoke(new Action(delegate ()
            {
                txt.Text = "OTWARTE";
            }));
        }
        public void updateValue(double amount)
        {
            panel.Invoke(new Action(delegate ()
            {
                txt.Text = "Kwota: " + amount;
            }));
        }
    }
}
