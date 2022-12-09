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

            //Attempt to place FORM 1 in upper right hand corner of screen.
            //NOT WORKING!
            this.Location = new Point(2275, 20);
            //Allows processor to handle other events in queue
            Application.DoEvents();
                        
            //Use this zipCode to fetch Weather Info and Forecast
            var zipCode = "66220";
            //Use Lenexa, KS zip as the defult value
            textBox1.Text = "66220";
            zipCode = textBox1.Text;


            var client = new HttpClient();

            //Pull Kanye Quote and post to LABEL 1
            var kanyeURL = "https://api.kanye.rest/";
            var kanyeResponse = client.GetStringAsync(kanyeURL).Result;
            var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
            var labelA = "      --- Compelling Thoughts ---  \n";
            var labelB = kanyeQuote + "\n                      -- Kanye West";
            label1.Text = labelA + labelB;
            Application.DoEvents();

            //Pull SECOND Kanye Quote and post to LABEL 2  -- USED FOR TESTING PURPOSES!!
            //var kanyeURL2 = "https://api.kanye.rest/";
            //var kanyeResponse2 = client.GetStringAsync(kanyeURL2).Result;
            //var kanyeQuote2 = JObject.Parse(kanyeResponse2).GetValue("quote").ToString();
            //
            //label5.Text = kanyeQuote2;
            //Application.DoEvents();




            //WEATHERAPI.COM Get Current Weather Conditions 
            var weatherURL = $"https://api.weatherapi.com/v1/forecast.json?key=4909eba08bf342b3b13214203220712&q={zipCode}&days=2&hour=12&aqi=no&alerts=no";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var location = JObject.Parse(wxResponse)["location"]["name"].ToString();///.ToUpper());
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["temp_f"].ToString()));
            var tempStr = Convert.ToString(temp);
            var condition = JObject.Parse(wxResponse)["current"]["condition"]["text"].ToString();
            var conditions = condition.Substring(0, Math.Min(8, condition.Length));
            var feelsLike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["feelslike_f"].ToString()));
            var windDir = JObject.Parse(wxResponse)["current"]["wind_dir"].ToString();
            var windSpeed = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["wind_mph"].ToString()));
            var gustSpeed = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["gust_mph"].ToString()));
            var humidity = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["humidity"].ToString()));
            
            //WEATHWERAPI.COM Get Tomorrow's Forecast Conditions
            var max = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["maxtemp_f"].ToString()));
            var min = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["mintemp_f"].ToString()));
            var maxWind = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["maxwind_mph"].ToString()));
            var rain = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["daily_chance_of_rain"].ToString()));
            var snow = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["daily_chance_of_snow"].ToString()));
            var cond = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["condition"]["text"].ToString();
            var conds = cond.Substring(0, Math.Min(8, cond.Length));
            var sunrise = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["astro"]["sunrise"].ToString().Replace("0", "");
            var sunset = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["astro"]["sunset"].ToString().Replace("0", "");

            var title = "         --- " + location + " Weather ---\n";
            var subtitle = "           Current Conditions:       \n";
            var text1 = "Currently: " + tempStr + "               Feels like: " + feelsLike + "\n";
            var text2 = "Conditions: " + conditions + "         Humidity: " + humidity + "% \n";
            var text3 = "Wind:   " + windDir + "   " + windSpeed + " mph       Gusts: " + gustSpeed + " mph\n"; 
            var text4 = "Sunrise: " + sunrise + "        Sunset: " + sunset;
            var footer = "           Tomorrow's Forecast:       \n";
            var text5 = "Hi Temp: " + max + "                 Low Temp: " + min + "\n";
            var text6 = cond + "                  Wind: " + maxWind + "mph\n";
            var text7 = "Chance of Rain:  " + rain + " %     Snow:  " + snow + "  % \n";

            label2.Text = title + subtitle + text1 + text2 + text3 + text4 + footer + text5 + text6 + text7;
            label6.Text = location;
            Application.DoEvents();
           




            //Pull BITCOIN Quote and post to LABEL 3
            var btcURL = "https://api.coindesk.com/v1/bpi/currentprice.json";
            var btcResponse = client.GetStringAsync(btcURL).Result;
            var btcQuote = Convert.ToDouble(JObject.Parse(btcResponse)["bpi"]["USD"]["rate"].ToString());
            btcQuote = System.Math.Round(btcQuote, 2);
            var bitcoinQuote1 = Convert.ToString($"         Current price of Bitcoin: \n" +
                $"                $ {btcQuote}  \n      As of:  {DateTime.Now.ToString("MM/dd/yy  hh:mm:ss tt")}");

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
            


            
            //Pull NEWS HEADLINES from CNBC and MSNBC and post to LABEL 5
            var newsURL = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=cnbc&language=en";
            var newsResponse = client.GetStringAsync(newsURL).Result;
            var newsHeadline = JObject.Parse(newsResponse)["results"][0]["title"].ToString();
            
            //Pull NEWS HEADLINES and post to LABEL 5
            var newsURL1 = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=msnbc&language=en";
            var newsResponse1 = client.GetStringAsync(newsURL1).Result;
            var newsHeadline1 = JObject.Parse(newsResponse1)["results"][0]["title"].ToString();
            var headline = "                 ---  NEWS  ---\n";
            label5.Text = headline + newsHeadline + "\n\n" + newsHeadline1;
            Application.DoEvents();



            //Program flow goes to line 40 on Program.cs
            /////////////////////////////////////////////////////////////////////////////////////
        }


        private void label4_Click(object sender, EventArgs e)
        {
            //Change the color and text to show updating data.
            label4.ForeColor = Color.LightGray;
            label4.Text = "Auto Updating";


            int upTimeSec = 0;

            int i = 0;
            int ctr = 0;

            var client = new HttpClient();


            //This is the TAG for the GOTO command for program recursion. 
            ProgramUpdate: 
            
            //Read the zipCode to see if it has been changed.
            var zipCode = textBox1.Text;

            var btcURL = "https://api.coindesk.com/v1/bpi/currentprice.json";
            var btcResponse = client.GetStringAsync(btcURL).Result;
            var quoteTime = JObject.Parse(btcResponse)["time"]["updated"].ToString();
            var btcQuote = Convert.ToDouble(JObject.Parse(btcResponse)["bpi"]["USD"]["rate"].ToString());
            btcQuote = System.Math.Round(btcQuote, 2);
            var bitcoinQuote1 = Convert.ToString($"         Current price of Bitcoin: \n" +
                $"                  $ {btcQuote}  \n      As of:  {DateTime.Now.ToString("MM/dd/yy  hh:mm:ss tt")}");
            Application.DoEvents();

            ///DO-WHILE STATEMENT *PREVIOUSLY WENT HERE///
           
            
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
            


            
            //WEATHWERAPI.COM Get Current Weather Conditions
            var weatherURL = $"https://api.weatherapi.com/v1/forecast.json?key=4909eba08bf342b3b13214203220712&q={zipCode}&days=2&hour=12&aqi=no&alerts=no";
            var wxResponse = client.GetStringAsync(weatherURL).Result;
            var location = JObject.Parse(wxResponse)["location"]["name"].ToString();///.ToUpper());
            var temp = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["temp_f"].ToString()));
            var tempStr = Convert.ToString(temp);
            var condition = JObject.Parse(wxResponse)["current"]["condition"]["text"].ToString();
            var feelsLike = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["feelslike_f"].ToString()));
            var windDir = JObject.Parse(wxResponse)["current"]["wind_dir"].ToString();
            var windSpeed = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["wind_mph"].ToString()));
            var gustSpeed = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["gust_mph"].ToString()));
            var humidity = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["current"]["humidity"].ToString()));

            //WEATHWERAPI.COM Get Tomorrow's Forecast Conditions
            var max = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["maxtemp_f"].ToString()));
            var min = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["mintemp_f"].ToString()));
            var maxWind = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["maxwind_mph"].ToString()));
            var rain = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["daily_chance_of_rain"].ToString()));
            var snow = Convert.ToInt32(double.Parse(JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["daily_chance_of_snow"].ToString()));
            var cond = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["day"]["condition"]["text"].ToString();
            var sunrise = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["astro"]["sunrise"].ToString().Replace("0", "");
            var sunset = JObject.Parse(wxResponse)["forecast"]["forecastday"][0]["astro"]["sunset"].ToString().Replace("0", "");

            var title = "         --- " + location + " Weather ---\n";
            var subtitle = "           Current Conditions:       \n";
            var text1 = "Currently: " + tempStr + "                Feels like: " + feelsLike + "\n";
            var text2 = "Conditions: " + condition + "         Humidity: " + humidity + " % \n";
            var text3 = "Wind:   " + windDir + "   " + windSpeed + " mph     Gusts: " + gustSpeed + " mph\n";
            var text4 = "Sunrise: " + sunrise + "         Sunset: " + sunset;
            var footer = "           Tomorrow's Forecast:       \n";
            var text5 = "Hi Temp: " + max + "                 Low Temp: " + min + "\n";
            var text6 = cond + "                    Winds: " + maxWind + "mph\n";
            var text7 = "Chance of Rain:  " + rain + " %     Snow:  " + snow + "  % \n";

            label2.Text = title + subtitle + text1 + text2 + text3 + text4 + footer + text5 + text6 + text7;
            label6.Text = location;
            Application.DoEvents();





             //Pull NEWS HEADLINES From Array 1 and post to LABEL 5???????
             var newsURL = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=cnbc&language=en";
             var newsResponse = client.GetStringAsync(newsURL).Result;
             var newsHeadline = JObject.Parse(newsResponse)["results"][1]["title"].ToString();
             Application.DoEvents();


             //Pull NEWS HEADLINES From Array 1 and post to LABEL 5
             var newsURL1 = "https://newsdata.io/api/1/news?apikey=pub_141850b41c3b3f8a3d35db517df733f591143&q=msnbc&language=en";
             var newsResponse1 = client.GetStringAsync(newsURL1).Result;
             var newsHeadline1 = JObject.Parse(newsResponse1)["results"][1]["title"].ToString();
             var headline = "                 ---  NEWS  ---\n";
             label5.Text = headline + newsHeadline + "\n\n" + newsHeadline1;
             



            for (i = 0; i < 10; i++)        //Iterates through 10 Thread.Sleeps -- (Pauses 10 sec.)
            {
                Application.DoEvents();     //DoEvents are used to allow for smoother window movement on screen
                Thread.Sleep(1000);         //Pauses for 1 second
                Application.DoEvents();
                upTimeSec++;                //Gets incremented every second
                //if (upTimeSec % 10 == 0)    //Fires at every 10 seconds
                //{
                //  Code placed in this scope could process other events on a separate interval --
                //    such as a CPU Monitor, Systm Monitoring, other API Calls, etc.
                //}

            }


            goto ProgramUpdate;

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
