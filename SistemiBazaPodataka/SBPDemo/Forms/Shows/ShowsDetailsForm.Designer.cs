namespace SBPDemo
{
    partial class ShowsDetailsForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxModels = new System.Windows.Forms.ListBox();
            this.listBoxDesigners = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonClose.Location = new System.Drawing.Point(241, 404);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 50);
            this.buttonClose.TabIndex = 22;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTime.Location = new System.Drawing.Point(205, 74);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(41, 13);
            this.labelTime.TabIndex = 13;
            this.labelTime.Text = "label11";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelDate.Location = new System.Drawing.Point(205, 48);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(41, 13);
            this.labelDate.TabIndex = 12;
            this.labelDate.Text = "label10";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTitle.Location = new System.Drawing.Point(205, 22);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(35, 13);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = "label9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Time:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLocation);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelTime);
            this.groupBox1.Controls.Add(this.labelDate);
            this.groupBox1.Controls.Add(this.labelTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 127);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Info";
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelLocation.Location = new System.Drawing.Point(205, 100);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(41, 13);
            this.labelLocation.TabIndex = 15;
            this.labelLocation.Text = "label11";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Location:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date:";
            // 
            // listBoxModels
            // 
            this.listBoxModels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.listBoxModels.FormattingEnabled = true;
            this.listBoxModels.Location = new System.Drawing.Point(220, 194);
            this.listBoxModels.Name = "listBoxModels";
            this.listBoxModels.Size = new System.Drawing.Size(201, 199);
            this.listBoxModels.TabIndex = 15;
            this.listBoxModels.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxModels_MouseDoubleClick);
            // 
            // listBoxDesigners
            // 
            this.listBoxDesigners.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.listBoxDesigners.FormattingEnabled = true;
            this.listBoxDesigners.Location = new System.Drawing.Point(12, 194);
            this.listBoxDesigners.Name = "listBoxDesigners";
            this.listBoxDesigners.Size = new System.Drawing.Size(201, 199);
            this.listBoxDesigners.TabIndex = 16;
            this.listBoxDesigners.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxDesigners_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Models:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Designers:";
            // 
            // ShowsDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 470);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBoxDesigners);
            this.Controls.Add(this.listBoxModels);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ShowsDetailsForm";
            this.Text = "ShowsDetailsForm";
            this.Load += new System.EventHandler(this.ShowsDetailsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxModels;
        private System.Windows.Forms.ListBox listBoxDesigners;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;

    }
}