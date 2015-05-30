using System;
using System.Threading;
using System.Windows.Forms;

namespace StreamsMaster
{
    class Program
    {
        static void Main()//string[] args)
        {
            var streamMaster = new StreamsMaster();
            streamMaster.Run();
        }
    }
    class StreamsMaster
    {
        private readonly HotkeysService _hotkeysService = new HotkeysService();

        public void Run() //string[] args)
        {
            //const string app = "Mozilla Firefox";

            //VolumeService.ControlApp(app, 50);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Form1();
            _hotkeysService.RegisterHotkeys(mainForm);
            Application.Run(mainForm);
        }
    }
}