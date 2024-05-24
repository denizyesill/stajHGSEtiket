using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;


namespace stajHGSEtiket
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());


            // log4net konfigürasyonunu yükleyin
            XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Başlangıçta bir log mesajı yazın
            log.Info("Uygulama başlatıldı.");

            Application.Run(new Form1());

        }


       
        

       

    }
}
