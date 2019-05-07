using System.Data;

namespace ReadyReq.Model
{
    public abstract class ClsObjBase
    {
        public DataTable Buscador { get; set; } = new DataTable();
        public int Id;
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public string Comentario { get; set; }
    }
}
