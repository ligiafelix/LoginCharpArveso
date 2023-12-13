using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LoginCharpArveso {

    public partial class FrmPrincipal : Form {
        public Recordset Qry;

        public FrmPrincipal() {
            InitializeComponent();
        }

        private void btnCarregar_Click(object sender, EventArgs e) {
            var dados = CarregarTxt();
            SalvarDadosSap(dados);
        }


        //seleciona e carrega o arquivo
        private List<string[]> CarregarTxt() {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //carregando o arquivo com o objeto OpenFileDialog 
            openFileDialog.Title = "Selecionar um Arquivo"; //titulo da caixa para carrega-lo
            openFileDialog.Filter = "Todos os Arquivos|*.*"; // tipo de arquivo p/ selecionar
            string caminhoArquivoSelecionado = ""; // string criada p/ receber o caminho "" 
            if (openFileDialog.ShowDialog() == DialogResult.OK) {

                caminhoArquivoSelecionado = openFileDialog.FileName;

                txtArquivoSelecionado.Text = Path.GetFileName(caminhoArquivoSelecionado);

            }

            //leitura do txt, salva em uma lista os itens separados por ;

            List<string[]> dados = new List<string[]>(); //lista de dados tipo um array com dados

            using (StreamReader leitor = new StreamReader(caminhoArquivoSelecionado, Encoding.Default)) {
                string linha, conteudoArquivo = "";
                while ((linha = leitor.ReadLine()) != null) {
                    string[] colunas = linha.Split(';');
                    conteudoArquivo = conteudoArquivo + linha + "\r\n";
                    dados.Add(colunas); //adicionando dados na lista
                }

            }

            return dados;

        }

        //salvando no SAP
        private void SalvarDadosSap(List<string[]> dados) {


            foreach (var dado in dados) {
                Items items = Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems); //criando objeto items 
                string query = "select itemcode from OITM where Itemcode = '" + dado[0] + "'";
                Qry = Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                Qry.DoQuery(query);

                if (!Qry.EoF) {
                    items.GetByKey(dado[0]);
                    items.ItemName = dado[1];
                    items.PriceList.SetCurrentLine(0);  //o item pode ter + de uma lista de preços essa é a primeira lista
                    items.PriceList.Price = double.Parse(dado[2]); //convertendo string p double
                    if (items.Update() != 0) {
                        MessageBox.Show("Erro ao atualizar item" + Program.oCompany.GetLastErrorDescription());
                    }
                }
                else {


                    items.ItemCode = dado[0];
                    items.ItemName = dado[1];
                    items.PriceList.SetCurrentLine(0);  //o item pode ter + de uma lista de preços essa é a primeira lista
                    items.PriceList.Price = double.Parse(dado[2]); //convertendo string p double
                    int resultado = items.Add();
                    if (resultado != 0) {
                        MessageBox.Show("Erro ao salvar no SAP" + Program.oCompany.GetLastErrorDescription());
                    }

                }
            }
            MessageBox.Show("Processo concluído com sucesso");

        }

        private void btnTerminar_Click(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }
    }
}



//ler txt carregado e salvo na lista

//cadastrar no SAP

//carregando o conteudo do txt na memoria

//percorrer por cada linha do arquivo

//atualizar itens
