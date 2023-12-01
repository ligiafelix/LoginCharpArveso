using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginCharpArveso
{
    internal static class Program
    {
        public static Company oCompany;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             if (ConectarSAP()) {
                MessageBox.Show("Conectou!");

            }
            else {
                MessageBox.Show("Não conectou!");
            }
            Application.Run(new FrmLogin());

        }
        
        private static bool ConectarSAP() {

            oCompany = new SAPbobsCOM.Company {
                DbServerType = BoDataServerTypes.dst_MSSQL2017,
                Server = "DESKTOP-MH559GP",
                SLDServer = "DESKTOP-MH559GP:40000",
                CompanyDB = "Tech",
                UserName = "manager", // SAP
                Password = "0321",
                DbUserName = "sa", //SQL
                DbPassword = "0321"
            };


            if (oCompany.Connect() == 0) {
                return true;
            }
            else return false;
        }

    }
}
