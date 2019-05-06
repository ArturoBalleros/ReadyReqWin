using System.Data;

namespace ReadyReq
{
    public class ClsPaq
    {
        private DataTable DTBuscar = new DataTable();
        private int IntId;
        private string StrNom;
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
            IntCate = 1;
            StrComen = string.Empty;
            DTBuscar.Rows.Clear();
        }
        public int Guardar()
        {
            if (IntId != 0)
            {
                if (ClsBaseDatos.BDBool("Update Paquetes Set Nombre = '" + StrNom + "', Categoria = " + IntCate + ", Comentario = '" + StrComen + "' where Id = " + IntId + ";") == false)
                    return -1;
            }
            else
            {
                if (ClsBaseDatos.BDBool("Insert into Paquetes(Nombre,Categoria,Comentario) values ('" + StrNom + "'," + IntCate + ",'" + StrComen + "');") == false)
                    return -2;
            }
            return 0;
        }
        public void Borrar()
        {
            if (IntId != 0)
            {
                ClsBaseDatos.BDBool("UPDATE ReqFun SET Paquete = " + ClsBaseDatos.BDString("Select Id from Paquetes Where Nombre = 'No Asignado';") + " Where Paquete = " + IntId + ";");
                ClsBaseDatos.BDBool("Delete from Paquetes where Id = " + IntId + ";");
                IniciarValores();
            }
        }
        public void Buscar(string valor)
        {
            DTBuscar = ClsBaseDatos.BDTable("Select Nombre,Id from Paquetes where Nombre LIKE '%" + valor + "%' and Nombre <> 'No Asignado' Order By Categoria Desc, Nombre;");
        }
        public void Cargar(int Id)
        {
            DataRow Paquete = ClsBaseDatos.BDTable("Select * from Paquetes where Id = " + Id + ";").Rows[0];
            IntId = int.Parse(Paquete[0].ToString());
            StrNom = Paquete[1].ToString();
            IntCate = int.Parse(Paquete[2].ToString());
            StrComen = Paquete[3].ToString();
        }
    }
}