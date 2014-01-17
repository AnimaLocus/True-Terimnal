namespace Ticker_BTC_e
{
    partial class FormBalance
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
            this.listViewNFBalance = new Ticker_BTC_e.ListViewNF();
            this.button1d = new System.Windows.Forms.Button();
            this.button1m = new System.Windows.Forms.Button();
            this.button1y = new System.Windows.Forms.Button();
            this.button1h = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewNFBalance
            // 
            this.listViewNFBalance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});

            this.listViewNFBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewNFBalance.FullRowSelect = true;
            this.listViewNFBalance.Location = new System.Drawing.Point(6, 41);
            this.listViewNFBalance.Margin = new System.Windows.Forms.Padding(0);
            this.listViewNFBalance.Name = "listViewNFBalance";
            this.listViewNFBalance.ShowGroups = false;
            this.listViewNFBalance.Size = new System.Drawing.Size(722, 497);
            this.listViewNFBalance.TabIndex = 0;
            this.listViewNFBalance.UseCompatibleStateImageBehavior = false;
            this.listViewNFBalance.View = System.Windows.Forms.View.Details;
            // 
            // button1d
            // 
            this.button1d.Location = new System.Drawing.Point(87, 9);
            this.button1d.Margin = new System.Windows.Forms.Padding(0);
            this.button1d.Name = "button1d";
            this.button1d.Size = new System.Drawing.Size(75, 23);
            this.button1d.TabIndex = 1;
            this.button1d.Text = "1 Day";
            this.button1d.UseVisualStyleBackColor = true;
            this.button1d.Click += new System.EventHandler(this.button1d_Click);
            // 
            // button1m
            // 
            this.button1m.Location = new System.Drawing.Point(168, 9);
            this.button1m.Margin = new System.Windows.Forms.Padding(0);
            this.button1m.Name = "button1m";
            this.button1m.Size = new System.Drawing.Size(75, 23);
            this.button1m.TabIndex = 1;
            this.button1m.Text = "1 Month";
            this.button1m.UseVisualStyleBackColor = true;
            this.button1m.Click += new System.EventHandler(this.button1m_Click);
            // 
            // button1y
            // 
            this.button1y.Location = new System.Drawing.Point(249, 9);
            this.button1y.Margin = new System.Windows.Forms.Padding(0);
            this.button1y.Name = "button1y";
            this.button1y.Size = new System.Drawing.Size(75, 23);
            this.button1y.TabIndex = 1;
            this.button1y.Text = "1 Year";
            this.button1y.UseVisualStyleBackColor = true;
            this.button1y.Click += new System.EventHandler(this.button1y_Click);
            // 
            // button1h
            // 
            this.button1h.Location = new System.Drawing.Point(6, 9);
            this.button1h.Margin = new System.Windows.Forms.Padding(0);
            this.button1h.Name = "button1h";
            this.button1h.Size = new System.Drawing.Size(75, 23);
            this.button1h.TabIndex = 1;
            this.button1h.Text = "1 Hour";
            this.button1h.UseVisualStyleBackColor = true;
            this.button1h.Click += new System.EventHandler(this.button1h_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "USD";
            this.columnHeader2.Width = 55;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "BTC";
            this.columnHeader3.Width = 55;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "LTC";
            this.columnHeader4.Width = 55;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "NMC";
            this.columnHeader5.Width = 55;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "RUR";
            this.columnHeader6.Width = 55;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "EUR";
            this.columnHeader7.Width = 55;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "NVC";
            this.columnHeader8.Width = 55;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "TRC";
            this.columnHeader9.Width = 55;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "PPC";
            this.columnHeader10.Width = 55;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "FTC";
            this.columnHeader11.Width = 55;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "XPM";
            this.columnHeader12.Width = 55;
            // 
            // FormBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 547);
            this.Controls.Add(this.button1h);
            this.Controls.Add(this.button1y);
            this.Controls.Add(this.button1m);
            this.Controls.Add(this.button1d);
            this.Controls.Add(this.listViewNFBalance);
            this.Name = "FormBalance";
            this.ShowIcon = false;
            this.Text = "True Terminal Balance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBalance_FormClosed);
            this.Load += new System.EventHandler(this.FormBalance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewNF listViewNFBalance;
        private System.Windows.Forms.Button button1d;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1m;
        private System.Windows.Forms.Button button1y;
        private System.Windows.Forms.Button button1h;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
    }
}