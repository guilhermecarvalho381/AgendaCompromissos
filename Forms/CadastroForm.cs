using AgendaCompromissos.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaCompromissos.Forms
{
    public partial class CadastroForm: Form
    {
        public CadastroForm()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string confirmar = txtConfSenha.Text.Trim();

            if (nome == "" || usuario == "" || senha == "")
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }

            if (senha != confirmar)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            using (SqlConnection conn = Banco.GetConnection())
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@usuario", usuario);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Usuário já existe.");
                    return;
                }

                string insertQuery = "INSERT INTO Usuarios (Nome, Usuario, Senha) VALUES (@nome, @usuario, @senha)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@senha", senha);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Usuário cadastrado com sucesso!");
                this.Close();
            }
        }
    }
}
