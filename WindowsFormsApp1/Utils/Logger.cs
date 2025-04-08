using System;
using System.IO;
using System.Windows.Forms;

namespace SeuProjeto.Utils
{
    public static class Logger
    {
        private static readonly string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");

        public static void Registrar(string acao)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - {acao}");
                }
            }
            catch (Exception ex)
            {
                // Se quiser, pode exibir erro de log ou ignorar
                MessageBox.Show("Erro ao registrar log: " + ex.Message);
            }
        }
    }
}
