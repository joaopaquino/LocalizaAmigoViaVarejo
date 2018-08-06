using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalizaAmigos.BO;
using LocalizaAmigos.Models;

namespace LocalizaAmigos
{
    public class Menu
    {
        string campo;
        int idAmigo;
        string nome;
        double longitude;
        double latitude;
        AmigosBO amigosBO;
        public Menu()
        {
            amigosBO = new AmigosBO();
            MenuInicial();
        }
        public void MenuInicial()
        {
            Console.Clear();
            Console.WriteLine("-------Menu Inicial-------");
            Console.WriteLine("Selecione a opção desejada");
            Console.WriteLine("1 - Visitar Amigo");
            Console.WriteLine("2 - Incluir Amigo");
            Console.WriteLine("3 - Alterar Amigo");
            Console.WriteLine("4 - Excluir Amigo");
            Console.WriteLine("5 - Sair");
            
            string tecla = Console.ReadKey().KeyChar.ToString();

            switch (tecla)
            {
                case "1":
                    VisitarAmigoMenu();
                    break;
                case "2":
                    IncluirAmigoMenu();
                    break;
                case "3":
                    AlterarAmigoMenu();
                    break;
                case "4":
                    ExcluirAmigoMenu();
                    break;
                case "5":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Console.ReadKey();
                    MenuInicial();
                    break;
            }
        }
        public void IncluirAmigoMenu()
        {
            Console.Clear();
            Console.WriteLine("Informe os dados do seu amigo.");

            Console.Write("Nome: ");
            campo = Console.ReadLine();
            ValidationConsole.ValidaCampoVazio("Nome", ref campo);
            nome = campo;

            Console.Write("Latitude: ");
            campo = Console.ReadLine();
            latitude = ValidationConsole.ValidaCordenada("Latitude: ", campo);

            Console.Write("longitude: ");
            campo = Console.ReadLine();
            longitude = ValidationConsole.ValidaCordenada("longitude: ", campo);
            
            if (amigosBO.InserirAmigo(nome, latitude, longitude))
            {
                Console.Write("Amigo incluído com sucesso! aperte enter para voltar ao Manu Inicial");
                SubmenuNovaAcao("Incluir");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Já existe um amigo cadastrado com essa mesma localização. Favor inserir uma localização diferente");
                Console.WriteLine("O que você deseja fazer?");
                SubmenuNovaAcao("Incluir");
            }
        }

