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
using System.Security.Cryptography;
using System.Web;
using System.Windows.Controls;

namespace Ticker_BTC_e
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            if (Setting.APIKey == "XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX") {
                bOnlyTicker = true;
            }

            APISecretHash = new HMACSHA512(Encoding.ASCII.GetBytes(Setting.APISecret));
            nonce = UnixTime.Now;

            InitializeComponent();

            Thread t = new Thread(NewThread);
            t.IsBackground = true;
            t.Start();
        }
        public bool bOnlyTicker = false;
        public HMACSHA512 APISecretHash;
        public UInt32 nonce;
        public Dictionary<string, object> dTmp = new Dictionary<string, object>();
        public Dictionary<string, object> dUserInfo = new Dictionary<string, object>();
        public Dictionary<string, object> dTradeHistory = new Dictionary<string, object>();
        public Dictionary<string, object> dOpenOrders = new Dictionary<string, object>();
        public double dLastPrice = 0;
        public double dBalance1 = 0;
        public double dBalance2 = 0;
        public string sBalance1 = "usd";
        public string sBalance2 = "btc";
        public string sBalanceUp1 = "USD";
        public string sBalanceUp2 = "BTC";
        void NewThread()
        {
            //return;
            Random rnd = new Random();
            int i = 0;
            Dictionary<string, object> dTmp2;

            Setting.TradingPair = "btc_usd";
            comboBoxPair.SelectedIndex = 0;

            UpdateHistory(Setting.TradingPair, 2000);
            /*
            MessageBox.Show((

                (double)
                (
                    (Dictionary<string, object>)
                    (
                        (Dictionary<string, object>)dUserInfo["return"]
                    )
                ["funds"]
                )["btc"]

            ).ToString());
            */

            if (Setting.DebugImitation)
            {
                dTmp["updated"] = 1;
            }
            while (true)
            {
                try
                {
                    if (!Setting.DebugImitation)
                    {
                        //dTmp = GetTick(Setting.TradingPair);
                        UpdateHistory(Setting.TradingPair, 500);
                        dDepthData = GetDepth(Setting.TradingPair);

                        if (bOnlyTicker)
                        {
                        }
                        else
                        {
                            dOpenOrders = GetOpenOrders();
                            dTradeHistory = GetTradeHistory();
                            dUserInfo = GetInfo();
                        }
                    }
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
                            label1now.Text = dLastPrice + " " + sBalanceUp1;

                            UpdateDepth();

                            if (!bOnlyTicker)
                            {
                                dBalance1 = Convert.ToDouble(((Dictionary<string, object>)dUserInfo["funds"])[sBalance1]);
                                if (dBalance1 < 0.0001) dBalance1 = 0;
                                dBalance2 = Convert.ToDouble(((Dictionary<string, object>)dUserInfo["funds"])[sBalance2]);
                                if (dBalance2 < 0.0001) dBalance2 = 0;

                                i = 0;
                                listViewHistory.BeginUpdate();
                                //listViewHistory.Items.Clear();
                                foreach (KeyValuePair<string, object> kv in dTradeHistory)
                                {
                                    dTmp2 = (Dictionary<string, object>)kv.Value;

                                    if ((string)dTmp2["type"] == "buy")
                                    {
                                        dTmp2["type"] = "B";
                                    }
                                    else if ((string)dTmp2["type"] == "sell")
                                    {
                                        dTmp2["type"] = "S";
                                    }

                                    listViewHistory.Items.Add(
                                        (string)dTmp2["pair"], i
                                    );
                                    listViewHistory.Items[i].SubItems.Add(
                                        (string)dTmp2["type"]
                                    );
                                    listViewHistory.Items[i].SubItems.Add(
                                        Convert.ToDouble(dTmp2["rate"]).ToString()
                                    );
                                    listViewHistory.Items[i].SubItems.Add(
                                        Convert.ToDouble(dTmp2["amount"]).ToString()
                                    );
                                    listViewHistory.Items[i].SubItems.Add(
                                        (Convert.ToDouble(dTmp2["rate"]) * Convert.ToDouble(dTmp2["amount"])).ToString()
                                    );

                                    i++;
                                }
                                listViewHistory.EndUpdate();


                                i = 0;
                                listViewOpenOrders.BeginUpdate();
                                listViewOpenOrders.Items.Clear();
                                foreach (KeyValuePair<string, object> kv in dOpenOrders)
                                {
                                    dTmp2 = (Dictionary<string, object>)kv.Value;

                                    if ((string)dTmp2["type"] == "buy")
                                    {
                                        dTmp2["type"] = "B";
                                    }
                                    else if ((string)dTmp2["type"] == "sell")
                                    {
                                        dTmp2["type"] = "S";
                                    }

                                    listViewOpenOrders.Items.Add(
                                        (string)dTmp2["pair"], i
                                    );
                                    listViewOpenOrders.Items[i].SubItems.Add(
                                        (string)dTmp2["type"]
                                    );
                                    listViewOpenOrders.Items[i].SubItems.Add(
                                        Convert.ToDouble(dTmp2["rate"]).ToString()
                                    );
                                    listViewOpenOrders.Items[i].SubItems.Add(
                                        Convert.ToDouble(dTmp2["amount"]).ToString()
                                    );
                                    listViewOpenOrders.Items[i].SubItems.Add(
                                        (Convert.ToDouble(dTmp2["rate"]) * Convert.ToDouble(dTmp2["amount"])).ToString()
                                    );

                                    i++;
                                }
                                listViewOpenOrders.EndUpdate();
                            }
                        }
                        
                        /*
                        DateTime dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["updated"]));
                        if (!Setting.DebugImitation)
                        {
                            dTmp["updated"] = dtTick.ToOADate();
                        }

                        dtTick = dtFloor(dtTick, new TimeSpan(0, 1, 0));
                        double dDate = dtTick.ToOADate();
                        double dPrice = Convert.ToDouble(dTmp["now"]);

                         */
                          
                        /*
                        DataManipulator myDataManip = chart1.DataManipulator;
                        myDataManip.Filter(CompareMethod.LessThanOrEqualTo,
                            (dtTick.AddMinutes(-30)).ToOADate(),
                            "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X");
                        */

                        /*
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
                        
                         */

                        labelBuyHave.Text = Math.Round(dBalance1, 2) + " " + sBalanceUp1 + " (" +
                            Math.Round(dBalance1 / dLastPrice, 2) + " " + sBalanceUp2 + ")";
                        labelSellHave.Text = Math.Round(dBalance2, 2) + " " + sBalanceUp2 + " (" +
                            Math.Round(dBalance2 * dLastPrice, 2) + " "+sBalanceUp1 + ")";
                        
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
        static long GetTimestampNow()
        {
            //ticks /= 10000000; //Convert windows ticks to seconds
            return DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
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
        public Dictionary<string, object> GetInfo()
        {
            var sResultTmp = Query(new Dictionary<string, string>()
            {
                { "method", "getInfo" }
            });
            var result = Json.Deserialize(sResultTmp) as Dictionary<string, object>;
            if ((long)result["success"] == 0)
                throw new Exception((string)result["error"]);
            return (Dictionary<string, object>)result["return"];
        }
        public Dictionary<string, object> GetTradeHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null
            )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "TradeHistory" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());

            var result = Json.Deserialize(Query(args)) as Dictionary<string, object>;
            if ((long)result["success"] == 0)
                throw new Exception((string)result["error"]);
            return (Dictionary<string, object>)result["return"];
        }
        public Dictionary<string, object> GetOpenOrders(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null,
            //BtcePair? pair = null,
            bool? active = null
            )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "ActiveOrders" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
            //if (pair != null) args.Add("pair", BtcePairHelper.ToString(pair.Value));
            if (active != null) args.Add("active", active.Value ? "1" : "0");

            var result = Json.Deserialize(Query(args)) as Dictionary<string, object>;
            if ((long)result["success"] == 0)
            {
                if ((string)result["error"] == "no orders")
                {
                    return new Dictionary<string, object>();
                }
                else
                throw new Exception((string)result["error"]);
            }
            return (Dictionary<string, object>)result["return"];
        }
        public Dictionary<string, object> Trade(string pair, string type, double rate, double amount)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair", pair },
                { "type", type },
                { "rate", rate.ToString() },
                { "amount", amount.ToString() }
            };
            var result = Json.Deserialize(Query(args)) as Dictionary<string, object>;
            if ((long)result["success"] == 0)
                throw new Exception((string)result["error"]);
            return (Dictionary<string, object>)result["return"];
        }
        static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        static string BuildPostData(Dictionary<string, string> d)
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in d)
            {
                s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
                s.Append("&");
            }
            if (s.Length > 0) s.Remove(s.Length - 1, 1);
            return s.ToString();
        }
        UInt32 GetNonce()
        {
            return nonce++;
        }
        string Query(Dictionary<string, string> args)
        {
            args.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", Setting.APIKey);
            request.Headers.Add("Sign", ByteArrayToString(APISecretHash.ComputeHash(data)).ToLower());
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
        public void UpdateDepth()
        {
            double dDepth = 0;

            //listViewAsk.DoubleBuffered = true;
            listViewAsk.BeginUpdate();
            listViewAsk.Items.Clear();
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
            listViewBid.BeginUpdate();
            listViewBid.Items.Clear();
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
        public void UpdateHistory(string sPair, int iLimit)
        {
            double dLastTimestamp = 0;
            double dLastLastTimestamp = 0;
            double dPrice = 0;
            Dictionary<string, object> dTmp;
            DateTime dtTick;
            try
            {
                var dJson = GetJson("https://btc-e.com/api/3/trades/" + sPair + "?ignore_invalid=1&limit=" + iLimit);
                var dJsonHistory = dJson[sPair] as List<object>;
                dJsonHistory.Reverse();
                int i = 0;
                foreach (object obj in dJsonHistory)
                {
                    dTmp = (Dictionary<string, object>)obj;

                    dPrice = Convert.ToDouble(dTmp["price"]);
                    dtTick = ConvertFromUnixTimestamp(Convert.ToDouble(dTmp["timestamp"]));
                    dtTick = dtFloor(dtTick, new TimeSpan(0, 1, 0));
                    dLastTimestamp = dtTick.ToOADate();

                    if (dLastLastTimestamp != dLastTimestamp)
                    {
                        if (!dVolumeDataLocked.ContainsKey(dLastTimestamp))
                        {
                            dVolumeData[dLastTimestamp] = 0;
                        }
                        dVolumeDataLocked[dLastLastTimestamp] = true;
                    }

                    dLastLastTimestamp = dLastTimestamp;

                    if (dVolumeDataLocked.ContainsKey(dLastTimestamp))
                    {
                        continue;
                    }

                    if (dCandlestickData.Count > Setting.ShowInterval)
                    {
                        dCandlestickData.Remove(dCandlestickData.Keys.Min());
                    }
                    if (dCandlestickData.ContainsKey(dLastTimestamp))
                    {
                        dCandlestickData[dLastTimestamp].Close = dPrice;
                        if (dCandlestickData[dLastTimestamp].Low > dPrice)
                        {
                            dCandlestickData[dLastTimestamp].Low = dPrice;
                        }
                        if (dCandlestickData[dLastTimestamp].High < dPrice)
                        {
                            dCandlestickData[dLastTimestamp].High = dPrice;
                        }
                    }
                    else
                    {
                        dCandlestickData[dLastTimestamp] = 
                            new CandlestickData(dLastTimestamp, dPrice, dPrice, dPrice, dPrice);
                    }

                    if (dVolumeData.Count > Setting.ShowInterval)
                    {
                        dVolumeData.Remove(dVolumeData.Keys.Min());
                    }
                    if (dVolumeData.ContainsKey(dLastTimestamp))
                    {
                        dVolumeData[dLastTimestamp] += Convert.ToDouble(dTmp["amount"]);
                    }
                    else
                    {
                        dVolumeData[dLastTimestamp] = 0;
                    }

                    i++;
                }
                dLastPrice = dPrice;
            }
            catch (Exception e){}
        }

        public Dictionary<double, double> dVolumeData = new Dictionary<double, double>();
        public Dictionary<double, bool> dVolumeDataLocked = new Dictionary<double, bool>();
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
            ChartMain.Series["Area"].Points.Clear();

            foreach (KeyValuePair<double, double> kv in dVolumeData)
            {
                ChartMain.Series["Area"].Points.AddXY(kv.Key, kv.Value);
            }
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
            ChartMain.Series["LineNow"].Points.AddXY(csdLast.Date, dLastPrice);
            ChartMain.Series["LineNow"].Points.AddXY(csdLast.Date - 60, dLastPrice);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Opacity = Setting.OpacityWithFocus;
            Size = new Size(745, 557);
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = Setting.OpacityWithoutFocus;
            Size = new Size(311, 163);
        }

        // BUY Block /START
        private void buttonBuyV11_Click(object sender, EventArgs e)
        {
            textBoxBuyV.Text = dBalance1.ToString();
        }
        private void buttonBuyV12_Click(object sender, EventArgs e)
        {
            textBoxBuyV.Text = (dBalance1 / 2).ToString();
        }
        private void buttonBuyV14_Click(object sender, EventArgs e)
        {
            textBoxBuyV.Text = (dBalance1 / 4).ToString();
        }
        private void buttonBuyPL_Click(object sender, EventArgs e)
        {
            textBoxBuyP.Text = (dLastPrice).ToString();
        }
        private void buttonBuyPM_Click(object sender, EventArgs e)
        {
            textBoxBuyP.Text = Convert.ToDouble(((List<object>)((List<object>)dDepthData["asks"])[0])[0]).ToString();
        }
        private string oldTextBoxBuyP = String.Empty;
        private void textBoxP_TextChanged(object sender, EventArgs e)
        {
            double val;
            if (!Double.TryParse(textBoxBuyP.Text, out val))
            {
                textBoxBuyP.TextChanged -= textBoxP_TextChanged;
                textBoxBuyP.Text = oldTextBoxBuyP;

                //textBoxBuyP.CaretIndex = oldIndex;
                textBoxBuyP.TextChanged += textBoxP_TextChanged;
            }

            labeBuyV.Text = Math.Round(Convert.ToDouble(textBoxBuyV.Text) / val, 2) + " BTC";
        }
        private void textBoxBuyP_KeyDown(object sender, KeyEventArgs e)
        {
            oldTextBoxBuyP = textBoxBuyP.Text;
        }
        private string oldTextBoxBuyV = String.Empty;
        private void textBoxV_TextChanged(object sender, EventArgs e)
        {
            double val;
            if (!Double.TryParse(textBoxBuyV.Text, out val))
            {
                textBoxBuyV.TextChanged -= textBoxV_TextChanged;
                textBoxBuyV.Text = oldTextBoxBuyV;

                //textBoxBuyV.CaretIndex = oldIndex;
                textBoxBuyV.TextChanged += textBoxV_TextChanged;
            }

            labeBuyV.Text = Math.Round(val / Convert.ToDouble(textBoxBuyP.Text), 2) + " BTC";
        }
        private void textBoxBuyV_KeyDown(object sender, KeyEventArgs e)
        {
            oldTextBoxBuyV = textBoxBuyV.Text;
        }
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            Trade(Setting.TradingPair, "buy", Convert.ToDouble(textBoxBuyP.Text),
                Convert.ToDouble(textBoxBuyV.Text) / Convert.ToDouble(textBoxBuyP.Text));
        }
        // BUY Block END/

        // SELL Block /START
        private void buttonSellV11_Click(object sender, EventArgs e)
        {
            textBoxSellV.Text = dBalance2.ToString();
        }
        private void buttonSellV12_Click(object sender, EventArgs e)
        {
            textBoxSellV.Text = (dBalance2 / 2).ToString();
        }
        private void buttonSellV14_Click(object sender, EventArgs e)
        {
            textBoxSellV.Text = (dBalance2 / 4).ToString();
        }
        private void buttonSellL_Click(object sender, EventArgs e)
        {
            textBoxSellP.Text = (dLastPrice).ToString();
        }
        private void buttonSellM_Click(object sender, EventArgs e)
        {
            textBoxSellP.Text = Convert.ToDouble(((List<object>)((List<object>)dDepthData["bids"])[0])[0]).ToString();
        }
        //private int oldIndexBoxSellP = 0;
        private string oldTextBoxSellP = String.Empty;
        private void textBoxSellP_TextChanged(object sender, EventArgs e)
        {
            double val;
            if (!Double.TryParse(textBoxSellP.Text, out val))
            {
                textBoxSellP.TextChanged -= textBoxSellP_TextChanged;
                textBoxSellP.Text = oldTextBoxSellP;

                //textBoxSellP.CaretIndex = oldIndexBoxSellP;
                textBoxSellP.TextChanged += textBoxSellP_TextChanged;
            }

            labelSellV.Text = "$" + Math.Round(Convert.ToDouble(textBoxSellV.Text) * val, 2);
        }
        private void textBoxSellP_KeyDown(object sender, KeyEventArgs e)
        {
            //oldIndexBoxSellP = textBoxSellP.CaretIndex;
            oldTextBoxSellP = textBoxSellP.Text;
        }
        private string oldTextBoxSellV = String.Empty;
        private void textBoxSellV_TextChanged(object sender, EventArgs e)
        {
            double val;
            if (!Double.TryParse(textBoxSellV.Text, out val))
            {
                textBoxSellV.TextChanged -= textBoxSellV_TextChanged;
                textBoxSellV.Text = oldTextBoxSellV;

                //textBoxSellV.CaretIndex = oldIndex;
                textBoxSellV.TextChanged += textBoxSellV_TextChanged;
            }

            labelSellV.Text = "$" + Math.Round(val * Convert.ToDouble(textBoxSellP.Text), 2);
        }
        private void textBoxSellV_KeyDown(object sender, KeyEventArgs e)
        {
            //oldIndex = textBoxSellV.CaretIndex;
            oldTextBoxSellV = textBoxSellV.Text;
        }
        private void buttonSell_Click(object sender, EventArgs e)
        {
            Trade(Setting.TradingPair, "sell", Convert.ToDouble(textBoxSellP.Text),
                Convert.ToDouble(textBoxSellV.Text));
        }
        // SELL Block END/

        // CANCEL Order Block /START
        private void buttonCancelS_Click(object sender, EventArgs e)
        {

        }
        private void buttonCancelA_Click(object sender, EventArgs e)
        {

        }


        // CANCEL Orde Block END/


        private void comboBoxPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSelected = comboBoxPair.SelectedItem.ToString();

            string[] saPair = sSelected.Split('/');

            sBalance2 = saPair[0].ToLower();
            sBalance1 = saPair[1].ToLower();

            Setting.TradingPair = sBalance2 + "_" + sBalance1;

            sBalanceUp2 = saPair[0];
            sBalanceUp1 = saPair[1];

            dVolumeData = new Dictionary<double, double>();
            dVolumeDataLocked = new Dictionary<double, bool>();
            dCandlestickData = new Dictionary<double, CandlestickData>();

            UpdateHistory(Setting.TradingPair, 2000);
        }
    }
    public class WebApi
    {
        public static string Query(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
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
        public static double OpacityWithFocus { get; set; }
        public static double OpacityWithoutFocus { get; set; }
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
                        case "Double":
                            propInfo.SetValue(null, Convert.ToDouble(value), null);
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
    public static class UnixTime
    {
        static DateTime unixEpoch;
        static UnixTime()
        {
            unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public static UInt32 Now { get { return GetFromDateTime(DateTime.UtcNow); } }
        public static UInt32 GetFromDateTime(DateTime d) { return (UInt32)(d - unixEpoch).TotalSeconds; }
        public static DateTime ConvertToDateTime(UInt32 unixtime) { return unixEpoch.AddSeconds(unixtime); }
    }
}
