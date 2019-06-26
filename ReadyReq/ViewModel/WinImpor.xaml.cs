/*
 * Autor: Arturo Balleros Albillo
 */
using Microsoft.Win32;
using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ReadyReq.ViewModel
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
            ClsConf.Idioma = DefValues.Español; Idioma();
            GridProg.Visibility = Visibility.Hidden;
        }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        private void ButClick(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("ButRuta"))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = StrMenArc + "|*.RR";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true) LblRutaBD.Content = openFileDialog.FileName;
            }
            if (ctrl.Name.Equals("ButCrear"))
                if (!string.IsNullOrEmpty(LblRutaBD.Content + ""))
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
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                ButRuta.Content = Ingles.Browse;
                ButCrear.Content = Ingles.Start;
                LblRuta.Content = Ingles.Path;
                Title = Ingles.Import;
                StrMenArc = Ingles.MenArc;
                StrErrFic = Ingles.ErrFic;
                StrErrBas = Ingles.ErrBas;
            }
            else
            {
                ButRuta.Content = Español.Examinar;
                ButCrear.Content = Español.Comenzar;
                LblRuta.Content = Español.Ruta;
                Title = Español.Importar;
                StrMenArc = Español.MenArc;
                StrErrFic = Español.ErrFic;
                StrErrBas = Español.ErrBas;
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
                    if (!string.IsNullOrEmpty(line)) BaseDatos.Add(ClsConf.Desencriptar(line));
                }
                sr.Close();
            }
            catch { return -1; }
            PBProg.Maximum = cont--;
            foreach (string line in BaseDatos)
                if (!string.IsNullOrEmpty(line))
                {
                    if (!ClsBaseDatos.BDBool(line))
                        if (!line.Substring(0, 6).Equals("Delete"))
                        {
                            MessageBox.Show(line);
                            return -2;
                        }
                    PBProg.Value++; DoEvents();
                }
            return 0;
        }
    }
}