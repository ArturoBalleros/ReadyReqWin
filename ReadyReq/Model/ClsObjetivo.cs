using System.Data;

namespace ReadyReq.Model
{
    public class ClsObjetivo
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
        DataTable DTBAutor = new DataTable();
        DataTable DTBFuente = new DataTable();
        DataTable DTBObjet = new DataTable();
        DataTable DTAutor = new DataTable();
        DataTable DTFuentes = new DataTable();
        DataTable DTSubObj = new DataTable();
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
            get { return DTAutor; }
            set { DTAutor = value; }
        }
        public DataTable Fuentes
        {
            get { return DTFuentes; }
            set { DTFuentes = value; }
        }
        public DataTable Subobjetivos
        {
            get { return DTSubObj; }
            set { DTSubObj = value; }
        }
        public DataTable BTrabajadores
        {
            get { return DTBAutor; }
            set { DTBAutor = value; }
        }
        public DataTable BFuentes
        {
            get { return DTBFuente; }
            set { DTBFuente = value; }
        }
        public DataTable BObjetivos
        {
            get { return DTBObjet; }
            set { DTBObjet = value; }
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
            DTAutor.Rows.Clear();
            DTBFuente.Rows.Clear();
            DTFuentes.Rows.Clear();
            DTSubObj.Rows.Clear();
        }
        public int Guardar()
        {
            int Estado;
            if (BoolEst == true) Estado = 1; else Estado = 0;
            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update Objetivos Set Nombre = '" + StrNom + "', Descripcion = '" + StrDesc + "', Prioridad = " + IntPrio + ", Urgencia = " + IntUrge + ", Estabilidad = " + IntEsta + ", Estado = " + Estado + ", Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + IntId + ";");
                if (GuardarTablas(IntId) == -1)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into Objetivos(Nombre,Descripcion,Prioridad,Urgencia,Estabilidad,Estado,Categoria,Comentario) values ('" + StrNom + "','" + StrDesc + "'," + IntPrio + "," + IntUrge + "," + IntEsta + "," + Estado + "," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from Objetivos order by Id Desc;")) == -1)
                    return -2;
            }
            return 0;
        }
        private int GuardarTablas(int Id)
        {
            DataRow Fila;
            for (int i = 0; i <= (DTAutor.Rows.Count - 1); i++)
            {
                Fila = DTAutor.Rows[i];

                if (ClsBaseDatos.BDBool("Insert into ObjAuto(IdAutor, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTFuentes.Rows.Count - 1); i++)
            {
                Fila = DTFuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ObjFuen(IdFuen, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + Id + ";");
                    return -1;
                }
            }
            for (int i = 0; i <= (DTSubObj.Rows.Count - 1); i++)
            {
                Fila = DTSubObj.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ObjSubobj(IdSubobj, IdObj) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + Id + ";");
                    return -1;
                }
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("Delete from ObjSubobj where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIObj where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNObj where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqObj where IdObj = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from Objetivos where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from Objetivos where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id)
        {
            DataRow Objetivo = ClsBaseDatos.BDTable("Select * from Objetivos where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Objetivo[0].ToString());
            StrNom = Objetivo[1].ToString();
            StrDesc = Objetivo[2].ToString();
            IntPrio = int.Parse(Objetivo[3].ToString());
            IntUrge = int.Parse(Objetivo[4].ToString());
            IntEsta = int.Parse(Objetivo[5].ToString());
            if ((int)Objetivo[6] == 1) BoolEst = true; else BoolEst = false;
            IntCate = int.Parse(Objetivo[7].ToString());
            StrComen = Objetivo[8].ToString();

            DTAutor = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjAuto oa where g.Id = oa.IdAutor and oa.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjFuen obf where g.Id = obf.IdFuen and obf.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");
            DTSubObj = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, Objsubobj os where o.Id = os.IdSubObj and os.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");

            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ObjAuto where idObj = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBFuente = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ObjFuen where idObj = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos where Id not IN (select idSubobj from ObjSubobj where idObj = " + IntId + ") and Id <> " + IntId + " Order By Categoria Desc, Nombre;");
        }
        public void CargarTablas()
        {
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBFuente = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBObjet = ClsBaseDatos.BDTable("Select Id,Nombre from Objetivos Order By Categoria Desc, Nombre;");
            DTAutor = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjAuto oa where g.Id = oa.IdAutor and oa.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ObjFuen obf where g.Id = obf.IdFuen and obf.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");
            DTSubObj = ClsBaseDatos.BDTable("Select o.Id as Id, o.Nombre as Nombre from Objetivos o, Objsubobj os where o.Id = os.IdSubobj and os.IdObj = " + IntId + " Order By Categoria Desc, Nombre;");
        }
    }
}