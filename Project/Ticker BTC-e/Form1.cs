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
using System.Windows.Forms.DataVisualization.Charting;

namespace Ticker_BTC_e
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*
            chart1.Series[2].Points.AddXY("0", "723");
            chart1.Series[2].Points.AddXY("1", "725");
            chart1.Series[2].Points.AddXY("2", "722");
            chart1.Series[2].Points.AddXY("3", "700");
            chart1.Series[2].Points.AddXY("4", "715");
            chart1.Series[2].Points.AddXY("5", "722");
            chart1.Series[2].Points.AddXY("6", "730");
            chart1.Series[2].Points.AddXY("7", "734");

            chart1.Series[0].Points.AddXY("0", "1");
            chart1.Series[0].Points.AddXY("1", "2");
            chart1.Series[0].Points.AddXY("2", "3");
            chart1.Series[0].Points.AddXY("3", "2");
            chart1.Series[0].Points.AddXY("4", "1");
            chart1.Series[0].Points.AddXY("5", "3");
            chart1.Series[0].Points.AddXY("6", "4");
            chart1.Series[0].Points.AddXY("7", "3");
            */
            Thread t = new Thread(NewThread);
            t.IsBackground = true;
            t.Start();
        }
        void NewThread()
        {
            this.Invoke((MethodInvoker)delegate
            {
            });

            Dictionary<string, object> dTmp = new Dictionary<string, object>();
            while (true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dTmp = GetTick("btc_usd");
                    label1now.Text = (string)dTmp["now"];
                    label1change.Text = (string)dTmp["change"];

                    DateTime dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["updated"]));
                    dTmp["updated"] = dtTick.ToOADate();

                    DataManipulator myDataManip = chart1.DataManipulator;
                    myDataManip.Filter(CompareMethod.LessThanOrEqualTo,
                        (dtTick.AddMinutes(-30)).ToOADate(),
                        "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X");

                    chart1.Series[2].Points.AddXY(dTmp["updated"], dTmp["now"]);

                    chart1.Series[3].Points.AddXY(dTmp["updated"], 
                        Convert.ToDouble(dTmp["vol"]) / Convert.ToDouble(dTmp["vol_cur"]));

                    //myDataManip.FilterTopN(4, "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X", true);
                    //myDataManip.FilterTopN(4, "", "SeriesLineVol", "X"); 
                    /*
                    */
                    
                    dTmp = GetTick("ltc_usd");
                    label2now.Text = (string)dTmp["now"];
                    label2change.Text = (string)dTmp["change"];
                });
                System.Threading.Thread.Sleep(5000);
            }
        }
        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        static Dictionary<string, object> GetTick(string sPair)
        {
            var dResult = new Dictionary<string, object>();
            dResult["now"] = "Off";
            dResult["change"] = "Off";
            dResult["vol"] = "Off";
            dResult["vol_cur"] = "Off";

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

                dResult["updated"] = Convert.ToDouble(dJsonTicker["updated"]);
                dResult["vol"] = Convert.ToDouble(dJsonTicker["vol"]);
                dResult["vol_cur"] = Convert.ToDouble(dJsonTicker["vol_cur"]);

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

        private void label1change_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
