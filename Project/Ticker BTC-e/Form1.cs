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
using System.Reflection;

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
            //return;
            Random rnd = new Random();
            int i = 0;

            Dictionary<string, object> dTmp = new Dictionary<string, object>();
            if (Setting.DebugImitation)
            {
                dTmp["updated"] = 1;
            }
            while (true)
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (Setting.DebugImitation)
                        {
                            dTmp["now"] = rnd.Next(50, 150);
                            dTmp["updated"] = 20 + Convert.ToDouble(dTmp["updated"]);

                            //listViewAsk.BeginUpdate();
                            //listViewAsk.EndUpdate();
                            listViewAsk.Items.Add(rnd.Next(1, 150000000).ToString());
                            listViewAsk.Items[i].SubItems.Add(rnd.Next(1, 150000000).ToString());
                            listViewAsk.Items[i].SubItems.Add(rnd.Next(1, 150000000).ToString());

                            listViewBid.Items.Add(rnd.Next(1, 150000000).ToString());
                            listViewBid.Items[i].SubItems.Add(rnd.Next(1, 150000000).ToString());
                            listViewBid.Items[i].SubItems.Add(rnd.Next(1, 150000000).ToString());

                            i++;
                        }
                        else
                        {
                            dTmp = GetTick(Setting.TradingPair);
                            label1now.Text = (string)dTmp["nowt"];
                            label1change.Text = (string)dTmp["change"];

                            dDepthData = GetDepth(Setting.TradingPair);
                            UpdateDepth();
                        }
                        
                        DateTime dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["updated"]));
                        if (!Setting.DebugImitation)
                        {
                            dTmp["updated"] = dtTick.ToOADate();
                        }

                        dtTick = dtFloor(dtTick, new TimeSpan(0, 1, 0));
                        double dDate = dtTick.ToOADate();
                        double dPrice = Convert.ToDouble(dTmp["now"]);

                        /*
                        DataManipulator myDataManip = chart1.DataManipulator;
                        myDataManip.Filter(CompareMethod.LessThanOrEqualTo,
                            (dtTick.AddMinutes(-30)).ToOADate(),
                            "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X");
                        */

                        if (dCandlestickData.Count > Setting.ShowInterval)
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
                            dCandlestickData[dDate] = new CandlestickData(dDate, dPrice, dPrice, dPrice, dPrice);
                        }

                        UpdateChartMain();
                    });
                }
                catch (Exception e)
                {

                }
                if (Setting.DebugImitation)
                {
                    System.Threading.Thread.Sleep(100);
                }
                else
                {
                    System.Threading.Thread.Sleep(Setting.UpdateInterval);
                }
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
        static Dictionary<string, object> GetJson(string sUrl)
        {
            var dResult = new Dictionary<string, object>();
            try
            {
                /*
                 * https://btc-e.com/api/2/btc_usd/trades
                 * https://btc-e.com/api/2/btc_usd/depth
                 * https://btc-e.com/api/2/btc_usd/ticker
                 */
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sUrl);
                request.MaximumAutomaticRedirections = 4;
                request.MaximumResponseHeadersLength = 4;
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var sResultTmp = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                dResult = Json.Deserialize(sResultTmp) as Dictionary<string, object>;
            }
            catch (Exception e) { }

            return dResult;
        }
        public Dictionary<string, object> dDepthData = new Dictionary<string, object>();
        static Dictionary<string, object> GetDepth(string sPair)
        {
            var dResult = new Dictionary<string, object>();
            try
            {
                dResult = GetJson("https://btc-e.com/api/2/" + sPair + "/depth");
            }
            catch (Exception e) { }

            return dResult;
        }
        public void UpdateDepth()
        {
            double dDepth = 0;
            listViewAsk.Items.Clear();
            listViewAsk.BeginUpdate();
            var lAsks = (List<object>)dDepthData["asks"];
            for (int i = 0; i < lAsks.Count; i++)
            {
                dDepth += Convert.ToDouble
                    (
                        ((List<object>)lAsks[i])[1]
                    );

                listViewAsk.Items.Add(
                    Convert.ToDouble
                    (
                        ((List<object>)lAsks[i])[0]
                    ).ToString()
                );
                listViewAsk.Items[i].SubItems.Add(
                    Convert.ToDouble
                    (
                        ((List<object>)lAsks[i])[1]
                    ).ToString()
                );
                listViewAsk.Items[i].SubItems.Add(dDepth.ToString());
            }
            listViewAsk.EndUpdate();

            dDepth = 0;
            listViewBid.Items.Clear();
            listViewBid.BeginUpdate();
            var lBids = (List<object>)dDepthData["bids"];
            for (int i = 0; i < lBids.Count; i++)
            {
                dDepth += Convert.ToDouble
                    (
                        ((List<object>)lBids[i])[1]
                    );

                listViewBid.Items.Add(
                    Convert.ToDouble
                    (
                        ((List<object>)lBids[i])[0]
                    ).ToString()
                );
                listViewBid.Items[i].SubItems.Add(
                    Convert.ToDouble
                    (
                        ((List<object>)lBids[i])[1]
                    ).ToString()
                );
                listViewBid.Items[i].SubItems.Add(dDepth.ToString());
            }
            listViewBid.EndUpdate();
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
                var dJson = GetJson("https://btc-e.com/api/2/" + sPair + "/ticker");
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

            CandlestickData csdLast = new CandlestickData(0,0,0,0,0);
            foreach (KeyValuePair<double, CandlestickData> kv in dCandlestickData)
            {
                ChartMain.Series["Line"].Points.AddXY(kv.Key, (kv.Value.High + kv.Value.Low) / 2);
                ChartMain.Series["Candlestick"].Points.AddXY(kv.Key, kv.Value.High);
                ChartMain.Series["Candlestick"].Points[i].YValues[1] = kv.Value.Low;
                ChartMain.Series["Candlestick"].Points[i].YValues[2] = kv.Value.Open;
                ChartMain.Series["Candlestick"].Points[i].YValues[3] = kv.Value.Close;

                csdLast = kv.Value;

                i++;
            }
            ChartMain.Series["LineNow"].Points.Clear();
            ChartMain.Series["LineNow"].Points.AddXY(csdLast.Date, csdLast.Close);
            ChartMain.Series["LineNow"].Points.AddXY(csdLast.Date-60, csdLast.Close);
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public sealed class Setting
    {
        public static string TradingPair { get; set; }
        public static int ShowInterval { get; set; }
        public static int UpdateInterval { get; set; }
        public static string APIKey { get; set; }
        public static string APISecret { get; set; }
        public static bool DebugImitation { get; set; }
        static readonly string SETTINGS = "config.ini";
        static readonly Setting instance = new Setting();
        Setting() { }
        static Setting()
        {
            string property = "";
            string[] settings = File.ReadAllLines(SETTINGS);
            foreach (string s in settings)
                try
                {
                    string[] split = s.Split(new char[] { ':' }, 2);
                    if (split.Length != 2)
                        continue;
                    property = split[0].Trim();
                    string value = split[1].Trim();
                    PropertyInfo propInfo = instance.GetType().GetProperty(property);
                    switch (propInfo.PropertyType.Name)
                    {
                        case "Boolean":
                            propInfo.SetValue(null, Convert.ToBoolean(value), null);
                            break;
                        case "Int32":
                            propInfo.SetValue(null, Convert.ToInt32(value), null);
                            break;
                        case "String":
                            propInfo.SetValue(null, value, null);
                            break;
                    }
                }
                catch
                {
                    throw new Exception("Invalid setting '" + property + "'");
                }
        }
    }
}
