using ReadyReq.Interface;
using System.Collections.ObjectModel;
using System.Data;

namespace ReadyReq.Model
{
    public sealed class ClsReqFun : ClsObjEstandar, IObjReq
    {
        //Propiedades
        public string Paquete { get; set; }
        public string Precondicion { get; set; }
        public string Postcondicion { get; set; }
        public int Complejidad { get; set; }
        public DataTable Requisitos { get; set; } = new DataTable();
        public DataTable Actores { get; set; } = new DataTable();
        public ObservableCollection<ClsDatDG> SecNormal { get; set; } = new ObservableCollection<ClsDatDG>();
        public ObservableCollection<ClsDatDG> SecExcepc { get; set; } = new ObservableCollection<ClsDatDG>();
        public DataTable BRequisitos { get; set; } = new DataTable();
        public DataTable BActores { get; set; } = new DataTable();
        public DataTable BPaquete { get; set; } = new DataTable();

        //Métodos de Interfaz
        public void IniciarValores()
        {
            Buscador.Rows.Clear();
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Paquete = string.Empty;
            Precondicion = string.Empty;
            Postcondicion = string.Empty;
            Prioridad = 0;
            Urgencia = 0;
            Estabilidad = 0;
            Estado = true;
            Categoria = 0;
            Comentario = string.Empty;
            Autores.Rows.Clear();
            Fuentes.Rows.Clear();
            Objetivos.Rows.Clear();
            Requisitos.Rows.Clear();
            Actores.Rows.Clear();
            SecNormal.Clear();
            SecExcepc.Clear();
            BGrupo.Rows.Clear();
            BObjetivos.Rows.Clear();
            BFuentes.Rows.Clear();
            BRequisitos.Rows.Clear();
            BActores.Rows.Clear();
        }
        public int Guardar()
        {
            int intEstado, intPaquete;
            if (Estado == true) intEstado = 1; else intEstado = 0;
            intPaquete = (int)ClsBaseDatos.BDDouble("Select Id from Paquetes where Nombre = '" + Paquete + "';");

            if (Id != 0)
            {
                if (!ClsBaseDatos.BDBool("Update ReqFun Set Nombre = '" + Nombre + "', Descripcion = '" + Descripcion + "', Paquete = " + intPaquete + ", PreCond = '" + Precondicion + "', PostCond = '" + Postcondicion + "', Complejidad = " + Complejidad + ", Prioridad = " + Prioridad + ", Urgencia = " + Urgencia + ", Estabilidad = " + Estabilidad + ", Estado = " + intEstado + ", Categoria = " + Categoria + ", Comentario = '" + Comentario + "' where Id = " + Id + ";"))
                    return -1;
                ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + Id + ";");
                if (GuardarTablas(Id) == -1)
                    return -1;
            }
            else
            {
                if (!ClsBaseDatos.BDBool("Insert into ReqFun(Nombre,Descripcion,Paquete,Precond,Postcond,Complejidad,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + Nombre + "','" + Descripcion + "'," + intPaquete + ",'" + Precondicion + "','" + Postcondicion + "'," + Complejidad + "," + Prioridad + "," + Urgencia + "," + Estabilidad + "," + intEstado + "," + Categoria + ",'" + Comentario + "');"))
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqFun order by Id Desc;")) == -1)
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (Id != 0)
            {
                ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            Buscador = ClsBaseDatos.BDTable("Select Nombre,Id from ReqFun where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void CargarTablas()
        {
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun Order By Categoria Desc, Nombre;");
            BActores = ClsBaseDatos.BDTable("Select Id,Nombre from Actores Order By Categoria Desc, Nombre;");
            BPaquete = ClsBaseDatos.BDTable("Select Nombre from Paquetes Order By Categoria Desc, Nombre;");

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqObj r where o.Id = r.IdObj and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Actores = ClsBaseDatos.BDTable("Select a.Id as Id, a.Nombre as Nombre from Actores a, ReqAct r where a.Id = r.IdAct and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqFun where Id = " + id + ";").Rows[0];
            Id = int.Parse(Requisito[0].ToString());
            Nombre = Requisito[1].ToString();
            Descripcion = Requisito[2].ToString();
            Paquete = ClsBaseDatos.BDString("Select Nombre from Paquetes where Id = " + Requisito[3].ToString() + ";");
            Precondicion = Requisito[4].ToString();
            Postcondicion = Requisito[5].ToString();
            Complejidad = int.Parse(Requisito[6].ToString());
            Prioridad = int.Parse(Requisito[7].ToString());
            Urgencia = int.Parse(Requisito[8].ToString());
            Estabilidad = int.Parse(Requisito[9].ToString());
            if ((int)Requisito[10] == 1) Estado = true; else Estado = false;
            Categoria = int.Parse(Requisito[11].ToString());
            Comentario = Requisito[12].ToString();

            Autores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqAuto r where g.Id = r.IdAutor and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Fuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqFuen r where g.Id = r.IdFuen and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Objetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqObj r where o.Id = r.IdObj and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Actores = ClsBaseDatos.BDTable("Select a.Id as Id, a.Nombre as Nombre from Actores a, ReqAct r where a.Id = r.IdAct and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");
            Requisitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " Order By Categoria Desc, Nombre;");

            DataTable TablaAux; DataRow Fila; SecNormal.Clear(); SecExcepc.Clear();

            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqSecNor where IdReq = " + Id + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                SecNormal.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqSecExc where IdReq = " + Id + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                SecExcepc.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            Requisitos.Rows.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = 2 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + Id + " and r.TipoReq = 3 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);

            BObjetivos = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqObj where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BGrupo = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqAuto where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqFuen where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
            BActores = ClsBaseDatos.BDTable("Select Id,Nombre from Actores where Id not IN (select IdAct from ReqAct where idReq = " + Id + ") Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int id, int tipoReq)
        {
            Cargar(id);
            CargarTablaReqRel(tipoReq);
        }
        public void CargarTablaReqRel(int TipoReq)
        {
            if (TipoReq == 1)
                BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqReqR where idReq = " + Id + " and TipoReq = 1) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 2)
                BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqReqR where idReq = " + Id + " and TipoReq = 2) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 3)
                BRequisitos = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqReqR where idReq = " + Id + " and TipoReq = 3) and Id <> " + Id + " Order By Categoria Desc, Nombre;");
        }

        //Métodos Privados
        private int GuardarTablas(int id)
        {
            DataRow Fila;
            for (int i = 0; i <= (Autores.Rows.Count - 1); i++)
            {
                Fila = Autores.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Fuentes.Rows.Count - 1); i++)
            {
                Fila = Fuentes.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Objetivos.Rows.Count - 1); i++)
            {
                Fila = Objetivos.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Requisitos.Rows.Count - 1); i++)
            {
                Fila = Requisitos.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqReqR(IdReqr, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (Actores.Rows.Count - 1); i++)
            {
                Fila = Actores.Rows[i];
                if (!ClsBaseDatos.BDBool("Insert into ReqAct(IdAct, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + id + ");"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }

            foreach (ClsDatDG Dato in SecNormal)
            {
                if (!ClsBaseDatos.BDBool("Insert into ReqSecNor(IdReq, Descrip) values (" + id + ",'" + Dato.Descrip + "');"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }

            foreach (ClsDatDG Dato in SecExcepc)
            {
                if (!ClsBaseDatos.BDBool("Insert into ReqSecExc(IdReq, Descrip) values (" + id + ",'" + Dato.Descrip + "');"))
                {
                    ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + id + ";");
                    return -1;
                }
            }
            return 0;
        }
        private void CargarTablaReq(DataTable tablaAux)
        {
            DataRow Fila;
            DataRow FilaNueva;
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