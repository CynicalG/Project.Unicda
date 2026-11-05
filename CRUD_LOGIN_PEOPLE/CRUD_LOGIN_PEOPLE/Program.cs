using CRUD_LOGIN_PEOPLE;
using System;
using System.Windows.Forms;

namespace CRUD_LOGIN_PEOPLE
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var login = new FormLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }
            }
        }
    }
}