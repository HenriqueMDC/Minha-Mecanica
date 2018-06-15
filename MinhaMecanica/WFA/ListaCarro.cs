using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA
{
    public partial class ListaCarro : Form
    {
        string nomeAntigo = string.Empty;
        List<Carro> carros = new List<Carro>();

        public ListaCarro()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Carro carro = new Carro();
                {
                    carro.Nome = txtNome.Text;
                    carro.Marca = txtMarca.Text;
                    carro.AnoFabricacao = Convert.ToInt16(txtAno.Text);
                    carro.Valor = Convert.ToDecimal(txtValor.Text);
                }
                if (nomeAntigo == "")
                {
                    carros.Add(carro);
                    AdicionarCarroATabela(carro);
                }

                else
                {
                    /*for (int i = 0; i < carros.Count(); i++)
                    {
                        Carro carroAux = carros[i];
                        if (carroAux.Nome == nomeAntigo)
                        {
                            carros[i] = carro;
                        }
                    }*/

                    int linha = carros.FindIndex(x => x.Nome == nomeAntigo);
                    carros[linha] = carro;
                    EditarCarroNaTebela(carro, linha);
                    MessageBox.Show("Alterado com sucesso");
                    nomeAntigo = string.Empty;
                }
                tabControl1.SelectedIndex = 0;
                LimparCampos();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditarCarroNaTebela(Carro carro, int linha)
        {
            dataGridView1.Rows[linha].Cells[0].Value = carro.Nome;
            dataGridView1.Rows[linha].Cells[1].Value = carro.Marca;
            dataGridView1.Rows[linha].Cells[2].Value = carro.AnoFabricacao;
            dataGridView1.Rows[linha].Cells[3].Value = carro.Valor;
        }

        private void AdicionarCarroATabela(Carro carro)
        {
            dataGridView1.Rows.Add(new Object[]{
                carro.Nome, carro.Marca, carro.AnoFabricacao, carro.Valor
            });
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtMarca.Text = "";
            txtAno.Text = "";
            txtValor.Text = "";
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Cadastre um carro");
                tabControl1.SelectedIndex = 1;
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma linha");
            }

            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString();
            for (int i = 0; i < carros.Count(); i++)
            {
                Carro carro = carros[i];
                if (carro.Nome == nome)
                {
                    carros.RemoveAt(i);
                }
            }

            //carros.Remove(carros.Find(x=> x.Nome == nome));

            dataGridView1.Rows.RemoveAt(linhaSelecionada);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Cadastre um carro");
                tabControl1.SelectedIndex = 1;
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecione uma linha");
            }

            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString();

            foreach (Carro carro in carros)
            {
                txtNome.Text = carro.Nome;
                txtMarca.Text = carro.Marca;
                txtAno.Text = Convert.ToString(carro.AnoFabricacao);
                txtValor.Text = Convert.ToString(carro.Valor);
                tabControl1.SelectedIndex = 1;
                nomeAntigo = carro.Nome;
                break;
            }
        }
    }
}
