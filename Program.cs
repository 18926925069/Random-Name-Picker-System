//version:v1.0
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace 随机点名系统
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // An unexpected exception is shown as a readable message instead of the default crash dialog, and the program
            // keeps running so the user can carry on with the day's roll call. Exceptions raised while the program is busy
            // with an operation are caught and handled where they occur; this handler is the last line of defence.
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Index());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // The name list contains names only, so the UI language is the best guess for the message language.
            bool english = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != "zh";
            string message = english
                ? "An unexpected error occurred:\n\n" + e.Exception.Message
                    + "\n\nThe program will keep running. Your name list is unchanged."
                : "程序遇到意外错误：\n\n" + e.Exception.Message
                    + "\n\n程序将继续运行，当前名单不会丢失。";
            MessageBox.Show(message, english ? "Random Roll Call" : "随机点名系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
