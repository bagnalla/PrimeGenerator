using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Prime_Generator
{

    public partial class Form1 : Form
    {
        PrimeGenerator primeGenerator;
        private uint numberOfPrimes;
        private Stopwatch stopWatch = new Stopwatch();
        private Thread currentPrimeThread;

        public Form1()
        {
            InitializeComponent();
            primeGenerator = new PrimeGenerator();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtBoxInput_TextChanged(object sender, EventArgs e)
        {
            uint temp;
            btnGo.Enabled = uint.TryParse(txtBoxInput.Text, out temp) && temp != 0 && (!checkBoxSave.Checked || txtBoxSave.Text.Length > 0);
        }

        private void txtBoxSave_TextChanged(object sender, EventArgs e)
        {
            txtBoxInput_TextChanged(sender, e);
        }

        private void checkBoxSave_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxSave.Enabled = checkBoxSave.Checked;
            txtBoxInput_TextChanged(sender, e);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            numberOfPrimes = uint.Parse(txtBoxInput.Text);
            txtBoxResults.Clear();
            btnGo.Enabled = false;
            btnStop.Enabled = btnCancel.Enabled = txtBoxInput.ReadOnly = true;
            progressBar.Value = 0;
            txtBoxProgress.Text = "0%";

            stopWatch.Restart();

            currentPrimeThread = new Thread(new ThreadStart(calculatePrimes));
            currentPrimeThread.IsBackground = true;
            currentPrimeThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            currentPrimeThread.Suspend();
            btnStop.Enabled = false;
            btnResume.Enabled = true;
            stopWatch.Stop();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            currentPrimeThread.Resume();
            btnStop.Enabled = true;
            btnResume.Enabled = false;
            stopWatch.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            currentPrimeThread.Suspend();
            currentPrimeThread.Resume();
            currentPrimeThread.Abort();

            btnGo.Enabled = true;
            btnStop.Enabled = false;
            btnResume.Enabled = false;
            btnCancel.Enabled = false;
            txtBoxInput.ReadOnly = false;

            stopWatch.Reset();

            progressBar.Value = 0;
            txtBoxProgress.Clear();
            this.Invoke((MethodInvoker)delegate { txtBoxResults.Text = ""; });
        }

        // prime thread delegates
        delegate void PerformStepDelegate();
        delegate void AppendTextDelegate(string s);
        delegate void SetTxtBoxProgressText(string s);

        // prime thread method
        private void calculatePrimes()
        {
            PerformStepDelegate performStep = new PerformStepDelegate(progressBar.PerformStep);
            AppendTextDelegate appendText = new AppendTextDelegate(txtBoxResults.AppendText);
            SetTxtBoxProgressText setTxtBoxProgressText = delegate(string s) { txtBoxProgress.Text = s; };

            uint count = 0;
            uint stepValue = numberOfPrimes / 100;
            if (stepValue == 0)
                stepValue = 1;

            // reset the prime generator
            primeGenerator.Reset();

            // add first prime since there will always be at least one
            this.Invoke(appendText, new object[] { primeGenerator.CurrentPrime + "\r\n" });

            // add more primes until count reaches numberOfPrimes
            while (++count != numberOfPrimes)
            {
                // perform step on progress bar
                if (count % stepValue == 0)
                {
                    this.Invoke(performStep);
                    this.Invoke((MethodInvoker)delegate { txtBoxProgress.Text = progressBar.Value + "%"; });
                }

                this.Invoke(appendText, new object[] { primeGenerator.NextPrime() + "\r\n" });
                //primeGenerator.NextPrime();
            }

            //this.Invoke(appendText, new object[] { primeGenerator.CurrentPrime + "\r\n" });


            this.Invoke((MethodInvoker)delegate { progressBar.Value = 100; });
            this.Invoke((MethodInvoker)delegate { txtBoxProgress.Text = "100%"; });
            this.Invoke((MethodInvoker)delegate { btnGo.Enabled = true; });
            this.Invoke((MethodInvoker)delegate { btnStop.Enabled = false; });
            this.Invoke((MethodInvoker)delegate { btnCancel.Enabled = false; });
            this.Invoke((MethodInvoker)delegate { txtBoxInput.ReadOnly = false; });

            stopWatch.Stop();

            if (checkBoxSave.Checked)
                File.WriteAllText(txtBoxSave.Text, txtBoxResults.Text, Encoding.Unicode);

            CustomTimeSpan t = new CustomTimeSpan(stopWatch.ElapsedMilliseconds);
            this.Invoke(appendText, new object[] { "\r\nCompleted in " + t });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (currentPrimeThread != null)
                currentPrimeThread.Abort();
        }
    }
}
