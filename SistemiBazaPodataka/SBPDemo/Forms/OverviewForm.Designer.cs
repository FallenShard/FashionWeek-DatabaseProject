namespace SBPDemo
{
    partial class OverviewForm
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
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewOverview = new System.Windows.Forms.ListView();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxTables
            // 
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.Location = new System.Drawing.Point(12, 51);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(180, 264);
            this.listBoxTables.TabIndex = 0;
            this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a table to display:";
            // 
            // listViewOverview
            // 
            this.listViewOverview.FullRowSelect = true;
            this.listViewOverview.GridLines = true;
            this.listViewOverview.Location = new System.Drawing.Point(198, 51);
            this.listViewOverview.Name = "listViewOverview";
            this.listViewOverview.Size = new System.Drawing.Size(574, 368);
            this.listViewOverview.TabIndex = 27;
            this.listViewOverview.UseCompatibleStateImageBehavior = false;
            this.listViewOverview.View = System.Windows.Forms.View.Details;
            this.listViewOverview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewOverview_MouseDoubleClick);
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.buttonClose.Location = new System.Drawing.Point(592, 425);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 50);
            this.buttonClose.TabIndex = 32;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Gist Upright Extrabold Demo", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(193, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 29);
            this.label2.TabIndex = 33;
            this.label2.Text = "Table Name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::SBPDemo.Properties.Resources.fashion_week_logo_new;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 316);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 103);
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // OverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SBPDemo.Properties.Resources.selector_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 487);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listViewOverview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxTables);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OverviewForm";
            this.Text = "Overview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OverviewForm_FormClosing);
            this.Load += new System.EventHandler(this.OverviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewOverview;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

