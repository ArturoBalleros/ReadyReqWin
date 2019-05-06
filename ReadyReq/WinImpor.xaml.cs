using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ReadyReq
{
    public partial class WinImpor : Window
    {
        Control ctrl = new Control();
        string StrMenArc;
        string StrErrFic;
        string StrErrBas;
        public WinImpor()
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
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = StrMenArc + "|*.RR";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true)
                    LblRutaBD.Content = openFileDialog.FileName;
            }
            if (ctrl.Name == "ButCrear")
            {
                if (string.IsNullOrEmpty(LblRutaBD.Content + "") == false)
                {
                    GridControl.Visibility = Visibility.Hidden;
                    GridProg.Visibility = Visibility.Visible;
                    int resultado = Importar();
                    if (resultado == -1) MessageBox.Show(StrErrFic);
                    if (resultado == -2) MessageBox.Show(StrErrBas);
                    GridControl.Visibility = Visibility.Visible;
                    GridProg.Visibility = Visibility.Hidden;
                }
            }
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

                //Window
                Title = "Import";

                //Mensajes
                StrMenArc = "ReadyReq (.RR)";
                StrErrFic = "Error reading the file";
                StrErrBas = "Error when inserting into the database";
            }
            else
            {
                //Botones
                ButRuta.Content = "Examinar...";
                ButCrear.Content = "Crear";

                //Label
                LblRuta.Content = "Ruta";

                //Window
                Title = "Importar";

                //Mensajes
                StrMenArc = "ReadyReq (.RR)";
                StrErrFic = "Error al leer el fichero";
                StrErrBas = "Error al insertar en la base de datos";
            }
        }
        private int Importar()
        {
            ArrayList BaseDatos = new ArrayList();
            int cont = 0;
            try
            {
                StreamReader sr = new StreamReader(LblRutaBD.Content + "");
                string line = "";

                while (line != null)
                {
                    cont++;
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line) == false) BaseDatos.Add(ClsConf.Desencriptar(line));
                }
                sr.Close();
            }
            catch
            {
                return -1;
            }
            PBProg.Maximum = cont--;
            foreach (string line in BaseDatos)
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    if (ClsBaseDatos.BDBool(line) == false)
                    
                        if (!line.Substring(0, 6).Equals("Delete")) {
                            MessageBox.Show(line);
                            return -2;
                    }
                    PBProg.Value++; DoEvents();
                }
            }
            return 0;
        }
    }
}