/*
 * Autor: Arturo Balleros Albillo
 */
using System;
using System.Data;

namespace ReadyReq.Model
{
    public abstract class ClsObjBase
    {
        public DataTable Buscador { get; set; } = new DataTable();
        public int Id;
        public string Nombre { get; set; }
        public double Version { get; set; }
        public DateTime Fecha { get; set; } 
        public int Categoria { get; set; }
        public string Comentario { get; set; }
    }
}
