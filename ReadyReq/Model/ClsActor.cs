using ReadyReq.Interface;
using ReadyReq.Util;
using System;
using System.Data;

namespace ReadyReq.Model
{
    public sealed class ClsActor : ClsObjBase, IObjEstandar
    {
        //Propiedades
        public string Descripcion { get; set; }
        public int Complejidad { get; set; }
        public string DescComplejidad { get; set; }
        public DataTable Autores { get; set; } = new DataTable();
        public DataTable Fuentes { get; set; } = new DataTable();
        public DataTable BGrupo { get; set; } = new DataTable();
        public DataTable BFuentes { get; set; } = new DataTable();

        //Métodos de Interfaz
        public void IniciarValores()
        {
            Buscador.Rows.Clear();
            Id = 0;
            Nombre = string.Empty;
            Version = 1.0;
            Fecha = DateTime.Today.Date;
            Descripcion = string.Empty;
            Complejidad = 0;
            DescComplejidad = string.Empty;
            Categoria = 0;
            Comentario = string.Empty;
            BGrupo.Rows.Clear();
            BFuentes.Rows.Clear();
            Autores.Rows.Clear();
            Fuentes.Rows.Clear();
        }
        public int Guardar()
        {
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update Actores Set Nombre = '" + Nombre + "', Version = " + ClsFunciones.DoubleToString(Version) + ", Fecha = '" + ClsFunciones.FechaMySQL(Fecha) + "', Descripcion = '" + Descripcion + "', Complejidad = " + Complejidad + ", DescComple = '" + DescComplejidad + "', Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";")) return -1;
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + Id + ";");
                if (GuardarTablas(Id) == -1) return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into Actores(Nombre,Version,Fecha,Descripcion,Complejidad,DescComple,Categoria,Comentario) values ('" + Nombre + "'," + ClsFunciones.DoubleToString(Version) + ",'" + ClsFunciones.FechaMySQL(Fecha) + "','" + Descripcion + "'," + Complejidad + ",'" + DescComplejidad + "'," + Categoria + ",'" + Comentario + "');")) return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from Actores order by Id Desc;")) == -1) return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from Actores where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from Actores where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Actor = ClsBaseDatos.BDTable("Select * from Actores where Id = " + id + ";").Rows[0];
            Id = int.Parse(Actor[0].ToString());
            Nombre = Actor[1].ToString();
            Version = (double)Actor[2];
            Fecha = (DateTime)Actor[3];
            Descripcion = Actor[4].ToString();
            Complejidad = int.Parse(Actor[5].ToString());
            DescComplejidad = Actor[6].ToString();
            Categoria = int.Parse(Actor[7].ToString());
            Comentario = Actor[8].ToString();

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActAuto aa where g.Id = aa.IdAutor and aa.IdAct = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActFuen af where g.Id = af.IdFuen and af.IdAct = " + Id + " Order By Categoria Desc, Nombre;");

            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ActAuto where idAct = " + Id + ") Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ActFuen where idAct = " + Id + ") Order By Categoria Desc, Nombre;");
        }
        public int ComprobarExistencia(string valor)
        {
            int id = (int)ClsBaseDatos.BDDouble("Select Id from Actores where Nombre = '" + valor + "';");
            return (id != -1) ? id : -1;
        }
        public void CargarTablas()
        {
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActAuto aa where g.Id = aa.IdAutor and aa.IdAct = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActFuen af where g.Id = af.IdFuen and af.IdAct = " + Id + " Order By Categoria Desc, Nombre;");
        }

        //Métodos Privados
        private int GuardarTablas(int id)
        {
            DataRow Fila;
            for (int i = 0; i <= (Autores.Rows.Count - 1); i++)
            {
                Fila = Autores.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ActAuto(IdAutor, IdAct) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from Actores where Id = " + id + ";");
                    return -1;
                }
            }

            for (int i = 0; i <= (Fuentes.Rows.Count - 1); i++)
            {
                Fila = Fuentes.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ActFuen(IdFuen, IdAct) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from Actores where Id = " + id + ";");
                    return -1;
                }
            }
            return 0;
        }
    }
}