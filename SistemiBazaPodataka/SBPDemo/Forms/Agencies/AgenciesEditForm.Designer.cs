namespace SBPDemo
{
    partial class AgenciesEditForm
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
            this.buttonRemove = new System.Windows.Forms.Button();
            this.radioButtonInt = new System.Windows.Forms.RadioButton();
            this.radioButtonLoc = new System.Windows.Forms.RadioButton();
            this.textBoxCountries = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listBoxCountries = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkedListBoxModels = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxHQ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPIN = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(115, 390);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(97, 23);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // radioButtonInt
            // 
            this.radioButtonInt.AutoSize = true;
            this.radioButtonInt.Location = new System.Drawing.Point(70, 178);
            this.radioButtonInt.Name = "radioButtonInt";
            this.radioButtonInt.Size = new System.Drawing.Size(83, 17);
            this.radioButtonInt.TabIndex = 4;
            this.radioButtonInt.TabStop = true;
            this.radioButtonInt.Text = "International";
            this.radioButtonInt.UseVisualStyleBackColor = true;
            // 
            // radioButtonLoc
            // 
            this.radioButtonLoc.AutoSize = true;
            this.radioButtonLoc.Location = new System.Drawing.Point(13, 178);
            this.radioButtonLoc.Name = "radioButtonLoc";
            this.radioButtonLoc.Size = new System.Drawing.Size(51, 17);
            this.radioButtonLoc.TabIndex = 3;
            this.radioButtonLoc.TabStop = true;
            this.radioButtonLoc.Text = "Local";
            this.radioButtonLoc.UseVisualStyleBackColor = true;
            // 
            // textBoxCountries
            // 
            this.textBoxCountries.Location = new System.Drawing.Point(13, 365);
            this.textBoxCountries.Name = "textBoxCountries";
            this.textBoxCountries.Size = new System.Drawing.Size(199, 20);
            this.textBoxCountries.TabIndex = 6;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 391);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(97, 23);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // listBoxCountries
            // 
            this.listBoxCountries.FormattingEnabled = true;
            this.listBoxCountries.Location = new System.Drawing.Point(13, 199);
            this.listBoxCountries.Name = "listBoxCountries";
            this.listBoxCountries.Size = new System.Drawing.Size(199, 160);
            this.listBoxCountries.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Available Models:";
            // 
            // checkedListBoxModels
            // 
            this.checkedListBoxModels.FormattingEnabled = true;
            this.checkedListBoxModels.Location = new System.Drawing.Point(219, 199);
            this.checkedListBoxModels.Name = "checkedListBoxModels";
            this.checkedListBoxModels.Size = new System.Drawing.Size(202, 214);
            this.checkedListBoxModels.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxHQ);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Location = new System.Drawing.Point(13, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 76);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Foundation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(126, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Headquarters:";
            // 
            // textBoxHQ
            // 
            this.textBoxHQ.Location = new System.Drawing.Point(206, 45);
            this.textBoxHQ.Name = "textBoxHQ";
            this.textBoxHQ.Size = new System.Drawing.Size(196, 20);
            this.textBoxHQ.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Founded:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(206, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(196, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonOK.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonOK.Location = new System.Drawing.Point(32, 420);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(180, 50);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonCancel.Location = new System.Drawing.Point(219, 419);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(180, 50);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPIN);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Name and ID";
            // 
            // textBoxPIN
            // 
            this.textBoxPIN.Location = new System.Drawing.Point(207, 19);
            this.textBoxPIN.Name = "textBoxPIN";
            this.textBoxPIN.Size = new System.Drawing.Size(196, 20);
            this.textBoxPIN.TabIndex = 1;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(207, 45);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(196, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Registration Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Agency Name:";
            // 
            // AgenciesEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 482);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.radioButtonInt);
            this.Controls.Add(this.radioButtonLoc);
            this.Controls.Add(this.textBoxCountries);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.listBoxCountries);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkedListBoxModels);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AgenciesEditForm";
            this.Text = "Edit Agency";
            this.Load += new System.EventHandler(this.AgenciesEditForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.RadioButton radioButtonInt;
        private System.Windows.Forms.RadioButton radioButtonLoc;
        private System.Windows.Forms.TextBox textBoxCountries;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListBox listBoxCountries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxModels;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxHQ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPIN;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}