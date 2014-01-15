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
            this.listViewNF1 = new Ticker_BTC_e.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1d = new System.Windows.Forms.Button();
            this.button1m = new System.Windows.Forms.Button();
            this.button1y = new System.Windows.Forms.Button();
            this.button1h = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewNF1
            // 
            this.listViewNF1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewNF1.FullRowSelect = true;
            this.listViewNF1.Location = new System.Drawing.Point(6, 41);
            this.listViewNF1.Margin = new System.Windows.Forms.Padding(0);
            this.listViewNF1.Name = "listViewNF1";
            this.listViewNF1.ShowGroups = false;
            this.listViewNF1.Size = new System.Drawing.Size(722, 497);
            this.listViewNF1.TabIndex = 0;
            this.listViewNF1.UseCompatibleStateImageBehavior = false;
            this.listViewNF1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Balance";
            this.columnHeader2.Width = 568;
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
            // FormBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 547);
            this.Controls.Add(this.button1h);
            this.Controls.Add(this.button1y);
            this.Controls.Add(this.button1m);
            this.Controls.Add(this.button1d);
            this.Controls.Add(this.listViewNF1);
            this.Name = "FormBalance";
            this.ShowIcon = false;
            this.Text = "True Terminal Balance";
            this.Load += new System.EventHandler(this.FormBalance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListViewNF listViewNF1;
        private System.Windows.Forms.Button button1d;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button button1m;
        private System.Windows.Forms.Button button1y;
        private System.Windows.Forms.Button button1h;
    }
}