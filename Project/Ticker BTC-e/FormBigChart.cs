using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticker_BTC_e
{
    public partial class FormBigChart : Form
    {
        public FormBigChart()
        {
            InitializeComponent();
            Resize();
        }

        private void FormBigChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void FormBigChart_SizeChanged(object sender, EventArgs e)
        {
            Resize();
        }

        private void Resize()
        {
            ChartMain.Size = new Size(Size.Width, (int)Math.Round(Size.Height * 0.8));
        }
    }
}
