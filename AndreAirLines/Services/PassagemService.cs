using AndreAirLines.Data.Repository;
using AndreAirLines.File;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreAirLines.Services
{
    public class PassagemService

    {
        VooService vooService = new VooService();
        PassageiroService passageiroService = new PassageiroService();

        PassagemRepository passagemRepository = new PassagemRepository();

        public void SubMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t _________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| Passagem |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Comprar Passagem                             |");
            Console.WriteLine("\t\t\t\t\t|2| - Importar JSON                                |");
            Console.WriteLine("\t\t\t\t\t|0| - Voltar                                       |");
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
                    AddFilePassagem();
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
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Voo...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }

        public void AddFilePassagem()
        {
            Console.WriteLine("Digite o caminho do arquivo Ex: C:\\passagens.json");
            string path = Console.ReadLine();

            var passagens = ReadJson.getData<Passagem>(path);

            if (passagens == null)
            {
                Console.WriteLine("Nenhuma passagem importada");
            }

            passagemRepository.AddRangeFile(passagens);
        }

        public void Add()
        {
            passagemRepository.Add(EntradaDadosPassagem(new Passagem()));
        }

        private Passagem EntradaDadosPassagem(Passagem passagem)
        {

            if (passagem.Passageiro == null)
            {
                passagem.Passageiro = passageiroService.GetPassageiroByCpf();

                if (string.IsNullOrEmpty(passagem.Passageiro.Nome))
                {
                    passagem.Passageiro = passageiroService.EntradaDadosPassageiro(passagem.Passageiro);
                }
            }

            Console.WriteLine("Voos para hoje:");
            Console.WriteLine();
            List<Voo> voos = vooService.GetVooNow();

            Console.WriteLine("Digite a posição do voo que passageiro irá hoje: ");
            int position = int.Parse(Console.ReadLine());

            passagem.Voo = voos[position -1];

            return passagem;
        }

    }
}