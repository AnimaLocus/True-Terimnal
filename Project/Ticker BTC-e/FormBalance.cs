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
            Dictionary<string, object> dTmp;
            Dictionary<DateTime, object> dBalance = new Dictionary<DateTime, object>();
            DateTime dtTick;
            DateTime dtLastTick = new DateTime(666);
            foreach (KeyValuePair<string, object> kv in Form1.dTradeHistoryGrouped)//.Reverse())
            {
                dTmp = (Dictionary<string, object>)kv.Value;

                dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["timestamp"]));
                dtTick = dtFloor(dtTick, tsPeriod);
                //dTimestamp = dtTick.ToOADate();

                if (!dBalance.ContainsKey(dtTick)) {
                    dBalance[dtTick] = new Dictionary<string, object> 
	                {
	                    {"usd", 0},
	                    {"rur", 0},
	                    {"eur", 0},
	                    {"btc", 0},
	                    {"ltc", 0},
	                    {"nmc", 0},
	                    {"nvc", 0},
	                    {"trc", 0},
	                    {"ppc", 0},
	                    {"ftc", 0},
	                    {"xpm", 0}
	                };
                    //if (dtLastTick != new DateTime(666))
                    //dBalance[dtTick] = dBalance[dtLastTick]; 
                }

                string[] saPair = ((string)dTmp["pair"]).Split('_');
                double dTmpV = Convert.ToDouble(((Dictionary<string, object>)dBalance[dtTick])[saPair[0]]);
                double dTmpV2 = Convert.ToDouble(((Dictionary<string, object>)dBalance[dtTick])[saPair[0]]);
                if ((string)dTmp["type"] == "B")
                {
                    dTmpV = dTmpV + Convert.ToDouble(dTmp["amount"]);// *Convert.ToDouble(dTmp["rate"]);
                    dTmpV2 = dTmpV2 - Convert.ToDouble(dTmp["amount"]) * Convert.ToDouble(dTmp["rate"]);
                }
                else if ((string)dTmp["type"] == "S")
                {
                    dTmpV = dTmpV - Convert.ToDouble(dTmp["amount"]);// * Convert.ToDouble(dTmp["rate"]);
                    dTmpV2 = dTmpV2 + Convert.ToDouble(dTmp["amount"]) * Convert.ToDouble(dTmp["rate"]);
                }

                ((Dictionary<string, object>)dBalance[dtTick])[saPair[0]] = dTmpV;
                ((Dictionary<string, object>)dBalance[dtTick])[saPair[1]] = dTmpV2;

                dtLastTick = dtTick;
            }
            listViewNFBalance.BeginUpdate();
            listViewNFBalance.Items.Clear();
            int i = 0;
            foreach (KeyValuePair<DateTime, object> kv in dBalance)//.Reverse())
            {
                listViewNFBalance.Items.Add(
                    kv.Key.ToString(), i
                );

                string sTmp = "";
                foreach (KeyValuePair<string, object> kv2 in (Dictionary<string, object>)kv.Value)
                {
                    sTmp = sTmp + " / " + kv2.Key + ":" + kv2.Value;
                }
                listViewNFBalance.Items[i].SubItems.Add(
                    sTmp
                );
                i++;
            }
            listViewNFBalance.EndUpdate();
        }
        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public static DateTime dtFloor(DateTime date, TimeSpan span) {
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
    }
}
