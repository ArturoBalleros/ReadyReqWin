using System.Data;

namespace ReadyReq
{
    public class ClsReqNFun
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
        DataTable DTAutores = new DataTable();
        DataTable DTFuentes = new DataTable();
        DataTable DTObjetivos = new DataTable();
        DataTable DTRequesitos = new DataTable();
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
            DTBAutor.Rows.Clear();
            DTBObjet.Rows.Clear();
            DTBFuent.Rows.Clear();
            DTBRequi.Rows.Clear();
            DTAutores.Rows.Clear();
            DTFuentes.Rows.Clear();
            DTObjetivos.Rows.Clear();
            DTRequesitos.Rows.Clear();
        }
        public int Guardar()
        {
            int Estado;
            if (BoolEst == true) Estado = 1; else Estado = 0;

            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update ReqNFunc Set Nombre = '" + StrNom + "',Descripcion = '" + StrDesc + "', Prioridad = " + IntPrio + ", Urgencia = " + IntUrge + ", Estabilidad = " + IntEsta + ", Estado = " + Estado + ", Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + IntId + ";");
                if (GuardarTablas(IntId) == -1)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into ReqNFunc(Nombre,Descripcion,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + StrNom + "','" + StrDesc + "'," + IntPrio + "," + IntUrge + "," + IntEsta + "," + Estado + "," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from ReqNFunc order by Id Desc;")) == -1)
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
                if (ClsBaseDatos.BDBool("Insert into ReqNAuto(IdAutor, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTFuentes.Rows.Count - 1); i++)
            {
                Fila = DTFuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqNFuen(IdFuen, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTObjetivos.Rows.Count - 1); i++)
            {
                Fila = DTObjetivos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqNObj(IdObj, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTRequesitos.Rows.Count - 1); i++)
            {
                Fila = DTRequesitos.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ReqNReqR(IdReqr, TipoReq, IdReq) values (" + int.Parse(Fila[0].ToString()) + "," + int.Parse(Fila[1].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 2 and IdReqR = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + Id + ";");
                    return -1;
                }
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNReqR where IdReq = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIReqR where TipoReq = 2 and IdReqR = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqReqR where TipoReq = 2 and IdReqR = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFunc where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from ReqNFunc where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id, int TipoReq)
        {
            DataRow Requisito = ClsBaseDatos.BDTable("Select * from ReqNFunc where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Requisito[0].ToString());
            StrNom = Requisito[1].ToString();
            StrDesc = Requisito[2].ToString();
            IntPrio = int.Parse(Requisito[3].ToString());
            IntUrge = int.Parse(Requisito[4].ToString());
            IntEsta = int.Parse(Requisito[5].ToString());
            if ((int)Requisito[6] == 1) BoolEst = true; else BoolEst = false;
            IntCate = int.Parse(Requisito[7].ToString());
            StrComen = Requisito[8].ToString();

            DTAutores = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNAuto r where g.Id = r.IdAutor and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ReqNFuen r where g.Id = r.IdFuen and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " +IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");
            DTRequesitos.Rows.Clear();

            DataTable TablaAux;
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqInfo rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 1 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 2 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);
            TablaAux = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqFun rn, ReqNReqR r where rn.Id = r.IdReqr and r.IdReq = " + IntId + " and r.TipoReq = 3 Order By Categoria Desc, Nombre;");
            CargarTablaReq(TablaAux);

            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idObj from ReqNObj where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ReqNAuto where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBFuent = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ReqNFuen where idReq = " + IntId + ") Order By Categoria Desc, Nombre;");

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
            DTObjetivos = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, ReqNObj r where o.Id = r.IdObj and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
            DTRequesitos = ClsBaseDatos.BDTable("Select rn.Id as Id, r.TipoReq as Tipo, rn.Nombre as Nombre from ReqNFunc rn, ReqNReqR r where rn.Id =  r.IdReqr and r.IdReq = " + IntId + " Order By Categoria Desc, Nombre;");
        }
        public void CargarTablaReqRel(int TipoReq)
        {
            if (TipoReq == 1)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqInfo where Id not IN (select IdReqr from ReqNReqR where idReq = " + IntId + " and TipoReq = 1) Order By Categoria Desc, Nombre;");
            else if (TipoReq == 2)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqNFunc where Id not IN (select IdReqr from ReqNReqR where idReq = " + IntId + " and TipoReq = 2) and Id <> " + IntId + " Order By Categoria Desc, Nombre;");
            else if (TipoReq == 3)
                DTBRequi = ClsBaseDatos.BDTable("Select Id,Nombre from ReqFun where Id not IN (select IdReqr from ReqNReqR where idReq = " + IntId + " and TipoReq = 3) Order By Categoria Desc, Nombre;");
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