using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SeuProjeto.Utils;
using WindowsFormsApp1.Database;

namespace WindowsFormsApp1.Forms
{
    public partial class FormFornecedor : Form
    {
        public FormFornecedor()
        {
            InitializeComponent();
            EstilizarFormulario(); // ← chama aqui

            dgvFornecedores.CellClick += dgvFornecedores_CellClick;
            btnEditar.Click += btnEditar_Click;
            btnExcluir.Click += btnExcluir_Click;
            btnLimpar.Click += btnLimpar_Click;
            btnConsultarCNPJ.Click += btnConsultarCNPJ_Click; // ← ADICIONA ESTA LINHA
            ListarFornecedores();
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string razaoSocial = txtRazaoSocial.Text.Trim();
            string cnpj = txtCNPJ.Text.Trim();
            string logradouro = txtLogradouro.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string estado = txtEstado.Text.Trim();
            string cep = txtCEP.Text.Trim();
            string telefone = txtTelefone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string responsavel = txtResponsavel.Text.Trim();

            if (string.IsNullOrEmpty(razaoSocial) || string.IsNullOrEmpty(cnpj))
            {
                MessageBox.Show("Razão Social e CNPJ são obrigatórios.");
                return;
            }

            try
            {
                var connection = WindowsFormsApp1.Database.Database.Instance.GetConnection();
                connection.Open();

                string query = @"INSERT INTO fornecedores 
                        (razao_social, cnpj, logradouro, numero, bairro, cidade, estado, cep, telefone, email, responsavel) 
                        VALUES 
                        (@RazaoSocial, @CNPJ, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @Responsavel)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RazaoSocial", razaoSocial);
                    cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                    cmd.Parameters.AddWithValue("@Logradouro", logradouro);
                    cmd.Parameters.AddWithValue("@Numero", numero);
                    cmd.Parameters.AddWithValue("@Bairro", bairro);
                    cmd.Parameters.AddWithValue("@Cidade", cidade);
                    cmd.Parameters.AddWithValue("@Estado", estado);
                    cmd.Parameters.AddWithValue("@CEP", cep);
                    cmd.Parameters.AddWithValue("@Telefone", telefone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Responsavel", responsavel);

                    cmd.ExecuteNonQuery();
                }

                connection.Close();

                MessageBox.Show("Fornecedor cadastrado com sucesso!");
                Logger.Registrar($"Fornecedor cadastrado: {razaoSocial} - CNPJ: {cnpj}");
                ListarFornecedores();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar fornecedor: " + ex.Message);
            }
        }

