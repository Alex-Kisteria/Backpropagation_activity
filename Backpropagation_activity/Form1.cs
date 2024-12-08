using Backprop;
using System;
using System.Windows.Forms;

namespace Backpropagation_activity
{
    public partial class Form1 : Form
    {
        NeuralNet nn;
        Form2 bpAND;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nn = new NeuralNet(2, 100, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 100; x++)
            {
                nn.setInputs(0, 0.0);
                nn.setInputs(1, 0.0);
                nn.setDesiredOutput(0, 0.0);
                nn.learn();

                nn.setInputs(0, 0.0);
                nn.setInputs(1, 1.0);
                nn.setDesiredOutput(0, 1.0);
                nn.learn();

                nn.setInputs(0, 1.0);
                nn.setInputs(1, 0.0);
                nn.setDesiredOutput(0, 1.0);
                nn.learn();

                nn.setInputs(0, 1.0);
                nn.setInputs(1, 1.0);
                nn.setDesiredOutput(0, 1.0);
                nn.learn();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nn.setInputs(0, Convert.ToDouble(textBox1.Text));
            nn.setInputs(1, Convert.ToDouble(textBox2.Text));
            nn.run();
            textBox3.Text = "" + nn.getOuputData(0);
        }

        private void changeTo4InputANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bpAND == null || bpAND.IsDisposed) // Ensure Form2 instance is valid
            {
                bpAND = new Form2
                {
                    Owner = this // Set ownership for proper navigation back
                };
            }
            bpAND.Show();
            this.Hide(); // Hide Form1 when switching
        }
    }
}
