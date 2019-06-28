/*
 * Autor: Arturo Balleros Albillo
 */
using Microsoft.Win32;
using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReadyReq.ViewModel
{
    public partial class WinConf : Window
    {
        Control ctrl = new Control();
        string StrMenArc;
        string StrMenPar;
        string StrMenFic;
        string StrMenConBD = "Error al conectarse a la base de datos, corríjalo en 'Configuración', para continuar.\n\nError when connecting to the database, correct it in 'Configuration', to continue.";
        string StrMenSal = "¿Desea cerrar la ventana?\n\nDo you want to close de windows?";
        public WinConf()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            IniciarInterfaz();
        }
        private void LblClick(object sender, MouseButtonEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("LblEsp"))
            {
                LblEspF.Background = Brushes.Aqua;
                LblIngF.Background = Brushes.White;
                ClsConf.Idioma = DefValues.Español; ;
                Idioma();
            }
            if (ctrl.Name.Equals("LblIng"))
            {
                LblEspF.Background = Brushes.White;
                LblIngF.Background = Brushes.Aqua;
                ClsConf.Idioma = DefValues.Ingles;
                Idioma();
            }
        }
        private void IniciarInterfaz()
        {
            //Poner imagenes
            ImageBrush ImgEsp = new ImageBrush();
            ImageBrush ImgIng = new ImageBrush();
            try
            {
                ImgEsp.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Esp.png"));
                ImgIng.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Ing.png"));
                LblEsp.Background = ImgEsp;
                LblIng.Background = ImgIng;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

            //Asignar Idioma
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                LblEspF.Background = Brushes.White;
                LblIngF.Background = Brushes.Aqua;
            }
            else
            {
                LblEspF.Background = Brushes.Aqua;
                LblIngF.Background = Brushes.White;
            }
            Idioma();

            //Asignar Base de Datos
            if (ClsConf.TipoBD.Equals(DefValues.MySql))
            {
                RBSql.IsChecked = true;
                VisibleGrid(DefValues.MySql);
                TxtHost.Text = ClsConf.Host;
                TxtUsu.Text = ClsConf.Usuario;
                TxtPassMS.Password = ClsConf.PassMySql;
                TxtBDMS.Text = ClsConf.BDMySql;
                TxtPortMS.Text = ClsConf.PortMySql;
            }
            else
            {
                RBAccess.IsChecked = true;
                VisibleGrid(DefValues.Access);
                LblRutaBD.Content = ClsConf.RutaAcc;
                if (ClsConf.FlgPass) TxtPassBD.Password = ClsConf.PassAcc.Substring(29, ClsConf.PassAcc.Length - 30);
                else TxtPassBD.Password = string.Empty;
            }
        }
        private void VisibleGrid(string opt)
        {
            if (opt.Equals(DefValues.MySql))
            {
                GridSql.Visibility = Visibility.Visible;
                GridAccess.Visibility = Visibility.Hidden;
            }
            else
            {
                GridSql.Visibility = Visibility.Hidden;
                GridAccess.Visibility = Visibility.Visible;
            }
        }
        private void ButClick(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("ButRuta"))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = StrMenArc + "|*.accdb";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true) LblRutaBD.Content = openFileDialog.FileName;
            }

            if (ctrl.Name.Equals("ButSalir"))
            {
                if (RBSql.IsChecked == true)
                {
                    if (string.IsNullOrEmpty(TxtHost.Text) || string.IsNullOrEmpty(TxtUsu.Text) || string.IsNullOrEmpty(TxtPassMS.Password) || string.IsNullOrEmpty(TxtBDMS.Text) || string.IsNullOrEmpty(TxtPortMS.Text))
                    {
                        MessageBox.Show(StrMenPar);
                        if (MessageBox.Show(StrMenSal, Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes) { Close(); return; } else return;
                    }
                    else
                    {
                        ClsConf.TipoBD = DefValues.MySql;
                        ClsConf.Host = TxtHost.Text;
                        ClsConf.Usuario = TxtUsu.Text;
                        ClsConf.PassMySql = TxtPassMS.Password;
                        ClsConf.BDMySql = TxtBDMS.Text;
                        ClsConf.PortMySql = TxtPortMS.Text;
                    }
                }
                else
                {
                    string RutaBD = "" + LblRutaBD.Content;

                    if (string.IsNullOrEmpty(RutaBD))
                    {
                        MessageBox.Show(StrMenPar);
                        if (MessageBox.Show(StrMenSal, Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes) { Close(); return; } else return;
                    }
                    else
                    {
                        ClsConf.TipoBD = DefValues.Access;
                        ClsConf.RutaAcc = RutaBD;
                        if (!string.IsNullOrEmpty(TxtPassBD.Password))
                        {
                            ClsConf.FlgPass = true;
                            ClsConf.PassAcc = TxtPassBD.Password;
                        }
                        else
                            ClsConf.FlgPass = false;
                    }
                }

                if (ClsConf.EscribirConf() == -1)
                {
                    MessageBox.Show(StrMenFic);
                    if (MessageBox.Show(StrMenSal, Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Close(); else return;
                }
                else
                {
                    string OptionCon = Ingles.No;

                    if (ClsConf.TipoBD.Equals(DefValues.MySql))
                        ClsConf.ConexionMySql();
                    else
                    {
                        OptionCon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ClsConf.RutaAcc + "; ";
                        if (ClsConf.FlgPass) OptionCon += "Jet OLEDB:Database Password =" + ClsConf.PassAcc + ";";
                        else OptionCon += "Persist Security Info = False;";
                    }

                    if (!ClsBaseDatos.BDConexion(OptionCon))
                    {
                        MessageBox.Show(StrMenConBD);
                        if (MessageBox.Show(StrMenSal, Title, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Close();
                    }
                    else Close();
                }
            }
        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            try
            {
                if (ctrl.Name.Equals("RBSql")) VisibleGrid(DefValues.MySql);
                if (ctrl.Name.Equals("RBAccess")) VisibleGrid(DefValues.Access);
            }
            catch { }
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                Title = Ingles.Configuration;
                ButRuta.Content = Ingles.Browse;
                ButSalir.Content = Ingles.SaveExit;
                LblHost.Content = Ingles.Server;
                LblUsu.Content = Ingles.User;
                LblPassSql.Content = LblPassBD.Content = Ingles.Password;
                LblBDSql.Content = Ingles.Database;
                LblPortSql.Content = Ingles.Port;
                LblRuta.Content = Ingles.Path;
                Title = Ingles.Configuration;
                StrMenArc = Ingles.DBFile;
                StrMenPar = Ingles.NPMenPar;
                StrMenFic = Ingles.NPMenFic;
                StrMenConBD = Ingles.MenConBD;
                StrMenSal = Ingles.MenSal;
            }
            else
            {
                Title = Español.Configuración;
                ButRuta.Content = Español.Examinar;
                ButSalir.Content = Español.GuarSal;
                LblHost.Content = Español.Servidor;
                LblUsu.Content = Español.Usuario;
                LblPassSql.Content = LblPassBD.Content = Español.Contaseña;
                LblBDSql.Content = Español.Base_Datos;
                LblPortSql.Content = Español.Puerto;
                LblRuta.Content = Español.Ruta;
                Title = Español.Configuración;
                StrMenArc = Español.DBFich;
                StrMenPar = Español.NPMenPar;
                StrMenFic = Español.NPMenFic;
                StrMenConBD = Español.MenConBD;
                StrMenSal = Español.MenSal;
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = (Control)sender;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtHost") && !string.IsNullOrEmpty(TxtHost.Text)) TxtUsu.Focus();
                if (ctrl.Name.Equals("TxtUsu") && !string.IsNullOrEmpty(TxtUsu.Text)) TxtPassMS.Focus();
                if (ctrl.Name.Equals("TxtPassMS") && !string.IsNullOrEmpty(TxtPassMS.Password)) TxtBDMS.Focus();
                if (ctrl.Name.Equals("TxtBDMS") && !string.IsNullOrEmpty(TxtBDMS.Text)) TxtPortMS.Focus();
                if (ctrl.Name.Equals("TxtPortMS") && !string.IsNullOrEmpty(TxtPortMS.Text)) ButSalir.Focus();
                if (ctrl.Name.Equals("TxtPassBD") && !string.IsNullOrEmpty(TxtPassBD.Password)) ButSalir.Focus();
            }
        }
    }
}