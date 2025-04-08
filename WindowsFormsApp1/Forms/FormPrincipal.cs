using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;

namespace WindowsFormsApp1
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            EstilizarFormulario();
        }

        private void btnCadastroFornecedores_Click(object sender, EventArgs e)
        {
            // Abre o formulário de fornecedores
            FormFornecedor formFornecedor = new FormFornecedor();
            formFornecedor.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            // Fecha a aplicação
            Application.Exit();
        }

        private void EstilizarFormulario()
        {
            this.Text = "Sistema de Cadastro de Fornecedores";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Size = new Size(450, 350); // Tamanho fixo da janela

            // Painel superior
            Panel panelTop = new Panel();
            panelTop.Height = 60;
            panelTop.Dock = DockStyle.Top;
            panelTop.BackColor = Color.FromArgb(44, 62, 80);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Sistema de Fornecedores";
            lblTitulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.AutoSize = false;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Dock = DockStyle.Fill;

            panelTop.Controls.Add(lblTitulo);
            this.Controls.Add(panelTop);

            // Dimensões dos botões
            int buttonWidth = 220;
            int buttonHeight = 45;

            // Botão Gerenciar Fornecedores
            Button btnCadastroFornecedores = new Button();
            btnCadastroFornecedores.Text = "Gerenciar Fornecedores";
            btnCadastroFornecedores.Size = new Size(buttonWidth, buttonHeight);
            btnCadastroFornecedores.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCadastroFornecedores.FlatStyle = FlatStyle.Flat;
            btnCadastroFornecedores.BackColor = Color.FromArgb(52, 152, 219);
            btnCadastroFornecedores.ForeColor = Color.White;
            btnCadastroFornecedores.Click += btnCadastroFornecedores_Click;

            // Botão Sair
            Button btnSair = new Button();
            btnSair.Text = "Sair";
            btnSair.Size = new Size(buttonWidth, buttonHeight);
            btnSair.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.BackColor = Color.FromArgb(231, 76, 60);
            btnSair.ForeColor = Color.White;
            btnSair.Click += btnSair_Click;

            // Posicionamento centralizado
            int centerX = (this.ClientSize.Width - buttonWidth) / 2;
            int centerY = (this.ClientSize.Height - panelTop.Height - (buttonHeight * 2 + 20)) / 2 + panelTop.Height;

            btnCadastroFornecedores.Location = new Point(centerX, centerY);
            btnSair.Location = new Point(centerX, centerY + buttonHeight + 20);

            this.Controls.Add(btnCadastroFornecedores);
            this.Controls.Add(btnSair);
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
