using System.Data;

namespace ReadyReq
{
    public class ClsGrupo
    {
        private DataTable DTBuscar = new DataTable();
        private int IntId;
        private string StrNom;
        private string StrOrga;
        private string StrRol;
        private bool BoolDesa;
        private int IntCate;
        private string StrComen;
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
        public string Organizacion
        {
            get { return StrOrga; }
            set { StrOrga = value; }
        }
        public string Rol
        {
            get { return StrRol; }
            set { StrRol = value; }
        }
        public bool Desarrollador
        {
            get { return BoolDesa; }
            set { BoolDesa = value; }
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
        public void IniciarValores()
        {
            IntId = 0;
            StrNom = string.Empty;
            StrOrga = string.Empty;
            StrRol = string.Empty;
            BoolDesa = false;
            IntCate = 1;
            StrComen = string.Empty;
            DTBuscar.Rows.Clear();
        }
        public int Guardar()
        {
            int Desarrollador;
            if (BoolDesa == true) Desarrollador = 1; else Desarrollador = 0;
            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update Grupo Set Nombre = '" + StrNom + "', Organizacion = '" + StrOrga + "', Rol = '" + StrRol + "', Desarrollador = " + Desarrollador + ", Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into Grupo(Nombre,Organizacion,Rol,Desarrollador,Categoria,Comentario) values ('" + StrNom + "','" + StrOrga + "','" + StrRol + "'," + Desarrollador + "," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("Delete from ObjAuto where IdAutor = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ObjFuen where IdFuen = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ActAuto where IdAutor = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ActFuen where IdFuen = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqAuto where IdAutor = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqFuen where IdFuen = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIAuto where IdAutor = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqIFuen where IdFuen = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNAuto where IdAutor = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from ReqNFuen where IdFuen = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from Grupo where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from Grupo where Nombre LIKE '%" + valor + "%' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id)
        {
            DataRow Trabajador = ClsBaseDatos.BDTable("Select * from Grupo where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Trabajador[0].ToString());
            StrNom = Trabajador[1].ToString();
            StrOrga = Trabajador[2].ToString();
            StrRol = Trabajador[3].ToString();
            if ((int)Trabajador[4] == 1) BoolDesa = true; else BoolDesa = false;
            IntCate = int.Parse(Trabajador[5].ToString());
            StrComen = Trabajador[6].ToString();
        }
    }
}