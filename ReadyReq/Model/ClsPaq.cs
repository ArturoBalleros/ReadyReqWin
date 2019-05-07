using ReadyReq.Interface;
using System.Data;

namespace ReadyReq.Model
{
    public class ClsPaq : ClsObjBase, IObjBase
    {
        //Métodos
        public void IniciarValores()
        {
            Id = 0;
            Nombre = string.Empty;
            Categoria = 1;
            Comentario = string.Empty;
            Buscador.Rows.Clear();
        }
        public int Guardar()
        {
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update Paquetes Set Nombre = '" + Nombre + "', Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";"))
                    return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into Paquetes(Nombre,Categoria,Comentario) values ('" + Nombre + "'," + Categoria + ",'" + Comentario + "');"))
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("UPDATE ReqFun SET Paquete = " + ClsBaseDatos.BDString("Select Id from Paquetes Where Nombre = 'No Asignado';") + " Where Paquete = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from Paquetes where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from Paquetes where Nombre LIKE '%" + valor + "%' and Nombre <> 'No Asignado' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Paquete = ClsBaseDatos.BDTable("Select * from Paquetes where Id = " + id + ";").Rows[0];
            Id = int.Parse(Paquete[0].ToString());
            Nombre = Paquete[1].ToString();
            Categoria = int.Parse(Paquete[2].ToString());
            Comentario = Paquete[3].ToString();
        }
    }
}