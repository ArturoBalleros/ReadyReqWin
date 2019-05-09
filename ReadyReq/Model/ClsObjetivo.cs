using ReadyReq.Interface;
using System.Data;

namespace ReadyReq.Model
{
    public sealed class ClsObjetivo : ClsObjEstandar, IObjEstandar
    {
        //Métodos de Interfaz
        public void IniciarValores()
        {
            Buscador.Rows.Clear();
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Prioridad = 0;
            Urgencia = 0;
            Estabilidad = 0;
            Estado = true;
            Categoria = 0;
            Comentario = string.Empty;
            BGrupo.Rows.Clear();
            BFuentes.Rows.Clear();
            BObjetivos.Rows.Clear();
            Autores.Rows.Clear();
            Fuentes.Rows.Clear();
            Objetivos.Rows.Clear();
        }
        public int Guardar()
        {
            int intEstado;
            if (Estado) intEstado = 1; else intEstado = 0;
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update Objetivos Set Nombre = '" + Nombre + "', Descripcion = '" + Descripcion + "', Prioridad = " + Prioridad + ", Urgencia = " + Urgencia + ", Estabilidad = " + Estabilidad + ", Estado = " + intEstado + ", Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";"))
                    return -1;
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + Id + ";");
                if (GuardarTablas(Id) == -1)
                    return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into Objetivos(Nombre,Descripcion,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + Nombre + "','" + Descripcion + "'," + Prioridad + "," + Urgencia + "," + Estabilidad + "," + intEstado + "," + Categoria + ",'" + Comentario + "');"))
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from Objetivos order by Id Desc;")) == -1)
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from Objetivos where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Objetivo = ClsBaseDatos.BDTable("Select * from Objetivos where Id = " + id + ";").Rows[0];
            Id = int.Parse(Objetivo[0].ToString());
            Nombre = Objetivo[1].ToString();
            Descripcion = Objetivo[2].ToString();
            Prioridad = int.Parse(Objetivo[3].ToString());
            Urgencia = int.Parse(Objetivo[4].ToString());
            Estabilidad = int.Parse(Objetivo[5].ToString());
            if ((int)Objetivo[6] == 1) Estado = true; else Estado = false;
            Categoria = int.Parse(Objetivo[7].ToString());
            Comentario = Objetivo[8].ToString();

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjAuto oa where g.Id = oa.IdAutor and oa.IdObj = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjFuen obf where g.Id = obf.IdFuen and obf.IdObj = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, Objsubobj os where o.Id = os.IdSubObj and os.IdObj = " + Id + " Order By Categoria Desc, Nombre;");

            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ObjAuto where idObj = " + Id + ") Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ObjFuen where idObj = " + Id + ") Order By Categoria Desc, Nombre;");
            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idSubobj from ObjSubobj where idObj = " + Id + ") and Id <> " + Id + " Order By Categoria Desc, Nombre;");
        }
        public void CargarTablas()
        {
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjAuto oa where g.Id = oa.IdAutor and oa.IdObj = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjFuen obf where g.Id = obf.IdFuen and obf.IdObj = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, Objsubobj os where o.Id = os.IdSubobj and os.IdObj = " + Id + " Order By Categoria Desc, Nombre;");
        }

        //Métodos Privados
        private int GuardarTablas(int id)
        {
            DataRow Fila;
            for (int i = 0; i <= (Autores.Rows.Count - 1); i++)
            {
                Fila = Autores.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ObjAuto(IdAutor, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Fuentes.Rows.Count - 1); i++)
            {
                Fila = Fuentes.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ObjFuen(IdFuen, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Objetivos.Rows.Count - 1); i++)
            {
                Fila = Objetivos.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ObjSubobj(IdSubobj, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + id + ";");
                    return -1;
                }
            }
            return 0;
        }
    }
}