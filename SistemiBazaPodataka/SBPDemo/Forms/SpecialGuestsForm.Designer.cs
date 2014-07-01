namespace SBPDemo
{
    partial class SpecialGuestsForm
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
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle1
            // 
            this.labelTitle1.AutoSize = true;
            this.labelTitle1.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelTitle1.Location = new System.Drawing.Point(111, 44);
            this.labelTitle1.Name = "labelTitle1";
            this.labelTitle1.Size = new System.Drawing.Size(340, 29);
            this.labelTitle1.TabIndex = 0;
            this.labelTitle1.Text = ":This edition of Fashion Week:";
            // 
            // labelTitle2
            // 
            this.labelTitle2.AutoSize = true;
            this.labelTitle2.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelTitle2.Location = new System.Drawing.Point(23, 73);
            this.labelTitle2.Name = "labelTitle2";
            this.labelTitle2.Size = new System.Drawing.Size(509, 29);
            this.labelTitle2.TabIndex = 1;
            this.labelTitle2.Text = ":proudly presents the following special guests:";
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonClose.Location = new System.Drawing.Point(180, 269);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 50);
            this.buttonClose.TabIndex = 32;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // SpecialGuestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 331);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelTitle2);
            this.Controls.Add(this.labelTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SpecialGuestsForm";
            this.Text = "Special Guests";
            this.Load += new System.EventHandler(this.SpecialGuestsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle1;
        private System.Windows.Forms.Label labelTitle2;
        private System.Windows.Forms.Button buttonClose;
    }
}