using ReadyReq.Model;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WinForms = System.Windows.Forms;

namespace ReadyReq.ViewModel
{
    public partial class WinExpor : Window
    {
        Control ctrl = new Control();
        string StrErrFic;
        public WinExpor()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            ClsConf.Idioma = "Español"; Idioma();
            GridProg.Visibility = Visibility.Hidden;
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        private void ButClick(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "ButRuta")
            {
                WinForms.FolderBrowserDialog browse = new WinForms.FolderBrowserDialog();
                browse.ShowDialog();
                LblRutaBD.Content = browse.SelectedPath;
            }
            if (ctrl.Name == "ButCrear")
            {
                if ((string.IsNullOrEmpty(TxtNomBD.Text) == false) && (string.IsNullOrEmpty(LblRutaBD.Content + "") == false))
                {
                    GridControl.Visibility = Visibility.Hidden;
                    GridProg.Visibility = Visibility.Visible;
                    int resultado = Exportar();
                    if (resultado == -1) MessageBox.Show(StrErrFic);
                    GridControl.Visibility = Visibility.Visible;
                    GridProg.Visibility = Visibility.Hidden;
                }
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (string.IsNullOrEmpty(TxtNomBD.Text) == false))
                ButCrear.Focus();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma == "Ingles")
            {
                //Botones
                ButRuta.Content = "Browse...";
                ButCrear.Content = "Create";

                //Label
                LblRuta.Content = "Path";
                LblNomBD.Content = "Name";

                //Window
                Title = "Export";

                //Mensajes
                StrErrFic = "Error when inserting into the file";
            }
            else
            {
                //Botones
                ButRuta.Content = "Examinar...";
                ButCrear.Content = "Crear";

                //Label
                LblRuta.Content = "Ruta";
                LblNomBD.Content = "Nombre";

                //Window
                Title = "Exportar";

                //Mensajes
                StrErrFic = "Error al insertar en el fichero";
            }
        }
        private int Exportar()
        {
            try
            {
                StreamWriter sw = new StreamWriter(LblRutaBD.Content + "\\" + TxtNomBD.Text + ".RR");
                DataTable tabla = new DataTable();
                DataRow fila;
                PBProg1.Maximum = 28; PBProg1.Value = 0;

                sw.WriteLine(ClsConf.Encriptar("Delete from ReqNReqR;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqNObj;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqNFuen;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqNAuto;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqIDatEsp;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqIReqR;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqIObj;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqIFuen;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqIAuto;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqSecExc;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqSecNor;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqAct;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqReqR;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqObj;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqFuen;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqAuto;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ObjSubobj;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ObjFuen;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ObjAuto;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ActFuen;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ActAuto;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqNFunc;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqFun;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from ReqInfo;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from Objetivos;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from Actores;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from Paquetes;")); 
                sw.WriteLine(ClsConf.Encriptar("Delete from Grupo;"));
                sw.WriteLine(ClsConf.Encriptar("Delete from Estim;"));

                //Generales
                tabla = ClsBaseDatos.BDTable("select * From Grupo");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into Grupo values(" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "','" + fila[3].ToString() + "'," + fila[4].ToString() + "," + fila[5].ToString() + ",'" + fila[6].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From Paquetes");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into Paquetes values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "'," + fila[2].ToString() + ",'" + fila[3].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From Actores");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into Actores values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "'," + fila[3].ToString() + ",'" + fila[4].ToString() + "'," + fila[5].ToString() + ",'" + fila[6].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From Objetivos");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into Objetivos values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "'," + fila[3].ToString() + "," + fila[4].ToString() + "," + fila[5].ToString() + "," + fila[6].ToString() + "," + fila[7].ToString() + ",'" + fila[8].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqInfo");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqInfo values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "'," + fila[3].ToString() + "," + fila[4].ToString() + "," + fila[5].ToString() + "," + fila[6].ToString() + "," + fila[7].ToString() + "," + fila[8].ToString() + "," + fila[9].ToString() + "," + fila[10].ToString() + "," + fila[11].ToString() + ",'" + fila[12].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqFun");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqFun values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "'," + fila[3].ToString() + ",'" + fila[4].ToString() + "','" + fila[5].ToString() + "'," + fila[6].ToString() + "," + fila[7].ToString() + "," + fila[8].ToString() + "," + fila[9].ToString() + "," + fila[10].ToString() + ",'" + fila[11].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqNFunc");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqNFunc values (" + fila[0].ToString() + ",'" + fila[1].ToString() + "','" + fila[2].ToString() + "'," + fila[3].ToString() + "," + fila[4].ToString() + "," + fila[5].ToString() + "," + fila[6].ToString() + "," + fila[7].ToString() + ",'" + fila[8].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Actores
                tabla = ClsBaseDatos.BDTable("select * From ActAuto");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ActAuto values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ActFuen");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ActFuen values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Objetivos
                tabla = ClsBaseDatos.BDTable("select * From ObjAuto");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ObjAuto values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ObjFuen");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ObjFuen values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ObjSubobj");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ObjSubobj values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Requisitos funcionales
                tabla = ClsBaseDatos.BDTable("select * From ReqAuto");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqAuto values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqFuen");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqFuen values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqObj");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqObj values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqReqR");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqReqR values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + "," + fila[3].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqAct");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqAct values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqSecNor");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqSecNor values (" + fila[0].ToString() + "," + fila[1].ToString() + ",'" + fila[2].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqSecExc");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqSecExc values (" + fila[0].ToString() + "," + fila[1].ToString() + ",'" + fila[2].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Requisitos de informacion
                tabla = ClsBaseDatos.BDTable("select * From ReqIAuto");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqIAuto values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqIFuen");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqIFuen values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqIObj");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqIObj values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqIReqR");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqIReqR values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + "," + fila[3].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqIDatEsp");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqIDatEsp values (" + fila[0].ToString() + "," + fila[1].ToString() + ",'" + fila[2].ToString() + "');")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Requisitos no funcionales
                tabla = ClsBaseDatos.BDTable("select * From ReqNAuto");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqNAuto values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqNFuen");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqNFuen values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqNObj");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqNObj values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                tabla = ClsBaseDatos.BDTable("select * From ReqNReqR");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into ReqNReqR values (" + fila[0].ToString() + "," + fila[1].ToString() + "," + fila[2].ToString() + "," + fila[3].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();

                //Estimaciones
                tabla = ClsBaseDatos.BDTable("select * From Estim");
                PBProg2.Maximum = tabla.Rows.Count - 1;
                for (int i = 0; (i <= tabla.Rows.Count - 1); i++)
                {
                    fila = tabla.Rows[i];
                    sw.WriteLine(ClsConf.Encriptar("Insert into Estim values ('" + fila[0].ToString() + "'," + fila[1].ToString() + ");")); PBProg2.Value = i; DoEvents();
                }
                PBProg1.Value++; DoEvents();
                sw.Close();
                return 0;
            }
            catch
            {
                return -1;
            }
        }
    }
}