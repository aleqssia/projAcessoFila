using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projAcessosFila
{
    class Usuario
    {
        private int id;
        private String nome;
        private List<Ambiente> ambientes = new List<Ambiente>();
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public List<Ambiente> Ambientes
        {
            get { return ambientes; }
            set { ambientes = value; }
        }

        public bool concederPermissao(Ambiente ambiente)
        {
            foreach (Ambiente ambienteListado in ambientes)
            {
                if (ambienteListado.Id.Equals(ambiente.Id))
                {
                    return false;
                }
            }
            ambientes.Add(ambiente);
            return true;
        }
        public bool revogarPermissao(Ambiente ambiente)
        {
            foreach (Ambiente ambienteListado in ambientes)
            {
                if (ambienteListado.Id.Equals(ambiente.Id))
                {
                    ambientes.Remove(ambienteListado);
                    return true;
                }
            }
            return false;
        }
    }
}
