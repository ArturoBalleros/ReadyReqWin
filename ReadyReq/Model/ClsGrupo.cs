using ReadyReq.Interface;
using System.Data;

namespace ReadyReq.Model
{
    public class ClsGrupo : ClsObjBase, IObjBase
    {
        //Propiedades
        public string Organizacion { get; set; }
        public string Rol { get; set; }
        public bool Desarrollador { get; set; }

        //Métodos
        public void IniciarValores()
        {
            Id = 0;
            Nombre = string.Empty;
            Organizacion = string.Empty;
            Rol = string.Empty;
            Desarrollador = false;
            Categoria = 1;
            Comentario = string.Empty;
            Buscador.Rows.Clear();
        }
        public int Guardar()
        {
            int intDesarrollador;
            if (Desarrollador) intDesarrollador = 1; else intDesarrollador = 0;
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update Grupo Set Nombre = '" + Nombre + "', Organizacion = '" + Organizacion + "', Rol = '" + Rol + "', Desarrollador = " + intDesarrollador + ", Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";"))
                    return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into Grupo(Nombre,Organizacion,Rol,Desarrollador,Categoria,Comentario) values ('" + Nombre + "','" + Organizacion + "','" + Rol + "'," + intDesarrollador + "," + Categoria + ",'" + Comentario + "');"))
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdAutor = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdFuen = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAutor = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdFuen = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdAutor = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdFuen = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIAuto where IdAutor = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIFuen where IdFuen = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdAutor = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdFuen = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from Grupo where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from Grupo where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Trabajador = ClsBaseDatos.BDTable("Select * from Grupo where Id = " + id + ";").Rows[0];
            Id = int.Parse(Trabajador[0].ToString());
            Nombre = Trabajador[1].ToString();
            Organizacion = Trabajador[2].ToString();
            Rol = Trabajador[3].ToString();
            if ((int)Trabajador[4] == 1) Desarrollador = true; else Desarrollador = false;
            Categoria = int.Parse(Trabajador[5].ToString());
            Comentario = Trabajador[6].ToString();
        }
    }
}