using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;





namespace InfoWidgetForm
{
    public class Program
    {
        //private static object label1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

                 

            
            Application.EnableVisualStyles();
            //Application.DoEvents();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.DoEvents();
            Application.Run(new Form1());
            Application.DoEvents();

            //var btcURL = "https://api.coindesk.com/v1/bpi/currentprice.json";
            //var btcResponse = client.GetStringAsync(btcURL).Result;
            //var quoteTime = JObject.Parse(btcResponse)["time"]["updated"].ToString();
            //var btcQuote = Convert.ToDouble(JObject.Parse(btcResponse)["bpi"]["USD"]["rate"].ToString());
            //btcQuote = System.Math.Round(btcQuote, 2);
            //var bitcoinQuote1 = Convert.ToString($"The current price of Bitcoin is: ${btcQuote} as of {quoteTime}");

            //return bitcoinQuote1;



        }








        



    }
}
