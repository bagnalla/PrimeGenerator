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
        private uint numberOfPrimes;
        private Stopwatch stopWatch = new Stopwatch();
        private Thread currentPrimeThread;

        public Form1()
        {
            InitializeComponent();
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
            //if (txtBoxSave.Text.Length == 0)
            //    btnGo.Enabled = false;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            numberOfPrimes = uint.Parse(txtBoxInput.Text);
            txtBoxResults.Clear();
            btnGo.Enabled = false;
            btnStop.Enabled = true;
            btnCancel.Enabled = true;
            txtBoxInput.ReadOnly = true;
            progressBar.Value = 0;
            txtBoxProgress.Text = "0%";

            stopWatch.Restart();

            currentPrimeThread = new Thread(new ThreadStart(calculatePrimes));
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
            //this.Invoke((MethodInvoker)delegate { txtBoxResults.Clear(); });
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
            //MethodInvoker performStep = delegate { progressBar.PerformStep(); };
            AppendTextDelegate appendText = new AppendTextDelegate(txtBoxResults.AppendText);
            SetTxtBoxProgressText setTxtBoxProgressText = delegate(string s) { txtBoxProgress.Text = s; };

            uint count = 0;
            uint stepValue = numberOfPrimes / 100;
            if (stepValue == 0)
                stepValue = 1;

            this.Invoke(appendText, new object[] { 2 + "\r\n" });
            if (++count == numberOfPrimes)
                goto end;

            for (ulong n = 3; ; n += 2)
            {
                if (isPrime(n))
                {
                    this.Invoke(appendText, new object[] { n + "\r\n" });
                    if (++count == numberOfPrimes)
                        break;
                    if (count % stepValue == 0)
                    {
                        this.Invoke(performStep);
                        //this.Invoke(setTxtBoxProgressText, new object[] { progressBar.Value + "%" });
                        this.Invoke((MethodInvoker)delegate { txtBoxProgress.Text = progressBar.Value + "%"; });
                    }
                }
            }

            end:
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
            //this.Invoke(appendText, new object[] { "\r\nCompleted in " + (stopWatch.ElapsedMilliseconds / 1000.0) + " seconds." });
        }

        public static bool isPrime(ulong n)
        {
            for (ulong i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;
            return true;
        }
    }

    class CustomTimeSpan
    {
        private long hours, minutes, milliseconds;
        private double seconds;

        public CustomTimeSpan()
        {
            hours = minutes = 0;
            seconds = 0.0;
        }
        public CustomTimeSpan(long ms)
        {
            hours = ms / (1000 * 60 * 60);
            ms = ms % (1000 * 60 * 60);
            minutes = ms / (1000 * 60);
            ms = ms % (1000 * 60);
            seconds = ms / 1000.0;
            milliseconds = ms;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("");
            if (hours > 0)
                result.Append(hours + (hours > 1 ? " hours, " : " hour, "));
            if (minutes > 0)
                result.Append(minutes + (minutes > 1 ? " minutes, " : " minute, "));
            if (seconds >= 1)
                result.Append(seconds + " seconds.");
            else
                result.Append(milliseconds + " milliseconds.");
            return result.ToString();
        }
    }
}