        private void ListarFornecedores()
        {
            try
            {
                var connection = WindowsFormsApp1.Database.Database.Instance.GetConnection();
                connection.Open();

                string query = "SELECT * FROM fornecedores";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvFornecedores.DataSource = dt;

                // Oculta a coluna ID da visualização
                if (dgvFornecedores.Columns.Contains("id"))
                {
                    dgvFornecedores.Columns["id"].Visible = false;
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar fornecedores: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtRazaoSocial.Text = "";
            txtCNPJ.Text = "";
            txtLogradouro.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEstado.Text = "";
            txtCEP.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtResponsavel.Text = "";
        }

        private void dgvFornecedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFornecedores.Rows[e.RowIndex];

                txtRazaoSocial.Text = row.Cells["razao_social"].Value.ToString();
                txtCNPJ.Text = row.Cells["cnpj"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtTelefone.Text = row.Cells["telefone"].Value.ToString();
                txtLogradouro.Text = row.Cells["logradouro"].Value.ToString();
                txtNumero.Text = row.Cells["numero"].Value.ToString();
                txtBairro.Text = row.Cells["bairro"].Value.ToString();
                txtCidade.Text = row.Cells["cidade"].Value.ToString();
                txtEstado.Text = row.Cells["estado"].Value.ToString();
                txtCEP.Text = row.Cells["cep"].Value.ToString();
                txtResponsavel.Text = row.Cells["responsavel"].Value.ToString();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvFornecedores.CurrentRow == null)
            {
                MessageBox.Show("Selecione um fornecedor na tabela para editar.");
                return;
            }

            // Pega o ID da linha selecionada
            if (!int.TryParse(dgvFornecedores.CurrentRow.Cells["id"].Value?.ToString(), out int id))
            {
                MessageBox.Show("ID do fornecedor inválido.");
                return;
            }

            string query = @"UPDATE fornecedores SET 
                    razao_social = @razao_social, 
                    cnpj = @cnpj, 
                    logradouro = @logradouro, 
                    numero = @numero, 
                    bairro = @bairro, 
                    cidade = @cidade, 
                    estado = @estado, 
                    cep = @cep, 
                    telefone = @telefone, 
                    email = @email, 
                    responsavel = @responsavel
                    WHERE id = @id";

            try
            {
                var connection = WindowsFormsApp1.Database.Database.Instance.GetConnection();
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@razao_social", txtRazaoSocial.Text.Trim());
                    cmd.Parameters.AddWithValue("@cnpj", txtCNPJ.Text.Trim());
                    cmd.Parameters.AddWithValue("@logradouro", txtLogradouro.Text.Trim());
                    cmd.Parameters.AddWithValue("@numero", txtNumero.Text.Trim());
                    cmd.Parameters.AddWithValue("@bairro", txtBairro.Text.Trim());
                    cmd.Parameters.AddWithValue("@cidade", txtCidade.Text.Trim());
                    cmd.Parameters.AddWithValue("@estado", txtEstado.Text.Trim());
                    cmd.Parameters.AddWithValue("@cep", txtCEP.Text.Trim());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@responsavel", txtResponsavel.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                connection.Close();
                MessageBox.Show("Fornecedor atualizado com sucesso!");
                Logger.Registrar($"Fornecedor atualizado: ID {id} - {txtRazaoSocial.Text}");
                ListarFornecedores();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar fornecedor: " + ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvFornecedores.CurrentRow == null)
            {
                MessageBox.Show("Selecione um fornecedor na tabela para excluir.");
                return;
            }

            // Confirma com o usuário
            DialogResult confirm = MessageBox.Show("Tem certeza que deseja excluir este fornecedor?",
                                                   "Confirmação",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            if (!int.TryParse(dgvFornecedores.CurrentRow.Cells["id"].Value?.ToString(), out int id))
            {
                MessageBox.Show("ID do fornecedor inválido.");
                return;
            }

            try
            {
                var connection = WindowsFormsApp1.Database.Database.Instance.GetConnection();
                connection.Open();

                string query = "DELETE FROM fornecedores WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
                MessageBox.Show("Fornecedor excluído com sucesso!");
                Logger.Registrar($"Fornecedor excluído: ID {id}");
                ListarFornecedores();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir fornecedor: " + ex.Message);
            }
        }

        private async void btnConsultarCNPJ_Click(object sender, EventArgs e)
        {
            string cnpj = txtCNPJ.Text.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            if (string.IsNullOrEmpty(cnpj))
            {
                MessageBox.Show("Informe o CNPJ para consulta.");
                return;
            }

            var service = new WindowsFormsApp1.Services.BrasilApiService();
            var fornecedor = await service.ConsultarCnpjAsync(cnpj);

            if (fornecedor != null)
            {
                txtRazaoSocial.Text = fornecedor.nome;
                txtLogradouro.Text = fornecedor.logradouro;
                txtNumero.Text = fornecedor.numero;
                txtBairro.Text = fornecedor.bairro;
                txtCidade.Text = fornecedor.municipio;
                txtEstado.Text = fornecedor.uf;
                txtCEP.Text = fornecedor.cep;

                Logger.Registrar($"Consulta de CNPJ: {cnpj} - Razão Social: {fornecedor.nome}");
            }
            else
            {
                MessageBox.Show("CNPJ não encontrado ou erro na consulta.");
                Logger.Registrar($"Falha na consulta de CNPJ: {cnpj}");
            }
        }

        private void EstilizarFormulario()
        {
            this.Text = "Cadastro de Fornecedores";
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            Font fonteLabel = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fonteCampo = new Font("Segoe UI", 10);
            Font fonteBotao = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (Control control in this.Controls)
            {
                if (control is Label lbl)
                {
                    lbl.Font = fonteLabel;
                }
                else if (control is TextBox txt)
                {
                    txt.Font = fonteCampo;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is Button btn)
                {
                    btn.Font = fonteBotao;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Height = 35;

                    if (btn.Text.Contains("Cadastrar"))
                        btn.BackColor = Color.FromArgb(46, 204, 113);
                    else if (btn.Text.Contains("Editar"))
                        btn.BackColor = Color.FromArgb(52, 152, 219);
                    else if (btn.Text.Contains("Excluir"))
                        btn.BackColor = Color.FromArgb(231, 76, 60);
                    else if (btn.Text.Contains("Limpar"))
                        btn.BackColor = Color.FromArgb(241, 196, 15);
                    else if (btn.Text.Contains("Consultar"))
                        btn.BackColor = Color.FromArgb(155, 89, 182);

                    btn.ForeColor = Color.White;
                }
                else if (control is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.DefaultCellStyle.Font = fonteCampo;
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgv.RowHeadersVisible = false;
                }
            }
        }

        // Eventos extras (podem ser excluídos se não forem usados)
        private void lblTitulo_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label8_Click_1(object sender, EventArgs e) { }
        private void txtCidade_TextChanged(object sender, EventArgs e) { }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
