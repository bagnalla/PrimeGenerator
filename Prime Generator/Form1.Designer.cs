namespace Prime_Generator
{
    partial class Form1
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
            this.txtBoxResults = new System.Windows.Forms.TextBox();
            this.txtBoxInput = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBoxProgress = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.checkBoxSave = new System.Windows.Forms.CheckBox();
            this.txtBoxSave = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBoxResults
            // 
            this.txtBoxResults.Location = new System.Drawing.Point(7, 67);
            this.txtBoxResults.Multiline = true;
            this.txtBoxResults.Name = "txtBoxResults";
            this.txtBoxResults.ReadOnly = true;
            this.txtBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxResults.Size = new System.Drawing.Size(260, 163);
            this.txtBoxResults.TabIndex = 0;
            this.txtBoxResults.TabStop = false;
            // 
            // txtBoxInput
            // 
            this.txtBoxInput.Location = new System.Drawing.Point(7, 12);
            this.txtBoxInput.Name = "txtBoxInput";
            this.txtBoxInput.Size = new System.Drawing.Size(100, 20);
            this.txtBoxInput.TabIndex = 0;
            this.txtBoxInput.TextChanged += new System.EventHandler(this.txtBoxInput_TextChanged);
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(7, 38);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(47, 23);
            this.btnGo.TabIndex = 1;
            this.btnGo.TabStop = false;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 236);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(219, 20);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(232, 38);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(176, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBoxProgress
            // 
            this.txtBoxProgress.Location = new System.Drawing.Point(232, 236);
            this.txtBoxProgress.Name = "txtBoxProgress";
            this.txtBoxProgress.ReadOnly = true;
            this.txtBoxProgress.Size = new System.Drawing.Size(35, 20);
            this.txtBoxProgress.TabIndex = 6;
            this.txtBoxProgress.TabStop = false;
            this.txtBoxProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(120, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnResume
            // 
            this.btnResume.Enabled = false;
            this.btnResume.Location = new System.Drawing.Point(60, 38);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(54, 23);
            this.btnResume.TabIndex = 8;
            this.btnResume.TabStop = false;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // checkBoxSave
            // 
            this.checkBoxSave.AutoSize = true;
            this.checkBoxSave.Location = new System.Drawing.Point(113, 14);
            this.checkBoxSave.Name = "checkBoxSave";
            this.checkBoxSave.Size = new System.Drawing.Size(66, 17);
            this.checkBoxSave.TabIndex = 9;
            this.checkBoxSave.TabStop = false;
            this.checkBoxSave.Text = "Save to:";
            this.checkBoxSave.UseVisualStyleBackColor = true;
            this.checkBoxSave.CheckedChanged += new System.EventHandler(this.checkBoxSave_CheckedChanged);
            // 
            // txtBoxSave
            // 
            this.txtBoxSave.Enabled = false;
            this.txtBoxSave.Location = new System.Drawing.Point(180, 12);
            this.txtBoxSave.Name = "txtBoxSave";
            this.txtBoxSave.Size = new System.Drawing.Size(82, 20);
            this.txtBoxSave.TabIndex = 1;
            this.txtBoxSave.TextChanged += new System.EventHandler(this.txtBoxSave_TextChanged);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(274, 261);
            this.Controls.Add(this.txtBoxSave);
            this.Controls.Add(this.checkBoxSave);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.txtBoxProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtBoxInput);
            this.Controls.Add(this.txtBoxResults);
            this.MaximumSize = new System.Drawing.Size(290, 299);
            this.MinimumSize = new System.Drawing.Size(290, 299);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prime Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxResults;
        private System.Windows.Forms.TextBox txtBoxInput;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBoxProgress;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.CheckBox checkBoxSave;
        private System.Windows.Forms.TextBox txtBoxSave;

    }
}

