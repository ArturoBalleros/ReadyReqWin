using ReadyReq.Interface;
using ReadyReq.Util;
using System.Collections.ObjectModel;
using System.Data;

namespace ReadyReq.Model
{
    public sealed class ClsReqInfo : ClsObjEstandar, IObjReq
    {
        //Propiedades
        public int TiempoMedio { get; set; }
        public int TiempoMaximo { get; set; }
        public int OcurreMedio { get; set; }
        public int OcurreMaximo { get; set; }
        public DataTable Requisitos { get; set; } = new DataTable();
        public ObservableCollection<ClsDatDG> DatosEspeci { get; set; } = new ObservableCollection<ClsDatDG>();
        public DataTable BRequisitos { get; set; } = new DataTable();

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
            TiempoMedio = 0;
            TiempoMaximo = 0;
            OcurreMedio = 0;
            OcurreMaximo = 0;
            Autores.Rows.Clear();
            Fuentes.Rows.Clear();
            Objetivos.Rows.Clear();
            Requisitos.Rows.Clear();
            DatosEspeci.Clear();
            BGrupo.Rows.Clear();
            BObjetivos.Rows.Clear();
            BFuentes.Rows.Clear();
            BRequisitos.Rows.Clear();
        }
        public int Guardar()
        {
            int intEstado = (Estado) ? 1 : 0;
            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update ReqInfo Set Nombre = '" + Nombre + "', Descripcion = '" + Descripcion + "', TiemMed = " + TiempoMedio + ", TiemMax = " + TiempoMaximo + ", OcuMed = " + OcurreMedio + ", OcuMax = " + OcurreMaximo + ", Prioridad = " + Prioridad + ", Urgencia = " + Urgencia + ", Estabilidad = " + Estabilidad + ", Estado = " + intEstado + ", Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";")) return -1;
                ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIDatEsp where IdReq = " + Id + ";");
                if (GuardarTablas(Id) == -1) return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into ReqInfo(Nombre,Descripcion,TiemMed,TiemMax,OcuMed,OcuMax,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + Nombre + "','" + Descripcion + "'," + TiempoMedio + "," + TiempoMaximo + "," + OcurreMedio + "," + OcurreMaximo + "," + Prioridad + "," + Urgencia + "," + Estabilidad + "," + intEstado + "," + Categoria + ",'" + Comentario + "');")) return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqInfo order by Id Desc;")) == -1) return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqIDatEsp where IdReq = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + Id + ";");
            ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from ReqInfo where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void CargarTablas()
        {
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun Order By Categoria Desc, Nombre;");

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " + +Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqInfo where Id = " + id + ";").Rows[0];
            Id = int.Parse(Requisito[0].ToString());
            Nombre = Requisito[1].ToString();
            Descripcion = Requisito[2].ToString();
            TiempoMedio = int.Parse(Requisito[3].ToString());
            TiempoMaximo = int.Parse(Requisito[4].ToString());
            OcurreMedio = int.Parse(Requisito[5].ToString());
            OcurreMaximo = int.Parse(Requisito[6].ToString());
            Prioridad = int.Parse(Requisito[7].ToString());
            Urgencia = int.Parse(Requisito[8].ToString());
            Estabilidad = int.Parse(Requisito[9].ToString());
            Estado = ((int)Requisito[10] == 1) ? true : false;
            Categoria = int.Parse(Requisito[11].ToString());
            Comentario = Requisito[12].ToString();

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqIAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqIFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqIObj r where o.Id = r.IdObj and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqInfo + " Order By Categoria Desc, Nombre;");

            DataTable TablaAux; DataRow Fila; DatosEspeci.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqIDatEsp where IdReq = " + Id + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                DatosEspeci.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            Requisitos.Rows.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqInfo + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqNFun + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = " + DefValues.ReqFun + " Order By Categoria Desc, Nombre;"); CargarTablaReq(TablaAux);

            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqIObj where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqIAuto where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqIFuen where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id, int tipoReq)
        {
            Cargar(id);
            CargarTablaReqRel(tipoReq);
        }
        public void CargarTablaReqRel(int TipoReq)
        {
            if (TipoReq == DefValues.ReqInfo) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqIReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqInfo + ") and Id <> " + Id + " Order By Categoria Desc, Nombre;");
            else if (TipoReq == DefValues.ReqNFun) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqIReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqNFun + ") Order By Categoria Desc, Nombre;");
            else if (TipoReq == DefValues.ReqFun) BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqIReqR where idReq = " + Id + " and TipoReq = " + DefValues.ReqFun + ") Order By Categoria Desc, Nombre;");
        }

        //Métodos Privados
        private int GuardarTablas(int id)
        {
            DataRow Fila;
            for (int i = 0; i <= (Autores.Rows.Count - 1); i++)
            {
                Fila = Autores.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Fuentes.Rows.Count - 1); i++)
            {
                Fila = Fuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; (i <= Objetivos.Rows.Count - 1); i++)
            {
                Fila = Objetivos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; (i <= Requisitos.Rows.Count - 1); i++)
            {
                Fila = Requisitos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIReqR(IdReqR, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + id + ";");
                    return -1;
                }
            }
            foreach (ClsDatDG Dato in DatosEspeci)
            {
                if (ClsBaseDatos.BDBool("Insert into ReqIDatEsp(IdReq, Descrip) values (" + id + ",'" + Dato.Descrip + "');") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = " + DefValues.ReqInfo + " and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + id + ";");
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