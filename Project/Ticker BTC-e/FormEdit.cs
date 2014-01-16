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
    public partial class FormEdit : Form
    {
        public string sBalanceUp1 = "";
        public string sBalanceUp2 = "";
        public string sSelected = "";
        public string sType = "";
        public FormEdit()
        {
            InitializeComponent();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var Form1Instance = Application.OpenForms.OfType<Form1>().Single();

            if (sType == "stop")
            {
                Dictionary<string, object> dStopLossesCopy =
                    Form1Instance.dStopLosses.ToDictionary(entry => entry.Key, entry => entry.Value);
                foreach (KeyValuePair<string, object> kv in dStopLossesCopy)
                {
                    Dictionary<string, object> dTmp2 = (Dictionary<string, object>)kv.Value;
                    if (labelOldP.Text == dTmp2["rate"].ToString()
                        && labelOldA.Text.Replace(" " + sBalanceUp2, "") == dTmp2["amount"].ToString())
                    {
                        Form1Instance.dStopLosses.Remove(kv.Key);
                    }
                }

                Dictionary<string, object> dTmpStopLoss = new Dictionary<string, object>();
                dTmpStopLoss["pair"] = sBalanceUp2.ToLower() + "_" + sBalanceUp1.ToLower();
                dTmpStopLoss["rate"] = Convert.ToDouble(textNewP.Text);
                dTmpStopLoss["amount"] =
                    Math.Floor(Convert.ToDouble(textNewV.Text) / Convert.ToDouble(textNewP.Text) * 100000000) / 100000000;
                if (Form1Instance.dBalance2 - Convert.ToDouble(dTmpStopLoss["amount"]) < 0
                    || Convert.ToDouble(dTmpStopLoss["rate"]) <= 0
                    || Convert.ToDouble(dTmpStopLoss["amount"]) <= 0)
                {
                    return;
                }
                Form1Instance.dStopLosses.Add(Form1Instance.dStopLosses.Count.ToString(), dTmpStopLoss);

                Form1Instance.UpdateOpenOrders();
            }
            else
            {
                Form1Instance.CancelOrder(sSelected);

                Form1Instance.Trade(sBalanceUp2.ToLower() + "_" + sBalanceUp1.ToLower(), sType,
                    Convert.ToDouble(textNewP.Text),
                    Math.Floor(Convert.ToDouble(textNewV.Text) / Convert.ToDouble(textNewP.Text) * 100000000) / 100000000);
            }
            Hide();
        }

        private string oldTextNewV = String.Empty;
        private void textNewV_TextChanged(object sender, EventArgs e)
        {
            var Form1Instance = Application.OpenForms.OfType<Form1>().Single();
            double val;
            if (!Double.TryParse(textNewV.Text, out val))
            {
                textNewV.TextChanged -= textNewV_TextChanged;
                textNewV.Text = oldTextNewV;

                //textBoxBuyV.CaretIndex = oldIndex;
                textNewV.TextChanged += textNewV_TextChanged;
            }

            if (textNewP.Text != "0")
                labelNewA.Text = Math.Round(val / Convert.ToDouble(textNewP.Text)
                    * (1 - Form1Instance.dFee), 9) + " " + sBalanceUp2;
        }
        private void textNewP_KeyDown(object sender, KeyEventArgs e)
        {
            oldTextNewV = textNewV.Text;
        }

        private string oldTextNewP = String.Empty;
        private void textNewP_TextChanged(object sender, EventArgs e)
        {
            var Form1Instance = Application.OpenForms.OfType<Form1>().Single();
            double val;
            if (!Double.TryParse(textNewP.Text, out val))
            {
                textNewP.TextChanged -= textNewP_TextChanged;
                textNewP.Text = oldTextNewP;

                textNewP.TextChanged += textNewP_TextChanged;
            }

            if (val != 0)
                labelNewA.Text = Math.Round(Convert.ToDouble(textNewV.Text) / val
                    * (1 - Form1Instance.dFee), 9) + " " + sBalanceUp1;
        }
        private void textNewV_KeyDown(object sender, KeyEventArgs e)
        {
            oldTextNewP = textNewP.Text;
        }

        private void FormEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
