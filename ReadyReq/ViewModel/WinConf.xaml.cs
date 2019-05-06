using Microsoft.Win32;
using ReadyReq.Model;
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
            ctrl = ((Control)sender);
            if (ctrl.Name == "LblEsp")
            {
                LblEspF.Background = Brushes.Aqua;
                LblIngF.Background = Brushes.White;
                ClsConf.Idioma = "Español";
                Idioma();
            }
            if (ctrl.Name == "LblIng")
            {
                LblEspF.Background = Brushes.White;
                LblIngF.Background = Brushes.Aqua;
                ClsConf.Idioma = "Ingles";
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
            if (ClsConf.Idioma == "Ingles")
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
            if (ClsConf.TipoBD == "MySql")
            {
                RBSql.IsChecked = true;
                VisibleGrid("MySql");
                TxtHost.Text = ClsConf.Host;
                TxtUsu.Text = ClsConf.Usuario;
                TxtPassMS.Password = ClsConf.PassMySql;
                TxtBDMS.Text = ClsConf.BDMySql;
                TxtPortMS.Text = ClsConf.PortMySql;
            }
            else
            {
                RBAccess.IsChecked = true;
                VisibleGrid("Access");
                LblRutaBD.Content = ClsConf.RutaAcc;
                if (ClsConf.FlgPass == true)
                    TxtPassBD.Password = ClsConf.PassAcc.Substring(29, ClsConf.PassAcc.Length - 30);
                else
                    TxtPassBD.Password = String.Empty;
            }
        }
        private void VisibleGrid(string opt)
        {
            if (opt == "MySql")
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
            ctrl = ((Control)sender);
            if (ctrl.Name == "ButRuta")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = StrMenArc + "|*.accdb";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog.ShowDialog() == true)
                    LblRutaBD.Content = openFileDialog.FileName;
            }

            if (ctrl.Name == "ButSalir")
            {
                if (RBSql.IsChecked == true)
                {
                    if ((string.IsNullOrEmpty(TxtHost.Text) == true) || (string.IsNullOrEmpty(TxtUsu.Text) == true) || (string.IsNullOrEmpty(TxtPassMS.Password) == true) || (string.IsNullOrEmpty(TxtBDMS.Text) == true) || (string.IsNullOrEmpty(TxtPortMS.Text) == true))
                    {
                        MessageBox.Show(StrMenPar);
                        return;
                    }
                    else
                    {
                        ClsConf.TipoBD = "MySql";
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

                    if (string.IsNullOrEmpty(RutaBD) == true)
                    {
                        MessageBox.Show(StrMenPar);
                        return;
                    }
                    else
                    {
                        ClsConf.TipoBD = "Access";
                        ClsConf.RutaAcc = RutaBD;
                        if (string.IsNullOrEmpty(TxtPassBD.Password) == false)
                        {
                            ClsConf.FlgPass = true;
                            ClsConf.PassAcc = TxtPassBD.Password;
                        }
                        else
                        {
                            ClsConf.FlgPass = false;
                        }
                    }
                }

                if (ClsConf.EscribirConf() == -1)
                    MessageBox.Show(StrMenFic);
                else
                {
                    string OptionCon = "No";

                    if (ClsConf.TipoBD == "MySql")
                        ClsConf.ConexionMySql();
                    else
                    {
                        OptionCon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ClsConf.RutaAcc + "; ";
                        if (ClsConf.FlgPass == true)
                            OptionCon += "Jet OLEDB:Database Password =" + ClsConf.PassAcc + ";";
                        else
                            OptionCon += "Persist Security Info = False;";
                    }

                    if (ClsBaseDatos.BDConexion(OptionCon) == false)
                        MessageBox.Show(StrMenConBD);
                    else
                        Close();
                }
            }
        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            try
            {
                if (ctrl.Name == "RBSql") VisibleGrid("MySql");
                if (ctrl.Name == "RBAccess") VisibleGrid("Access");
            }
            catch { }
        }
        private void Idioma()
        {
            if (ClsConf.Idioma == "Ingles")
            {
                //Botones
                ButRuta.Content = "Browse...";
                ButSalir.Content = "Save/Exit";

                //Label
                LblHost.Content = "Server";
                LblUsu.Content = "User";
                LblPassSql.Content = "Password";
                LblBDSql.Content = "Database";
                LblPortSql.Content = "Port";

                LblRuta.Content = "Path";
                LblPassBD.Content = "Password";

                //Window
                Title = "Configuration";

                //Mensajes
                StrMenArc = "Databases (.accdb)";
                StrMenPar = "There are empty parameters";
                StrMenFic = "Unexpected error saving configuration";
                StrMenConBD = "Error when connecting to the database, correct it, to continue.";
            }
            else
            {
                //Botones
                ButRuta.Content = "Examinar...";
                ButSalir.Content = "Guardar/Salir";

                //Label
                LblHost.Content = "Servidor";
                LblUsu.Content = "Usuario";
                LblPassSql.Content = "Contaseña";
                LblBDSql.Content = "Base de datos";
                LblPortSql.Content = "Puerto";

                LblRuta.Content = "Ruta";
                LblPassBD.Content = "Contaseña";

                //Window
                Title = "Configuración";

                //Mensajes
                StrMenArc = "Bases de datos (.accdb)";
                StrMenPar = "Hay parámetros vacíos";
                StrMenFic = "Error inesperado al guardar la configuración";
                StrMenConBD = "Error al conectarse a la base de datos, corríjalo, para continuar.";
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = ((Control)sender);
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtHost") && (string.IsNullOrEmpty(TxtHost.Text) == false)) TxtUsu.Focus();
                if ((ctrl.Name == "TxtUsu") && (string.IsNullOrEmpty(TxtUsu.Text) == false)) TxtPassMS.Focus();
                if ((ctrl.Name == "TxtPassMS") && (string.IsNullOrEmpty(TxtPassMS.Password) == false)) TxtBDMS.Focus();
                if (ctrl.Name == "TxtBDMS" && (string.IsNullOrEmpty(TxtBDMS.Text) == false)) TxtPortMS.Focus();
                if ((ctrl.Name == "TxtPortMS") && (string.IsNullOrEmpty(TxtPortMS.Text) == false)) ButSalir.Focus();
                if (ctrl.Name == "TxtPassBD" && (string.IsNullOrEmpty(TxtPassBD.Password) == false)) ButSalir.Focus();
            }
        }
    }
}