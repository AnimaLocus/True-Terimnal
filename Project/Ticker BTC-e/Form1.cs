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
            while (true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label1now.Text = GetTick("btc_usd");
                    label2now.Text = GetTick("ltc_usd");
                });
                System.Threading.Thread.Sleep(1000);
            }
        }

        static string GetTick(string sPair)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://btc-e.com/api/2/" + sPair + "/ticker");
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var sResult = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            try
            {
                var dJson = Json.Deserialize(sResult) as Dictionary<string, object>;
                var dJsonTicker = dJson["ticker"] as Dictionary<string, object>;
                sResult = ((double)dJsonTicker["last"]).ToString();
            }
            catch (Exception e)
            {

            }

            return sResult;
        }
    }
}
