using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.Database
{
    public sealed class Database
    {
        private static Database instance = null;
        private static readonly object padlock = new object();

        private string connectionString = "server=localhost;database=cadastro_fornecedores;user=root;password=123;";

        private Database() { }

        public static Database Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Database();
                    }
                    return instance;
                }
            }
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
