using System;

namespace ReadyReq.Util
{
    public static class ClsFunciones
    {
        public static string FechaMySQL(DateTime fecha)
        {
            return fecha.ToString("yyyy/MM/dd");
        }

        public static double StringToDouble(string cadena)
        {
            return double.Parse(cadena.Replace(".", ","));
        }

        public static string DoubleToString(double numero)
        {
            return numero.ToString().Contains(",") ? numero.ToString().Replace(",", ".") : numero.ToString() + ".0";
        }

        public static bool TryConvertToDate(string fecha)
        {
            try { DateTime.ParseExact(fecha, "dd/MM/yyyy", null); return true; } catch { return false; }
        }

        public static bool TryConvertToDouble(string numero)
        {
            try { StringToDouble(numero); return true; } catch { return false; }
        }
    }
}
