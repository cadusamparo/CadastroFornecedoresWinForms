using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Database
{
    public class FornecedorDAO
    {
        private MySqlConnection connection;

        public FornecedorDAO()
        {
            connection = Database.Instance.GetConnection();
        }

        //Criar Fornecedor
        public bool AdicionarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                string query = "INSERT INTO fornecedores (razao_social, cnpj, logradouro, numero, bairro, cidade, estado, cep, telefone, email, responsavel) " +
                               "VALUES (@RazaoSocial, @CNPJ, @Logradouro, @Numero, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @Responsavel)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RazaoSocial", fornecedor.RazaoSocial);
                cmd.Parameters.AddWithValue("@CNPJ", fornecedor.CNPJ);
                cmd.Parameters.AddWithValue("@Logradouro", fornecedor.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", fornecedor.Numero);
                cmd.Parameters.AddWithValue("@Bairro", fornecedor.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", fornecedor.Cidade);
                cmd.Parameters.AddWithValue("@Estado", fornecedor.Estado);
                cmd.Parameters.AddWithValue("@CEP", fornecedor.CEP);
                cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);
                cmd.Parameters.AddWithValue("@Email", fornecedor.Email);
                cmd.Parameters.AddWithValue("@Responsavel", fornecedor.Responsavel);

                connection.Open();
                int resultado = cmd.ExecuteNonQuery();
                connection.Close();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar fornecedor: " + ex.Message);
                return false;
            }
        }

        // 🔹 Ler (Listar) Fornecedores
        public List<Fornecedor> ListarFornecedores()
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            try
            {
                string query = "SELECT * FROM fornecedores";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Fornecedor fornecedor = new Fornecedor
                    {
                        Id = reader.GetInt32("id"),
                        RazaoSocial = reader.GetString("razao_social"),
                        CNPJ = reader.GetString("cnpj"),
                        Logradouro = reader.GetString("logradouro"),
                        Numero = reader.GetString("numero"),
                        Bairro = reader.GetString("bairro"),
                        Cidade = reader.GetString("cidade"),
                        Estado = reader.GetString("estado"),
                        CEP = reader.GetString("cep"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        Responsavel = reader.GetString("responsavel"),
                        DataCadastro = reader.GetDateTime("data_cadastro").ToString("dd/MM/yyyy HH:mm")
                    };
                    lista.Add(fornecedor);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar fornecedores: " + ex.Message);
            }

            return lista;
        }

        // 🔹 Atualizar Fornecedor
        public bool AtualizarFornecedor(Fornecedor fornecedor)
        {
            try
            {
                string query = "UPDATE fornecedores SET razao_social=@RazaoSocial, cnpj=@CNPJ, logradouro=@Logradouro, " +
                               "numero=@Numero, bairro=@Bairro, cidade=@Cidade, estado=@Estado, cep=@CEP, telefone=@Telefone, " +
                               "email=@Email, responsavel=@Responsavel WHERE id=@Id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", fornecedor.Id);
                cmd.Parameters.AddWithValue("@RazaoSocial", fornecedor.RazaoSocial);
                cmd.Parameters.AddWithValue("@CNPJ", fornecedor.CNPJ);
                cmd.Parameters.AddWithValue("@Logradouro", fornecedor.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", fornecedor.Numero);
                cmd.Parameters.AddWithValue("@Bairro", fornecedor.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", fornecedor.Cidade);
                cmd.Parameters.AddWithValue("@Estado", fornecedor.Estado);
                cmd.Parameters.AddWithValue("@CEP", fornecedor.CEP);
                cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);
                cmd.Parameters.AddWithValue("@Email", fornecedor.Email);
                cmd.Parameters.AddWithValue("@Responsavel", fornecedor.Responsavel);

                connection.Open();
                int resultado = cmd.ExecuteNonQuery();
                connection.Close();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar fornecedor: " + ex.Message);
                return false;
            }
        }

        // 🔹 Excluir Fornecedor
        public bool ExcluirFornecedor(int id)
        {
            try
            {
                string query = "DELETE FROM fornecedores WHERE id=@Id";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                int resultado = cmd.ExecuteNonQuery();
                connection.Close();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao excluir fornecedor: " + ex.Message);
                return false;
            }
        }
    }
}
