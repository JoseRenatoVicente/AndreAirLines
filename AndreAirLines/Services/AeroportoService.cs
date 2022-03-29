using AndreAirLines.Data.Repository;
using AndreAirLines.File;
using AndreAirLines.Model.Entities;
using System;

namespace AndreAirLines.Services
{
    public class AeroportoService
    {
        AeroportoRepository aeroportoRepository = new AeroportoRepository();

        public void SubMenu()
        {

            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| AEROPORTO |++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Adicionar Aeroporto                          |");
            Console.WriteLine("\t\t\t\t\t|2| - Localizar Aeroporto por Nome                 |");
            Console.WriteLine("\t\t\t\t\t|3| - Localizar Aeroporto por Sigla                |");
            Console.WriteLine("\t\t\t\t\t|4| - Importar JSON                                |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                         |");
            Console.Write("\t\t\t\t\t|__________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "0":
                    break;

                case "1":
                    Console.Clear();
                    Add();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    GetAeroportoByNome();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    GetAeroportoBySigla();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    AddFileAeroporto();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("\t\t\t\t\tOpção inválida! ");
                    Console.ReadKey();
                    SubMenu();
                    break;
            }
        }

        public void BackMenu()
        {
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Aeroportos...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }

        public Aeroporto GetAeroportoBySigla()
        {
            Console.WriteLine("Digite a Silga do Aeroporto: ");
            string sigla = Console.ReadLine();
            if (string.IsNullOrEmpty(sigla))
            {
                Console.WriteLine("\n\t\t\tSigla inválida");
                GetAeroportoBySigla();
            }

            Aeroporto aeroporto = aeroportoRepository.GetAeroportoBySigla(sigla);

            if (aeroporto is null)
            {
                Console.WriteLine("Nenhum Aeroporto encontrado com essa sigla");
            }
            else
            {
                Console.WriteLine(aeroporto.ToString());
            }

            return aeroporto;
        }

        public void GetAeroportoByNome()
        {
            Console.WriteLine("Digite o Nome do Aeroporto: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("\n\t\t\tNome inválido");
                GetAeroportoByNome();
            }

            aeroportoRepository.GetAeroportoByNome(nome).ForEach(c => Console.WriteLine(c.ToString()));
        }


        public void AddFileAeroporto()
        {
            Console.WriteLine("Digite o caminho do arquivo Ex: C:\\aeroportos.json");
            string path = Console.ReadLine();

            var aeroportos = ReadJson.getData<Aeroporto>(path);

            if (aeroportos == null)
            {
                Console.WriteLine("Nenhum aeroporto importado");
            }

            aeroportoRepository.AddRange(aeroportos);
        }

        public void Add()
        {
            Aeroporto aeroporto = new Aeroporto();

            Console.Write("\n\t\t\tSigla: ");
            aeroporto.Sigla = Console.ReadLine();
            if (string.IsNullOrEmpty(aeroporto.Sigla))
            {
                Console.WriteLine("Sigla inválida");
                Add();
            }
            else if (aeroportoRepository.GetAeroportoBySigla(aeroporto.Sigla) is not null)
            {
                Console.WriteLine("Aeroporto já cadastrado com essa Sigla");
            }
            else
                aeroportoRepository.Add(EntradaDadosAeroporto(aeroporto));
        }

        private Aeroporto EntradaDadosAeroporto(Aeroporto aeroporto)
        {
            if (string.IsNullOrEmpty(aeroporto.Nome))
            {
                Console.Write("\n\t\t\tNome: ");
                aeroporto.Nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(aeroporto.Nome))
                {
                    Console.WriteLine("\n\t\t\tNome inválido");
                    EntradaDadosAeroporto(aeroporto);
                }
            }

            if (aeroporto.Endereco is null)
                aeroporto.Endereco = EntradaDadosEndereco(new Endereco());

            return aeroporto;
        }

        private Endereco EntradaDadosEndereco(Endereco endereco)
        {
            if (string.IsNullOrEmpty(endereco.CEP))
            {
                Console.Write("\n\t\t\tCEP: ");
                endereco.CEP = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.CEP))
                {
                    Console.WriteLine("\n\t\t\tCEP inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                Console.Write("\n\t\t\tLogradouro: ");
                endereco.Logradouro = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Logradouro))
                {
                    Console.WriteLine("\n\t\t\tLogradouro inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Numero))
            {
                Console.Write("\n\t\t\tNúmero: ");
                endereco.Numero = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Numero))
                {
                    Console.WriteLine("\n\t\t\tNúmero inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Cidade))
            {
                Console.Write("\n\t\t\tCidade: ");
                endereco.Cidade = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Cidade))
                {
                    Console.WriteLine("\n\t\t\tCidade inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Estado))
            {
                Console.Write("\n\t\t\tEstado: ");
                endereco.Estado = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Estado))
                {
                    Console.WriteLine("\n\t\t\tEstado inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Pais))
            {
                Console.Write("\n\t\t\tPais: ");
                endereco.Pais = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Pais))
                {
                    Console.WriteLine("\n\t\t\tPais inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            if (string.IsNullOrEmpty(endereco.Bairro))
            {
                Console.Write("\n\t\t\tBairro: ");
                endereco.Bairro = Console.ReadLine();
                if (string.IsNullOrEmpty(endereco.Bairro))
                {
                    Console.WriteLine("\n\t\t\tBairro inválido");
                    EntradaDadosEndereco(endereco);
                }
            }

            Console.Write("\n\t\t\tComplemento: ");
            endereco.Complemento = Console.ReadLine();


            return endereco;
        }
    }
}
