namespace SuperLauncher
{
    partial class Run
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.lblOpen = new System.Windows.Forms.Label();
            this.cbInput = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(115, 18);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(206, 18);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBrowse.Location = new System.Drawing.Point(297, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(85, 25);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDesc.Location = new System.Drawing.Point(60, 25);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(312, 30);
            this.lblDesc.TabIndex = 4;
            this.lblDesc.Text = "Type the name of a program, folder, document, or Internet resource, and Super Lau" +
    "ncher will open it for you.";
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(12, 23);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(32, 32);
            this.pbIcon.TabIndex = 5;
            this.pbIcon.TabStop = false;
            // 
            // pnl1
            // 
            this.pnl1.BackColor = System.Drawing.SystemColors.Control;
            this.pnl1.Controls.Add(this.btnBrowse);
            this.pnl1.Controls.Add(this.btnOK);
            this.pnl1.Controls.Add(this.btnCancel);
            this.pnl1.Location = new System.Drawing.Point(0, 119);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(398, 56);
            this.pnl1.TabIndex = 6;
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblOpen.Location = new System.Drawing.Point(12, 75);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(39, 15);
            this.lblOpen.TabIndex = 7;
            this.lblOpen.Text = "&Open:";
            // 
            // cbInput
            // 
            this.cbInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllSystemSources;
            this.cbInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbInput.FormattingEnabled = true;
            this.cbInput.Location = new System.Drawing.Point(60, 72);
            this.cbInput.Name = "cbInput";
            this.cbInput.Size = new System.Drawing.Size(312, 23);
            this.cbInput.TabIndex = 0;
            this.cbInput.SelectedIndexChanged += new System.EventHandler(this.cbInput_TextUpdate);
            this.cbInput.TextUpdate += new System.EventHandler(this.cbInput_TextUpdate);
            // 
            // Run
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(398, 176);
            this.Controls.Add(this.cbInput);
            this.Controls.Add(this.lblOpen);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.pnl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Run";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Run";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Run_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.pnl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.ComboBox cbInput;
    }
}