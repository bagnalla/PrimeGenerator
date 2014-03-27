using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Prime_Generator
{

    public partial class Form1 : Form
    {
        readonly PrimeGenerator _primeGenerator;
        private uint _numberOfPrimes;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private Thread _currentPrimeThread;

        public Form1()
        {
            InitializeComponent();
            _primeGenerator = new PrimeGenerator();
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
            _numberOfPrimes = uint.Parse(txtBoxInput.Text);
            txtBoxResults.Clear();
            btnGo.Enabled = checkBoxSave.Enabled = checkBoxShowAll.Enabled = false;
            btnStop.Enabled = btnCancel.Enabled = txtBoxInput.ReadOnly = true;
            progressBar.Value = 0;
            txtBoxProgress.Text = @"0%";

            _stopWatch.Restart();

            _currentPrimeThread = new Thread(CalculatePrimes);
            _currentPrimeThread.IsBackground = true;
            _currentPrimeThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _currentPrimeThread.Suspend();
            btnStop.Enabled = false;
            btnResume.Enabled = true;
            _stopWatch.Stop();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            _currentPrimeThread.Resume();
            btnStop.Enabled = true;
            btnResume.Enabled = false;
            _stopWatch.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _currentPrimeThread.Suspend();
            _currentPrimeThread.Resume();
            _currentPrimeThread.Abort();

            btnGo.Enabled = true;
            btnStop.Enabled = false;
            btnResume.Enabled = false;
            btnCancel.Enabled = false;
            txtBoxInput.ReadOnly = false;
            checkBoxSave.Enabled = checkBoxShowAll.Enabled = true;

            _stopWatch.Reset();

            progressBar.Value = 0;
            txtBoxProgress.Clear();
            Invoke((MethodInvoker)delegate { txtBoxResults.Text = ""; });
        }

        // prime thread delegate types
        delegate void PerformStepDelegate();
        delegate void AppendTextDelegate(object o);
        delegate void SetTxtBoxProgressText(object o);

        // prime thread method
        private void CalculatePrimes()
        {
            // make delegate instances
            //var performStep = new PerformStepDelegate(progressBar.PerformStep);
            MethodInvoker performStep = () => progressBar.PerformStep();
            /*Delegate appendText = (o) =>
            {
                txtBoxResults.AppendText(o.ToString());
                txtBoxResults.AppendText("\r\n");
            };*/
            var appendText = new AppendTextDelegate((o) =>
            {
                txtBoxResults.AppendText(o.ToString());
                txtBoxResults.AppendText("\r\n");
            });
            var setTxtBoxProgressText = new SetTxtBoxProgressText((o) => txtBoxProgress.Text = o + @"%");

            uint count = 0;
            var stepValue = _numberOfPrimes / 100;
            if (stepValue == 0)
                stepValue = 1;

            // reset the prime generator
            _primeGenerator.Reset();
            _primeGenerator.SetPrimeListCapacity((int)_numberOfPrimes);

            if (checkBoxShowAll.Checked)
            {
                // add first prime since there will always be at least one
                Invoke(appendText, _primeGenerator.CurrentPrime);

                // add more primes until count reaches numberOfPrimes
                while (++count != _numberOfPrimes)
                {
                    // perform step on progress bar
                    if (count % stepValue == 0)
                    {
                        Invoke(performStep);
                        //this.Invoke((MethodInvoker)delegate { txtBoxProgress.Text = progressBar.Value + @"%"; });
                        Invoke(setTxtBoxProgressText, progressBar.Value);
                    }

                    Invoke(appendText, _primeGenerator.NextPrime());
                }
            }
            else
            {
                while (++count != _numberOfPrimes)
                {
                    // perform step on progress bar
                    if (count % stepValue == 0)
                    {
                        Invoke(performStep);
                        //this.Invoke((MethodInvoker)delegate { txtBoxProgress.Text = progressBar.Value + @"%"; });
                        Invoke(setTxtBoxProgressText, progressBar.Value);
                    }

                    _primeGenerator.NextPrime();
                }

                Invoke(appendText, _primeGenerator.CurrentPrime);
            }


            //this.Invoke((MethodInvoker)delegate { progressBar.Value = 100; });
            Invoke((MethodInvoker)(() =>
            {
                progressBar.Value = 100;
                txtBoxProgress.Text = @"100%";
                btnGo.Enabled = checkBoxSave.Enabled = checkBoxShowAll.Enabled = true;
                btnStop.Enabled = btnCancel.Enabled = txtBoxInput.ReadOnly = false;
            }));

            _stopWatch.Stop();

            if (checkBoxSave.Checked)
                File.WriteAllText(txtBoxSave.Text, txtBoxResults.Text, Encoding.Unicode);

            var t = new CustomTimeSpan(_stopWatch.ElapsedMilliseconds);
            Invoke(appendText, "\r\nCompleted in " + t);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_currentPrimeThread != null)
                _currentPrimeThread.Abort();
        }

        private void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
