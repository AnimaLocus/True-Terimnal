namespace Ticker_BTC_e
{
    partial class FormEdit
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
            this.textNewV = new System.Windows.Forms.TextBox();
            this.textNewP = new System.Windows.Forms.TextBox();
            this.labelOldP = new System.Windows.Forms.Label();
            this.labelOldV = new System.Windows.Forms.Label();
            this.labelOldA = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.labelNewA = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textNewV
            // 
            this.textNewV.Location = new System.Drawing.Point(164, 13);
            this.textNewV.Name = "textNewV";
            this.textNewV.Size = new System.Drawing.Size(105, 20);
            this.textNewV.TabIndex = 6;
            this.textNewV.Text = "0";
            this.textNewV.TextChanged += new System.EventHandler(this.textNewV_TextChanged);
            this.textNewV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNewV_KeyDown);
            // 
            // textNewP
            // 
            this.textNewP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textNewP.Location = new System.Drawing.Point(164, 39);
            this.textNewP.Name = "textNewP";
            this.textNewP.Size = new System.Drawing.Size(105, 20);
            this.textNewP.TabIndex = 7;
            this.textNewP.Text = "0";
            this.textNewP.TextChanged += new System.EventHandler(this.textNewP_TextChanged);
            this.textNewP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNewP_KeyDown);
            // 
            // labelOldP
            // 
            this.labelOldP.AutoSize = true;
            this.labelOldP.BackColor = System.Drawing.Color.Transparent;
            this.labelOldP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOldP.Location = new System.Drawing.Point(12, 39);
            this.labelOldP.Name = "labelOldP";
            this.labelOldP.Size = new System.Drawing.Size(16, 17);
            this.labelOldP.TabIndex = 8;
            this.labelOldP.Text = "0";
            // 
            // labelOldV
            // 
            this.labelOldV.AutoSize = true;
            this.labelOldV.BackColor = System.Drawing.Color.Transparent;
            this.labelOldV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOldV.Location = new System.Drawing.Point(12, 13);
            this.labelOldV.Name = "labelOldV";
            this.labelOldV.Size = new System.Drawing.Size(49, 17);
            this.labelOldV.TabIndex = 8;
            this.labelOldV.Text = "0 USD";
            // 
            // labelOldA
            // 
            this.labelOldA.AutoSize = true;
            this.labelOldA.BackColor = System.Drawing.Color.Transparent;
            this.labelOldA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOldA.Location = new System.Drawing.Point(12, 66);
            this.labelOldA.Name = "labelOldA";
            this.labelOldA.Size = new System.Drawing.Size(47, 17);
            this.labelOldA.TabIndex = 8;
            this.labelOldA.Text = "0 BTC";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEdit.ForeColor = System.Drawing.Color.DarkCyan;
            this.buttonEdit.Location = new System.Drawing.Point(15, 96);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(265, 25);
            this.buttonEdit.TabIndex = 9;
            this.buttonEdit.Text = "EDIT";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // labelNewA
            // 
            this.labelNewA.AutoSize = true;
            this.labelNewA.BackColor = System.Drawing.Color.Transparent;
            this.labelNewA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNewA.Location = new System.Drawing.Point(161, 66);
            this.labelNewA.Name = "labelNewA";
            this.labelNewA.Size = new System.Drawing.Size(47, 17);
            this.labelNewA.TabIndex = 8;
            this.labelNewA.Text = "0 BTC";
            // 
            // FormEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 134);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelOldV);
            this.Controls.Add(this.labelNewA);
            this.Controls.Add(this.labelOldA);
            this.Controls.Add(this.labelOldP);
            this.Controls.Add(this.textNewV);
            this.Controls.Add(this.textNewP);
            this.Name = "FormEdit";
            this.ShowIcon = false;
            this.Text = "True Terminal Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEdit_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        public System.Windows.Forms.TextBox textNewV;
        public System.Windows.Forms.TextBox textNewP;
        public System.Windows.Forms.Label labelOldP;
        public System.Windows.Forms.Label labelOldV;
        public System.Windows.Forms.Label labelOldA;
        public System.Windows.Forms.Label labelNewA;
    }
}