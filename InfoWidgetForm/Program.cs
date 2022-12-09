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
            Application.SetCompatibleTextRenderingDefault(false);
            //This is the point at which FORM 1 appears.
            Application.Run(new Form1());
            Application.DoEvents();

            



        }








        



    }
}
