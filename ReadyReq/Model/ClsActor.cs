using System.Data;

namespace ReadyReq.Model
{
    public class ClsActor
    {
        DataTable DTBuscar = new DataTable();
        int IntId;
        string StrNom;
        string StrDesc;
        int IntCompl;
        string StrDescCompl;
        int IntCate;
        string StrComen;
        DataTable DTBAutor = new DataTable();
        DataTable DTBFuentes = new DataTable();
        DataTable DTAutor = new DataTable();
        DataTable DTFuentes = new DataTable();
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
        public int Complejidad
        {
            get { return IntCompl; }
            set { IntCompl = value; }
        }
        public string DescComplejidad
        {
            get { return StrDescCompl; }
            set { StrDescCompl = value; }
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
        public DataTable BGrupo
        {
            get { return DTBAutor; }
            set { DTBAutor = value; }
        }
        public DataTable BFuentes
        {
            get { return DTBFuentes; }
            set { DTBFuentes = value; }
        }
        public void IniciarValores()
        {
            DTBuscar.Rows.Clear();
            IntId = 0;
            StrNom = string.Empty;
            StrDesc = string.Empty;
            IntCompl = 0;
            StrDescCompl = string.Empty;
            IntCate = 0;
            StrComen = string.Empty;
            DTBAutor.Rows.Clear();
            DTBFuentes.Rows.Clear();
            DTAutor.Rows.Clear();
            DTFuentes.Rows.Clear();
        }
        public int Guardar()
        {
            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update Actores Set Nombre = '" + StrNom + "', Descripcion = '" + StrDesc + "', Complejidad = " + IntCompl + ", DescComple = '" + StrDescCompl + "', Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + IntId + ";");
                if (GuardarTablas(IntId) == -1)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into Actores(Nombre,Descripcion,Complejidad,DescComple,Categoria,Comentario) values ('" + StrNom + "','" + StrDesc + "'," + IntCompl + ",'" + StrDescCompl + "'," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
                if (GuardarTablas((int)ClsBaseDatos.BDDouble("Select Id from Actores order by Id Desc;")) == -1)
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
                if (ClsBaseDatos.BDBool("Insert into ActAuto(IdAutor, IdAct) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from Actores where Id = " + Id + ";");
                    return -1;
                }
            }

            for (int i = 0; i <= (DTFuentes.Rows.Count - 1); i++)
            {
                Fila = DTFuentes.Rows[i];
                if (ClsBaseDatos.BDBool("Insert into ActFuen(IdFuen, IdAct) values (" + int.Parse(Fila[0].ToString()) + "," + Id + ");") == false)
                {
                    ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + Id + ";");
                    ClsBaseDatos.BDBool("Delete from Actores where Id = " + Id + ";");
                    return -1;
                }
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAct = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdAct = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqAct where IdAct = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from Actores where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from Actores where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id)
        {
            DataRow Actor = ClsBaseDatos.BDTable("Select * from Actores where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Actor[0].ToString());
            StrNom = Actor[1].ToString();
            StrDesc = Actor[2].ToString();
            IntCompl = int.Parse(Actor[3].ToString());
            StrDescCompl = Actor[4].ToString();
            IntCate = int.Parse(Actor[5].ToString());
            StrComen = Actor[6].ToString();
            DTAutor = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActAuto aa where g.Id = aa.IdAutor and aa.IdAct = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActFuen af where g.Id = af.IdFuen and af.IdAct = " + IntId + " Order By Categoria Desc, Nombre;");

            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdAutor from ActAuto where idAct = " + IntId + ") Order By Categoria Desc, Nombre;");
            DTBFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo where Id not IN (select IdFuen from ActFuen where idAct = " + IntId + ") Order By Categoria Desc, Nombre;");

        }
        public void CargarTablas()
        {
            DTBAutor = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTBFuentes = ClsBaseDatos.BDTable("Select Id,Nombre from Grupo Order By Categoria Desc, Nombre;");
            DTAutor = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActAuto aa where g.Id = aa.IdAutor and aa.IdAct = " + IntId + " Order By Categoria Desc, Nombre;");
            DTFuentes = ClsBaseDatos.BDTable("Select g.Id as Id, g.Nombre as Nombre from Grupo g, ActFuen af where g.Id = af.IdFuen and af.IdAct = " + IntId + " Order By Categoria Desc, Nombre;");
        }
    }
}