        public void AlterarAmigoMenu()
        {
            Amigo amigo = BuscaAmigoPorNome("Alterar");
            bool sucesso = true;
            if (amigo != null)
            {
                bool continuaAlteracao = true;
                while (continuaAlteracao)
                {
                    Console.Clear();
                    Console.WriteLine("----------Alterar Amigo----------");
                    Console.WriteLine("Nome:{0}", amigo.nome);
                    Console.WriteLine("Latitude:{0}", amigo.latitude);
                    Console.WriteLine("longitude:{0}", amigo.longitude);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("Selecione a opção que deseja alterar?");
                    Console.WriteLine("1 - Nome");
                    Console.WriteLine("2 - Latitude");
                    Console.WriteLine("3 - longitude");
                    Console.WriteLine("4 - Finaliza");

                    string tecla = Console.ReadKey().KeyChar.ToString();

                    switch (tecla)
                    {
                        case "1":
                            Console.Write("Insira o novo Nome:");
                            campo = Console.ReadLine();
                            ValidationConsole.ValidaCampoVazio("Nome", ref campo);
                            nome = campo;
                            amigo.nome = nome;
                            Console.WriteLine("Selecione próxima ação");
                            Console.WriteLine("1 - Alterar outro campo");
                            Console.WriteLine("2 - Finalizar alteração");

                            switch (Console.ReadKey().KeyChar.ToString())
                            {
                                case "1":
                                    continuaAlteracao = true;
                                    break;
                                case "2":
                                    sucesso = amigosBO.AlterarAmigo(amigo);
                                    continuaAlteracao = false;
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida");
                                    break;
                            }
                            break;
                        case "2":
                            Console.Write("Insira uma nova latitude:");
                            campo = Console.ReadLine();
                            amigo.latitude = ValidationConsole.ValidaCordenada("Latitude", campo);
                            Console.WriteLine("Selecione próxima ação");
                            Console.WriteLine("1 - Alterar outro campo");
                            Console.WriteLine("2 - Finalizar alteração");

                            switch (Console.ReadKey().KeyChar.ToString())
                            {
                                case "1":
                                    continuaAlteracao = true;
                                    break;
                                case "2":
                                    sucesso = amigosBO.AlterarAmigo(amigo);
                                    continuaAlteracao = false;
                                    break;
                            }
                            break;
                        case "3":
                            Console.WriteLine("Insira uma nova longitude:");
                            campo = Console.ReadLine();
                            amigo.longitude = ValidationConsole.ValidaCordenada("longitude", campo);
                            Console.WriteLine("Selecione próxima ação");
                            Console.WriteLine("1 - Alterar outro campo");
                            Console.WriteLine("2 - Finalizar alteração");

                            switch (Console.ReadKey().KeyChar.ToString())
                            {
                                case "1":
                                    continuaAlteracao = true;
                                    break;
                                case "2":
                                    sucesso = amigosBO.AlterarAmigo(amigo);
                                    continuaAlteracao = false;
                                    break;
                            }
                            break;
                        case "4":
                            SubmenuNovaAcao("Alterar");
                            break;
                    }
                }
            }
            else
            {
                Console.Write("Não foi encontrado nenhum amigo com o ID informado.");
                BuscarNovamenteMenu("ID", "Alterar");
            }
            if (sucesso)
            {
                Console.Write("Amigo alterado com sucesso! aperte enter para voltar ao Manu Inicial");
                Console.ReadLine();
                MenuInicial();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Já existe um amigo cadastrado com essa mesma localização. Favor inserir uma localização diferente");
                SubmenuNovaAcao("Alterar");
            }
        }
        public void ExcluirAmigoMenu()
        {
            Amigo amigo = BuscaAmigoPorNome("Excluir");
            if (amigo != null)
            {
                amigosBO.ExcluirAmigo(amigo.amigoID);
                Console.Clear();
                Console.WriteLine("Amigo excluído com Sucesso!");
                SubmenuNovaAcao("Excluir");
            }
            else
            {
                Console.Write("Não foi encontrado nenhum amigo com o ID informado.");
                BuscarNovamenteMenu("ID", "Excluir");
            }
        }
        public void VisitarAmigoMenu()
        {
            Amigo amigo = BuscaAmigoPorNome("Visitar");
            if (amigo != null)
            {
                List<Amigo> amigosProximos = amigosBO.BuscarAmigosProximos(amigo);
                Console.Write("----------Foram encontrados alguns amigos proximos----------");
                ExibirAmigos(amigosProximos, "Visitar");
                SubmenuNovaAcao("Visitar");
            }
            else
            {
                Console.WriteLine("Não foi encontrado nenhum amigo com o ID informado.");
                BuscarNovamenteMenu("ID", "Visitar");
            }
        }
        public void ExibirAmigos(List<Amigo> _amigos, string _ação)
        {
            int count = _amigos.Count;
            if (_amigos == null || count == 0)
            {
                Console.Clear();
                Console.WriteLine("Não foram encontrados Amigos");
                BuscarNovamenteMenu("Nome", _ação);
            }
            else
            {
                Console.WriteLine("");
                foreach (Amigo amigo in _amigos)
                {
                    Console.WriteLine("ID:{0}", amigo.amigoID);
                    Console.WriteLine("Nome:{0}", amigo.nome);
                    Console.WriteLine("Latitude:{0}", amigo.latitude);
                    Console.WriteLine("longitude:{0}", amigo.longitude);
                    Console.WriteLine("--------------------------------");
                }
            }
        }
        public void BuscarNovamenteMenu(string _campo, string _acao)
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Tentar novamente");
            Console.WriteLine("2 - Voltar ao Menu Principal");

            string tecla = Console.ReadKey().KeyChar.ToString();

            switch (tecla)
            {
                case "1":
                    switch (_acao)
                    {
                        case "Alterar":
                            AlterarAmigoMenu();
                            break;
                        case "Excluir":
                            ExcluirAmigoMenu();
                            break;
                        case "Visitar":
                            VisitarAmigoMenu();
                            break;
                    }
                    break;
                case "2":
                    MenuInicial();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
        public Amigo BuscaAmigoPorNome(string _acaoMenu)
        {
            Console.Clear();
            Console.WriteLine("Informe o nome do amigo que deseja {0}:", _acaoMenu);

            Console.Write("Nome: ");
            campo = Console.ReadLine();
            ValidationConsole.ValidaCampoVazio("Nome", ref campo);
            nome = campo;

            List<Amigo> amigos = amigosBO.ConsultarAmigoPorNome(nome);

            ExibirAmigos(amigos, _acaoMenu);

            if (amigos.Count > 1)
            {
                Console.WriteLine("Insira o ID do Amigo que deseja {0}:", _acaoMenu);
                int.TryParse(Console.ReadLine(), out idAmigo);
                return amigos.Where(x => x.amigoID == idAmigo).FirstOrDefault();
            }
            else
            {
                return amigos.FirstOrDefault();
            }
        }
        public void SubmenuNovaAcao(string _acao)
        {
            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - {0} Novamente", _acao);
            Console.WriteLine("2 - Retornar ao Menu Inicial");

            string tecla = Console.ReadKey().KeyChar.ToString();

            switch (tecla)
            {
                case "1":
                    switch (_acao)
                    {
                        case "Incluir":
                            IncluirAmigoMenu();
                            break;
                        case "Alterar":
                            AlterarAmigoMenu();
                            break;
                        case "Excluir":
                            ExcluirAmigoMenu();
                            break;
                        case "Visitar":
                            VisitarAmigoMenu();
                            break;
                    }
                    break;
                case "2":
                    MenuInicial();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }
}
