﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Responsavel { get; set; }
        public string DataCadastro { get; set; }
    }
}
