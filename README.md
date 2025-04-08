# Cadastro de Fornecedores - Windows Forms

##  Descrição do Projeto

Este projeto é um software desktop desenvolvido com C# e .NET Framework, utilizando a interface gráfica Windows Forms. O sistema tem como objetivo facilitar o cadastro de fornecedores, oferecendo duas formas de preenchim

- **Consulta automática via CNPJ: ao digitar um CNPJ válido, o sistema consulta uma API pública (BrasilAPI) e preenche automaticamente os campos do formulário.
- **Preenchimento manual: caso o usuário prefira, pode preencher todos os campos manualmente.

Além disso, o sistema permite:
- **Listar** fornecedores cadastrados;
- **Editar ou excluir** registros;
- **Armazenar os dados** em banco de dados MariaDB;
- **Tratar erros** de forma amigável e registrar logs das operações realizadas.

O projeto também aplica dois **padrões de projeto (Design Patterns)**: Singleton e Abstract Factory.

---

## Instruções para Configuração e Execução

### Pré-requisitos

Antes de iniciar, certifique-se de que os seguintes itens estejam instalados no seu computador:

- Visual Studio (com suporte a projetos Windows Forms e .NET Framework)
- Banco de dados MariaDB ou MySQL
- Git (opcional, para clonar o repositório)
- Biblioteca NuGet `MySql.Data`

---

### 1. Clone o Repositório

Se desejar baixar o projeto via Git:

git clone https://github.com/cadusamparo/CadastroFornecedoresWinForms.git

---

### 2. Configure o Banco de Dados
Crie um banco de dados chamado fornecedores_db (ou o nome que você preferir) e execute o seguinte script SQL para criar a tabela:
CREATE TABLE fornecedores (
  id INT PRIMARY KEY AUTO_INCREMENT,
  razao_social VARCHAR(255) NOT NULL,
  cnpj VARCHAR(20) NOT NULL,
  logradouro VARCHAR(255),
  numero VARCHAR(20),
  bairro VARCHAR(100),
  cidade VARCHAR(100),
  estado VARCHAR(2),
  cep VARCHAR(20),
  telefone VARCHAR(20),
  email VARCHAR(100),
  responsavel VARCHAR(100)
);

---

### 3. Ajuste a Conexão com o Banco
Abra o arquivo Database.cs e localize a string de conexão. Ajuste com as informações corretas do seu banco local:
"server=localhost;database=fornecedores_db;uid=root;pwd=sua_senha;"

### 4. Execute a Aplicação
Abra o projeto no Visual Studio, restaure os pacotes NuGet se necessário e pressione F5 para executar. A interface principal será carregada, permitindo o cadastro e gerenciamento dos fornecedores.

---

## Padrões de Projeto Utilizados

### Singleton
Usado para a conexão com o banco de dados. Garante que apenas uma instância de conexão seja criada e utilizada em toda a aplicação.

Motivo: Reduz o consumo de recursos e centraliza a configuração da conexão, tornando a manutenção mais simples.

### Abstract Factory

Utilizado na criação de serviços de consulta de CNPJ. Isso facilita a troca futura da API (ex: usar outro provedor além da BrasilAPI) sem alterar o restante do código.

Motivo: Garante que a lógica da aplicação não fique presa a uma API específica, permitindo manutenção e testes mais fáceis.

---

## Funcionalidades do Sistema
- Cadastro de fornecedores com dados obrigatórios validados
- Consulta de dados via CNPJ com preenchimento automático
- Preenchimento manual alternativo
- Listagem completa dos fornecedores cadastrados
- Edição e exclusão de fornecedores
- Armazenamento em banco de dados
- Tratamento de erros (ex: falha na API ou banco)
- Logs de operações salvos automaticament

---

##Tecnologias e Ferramentas Utilizadas
- Linguagem: C#
- Framework: .NET Framework
- Interface Gráfica: Windows Forms
- Banco de Dados: MariaDB
- API Pública: BrasilAPI
- Controle de Versão: Git
- Padrões de Projeto: Singleton e Abstract Factory

---

## Requisitos Necessários para Execução
- Visual Studio instalado
- .NET Framework compatível com Windows Forms
- MariaDB ou MySQL em funcionamento
- Conexão com a internet (para consultar a API BrasilAPI)

---

## Vídeo Explicativo
Um vídeo explicativo com a apresentação do sistema, demonstração das funcionalidades e justificativa técnica dos padrões utilizados está disponível no link abaixo:

---

## Estrutura do Projeto
CadastroFornecedoresWinForms/
Forms/                # Telas do sistema
Services/             # Serviços de consulta e API
    AbstractFactory/
    └️ BrasilApiService.cs
Database/             # Conexão com o banco (Singleton)
    └️ Database.cs
Utils/                # Utilitários e logs
    └️ Logger.cs

##  Autor

Desenvolvido por Carlos (cadusamparo) como parte de um projeto acadêmico para simular um sistema profissional de cadastro de fornecedores.
