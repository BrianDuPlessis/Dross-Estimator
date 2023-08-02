using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Dross_Estimator
{
    public partial class frmMain : Form
    {

        const double ZincWeight = 7133;
        double[] arrHeights = new double[15];


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidateInput()
        {
            try
            {

                //Left validation
                if (double.TryParse(txtLeft1.Text, out arrHeights[0]))
                {
                    if (double.TryParse(txtLeft2.Text, out arrHeights[1]))
                    {
                        if (double.TryParse(txtLeft3.Text, out arrHeights[2]))
                        {
                            if (double.TryParse(txtLeft4.Text, out arrHeights[3]))
                            {
                                if (double.TryParse(txtLeft5.Text, out arrHeights[4]))
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Please enter a valid value");
                                    txtLeft5.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid value");
                                txtLeft4.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value");
                            txtLeft3.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid value");
                        txtLeft2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid value");
                    txtLeft1.Focus();
                }


                //mid validation
                if (double.TryParse(txtMid1.Text, out arrHeights[5]))
                {
                    if (double.TryParse(txtMid2.Text, out arrHeights[6]))
                    {
                        if (double.TryParse(txtMid3.Text, out arrHeights[7]))
                        {
                            if (double.TryParse(txtMid4.Text, out arrHeights[8]))
                            {
                                if (double.TryParse(txtMid5.Text, out arrHeights[9]))
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Please enter a valid value");
                                    txtMid5.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid value");
                                txtMid4.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value");
                            txtMid3.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid value");
                        txtMid2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid value");
                    txtMid1.Focus();
                }

                //Right validation
                if (double.TryParse(txtRight1.Text, out arrHeights[10]))
                {
                    if (double.TryParse(txtRight2.Text, out arrHeights[11]))
                    {
                        if (double.TryParse(txtRight3.Text, out arrHeights[12]))
                        {
                            if (double.TryParse(txtRight4.Text, out arrHeights[13]))
                            {
                                if (double.TryParse(txtRight5.Text, out arrHeights[14]))
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Please enter a valid value");
                                    txtRight5.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid value");
                                txtRight4.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value");
                            txtRight3.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid value");
                        txtRight2.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid value");
                    txtRight1.Focus();
                }

                if(txtDate.Text == "")
                {
                    MessageBox.Show("Please enter the Date");
                    txtDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private double CalculateDross()
        {
           double drossVolume = 0;
            double drossWeight = 0;

            for(int k = 0; k < 15; k++)
            {
                drossVolume = drossVolume + ((arrHeights[k] / 1000) * 0.65);
            }
            drossWeight = drossVolume * ZincWeight;

            return drossWeight;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double drossWeight;

            ValidateInput();
            drossWeight = CalculateDross();

            MessageBox.Show("The total estimated dross is " + drossWeight.ToString("f2") + "kg");
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnClear.Enabled = false;
            txtDate.Clear();
            //Left
            txtLeft1.Text = "";
            txtLeft2.Text = "";
            txtLeft3.Text = "";
            txtLeft4.Text = "";
            txtLeft5.Text = "";

            //Mid
            txtMid1.Text = "";
            txtMid2.Text = "";
            txtMid3.Text = "";
            txtMid4.Text = "";
            txtMid5.Text = "";

            //Right
            txtRight1.Text = "";
            txtRight2.Text = "";
            txtRight3.Text = "";
            txtRight4.Text = "";
            txtRight5.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string path = "", sOutLeft = "", sOutMid = "", sOutRight = "";
            double drossWeight;
            StreamWriter outFile;

            try
            {
                drossWeight = CalculateDross();
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    path = saveFile.FileName;
                    outFile = File.AppendText(path);

                    outFile.WriteLine("");
                    outFile.WriteLine("");

                    //Build string containing left values
                    for(int k = 0; k < 5; k++)
                    {
                        sOutLeft += arrHeights[k].ToString() + " ";
                    }

                    //Build string containing middel values
                    for (int k = 5; k < 10; k++)
                    {
                        sOutMid += arrHeights[k].ToString() + " ";
                    }

                    //Build string containing right values
                    for (int k = 10; k < 15; k++)
                    {
                        sOutRight += arrHeights[k].ToString() + " ";
                    }

                    outFile.WriteLine(txtDate.Text);
                    outFile.WriteLine("Left values = " + sOutLeft);
                    outFile.WriteLine("Middel values = " + sOutMid);
                    outFile.WriteLine("Right values = " + sOutRight);
                    outFile.WriteLine("Total dross weigth = " + drossWeight.ToString("f2") + "kg.");

                    outFile.Close();
                }
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
