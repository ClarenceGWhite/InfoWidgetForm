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
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace InfoWidgetForm
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();

            this.Location = new Point(2275, 20);
            Application.DoEvents();
            

            var client = new HttpClient();
            
            updateAll:
            var zipCode = "66220";
            textBox1.Text = "66220";
            zipCode = textBox1.Text;
           
           

            //Pull Kanye Quote and post to LABEL 1
            var kanyeURL = "https://api.kanye.rest/";
            var kanyeResponse = client.GetStringAsync(kanyeURL).Result;
            var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
            var labelA = "      --- Compelling Thoughts ---  \n";
            var labelB = kanyeQuote + "\n                      -- Kanye West";
            label1.Text = labelA + labelB;
            Application.DoEvents();

            //Pull SECOND Kanye Quote and post to LABEL 2  -- FOR TESTING PURPOSES
            //var kanyeURL2 = "https://api.kanye.rest/";
            //var kanyeResponse2 = client.GetStringAsync(kanyeURL2).Result;
            //var kanyeQuote2 = JObject.Parse(kanyeResponse2).GetValue("quote").ToString();
            //
            //label5.Text = kanyeQuote2;
            //Application.DoEvents();
            //label2.Update();
            //label2.Refresh();

            //Pull WEATHER INFO and post to LABEL 2
            var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=c16a202a9e4084b5d1d5ade63b102a98";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["temp"].ToString()));
            var tempStr = Convert.ToString(temp);
            var feelslike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["feels_like"].ToString()));
            var location = (JObject.Parse(wxResponse)["name"].ToString());///.ToUpper());
            var sunrise = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunrise"].ToString());
            var sunriseTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunrise)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            var sunset = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunset"].ToString());
            var sunsetTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunset)).AddHours(-6).ToString("hh:mm").TrimStart('0');


            //Pull WEATHER FORECAST and post to LABEL 2
            /*var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=c16a202a9e4084b5d1d5ade63b102a98";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["temp"].ToString()));
            var tempStr = Convert.ToString(temp);
            var feelslike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["feels_like"].ToString()));
            var location = (JObject.Parse(wxResponse)["name"].ToString().ToUpper());
            var sunrise = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunrise"].ToString());
            var sunriseTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunrise)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            var sunset = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunset"].ToString());
            var sunsetTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunset)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            */

            var title = "        --- " + location + " Weather ---\n";
            var text1 = "Currently: " + tempStr + "                Feels like: " + feelslike + "\n";
            var text2 = "";
            var text3 = "\n";
            var text4 = "\n";
            var text5 = "Sunrise: " + sunriseTime + "AM         Sunset: " + sunsetTime + "PM";

            label2.Text = title + text1 + text2 + text3 + text4 + text5;
            label6.Text = location;
            Application.DoEvents();
           




            //Pull BITCOIN Quote and post to LABEL 3
            var btcURL = "https://api.coindesk.com/v1/bpi/currentprice.json";
            var btcResponse = client.GetStringAsync(btcURL).Result;
            //var quoteTime = JObject.Parse(btcResponse)["time"]["updated"].ToString();
            //var timeUtc =  DateTime.UtcNow;
            //var centralZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            //var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, centralZone);
            var btcQuote = Convert.ToDouble(JObject.Parse(btcResponse)["bpi"]["USD"]["rate"].ToString());
            btcQuote = System.Math.Round(btcQuote, 2);
            var bitcoinQuote1 = Convert.ToString($"         Current price of Bitcoin: \n" +
                $"                $ {btcQuote}  \n      As of:  {DateTime.Now.ToString("MM/dd/yy  hh:mm:ss tt")}");

            //label3.Text = bitcoinQuote1;
            Application.DoEvents();
            

            // SP500 Quote 
            var finURL1 = $"https://financialmodelingprep.com/api/v3/quote/SPY?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse1 = client.GetStringAsync(finURL1).Result;
            var finHeadline1 = JArray.Parse(finResponse1)[0]["price"].ToString();
            var SP500 = (Convert.ToDecimal(finHeadline1) * 10);
            var spQuote = "              S&P:  $ " + SP500;
            // DJIA quote 
            var finURL2 = $"https://financialmodelingprep.com/api/v3/quote/DIA?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse2 = client.GetStringAsync(finURL2).Result;
            var finHeadline2 = JArray.Parse(finResponse2)[0]["price"].ToString();
            var DJ30 = (Convert.ToDecimal(finHeadline2) * 100);
            var djQuote = "            DJIA:  $ " + DJ30;
            // NASDAQ Quote
            var finURL3 = $"https://financialmodelingprep.com/api/v3/quote/QQQ?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse3 = client.GetStringAsync(finURL3).Result;
            var finHeadline3 = JArray.Parse(finResponse3)[0]["price"].ToString();
            var QQQ = (Convert.ToDecimal(finHeadline3) * 10);
            var QQQquote = "           NASDAQ:  $ " + QQQ;

            var header = "               ---  QUOTES  ---\n";
            label3.Text = header + bitcoinQuote1 + "\n\n" + spQuote + "\n" + djQuote + "\n" + QQQquote;
            Application.DoEvents();





            ////////////////////////////////////////






            //Pull NEWS HEADLINES and post to LABEL 5???????
            var newsURL = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=cnbc&language=en";
            var newsResponse = client.GetStringAsync(newsURL).Result;
            var newsHeadline = JObject.Parse(newsResponse)["results"][0]["title"].ToString();
            Application.DoEvents();
            //Pull NEWS HEADLINES and post to LABEL 5
            var newsURL1 = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=msnbc&language=en";
            var newsResponse1 = client.GetStringAsync(newsURL1).Result;
            var newsHeadline1 = JObject.Parse(newsResponse1)["results"][0]["title"].ToString();
            var headline = "                 ---  NEWS  ---\n";
            label5.Text = headline + newsHeadline + "\n\n" + newsHeadline1;

            




            //Program flow goes to line 40 on Program.cs
            /////////////////////////////////////////////////////////////////////////////////////
        }


        private void label4_Click(object sender, EventArgs e)
        {
            label4.ForeColor = Color.LightGray;
            label4.Text = "Auto Updating";

               

            int upTimeSec = 0;

            int i = 0;
            int ctr = 0;
            var client = new HttpClient();

        BitCoinUpdate:
            var zipCode = textBox1.Text;

            var btcURL = "https://api.coindesk.com/v1/bpi/currentprice.json";
            var btcResponse = client.GetStringAsync(btcURL).Result;
            var quoteTime = JObject.Parse(btcResponse)["time"]["updated"].ToString();
            var btcQuote = Convert.ToDouble(JObject.Parse(btcResponse)["bpi"]["USD"]["rate"].ToString());
            btcQuote = System.Math.Round(btcQuote, 2);
            var bitcoinQuote1 = Convert.ToString($"         Current price of Bitcoin: \n" +
                $"                  $ {btcQuote}  \n      As of:  {DateTime.Now.ToString("MM/dd/yy  hh:mm:ss tt")}");
            Application.DoEvents();

            ///DO-WHILE STATEMENT *PREVIOUSLY WENT HERE
           
            


            // SP500 Quote 
            var finURL1 = $"https://financialmodelingprep.com/api/v3/quote/SPY?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse1 = client.GetStringAsync(finURL1).Result;
            var finHeadline1 = JArray.Parse(finResponse1)[0]["price"].ToString();
            var SP500 = (Convert.ToDecimal(finHeadline1) * 10);
            var spQuote = "              S&P:  $ " + SP500;
            // DJIA quote 
            var finURL2 = $"https://financialmodelingprep.com/api/v3/quote/DIA?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse2 = client.GetStringAsync(finURL2).Result;
            var finHeadline2 = JArray.Parse(finResponse2)[0]["price"].ToString();
            var DJ30 = (Convert.ToDecimal(finHeadline2) * 100);
            var djQuote = "            DJIA:  $ " + DJ30;
            // NASDAQ Quote
            var finURL3 = $"https://financialmodelingprep.com/api/v3/quote/QQQ?apikey=8f5cca4629ea78f500bd0ccf609372cf";
            var finResponse3 = client.GetStringAsync(finURL3).Result;
            var finHeadline3 = JArray.Parse(finResponse3)[0]["price"].ToString();
            var QQQ = (Convert.ToDecimal(finHeadline3) * 10);
            var QQQquote = "           NASDAQ:  $ " + QQQ;

            var header = "               ---  QUOTES  ---\n";
            label3.Text = header + bitcoinQuote1 + "\n\n" + spQuote + "\n" + djQuote + "\n" + QQQquote;
            
            Application.DoEvents();






            //Pull WEATHER INFO and post to LABEL 2
            var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=c16a202a9e4084b5d1d5ade63b102a98";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["temp"].ToString()));
            var tempStr = Convert.ToString(temp);
            var feelslike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["feels_like"].ToString()));
            var location = (JObject.Parse(wxResponse)["name"].ToString());///.ToUpper());
            var sunrise = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunrise"].ToString());
            var sunriseTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunrise)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            var sunset = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunset"].ToString());
            var sunsetTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunset)).AddHours(-6).ToString("hh:mm").TrimStart('0');


            ///Pull WEATHER FORECAST and post to LABEL 2
            /*var weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zipCode}&units=imperial&appid=c16a202a9e4084b5d1d5ade63b102a98";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["temp"].ToString()));
            var tempStr = Convert.ToString(temp);
            var feelslike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["main"]["feels_like"].ToString()));
            var location = (JObject.Parse(wxResponse)["name"].ToString().ToUpper());
            var sunrise = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunrise"].ToString());
            var sunriseTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunrise)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            var sunset = Convert.ToInt32(JObject.Parse(wxResponse)["sys"]["sunset"].ToString());
            var sunsetTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(sunset)).AddHours(-6).ToString("hh:mm").TrimStart('0');
            */



            var title = "        --- " + location + " Weather ---\n";
            var text1 = "Currently: " + tempStr + "                Feels like: " + feelslike + "\n";
            var text2 = "";
            var text3 = "\n";
            var text4 = "\n";
            var text5 = "Sunrise: " + sunriseTime + "AM         Sunset: " + sunsetTime + "PM";

            label2.Text = title + text1 + text2 + text3 + text4 + text5;
            label6.Text = location;
            Application.DoEvents();






            /* //Pull NEWS HEADLINES and post to LABEL 5???????
             var newsURL = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=cnbc&language=en";
             var newsResponse = client.GetStringAsync(newsURL).Result;
             var newsHeadline = JObject.Parse(newsResponse)["results"][0]["title"].ToString();
             Application.DoEvents();


             //Pull NEWS HEADLINES and post to LABEL 5
             var newsURL1 = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=msnbc&language=en";
             var newsResponse1 = client.GetStringAsync(newsURL1).Result;
             var newsHeadline1 = JObject.Parse(newsResponse1)["results"][0]["title"].ToString();
             var headline = "                 ---  NEWS  ---\n";
             label5.Text = headline + newsHeadline + "\n\n" + newsHeadline1;
             */



            for (i = 0; i < 20; i++)
            {
                Application.DoEvents();
                Thread.Sleep(1000);
                Application.DoEvents();
                upTimeSec++;                //Gets incremented every second
                if (upTimeSec % 10 == 0) //Fires at 10 seconds
                {

                }

            }


            goto BitCoinUpdate;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }






        private void label6_Click(object sender, EventArgs e)
        {
           

        }



    }


}
