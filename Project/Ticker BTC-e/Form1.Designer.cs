namespace Ticker_BTC_e
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label1now = new System.Windows.Forms.Label();
            this.label1change = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label2now = new System.Windows.Forms.Label();
            this.label2change = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "BTC/USD";
            // 
            // label1now
            // 
            this.label1now.AutoSize = true;
            this.label1now.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1now.Location = new System.Drawing.Point(85, 0);
            this.label1now.Name = "label1now";
            this.label1now.Size = new System.Drawing.Size(76, 20);
            this.label1now.TabIndex = 1;
            this.label1now.Text = "12834.21";
            this.label1now.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1change
            // 
            this.label1change.AutoSize = true;
            this.label1change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1change.Location = new System.Drawing.Point(167, 0);
            this.label1change.Name = "label1change";
            this.label1change.Size = new System.Drawing.Size(187, 20);
            this.label1change.TabIndex = 1;
            this.label1change.Text = "+1.1 / +2.2 / +3.3 / +4.4 %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "LTC/USD";
            // 
            // label2now
            // 
            this.label2now.AutoSize = true;
            this.label2now.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2now.Location = new System.Drawing.Point(85, 20);
            this.label2now.Name = "label2now";
            this.label2now.Size = new System.Drawing.Size(58, 20);
            this.label2now.TabIndex = 1;
            this.label2now.Text = "834.21";
            this.label2now.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2change
            // 
            this.label2change.AutoSize = true;
            this.label2change.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2change.Location = new System.Drawing.Point(167, 20);
            this.label2change.Name = "label2change";
            this.label2change.Size = new System.Drawing.Size(187, 20);
            this.label2change.TabIndex = 1;
            this.label2change.Text = "+1.1 / +2.2 / +3.3 / +4.4 %";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(159, 39);
            this.Controls.Add(this.label2change);
            this.Controls.Add(this.label1change);
            this.Controls.Add(this.label2now);
            this.Controls.Add(this.label1now);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Ticker BTC-e";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label1change;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label2change;
        public System.Windows.Forms.Label label1now;
        public System.Windows.Forms.Label label2now;
    }
}

