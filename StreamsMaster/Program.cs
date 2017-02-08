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

        public void Run() //string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                using (var mainForm = new Form1())
                using (var hotkeysService = new HotkeysService())
                using (var pi = new ProcessIcon())
                {
                    pi.Display();
                    hotkeysService.RegisterHotkeys(mainForm);
                    var specialKeysService = new SpecialKeysService();
                    specialKeysService.Register();
                    Application.Run();
                }
        }
    }
}