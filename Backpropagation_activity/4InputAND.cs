using Backprop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backpropagation_activity
{
    public partial class Form2 : Form
    {
        NeuralNet nn;
        int inputSize = 4;
        int outputSize = 1;
        int hiddenNeuron;
        int min_epoch;
        int curr_epoch;
        int[,] dataSet;
        Form1 bpOR;

        public Form2()
        {
            InitializeComponent();
            bpOR = new Form1();
            bpOR.Owner = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createNN();
            curr_epoch = 0;
            textBox6.Visible = false;
            label1.Visible = false;
            label3.Text = "Neural Network successfully created.";
            label2.Text = "curr epoch: 0";
        }

        private void createNN()
        {
            hiddenNeuron = (int)((2 / 3) * inputSize) + outputSize;
            nn = new NeuralNet(inputSize, hiddenNeuron, outputSize);
        }
        private void trainNN()
        {
            for (int i = 0; i < Math.Pow(2, inputSize); i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    nn.setInputs(j, dataSet[i, j]);
                }

                nn.setDesiredOutput(0, dataSet[i, inputSize]);
                nn.learn();
            }
        }

        private double testTrainNN()
        {
            double errorSum = 0;

            for (int i = 0; i < Math.Pow(2, inputSize); i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    nn.setInputs(j, dataSet[i, j]);
                }

                nn.run();
                errorSum = Math.Abs((double)dataSet[i, inputSize] - nn.getOuputData(0));
            }

            return errorSum / Math.Pow(2, inputSize);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nn == null)
            {
                createNN();
            }

            if (curr_epoch == 0)
            {
                for (int i = 1; i <= min_epoch; i++)
                {
                    trainNN();
                    double mad = testTrainNN();
                    if (mad <= 0.01)
                    {
                        min_epoch = i;
                        break;
                    }
                }

                curr_epoch = min_epoch;
                textBox6.Visible = true;
                label1.Visible = true;
            }
            else
            {
                if (curr_epoch >= 100000)
                {
                    return;
                }

                int epoch = Convert.ToInt32(textBox6.Text);

                for (int i = 0; i < epoch; i++)
                {
                    trainNN();
                }

                curr_epoch += epoch;
            }

            label2.Text = "curr epoch: " + curr_epoch;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nn == null)
            {
                label3.Text = "Neural network not found.";
                return;
            }

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                label3.Text = "Invalid inputs.";
                return;
            }

            nn.setInputs(0, Convert.ToDouble(textBox1.Text));
            nn.setInputs(1, Convert.ToDouble(textBox2.Text));
            nn.setInputs(2, Convert.ToDouble(textBox3.Text));
            nn.setInputs(3, Convert.ToDouble(textBox4.Text));
            nn.run();

            textBox5.Text = "" + nn.getOuputData(0);
        }

        private void switchTo2InputORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bpOR == null || bpOR.IsDisposed) // Ensure Form1 instance is valid
            {
                bpOR = new Form1
                {
                    Owner = this // Set ownership for proper navigation back
                };
            }
            bpOR.Show();
            this.Hide(); // Hide Form2 when switching
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); // Close the application if Form2 is closed
        }
    }
}
