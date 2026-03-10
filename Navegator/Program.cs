using EasyTabs;
using System;
using System.Windows.Forms;

namespace Navegator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppContainer container = new AppContainer();

            TitleBarTabsApplicationContext context = new TitleBarTabsApplicationContext();
            context.Start(container);
            Application.Run();frmBrowser frmBrowser = new frmBrowser();
        }
    }
}