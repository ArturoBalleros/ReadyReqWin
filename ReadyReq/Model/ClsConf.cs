using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace ReadyReq
{
    public static class ClsConf
    {
        static string MenErrFic = "Se produjo un error con la configuración, configure el programa correctamente para continuar.\n\nAn error occurred with the configuration, configure the program correctly to continue.";
        static string MenErrIdi = "Seleccione un idioma en 'Configuración', para continuar.\n\nSelect a language in 'Configuration', to continue.";
        static string MenErrBD = "Seleccione un tipo de base de datos en 'Configuración', para continuar.\n\nSelect a type of database in 'Configuration', to continue.";
        static string MenErrConBD = "Error al conectarse a la base de datos, corríjalo en 'Configuración', para continuar.\n\nError when connecting to the database, correct it in 'Configuration', to continue.";
        public static MySqlConnectionStringBuilder BuilderMySql = new MySqlConnectionStringBuilder();
        public static string Idioma { get; set; }
        public static string TipoBD { get; set; }
        public static string Host { get; set; }
        public static string Usuario { get; set; }
        public static string PassMySql { get; set; }
        public static string BDMySql { get; set; }
        public static string PortMySql { get; set; }
        public static string RutaAcc { get; set; }
        public static string ProvAcc { get; set; }
        public static bool FlgPass { get; set; }
        public static string PassAcc { get; set; }
        public static int LeerConf()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("Conf.RR");
                line = sr.ReadLine();
                Idioma = Desencriptar(line);
                line = sr.ReadLine();
                TipoBD = Desencriptar(line);
                if (TipoBD == "MySql")
                {
                    line = sr.ReadLine();
                    Host = Desencriptar(line);
                    line = sr.ReadLine();
                    Usuario = Desencriptar(line);
                    line = sr.ReadLine();
                    PassMySql = Desencriptar(line).ToString().Substring(0, Desencriptar(line).ToString().Length - 1);
                    line = sr.ReadLine();
                    BDMySql = Desencriptar(line);
                    line = sr.ReadLine();
                    PortMySql = Desencriptar(line);
                }
                else
                {
                    line = sr.ReadLine();
                    RutaAcc = Desencriptar(line);
                    line = sr.ReadLine();
                    ProvAcc = Desencriptar(line);
                    line = sr.ReadLine();
                    FlgPass = bool.Parse(Desencriptar(line));
                    line = sr.ReadLine();
                    PassAcc = Desencriptar(line).ToString().Substring(0, Desencriptar(line).ToString().Length - 1);
                    if (FlgPass == true)
                        PassAcc = "Jet OLEDB:Database Password =" + PassAcc + ";";
                }
                sr.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public static int EscribirConf()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Conf.RR");
                sw.WriteLine(Encriptar(Idioma));
                sw.WriteLine(Encriptar(TipoBD));
                if (TipoBD == "MySql")
                {
                    sw.WriteLine(Encriptar(Host));
                    sw.WriteLine(Encriptar(Usuario));
                    sw.WriteLine(Encriptar(PassMySql + "7"));
                    sw.WriteLine(Encriptar(BDMySql));
                    sw.WriteLine(Encriptar(PortMySql));
                }
                else
                {
                    sw.WriteLine(Encriptar(RutaAcc));
                    sw.WriteLine(Encriptar("Provider=Microsoft.ACE.OLEDB.12.0; "));
                    sw.WriteLine(Encriptar(FlgPass.ToString()));
                    if (FlgPass == true)
                    {
                        sw.WriteLine(Encriptar(PassAcc + "7"));
                        PassAcc = "Jet OLEDB:Database Password =" + PassAcc + ";";
                    }
                    else
                        sw.WriteLine(Encriptar("Persist Security Info = False;7"));
                }
                sw.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public static string Encriptar(string Encriptar)
        {
            string resultado = string.Empty;
            byte[] encriptado = Encoding.Unicode.GetBytes(Encriptar);
            resultado = Convert.ToBase64String(encriptado);
            return resultado;
        }
        public static string Desencriptar(string Desencriptar)
        {
            string resultado = string.Empty;
            byte[] desencriptado = Convert.FromBase64String(Desencriptar);
            resultado = Encoding.Unicode.GetString(desencriptado);
            return resultado;
        }
        public static void ConexionMySql()
        {
            BuilderMySql.Server = Host;
            BuilderMySql.UserID = Usuario;
            BuilderMySql.Password = PassMySql;
            BuilderMySql.Database = BDMySql;
            BuilderMySql.Port = uint.Parse(PortMySql);
        }
        public static bool Iniciar()
        {
            if (LeerConf() == -1) { MessageBox.Show(MenErrFic); return false; }
            else
            {
                if ((Idioma != "Español") && (Idioma != "Ingles")) { MessageBox.Show(MenErrIdi); return false; }
                else
                {
                    if (TipoBD != "MySql" && TipoBD != "Access") { MessageBox.Show(MenErrBD); return false; }
                    else
                    {
                        if (TipoBD == "MySql") ConexionMySql();
                        if (ClsBaseDatos.BDConexion() == false) { MessageBox.Show(MenErrConBD); return false; }
                        return true;
                    }
                }
            }
        }
    }
}