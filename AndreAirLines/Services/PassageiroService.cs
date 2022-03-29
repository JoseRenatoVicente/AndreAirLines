using AndreAirLines.Data.Repository;
using AndreAirLines.File;
using AndreAirLines.Model.Entities;
using AndreAirLines.Model.Entities.Enums;
using AndreAirLines.Model.Entities.Helpers;
using System;

namespace AndreAirLines.Services
{
    public class PassageiroService
    {
        PassageiroRepository passageiroRepository = new PassageiroRepository();

        public void SubMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| PASSAGEIROS |++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - Adicionar Passageiro                         |");
            Console.WriteLine("\t\t\t\t\t|2| - Localizar Passageiro por CPF                 |");
            Console.WriteLine("\t\t\t\t\t|3| - Localizar Passageiro por Nome                |");
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
                    GetPassageiroByCpf();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    GetPassageiroByNome();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    AddFilePassageiro();
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
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Passageiro...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }

        public void Add()
        {
            Passageiro passageiro = new Passageiro();

            Console.Write("\n\t\t\tCPF: ");
            passageiro.Cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
            if (!passageiro.ValidarCpf())
            {
                Console.WriteLine("\n\t\t\tCPF inválido");
                Add();
            }
            else if (passageiroRepository.GetPassageiroByCPF(passageiro.Cpf) is not null)
            {
                Console.WriteLine("Passageiro já cadastrado com esse cpf");
            }
            else
                passageiroRepository.Add(EntradaDadosPassageiro(passageiro));
        }

        public Passageiro GetPassageiroByCpf()
        {
            Console.WriteLine("Digite o CPF do Passageiro: ");
            string cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
            if (!ValidaCPF.IsCpf(cpf))
            {
                Console.WriteLine("\n\t\t\tCPF inválido");
                GetPassageiroByCpf();
            }

            Passageiro passageiro = passageiroRepository.GetPassageiroByCPF(cpf);

            if (passageiro is not null)
            {

                Console.WriteLine(passageiro.ToString());
                return passageiro;
            }

            return passageiro = new Passageiro { Cpf = cpf };

        }

        public void GetPassageiroByNome()
        {
            Console.WriteLine("Digite o Nome do Passageiro: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrEmpty(nome))
            {
                Console.WriteLine("\n\t\t\tNome inválido");
                GetPassageiroByNome();
            }

            passageiroRepository.GetPassageiroByNome(nome).ForEach(c => Console.WriteLine(c.ToString()));
        }

        public void AddFilePassageiro()
        {
            Console.WriteLine("Digite o caminho do arquivo Ex: C:\\Passageiros.json");
            string path = Console.ReadLine();

            var passageiros = ReadJson.getData<Passageiro>(path);

            if (passageiros == null)
            {
                Console.WriteLine("Nenhum passageiro importado");
            }

            passageiroRepository.AddRange(passageiros);
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

        public Passageiro EntradaDadosPassageiro(Passageiro passageiro)
        {
            if (string.IsNullOrEmpty(passageiro.Nome))
            {
                Console.Write("\n\t\t\tNome: ");
                passageiro.Nome = Console.ReadLine();
                if (string.IsNullOrEmpty(passageiro.Nome))
                {
                    Console.WriteLine("\n\t\t\tNome inválido");
                    EntradaDadosPassageiro(passageiro);
                }
            }

            if (string.IsNullOrEmpty(passageiro.Telefone))
            {
                Console.Write("\n\t\t\tTelefone: ");
                passageiro.Telefone = Console.ReadLine();
                if (string.IsNullOrEmpty(passageiro.Telefone))
                {
                    Console.WriteLine("\n\t\t\tTelefone inválido");
                    EntradaDadosPassageiro(passageiro);
                }
            }

            if (passageiro.Sexo == 0)
            {
                Console.Write("\n\t\t\tSexo M - Masculino e F - Feminino: ");
                if (char.TryParse(Console.ReadLine(), out char sexo))
                {
                    passageiro.Sexo = (Sexo)sexo;

                }
                else
                {
                    Console.WriteLine("\n\t\t\tSexo inválido");
                    EntradaDadosPassageiro(passageiro);
                }
            }

            if (passageiro.DataNasc == default(DateTime))
            {
                Console.Write("\n\t\t\tData Nascimento Ex: 10/10/200: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataNascimento))
                {
                    passageiro.DataNasc = dataNascimento;

                }
                else
                {
                    Console.WriteLine("\n\t\t\tData Nascimento inválido");
                    EntradaDadosPassageiro(passageiro);
                }
            }

            if (!passageiro.ValidarEmail())
            {
                Console.Write("\n\t\t\tEmail: ");
                passageiro.Email = Console.ReadLine();
                if (!passageiro.ValidarEmail())
                {
                    Console.WriteLine("\n\t\t\tEmail inválido");
                    EntradaDadosPassageiro(passageiro);
                }
            }

            if (passageiro.Endereco is null)
                passageiro.Endereco = EntradaDadosEndereco(new Endereco());

            return passageiro;
        }
    }
}
