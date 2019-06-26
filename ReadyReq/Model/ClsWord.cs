/*
 * Autor: Arturo Balleros Albillo
 */
using Microsoft.Office.Interop.Word;
using ReadyReq.Util;
using System;
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

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 6, 2);//Filas y columnas
            oTable.Range.ParagraphFormat.SpaceAfter = 3;//Espacio despues de la tabla
            oTable.Borders.Enable = 1;//Dibuja bordes en la tabla

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = fila[6].ToString();
            oTable.Cell(5, 2).Range.Text = fila[7].ToString();
            oTable.Cell(6, 2).Range.Text = fila[8].ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Organization;
                oTable.Cell(4, 1).Range.Text = Ingles.Role;
                oTable.Cell(5, 1).Range.Text = Ingles.IsDev;
                oTable.Cell(6, 1).Range.Text = Ingles.Commentary;
                oTable.Columns[1].Width = oWord.InchesToPoints(1);//Ancho columna 1
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Organización;
                oTable.Cell(4, 1).Range.Text = Español.Rol;
                oTable.Cell(5, 1).Range.Text = Español.Es_Des;
                oTable.Cell(6, 1).Range.Text = Español.Comentario;
                oTable.Columns[1].Width = oWord.InchesToPoints(1.25f);//Ancho columna 1
            }

            Negrita(oTable, 6);//Negrita columna 1 y fila 1 columna 2            
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;//Color fondo columna 1
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;//color fondo fila 1 columna 2
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);//Añade tabla a documento
        }
        public static void Objetivos(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] SubObj)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 11, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(SubObj, DefValues.DataRow, false);
            oTable.Cell(7, 2).Range.Text = fila[6].ToString();
            oTable.Cell(8, 2).Range.Text = fila[7].ToString();
            oTable.Cell(9, 2).Range.Text = fila[8].ToString();
            oTable.Cell(10, 2).Range.Text = fila[9].ToString();
            oTable.Cell(11, 2).Range.Text = fila[10].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Description;
                oTable.Cell(4, 1).Range.Text = Ingles.Authors;
                oTable.Cell(5, 1).Range.Text = Ingles.Sources;
                oTable.Cell(6, 1).Range.Text = Ingles.Subobjectives;
                oTable.Cell(7, 1).Range.Text = Ingles.Priority;
                oTable.Cell(8, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(9, 1).Range.Text = Ingles.Stability;
                oTable.Cell(10, 1).Range.Text = Ingles.State;
                oTable.Cell(11, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Descripción;
                oTable.Cell(4, 1).Range.Text = Español.Autores;
                oTable.Cell(5, 1).Range.Text = Español.Fuentes;
                oTable.Cell(6, 1).Range.Text = Español.Subobjetivos;
                oTable.Cell(7, 1).Range.Text = Español.Prioridad;
                oTable.Cell(8, 1).Range.Text = Español.Urgencia;
                oTable.Cell(9, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(10, 1).Range.Text = Español.Estado;
                oTable.Cell(11, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 11);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void Actores(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 7, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(6, 2).Range.Text = fila[6].ToString() + " (" + fila[7].ToString() + ")";
            oTable.Cell(7, 2).Range.Text = fila[8].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Description;
                oTable.Cell(4, 1).Range.Text = Ingles.Authors;
                oTable.Cell(5, 1).Range.Text = Ingles.Sources;
                oTable.Cell(6, 1).Range.Text = Ingles.Complexity;
                oTable.Cell(7, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Descripción;
                oTable.Cell(4, 1).Range.Text = Español.Autores;
                oTable.Cell(5, 1).Range.Text = Español.Fuentes;
                oTable.Cell(6, 1).Range.Text = Español.Complejidad;
                oTable.Cell(7, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 7);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqNFun(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req)
        {
            Table oTable;

            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 12, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Obj, DefValues.DataRow, false);
            oTable.Cell(7, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(8, 2).Range.Text = fila[6].ToString();
            oTable.Cell(9, 2).Range.Text = fila[7].ToString();
            oTable.Cell(10, 2).Range.Text = fila[8].ToString();
            oTable.Cell(11, 2).Range.Text = fila[9].ToString();
            oTable.Cell(12, 2).Range.Text = fila[10].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Description;
                oTable.Cell(4, 1).Range.Text = Ingles.Authors;
                oTable.Cell(5, 1).Range.Text = Ingles.Sources;
                oTable.Cell(6, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(7, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(8, 1).Range.Text = Ingles.Priority;
                oTable.Cell(9, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(10, 1).Range.Text = Ingles.Stability;
                oTable.Cell(11, 1).Range.Text = Ingles.State;
                oTable.Cell(12, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Descripción;
                oTable.Cell(4, 1).Range.Text = Español.Autores;
                oTable.Cell(5, 1).Range.Text = Español.Fuentes;
                oTable.Cell(6, 1).Range.Text = Español.RelObjet;
                oTable.Cell(7, 1).Range.Text = Español.RelRequi;
                oTable.Cell(8, 1).Range.Text = Español.Prioridad;
                oTable.Cell(9, 1).Range.Text = Español.Urgencia;
                oTable.Cell(10, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(11, 1).Range.Text = Español.Estado;
                oTable.Cell(12, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 12);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqInfo(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataTable DatEsp)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 15, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Obj, DefValues.DataRow, false);
            oTable.Cell(7, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(8, 2).Range.Text = GeneraInfo(DatEsp, DefValues.DataTable);
            oTable.Cell(11, 2).Range.Text = fila[10].ToString();
            oTable.Cell(12, 2).Range.Text = fila[11].ToString();
            oTable.Cell(13, 2).Range.Text = fila[12].ToString();
            oTable.Cell(14, 2).Range.Text = fila[13].ToString();
            oTable.Cell(15, 2).Range.Text = fila[14].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Description;
                oTable.Cell(4, 1).Range.Text = Ingles.Authors;
                oTable.Cell(5, 1).Range.Text = Ingles.Sources;
                oTable.Cell(6, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(7, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(8, 1).Range.Text = Ingles.SpeDat;
                oTable.Cell(9, 1).Range.Text = Ingles.TimeLife;
                oTable.Cell(10, 1).Range.Text = Ingles.Occurrences;
                oTable.Cell(11, 1).Range.Text = Ingles.Priority;
                oTable.Cell(12, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(13, 1).Range.Text = Ingles.Stability;
                oTable.Cell(14, 1).Range.Text = Ingles.State;
                oTable.Cell(15, 1).Range.Text = Ingles.Commentary;
                oTable.Cell(9, 2).Range.Text = Ingles.Medium + ": " + fila[6].ToString() + " " + Ingles.Maximum + ": " + fila[7].ToString();
                oTable.Cell(10, 2).Range.Text = Ingles.Medium + ": " + fila[8].ToString() + " " + Ingles.Maximum + ": " + fila[9].ToString();
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Descripción;
                oTable.Cell(4, 1).Range.Text = Español.Autores;
                oTable.Cell(5, 1).Range.Text = Español.Fuentes;
                oTable.Cell(6, 1).Range.Text = Español.RelObjet;
                oTable.Cell(7, 1).Range.Text = Español.RelRequi;
                oTable.Cell(8, 1).Range.Text = Español.DatSpe;
                oTable.Cell(9, 1).Range.Text = Español.TiemVida;
                oTable.Cell(10, 1).Range.Text = Español.Ocurrencias;
                oTable.Cell(11, 1).Range.Text = Español.Prioridad;
                oTable.Cell(12, 1).Range.Text = Español.Urgencia;
                oTable.Cell(13, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(14, 1).Range.Text = Español.Estado;
                oTable.Cell(15, 1).Range.Text = Español.Comentario;
                oTable.Cell(9, 2).Range.Text = Español.Medio + ": " + fila[6].ToString() + " " + Español.Máximo + ": " + fila[7].ToString();
                oTable.Cell(10, 2).Range.Text = Español.Medio + ": " + fila[8].ToString() + " " + Español.Máximo + ": " + fila[9].ToString();
            }
            Negrita(oTable, 15);
            oTable.Columns[1].Width = oWord.InchesToPoints(1.25f);
            oTable.Columns[2].Width = oWord.InchesToPoints(4.75f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void ReqFun(Application oWord, Document oDoc, DataRow fila, DataRow[] Auto, DataRow[] Fuen, DataRow[] Obj, ArrayList Req, DataRow[] Act, DataTable SecNor, DataTable SecExc)
        {
            Microsoft.Office.Interop.Word.Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 18, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(1, 1).Range.Text = fila[0].ToString();
            oTable.Cell(1, 2).Range.Text = fila[2].ToString();
            oTable.Cell(2, 2).Range.Text = ClsFunciones.DoubleToString((double)fila[3]) + " (" + ((DateTime)fila[4]).ToShortDateString() + ")";
            oTable.Cell(3, 2).Range.Text = fila[5].ToString();
            oTable.Cell(4, 2).Range.Text = GeneraInfo(Auto);
            oTable.Cell(5, 2).Range.Text = GeneraInfo(Fuen);
            oTable.Cell(6, 2).Range.Text = GeneraInfo(Obj, DefValues.DataRow, false);
            oTable.Cell(7, 2).Range.Text = GeneraInfo(Req, DefValues.ArrayList);
            oTable.Cell(8, 2).Range.Text = GeneraInfo(Act, DefValues.DataRow, false);
            oTable.Cell(9, 2).Range.Text = fila[7].ToString();
            oTable.Cell(10, 2).Range.Text = GeneraInfo(SecNor, DefValues.DataTable);
            oTable.Cell(11, 2).Range.Text = fila[8].ToString();
            oTable.Cell(12, 2).Range.Text = GeneraInfo(SecExc, DefValues.DataTable);
            oTable.Cell(13, 2).Range.Text = fila[9].ToString();
            oTable.Cell(14, 2).Range.Text = fila[10].ToString();
            oTable.Cell(15, 2).Range.Text = fila[11].ToString();
            oTable.Cell(16, 2).Range.Text = fila[12].ToString();
            oTable.Cell(17, 2).Range.Text = fila[13].ToString();
            oTable.Cell(18, 2).Range.Text = fila[14].ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(2, 1).Range.Text = Ingles.Version;
                oTable.Cell(3, 1).Range.Text = Ingles.Description;
                oTable.Cell(4, 1).Range.Text = Ingles.Authors;
                oTable.Cell(5, 1).Range.Text = Ingles.Sources;
                oTable.Cell(6, 1).Range.Text = Ingles.RelObjet;
                oTable.Cell(7, 1).Range.Text = Ingles.RelRequi;
                oTable.Cell(8, 1).Range.Text = Ingles.Actors;
                oTable.Cell(9, 1).Range.Text = Ingles.Precondición;
                oTable.Cell(10, 1).Range.Text = Ingles.SecNor;
                oTable.Cell(11, 1).Range.Text = Ingles.Postcondición;
                oTable.Cell(12, 1).Range.Text = Ingles.SecExc;
                oTable.Cell(13, 1).Range.Text = Ingles.Complexity;
                oTable.Cell(14, 1).Range.Text = Ingles.Priority;
                oTable.Cell(15, 1).Range.Text = Ingles.Urgency;
                oTable.Cell(16, 1).Range.Text = Ingles.Stability;
                oTable.Cell(17, 1).Range.Text = Ingles.State;
                oTable.Cell(18, 1).Range.Text = Ingles.Commentary;
            }
            else
            {
                oTable.Cell(2, 1).Range.Text = Español.Version;
                oTable.Cell(3, 1).Range.Text = Español.Descripción;
                oTable.Cell(4, 1).Range.Text = Español.Autores;
                oTable.Cell(5, 1).Range.Text = Español.Fuentes;
                oTable.Cell(6, 1).Range.Text = Español.RelObjet;
                oTable.Cell(7, 1).Range.Text = Español.RelRequi;
                oTable.Cell(8, 1).Range.Text = Español.Actores;
                oTable.Cell(9, 1).Range.Text = Español.Precondición;
                oTable.Cell(10, 1).Range.Text = Español.SecNor;
                oTable.Cell(11, 1).Range.Text = Español.Postcondición;
                oTable.Cell(12, 1).Range.Text = Español.SecExc;
                oTable.Cell(13, 1).Range.Text = Español.Complejidad;
                oTable.Cell(14, 1).Range.Text = Español.Prioridad;
                oTable.Cell(15, 1).Range.Text = Español.Urgencia;
                oTable.Cell(16, 1).Range.Text = Español.Estabilidad;
                oTable.Cell(17, 1).Range.Text = Español.Estado;
                oTable.Cell(18, 1).Range.Text = Español.Comentario;
            }
            Negrita(oTable, 18);
            oTable.Columns[1].Width = oWord.InchesToPoints(1);
            oTable.Columns[2].Width = oWord.InchesToPoints(5);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        public static void Estim(Application oWord, Document oDoc)
        {
            ClsEstim Estimaciones = new ClsEstim();
            Estimaciones.CargarValores();
            Estimaciones.CalcValores();

            if (ClsConf.Idioma.Equals(DefValues.Ingles)) AddParrafo(oDoc, Ingles.TecFac); else AddParrafo(oDoc, Español.TecFac);
            TableTecFac(oWord, oDoc, Estimaciones);
            if (ClsConf.Idioma.Equals(DefValues.Ingles)) AddParrafo(oDoc, Ingles.EnvFac); else AddParrafo(oDoc, Español.EnvFac);
            TableEnvFac(oWord, oDoc, Estimaciones);
            if (ClsConf.Idioma.Equals(DefValues.Ingles)) AddParrafo(oDoc, Ingles.UUCP); else AddParrafo(oDoc, Español.UUCP);
            TableUUCP(oWord, oDoc, Estimaciones);
            if (ClsConf.Idioma.Equals(DefValues.Ingles)) AddParrafo(oDoc, Ingles.AW); else AddParrafo(oDoc, Español.AW);
            TableAW(oWord, oDoc, Estimaciones);
            if (ClsConf.Idioma.Equals(DefValues.Ingles)) AddParrafo(oDoc, Ingles.TiFC); else AddParrafo(oDoc, Español.TiFC);
            TableTiFC(oWord, oDoc, Estimaciones);

        }
        private static string GeneraInfo(object o, string Tipo = DefValues.DataRow, bool isGrupo = true)
        {
            string resultado = string.Empty;
            if (Tipo.Equals(DefValues.DataRow))
            {
                DataRow[] Filas = (DataRow[])o;
                if (Filas.Length > 0)
                {
                    foreach (DataRow fila in Filas)
                        if (isGrupo) resultado += fila[0].ToString() + " " + fila[2] + " (" + fila[5].ToString() + ")\n";
                        else resultado += fila[0].ToString() + " " + fila[2] + "\n";
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
        private static void AddParrafo(Document oDoc, string text)
        {
            Paragraph oPara;
            oPara = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
            oPara.Range.Text = text;
            oPara.Range.Font.Bold = 1;
            oPara.Range.Font.Size = 16;
            oPara.Format.SpaceAfter = 20;
            oPara.Range.InsertParagraphAfter();
            oPara.Range.Font.Size = 11;
            oPara.Range.Font.Bold = 0;
        }
        private static void TableTecFac(Application oWord, Document oDoc, ClsEstim Estimaciones)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 14, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(2, 2).Range.Text = Estimaciones.DSR.ToString();
            oTable.Cell(3, 2).Range.Text = Estimaciones.RTII.ToString();
            oTable.Cell(4, 2).Range.Text = Estimaciones.EUE.ToString();
            oTable.Cell(5, 2).Range.Text = Estimaciones.CIPR.ToString();
            oTable.Cell(6, 2).Range.Text = Estimaciones.RCMBAF.ToString();
            oTable.Cell(7, 2).Range.Text = Estimaciones.IE.ToString();
            oTable.Cell(8, 2).Range.Text = Estimaciones.U.ToString();
            oTable.Cell(9, 2).Range.Text = Estimaciones.CPS.ToString();
            oTable.Cell(10, 2).Range.Text = Estimaciones.ETC.ToString();
            oTable.Cell(11, 2).Range.Text = Estimaciones.HC.ToString();
            oTable.Cell(12, 2).Range.Text = Estimaciones.CS.ToString();
            oTable.Cell(13, 2).Range.Text = Estimaciones.DOTPC.ToString();
            oTable.Cell(14, 2).Range.Text = Estimaciones.UT.ToString();
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(1, 1).Range.Text = Ingles.TecFac;
                oTable.Cell(1, 2).Range.Text = Ingles.Value;
                oTable.Cell(2, 1).Range.Text = Ingles.DSR;
                oTable.Cell(3, 1).Range.Text = Ingles.RTII;
                oTable.Cell(4, 1).Range.Text = Ingles.EUE;
                oTable.Cell(5, 1).Range.Text = Ingles.CIPR;
                oTable.Cell(6, 1).Range.Text = Ingles.RCMBAF;
                oTable.Cell(7, 1).Range.Text = Ingles.IE;
                oTable.Cell(8, 1).Range.Text = Ingles.U;
                oTable.Cell(9, 1).Range.Text = Ingles.CPS;
                oTable.Cell(10, 1).Range.Text = Ingles.ETC;
                oTable.Cell(11, 1).Range.Text = Ingles.HC;
                oTable.Cell(12, 1).Range.Text = Ingles.CS;
                oTable.Cell(13, 1).Range.Text = Ingles.DOTPC;
                oTable.Cell(14, 1).Range.Text = Ingles.UT;
                oTable.Columns[1].Width = oWord.InchesToPoints(2.5f);
            }
            else
            {
                oTable.Cell(1, 1).Range.Text = Español.TecFac;
                oTable.Cell(1, 2).Range.Text = Español.Valor;
                oTable.Cell(2, 1).Range.Text = Español.DSR;
                oTable.Cell(3, 1).Range.Text = Español.RTII;
                oTable.Cell(4, 1).Range.Text = Español.EUE;
                oTable.Cell(5, 1).Range.Text = Español.CIPR;
                oTable.Cell(6, 1).Range.Text = Español.RCMBAF;
                oTable.Cell(7, 1).Range.Text = Español.IE;
                oTable.Cell(8, 1).Range.Text = Español.U;
                oTable.Cell(9, 1).Range.Text = Español.CPS;
                oTable.Cell(10, 1).Range.Text = Español.ETC;
                oTable.Cell(11, 1).Range.Text = Español.HC;
                oTable.Cell(12, 1).Range.Text = Español.CS;
                oTable.Cell(13, 1).Range.Text = Español.DOTPC;
                oTable.Cell(14, 1).Range.Text = Español.UT;
                oTable.Columns[1].Width = oWord.InchesToPoints(3);

            }

            Negrita(oTable, 14);
            oTable.Columns[2].Width = oWord.InchesToPoints(0.5f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        private static void TableEnvFac(Application oWord, Document oDoc, ClsEstim Estimaciones)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 9, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(2, 2).Range.Text = Estimaciones.FWTP.ToString();
            oTable.Cell(3, 2).Range.Text = Estimaciones.AE.ToString();
            oTable.Cell(4, 2).Range.Text = Estimaciones.OOPE.ToString();
            oTable.Cell(5, 2).Range.Text = Estimaciones.LAC.ToString();
            oTable.Cell(6, 2).Range.Text = Estimaciones.M.ToString();
            oTable.Cell(7, 2).Range.Text = Estimaciones.SR.ToString();
            oTable.Cell(8, 2).Range.Text = Estimaciones.PTS.ToString();
            oTable.Cell(9, 2).Range.Text = Estimaciones.DPL.ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(1, 1).Range.Text = Ingles.EnvFac;
                oTable.Cell(1, 2).Range.Text = Ingles.Value;
                oTable.Cell(2, 1).Range.Text = Ingles.FWTP;
                oTable.Cell(3, 1).Range.Text = Ingles.AE;
                oTable.Cell(4, 1).Range.Text = Ingles.OOPE;
                oTable.Cell(5, 1).Range.Text = Ingles.LAC;
                oTable.Cell(6, 1).Range.Text = Ingles.M;
                oTable.Cell(7, 1).Range.Text = Ingles.SR;
                oTable.Cell(8, 1).Range.Text = Ingles.PTS;
                oTable.Cell(9, 1).Range.Text = Ingles.DPL;
                oTable.Columns[1].Width = oWord.InchesToPoints(2.5f);
            }
            else
            {
                oTable.Cell(1, 1).Range.Text = Español.EnvFac;
                oTable.Cell(1, 2).Range.Text = Español.Valor;
                oTable.Cell(2, 1).Range.Text = Español.FWTP;
                oTable.Cell(3, 1).Range.Text = Español.AE;
                oTable.Cell(4, 1).Range.Text = Español.OOPE;
                oTable.Cell(5, 1).Range.Text = Español.M;
                oTable.Cell(6, 1).Range.Text = Español.LAC;
                oTable.Cell(7, 1).Range.Text = Español.SR;
                oTable.Cell(8, 1).Range.Text = Español.PTS;
                oTable.Cell(9, 1).Range.Text = Español.DPL;
                oTable.Columns[1].Width = oWord.InchesToPoints(3);

            }

            Negrita(oTable, 9);
            oTable.Columns[2].Width = oWord.InchesToPoints(0.5f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        private static void TableUUCP(Application oWord, Document oDoc, ClsEstim Estimaciones)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 4, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(2, 2).Range.Text = Estimaciones.UUCPSim.ToString();
            oTable.Cell(3, 2).Range.Text = Estimaciones.UUCPMed.ToString();
            oTable.Cell(4, 2).Range.Text = Estimaciones.UUCPMax.ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(1, 1).Range.Text = Ingles.UUCP;
                oTable.Cell(1, 2).Range.Text = Ingles.Value;
                oTable.Cell(2, 1).Range.Text = Ingles.Sim;
                oTable.Cell(3, 1).Range.Text = Ingles.Ave;
                oTable.Cell(4, 1).Range.Text = Ingles.Com;
                oTable.Columns[1].Width = oWord.InchesToPoints(2.5f);
            }
            else
            {
                oTable.Cell(1, 1).Range.Text = Español.UUCP;
                oTable.Cell(1, 2).Range.Text = Español.Valor;
                oTable.Cell(2, 1).Range.Text = Español.Sim;
                oTable.Cell(3, 1).Range.Text = Español.Ave;
                oTable.Cell(4, 1).Range.Text = Español.Com;
                oTable.Columns[1].Width = oWord.InchesToPoints(3);

            }

            Negrita(oTable, 4);
            oTable.Columns[2].Width = oWord.InchesToPoints(0.5f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        private static void TableAW(Application oWord, Document oDoc, ClsEstim Estimaciones)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 4, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(2, 2).Range.Text = Estimaciones.AWSim.ToString();
            oTable.Cell(3, 2).Range.Text = Estimaciones.AWMed.ToString();
            oTable.Cell(4, 2).Range.Text = Estimaciones.AWMax.ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(1, 1).Range.Text = Ingles.AW;
                oTable.Cell(1, 2).Range.Text = Ingles.Value;
                oTable.Cell(2, 1).Range.Text = Ingles.Sim;
                oTable.Cell(3, 1).Range.Text = Ingles.Ave;
                oTable.Cell(4, 1).Range.Text = Ingles.Com;
                oTable.Columns[1].Width = oWord.InchesToPoints(2.5f);
            }
            else
            {
                oTable.Cell(1, 1).Range.Text = Español.AW;
                oTable.Cell(1, 2).Range.Text = Español.Valor;
                oTable.Cell(2, 1).Range.Text = Español.Sim;
                oTable.Cell(3, 1).Range.Text = Español.Ave;
                oTable.Cell(4, 1).Range.Text = Español.Com;
                oTable.Columns[1].Width = oWord.InchesToPoints(3);

            }

            Negrita(oTable, 4);
            oTable.Columns[2].Width = oWord.InchesToPoints(0.5f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
        private static void TableTiFC(Application oWord, Document oDoc, ClsEstim Estimaciones)
        {
            Table oTable;
            oTable = oDoc.Tables.Add(oDoc.Bookmarks[DefValues.FinFichero].Range, 6, 2);
            oTable.Range.ParagraphFormat.SpaceAfter = 3;
            oTable.Borders.Enable = 1;

            oTable.Cell(2, 2).Range.Text = Estimaciones.TCF.ToString();
            oTable.Cell(3, 2).Range.Text = Estimaciones.EF.ToString();
            oTable.Cell(4, 2).Range.Text = Estimaciones.UUCP.ToString();
            oTable.Cell(5, 2).Range.Text = Estimaciones.AW.ToString();
            oTable.Cell(6, 2).Range.Text = Estimaciones.UCP.ToString();

            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                oTable.Cell(1, 1).Range.Text = Ingles.TiFC;
                oTable.Cell(1, 2).Range.Text = Ingles.Value;
                oTable.Cell(2, 1).Range.Text = Ingles.TecFac;
                oTable.Cell(3, 1).Range.Text = Ingles.EnvFac;
                oTable.Cell(4, 1).Range.Text = Ingles.UUCP;
                oTable.Cell(5, 1).Range.Text = Ingles.AW;
                oTable.Cell(6, 1).Range.Text = Ingles.UCP;
                oTable.Columns[1].Width = oWord.InchesToPoints(2.5f);
            }
            else
            {
                oTable.Cell(1, 1).Range.Text = Español.TiFC;
                oTable.Cell(1, 2).Range.Text = Español.Valor;
                oTable.Cell(2, 1).Range.Text = Español.TecFac;
                oTable.Cell(3, 1).Range.Text = Español.EnvFac;
                oTable.Cell(4, 1).Range.Text = Español.UUCP;
                oTable.Cell(5, 1).Range.Text = Español.AW;
                oTable.Cell(6, 1).Range.Text = Español.UCP;
                oTable.Columns[1].Width = oWord.InchesToPoints(3);

            }

            Negrita(oTable, 6);
            oTable.Columns[2].Width = oWord.InchesToPoints(0.6f);
            oTable.Columns[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oTable.Rows[1].Shading.BackgroundPatternColor = WdColor.wdColorGray125;
            oDoc.Content.Paragraphs.Add(oDoc.Bookmarks[DefValues.FinFichero].Range);
        }
    }
}