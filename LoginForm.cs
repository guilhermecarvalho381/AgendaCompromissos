using AgendaCompromissos.Util;
using AgendaCompromissos.DAL; 
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
using AgendaCompromissos.Forms;

namespace AgendaCompromissos
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;

            using (SqlConnection conn = Banco.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome FROM Usuarios WHERE Usuario = @usuario AND Senha = @senha", conn);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@senha", senha);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Sessao.UsuarioId = reader.GetInt32(0);
                    Sessao.NomeUsuario = reader.GetString(1);

                    MessageBox.Show("Login bem-sucedido!");
                    new MainForm().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos.");
                }
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnCadUsu_Click(object sender, EventArgs e)
        {
            var cadastro = new CadastroForm();
            cadastro.ShowDialog();
        }
    }

}
