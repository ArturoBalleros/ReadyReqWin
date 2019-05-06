using System;
using System.Collections;
using System.Data;

namespace ReadyReq.Model
{
    public class ClsWord
    {
        public static void Grupo(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila)
        {
            Microsoft.Office.Interop.Word.Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 5, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = fila[4].ToString();
            oTable.Cell(4, 2).Range.Text = fila[5].ToString();
            oTable.Cell(5, 2).Range.Text = fila[6].ToString();

            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Organization";
                oTable.Cell(3, 1).Range.Text = "Role";
                oTable.Cell(4, 1).Range.Text = "Is Developer";
                oTable.Cell(5, 1).Range.Text = "Commentary";
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Organización";
                oTable.Cell(3, 1).Range.Text = "Rol";
                oTable.Cell(4, 1).Range.Text = "Es Desarrollador";
                oTable.Cell(5, 1).Range.Text = "Comentario";
            }
            Negrita(oTable, 5);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        public static void Objetivos(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] SubObj)
        {
            Microsoft.Office.Interop.Word.Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 10, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(SubObj);
            oTable.Cell(6, 2).Range.Text = fila[4].ToString();
            oTable.Cell(7, 2).Range.Text = fila[5].ToString();
            oTable.Cell(8, 2).Range.Text = fila[6].ToString();
            oTable.Cell(9, 2).Range.Text = fila[7].ToString();
            oTable.Cell(10, 2).Range.Text = fila[8].ToString();
            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Description";
                oTable.Cell(3, 1).Range.Text = "Authors";
                oTable.Cell(4, 1).Range.Text = "Sources";
                oTable.Cell(5, 1).Range.Text = "Subobjectives";
                oTable.Cell(6, 1).Range.Text = "Priority";
                oTable.Cell(7, 1).Range.Text = "Urgency";
                oTable.Cell(8, 1).Range.Text = "Stability";
                oTable.Cell(9, 1).Range.Text = "State";
                oTable.Cell(10, 1).Range.Text = "Commentary";
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Descripción";
                oTable.Cell(3, 1).Range.Text = "Autores";
                oTable.Cell(4, 1).Range.Text = "Fuentes";
                oTable.Cell(5, 1).Range.Text = "Subobjetivos";
                oTable.Cell(6, 1).Range.Text = "Prioridad";
                oTable.Cell(7, 1).Range.Text = "Urgencia";
                oTable.Cell(8, 1).Range.Text = "Estabilidad";
                oTable.Cell(9, 1).Range.Text = "Estado";
                oTable.Cell(10, 1).Range.Text = "Comentario";
            }
            Negrita(oTable, 10);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        public static void Actores(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen)
        {
            Microsoft.Office.Interop.Word.Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 6, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = fila[4].ToString() + " (" + fila[5].ToString() + ")";
            oTable.Cell(6, 2).Range.Text = fila[6].ToString();
            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Description";
                oTable.Cell(3, 1).Range.Text = "Authors";
                oTable.Cell(4, 1).Range.Text = "Sources";
                oTable.Cell(5, 1).Range.Text = "Complexity";
                oTable.Cell(6, 1).Range.Text = "Commentary";
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Descripción";
                oTable.Cell(3, 1).Range.Text = "Autores";
                oTable.Cell(4, 1).Range.Text = "Fuentes";
                oTable.Cell(5, 1).Range.Text = "Complejidad";
                oTable.Cell(6, 1).Range.Text = "Comentario";
            }
            Negrita(oTable, 6);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        public static void ReqNFun(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req)
        {
            Microsoft.Office.Interop.Word.Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 10, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, "ArrayList");
            oTable.Cell(7, 2).Range.Text = fila[4].ToString();
            oTable.Cell(8, 2).Range.Text = fila[5].ToString();
            oTable.Cell(9, 2).Range.Text = fila[6].ToString();
            oTable.Cell(10, 2).Range.Text = fila[7].ToString();
            oTable.Cell(11, 2).Range.Text = fila[8].ToString();
            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Description";
                oTable.Cell(3, 1).Range.Text = "Authors";
                oTable.Cell(4, 1).Range.Text = "Sources";
                oTable.Cell(5, 1).Range.Text = "Related objectives";
                oTable.Cell(6, 1).Range.Text = "Related requirements";
                oTable.Cell(7, 1).Range.Text = "Priority";
                oTable.Cell(8, 1).Range.Text = "Urgency";
                oTable.Cell(9, 1).Range.Text = "Stability";
                oTable.Cell(10, 1).Range.Text = "State";
                oTable.Cell(11, 1).Range.Text = "Commentary";
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Descripción";
                oTable.Cell(3, 1).Range.Text = "Autores";
                oTable.Cell(4, 1).Range.Text = "Fuentes";
                oTable.Cell(5, 1).Range.Text = "Objetivos relacionados";
                oTable.Cell(6, 1).Range.Text = "Requisitos relacionados";
                oTable.Cell(7, 1).Range.Text = "Prioridad";
                oTable.Cell(8, 1).Range.Text = "Urgencia";
                oTable.Cell(9, 1).Range.Text = "Estabilidad";
                oTable.Cell(10, 1).Range.Text = "Estado";
                oTable.Cell(11, 1).Range.Text = "Comentario";
            }
            Negrita(oTable, 10);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        public static void ReqInfo(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataTable DatEsp)
        {
            Microsoft.Office.Interop.Word.Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 14, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, "ArrayList");
            oTable.Cell(7, 2).Range.Text = GeneraInfo(DatEsp, "DataTable");
            oTable.Cell(10, 2).Range.Text = fila[8].ToString();
            oTable.Cell(11, 2).Range.Text = fila[9].ToString();
            oTable.Cell(12, 2).Range.Text = fila[10].ToString();
            oTable.Cell(13, 2).Range.Text = fila[11].ToString();
            oTable.Cell(14, 2).Range.Text = fila[12].ToString();
            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Description";
                oTable.Cell(3, 1).Range.Text = "Authors";
                oTable.Cell(4, 1).Range.Text = "Sources";
                oTable.Cell(5, 1).Range.Text = "Related objectives";
                oTable.Cell(6, 1).Range.Text = "Related requirements";
                oTable.Cell(7, 1).Range.Text = "Specific dates";
                oTable.Cell(8, 1).Range.Text = "Time of life";
                oTable.Cell(9, 1).Range.Text = "Occurrences";
                oTable.Cell(10, 1).Range.Text = "Priority";
                oTable.Cell(11, 1).Range.Text = "Urgency";
                oTable.Cell(12, 1).Range.Text = "Stability";
                oTable.Cell(13, 1).Range.Text = "State";
                oTable.Cell(14, 1).Range.Text = "Commentary";
                oTable.Cell(8, 2).Range.Text = "Medium: " + fila[4].ToString() + " Maximum: " + fila[5].ToString();
                oTable.Cell(9, 2).Range.Text = "Medium: " + fila[6].ToString() + " Maximum: " + fila[7].ToString();
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Descripción";
                oTable.Cell(3, 1).Range.Text = "Autores";
                oTable.Cell(4, 1).Range.Text = "Fuentes";
                oTable.Cell(5, 1).Range.Text = "Objetivos relacionados";
                oTable.Cell(6, 1).Range.Text = "Requisitos relacionados";
                oTable.Cell(7, 1).Range.Text = "Datos específicos";
                oTable.Cell(8, 1).Range.Text = "Tiempo de vida";
                oTable.Cell(9, 1).Range.Text = "Ocurrencias";
                oTable.Cell(10, 1).Range.Text = "Prioridad";
                oTable.Cell(11, 1).Range.Text = "Urgencia";
                oTable.Cell(12, 1).Range.Text = "Estabilidad";
                oTable.Cell(13, 1).Range.Text = "Estado";
                oTable.Cell(14, 1).Range.Text = "Comentario";
                oTable.Cell(8, 2).Range.Text = "Medio: " + fila[4].ToString() + " Máximo: " + fila[5].ToString();
                oTable.Cell(9, 2).Range.Text = "Medio: " + fila[6].ToString() + " Máximo: " + fila[7].ToString();
            }
            Negrita(oTable, 14);
            oTable.Columns[1].Width = oWord.InchesToPoints(1.25f);
            oTable.Columns[2].Width = oWord.InchesToPoints(4.75f);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        public static void ReqFun(Microsoft.Office.Interop.Word.Application oWord, Microsoft.Office.Interop.Word.Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataRow[] Act, DataTable SecNor, DataTable SecExc)
        {
            Microsoft.Office.Interop.Word.Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks["\\endofdoc"].Range, 16, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, "ArrayList");
            oTable.Cell(7, 2).Range.Text = GeneraInfo(Act);
            oTable.Cell(8, 2).Range.Text = fila[5].ToString();
            oTable.Cell(9, 2).Range.Text = GeneraInfo(SecNor, "DataTable");
            oTable.Cell(10, 2).Range.Text = fila[6].ToString();
            oTable.Cell(11, 2).Range.Text = GeneraInfo(SecExc, "DataTable");
            oTable.Cell(12, 2).Range.Text = fila[7].ToString();
            oTable.Cell(13, 2).Range.Text = fila[8].ToString();
            oTable.Cell(14, 2).Range.Text = fila[9].ToString();
            oTable.Cell(15, 2).Range.Text = fila[10].ToString();
            oTable.Cell(16, 2).Range.Text = fila[11].ToString();
            if (ClsConf.Idioma == "Ingles")
            {
                oTable.Cell(2, 1).Range.Text = "Description";
                oTable.Cell(3, 1).Range.Text = "Authors";
                oTable.Cell(4, 1).Range.Text = "Sources";
                oTable.Cell(5, 1).Range.Text = "Related objectives";
                oTable.Cell(6, 1).Range.Text = "Related requirements";
                oTable.Cell(7, 1).Range.Text = "Actors";
                oTable.Cell(8, 1).Range.Text = "Precondition";
                oTable.Cell(9, 1).Range.Text = "Normal sequence";
                oTable.Cell(10, 1).Range.Text = "Postcondition";
                oTable.Cell(11, 1).Range.Text = "Sequence of exceptions";
                oTable.Cell(12, 1).Range.Text = "Priority";
                oTable.Cell(13, 1).Range.Text = "Urgency";
                oTable.Cell(14, 1).Range.Text = "Stability";
                oTable.Cell(15, 1).Range.Text = "State";
                oTable.Cell(16, 1).Range.Text = "Commentary";
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = "Descripción";
                oTable.Cell(3, 1).Range.Text = "Autores";
                oTable.Cell(4, 1).Range.Text = "Fuentes";
                oTable.Cell(5, 1).Range.Text = "Objetivos relacionados";
                oTable.Cell(6, 1).Range.Text = "Requisitos relacionados";
                oTable.Cell(7, 1).Range.Text = "Actores";
                oTable.Cell(8, 1).Range.Text = "Precondición";
                oTable.Cell(9, 1).Range.Text = "Secuencia normal";
                oTable.Cell(10, 1).Range.Text = "Postcondición";
                oTable.Cell(11, 1).Range.Text = "Secuencia de excepciones";
                oTable.Cell(12, 1).Range.Text = "Prioridad";
                oTable.Cell(13, 1).Range.Text = "Urgencia";
                oTable.Cell(14, 1).Range.Text = "Estabilidad";
                oTable.Cell(15, 1).Range.Text = "Estado";
                oTable.Cell(16, 1).Range.Text = "Comentario";
            }
            Negrita(oTable, 16);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
        }
        private static string GeneraInfo(object o, string Tipo = "DataRow")
        {
            string resultado = String.Empty;
            if (Tipo == "DataRow")
            {
                DataRow[] Filas = (DataRow[])o;
                if (Filas.Length > 0)
                {
                    foreach (DataRow fila in Filas)
                        resultado += fila[0].ToString() + " " + fila[2] + " (" + fila[3].ToString() + ")\n";
                    return resultado.Substring(0, resultado.Length - 1);
                }
                else return "-";
            }
            else if (Tipo == "ArrayList")
            {
                ArrayList Array = (ArrayList)o;
                if (Array.Count > 0)
                {
                    foreach (DataRow req in Array)
                        resultado += req[0].ToString() + " " + req[2].ToString() + "\n";
                    return resultado.Substring(0, resultado.Length - 1);
                }
                else return "-";
            }
            else
            {
                DataTable tabla = (DataTable)o;
                DataRow fila;
                if (tabla.Rows.Count > 0)
                {
                    for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                    {
                        fila = tabla.Rows[i];
                        resultado += (i + 1) + ": " + fila[0].ToString() + "\n";
                    }
                    return resultado.Substring(0, resultado.Length - 1);
                }
                else return "-";
            }
        }
        private static void Negrita(Microsoft.Office.Interop.Word.Table oTable, int num)
        {
            oTable.Cell(1, 2).Range.Font.Bold = 1;
            for (int i = 0; i < num; i++)
                oTable.Cell(i + 1, 1).Range.Font.Bold = 1;
        }
        public static void RellenarFila(Microsoft.Office.Interop.Excel.Range aRange, DataTable tabla, int nFila, string Req)
        {
            DataRow fila;
            aRange.Cells[nFila, 1].Value2 = Req;//IRQ/UC..
            for (int j = 0; j <= (tabla.Rows.Count - 1); j++)
            {
                fila = tabla.Rows[j];
                aRange.Cells[nFila, int.Parse(fila[0].ToString()) + 1].Value2 = "X";
            }
        }
        public static DataTable ObjRelacionados(DataTable DTObjRel, DataTable DTObj)
        {
            DataTable tabla = new DataTable(); DataRow fila, fila2, filaN;
            tabla.Columns.Add("Pos", typeof(Double));

            for (int i = 0; i <= (DTObjRel.Rows.Count - 1); i++)
            {
                fila = DTObjRel.Rows[i];
                for (int j = 0; j <= (DTObj.Rows.Count - 1); j++)
                {
                    fila2 = DTObj.Rows[j];
                    if (fila[0].ToString() == fila2[1].ToString())
                    {
                        filaN = tabla.NewRow();
                        filaN[0] = j + 1;
                        tabla.Rows.Add(filaN);
                        break;
                    }
                }
            }


            return tabla;
        }
        public static Microsoft.Office.Interop.Excel.Range DarFormatoExcel(Microsoft.Office.Interop.Excel.Worksheet ws, DataTable tabla, int numObj)
        {
            Microsoft.Office.Interop.Excel.Range aRange;

            //Bordes
            aRange = ws.get_Range("A1", letraColumna(numObj) + (tabla.Rows.Count + 1));
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //Fila uno
            aRange = ws.get_Range("A1", letraColumna(numObj) + 1);
            aRange.Interior.ColorIndex = 15;
            //Columna uno
            aRange = ws.get_Range("A1", "A" + (tabla.Rows.Count + 1));
            aRange.Interior.ColorIndex = 15;
            if (ClsConf.Idioma == "Ingles") aRange.Cells[1, 1].Value2 = "Objectives"; else aRange.Cells[1, 1].Value2 = "Objetivos";
            aRange.ColumnWidth = 10;
            //Formato columnas 2...
            aRange = ws.get_Range("B1", letraColumna(numObj) + (tabla.Rows.Count + 1));
            aRange.ColumnWidth = 3;
            aRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
            //Cabeceras columnas 2...
            aRange = ws.get_Range("B1", letraColumna(numObj) + 1);
            for (int i = 1; (i <= numObj - 1); i++)
                aRange.Cells[1, i].Value2 = i;
            //Filas 2...
            aRange = ws.get_Range("A2", letraColumna(numObj) + (tabla.Rows.Count + 1));
            return aRange;
        }
        private static string letraColumna(int nCol)
        {
            int n1, n2;
            if (nCol > 26)
            {
                n2 = nCol; n1 = 0;
                while (n2 > 26) { n1 = (n1 + 1); n2 = (n2 - 26); }
                return ((char)(n1 + 64)).ToString() + ((char)(n2 + 64)).ToString();
            }
            else
                return ((char)(nCol + 64)).ToString();
        }
    }
}