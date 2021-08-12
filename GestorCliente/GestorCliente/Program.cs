using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GestorCliente
{
  
    class Program
    {
        [System.Serializable]
        struct Cliente { //struct para definição de dados do clientes
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>(); //criação de lista de clientes

        enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}

        static void Main(string[] args)
        {
            Carregar();
            bool Sair = false;
            while (!Sair)
            {
                Console.WriteLine("Sistema de clientes");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Sair");

                int intOp = int.Parse(Console.ReadLine());

                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        Sair = true;
                        break;
                }
                Console.Clear();
            }
            
        }

        static void Adicionar() {
            Cliente cliente = new Cliente(); //chamando lista de clientes

            Console.WriteLine("Cadastro do cliente");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente); //Linha para adicionar cliente na lista de clientes
            Salvar();

            Console.WriteLine("Cadastro concluido, aperte enter para sair");
            Console.ReadLine();

        }

        static void Listagem() {

            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes: ");
                int i = 0;
                foreach (Cliente clientes in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {clientes.nome}");
                    Console.WriteLine($"Email: {clientes.email}");
                    Console.WriteLine($"CPF: {clientes.cpf}");
                    Console.WriteLine("-----------------------------------");
                    i++;                    
                }
                
            } else {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
            Console.WriteLine("Aperte enter para sair");
            Console.ReadLine();
        }

        static void Remover() {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que quer remover");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id); //Linha para remover pelo id
                Salvar();
            }
            else {
                Console.WriteLine("ID digitado é invalido");
                Console.ReadLine();
            }
        }


        //Função para salvar dados em arquivos
        static void Salvar() {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        //Função para salvar dados em arquivos
        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            try
            {
              
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if (clientes == null) {
                    clientes = new List<Cliente>();
                }

                
            }
            catch (Exception e)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }
    }
}
