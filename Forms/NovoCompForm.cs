using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AgendaCompromissos.Util;
using AgendaCompromissos.DAL;
using System.Data.SqlClient;

namespace AgendaCompromissos.Forms
{
    public partial class NovoCompForm: Form
    {
        private int compromissoId = 0;

        public NovoCompForm()
        {
            InitializeComponent();
        }

        public NovoCompForm(int compromissoId) : this()
        {
            this.compromissoId = compromissoId;
            CarregarCompromisso();
        }

        //Carregar função SQL Server
        private void CarregarCompromisso()
        {
            if (compromissoId == 0) return;

            using (SqlConnection conn = Banco.GetConnection())
            {
                conn.Open();
                string query = "SELECT Titulo, Descricao, DataCompromisso FROM Compromissos WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", compromissoId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtTitulo.Text = reader.GetString(0);
                    txtDescricao.Text = reader.GetString(1);
                    dtpDataComp.Value = reader.GetDateTime(2);
                }
            }
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text;
            string descricao = txtDescricao.Text;
            DateTime dataCompromisso = dtpDataComp.Value;

            using (SqlConnection conn = Banco.GetConnection())
            {
                conn.Open();

                string query;
                SqlCommand cmd;

                if (compromissoId == 0) // Inserir
                {
                    query = "INSERT INTO Compromissos (UsuarioId, Titulo, Descricao, DataCompromisso) " +
                            "VALUES (@usuarioId, @titulo, @descricao, @dataCompromisso)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuarioId", Sessao.UsuarioId);
                }
                else // Editar
                {
                    query = "UPDATE Compromissos SET Titulo = @titulo, Descricao = @descricao, DataCompromisso = @dataCompromisso " +
                            "WHERE Id = @id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", compromissoId);
                }

                cmd.Parameters.AddWithValue("@titulo", titulo);
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.Parameters.AddWithValue("@dataCompromisso", dataCompromisso);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Compromisso salvo com sucesso!");
            this.Close();
        
        }
    }
}
