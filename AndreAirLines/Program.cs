using AndreAirLines.Services;
using System;
using System.Globalization;

namespace AndreAirLines
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

            Console.WriteLine(@"
                             _                                         
             /\             | |                                        
            /  \   _ __   __| |_ __ ___                                
           / /\ \ | '_ \ / _` | '__/ _ \                               
          / ____ \| | | | (_| | | |  __/                               
         /_/    \_\_| |_|\__,_|_|  \___|                               
                         _____ _____  _      _____ _   _ ______  _____ 
                   /\   |_   _|  __ \| |    |_   _| \ | |  ____|/ ____|
                  /  \    | | | |__) | |      | | |  \| | |__  | (___  
                 / /\ \   | | |  _  /| |      | | | . ` |  __|  \___ \ 
                / ____ \ _| |_| | \ \| |____ _| |_| |\  | |____ ____) |
               /_/    \_\_____|_|  \_\______|_____|_| \_|______|_____/ ");

            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++| Andre AIRLINES |+++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Passagem                                     |");
            Console.WriteLine("\t\t\t\t\t|2| - Voos                                         |");
            Console.WriteLine("\t\t\t\t\t|3| - Passageiros                                  |");
            Console.WriteLine("\t\t\t\t\t|4| - Aeronaves                                    |");
            Console.WriteLine("\t\t\t\t\t|5| - Aeroporto                                    |");
            Console.WriteLine("\t\t\t\t\t|0| - Sair                                         |");
            Console.Write("\t\t\t\t\t|__________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "0": Environment.Exit(0); break;


                case "1":
                    Console.Clear();
                    new PassagemService().SubMenu();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    new VooService().SubMenu();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    new PassageiroService().SubMenu();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    new AeronaveService().SubMenu();
                    BackMenu();
                    break;

                case "5":
                    Console.Clear();
                    new AeroportoService().SubMenu();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("\t\t\t\tOpção inválida! ");
                    BackMenu();
                    break;
            }
        }
        public static void BackMenu()
        {
            Console.WriteLine("\n\t\t\t\t Pressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}
