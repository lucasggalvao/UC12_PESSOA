using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UC12_PESSOA
{
    public partial class Form1 : Form
    {
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;

        public Form1()
        {
            InitializeComponent();
            servidor = "Server=localhost;Database=pessoa_endereco;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();

            atualizarLISTA();
        }

        private void atualizarLISTA()
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT nome, cpf, logradouro, estado FROM tbl_pessoa INNER JOIN tbl_endereco ON(tbl_pessoa.fk_endereco = tbl_endereco.id);";
                
                MySqlDataAdapter adaptadorLISTA = new MySqlDataAdapter(comando);
                DataTable tabelaLISTA = new DataTable();
                adaptadorLISTA.Fill(tabelaLISTA);

                dataGridViewLista.DataSource = tabelaLISTA;
                dataGridViewLista.Columns["nome"].HeaderText = "Nome";
                dataGridViewLista.Columns["cpf"].HeaderText = "CPF";
                dataGridViewLista.Columns["logradouro"].HeaderText = "Logradouro";
                dataGridViewLista.Columns["estado"].HeaderText = "Estado";

            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void buttoncadastrar_Click(object sender, EventArgs e)
        {
            string ultimoID = "";
            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_endereco(logradouro,bairro, cidade, estado, uf) VALUES ('" + textBoxLogradouro.Text + "','" + textBoxBairro.Text + "', '" + textBoxCidade.Text + "', '" + comboBoxEstado.Text + "', '" + comboBoxUF.Text + "');";
                comando.ExecuteNonQuery();
                MessageBox.Show("Endereço cadastrado com sucesso!");

            }
            catch (Exception erro_mysql)
            {
                //MessageBox.Show(erro.Message);
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }

            try
            {
                conexao.Open();
                comando.CommandText = "SELECT MAX(id) FROM tbl_endereco;";
                MySqlDataReader readerID = comando.ExecuteReader();
             
                if (readerID.Read())
                {
                    ultimoID = readerID.GetString(0);
                    MessageBox.Show(ultimoID);
                }
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }

            string genero = "";
            if(radioButtonMasc.Checked)
            {
                genero = "Masculino";
            }
            if (radioButtonFem.Checked)
            {
                genero = "Feminino";
            }
            if (Outro.Checked)
            {
                genero = "Outros";
            }

            try
            {
                conexao.Open();
                comando.CommandText = "INSERT INTO tbl_pessoa(nome, sobrenome, nome_social, rg, cpf, data_nasc, etnia, genero, fk_endereco) VALUES ('" + textBoxNome.Text + "','" + textBoxSobrenome.Text + "', '" + textBoxNomeSocial.Text + "', '" + textBoxRG.Text + "', '" + textBoxCPF.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', '" + comboBoxEtnia.Text + "', '" + genero + "', '" + ultimoID + "');";
                comando.ExecuteNonQuery();
                MessageBox.Show("Endereço cadastrado com sucesso!");

            }
            catch (Exception erro_mysql)
            {
                //MessageBox.Show(erro.Message);
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
            atualizarLISTA();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
