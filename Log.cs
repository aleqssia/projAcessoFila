using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projAcessosFila
{
    class Log
    {
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipoAcesso;

        public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
        {
            this.dtAcesso = dtAcesso;
            this.usuario = usuario;
            this.tipoAcesso = tipoAcesso;
        }
        public DateTime DtAcesso
        {
            get { return dtAcesso;  }
        }
        public Usuario Usuario
        {
            get { return usuario; }
        }
        public bool TipoAcesso
        {
            get { return tipoAcesso; }
        }
    }
}
