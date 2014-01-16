using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Ticker_BTC_e
{
    public partial class FormBalance : Form
    {
        public FormBalance()
        {
            InitializeComponent();
        }

        public TimeSpan tsPeriod = new TimeSpan(24, 0, 0);
        private void FormBalance_Load(object sender, EventArgs e)
        {
            Dictionary<DateTime, object> dBalance = new Dictionary<DateTime, object>();
            DateTime dtTick = new DateTime(666);

            listViewNFBalance.BeginUpdate();
            listViewNFBalance.Items.Clear();
            if (File.Exists("balance.db"))
            {
                string[] saLines = File.ReadAllLines("balance.db");
                Array.Reverse(saLines);
                int i = 0;
                foreach (string sTmp in saLines)
                {
                    string[] split = sTmp.Split(new char[] { '/' }, 12);
                    if (split.Length == 12)
                    {
                        for (int iTmp = 0; iTmp < split.Length; iTmp++)
                        {
                            if (iTmp == 0)
                            {
                                dtTick = DateTime.Parse(split[iTmp]);
                                listViewNFBalance.Items.Add(
                                    dtTick.ToString(), i
                                );
                                continue;
                            }
                            string[] split2 = split[iTmp].Split(new char[] { ':' }, 2);
                            listViewNFBalance.Items[i].SubItems.Add(
                                Convert.ToDouble(split2[1]).ToString()
                            );
                        }
                        i++;
                    }
                }
            }
            listViewNFBalance.EndUpdate();
        }
        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public static DateTime dtFloor(DateTime date, TimeSpan span)
        {
            long ticks = (date.Ticks / span.Ticks);
            return new DateTime(ticks * span.Ticks);
        }

        private void button1h_Click(object sender, EventArgs e)
        {
            tsPeriod = new TimeSpan(1, 0, 0);
            FormBalance_Load(sender, e);
        }

        private void button1d_Click(object sender, EventArgs e)
        {
            tsPeriod = new TimeSpan(24, 0, 0);
            FormBalance_Load(sender, e);
        }

        private void button1m_Click(object sender, EventArgs e)
        {
            tsPeriod = new TimeSpan(24 * 30, 0, 0);
            FormBalance_Load(sender, e);
        }

        private void button1y_Click(object sender, EventArgs e)
        {
            tsPeriod = new TimeSpan(12 * 24 * 30, 0, 0);
            FormBalance_Load(sender, e);
        }

        private void FormBalance_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void FormBalance_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
