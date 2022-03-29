using AndreAirLines.Data.Repository;
using AndreAirLines.File;
using AndreAirLines.Model.Entities;
using System;

namespace AndreAirLines.Services
{
    public class AeronaveService
    {
        AeronaveRepository aeronaveRepository = new AeronaveRepository();

        public void SubMenu()
        {

            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| AERONAVE |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Adicionar Aeronave                           |");
            Console.WriteLine("\t\t\t\t\t|2| - Localizar Aeronave por Nome                  |");
            Console.WriteLine("\t\t\t\t\t|3| - Localizar Aeronave por Placa                 |");
            Console.WriteLine("\t\t\t\t\t|4| - Editar uma Aeronave                          |");
            Console.WriteLine("\t\t\t\t\t|5| - Importar JSON                                |");
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
                    GetAeronaveByNome();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    GetAeronaveByPlaca();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    Editar();
                    BackMenu();
                    break;

                case "5":
                    Console.Clear();
                    AddFileAeronave();
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
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Aeronaves...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }
        public Aeronave GetAeronaveByPlaca()
        {
            Console.WriteLine("Digite a Placa da Aeronave: ");
            string placa = Console.ReadLine();
            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("\n\t\t\tPlaca inválida");
                GetAeronaveByPlaca();
            }

            Aeronave aeronave = aeronaveRepository.GetAeronaveById(placa);

            if (aeronave is null)
            {
                Console.WriteLine("Nenhuma Aeronave encontrada com essa placa");
            }
            else
            {
                Console.WriteLine(aeronave.ToString());
            }

            return aeronave;
        }

        public void GetAeronaveByNome()
        {
            Console.WriteLine("Digite o Nome do Passageiro: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("\n\t\t\tNome inválido");
                GetAeronaveByNome();
            }

            aeronaveRepository.GetAeronaveByNome(nome).ForEach(c => Console.WriteLine(c.ToString()));
        }

        public void AddFileAeronave()
        {
            Console.WriteLine("Digite o caminho do arquivo Ex: C:\\aeronaves.json");
            string path = Console.ReadLine();

            var aeronaves = ReadJson.getData<Aeronave>(path);

            if (aeronaves == null)
            {
                Console.WriteLine("Não foi possível importar o arquivo");
            }

            aeronaveRepository.AddRange(aeronaves);
        }

        public void Add()
        {
            Aeronave aeronave = new Aeronave();

            Console.Write("\n\t\t\tPlaca: ");
            aeronave.Id = Console.ReadLine();
            if (string.IsNullOrEmpty(aeronave.Id))
            {
                Console.WriteLine("Placa inválida");
                Add();
            }
            else if (aeronaveRepository.GetAeronaveById(aeronave.Id) is not null)
            {
                Console.WriteLine("Aeronave já cadastrado com essa Placa");
            }
            else
                aeronaveRepository.Add(EntradaDadosAeronave(aeronave));
        }

        private Aeronave EntradaDadosAeronave(Aeronave aeronave)
        {
            if (string.IsNullOrEmpty(aeronave.Nome))
            {
                Console.Write("\n\t\t\tNome: ");
                aeronave.Nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(aeronave.Nome))
                {
                    Console.WriteLine("\n\t\t\tNome inválido");
                    EntradaDadosAeronave(aeronave);
                }
            }

            if (aeronave.Capacidade == 0)
            {
                Console.Write("\n\t\t\tCapacidade: ");
                if (int.TryParse(Console.ReadLine(), out int capacidade))
                {
                    aeronave.Capacidade = capacidade;
                }
                else
                {
                    Console.WriteLine("\n\t\t\tTelefone inválido");
                    EntradaDadosAeronave(aeronave);
                }
            }
            return aeronave;
        }

        public Aeronave Editar()
        {
            Aeronave aeronave = GetAeronaveByPlaca();
            if (aeronave == null)
                return null;

            aeronave.Capacidade = 0;

            aeronave = EntradaDadosAeronave(aeronave);

            return aeronaveRepository.Update(aeronave);
        }
    }
}
