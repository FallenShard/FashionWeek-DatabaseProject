namespace SBPDemo
{
    partial class SelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectorForm));
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonShows = new System.Windows.Forms.Button();
            this.buttonDesigners = new System.Windows.Forms.Button();
            this.buttonModels = new System.Windows.Forms.Button();
            this.buttonAgencies = new System.Windows.Forms.Button();
            this.buttonSpecialGuests = new System.Windows.Forms.Button();
            this.buttonOverview = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLogo.Image = global::SBPDemo.Properties.Resources.logo_big;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(624, 442);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonShows
            // 
            this.buttonShows.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShows.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonShows.Location = new System.Drawing.Point(12, 212);
            this.buttonShows.Name = "buttonShows";
            this.buttonShows.Size = new System.Drawing.Size(180, 50);
            this.buttonShows.TabIndex = 1;
            this.buttonShows.Text = "The Shows";
            this.buttonShows.UseVisualStyleBackColor = true;
            this.buttonShows.Click += new System.EventHandler(this.buttonShows_Click);
            // 
            // buttonDesigners
            // 
            this.buttonDesigners.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDesigners.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonDesigners.Location = new System.Drawing.Point(12, 268);
            this.buttonDesigners.Name = "buttonDesigners";
            this.buttonDesigners.Size = new System.Drawing.Size(180, 50);
            this.buttonDesigners.TabIndex = 2;
            this.buttonDesigners.Text = "The Designers";
            this.buttonDesigners.UseVisualStyleBackColor = true;
            this.buttonDesigners.Click += new System.EventHandler(this.buttonDesigners_Click);
            // 
            // buttonModels
            // 
            this.buttonModels.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModels.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonModels.Location = new System.Drawing.Point(12, 324);
            this.buttonModels.Name = "buttonModels";
            this.buttonModels.Size = new System.Drawing.Size(180, 50);
            this.buttonModels.TabIndex = 3;
            this.buttonModels.Text = "The Models";
            this.buttonModels.UseVisualStyleBackColor = true;
            this.buttonModels.Click += new System.EventHandler(this.buttonModels_Click);
            // 
            // buttonAgencies
            // 
            this.buttonAgencies.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgencies.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonAgencies.Location = new System.Drawing.Point(12, 380);
            this.buttonAgencies.Name = "buttonAgencies";
            this.buttonAgencies.Size = new System.Drawing.Size(180, 50);
            this.buttonAgencies.TabIndex = 4;
            this.buttonAgencies.Text = "The Agencies";
            this.buttonAgencies.UseVisualStyleBackColor = true;
            this.buttonAgencies.Click += new System.EventHandler(this.buttonAgencies_Click);
            // 
            // buttonSpecialGuests
            // 
            this.buttonSpecialGuests.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSpecialGuests.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonSpecialGuests.Location = new System.Drawing.Point(432, 380);
            this.buttonSpecialGuests.Name = "buttonSpecialGuests";
            this.buttonSpecialGuests.Size = new System.Drawing.Size(180, 50);
            this.buttonSpecialGuests.TabIndex = 5;
            this.buttonSpecialGuests.Text = "Special Guests";
            this.buttonSpecialGuests.UseVisualStyleBackColor = true;
            this.buttonSpecialGuests.Click += new System.EventHandler(this.buttonSpecialGuests_Click);
            // 
            // buttonOverview
            // 
            this.buttonOverview.BackColor = System.Drawing.Color.Transparent;
            this.buttonOverview.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOverview.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonOverview.Location = new System.Drawing.Point(432, 324);
            this.buttonOverview.Name = "buttonOverview";
            this.buttonOverview.Size = new System.Drawing.Size(180, 50);
            this.buttonOverview.TabIndex = 7;
            this.buttonOverview.Text = "Overview";
            this.buttonOverview.UseVisualStyleBackColor = false;
            this.buttonOverview.Click += new System.EventHandler(this.buttonOverview_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Stars", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelTitle.Location = new System.Drawing.Point(3, 36);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(553, 51);
            this.labelTitle.TabIndex = 8;
            this.labelTitle.Text = "Fashion Week";
            // 
            // SelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonOverview);
            this.Controls.Add(this.buttonSpecialGuests);
            this.Controls.Add(this.buttonAgencies);
            this.Controls.Add(this.buttonModels);
            this.Controls.Add(this.buttonDesigners);
            this.Controls.Add(this.buttonShows);
            this.Controls.Add(this.pictureBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fashion Week";
            this.Load += new System.EventHandler(this.SelectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonShows;
        private System.Windows.Forms.Button buttonDesigners;
        private System.Windows.Forms.Button buttonModels;
        private System.Windows.Forms.Button buttonAgencies;
        private System.Windows.Forms.Button buttonSpecialGuests;
        private System.Windows.Forms.Button buttonOverview;
        private System.Windows.Forms.Label labelTitle;
    }
}