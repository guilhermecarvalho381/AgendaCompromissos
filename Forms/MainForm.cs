using AgendaCompromissos.DAL;
using AgendaCompromissos.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaCompromissos.Forms
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            CarregarCompromissos();
        }

        private void CarregarCompromissos()
        {
            using (SqlConnection conn = Banco.GetConnection())
            {
                conn.Open();
                string query = "SELECT Id, Titulo, Descricao, DataCompromisso FROM Compromissos WHERE UsuarioId = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", Sessao.UsuarioId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);

                dgvCompromissos.DataSource = tabela;

                // Esconde a coluna ID
                dgvCompromissos.Columns["Id"].Visible = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Mainf(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

            new NovoCompForm().ShowDialog();
            CarregarCompromissos(); // Atualiza a lista de compromissos

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCompromissos.SelectedRows.Count == 1)
            {
                int compromissoId = Convert.ToInt32(dgvCompromissos.SelectedRows[0].Cells[0].Value);
                new NovoCompForm(compromissoId).ShowDialog();
                CarregarCompromissos(); // Atualiza a lista de compromissos
            }
            else
            {
                MessageBox.Show("Selecione um compromisso para editar.");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvCompromissos.SelectedRows.Count == 1)
            {
                var resultado = MessageBox.Show("Tem certeza que deseja excluir este compromisso?",
                                                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    int compromissoId = Convert.ToInt32(dgvCompromissos.SelectedRows[0].Cells[0].Value);

                    using (SqlConnection conn = Banco.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Compromissos WHERE Id = @id AND UsuarioId = @usuarioId";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", compromissoId);
                        cmd.Parameters.AddWithValue("@usuarioId", Sessao.UsuarioId); // Garantir que o usuário logado exclua apenas seus compromissos

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Compromisso excluído com sucesso!");
                    CarregarCompromissos(); // Atualiza a lista de compromissos
                }
            }
            else
            {
                MessageBox.Show("Selecione um compromisso para excluir.");
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Tem certeza que deseja sair?", "Confirmar saída",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Fecha o MainForm
                this.Hide();

                // Abre o Formulário de Login
                var loginForm = new LoginForm();
                loginForm.ShowDialog();

                
            }
    }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Pergunta ao usuário se ele realmente quer fechar
            var resultado = MessageBox.Show("Tem certeza que deseja sair?", "Confirmar saída",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                e.Cancel = true; // Cancela o fechamento da janela
            }
            else
            {
                Application.Exit(); // Encerra o aplicativo
            }
        }
    }
}
