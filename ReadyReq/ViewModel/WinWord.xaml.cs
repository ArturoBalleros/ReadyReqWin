using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace ReadyReq.ViewModel
{
    public partial class WinWord : Window
    {
        DataTable DTGrupo = new DataTable();
        DataTable DTObjetivos = new DataTable();
        DataTable DTActores = new DataTable();
        DataTable DTReqInfo = new DataTable();
        DataTable DTReqFun = new DataTable();
        DataTable DTReqNFun = new DataTable();
        string StrMenErr;
        string StrMenErrTab;
        public WinWord()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
        }
        private void ButClick(object sender, RoutedEventArgs e)
        {
            ButEmpezar.IsEnabled = false;
            if (ChkAct.IsChecked == true || ChkGru.IsChecked == true || ChkObj.IsChecked == true || ChkTra.IsChecked == true || ChkReqI.IsChecked == true || ChkReqN.IsChecked == true || ChkReqF.IsChecked == true)
            {
                if (CreaTablas() == -1) MessageBox.Show(StrMenErrTab);
                if (ChkAct.IsChecked == true)
                    if (Actores() == -1) MessageBox.Show(StrMenErr);
                if (ChkGru.IsChecked == true)
                    if (GrupoTrabajo() == -1) MessageBox.Show(StrMenErr);
                if (ChkObj.IsChecked == true)
                    if (Objetivos() == -1) MessageBox.Show(StrMenErr);
                if (ChkTra.IsChecked == true)
                    if (Traza() == -1) MessageBox.Show(StrMenErr);
                if (ChkReqI.IsChecked == true)
                    if (ReqInfo() == -1) MessageBox.Show(StrMenErr);
                if (ChkReqN.IsChecked == true)
                    if (ReqNFun() == -1) MessageBox.Show(StrMenErr);
                if (ChkReqF.IsChecked == true)
                    if (ReqFun() == -1) MessageBox.Show(StrMenErr);
                Close();
            }
            ButEmpezar.IsEnabled = true;
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                Title = Ingles.Export_Word;
                ChkGru.Content = Ingles.Workgroup;
                ChkObj.Content = Ingles.Objectives;
                ChkAct.Content = Ingles.Actors;
                ChkReqI.Content = Ingles.ReqInfo;
                ChkReqF.Content = Ingles.ReqFun;
                ChkReqN.Content = Ingles.ReqNFun;
                ChkTra.Content = Ingles.TracTable;
                ButEmpezar.Content = Ingles.Start;
                StrMenErr = Ingles.MenErrRes;
                StrMenErrTab = Ingles.MenErrTab;
            }
            else
            {
                Title = Español.Esportar_Word;
                ChkGru.Content = Español.Grupo_Trabajo;
                ChkObj.Content = Español.Objetivos;
                ChkAct.Content = Español.Actores;
                ChkReqI.Content = Español.ReqInfo;
                ChkReqF.Content = Español.ReqFun;
                ChkReqN.Content = Español.ReqNFun;
                ChkTra.Content = Español.TablaTraz;
                ButEmpezar.Content = Español.Comenzar;
                StrMenErr = Español.MenErrRes;
                StrMenErrTab = Español.MenErrTab;
            }
        }
        private int CreaTablas()
        {
            DataRow fila, filaN;
            DataTable tabla;
            try
            {
                tabla = ClsBaseDatos.BDTable("Select * From Grupo Order By Categoria Desc, Nombre;");
                DTGrupo.Columns.Add("IdW", Type.GetType("System.String"));
                DTGrupo.Columns.Add("Id", Type.GetType("System.Double"));
                DTGrupo.Columns.Add("Nombre", Type.GetType("System.String"));
                DTGrupo.Columns.Add("Organizacion", Type.GetType("System.String"));
                DTGrupo.Columns.Add("Rol", Type.GetType("System.String"));
                DTGrupo.Columns.Add("Desarrollador", Type.GetType("System.String"));
                DTGrupo.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTGrupo.NewRow();
                    filaN[0] = "STK-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = fila[3].ToString();
                    filaN[5] = DetEstado((int)fila[4], 4);
                    filaN[6] = fila[6].ToString();
                    DTGrupo.Rows.Add(filaN);
                }

                tabla = ClsBaseDatos.BDTable("Select * From Objetivos Order By Categoria Desc, Nombre;");
                DTObjetivos.Columns.Add("IdW", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Id", Type.GetType("System.Double"));
                DTObjetivos.Columns.Add("Nombre", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Descripcion", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Prioridad", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Urgencia", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Estabilidad", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Estado", Type.GetType("System.String"));
                DTObjetivos.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTObjetivos.NewRow();
                    filaN[0] = "OBJ-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = DetEstado((int)fila[3], 1);
                    filaN[5] = DetEstado((int)fila[4], 1);
                    filaN[6] = DetEstado((int)fila[5], 1);
                    filaN[7] = DetEstado((int)fila[6], 3);
                    filaN[8] = fila[8].ToString();
                    DTObjetivos.Rows.Add(filaN);
                }

                tabla = ClsBaseDatos.BDTable("Select * From Actores Order By Categoria Desc, Nombre;");
                DTActores.Columns.Add("IdW", Type.GetType("System.String"));
                DTActores.Columns.Add("Id", Type.GetType("System.Double"));
                DTActores.Columns.Add("Nombre", Type.GetType("System.String"));
                DTActores.Columns.Add("Descripcion", Type.GetType("System.String"));
                DTActores.Columns.Add("Complejidad", Type.GetType("System.String"));
                DTActores.Columns.Add("DescComple", Type.GetType("System.String"));
                DTActores.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTActores.NewRow();
                    filaN[0] = "ACT-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = DetEstado((int)fila[3], 2);
                    filaN[5] = fila[4].ToString();
                    filaN[6] = fila[6].ToString();
                    DTActores.Rows.Add(filaN);
                }

                tabla = ClsBaseDatos.BDTable("Select * From ReqFun f, Paquetes p where p.Id = f.Paquete Order By p.Categoria Desc, p.Nombre, f.Categoria Desc, f.Nombre;");
                DTReqFun.Columns.Add("IdW", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Id", Type.GetType("System.Double"));
                DTReqFun.Columns.Add("Nombre", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Descripcion", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Paquete", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Precond", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Postcond", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Prioridad", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Urgencia", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Estabilidad", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Estado", Type.GetType("System.String"));
                DTReqFun.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTReqFun.NewRow();
                    filaN[0] = "UC-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = fila[3].ToString();
                    filaN[5] = fila[4].ToString();
                    filaN[6] = fila[5].ToString();
                    filaN[7] = DetEstado((int)fila[6], 1);
                    filaN[8] = DetEstado((int)fila[7], 1);
                    filaN[9] = DetEstado((int)fila[8], 1);
                    filaN[10] = DetEstado((int)fila[9], 3);
                    filaN[11] = fila[11].ToString();
                    DTReqFun.Rows.Add(filaN);
                }

                tabla = ClsBaseDatos.BDTable("Select * From ReqInfo Order By Categoria Desc, Nombre;");
                DTReqInfo.Columns.Add("IdW", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Id", Type.GetType("System.Double"));
                DTReqInfo.Columns.Add("Nombre", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Descripcion", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("TiemMed", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("TiemMax", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("OcuMed", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("OcuMax", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Prioridad", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Urgencia", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Estabilidad", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Estado", Type.GetType("System.String"));
                DTReqInfo.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTReqInfo.NewRow();
                    filaN[0] = "IRQ-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = fila[3].ToString();
                    filaN[5] = fila[4].ToString();
                    filaN[6] = fila[5].ToString();
                    filaN[7] = fila[6].ToString();
                    filaN[8] = DetEstado((int)fila[7], 1);
                    filaN[9] = DetEstado((int)fila[8], 1);
                    filaN[10] = DetEstado((int)fila[9], 1);
                    filaN[11] = DetEstado((int)fila[10], 3);
                    filaN[12] = fila[12].ToString();
                    DTReqInfo.Rows.Add(filaN);
                }

                tabla = ClsBaseDatos.BDTable("Select * From ReqNFunc Order By Categoria Desc, Nombre;");
                DTReqNFun.Columns.Add("IdW", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Id", Type.GetType("System.Double"));
                DTReqNFun.Columns.Add("Nombre", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Descripcion", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Prioridad", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Urgencia", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Estabilidad", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Estado", Type.GetType("System.String"));
                DTReqNFun.Columns.Add("Comentario", Type.GetType("System.String"));
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    filaN = DTReqNFun.NewRow();
                    filaN[0] = "NFR-" + RellenarCeros(i + 1 + "");
                    filaN[1] = fila[0].ToString();
                    filaN[2] = fila[1].ToString();
                    filaN[3] = fila[2].ToString();
                    filaN[4] = DetEstado((int)fila[3], 1);
                    filaN[5] = DetEstado((int)fila[4], 1);
                    filaN[6] = DetEstado((int)fila[5], 1);
                    filaN[7] = DetEstado((int)fila[6], 3);
                    filaN[8] = fila[8].ToString();
                    DTReqNFun.Rows.Add(filaN);
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int GrupoTrabajo()
        {
            DataRow fila;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTGrupo.Rows.Count - 1; DoEvents();
                for (int i = 0; (i <= DTGrupo.Rows.Count - 1); i++)
                {
                    fila = DTGrupo.Rows[i];
                    ClsWord.Grupo(oWord, oDoc, fila);
                    PBProg.Value++; DoEvents();
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int Objetivos()
        {
            DataRow fila;
            DataRow[] Auto, Fuen, SubObj;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTObjetivos.Rows.Count - 1; DoEvents();
                for (int i = 0; (i <= DTObjetivos.Rows.Count - 1); i++)
                {
                    fila = DTObjetivos.Rows[i];
                    Auto = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAutor From ObjAuto Where IdObj = " + fila[1].ToString() + ";")), "IdW");
                    Fuen = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdFuen From ObjFuen Where IdObj = " + fila[1].ToString() + ";")), "IdW");
                    SubObj = DTObjetivos.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdSubobj From ObjSubObj Where IdObj = " + fila[1].ToString() + ";")), "IdW");
                    ClsWord.Objetivos(oWord, oDoc, fila, Auto, Fuen, SubObj);
                    PBProg.Value++; DoEvents();
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int Actores()
        {
            DataRow fila;
            DataRow[] Auto, Fuen;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTActores.Rows.Count - 1; DoEvents();
                for (int i = 0; (i <= DTActores.Rows.Count - 1); i++)
                {
                    fila = DTActores.Rows[i];
                    Auto = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAutor From ActAuto Where IdAct = " + fila[1].ToString() + ";")), "IdW");
                    Fuen = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdFuen From ActFuen Where IdAct = " + fila[1].ToString() + ";")), "IdW");
                    ClsWord.Actores(oWord, oDoc, fila, Auto, Fuen);
                    PBProg.Value++; DoEvents();
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int ReqNFun()
        {
            DataRow fila;
            DataRow[] Auto, Fuen, Obj, Req;
            ArrayList ALReq = new ArrayList();
            DataTable tabla;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTReqNFun.Rows.Count - 1; DoEvents();
                for (int i = 0; (i <= DTReqNFun.Rows.Count - 1); i++)
                {
                    fila = DTReqNFun.Rows[i];
                    Auto = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAutor From ReqNAuto Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    Fuen = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdFuen From ReqNFuen Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    Obj = DTObjetivos.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdObj From ReqNObj Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    tabla = ClsBaseDatos.BDTable("Select IdReqR, TipoReq From ReqNReqR Where IdReq = " + fila[1].ToString() + " Order By TipoReq;");
                    Req = DTReqInfo.Select(CadenaBusqueda(tabla, "1"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    Req = DTReqNFun.Select(CadenaBusqueda(tabla, "2"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    Req = DTReqFun.Select(CadenaBusqueda(tabla, "3"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    ClsWord.ReqNFun(oWord, oDoc, fila, Auto, Fuen, Obj, ALReq);
                    ALReq.Clear();
                    PBProg.Value++; DoEvents();
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int ReqFun()
        {
            DataRow fila, fila2;
            DataRow[] Auto, Fuen, Obj, Req, Act;
            ArrayList ALReq = new ArrayList();
            DataTable tabla, tabla2, tablaPaq;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            Microsoft.Office.Interop.Word.Paragraph oPara;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTReqFun.Rows.Count - 1; DoEvents();
                tablaPaq = ClsBaseDatos.BDTable("Select * From Paquetes Order by Categoria Desc, Nombre");
                for (int j = 0; (j <= tablaPaq.Rows.Count - 1); j++)
                {
                    fila2 = tablaPaq.Rows[j];
                    oPara = oDoc.Content.Paragraphs.Add(oDoc.Bookmarks["\\endofdoc"].Range);
                    oPara.Range.Text = fila2[1].ToString();
                    oPara.Range.Font.Bold = 1;
                    oPara.Range.Font.Size = 16;
                    oPara.Format.SpaceAfter = 20;
                    oPara.Range.InsertParagraphAfter();
                    oPara.Range.Font.Size = 11;
                    oPara.Range.Font.Bold = 0;
                    for (int i = 0; (i <= DTReqFun.Rows.Count - 1); i++)
                    {
                        fila = DTReqFun.Rows[i];
                        if (fila2[0].ToString() == fila[4].ToString())
                        {
                            Act = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAct From ReqAct Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                            Auto = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAutor From ReqAuto Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                            Fuen = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdFuen From ReqFuen Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                            Obj = DTObjetivos.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdObj From ReqObj Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                            tabla = ClsBaseDatos.BDTable("Select IdReqR, TipoReq From ReqReqR Where IdReq = " + fila[1].ToString() + " Order By TipoReq;");
                            Req = DTReqInfo.Select(CadenaBusqueda(tabla, "1"), "IdW");
                            foreach (DataRow fReq in Req) ALReq.Add(fReq);
                            Req = DTReqNFun.Select(CadenaBusqueda(tabla, "2"), "IdW");
                            foreach (DataRow fReq in Req) ALReq.Add(fReq);
                            Req = DTReqFun.Select(CadenaBusqueda(tabla, "3"), "IdW");
                            foreach (DataRow fReq in Req) ALReq.Add(fReq);
                            tabla = ClsBaseDatos.BDTable("Select Descrip From ReqSecNor Where IdReq = " + fila[1].ToString() + " Order by Id;");
                            tabla2 = ClsBaseDatos.BDTable("Select Descrip From ReqSecExc Where IdReq = " + fila[1].ToString() + " Order by Id;");
                            ClsWord.ReqFun(oWord, oDoc, fila, Auto, Fuen, Obj, ALReq, Act, tabla, tabla2);
                            ALReq.Clear();
                            PBProg.Value++; DoEvents();
                        }
                    }
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int ReqInfo()
        {
            DataRow fila;
            DataRow[] Auto, Fuen, Obj, Req;
            ArrayList ALReq = new ArrayList();
            DataTable tabla;
            Microsoft.Office.Interop.Word.Application oWord;
            Microsoft.Office.Interop.Word.Document oDoc;
            try
            {
                oWord = new Microsoft.Office.Interop.Word.Application();
                oDoc = oWord.Documents.Add();
                PBProg.Value = 0; PBProg.Maximum = DTReqInfo.Rows.Count - 1; DoEvents();
                for (int i = 0; (i <= DTReqInfo.Rows.Count - 1); i++)
                {
                    fila = DTReqInfo.Rows[i];
                    Auto = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdAutor From ReqIAuto Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    Fuen = DTGrupo.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdFuen From ReqIFuen Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    Obj = DTObjetivos.Select(CadenaBusqueda(ClsBaseDatos.BDTable("Select IdObj From ReqIObj Where IdReq = " + fila[1].ToString() + ";")), "IdW");
                    tabla = ClsBaseDatos.BDTable("Select IdReqR, TipoReq From ReqIReqR Where IdReq = " + fila[1].ToString() + " Order By TipoReq;");
                    Req = DTReqInfo.Select(CadenaBusqueda(tabla, "1"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    Req = DTReqNFun.Select(CadenaBusqueda(tabla, "2"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    Req = DTReqFun.Select(CadenaBusqueda(tabla, "3"), "IdW");
                    foreach (DataRow fReq in Req) ALReq.Add(fReq);
                    tabla = ClsBaseDatos.BDTable("Select Descrip From ReqIDatEsp Where IdReq = " + fila[1].ToString() + " Order by Id;");
                    ClsWord.ReqInfo(oWord, oDoc, fila, Auto, Fuen, Obj, ALReq, tabla);
                    ALReq.Clear();
                    PBProg.Value++; DoEvents();
                }
                oWord.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private int Traza()
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook wb;
            Microsoft.Office.Interop.Excel.Worksheet ws1, ws2, ws3;
            Microsoft.Office.Interop.Excel.Range aRange;
            DataTable tabla; DataRow fila;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                wb = xlApp.Workbooks.Add();
                wb.Worksheets.Add();
                wb.Worksheets.Add();
                ws1 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];
                ws1.Name = ClsConf.Idioma.Equals(DefValues.Ingles) ? Ingles.ReqFun : Español.ReqFun;
                ws2 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[2];
                ws2.Name = ClsConf.Idioma.Equals(DefValues.Ingles) ? Ingles.ReqNFun : Español.ReqNFun;
                ws3 = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[3];
                ws3.Name = ClsConf.Idioma.Equals(DefValues.Ingles) ? Ingles.ReqInfo : Español.ReqInfo;
                aRange = ClsWord.DarFormatoExcel(ws1, DTReqFun, DTObjetivos.Rows.Count + 1);
                PBProg.Value = 0; PBProg.Maximum = DTReqFun.Rows.Count - 1; DoEvents();
                for (int i = 0; i <= (DTReqFun.Rows.Count - 1); i++)
                {
                    fila = DTReqFun.Rows[i];
                    tabla = ClsBaseDatos.BDTable("Select IdObj From ReqObj Where IdReq = " + fila[1].ToString() + ";");
                    ClsWord.RellenarFila(aRange, ClsWord.ObjRelacionados(tabla, DTObjetivos), i + 1, fila[0].ToString());
                    PBProg.Value++; DoEvents();
                }
                aRange = ClsWord.DarFormatoExcel(ws2, DTReqNFun, DTObjetivos.Rows.Count + 1);
                PBProg.Value = 0; PBProg.Maximum = DTReqNFun.Rows.Count - 1; DoEvents();
                for (int i = 0; i <= (DTReqNFun.Rows.Count - 1); i++)
                {
                    fila = DTReqNFun.Rows[i];
                    tabla = ClsBaseDatos.BDTable("Select IdObj From ReqNObj Where IdReq = " + fila[1].ToString() + ";");
                    ClsWord.RellenarFila(aRange, ClsWord.ObjRelacionados(tabla, DTObjetivos), i + 1, fila[0].ToString());
                    PBProg.Value++; DoEvents();
                }
                aRange = ClsWord.DarFormatoExcel(ws3, DTReqInfo, DTObjetivos.Rows.Count + 1);
                PBProg.Value = 0; PBProg.Maximum = DTReqInfo.Rows.Count - 1; DoEvents();
                for (int i = 0; i <= (DTReqInfo.Rows.Count - 1); i++)
                {
                    fila = DTReqInfo.Rows[i];
                    tabla = ClsBaseDatos.BDTable("Select IdObj From ReqIObj Where IdReq = " + fila[1].ToString() + ";"); //objetivos donde esta relacionado
                    ClsWord.RellenarFila(aRange, ClsWord.ObjRelacionados(tabla, DTObjetivos), i + 1, fila[0].ToString());
                    PBProg.Value++; DoEvents();
                }
                xlApp.Visible = true;
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        private string RellenarCeros(string Cadena)
        {
            while (Cadena.Length < 3) Cadena = "0" + Cadena;
            return Cadena;
        }
        private string DetEstado(int Estado, int Tipo)
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                if (Tipo == 1)
                {
                    if (Estado == 1) return Ingles.VLow;
                    else if (Estado == 2) return Ingles.Low;
                    else if (Estado == 3) return Ingles.Medium;
                    else if (Estado == 4) return Ingles.High;
                    else return Ingles.VHigh;
                }
                else if (Tipo == 2)
                {
                    if (Estado == 1) return Ingles.Low;
                    else if (Estado == 2) return Ingles.Medium;
                    else return Ingles.High;
                }
                else if (Tipo == 3)
                {
                    if (Estado == 1) return Ingles.Verified;
                    else return Ingles.NVerified;
                }
                else
                {
                    if (Estado == 1) return Ingles.yes;
                    else return Ingles.No;
                }
            }
            else
            {
                if (Tipo == 1)
                {
                    if (Estado == 1) return Español.MBaja;
                    else if (Estado == 2) return Español.Baja;
                    else if (Estado == 3) return Español.Media;
                    else if (Estado == 4) return Español.Alta;
                    else return Español.MAlta;
                }
                else if (Tipo == 2)
                {
                    if (Estado == 1) return Español.Baja;
                    else if (Estado == 2) return Español.Media;
                    else return Español.Alta;
                }
                else if (Tipo == 3)
                {
                    if (Estado == 1) return Español.Verificado;
                    else return Español.NVerificado;
                }
                else
                {
                    if (Estado == 1) return Español.Si;
                    else return Español.No;
                }
            }
        }
        private string CadenaBusqueda(DataTable tabla, string flgReq = Ingles.No)
        {
            DataRow fila; string Busqueda = "Id = ";
            if (flgReq.Equals(Ingles.No))
            {
                for (int j = 0; (j <= tabla.Rows.Count - 1); j++)
                {
                    fila = tabla.Rows[j];
                    if (j < tabla.Rows.Count - 1) Busqueda += fila[0].ToString() + " or Id = ";
                    else Busqueda += fila[0].ToString();
                }
            }
            else
            {
                for (int j = 0; (j <= tabla.Rows.Count - 1); j++)
                {
                    fila = tabla.Rows[j];
                    if (fila[1].ToString() == flgReq) Busqueda += fila[0].ToString() + " or Id = ";
                }
                if (Busqueda.Length > 10) Busqueda = Busqueda.Substring(0, Busqueda.Length - 9);
            }
            if (Busqueda == "Id = ") return "Id = -1";
            else return Busqueda;
        }
    }
}