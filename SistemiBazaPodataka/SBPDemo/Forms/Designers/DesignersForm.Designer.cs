namespace SBPDemo
{
    partial class DesignersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignersForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAddNew = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.listViewDesigners = new System.Windows.Forms.ListView();
            this.buttonHistory = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonClose.Location = new System.Drawing.Point(592, 424);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 50);
            this.buttonClose.TabIndex = 23;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonDelete.Location = new System.Drawing.Point(592, 179);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(180, 50);
            this.buttonDelete.TabIndex = 22;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonEdit.Location = new System.Drawing.Point(592, 123);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(180, 50);
            this.buttonEdit.TabIndex = 21;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAddNew
            // 
            this.buttonAddNew.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonAddNew.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonAddNew.Location = new System.Drawing.Point(592, 67);
            this.buttonAddNew.Name = "buttonAddNew";
            this.buttonAddNew.Size = new System.Drawing.Size(180, 50);
            this.buttonAddNew.TabIndex = 20;
            this.buttonAddNew.Text = "New Designer";
            this.buttonAddNew.UseVisualStyleBackColor = true;
            this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonRefresh.Location = new System.Drawing.Point(592, 11);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(180, 50);
            this.buttonRefresh.TabIndex = 19;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // listViewDesigners
            // 
            this.listViewDesigners.FullRowSelect = true;
            this.listViewDesigners.GridLines = true;
            this.listViewDesigners.Location = new System.Drawing.Point(12, 11);
            this.listViewDesigners.Name = "listViewDesigners";
            this.listViewDesigners.Size = new System.Drawing.Size(574, 464);
            this.listViewDesigners.TabIndex = 18;
            this.listViewDesigners.UseCompatibleStateImageBehavior = false;
            this.listViewDesigners.View = System.Windows.Forms.View.Details;
            this.listViewDesigners.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDesigners_MouseDoubleClick);
            // 
            // buttonHistory
            // 
            this.buttonHistory.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonHistory.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonHistory.Location = new System.Drawing.Point(592, 291);
            this.buttonHistory.Name = "buttonHistory";
            this.buttonHistory.Size = new System.Drawing.Size(180, 50);
            this.buttonHistory.TabIndex = 24;
            this.buttonHistory.Text = "Check History";
            this.buttonHistory.UseVisualStyleBackColor = true;
            this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
            // 
            // buttonDetails
            // 
            this.buttonDetails.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonDetails.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonDetails.Location = new System.Drawing.Point(592, 235);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(180, 50);
            this.buttonDetails.TabIndex = 25;
            this.buttonDetails.Text = "Check Details";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::SBPDemo.Properties.Resources.fashion_week_logo_new;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(592, 315);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 103);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // DesignersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 487);
            this.Controls.Add(this.buttonHistory);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAddNew);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listViewDesigners);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DesignersForm";
            this.Text = "The Designers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignersForm_FormClosing);
            this.Load += new System.EventHandler(this.DesignersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAddNew;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ListView listViewDesigners;
        private System.Windows.Forms.Button buttonHistory;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}