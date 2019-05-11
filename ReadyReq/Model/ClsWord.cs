﻿using Microsoft.Office.Interop.Word;
using ReadyReq.Util;
using System.Collections;
using System.Data;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadyReq.Model
{
    public sealed class ClsWord
    {
        public static void Grupo(Application oWord, Document oDoc, DataRow fila)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 5, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = fila[4].ToString();
            oTable.Cell(4, 2).Range.Text = fila[5].ToString();
            oTable.Cell(5, 2).Range.Text = fila[6].ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Organization;
                oTable.Cell(3, 1).Range.Text = Ingles.Role;
                oTable.Cell(4, 1).Range.Text = Ingles.IsDev;
                oTable.Cell(5, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Organización;
                oTable.Cell(3, 1).Range.Text = Español.Rol;
                oTable.Cell(4, 1).Range.Text = Español.Es_Des;
                oTable.Cell(5, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 5);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void Objetivos(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] SubObj)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 10, 2);
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
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Description;
                oTable.Cell(3, 1).Range.Text = Ingles.Authors;
                oTable.Cell(4, 1).Range.Text = Ingles.Sources;
                oTable.Cell(5, 1).Range.Text = Ingles.Subobjectives;
                oTable.Cell(6, 1).Range.Text = Ingles.Priority;
                oTable.Cell(7, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(8, 1).Range.Text = Ingles.Stability;
                oTable.Cell(9, 1).Range.Text = Ingles.State;
                oTable.Cell(10, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Descripción;
                oTable.Cell(3, 1).Range.Text = Español.Autores;
                oTable.Cell(4, 1).Range.Text = Español.Fuentes;
                oTable.Cell(5, 1).Range.Text = Español.Subobjetivos;
                oTable.Cell(6, 1).Range.Text = Español.Prioridad;
                oTable.Cell(7, 1).Range.Text = Español.Urgencia;
                oTable.Cell(8, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(9, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(10, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 10);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void Actores(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 6, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = fila[4].ToString() + " (" + fila[5].ToString() + ")";
            oTable.Cell(6, 2).Range.Text = fila[6].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Description;
                oTable.Cell(3, 1).Range.Text = Ingles.Authors;
                oTable.Cell(4, 1).Range.Text = Ingles.Sources;
                oTable.Cell(5, 1).Range.Text = Ingles.Complexity;
                oTable.Cell(6, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Descripción;
                oTable.Cell(3, 1).Range.Text = Español.Autores;
                oTable.Cell(4, 1).Range.Text = Español.Fuentes;
                oTable.Cell(5, 1).Range.Text = Español.Complejidad;
                oTable.Cell(6, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 6);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqNFun(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 10, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(7, 2).Range.Text = fila[4].ToString();
            oTable.Cell(8, 2).Range.Text = fila[5].ToString();
            oTable.Cell(9, 2).Range.Text = fila[6].ToString();
            oTable.Cell(10, 2).Range.Text = fila[7].ToString();
            oTable.Cell(11, 2).Range.Text = fila[8].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Description;
                oTable.Cell(3, 1).Range.Text = Ingles.Authors;
                oTable.Cell(4, 1).Range.Text = Ingles.Sources;
                oTable.Cell(5, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(6, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(7, 1).Range.Text = Ingles.Priority;
                oTable.Cell(8, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(9, 1).Range.Text = Ingles.Stability;
                oTable.Cell(10, 1).Range.Text = Ingles.State;
                oTable.Cell(11, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Descripción;
                oTable.Cell(3, 1).Range.Text = Español.Autores;
                oTable.Cell(4, 1).Range.Text = Español.Fuentes;
                oTable.Cell(5, 1).Range.Text = Español.RelObjet;
                oTable.Cell(6, 1).Range.Text = Español.RelRequi;
                oTable.Cell(7, 1).Range.Text = Español.Prioridad;
                oTable.Cell(8, 1).Range.Text = Español.Urgencia;
                oTable.Cell(9, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(10, 1).Range.Text = Español.Estado;
                oTable.Cell(11, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 10);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqInfo(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataTable DatEsp)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 14, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(7, 2).Range.Text = GeneraInfo(DatEsp, DefValues.DataTable);
            oTable.Cell(10, 2).Range.Text = fila[8].ToString();
            oTable.Cell(11, 2).Range.Text = fila[9].ToString();
            oTable.Cell(12, 2).Range.Text = fila[10].ToString();
            oTable.Cell(13, 2).Range.Text = fila[11].ToString();
            oTable.Cell(14, 2).Range.Text = fila[12].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Description;
                oTable.Cell(3, 1).Range.Text = Ingles.Authors;
                oTable.Cell(4, 1).Range.Text = Ingles.Sources;
                oTable.Cell(5, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(6, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(7, 1).Range.Text = Ingles.SpeDat;
                oTable.Cell(8, 1).Range.Text = Ingles.TimeLife;
                oTable.Cell(9, 1).Range.Text = Ingles.Occurrences;
                oTable.Cell(10, 1).Range.Text = Ingles.Priority;
                oTable.Cell(11, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(12, 1).Range.Text = Ingles.Stability;
                oTable.Cell(13, 1).Range.Text = Ingles.State;
                oTable.Cell(14, 1).Range.Text = Ingles.Commentary;
                oTable.Cell(8, 2).Range.Text = Ingles.Medium + ": " + fila[4].ToString() + " " + Ingles.Maximum + ": " + fila[5].ToString();
                oTable.Cell(9, 2).Range.Text = Ingles.Medium + ": " + fila[6].ToString() + " " + Ingles.Maximum + ": " + fila[7].ToString();
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Descripción;
                oTable.Cell(3, 1).Range.Text = Español.Autores;
                oTable.Cell(4, 1).Range.Text = Español.Fuentes;
                oTable.Cell(5, 1).Range.Text = Español.RelObjet;
                oTable.Cell(6, 1).Range.Text = Español.RelRequi;
                oTable.Cell(7, 1).Range.Text = Español.DatSpe;
                oTable.Cell(8, 1).Range.Text = Español.TiemVida;
                oTable.Cell(9, 1).Range.Text = Español.Ocurrencias;
                oTable.Cell(10, 1).Range.Text = Español.Prioridad;
                oTable.Cell(11, 1).Range.Text = Español.Urgencia;
                oTable.Cell(12, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(13, 1).Range.Text = Español.Estado;
                oTable.Cell(14, 1).Range.Text = Español.Comentario;
                oTable.Cell(8, 2).Range.Text = Español.Medio + ": " + fila[4].ToString() + " " + Español.Máximo + ": " + fila[5].ToString();
                oTable.Cell(9, 2).Range.Text = Español.Medio + ": " + fila[6].ToString() + " " + Español.Máximo + ": " + fila[7].ToString();
            }
            Negrita(oTable, 14);
            oTable.Columns[1].Width = oWord.InchesToPoints(1.25f);
            oTable.Columns[2].Width = oWord.InchesToPoints(4.75f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqFun(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataRow[] Act, DataTable SecNor, DataTable SecExc)
        {
            Microsoft.Office.Interop.Word.Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 16, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = fila[3].ToString();
            oTable.Cell(3, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Obj);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(7, 2).Range.Text = GeneraInfo(Act);
            oTable.Cell(8, 2).Range.Text = fila[5].ToString();
            oTable.Cell(9, 2).Range.Text = GeneraInfo(SecNor, DefValues.DataTable);
            oTable.Cell(10, 2).Range.Text = fila[6].ToString();
            oTable.Cell(11, 2).Range.Text = GeneraInfo(SecExc, DefValues.DataTable);
            oTable.Cell(12, 2).Range.Text = fila[7].ToString();
            oTable.Cell(13, 2).Range.Text = fila[8].ToString();
            oTable.Cell(14, 2).Range.Text = fila[9].ToString();
            oTable.Cell(15, 2).Range.Text = fila[10].ToString();
            oTable.Cell(16, 2).Range.Text = fila[11].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Description;
                oTable.Cell(3, 1).Range.Text = Ingles.Authors;
                oTable.Cell(4, 1).Range.Text = Ingles.Sources;
                oTable.Cell(5, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(6, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(7, 1).Range.Text = Ingles.Actors;
                oTable.Cell(8, 1).Range.Text = Ingles.Precondición;
                oTable.Cell(9, 1).Range.Text = Ingles.SecNor;
                oTable.Cell(10, 1).Range.Text = Ingles.Postcondición;
                oTable.Cell(11, 1).Range.Text = Ingles.SecExc;
                oTable.Cell(12, 1).Range.Text = Ingles.Priority;
                oTable.Cell(13, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(14, 1).Range.Text = Ingles.Stability;
                oTable.Cell(15, 1).Range.Text = Ingles.State;
                oTable.Cell(16, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Descripción;
                oTable.Cell(3, 1).Range.Text = Español.Autores;
                oTable.Cell(4, 1).Range.Text = Español.Fuentes;
                oTable.Cell(5, 1).Range.Text = Español.RelObjet;
                oTable.Cell(6, 1).Range.Text = Español.RelRequi;
                oTable.Cell(7, 1).Range.Text = Español.Actores;
                oTable.Cell(8, 1).Range.Text = Español.Precondición;
                oTable.Cell(9, 1).Range.Text = Español.SecNor;
                oTable.Cell(10, 1).Range.Text = Español.Postcondición;
                oTable.Cell(11, 1).Range.Text = Español.SecExc;
                oTable.Cell(12, 1).Range.Text = Español.Prioridad;
                oTable.Cell(13, 1).Range.Text = Español.Urgencia;
                oTable.Cell(14, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(15, 1).Range.Text = Español.Estado;
                oTable.Cell(16, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 16);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        private static string GeneraInfo(object o, string Tipo = DefValues.DataRow)
        {
            string resultado = string.Empty;
            if (Tipo.Equals(DefValues.DataRow))
            {
                DataRow[] Filas = (DataRow[])o;
                if (Filas.Length > 0)
                {
                    foreach (DataRow fila in Filas) resultado += fila[0].ToString() + " " + fila[2] + " (" + fila[3].ToString() + ")\n";
                    return resultado.Substring(0, resultado.Length - 1);
                }
                else return "-";
            }
            else if (Tipo.Equals(DefValues.ArrayList))
            {
                ArrayList Array = (ArrayList)o;
                if (Array.Count > 0)
                {
                    foreach (DataRow req in Array) resultado += req[0].ToString() + " " + req[2].ToString() + "\n";
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
        private static void Negrita(Table oTable, int num)
        {
            oTable.Cell(1, 2).Range.Font.Bold = 1;
            for (int i = 0; i < num; i++) oTable.Cell(i + 1, 1).Range.Font.Bold = 1;
        }
        public static void RellenarFila(Excel.Range aRange, DataTable tabla, int nFila, string Req)
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
            tabla.Columns.Add("Pos", typeof(double));
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
        public static Excel.Range DarFormatoExcel(Excel.Worksheet ws, DataTable tabla, int numObj)
        {
            Excel.Range aRange;

            //Bordes
            aRange = ws.get_Range("A1", letraColumna(numObj) + (tabla.Rows.Count + 1));
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            aRange.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;

            //Fila uno
            aRange = ws.get_Range("A1", letraColumna(numObj) + 1);
            aRange.Interior.ColorIndex = 15;
            //Columna uno
            aRange = ws.get_Range("A1", "A" + (tabla.Rows.Count + 1));
            aRange.Interior.ColorIndex = 15;
            aRange.Cells[1, 1].Value2 = ClsConf.Idioma.Equals(DefValues.Ingles) ? Ingles.Objectives : Español.Objetivos;
            aRange.ColumnWidth = 10;
            //Formato columnas 2...
            aRange = ws.get_Range("B1", letraColumna(numObj) + (tabla.Rows.Count + 1));
            aRange.ColumnWidth = 3;
            aRange.HorizontalAlignment = Excel.Constants.xlCenter;
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