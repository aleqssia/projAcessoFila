using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projAcessosFila
{
    class Ambiente
    {
        private int id;
        private String nome;
        private Queue<Log> logs = new Queue<Log>();

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
        public Queue<Log> Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public void registrarLog(Log log)
        {
            if(logs.Count == 100)            
                logs.Dequeue();
            
            logs.Enqueue(log);
        }
    }
}
