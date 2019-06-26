/*
 * Autor: Arturo Balleros Albillo
 */
using MySql.Data.MySqlClient;
using ReadyReq.Util;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows;

namespace ReadyReq.Model
{
    public static class ClsBaseDatos
    {
        public static bool BDConexion(string OptionCon = Ingles.No, string OpcionBD = Ingles.No)
        {
            string TipoBD = OpcionBD.Equals(Ingles.No) ? ClsConf.TipoBD : OpcionBD;
            if (TipoBD.Equals(DefValues.MySql)) return OptionCon.Equals(Ingles.No) ? SqlConexion(ClsConf.BuilderMySql.ToString()) : SqlConexion(OptionCon);
            else return OptionCon.Equals(Ingles.No) ? AccConexion(ClsConf.ProvAcc + "Data Source=" + ClsConf.RutaAcc + "; " + ClsConf.PassAcc) : AccConexion(OptionCon);
        }
        public static bool BDBool(string OrdenSql, string OptionCon = Ingles.No, string OpcionBD = Ingles.No)
        {
            string TipoBD = OpcionBD.Equals(Ingles.No) ? ClsConf.TipoBD : OpcionBD;
            if (TipoBD.Equals(DefValues.MySql)) return OptionCon.Equals(Ingles.No) ? SqlBool(OrdenSql, ClsConf.BuilderMySql.ToString()) : SqlBool(OrdenSql, OptionCon);
            else return OptionCon.Equals(Ingles.No) ? AccBool(OrdenSql, ClsConf.ProvAcc + "Data Source=" + ClsConf.RutaAcc + "; " + ClsConf.PassAcc) : AccBool(OrdenSql, OptionCon);
        }
        public static DataTable BDTable(string OrdenSql, string OptionCon = Ingles.No, string OpcionBD = Ingles.No)
        {
            string TipoBD = OpcionBD.Equals(Ingles.No) ? ClsConf.TipoBD : OpcionBD;

            if (TipoBD.Equals(DefValues.MySql))
            {
                if (OptionCon.Equals(Ingles.No)) return SqlTable(OrdenSql, ClsConf.BuilderMySql.ToString());
                else return SqlTable(OrdenSql, OptionCon);
            }
            else
            {
                if (OptionCon.Equals(Ingles.No)) return AccTable(OrdenSql, ClsConf.ProvAcc + "Data Source=" + ClsConf.RutaAcc + "; " + ClsConf.PassAcc);
                else return AccTable(OrdenSql, OptionCon);
            }
        }
        public static string BDString(string OrdenSql, string OptionCon = Ingles.No, string OpcionBD = Ingles.No)
        {
            string TipoBD = OpcionBD.Equals(Ingles.No) ? ClsConf.TipoBD : OpcionBD;

            if (TipoBD.Equals(DefValues.MySql))
            {
                if (OptionCon.Equals(Ingles.No)) return SqlString(OrdenSql, ClsConf.BuilderMySql.ToString());
                else return SqlString(OrdenSql, OptionCon);
            }
            else
            {
                if (OptionCon.Equals(Ingles.No)) return AccString(OrdenSql, ClsConf.ProvAcc + "Data Source=" + ClsConf.RutaAcc + "; " + ClsConf.PassAcc);
                else return AccString(OrdenSql, OptionCon);
            }
        }
        public static double BDDouble(string OrdenSql, string OptionCon = Ingles.No, string OpcionBD = Ingles.No)
        {
            string TipoBD = OpcionBD.Equals(Ingles.No) ? ClsConf.TipoBD : OpcionBD;

            if (TipoBD.Equals(DefValues.MySql))
            {
                if (OptionCon.Equals(Ingles.No)) return SqlDouble(OrdenSql, ClsConf.BuilderMySql.ToString());
                else return SqlDouble(OrdenSql, OptionCon);
            }
            else
            {
                if (OptionCon.Equals(Ingles.No)) return AccDouble(OrdenSql, ClsConf.ProvAcc + "Data Source=" + ClsConf.RutaAcc + "; " + ClsConf.PassAcc);
                else return AccDouble(OrdenSql, OptionCon);
            }
        }
        private static bool AccConexion(string builder)
        {
            OleDbConnection ConexionBaseDatos = new OleDbConnection();
            try
            {
                ConexionBaseDatos.ConnectionString = builder;
                ConexionBaseDatos.Open();
                ConexionBaseDatos.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static bool AccBool(string OrdenSql, string builder)
        {
            OleDbCommand ComandoBD = new OleDbCommand();
            OleDbConnection ConexionBaseDatos = new OleDbConnection(builder);
            try
            {
                ConexionBaseDatos.Open();
                ComandoBD.Connection = ConexionBaseDatos;
                ComandoBD.CommandText = OrdenSql;
                if (ComandoBD.ExecuteNonQuery() == 0)
                {
                    ConexionBaseDatos.Close();
                    return false;
                }
                else
                {
                    ConexionBaseDatos.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error en la funcion AccBool: " + ex.Message);
                return false;
            }
        }
        private static DataTable AccTable(string OrdenSql, string builder)
        {
            OleDbCommand ComandoBD = new OleDbCommand();
            OleDbConnection ConexionBaseDatos = new OleDbConnection(builder);
            DataTable DataTab = new DataTable();
            try
            {
                ConexionBaseDatos.Open();
                ComandoBD.Connection = ConexionBaseDatos;
                ComandoBD.CommandText = OrdenSql;
                OleDbDataAdapter DataAdp = new OleDbDataAdapter(ComandoBD);
                DataAdp.Fill(DataTab);
                ConexionBaseDatos.Close();
                return DataTab;
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                if (ex.HResult != -2146233079) MessageBox.Show("Error en la funcion AccTable: " + ex.Message);
                return null;
            }
        }
        private static string AccString(string OrdenSql, string builder)
        {
            OleDbCommand ComandoBD = new OleDbCommand();
            OleDbConnection ConexionBaseDatos = new OleDbConnection(builder);
            string Resultado;
            ComandoBD.CommandText = OrdenSql;
            ComandoBD.Connection = ConexionBaseDatos;
            ConexionBaseDatos.Open();
            try
            {
                try
                {
                    Resultado = ComandoBD.ExecuteScalar().ToString();
                }
                catch
                {
                    Resultado = string.Empty;
                }
                ConexionBaseDatos.Close();
                return Resultado;
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error en la funcion AccString: " + ex.Message);
                return null;
            }
        }
        private static double AccDouble(string OrdenSql, string builder)
        {
            OleDbCommand ComandoBD = new OleDbCommand();
            OleDbConnection ConexionBaseDatos = new OleDbConnection(builder);
            double Resultado;
            ComandoBD.CommandText = OrdenSql;
            ComandoBD.Connection = ConexionBaseDatos;
            ConexionBaseDatos.Open();
            try
            {
                try
                {
                    Resultado = double.Parse(ComandoBD.ExecuteScalar().ToString());
                }
                catch
                {
                    Resultado = -1;
                }

                ConexionBaseDatos.Close();
                return Resultado;

            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error en la funcion AccDouble: " + ex.Message);
                return -1;
            }
        }
        private static bool SqlConexion(string builder)
        {
            MySqlConnection ConexionBaseDatos = new MySqlConnection(builder);
            try
            {
                ConexionBaseDatos.ConnectionString = builder;
                ConexionBaseDatos.Open();
                ConexionBaseDatos.Close();
                return true;
            }
            catch
            {
                ConexionBaseDatos.Close();
                return false;
            }
        }
        private static bool SqlBool(string OrdenSql, string builder)
        {
            MySqlConnection ConexionBaseDatos = new MySqlConnection(builder);
            MySqlCommand ComandoBD = ConexionBaseDatos.CreateCommand();
            try
            {
                ComandoBD.CommandText = OrdenSql;
                ConexionBaseDatos.Open();
                if (ComandoBD.ExecuteNonQuery() == 0)
                {
                    ConexionBaseDatos.Close();
                    return false;
                }
                else
                {
                    ConexionBaseDatos.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error SQLBool: " + ex.Message);
                return false;
            }
        }
        private static DataTable SqlTable(string OrdenSql, string builder)
        {
            MySqlConnection ConexionBaseDatos = new MySqlConnection(builder);
            MySqlCommand ComandoBD = ConexionBaseDatos.CreateCommand();
            DataTable DataTab = new DataTable();
            try
            {
                ComandoBD.CommandText = OrdenSql;
                ConexionBaseDatos.Open();
                MySqlDataAdapter DataAdp = new MySqlDataAdapter(ComandoBD);
                DataAdp.Fill(DataTab);
                ConexionBaseDatos.Close();
                return DataTab;
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error SQLTable: " + ex.Message);
                return null;
            }
        }
        private static string SqlString(string OrdenSql, string builder)
        {
            MySqlConnection ConexionBaseDatos = new MySqlConnection(builder);
            MySqlCommand ComandoBD = ConexionBaseDatos.CreateCommand();
            string Resultado;
            try
            {
                ComandoBD.CommandText = OrdenSql;
                ConexionBaseDatos.Open();
                try
                {
                    Resultado = ComandoBD.ExecuteScalar().ToString();
                }
                catch
                {
                    Resultado = string.Empty;
                }

                ConexionBaseDatos.Close();
                return Resultado;
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error SQLString: " + ex.Message);
                return String.Empty;
            }
        }
        private static double SqlDouble(string OrdenSql, string builder)
        {
            double Resultado;
            MySqlConnection ConexionBaseDatos = new MySqlConnection(builder);
            MySqlCommand ComandoBD = ConexionBaseDatos.CreateCommand();
            try
            {
                ComandoBD.CommandText = OrdenSql;
                ConexionBaseDatos.Open();
                try
                {
                    Resultado = double.Parse(ComandoBD.ExecuteScalar().ToString());
                }
                catch
                {
                    Resultado = -1;
                }

                ConexionBaseDatos.Close();
                return Resultado;
            }
            catch (Exception ex)
            {
                // Cierra base de datos y da el mensaje de error
                ConexionBaseDatos.Close();
                MessageBox.Show("Error SQLDouble: " + ex.Message);
                return -1;
            }
        }
    }
}