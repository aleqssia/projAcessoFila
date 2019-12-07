using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projAcessosFila
{
    class Program
    {
        private static Cadastro cadastro = new Cadastro();
        static void Main(string[] args)
        {
            cadastro.download();
            int opcao;
            do
            {
                Console.WriteLine("*-------------------------------------------------------------------------------------------------------------------*");
                Console.WriteLine("11. Limpeza de  tela");
                Console.WriteLine("10. Consulta dos logs de acessos");
                Console.WriteLine("9.  Registrar acessos");
                Console.WriteLine("8.  Revogar permissão de acesso ao usuario");
                Console.WriteLine("7.  Conceder permissão de acesso ao usuario");
                Console.WriteLine("6.  Exclusão do usuario");
                Console.WriteLine("5.  Consulta de usuario");
                Console.WriteLine("4.  Cadastro de usuario");
                Console.WriteLine("3.  Exclusão de ambiente");
                Console.WriteLine("2.  Consulta de ambiente ");
                Console.WriteLine("1.  Cadastro de ambiente ");
                Console.WriteLine("0.  Sair");
                Console.WriteLine("*-------------------------------------------------------------------------------------------------------------------*");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());
                if (opcao > 10)
                {
                    Console.WriteLine("Opcão não é valida!\n");
                }
                try
                {
                    switch (opcao)
                    {
                        case 0:
                            cadastro.upload();
                            break;
                        case 1:
                            cadastrarAmbiente();
                            break;
                        case 2:
                            consultaDeAmbiente();
                            break;
                        case 3:
                            excluirAmbiente();
                            break;
                        case 4:
                            cadastroDeUsuario();
                            break;
                        case 5:
                            consultaDeUsuario();
                            break;
                        case 6:
                            exclusaoDeUsuario();
                            break;
                        case 7:
                            concederPermissao();
                            break;
                        case 8:
                            revogarPermissao();
                            break;
                        case 9:
                            registroDeAcessos();
                            break;
                        case 10:
                            consultaDeLogs();
                            break;
                        case 11:
                            limparTela();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERRO: {0}", e.ToString());
                }

            } while (opcao != 0);
        }
        
        public static void cadastrarAmbiente()
        {
            Ambiente ambienteNovo = new Ambiente();
            Console.Write("Digite o id do ambiente: ");
            ambienteNovo.Id = int.Parse(Console.ReadLine());
            Console.Write("Digite o nome do ambiente: ");
            ambienteNovo.Nome = Console.ReadLine();
            cadastro.adicionarAmbiente(ambienteNovo);
        }
        public static void consultaDeAmbiente()
        {
            Ambiente ambienteConsulta = new Ambiente();
            Console.Write("Digite o id do ambiente a ser consultado: ");
            ambienteConsulta.Id = int.Parse(Console.ReadLine());
            Ambiente ambienteConsultado = cadastro.pesquisarAmbiente(ambienteConsulta);
            if (ambienteConsultado == null)
            {
                Console.WriteLine("Ambiente nao encontrado!");
            }
            else
            {
                Console.WriteLine("Ambiente ID: {0} ", ambienteConsultado.Id);
                Console.WriteLine("Ambiente Nome: {0}", ambienteConsultado.Nome);
                Console.WriteLine("Logs Registrados: ");
                foreach (Log log in ambienteConsultado.Logs)
                {
                    Console.WriteLine(log.DtAcesso);
                    Console.WriteLine(log.Usuario.Nome);
                    Console.WriteLine(log.TipoAcesso + "\n");
                }
            }
        }

        public static void limparTela()
        {
            Console.Clear();
        }
        public static void excluirAmbiente()
        {
            Ambiente ambienteExcluir = new Ambiente();
            Console.Write("Digite o id do ambiente a ser excluido: ");
            ambienteExcluir.Id = int.Parse(Console.ReadLine());

            if (!cadastro.removerAmbiente(ambienteExcluir))            
                Console.WriteLine("Ambiente não encontrado");            
            else            
                Console.WriteLine("Ambiente Excluido");            
        }
        public static void cadastroDeUsuario()
        {
            Usuario usuarioNovo = new Usuario();
            Console.Write("Digite o id do usuario: ");
            usuarioNovo.Id = int.Parse(Console.ReadLine());
            Console.Write("Digite o nome do usuario: ");
            usuarioNovo.Nome = Console.ReadLine();
            cadastro.adicionarUsuario(usuarioNovo);
        }
        public static void consultaDeUsuario()
        {
            Usuario usuarioConsulta = new Usuario();
            Console.Write("Digite o id do usuario a ser consultado: ");
            usuarioConsulta.Id = int.Parse(Console.ReadLine());
            Usuario usuarioConsultado = cadastro.pesquisarUsuario(usuarioConsulta);
            if (usuarioConsultado == null)
            {
                Console.WriteLine("Usuario nao encontrado!");
            }
            else
            {
                Console.WriteLine("Usuario com ID: {0} ", usuarioConsultado.Id);
                Console.WriteLine("Usuario com Nome: {0}", usuarioConsultado.Nome);
                Console.WriteLine("Ambientes Permitidos para o usuario: ");
                foreach (Ambiente ambiente in usuarioConsultado.Ambientes)
                {
                    Console.WriteLine(ambiente.Nome);
                }
            }

        }
        public static void exclusaoDeUsuario()
        {
            Usuario usuarioExcluir = new Usuario();
            Console.Write("Digite o id do usuario a ser excluido: ");
            usuarioExcluir.Id = int.Parse(Console.ReadLine());

            if (!cadastro.removerUsuario(usuarioExcluir))
                Console.WriteLine("Usuario não encontrado");            
            else            
                Console.WriteLine("Usuario Excluido");            
        }
        public static void concederPermissao()
        {
            Usuario usuarioConsulta = new Usuario();
            Ambiente ambienteConsulta = new Ambiente();

            Console.Write("Digite o id do ambiente a ser vinculado: ");
            ambienteConsulta.Id = int.Parse(Console.ReadLine());
            Ambiente ambienteConsultado = cadastro.pesquisarAmbiente(ambienteConsulta);
            Console.Write("Digite o id do usuario a ser permitido: ");
            usuarioConsulta.Id = int.Parse(Console.ReadLine());
            Usuario usuarioConsultado = cadastro.pesquisarUsuario(usuarioConsulta);

            if (usuarioConsultado.concederPermissao(ambienteConsultado))            
                Console.WriteLine("Permissao concedida");            
            else            
                Console.WriteLine("Permissao ja concedida");
        }
        public static void revogarPermissao()
        {
            Usuario usuarioConsulta = new Usuario();
            Ambiente ambienteConsulta = new Ambiente();

            Console.Write("Digite o id do usuario a ser revogado: ");
            usuarioConsulta.Id = int.Parse(Console.ReadLine());
            Usuario usuarioConsultado = cadastro.pesquisarUsuario(usuarioConsulta);

            Console.Write("Digite o id do ambiente: ");
            ambienteConsulta.Id = int.Parse(Console.ReadLine());
            Ambiente ambienteConsultado = cadastro.pesquisarAmbiente(ambienteConsulta);


            if (usuarioConsultado.revogarPermissao(ambienteConsultado))
                Console.WriteLine("Permissao foi revogada");       
            else            
                Console.WriteLine("Não há permissao registrada para este ambiente");            
        }
        public static void registroDeAcessos()
        {
            Usuario usuarioConsulta = new Usuario();
            Ambiente ambienteConsulta = new Ambiente();

            Console.Write("Digite o id do ambiente que vai ser registrado: ");
            ambienteConsulta.Id = int.Parse(Console.ReadLine());
            Ambiente ambienteConsultado = cadastro.pesquisarAmbiente(ambienteConsulta);

            Console.Write("Digite o id do usuario que vai ser registrado: ");
            usuarioConsulta.Id = int.Parse(Console.ReadLine());
            Usuario usuarioConsultado = cadastro.pesquisarUsuario(usuarioConsulta);

                if (usuarioConsultado.Ambientes.Contains(ambienteConsultado))
                    ambienteConsultado.registrarLog(new Log(DateTime.Now, usuarioConsultado, true));
                else
                    ambienteConsultado.registrarLog(new Log(DateTime.Now, usuarioConsultado, false));
        }
        public static void consultaDeLogs()
        {
            Ambiente ambienteConsulta = new Ambiente();
            Console.Write("Digite o id do ambiente que vai ser vinculado: ");
            ambienteConsulta.Id = int.Parse(Console.ReadLine());
            Ambiente ambienteConsultado = cadastro.pesquisarAmbiente(ambienteConsulta);
            int opcao;
            do
            {
                Console.WriteLine("Deseja filtrar por: ");
                Console.WriteLine("1. Autorizados");
                Console.WriteLine("2. Negados");
                Console.WriteLine("3. Todos");
                Console.Write("Opção: ");
                opcao = int.Parse(Console.ReadLine());
            } while (opcao < 1 || opcao > 3);

            switch (opcao)
            {
                case 1:
                    foreach (Log log in ambienteConsultado.Logs)
                    {
                        if (log.TipoAcesso)
                        {
                            Console.WriteLine(log.DtAcesso);
                            Console.WriteLine(log.Usuario.Nome);
                        }
                    }
                    break;
                case 2:
                    foreach (Log log in ambienteConsultado.Logs)
                    {
                        if (!log.TipoAcesso)
                        {
                            Console.WriteLine(log.DtAcesso);
                            Console.WriteLine(log.Usuario.Nome);
                        }
                    }
                    break;
                case 3:
                    foreach (Log log in ambienteConsultado.Logs)
                    {
                        Console.WriteLine(log.DtAcesso);
                        Console.WriteLine(log.Usuario.Nome);
                    }
                    break;
            }

        }
    }
}
