using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyReq.Interface
{
    public interface IObjBase
    {
        void IniciarValores();
        int Guardar();
        void Borrar();
        void Buscar(string valor);
        void Cargar(int id);

    }
}
