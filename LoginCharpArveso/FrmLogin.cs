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
namespace LoginCharpArveso
{
    public partial class FrmLogin : Form
    {
        //Referencia da conexao
        SqlConnection Conexao = new SqlConnection(@"Data Source=DESKTOP-MH559GP;Initial Catalog=Logincharp;Integrated Security=True");
        public FrmLogin()
        {
            InitializeComponent();
            txtUsuario.Select();
        }

        //verificação de textbox's vazios
        void verificar()
        {
            if (txtUsuario.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Preencha os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Select();
            }
        }

        //Botao Entrar

        private void btnEntrar_Click_1(object sender, EventArgs e)
        {
            Conexao.Open(); //Abrir a conexão
            verificar();
            string query = "SELECT * FROM Usuario WHERE Username='" + txtUsuario.Text + "' AND Password = '" + txtPassword.Text + "'";
            SqlDataAdapter dp = new SqlDataAdapter(query, Conexao);
            DataTable dt = new DataTable();
            dp.Fill(dt);

            try
            {
                if (dt.Rows.Count == 1)
                {
                    FrmPrincipal principal = new FrmPrincipal();
                    this.Hide();
                    principal.Show();
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Usuario ou password incorretos" + erro);
                txtUsuario.Text = ""; //Limpa as textbox dps de serem verificadas
                txtPassword.Text = "";
                txtUsuario.Select(); //cursor ira sinalizar a primeira textbox
            }
            Conexao.Close(); //fechar a conexao
        }
     

        //Botão Sair
        private void btnSair_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Conexao.Open();
            try
            {
                string query = "INSERT INTO Usuario (Username, Password) VALUES ('" + txtUsuario.Text + "', '" + txtPassword.Text + "')"; //Usuario WHERE Username='" + txtUsuario.Text + "' AND Password = "
                SqlDataAdapter dp = new SqlDataAdapter(query, Conexao);
                DataTable dt = new DataTable(); //chama a tabela e add linha
                dp.Fill(dt); //executa query
            }
            catch (Exception erro)
            {
                MessageBox.Show("Falha ao inserir" + erro);
            }
            Conexao.Close();
        }
    }

}