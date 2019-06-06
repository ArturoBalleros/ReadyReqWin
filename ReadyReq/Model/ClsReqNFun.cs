using ReadyReq.Interface;
using ReadyReq.Util;
using System;
using System.Data;

namespace ReadyReq.Model
{
    public sealed class ClsReqNFun : ClsObjEstandar, IObjReq
    {
        //Propiedades
        public DataTable Requisitos { get; set; } = new DataTable();
        public DataTable BRequisitos { get; set; } = new DataTable();

        //Métodos de Interfaz
        public void IniciarValores()
        {
            Buscador.Rows.Clear();
            Id = 0;
            Nombre = string.Empty;
            Version = 1.0;
            Fecha = DateTime.Today.Date;
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
            BRequisitos.Rows.Clear();
            Autores.Rows.Clear();
            Fuentes.Rows.Clear();
            Objetivos.Rows.Clear();
            Requisitos.Rows.Clear();
        }
        public int Guardar()
        {
            int intEstado = (Estado) ? 1 : 0;
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update ReqNFunc Set Nombre = '" + Nombre + "', Version = " + ClsFunciones.DoubleToString(Version) + ", Fecha = '" + ClsFunciones.FechaMySQL(Fecha) + "', Descripcion = '" + Descripcion + "', Prioridad = " + Prioridad + ", Urgencia = " + Urgencia + ", Estabilidad = " + Estabilidad + ", Estado = " + intEstado + ", Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";")) return -1;
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + Id + ";");
                if (GuardarTablas(Id) == -1) return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into ReqNFunc(Nombre,Version,Fecha,Descripcion,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + Nombre + "'," + ClsFunciones.DoubleToString(Version) + ",'" + ClsFunciones.FechaMySQL(Fecha) + "','" + Descripcion + "'," + Prioridad + "," + Urgencia + "," + Estabilidad + "," + intEstado + "," + Categoria + ",'" + Comentario + "');")) return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqNFunc order by Id Desc;")) == -1) return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from ReqNFunc where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void CargarTablas()
        {
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun Order By Categoria Desc, Nombre;");
            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id =  r.IdReqr and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqNFunc where Id = " + id + ";").Rows[0];
            Id = int.Parse(Requisito[0].ToString());
            Nombre = Requisito[1].ToString();
            Version = (double)Requisito[2];
            Fecha = (DateTime)Requisito[3];
            Descripcion = Requisito[4].ToString();
            Prioridad = int.Parse(Requisito[5].ToString());
            Urgencia = int.Parse(Requisito[6].ToString());
            Estabilidad = int.Parse(Requisito[7].ToString());
            Estado = ((int)Requisito[8] == 1) ? true : false;
            Categoria = int.Parse(Requisito[9].ToString());
            Comentario = Requisito[10].ToString();

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqInfo + " Order By Categoria Desc, Nombre;");
            Requisitos.Rows.Clear();

            DataTable TablaAux;
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqInfo + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqNFun + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqFun + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);

            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqNObj where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqNAuto where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqNFuen where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id, int tipoReq)
        {
            Cargar(id);
            CargarTablaReqRel(tipoReq);
        }
        public int ComprobarExistencia(string valor)
        {
            int id = (int)ClsBaseDatos.BDDouble("Select Id from ReqNFunc where Nombre = '" + valor + "';");
            return (id != -1) ? id : -1;
        }
        public void CargarTablaReqRel(int tipoReq)
        {
            if (tipoReq == DefValues.ReqInfo) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqNReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqInfo + ") Order By Categoria Desc, Nombre;");
            else if (tipoReq == DefValues.ReqNFun) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqNReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqNFun + ") and Id <> " + Id + " Order By Categoria Desc, Nombre;");
            else if (tipoReq == DefValues.ReqFun) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqNReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqFun + ") Order By Categoria Desc, Nombre;");
        }

        //Métodos Privados
        private int GuardarTablas(int id)
        {
            DataRow Fila;
            for (int i = 0; i <= (Autores.Rows.Count - 1); i++)
            {
                Fila = Autores.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqNAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Fuentes.Rows.Count - 1); i++)
            {
                Fila = Fuentes.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqNFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Objetivos.Rows.Count - 1); i++)
            {
                Fila = Objetivos.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqNObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Requisitos.Rows.Count - 1); i++)
            {
                Fila = Requisitos.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqNReqR(IdReqr, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqNFun + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + id + ";");
                    return -1;
                }
            }
            return 0;
        }
        private void CargarTablaReq(DataTable tablaAux)
        {
            DataRow Fila, FilaNueva;
            for (int i = 0; i <= (tablaAux.Rows.Count - 1); i++)
            {
                Fila = tablaAux.Rows[i];
                FilaNueva = Requisitos.NewRow();
                FilaNueva[0] = Fila[0].ToString();
                FilaNueva[1] = Fila[1].ToString();
                FilaNueva[2] = Fila[2].ToString();
                Requisitos.Rows.Add(FilaNueva);
            }
        }
    }
}