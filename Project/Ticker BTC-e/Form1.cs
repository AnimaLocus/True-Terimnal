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
            if (Setting.APIKey == "XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX")
            {
                bOnlyTicker = true;
            }

            APISecretHash = new HMACSHA512(Encoding.ASCII.GetBytes(Setting.APISecret));
            nonce = UnixTime.Now;

            InitializeComponent();

            Setting.TradingPair = "btc_usd";
            comboBoxPair.SelectedIndex = 0;

            UpdateHistory(Setting.TradingPair, 2000);

            Thread t = new Thread(NewThread);
            t.IsBackground = true;
            t.Start();
        }
        public bool bOnlyTicker = false;
        public HMACSHA512 APISecretHash;
        public UInt32 nonce;
        public Dictionary<string, object> dTmp = new Dictionary<string, object>();
        public Dictionary<string, object> dUserInfo = new Dictionary<string, object>();
        public Dictionary<string, double> dBalance = new Dictionary<string, double>();
        public Dictionary<string, object> dTradeHistory = new Dictionary<string, object>();
        public static Dictionary<string, object> dTradeHistoryGrouped = new Dictionary<string, object>();
        public List<string> lOpenOrdersIndex = new List<string>();
        public Dictionary<string, object> dOpenOrders = new Dictionary<string, object>();
        public double dLastPrice = 0;
        public double dBalance1 = 0;
        public double dBalance2 = 0;
        public string sBalance1 = "usd";
        public string sBalance2 = "btc";
        public string sBalanceUp1 = "USD";
        public string sBalanceUp2 = "BTC";
        public double dFee = 0.002;
        public double dSecLag = 0;
        void NewThread()
        {
            Random rnd = new Random();
            int i = 0;
            Dictionary<string, object> dTmp2;

            dBalance["usd"] = -1;
            dBalance["btc"] = -1;
            dBalance["ltc"] = -1;
            dBalance["nmc"] = -1;
            dBalance["rur"] = -1;
            dBalance["eur"] = -1;
            dBalance["nvc"] = -1;
            dBalance["trc"] = -1;
            dBalance["ppc"] = -1;
            dBalance["ftc"] = -1;
            dBalance["xpm"] = -1;
            if (File.Exists("balance.db"))
            {
                string[] saLines = File.ReadAllLines("balance.db");
                string sTmp = saLines[saLines.Length - 1];
                //foreach (string sTmp in saLines)
                //{
                string[] split = sTmp.Split(new char[] { '/' }, 12);
                //MessageBox.Show(split.Length.ToString());
                if (split.Length == 12)
                {
                    for (int iTmp = 1; iTmp < split.Length; iTmp++)
                    {
                        string[] split2 = split[iTmp].Split(new char[] { ':' }, 2);
                        dBalance[split2[0]] = Convert.ToDouble(split2[1]);
                    }
                }
                //}
            }
            while (true)
            {
                dSecLag = UnixTime.Now;
                try
                {
                    UpdateHistory(Setting.TradingPair, 500);
                    dDepthData = GetDepth(Setting.TradingPair);

                    if (bOnlyTicker)
                    {
                    }
                    else
                    {
                        UpdateBalance();

                        dOpenOrders = GetOpenOrders();
                        dTradeHistory = GetTradeHistory();
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        label1now.Text = dLastPrice + " " + sBalanceUp1;

                        UpdateDepth();

                        if (!bOnlyTicker)
                        {
                            UpdateTradeHistory();
                            UpdateOpenOrders();
                        }

                        /*
                        DataManipulator myDataManip = chart1.DataManipulator;
                        myDataManip.Filter(CompareMethod.LessThanOrEqualTo,
                            (dtTick.AddMinutes(-30)).ToOADate(),
                            "SeriesLine,SeriesLineVol", "SeriesLine,SeriesLineVol", "X");
                        */

                        labelBuyHave.Text = Math.Round(dBalance1, 2) + " " + sBalanceUp1 + " (" +
                            Math.Round(dBalance1 / dLastPrice * (1 - dFee), 2) + " " + sBalanceUp2 + ")";
                        labelSellHave.Text = Math.Round(dBalance2, 2) + " " + sBalanceUp2 + " (" +
                            Math.Round(dBalance2 * dLastPrice * (1 - dFee), 2) + " " + sBalanceUp1 + ")";

                        UpdateChartMain();

                        dSecLag = UnixTime.Now - dSecLag;
                        labelLag.Text = "Lag: " + Math.Round(Convert.ToDouble(Setting.UpdateInterval) / 1000, 2)
                            + "+" + dSecLag + " s.";
                    });
                }
                catch (Exception e)
                {

                }
                System.Threading.Thread.Sleep(Setting.UpdateInterval);
            }
        }
        public void UpdateOpenOrders()
        {
            Dictionary<string, object> dTmp2 = new Dictionary<string, object>();
            int i = 0;
            listViewOpenOrders.BeginUpdate();
            listViewOpenOrders.Items.Clear();
            lOpenOrdersIndex = new List<string>();
            dHighlightInDepthAsk = new List<double>();
            dHighlightInDepthBid = new List<double>();
            foreach (KeyValuePair<string, object> kv in dOpenOrders)
            {
                dTmp2 = (Dictionary<string, object>)kv.Value;

                lOpenOrdersIndex.Add(kv.Key);

                listViewOpenOrders.Items.Add(
                    (((string)dTmp2["pair"]).Replace("_", "/")).ToUpper(), i
                );
                if ((string)dTmp2["type"] == "buy")
                {
                    dHighlightInDepthBid.Add(Convert.ToDouble(dTmp2["rate"]));
                    listViewOpenOrders.Items[i].ForeColor = Color.FromArgb(0, 0, 128, 0);
                    dTmp2["type"] = "BUY";
                }
                else if ((string)dTmp2["type"] == "sell")
                {
                    dHighlightInDepthAsk.Add(Convert.ToDouble(dTmp2["rate"]));
                    listViewOpenOrders.Items[i].ForeColor = Color.FromArgb(0, 128, 0, 0);
                    dTmp2["type"] = "SELL";
                }
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
            Dictionary<string, object> dStopLossesCopy =
                dStopLosses.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (KeyValuePair<string, object> kv in dStopLossesCopy)
            {
                dTmp2 = (Dictionary<string, object>)kv.Value;

                if ((string)dTmp2["pair"] == Setting.TradingPair)
                {
                    if (dLastPrice <= Convert.ToDouble(dTmp2["rate"]))
                    {
                        Trade((string)dTmp2["pair"], "sell", Convert.ToDouble(dTmp2["rate"]),
                            Convert.ToDouble(dTmp2["amount"]));
                        dStopLosses.Remove(kv.Key);
                        continue;
                    }
                    //dBalance2 -= Convert.ToDouble(dTmp2["amount"]);
                }

                listViewOpenOrders.Items.Add(
                    (((string)dTmp2["pair"]).Replace("_", "/")).ToUpper(), i
                );
                listViewOpenOrders.Items[i].ForeColor = Color.FromArgb(0, 75, 0, 130);
                listViewOpenOrders.Items[i].SubItems.Add(
                    "STOP"
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
            Dictionary<string, object> dTakeProfitCopy =
                dTakeProfit.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (KeyValuePair<string, object> kv in dTakeProfitCopy)
            {
                dTmp2 = (Dictionary<string, object>)kv.Value;

                if ((string)dTmp2["pair"] == Setting.TradingPair)
                {
                    if (dLastPrice >= Convert.ToDouble(dTmp2["rate"]))
                    {
                        Trade((string)dTmp2["pair"], "sell", Convert.ToDouble(dTmp2["rate"]),
                            Convert.ToDouble(dTmp2["amount"]));
                        dTakeProfit.Remove(kv.Key);
                        continue;
                    }
                    //dBalance2 -= Convert.ToDouble(dTmp2["amount"]);
                }

                listViewOpenOrders.Items.Add(
                    (((string)dTmp2["pair"]).Replace("_", "/")).ToUpper(), i
                );
                listViewOpenOrders.Items[i].ForeColor = Color.MediumVioletRed;
                listViewOpenOrders.Items[i].SubItems.Add(
                    "TAKE"
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
        public void UpdateTradeHistory()
        {
            Dictionary<string, object>  dTmp2 = new Dictionary<string, object>();

            dBalance1 = Convert.ToDouble(((Dictionary<string, object>)dUserInfo["funds"])[sBalance1]);
            if (dBalance1 < 0.0001) dBalance1 = 0;
            dBalance2 = Convert.ToDouble(((Dictionary<string, object>)dUserInfo["funds"])[sBalance2]);
            if (dBalance2 < 0.0001) dBalance2 = 0;

            dTradeHistoryGrouped = new Dictionary<string, object>();
            Dictionary<string, object> dLast = new Dictionary<string, object>();
            int i = 0; dLast["pair"] = ""; dLast["type"] = ""; dLast["rate"] = 0; dLast["amount"] = 0;
            dLast["timestamp"] = 0;
            foreach (KeyValuePair<string, object> kv in dTradeHistory)
            {
                dTmp2 = (Dictionary<string, object>)kv.Value;

                if (dLast["pair"].ToString() == dTmp2["pair"].ToString()
                    && dLast["type"].ToString() == dTmp2["type"].ToString()
                    && dLast["rate"].ToString() == dTmp2["rate"].ToString())
                {
                    dLast["amount"] =
                        Convert.ToDouble(dTmp2["amount"]) + Convert.ToDouble(dLast["amount"]);
                    continue;
                }
                else
                {
                    if (dLast["rate"].ToString() != "0")
                    {
                        dTradeHistoryGrouped[i.ToString()] = dLast;
                    }

                    dLast = new Dictionary<string, object>();
                    dLast["pair"] = dTmp2["pair"]; dLast["type"] = dTmp2["type"];
                    dLast["rate"] = dTmp2["rate"]; dLast["amount"] = dTmp2["amount"];
                    dLast["timestamp"] = dTmp2["timestamp"];

                    i++;
                }
            }
            listViewHistory.BeginUpdate();
            listViewHistory.Items.Clear();
            i = 0;
            foreach (KeyValuePair<string, object> kv in dTradeHistoryGrouped)
            {
                dTmp2 = (Dictionary<string, object>)kv.Value;

                listViewHistory.Items.Add(
                    (((string)dTmp2["pair"]).Replace("_", "/")).ToUpper(), i
                );

                if ((string)dTmp2["type"] == "buy")
                {
                    listViewHistory.Items[i].ForeColor = Color.FromArgb(0, 0, 128, 0);
                    dTmp2["type"] = "BUY";
                }
                else if ((string)dTmp2["type"] == "sell")
                {
                    listViewHistory.Items[i].ForeColor = Color.FromArgb(0, 128, 0, 0);
                    dTmp2["type"] = "SELL";
                }
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
                double dTmpRate = Convert.ToDouble(dTmp2["rate"]);
                listViewHistory.Items[i].SubItems.Add(
                    (dTmpRate * (1 + dFee + dFee)).ToString()
                );
                listViewHistory.Items[i].SubItems.Add(
                    (dTmpRate * (1.01 + dFee + dFee)).ToString()
                );
                listViewHistory.Items[i].SubItems.Add(
                    (dTmpRate * (1.02 + dFee + dFee)).ToString()
                );
                listViewHistory.Items[i].SubItems.Add(
                    (dTmpRate * (1.04 + dFee + dFee)).ToString()
                );
                listViewHistory.Items[i].SubItems.Add(
                    (dTmpRate * (1.08 + dFee + dFee)).ToString()
                );

                i++;
            }
            listViewHistory.EndUpdate();
        }
        public void UpdateBalance()
        {
            dUserInfo = GetInfo();
            bool bBalanceNeedUpdate = false;
            foreach (KeyValuePair<string, object> kv in (Dictionary<string, object>)dUserInfo["funds"])
            {
                if (dBalance[kv.Key] != Convert.ToDouble(kv.Value))
                {
                    bBalanceNeedUpdate = true;
                    dBalance[kv.Key] = Convert.ToDouble(kv.Value);
                }
            }
            if (bBalanceNeedUpdate)
            {
                string sContent = DateTime.Now.ToString();
                foreach (KeyValuePair<string, double> kv in dBalance)
                {
                    sContent = sContent + "/" + kv.Key + ":" + kv.Value;
                }
                sContent = sContent + "\n";
                File.AppendAllText("balance.db", sContent);
            }
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
        static long GetTimestampNow()
        {
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
        public Dictionary<string, object> CancelOrder(string orderId)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "CancelOrder" },
                { "order_id", orderId }
            };
            var result = Json.Deserialize(Query(args)) as Dictionary<string, object>;
            if ((long)result["success"] == 0)
            {
                string[] split = ((string)result["error"]).Split(new char[] { ';' }, 2);
                if (split[0] == "invalid nonce parameter")
                {
                    CancelOrder(orderId);
                }
                else
                {
                    throw new Exception((string)result["error"]);
                }
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
            {
                string[] split = ((string)result["error"]).Split(new char[] { ';' }, 2);
                if (split[0] == "invalid nonce parameter")
                {
                    Trade(pair, type, rate, amount);
                }
                else
                {
                    throw new Exception((string)result["error"]);
                }
            }
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

        public double dDepthMinBold = 9999999999999999999;
        public double dLastDepth = 9999999999999999999;
        public List<double> dHighlightInDepthAsk = new List<double>();
        public List<double> dHighlightInDepthBid = new List<double>();
        public void UpdateDepth()
        {
            double dDepth = 0;
            double dTmpRate = 0;

            listViewAsk.BeginUpdate();
            listViewAsk.Items.Clear();
            var lAsks = (List<object>)dDepthData["asks"];
            dDepthMinBold = dLastDepth / lAsks.Count;
            for (int i = 0; i < lAsks.Count; i++)
            {
                dDepth += Convert.ToDouble
                    (
                        ((List<object>)lAsks[i])[1]
                    );

                dTmpRate = Convert.ToDouble
                    (
                        ((List<object>)lAsks[i])[0]
                    );
                listViewAsk.Items.Add(
                    dTmpRate.ToString()
                );

                foreach (double dTmp in dHighlightInDepthAsk)
                {
                    if (dTmpRate == dTmp)
                    {
                        listViewAsk.Items[i].BackColor = Color.Gainsboro;
                    }
                }

                if (Convert.ToDouble(((List<object>)lAsks[i])[1]) > dDepthMinBold)
                    listViewAsk.Items[i].Font = new Font(listViewAsk.Items[i].Font, FontStyle.Bold);
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

                dTmpRate = Convert.ToDouble
                    (
                        ((List<object>)lBids[i])[0]
                    );
                listViewBid.Items.Add(
                    dTmpRate.ToString()
                );

                foreach (double dTmp in dHighlightInDepthBid)
                {
                    if (dTmpRate == dTmp)
                    {
                        listViewBid.Items[i].BackColor = Color.Gainsboro;
                    }
                }

                if (Convert.ToDouble(((List<object>)lBids[i])[1]) > dDepthMinBold)
                    listViewBid.Items[i].Font = new Font(listViewBid.Items[i].Font, FontStyle.Bold);
                listViewBid.Items[i].SubItems.Add(
                    Convert.ToDouble
                    (
                        ((List<object>)lBids[i])[1]
                    ).ToString()
                );
                listViewBid.Items[i].SubItems.Add(dDepth.ToString());
            }
            listViewBid.EndUpdate();

            dLastDepth = dDepth;
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
            catch (Exception e) { }
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
            ChartMain.Series["LineMA1"].Points.Clear();
            ChartMain.Series["LineMA2"].Points.Clear();
            ChartMain.Series["Candlestick"].Points.Clear();
            ChartMain.Series["Area"].Points.Clear();
            ChartMain.Series["LineNow"].Points.Clear();

            var dCandlestickDataSorted = from pair in dCandlestickData
                        orderby pair.Key ascending
                        select pair;
            foreach (KeyValuePair<double, CandlestickData> kv in dCandlestickDataSorted)
            {
                if (dVolumeData.ContainsKey(kv.Key))
                {
                    ChartMain.Series["Area"].Points.AddXY(kv.Key, dVolumeData[kv.Key]);
                }
                else
                {
                    ChartMain.Series["Area"].Points.AddXY(kv.Key, 0);
                }

                ChartMain.Series["Line"].Points.AddXY(kv.Key, kv.Value.Close);

                ChartMain.Series["LineNow"].Points.AddXY(kv.Key, dLastPrice);

                ChartMain.Series["Candlestick"].Points.AddXY(kv.Key, kv.Value.High);
                ChartMain.Series["Candlestick"].Points[i].YValues[1] = kv.Value.Low;
                ChartMain.Series["Candlestick"].Points[i].YValues[2] = kv.Value.Open;
                ChartMain.Series["Candlestick"].Points[i].YValues[3] = kv.Value.Close;

                i++;
            }

            if (i > 7)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "7", "Line:Y", "LineMA1:Y");
            }
            if (i > 14)
            {
                ChartMain.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, "14", "Line:Y", "LineMA2:Y");
            }
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
            checkBoxTop_CheckedChanged(sender, e);
            this.Opacity = Setting.OpacityWithFocus;
            Size = new Size(745, 578);
            Refresh();
            Update();
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            checkBoxTop_CheckedChanged(sender, e);
            if (checkBoxOpacity.Checked) this.Opacity = Setting.OpacityWithoutFocus;
            if (checkBoxResize.Checked) Size = new Size(311, 163);
            Refresh();
            Update();
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
        private void buttonBuyV18_Click(object sender, EventArgs e)
        {
            textBoxBuyV.Text = (dBalance1 / 8).ToString();
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

            if (val != 0)
            {
                labeBuyV.Text = Math.Round(Convert.ToDouble(textBoxBuyV.Text) / val * (1 - dFee), 2) + " " + sBalanceUp2;
                labelBuyProcent.Text = ((val - dLastPrice) / dLastPrice * 100).ToString("+0.00;-0.00;0") + "%";
            }
            else
            {
                labelBuyProcent.Text = "0%";
            }
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

            if (textBoxBuyP.Text != "0")
                labeBuyV.Text = Math.Round(val / Convert.ToDouble(textBoxBuyP.Text) * (1 - dFee), 2) + " " + sBalanceUp2;
        }
        private void textBoxBuyV_KeyDown(object sender, KeyEventArgs e)
        {
            oldTextBoxBuyV = textBoxBuyV.Text;
        }
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            Trade(Setting.TradingPair, "buy", Convert.ToDouble(textBoxBuyP.Text),
                Math.Floor(Convert.ToDouble(textBoxBuyV.Text) / Convert.ToDouble(textBoxBuyP.Text) * 100000000) / 100000000);
            textBoxBuyV.Text = "0";
            textBoxBuyP.Text = "0";
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
        private void buttonSellV18_Click(object sender, EventArgs e)
        {
            textBoxSellV.Text = (dBalance2 / 8).ToString();
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

            labelSellV.Text = Math.Round(Convert.ToDouble(textBoxSellV.Text) * val * (1 - dFee), 2) + " " + sBalanceUp1;

            if (val != 0)
            {
                labelSellProcent.Text = ((val - dLastPrice) / dLastPrice * 100).ToString("+0.00;-0.00;0") + "%";
            }
            else
            {
                labelSellProcent.Text = "0%";
            }
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

            labelSellV.Text = Math.Round(val * Convert.ToDouble(textBoxSellP.Text) * (1 - dFee), 2) + " " + sBalanceUp1;
        }
        private void textBoxSellV_KeyDown(object sender, KeyEventArgs e)
        {
            //oldIndex = textBoxSellV.CaretIndex;
            oldTextBoxSellV = textBoxSellV.Text;
        }
        private void buttonSell_Click(object sender, EventArgs e)
        {
            Trade(Setting.TradingPair, "sell", Convert.ToDouble(textBoxSellP.Text),
                Math.Floor(Convert.ToDouble(textBoxSellV.Text) * 100000000) / 100000000);
            textBoxSellP.Text = "0";
            textBoxSellV.Text = "0";
        }
        // SELL Block END/

        // CANCEL Order Block /START
        private void buttonCancelS_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection lvTmp = listViewOpenOrders.SelectedItems;
            if (lvTmp.Count > 0)
            {
                if (lvTmp[0].SubItems[1].Text == "STOP")
                {
                    Dictionary<string, object> dStopLossesCopy =
                        dStopLosses.ToDictionary(entry => entry.Key, entry => entry.Value);
                    foreach (KeyValuePair<string, object> kv in dStopLossesCopy)
                    {
                        Dictionary<string, object> dTmp2 = (Dictionary<string, object>)kv.Value;
                        if (lvTmp[0].SubItems[2].Text == dTmp2["rate"].ToString()
                            && lvTmp[0].SubItems[3].Text == dTmp2["amount"].ToString())
                        {
                            dStopLosses.Remove(kv.Key);
                        }
                    }
                }
                else // Regular Buy/Sell
                {
                    string sSelected = lOpenOrdersIndex[lvTmp[0].Index];
                    CancelOrder(sSelected);
                }
            }

            UpdateOpenOrders();
        }
        private void buttonCancelA_Click(object sender, EventArgs e)
        {
            dStopLosses = new Dictionary<string, object>();
            foreach (string sSelected in lOpenOrdersIndex)
            {
                CancelOrder(sSelected);
            }

            UpdateOpenOrders();
        }


        // CANCEL Order Block END/


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

            ChartMain.Focus();

            UpdateHistory(Setting.TradingPair, 2000);
        }

        private void checkBoxResize_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxOpacity_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxTop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTop.Checked)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void listViewAsk_DoubleClick(object sender, EventArgs e)
        {
            textBoxBuyP.Text = listViewAsk.SelectedItems[0].Text;
            textBoxSellP.Text = listViewAsk.SelectedItems[0].Text;
        }

        private void listViewBid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxBuyP.Text = listViewBid.SelectedItems[0].Text;
            textBoxSellP.Text = listViewBid.SelectedItems[0].Text;
        }

        private void listViewHistory_DoubleClick(object sender, EventArgs e)
        {
            textBoxBuyP.Text = listViewHistory.SelectedItems[0].SubItems[2].Text;
            textBoxSellP.Text = listViewHistory.SelectedItems[0].SubItems[2].Text;
        }

        private void listViewOpenOrders_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxBuyP.Text = listViewOpenOrders.SelectedItems[0].SubItems[2].Text;
            textBoxSellP.Text = listViewOpenOrders.SelectedItems[0].SubItems[2].Text;
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBoxBuyV.Text = "0";
            textBoxSellV.Text = "0";
            textBoxBuyP.Text = "0";
            textBoxSellP.Text = "0";
        }

        public Dictionary<string, object> dStopLosses = new Dictionary<string, object>();
        private void buttonStopLoss_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dTmpStopLoss = new Dictionary<string, object>();
            dTmpStopLoss["pair"] = Setting.TradingPair;
            dTmpStopLoss["rate"] = Convert.ToDouble(textBoxSellP.Text);
            dTmpStopLoss["amount"] = Convert.ToDouble(textBoxSellV.Text);
            if (dBalance2 - Convert.ToDouble(dTmpStopLoss["amount"]) < 0
                || Convert.ToDouble(dTmpStopLoss["rate"]) <= 0
                || Convert.ToDouble(dTmpStopLoss["amount"]) <= 0)
            {
                return;
            }
            dStopLosses.Add(dStopLosses.Count.ToString(), dTmpStopLoss);
            textBoxSellV.Text = "0";
            textBoxSellP.Text = "0";
        }
        public Dictionary<string, object> dTakeProfit = new Dictionary<string, object>();
        private void buttonTake_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dTmpTakeProfit = new Dictionary<string, object>();
            dTmpTakeProfit["pair"] = Setting.TradingPair;
            dTmpTakeProfit["rate"] = Convert.ToDouble(textBoxSellP.Text);
            dTmpTakeProfit["amount"] = Convert.ToDouble(textBoxSellV.Text);
            if (dBalance2 - Convert.ToDouble(dTmpTakeProfit["amount"]) < 0
                || Convert.ToDouble(dTmpTakeProfit["rate"]) <= 0
                || Convert.ToDouble(dTmpTakeProfit["amount"]) <= 0)
            {
                return;
            }
            dTakeProfit.Add(dTakeProfit.Count.ToString(), dTmpTakeProfit);
            textBoxSellV.Text = "0";
            textBoxSellP.Text = "0";
        }

        FormBalance FormBalanceInstance = new FormBalance();
        private void buttonBalance_Click(object sender, EventArgs e)
        {
            FormBalanceInstance.Show();
        }

        FormEdit FormEditInstance = new FormEdit();
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection lvTmp = listViewOpenOrders.SelectedItems;
            if (lvTmp.Count > 0)
            {
                if (lvTmp[0].SubItems[1].Text == "STOP")
                {
                }
                else
                {
                    FormEditInstance.sSelected = lOpenOrdersIndex[lvTmp[0].Index];
                }
                string[] saPair = (lvTmp[0].Text).Split('/');

                FormEditInstance.sBalanceUp2 = saPair[0].ToUpper();
                FormEditInstance.sBalanceUp1 = saPair[1].ToUpper();
                FormEditInstance.sType = lvTmp[0].SubItems[1].Text.ToLower();

                FormEditInstance.labelOldV.Text = lvTmp[0].SubItems[4].Text + " " + FormEditInstance.sBalanceUp1;
                FormEditInstance.labelOldP.Text = lvTmp[0].SubItems[2].Text;
                FormEditInstance.labelOldA.Text = lvTmp[0].SubItems[3].Text + " " + FormEditInstance.sBalanceUp2;
                FormEditInstance.textNewV.Text = lvTmp[0].SubItems[4].Text;
                FormEditInstance.textNewP.Text = lvTmp[0].SubItems[2].Text;
                FormEditInstance.labelNewA.Text = (Convert.ToDouble(FormEditInstance.textNewV.Text)
                    / Convert.ToDouble(FormEditInstance.textNewP.Text))
                    + " " + FormEditInstance.sBalanceUp2;

                FormEditInstance.Show();
            }
        }

        FormBigChart FormBigChartInstance = new FormBigChart();
        private void buttonBigChart_Click(object sender, EventArgs e)
        {
            FormBigChartInstance.ChartMain.Series["Area"].Points.Clear();
            FormBigChartInstance.ChartMain.Series["Candlestick"].Points.Clear();
            FormBigChartInstance.ChartMain.Series["LineNow"].Points.Clear();
            FormBigChartInstance.ChartMain.Series["LineMA1"].Points.Clear();
            FormBigChartInstance.ChartMain.Series["LineMA2"].Points.Clear();
            
            foreach (DataPoint dp in ChartMain.Series["Area"].Points)
            {
                FormBigChartInstance.ChartMain.Series["Area"].Points.AddXY(dp.XValue, dp.YValues[0]);
            }

            int i = 0;
            foreach (DataPoint dp in ChartMain.Series["Candlestick"].Points)
            {
                FormBigChartInstance.ChartMain.Series["Candlestick"].Points.AddXY(dp.XValue, dp.YValues[0]);
                FormBigChartInstance.ChartMain.Series["Candlestick"].Points[i].YValues[1] = dp.YValues[1];
                FormBigChartInstance.ChartMain.Series["Candlestick"].Points[i].YValues[2] = dp.YValues[2];
                FormBigChartInstance.ChartMain.Series["Candlestick"].Points[i].YValues[3] = dp.YValues[3];
                i++;
            }

            foreach (DataPoint dp in ChartMain.Series["LineNow"].Points)
            {
                FormBigChartInstance.ChartMain.Series["LineNow"].Points.AddXY(dp.XValue, dp.YValues[0]);
            }

            foreach (DataPoint dp in ChartMain.Series["LineMA1"].Points)
            {
                FormBigChartInstance.ChartMain.Series["LineMA1"].Points.AddXY(dp.XValue, dp.YValues[0]);
            }
            foreach (DataPoint dp in ChartMain.Series["LineMA2"].Points)
            {
                FormBigChartInstance.ChartMain.Series["LineMA2"].Points.AddXY(dp.XValue, dp.YValues[0]);
            }


            FormBigChartInstance.ChartMACD.Series["Line"].Points.Clear();
            FormBigChartInstance.ChartMACD.Series["EMA1"].Points.Clear();
            FormBigChartInstance.ChartMACD.Series["EMA2"].Points.Clear();
            FormBigChartInstance.ChartMACD.Series["MACD"].Points.Clear();
            FormBigChartInstance.ChartMACD.Series["Signal"].Points.Clear();
            FormBigChartInstance.ChartMACD.Series["Historgam"].Points.Clear();
            foreach (DataPoint dp in ChartMain.Series["Line"].Points)
            {
                FormBigChartInstance.ChartMACD.Series["Line"].Points.AddXY(dp.XValue, dp.YValues[0]);
            }
            FormBigChartInstance.ChartMACD.DataManipulator.FinancialFormula(
                FinancialFormula.ExponentialMovingAverage, "12", "Line:Y", "EMA1:Y"); // For MACD Line
            FormBigChartInstance.ChartMACD.DataManipulator.FinancialFormula(
                FinancialFormula.ExponentialMovingAverage, "26", "Line:Y", "EMA2:Y"); // For MACD Line
            i = 0;
            foreach (DataPoint dp in FormBigChartInstance.ChartMACD.Series["EMA2"].Points)
            {
                FormBigChartInstance.ChartMACD.Series["MACD"].Points.AddXY(dp.XValue,
                    FormBigChartInstance.ChartMACD.Series["EMA1"].Points[i].YValues[0] - dp.YValues[0]);
                i++;
            }
            FormBigChartInstance.ChartMACD.DataManipulator.FinancialFormula(
                FinancialFormula.ExponentialMovingAverage, "9", "MACD:Y", "Signal:Y"); // Signal MACD Line
            i = 0;
            foreach (DataPoint dp in FormBigChartInstance.ChartMACD.Series["Signal"].Points)
            {
                FormBigChartInstance.ChartMACD.Series["Historgam"].Points.AddXY(dp.XValue,
                    FormBigChartInstance.ChartMACD.Series["MACD"].Points[i].YValues[0] - dp.YValues[0]);
                i++;
            }

            FormBigChartInstance.Show();
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
        public static double OpacityWithFocus { get; set; }
        public static double OpacityWithoutFocus { get; set; }
        static readonly string SETTINGS = "config.ini";
        static readonly Setting instance = new Setting();
        Setting()
        {
        }
        static Setting()
        {
            string property = "";
            try
            {
                string[] settings = File.ReadAllLines(SETTINGS);
                foreach (string s in settings)
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
            }
            catch
            {
                Setting.ShowInterval = 60;
                Setting.UpdateInterval = 5000;
                Setting.TradingPair = "btc_usd";
                Setting.APIKey = "XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX";
                Setting.APISecret = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                Setting.OpacityWithFocus = 1;
                Setting.OpacityWithoutFocus = 0.69;
                //throw new Exception("Invalid setting '" + property + "'");
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
