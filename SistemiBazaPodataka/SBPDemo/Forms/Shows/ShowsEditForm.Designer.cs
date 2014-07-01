namespace SBPDemo
{
    partial class ShowsEditForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLoc = new System.Windows.Forms.TextBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxModels = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxDesigners = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxLoc);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Controls.Add(this.textBoxTitle);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 106);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Info";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Date && Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(175, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "City:";
            // 
            // textBoxLoc
            // 
            this.textBoxLoc.Location = new System.Drawing.Point(208, 71);
            this.textBoxLoc.Name = "textBoxLoc";
            this.textBoxLoc.Size = new System.Drawing.Size(196, 20);
            this.textBoxLoc.TabIndex = 12;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "dd.MM.yyyy      hh:mm";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(208, 45);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(196, 20);
            this.dateTimePicker.TabIndex = 9;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(208, 19);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(196, 20);
            this.textBoxTitle.TabIndex = 2;
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOK.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonOK.Location = new System.Drawing.Point(35, 406);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(180, 50);
            this.buttonOK.TabIndex = 23;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonCancel.Location = new System.Drawing.Point(221, 406);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(180, 50);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Models:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Designers:";
            // 
            // checkedListBoxModels
            // 
            this.checkedListBoxModels.FormattingEnabled = true;
            this.checkedListBoxModels.Location = new System.Drawing.Point(221, 153);
            this.checkedListBoxModels.Name = "checkedListBoxModels";
            this.checkedListBoxModels.Size = new System.Drawing.Size(202, 199);
            this.checkedListBoxModels.TabIndex = 25;
            // 
            // checkedListBoxDesigners
            // 
            this.checkedListBoxDesigners.FormattingEnabled = true;
            this.checkedListBoxDesigners.Location = new System.Drawing.Point(14, 153);
            this.checkedListBoxDesigners.Name = "checkedListBoxDesigners";
            this.checkedListBoxDesigners.Size = new System.Drawing.Size(201, 199);
            this.checkedListBoxDesigners.TabIndex = 24;
            // 
            // ShowsEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 466);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkedListBoxModels);
            this.Controls.Add(this.checkedListBoxDesigners);
            this.Name = "ShowsEditForm";
            this.Text = "Edit Show";
            this.Load += new System.EventHandler(this.ShowsEditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxLoc;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxModels;
        private System.Windows.Forms.CheckedListBox checkedListBoxDesigners;
    }
}