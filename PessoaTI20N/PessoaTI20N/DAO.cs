using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //importar o MySql

namespace PessoaTI20N
{
    class DAO
    {
        public MySqlConnection conexao;
        public long[] cpf;
        public string[] nome;
        public string[] telefone;
        public string[] endereco;
        public int i;
        public int contador;

        public DAO()
        { 
            conexao = new MySqlConnection("Server=localhost;Database=empresaTI20N;Uid=root;password=");
            try
            {
                conexao.Open();//abrir conexao
                MessageBox.Show("Conectado");

            }
            catch(Exception ex) 
            {
                MessageBox.Show("Algo deu errado\n\n" + ex);
           
            } 
        }//fim do construtor

        public string Inserir(long cpf, string nome, string telefone, string endereco)
        {
            string inserir = $"Insert into pessoa(cpf, nome, telefone, endereco) values" + $"('{cpf}',{nome}','{telefone}'{endereco}')";        
                
              MySqlCommand sql = new MySqlCommand(inserir, conexao);
              string resultado = sql.ExecuteNonQuery() + " Executado";
              return resultado;
                
                
        }//fim do metodo

        public void PreencherVetor()
        {
            string query = "select * from pessoa";

            //instacionar
            this.cpf = new long[100];
            this.nome = new string[100];
            this.telefone = new string[100];
            this.endereco = new string[100];

            //fazer o commando de seleção do banco 
            MySqlCommand sql = new MySqlCommand(@query, conexao);
            //leitor do banco 
            MySqlDataReader leitura = sql.ExecuteReader();

            i = 0;
            contador = 0;
            while (leitura.Read())
            {
                cpf[i] = Convert.ToInt32(leitura["cpf"]);
                nome[i] = leitura["nome"] + "";
                telefone[i] = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                i++;
                contador++;

            }//fim do while
        }

            //encerro o metodo para retornar o contador 

            public int QuantidaDados()
            {
                return contador;
            }//fim do quantidade de dados 

            public string Atualizar(long cpf, string nomeTabela, string campo, string dado)
            {
                string query = $"update {nomeTabela} set {campo} = '{dado}' where cpf = '{cpf}'";
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = sql.ExecuteNonQuery() + "Atualizado!";
                return resultado;
            }//Fim do Método

            public string Excluir(long cpf, string nomeTabela)
            {
                string query = $"delete from {nomeTabela} where cpf = '{cpf}'";
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = sql.ExecuteNonQuery() + "Excluido!";
                return resultado;

            }//fim do excluir 




          

         
    }//fim da classe 
}//fim do projeto 
