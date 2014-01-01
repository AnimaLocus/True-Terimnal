using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;
using MiniJSON;

namespace Ticker_BTC_e
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Thread t = new Thread(NewThread);
            t.IsBackground = true;
            t.Start();
        }
        void NewThread()
        {
            Dictionary<string, object> dTmp = new Dictionary<string, object>();
            while (true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dTmp = GetTick("btc_usd");
                    label1now.Text = (string)dTmp["now"];
                    label1change.Text = (string)dTmp["change"];

                    dTmp = GetTick("ltc_usd");
                    label2now.Text = (string)dTmp["now"];
                    label2change.Text = (string)dTmp["change"];
                });
                System.Threading.Thread.Sleep(5000);
            }
        }

        static Dictionary<string, object> GetTick(string sPair)
        {
            var dResult = new Dictionary<string, object>();
            dResult["now"] = "Off";
            dResult["change"] = "Off";

            double dAvg;
            double dLast;
            string sPlus = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://btc-e.com/api/2/" + sPair + "/ticker");
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var sResultTmp = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                var dJson = Json.Deserialize(sResultTmp) as Dictionary<string, object>;
                var dJsonTicker = dJson["ticker"] as Dictionary<string, object>;

                dAvg = Convert.ToDouble(dJsonTicker["avg"]);
                dLast = Convert.ToDouble(dJsonTicker["last"]);

                dResult["now"] = "$" + Math.Round(dLast, 2).ToString();

                var dTmp = Math.Round(dLast - dAvg, 2);
                if (dTmp > 0)
                {
                    sPlus = "+";
                }
                dResult["change"] = "Δ " + sPlus + Math.Round((dLast - dAvg) / dAvg * 100, 2)
                    + "% <> $" + Math.Round(dAvg, 2);
            }
            catch (Exception e)
            {

            }

            return dResult;
        }
    }
}
