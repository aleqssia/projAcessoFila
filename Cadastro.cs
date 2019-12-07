using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace projAcessosFila
{
    class Cadastro
    {
        private String conString = "Data Source=DESKTOP-2NVF8E5\\SQLEXPRESS;Initial Catalog=projAcessos;Integrated Security=True";
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Ambiente> ambientes = new List<Ambiente>();

        public void adicionarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }
        public bool removerUsuario(Usuario usuario)
        {
            foreach (Usuario usuarioListado in usuarios)
            {
                if (usuarioListado.Id.Equals(usuario.Id))
                {
                    usuarios.Remove(usuarioListado);
                    return true;
                }
            }
            return false;
        }
        public Usuario pesquisarUsuario(Usuario usuario)
        {
            foreach (Usuario usuarioListado in usuarios)
            {
                if (usuarioListado.Id.Equals(usuario.Id))
                {
                    return usuarioListado;
                }
            }
            return null;
        }
        public void adicionarAmbiente(Ambiente ambiente)
        {
            ambientes.Add(ambiente);
        }
        public bool removerAmbiente(Ambiente ambiente)
        {
            foreach(Ambiente amb in ambientes)
            {
                if (amb.Id.Equals(ambiente.Id))
                {
                    ambientes.Remove(amb);
                    return true;
                }
            }
            return false;
        }
        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            foreach(Ambiente amb in ambientes)
            {
                if (amb.Id.Equals(ambiente.Id))
                {
                    return amb;
                }
            }
            return null;
        }
        public void upload()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    string query;
                    new SqlCommand("delete from tb_usuarios_ambientes", con).ExecuteNonQuery();
                    new SqlCommand("delete from tb_log", con).ExecuteNonQuery();
                    new SqlCommand("delete from tb_usuarios", con).ExecuteNonQuery();
                    new SqlCommand("delete from tb_ambientes", con).ExecuteNonQuery();
                    
                    foreach (Ambiente ambiente in ambientes)
                    {
                        query = $"insert into tb_ambientes values ({ambiente.Id}, '{ambiente.Nome}')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();

                    }

                    foreach(Usuario usuario in usuarios)
                    {
                        query = $"insert into tb_usuarios values ({usuario.Id}, '{usuario.Nome}')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        foreach(Ambiente ambiente in usuario.Ambientes)
                        {
                            query = $"insert into tb_usuarios_ambientes values ({ambiente.Id}, {usuario.Id})";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    foreach(Ambiente ambiente in ambientes)
                    {
                        foreach (Log log in ambiente.Logs)
                        {
                            int tpAcesso;

                            if (log.TipoAcesso)
                                tpAcesso = 1;
                            else
                                tpAcesso = 0;

                            query = $"insert into tb_log values ('{log.DtAcesso}', {log.Usuario.Id}, {ambiente.Id}, {tpAcesso})";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                   
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }

            }
            con.Close();
        }
        public void download()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if(con.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from tb_ambientes";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Ambiente ambienteNovo = new Ambiente();
                    ambienteNovo.Id = (int)rdr["id"];
                    ambienteNovo.Nome = (String)rdr["nome"];
                    adicionarAmbiente(ambienteNovo);
                }
                rdr.Close();
                query = "select * from tb_usuarios";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Usuario usuarioNovo = new Usuario();
                    usuarioNovo.Id = (int)rdr["id"];
                    usuarioNovo.Nome = (String)rdr["nome"];
                    adicionarUsuario(usuarioNovo);
                }
                rdr.Close();
                query = "select * from tb_usuarios_ambientes";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Ambiente ambienteNovo = new Ambiente();
                    ambienteNovo.Id = (int)rdr["fk_ambiente"];
                    Ambiente ambienteBaixado = pesquisarAmbiente(ambienteNovo);
                    Usuario usuarioNovo = new Usuario();
                    usuarioNovo.Id = (int)rdr["fk_usuarios"];
                    Usuario usuarioBaixado = pesquisarUsuario(usuarioNovo);
                    usuarioBaixado.concederPermissao(ambienteBaixado);
                }
                rdr.Close();
                query = "select * from tb_log";
                cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Ambiente ambienteNovo = new Ambiente();
                    ambienteNovo.Id = (int)rdr["fk_ambiente"];
                    Ambiente ambienteBaixado = pesquisarAmbiente(ambienteNovo);
                    
                    Usuario usuarioNovo = new Usuario();
                    usuarioNovo.Id = (int)rdr["fk_usuario"];
                    Usuario usuarioBaixado = pesquisarUsuario(usuarioNovo);

                    bool tpAcesso;

                    if ((bool)rdr["tpAcesso"])
                        tpAcesso = true;
                    else
                        tpAcesso = false;

                    ambienteBaixado.registrarLog(new Log((DateTime)rdr["dtAcesso"], usuarioBaixado, tpAcesso));
                }
                rdr.Close();
                con.Close();
            }
        }
    }
}
