/*
 * Autor: Arturo Balleros Albillo
 */
using System.Data;

namespace ReadyReq.Model
{
    public abstract class ClsObjEstandar : ClsObjBase
    {
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
        public int Urgencia { get; set; }
        public int Estabilidad { get; set; }
        public bool Estado { get; set; }
        public DataTable Autores { get; set; } = new DataTable();
        public DataTable Fuentes { get; set; } = new DataTable();
        public DataTable Objetivos { get; set; } = new DataTable();
        public DataTable BGrupo { get; set; } = new DataTable();
        public DataTable BFuentes { get; set; } = new DataTable();
        public DataTable BObjetivos { get; set; } = new DataTable();
    }
}
