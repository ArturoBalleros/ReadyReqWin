using System.Collections.ObjectModel;
using System.Data;

namespace ReadyReq.Model
{
    public class ClsReqFun
    {
        DataTable DTBuscar = new DataTable();
        int IntId;
        string StrNom;
        string StrDesc;
        string StrPaq;
        string StrPreCond;
        string StrPostCond;
        int IntComp;
        int IntPrio;
        int IntUrge;
        int IntEsta;
        bool BoolEst;
        int IntCate;
        string StrComen;
        DataTable DTAutores = new DataTable();
        DataTable DTFuentes = new DataTable();
        DataTable DTObjetivos = new DataTable();
        DataTable DTRequesitos = new DataTable();
        DataTable DTActores = new DataTable();
        ObservableCollection<ClsDatDG> OCSecNor = new ObservableCollection<ClsDatDG>();
        ObservableCollection<ClsDatDG> OCSecExc = new ObservableCollection<ClsDatDG>();
        DataTable DTBAutor = new DataTable();
        DataTable DTBFuent = new DataTable();
        DataTable DTBObjet = new DataTable();
        DataTable DTBRequi = new DataTable();
        DataTable DTBActor = new DataTable();
        DataTable DTBPaquete = new DataTable();
        public DataTable Buscador
        {
            get { return DTBuscar; }
            set { DTBuscar = value; }
        }
        public string Nombre
        {
            get { return StrNom; }
            set { StrNom = value; }
        }
        public string Descripcion
        {
            get { return StrDesc; }
            set { StrDesc = value; }
        }
        public string Paquete
        {
            get { return StrPaq; }
            set { StrPaq = value; }
        }
        public string Precondicion
        {
            get { return StrPreCond; }
            set { StrPreCond = value; }
        }
        public string Postcondicion
        {
            get { return StrPostCond; }
            set { StrPostCond = value; }
        }
        public int Complejidad
        {
            get { return IntComp; }
            set { IntComp = value; }
        }
        public int Prioridad
        {
            get { return IntPrio; }
            set { IntPrio = value; }
        }
        public int Urgencia
        {
            get { return IntUrge; }
            set { IntUrge = value; }
        }
        public int Estabilidad
        {
            get { return IntEsta; }
            set { IntEsta = value; }
        }
        public bool Estado
        {
            get { return BoolEst; }
            set { BoolEst = value; }
        }
        public int Categoria
        {
            get { return IntCate; }
            set { IntCate = value; }
        }
        public string Comentario
        {
            get { return StrComen; }
            set { StrComen = value; }
        }
        public DataTable Autores
        {
            get { return DTAutores; }
            set { DTAutores = value; }
        }
        public DataTable Fuentes
        {
            get { return DTFuentes; }
            set { DTFuentes = value; }
        }
        public DataTable Objetivos
        {
            get { return DTObjetivos; }
            set { DTObjetivos = value; }
        }
        public DataTable Requisitos
        {
            get { return DTRequesitos; }
            set { DTRequesitos = value; }
        }
        public DataTable Actores
        {
            get { return DTActores; }
            set { DTActores = value; }
        }
        public ObservableCollection<ClsDatDG> SecNormal
        {
            get { return OCSecNor; }
            set { OCSecNor = value; }
        }
        public ObservableCollection<ClsDatDG> SecExcepc
        {
            get { return OCSecExc; }
            set { OCSecExc = value; }
        }
        public DataTable BGrupo
        {
            get { return DTBAutor; }
            set { DTBAutor = value; }
        }
        public DataTable BFuentes
        {
            get { return DTBFuent; }
            set { DTBFuent = value; }
        }
        public DataTable BObjetivos
        {
            get { return DTBObjet; }
            set { DTBObjet = value; }
        }
        public DataTable BRequisitos
        {
            get { return DTBRequi; }
            set { DTBRequi = value; }
        }
        public DataTable BActores
        {
            get { return DTBActor; }
            set { DTBActor = value; }
        }
        public DataTable BPaquete
        {
            get { return DTBPaquete; }
            set { DTBPaquete = value; }
        }
        public void IniciarValores()
        {
            DTBuscar.Rows.Clear();
            IntId = 0;
            StrNom = string.Empty;
            StrDesc = string.Empty;
            StrPaq = string.Empty;
            StrPreCond = string.Empty;
            StrPostCond = string.Empty;
            IntPrio = 0;
            IntUrge = 0;
            IntEsta = 0;
            BoolEst = true;
            IntCate = 0;
            StrComen = string.Empty;
            DTAutores.Rows.Clear();
            DTFuentes.Rows.Clear();
            DTObjetivos.Rows.Clear();
            DTRequesitos.Rows.Clear();
            DTActores.Rows.Clear();
            OCSecNor.Clear();
            OCSecExc.Clear();
            DTBAutor.Rows.Clear();
            DTBObjet.Rows.Clear();
            DTBFuent.Rows.Clear();
            DTBRequi.Rows.Clear();
            DTBActor.Rows.Clear();
        }
        public int Guardar()
        {
            int Estado, Paquete;
            if (BoolEst == true) Estado = 1; else Estado = 0;
            Paquete = (int)ClsBaseDatos.BDDouble("Select Id from Paquetes where Nombre = '" + StrPaq + "';");

            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update ReqFun Set Nombre = '" + StrNom + "', Descripcion = '" + StrDesc + "', Paquete = " + Paquete + ", PreCond = '" + StrPreCond + "', PostCond = '" + StrPostCond + "', Complejidad = " + IntComp + ", Prioridad = " + IntPrio + ", Urgencia = " + IntUrge + ", Estabilidad = " + IntEsta + ", Estado = " + Estado + ", Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
                ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + IntId + ";");
                if (GuardarTablas(IntId) == -1)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into ReqFun(Nombre,Descripcion,Paquete,Precond,Postcond,Complejidad,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + StrNom + "','" + StrDesc + "'," + Paquete + ",'" + StrPreCond + "','" + StrPostCond + "'," + IntComp + "," + IntPrio + "," + IntUrge + "," + IntEsta + "," + Estado + "," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqFun order by Id Desc;")) == -1)
                    return -2;
            }
            return 0;
        }
        private int GuardarTablas(int Id)
        {
            DataRow Fila;
            for (int i = 0; i <= (DTAutores.Rows.Count - 1); i++)
            {
                Fila = DTAutores.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTFuentes.Rows.Count - 1); i++)
            {
                Fila = DTFuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTObjetivos.Rows.Count - 1); i++)
            {
                Fila = DTObjetivos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTRequesitos.Rows.Count - 1); i++)
            {
                Fila = DTRequesitos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqReqR(IdReqr, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTActores.Rows.Count - 1); i++)
            {
                Fila = DTActores.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqAct(IdAct, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }

            foreach (ClsDatDG Dato in OCSecNor)
            {
                if (ClsBaseDatos.BDBool("Insert into ReqSecNor(IdReq, Descrip) values (" + Id + ",'" + Dato.Descrip + "');") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }

            foreach (ClsDatDG Dato in OCSecExc)
            {
                if (ClsBaseDatos.BDBool("Insert into ReqSecExc(IdReq, Descrip) values (" + Id + ",'" + Dato.Descrip + "');") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + Id + ";");
                    return -1;
                }
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("Delete from ReqAct where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecNor where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqSecExc where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 3 and IdReqR = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 3 and IdReqR = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqFun where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from ReqFun where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id, int TipoReq)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqFun where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Requisito[0].ToString());
            StrNom = Requisito[1].ToString();
            StrDesc = Requisito[2].ToString();
            StrPaq = ClsBaseDatos.BDString("Select Nombre from Paquetes where Id = " + Requisito[3].ToString() + ";");
            StrPreCond = Requisito[4].ToString();
            StrPostCond = Requisito[5].ToString();
            IntComp = int.Parse(Requisito[6].ToString());
            IntPrio = int.Parse(Requisito[7].ToString());
            IntUrge = int.Parse(Requisito[8].ToString());
            IntEsta = int.Parse(Requisito[9].ToString());
            if ((int)Requisito[10] == 1) BoolEst = true; else BoolEst = false;
            IntCate = int.Parse(Requisito[11].ToString());
            StrComen = Requisito[12].ToString();

            DTAutores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqAuto r where g.Id = r.IdAutor and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqFuen r where g.Id = r.IdFuen and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqObj r where o.Id = r.IdObj and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTActores = ClsBaseDatos.BDTable("Select a.Id as Id, a.Nombre as Nombre from Actores a, ReqAct r where a.Id = r.IdAct and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");

            DataTable TablaAux; DataRow Fila; OCSecNor.Clear(); OCSecExc.Clear();

            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqSecNor where IdReq = " + IntId + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                OCSecNor.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqSecExc where IdReq = " + IntId + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                OCSecExc.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            DTRequesitos.Rows.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 2 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 3 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);

            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqObj where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqAuto where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBFuent = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqFuen where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBActor = ClsBaseDatos.BDTable("Select Id,Nombre from Actores where Id not IN (select IdAct from ReqAct where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");

            CargarTablaReqRel(TipoReq);
        }
        public void CargarTablas()
        {
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBFuent = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun Order By Categoria Desc, Nombre;");
            DTBActor = ClsBaseDatos.BDTable("Select Id,Nombre from Actores Order By Categoria Desc, Nombre;");
            DTBPaquete = ClsBaseDatos.BDTable("Select Nombre from Paquetes Order By Categoria Desc, Nombre;");

            DTAutores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqAuto r where g.Id = r.IdAutor and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqFuen r where g.Id = r.IdFuen and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqObj r where o.Id = r.IdObj and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTActores = ClsBaseDatos.BDTable("Select a.Id as Id, a.Nombre as Nombre from Actores a, ReqAct r where a.Id = r.IdAct and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
        }
        public void CargarTablaReqRel(int TipoReq)
        {
            if (TipoReq == 1)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqReqR where idReq = " + IntId + " and TipoReq = 1) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 2)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqReqR where idReq = " + IntId + " and TipoReq = 2) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 3)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqReqR where idReq = " + IntId + " and TipoReq = 3) and Id <> " + IntId + " Order By Categoria Desc, Nombre;");
        }
        private void CargarTablaReq(DataTable TablaAux)
        {
            DataRow Fila;
            DataRow FilaNueva;
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                FilaNueva = DTRequesitos.NewRow();
                FilaNueva[0] = Fila[0].ToString();
                FilaNueva[1] = Fila[1].ToString();
                FilaNueva[2] = Fila[2].ToString();
                DTRequesitos.Rows.Add(FilaNueva);
            }
        }
    }
}