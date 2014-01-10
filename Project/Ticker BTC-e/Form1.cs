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
using System.Globalization;

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

            //UpdateChartMain();
            Thread t = new Thread(NewThread);
            t.IsBackground = true;
            t.Start();
        }
        void NewThread()
        {
            //return;

            Dictionary<string, object> dTmp = new Dictionary<string, object>();
            dTmp["updated"] = 1;
            Random rnd = new Random();
            while (true)
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        dTmp = GetTick("btc_usd");
                        label1now.Text = (string)dTmp["nowt"];
                        label1change.Text = (string)dTmp["change"];

                        //dTmp["now"] = rnd.Next(50, 150);
                        //dTmp["updated"] = 20 + Convert.ToDouble(dTmp["updated"]);

                        DateTime dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["updated"]));
                        dTmp["updated"] = dtTick.ToOADate();

                        dtTick = dtFloor(dtTick, new TimeSpan(0, 1, 0));
                        double dDate = dtTick.ToOADate();
                        //double dPrice = 0;
                        double dPrice = Convert.ToDouble(dTmp["now"]);
                        //double.Parse((string)dTmp["now"], CultureInfo.InvariantCulture);
                        //double dPrice = (double)dTmp["now"];

                        /*
                        DataManipulator myDataManip = chart1.DataManipulator;
                        myDataManip.Filter(CompareMethod.LessThanOrEqualTo,
                            (dtTick.AddMinutes(-30)).ToOADate(),
                            "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X");
                        */

                        if (dCandlestickData.Count > 60)
                        {
                            dCandlestickData.Remove(dCandlestickData.Keys.Min());
                        }
                        if (dCandlestickData.ContainsKey(dDate))
                        {
                            dCandlestickData[dDate].Close = dPrice;
                            if (dCandlestickData[dDate].Low > dPrice)
                            {
                                dCandlestickData[dDate].Low = dPrice;
                            }
                            if (dCandlestickData[dDate].High < dPrice)
                            {
                                dCandlestickData[dDate].High = dPrice;
                            }
                        }
                        else
                        {
                            dCandlestickData[dDate] = new CandlestickData(
                                dDate,
                                dPrice,
                                dPrice,
                                dPrice,
                                dPrice
                            );
                        }
                        //chart1.Series["SeriesLine"].Points.AddXY(dTmp["updated"], dTmp["now"]);

                        /*
                        // adding date and high
                        chart1.Series["SeriesChart"].Points.AddXY(dTmp["updated"], dTmp["now"]);
                        // adding low
                        chart1.Series["SeriesChart"].Points[0].YValues[1] = Convert.ToDouble(dTmp["now"]);
                        //adding open
                        chart1.Series["SeriesChart"].Points[0].YValues[2] = Convert.ToDouble(dTmp["now"]);
                        // adding close
                        chart1.Series["SeriesChart"].Points[0].YValues[3] = Convert.ToDouble(dTmp["now"]);
                        */



                        //chart1.Series[3].Points.AddXY(dTmp["updated"], 
                        //    Convert.ToDouble(dTmp["vol"]) / Convert.ToDouble(dTmp["vol_cur"]));

                        //myDataManip.FilterTopN(4, "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X", true);
                        //myDataManip.FilterTopN(4, "", "SeriesLineVol", "X"); 
                        /*
                        dTmp = GetTick("ltc_usd");
                        label2now.Text = (string)dTmp["now"];
                        label2change.Text = (string)dTmp["change"];
                        */

                        UpdateChartMain();
                    });
                }
                catch (Exception e)
                {

                }
                System.Threading.Thread.Sleep(1000);
            }
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

                dResult["now"] = dJsonTicker["last"];
                dResult["nowt"] = "$" + Math.Round(dLast, 2).ToString();

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

        public Dictionary<double, CandlestickData> dCandlestickData = new Dictionary<double, CandlestickData>();
        public class CandlestickData
        {
            public double Date;
            public double High;
            public double Low;
            public double Open;
            public double Close;
            public CandlestickData(double d, double h, double l, double o, double c) 
                { Date = d; High = h; Low = l; Open = o; Close = c; }
        }
        public void UpdateChartMain()
        {
            int i = 0;
            //To remove the first point in the series: Chart1.Series[0].Points.RemoveAt(0);
            ChartMain.Series["Line"].Points.Clear();
            ChartMain.Series["Candlestick"].Points.Clear();
            foreach (KeyValuePair<double, CandlestickData> kv in dCandlestickData)
            {
                // adding date and high
                ChartMain.Series["Line"].Points.AddXY(kv.Key, (kv.Value.High + kv.Value.Low)/2);
                ChartMain.Series["Candlestick"].Points.AddXY(kv.Key, kv.Value.High);
                // adding low
                ChartMain.Series["Candlestick"].Points[i].YValues[1] = kv.Value.Low;
                //adding open
                ChartMain.Series["Candlestick"].Points[i].YValues[2] = kv.Value.Open;
                // adding close
                ChartMain.Series["Candlestick"].Points[i].YValues[3] = kv.Value.Close;

                i++;
            }
            /*
            if (i > 5)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "5", "Line:Y", "LineMA1:Y");
            }
            if (i > 10)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "10", "Line:Y", "LineMA2:Y");
            }
            if (i > 20)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "20", "Line:Y", "LineMA3:Y");
            }
            if (i > 40)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "40", "Line:Y", "LineMA4:Y");
            }
             * */
        }

        private void label1change_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void ChartMain_Click(object sender, EventArgs e)
        {

        }
    }
}
