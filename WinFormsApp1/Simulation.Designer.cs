
namespace WinFormsApp1
{
    partial class Simulation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.resetPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.resetPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // resetPanel
            // 
            this.resetPanel.AutoSize = true;
            this.resetPanel.Controls.Add(this.textBox1);
            this.resetPanel.Controls.Add(this.btnStart);
            this.resetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetPanel.Location = new System.Drawing.Point(0, 0);
            this.resetPanel.Name = "resetPanel";
            this.resetPanel.Size = new System.Drawing.Size(1209, 561);
            this.resetPanel.TabIndex = 17;
            this.resetPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 50);
            this.textBox1.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStart.Location = new System.Drawing.Point(1122, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 27);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 561);
            this.Controls.Add(this.resetPanel);
            this.Name = "Simulation";
            this.Text = "Stacja Paliw";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onClose);
            this.resetPanel.ResumeLayout(false);
            this.resetPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel resetPanel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBox1;
    }
}