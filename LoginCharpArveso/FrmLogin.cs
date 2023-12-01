using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SAPbobsCOM;
using static System.Net.Mime.MediaTypeNames;

namespace LoginCharpArveso {
    public partial class FrmLogin : Form {
        //Referencia da conexao

        public Recordset Qry;

        public FrmLogin() {
            InitializeComponent();
            txtUsuario.Select();
             
        }

        //verificação de textbox's vazios
        void verificar() {
            if (txtUsuario.Text == "" && txtPassword.Text == "") {
                MessageBox.Show("Preencha os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Select();

            }

        }
        //Botao Entrar

        private void btnEntrar_Click_1(object sender, EventArgs e) {
          
            try {
                string query = "select * from OHEM where firstName = '" + txtUsuario.Text + "'and pager = '" + txtPassword.Text + "'";
                Qry = Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                Qry.DoQuery(query);
                MessageBox.Show("foi");
            }
            catch (Exception erro) {
                MessageBox.Show("Usuario ou password incorretos" + erro);
                txtUsuario.Text = ""; //Limpa as textbox dps de serem verificadas
                txtPassword.Text = "";
                txtUsuario.Select(); //cursor ira sinalizar a primeira textbox
            }
   
        }


        //Botão Sair
        private void btnSair_Click_1(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }
        //cadastrar:
        private void btnCadastrar_Click(object sender, EventArgs e) {
            var a = Program.oCompany.UserName;
            SAPbobsCOM.EmployeesInfo oEmployee = (SAPbobsCOM.EmployeesInfo)Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
            string[] nomeseparado = new string[2]; //declarando array de 2 strings
           
            nomeseparado = txtUsuario.Text.Split('.'); // separando por.

            oEmployee.FirstName = nomeseparado[0];
            oEmployee.LastName = nomeseparado[1];
            oEmployee.Pager = txtPassword.Text;

            if (oEmployee.Add() != 0) {
                MessageBox.Show("Erro: " + Program.oCompany.GetLastErrorDescription() );
               
            }
            else {
                MessageBox.Show("Sucesso!");
                
            }


        }

    }

}
