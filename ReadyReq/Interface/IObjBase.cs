namespace ReadyReq.Interface
{
    public interface IObjBase
    {
        void IniciarValores();
        int Guardar();
        void Borrar();
        void Buscar(string valor);
        void Cargar(int id);
        int ComprobarExistencia(string valor);
    }
}
