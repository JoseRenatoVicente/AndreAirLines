using AndreAirLines.Data.Repository;
using AndreAirLines.File;
using AndreAirLines.Model.Entities;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AndreAirLines.Services
{
    public class VooService
    {
        AeronaveService aeronaveService = new AeronaveService();
        AeroportoService aeroportoService = new AeroportoService();

        public VooRepository vooRepository = new VooRepository();

        public void SubMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t _________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| VOOS |+++++++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Adicionar Voos                               |");
            Console.WriteLine("\t\t\t\t\t|2| - Mostrar Voos de hoje                         |");
            Console.WriteLine("\t\t\t\t\t|3| - Buscar Voos por Pais                         |");
            Console.WriteLine("\t\t\t\t\t|4| - Buscar Voos por Destino                      |");
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
                    List<Voo> voos = GetVooNow();
                    if (voos.Count != 0) GenerateXMl(voos);
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();

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

        public List<Voo> GetVooNow()
        {
            List<Voo> voo = vooRepository.GetVooByDate(DateTime.Now);

            if (voo.Count == 0)
            {
                Console.WriteLine("Nenhum Voo para hoje");
            }
            voo.ForEach(c => Console.WriteLine(c.ToString()));


            return voo;
        }

        public void GenerateXMl(List<Voo> voos)
        {
            Console.WriteLine("Deseja gerar um xml? s/n");
            bool confirma = Console.ReadLine().ToLower() == "s";

            if (confirma)
            {
                XElement xmlVoos = GerarXML.VoosToXML(voos);
                Console.WriteLine(xmlVoos);
            }
        }


        public void Add()
        {
            vooRepository.Add(EntradaDadosVoo(new Voo()));
        }

        private Voo EntradaDadosVoo(Voo voo)
        {

            if (voo.Aeronave == null)
            {
                Console.Write("Qual a aeronave para esse voo, digite a sua ");
                voo.Aeronave = aeronaveService.GetAeronaveByPlaca();

                if (voo.Aeronave == null)
                {
                    Console.WriteLine("\n\t\t\tAeronave inválido");
                    EntradaDadosVoo(voo);
                }
            }

            if (voo.Origem == null)
            {
                Console.Write("Qual a Origem para esse voo. ");
                voo.Origem = aeroportoService.GetAeroportoBySigla();

                if (voo.Origem == null)
                {
                    Console.WriteLine("\n\t\t\tOrigem inválido");
                    EntradaDadosVoo(voo);
                }
            }

            if (voo.Destino == null)
            {
                Console.Write("Qual a Destino para esse voo. ");
                voo.Destino = aeroportoService.GetAeroportoBySigla();

                if (voo.Destino == null)
                {
                    Console.WriteLine("\n\t\t\tDestino inválido");
                    EntradaDadosVoo(voo);
                }
            }

            if (voo.HorarioEmbarque == default(DateTime))
            {
                Console.Write("\n\t\t\tHorário de Embarque Ex: 10/10/2022 10:50: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime horarioEmbarque))
                {
                    voo.HorarioEmbarque = horarioEmbarque;

                }
                else
                {
                    Console.WriteLine("\n\t\t\tHorário de Embarque inválido");
                    EntradaDadosVoo(voo);
                }
            }

            if (voo.HorarioDesembarque == default(DateTime))
            {
                Console.Write("\n\t\t\tHorário de Desembarque Ex: 10/10/2022 10:50: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime horarioDesembarque))
                {
                    voo.HorarioDesembarque = horarioDesembarque;

                }
                else
                {
                    Console.WriteLine("\n\t\t\tHorário de Desembarque inválido");
                    EntradaDadosVoo(voo);
                }
            }

            return voo;
        }

    }
}
