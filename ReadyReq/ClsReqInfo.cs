using System.Collections.ObjectModel;
using System.Data;

namespace ReadyReq
{
    public class ClsReqInfo
    {
        DataTable DTBuscar = new DataTable();
        int IntId;
        string StrNom;
        string StrDesc;
        int IntPrio;
        int IntUrge;
        int IntEsta;
        bool BoolEst;
        int IntCate;
        string StrComen;
        int IntTM;
        int IntTMX;
        int IntOM;
        int IntOMX;
        DataTable DTAutores = new DataTable();
        DataTable DTFuentes = new DataTable();
        DataTable DTObjetivos = new DataTable();
        DataTable DTRequesitos = new DataTable();
        ObservableCollection<ClsDatDG> OCDatEsp = new ObservableCollection<ClsDatDG>();
        DataTable DTBAutor = new DataTable();
        DataTable DTBFuent = new DataTable();
        DataTable DTBObjet = new DataTable();
        DataTable DTBRequi = new DataTable();
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
        public int TiempoMedio
        {
            get { return IntTM; }
            set { IntTM = value; }
        }
        public int TiempoMaximo
        {
            get { return IntTMX; }
            set { IntTMX = value; }
        }
        public int OcurreMedio
        {
            get { return IntOM; }
            set { IntOM = value; }
        }
        public int OcurreMaximo
        {
            get { return IntOMX; }
            set { IntOMX = value; }
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
        public ObservableCollection<ClsDatDG> DatosEspeci
        {
            get { return OCDatEsp; }
            set { OCDatEsp = value; }
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
        public void IniciarValores()
        {
            DTBuscar.Rows.Clear();
            IntId = 0;
            StrNom = string.Empty;
            StrDesc = string.Empty;
            IntPrio = 0;
            IntUrge = 0;
            IntEsta = 0;
            BoolEst = true;
            IntCate = 0;
            StrComen = string.Empty;
            IntTM = 0;
            IntTMX = 0;
            IntOM = 0;
            IntOMX = 0;
            DTAutores.Rows.Clear();
            DTFuentes.Rows.Clear();
            DTObjetivos.Rows.Clear();
            DTRequesitos.Rows.Clear();
            OCDatEsp.Clear();
            DTBAutor.Rows.Clear();
            DTBObjet.Rows.Clear();
            DTBFuent.Rows.Clear();
            DTBRequi.Rows.Clear();
        }
        public int Guardar()
        {
            int Estado;
            if (BoolEst == true) Estado = 1; else Estado = 0;

            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update ReqInfo Set Nombre = '" + StrNom + "', Descripcion = '" + StrDesc + "', TiemMed = " + IntTM + ", TiemMax = " + IntTMX + ", OcuMed = " + IntOM + ", OcuMax = " + IntOMX + ", Prioridad = " + IntPrio + ", Urgencia = " + IntUrge + ", Estabilidad = " + IntEsta + ", Estado = " + Estado + ", Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
                ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIDatEsp where IdReq = " + IntId + ";");
                if (GuardarTablas(IntId) == -1)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into ReqInfo(Nombre,Descripcion,TiemMed,TiemMax,OcuMed,OcuMax,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + StrNom + "','" + StrDesc + "'," + IntTM + "," + IntTMX + "," + IntOM + "," + IntOMX + "," + IntPrio + "," + IntUrge + "," + IntEsta + "," + Estado + "," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqInfo order by Id Desc;")) == -1)
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
                if (ClsBaseDatos.BDBool("Insert into ReqIAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTFuentes.Rows.Count - 1); i++)
            {
                Fila = DTFuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; (i <= DTObjetivos.Rows.Count - 1); i++)
            {
                Fila = DTObjetivos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; (i <= DTRequesitos.Rows.Count - 1); i++)
            {
                Fila = DTRequesitos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqIReqR(IdReqR, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
                    return -1;
                }
            }
            foreach (ClsDatDG Dato in OCDatEsp)
            {
                if (ClsBaseDatos.BDBool("Insert into ReqIDatEsp(IdReq, Descrip) values (" + Id + ",'" + Dato.Descrip + "');") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + Id + ";");
                    return -1;
                }
            }
            return 0;
        }
        public void Borrar()
        {
            ClsBaseDatos.BDBool("Delete from ReqIAuto where IdReq = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqIFuen where IdReq = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqIObj where IdReq = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqIReqR where IdReq = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqIDatEsp where IdReq = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqNReqR where TipoReq = 1 and IdReqR = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 1 and IdReqR = " + IntId + ";");
            ClsBaseDatos.BDBool("Delete from ReqInfo where Id = " + IntId + ";");
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from ReqInfo where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id, int TipoReq)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqInfo where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Requisito[0].ToString());
            StrNom = Requisito[1].ToString();
            StrDesc = Requisito[2].ToString();
            IntTM = int.Parse(Requisito[3].ToString());
            IntTMX = int.Parse(Requisito[4].ToString());
            IntOM = int.Parse(Requisito[5].ToString());
            IntOMX = int.Parse(Requisito[6].ToString());
            IntPrio = int.Parse(Requisito[7].ToString());
            IntUrge = int.Parse(Requisito[8].ToString());
            IntEsta = int.Parse(Requisito[9].ToString());
            if ((int)Requisito[10] == 1) BoolEst = true; else BoolEst = false;
            IntCate = int.Parse(Requisito[11].ToString());
            StrComen = Requisito[12].ToString();

            DTAutores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqIAuto r where g.Id = r.IdAutor and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqIFuen r where g.Id = r.IdFuen and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqIObj r where o.Id = r.IdObj and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");

            DataTable TablaAux; DataRow Fila; OCDatEsp.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select Descrip from ReqIDatEsp where IdReq = " + IntId + ";");
            for (int i = 0; i <= (TablaAux.Rows.Count - 1); i++)
            {
                Fila = TablaAux.Rows[i];
                OCDatEsp.Add(new ClsDatDG() { Descrip = Fila[0].ToString() });
            }

            DTRequesitos.Rows.Clear();
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 2 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqIReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 3 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);

            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqIObj where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqIAuto where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBFuent = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqIFuen where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");

            CargarTablaReqRel(TipoReq);
        }
        public void CargarTablas()
        {
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBFuent = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun Order By Categoria Desc, Nombre;");

            DTAutores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNAuto r where g.Id = r.IdAutor and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNFuen r where g.Id = r.IdFuen and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " + +IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
        }
        public void CargarTablaReqRel(int TipoReq)
        {
            if (TipoReq == 1)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqIReqR where idReq = " + IntId + " and TipoReq = 1) and Id <> " + IntId + " Order By Categoria Desc, Nombre;");
            else if (TipoReq == 2)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqIReqR where idReq = " + IntId + " and TipoReq = 2) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 3)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqIReqR where idReq = " + IntId + " and TipoReq = 3) Order By Categoria Desc, Nombre;");
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