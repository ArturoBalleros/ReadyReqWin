using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace ReadyReq
{
    public partial class WinNuePro : Window
    {
        Control ctrl = new Control();
        ClsNuePro NuevoProyecto = new ClsNuePro();
        string StrConf;
        string StrMenPar;
        string StrMenCon;
        string StrMenExi;
        string StrMenPre;
        string StrMenFic;
        string StrMenCorrec;
        public WinNuePro()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            RBSql.IsChecked = true;
            VisibleGrid("MySql");
            Idioma();
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
                WinForms.FolderBrowserDialog browse = new WinForms.FolderBrowserDialog();
                browse.ShowDialog();
                LblRutaBD.Content = browse.SelectedPath;
            }
            if (ctrl.Name == "ButCrear")
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
                        NuevoProyecto.TipoBD = "MySql";
                        NuevoProyecto.Host = TxtHost.Text;
                        NuevoProyecto.Usuario = TxtUsu.Text;
                        NuevoProyecto.PassMySql = TxtPassMS.Password;
                        NuevoProyecto.BDMySql = TxtBDMS.Text;
                        NuevoProyecto.PortMySql = TxtPortMS.Text;
                        NuevoProyecto.CreateMySql = (ChkCreBase.IsChecked == true) ? true : false;
                    }
                }
                else
                {
                    //access
                    string RutaBD = "" + LblRutaBD.Content;
                    if (string.IsNullOrEmpty(RutaBD) == true)
                    {
                        MessageBox.Show(StrMenPar);
                        return;
                    }
                    else
                    {
                        NuevoProyecto.TipoBD = "Access";
                        NuevoProyecto.RutaAcc = RutaBD + "\\" + TxtNomBD.Text + ".accdb";
                        if (string.IsNullOrEmpty(TxtPassBD.Password) == false)
                        {
                            NuevoProyecto.FlgPass = true;
                            NuevoProyecto.PassAcc = "Jet OLEDB:Database Password =" + TxtPassBD.Password + ";";
                        }
                        else
                        {
                            NuevoProyecto.FlgPass = false;
                            NuevoProyecto.PassAcc = "Persist Security Info = False;";
                        }
                    }
                }

                int resultado = NuevoProyecto.CrearBase();
                if (resultado == -1) MessageBox.Show(StrMenCon);
                else if (resultado == -2) MessageBox.Show(StrMenExi);
                else
                {
                    if (MessageBox.Show(StrMenPre, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        ClsConf.TipoBD = NuevoProyecto.TipoBD;
                        if (RBSql.IsChecked == true)
                        {
                            ClsConf.Host = NuevoProyecto.Host;
                            ClsConf.Usuario = NuevoProyecto.Usuario;
                            ClsConf.PassMySql = NuevoProyecto.PassMySql;
                            ClsConf.BDMySql = NuevoProyecto.BDMySql;
                            ClsConf.PortMySql = NuevoProyecto.PortMySql;
                            ClsConf.ConexionMySql();
                        }
                        else
                        {
                            ClsConf.RutaAcc = NuevoProyecto.RutaAcc;
                            ClsConf.FlgPass = NuevoProyecto.FlgPass;
                            if (NuevoProyecto.FlgPass == true)
                                ClsConf.PassAcc = NuevoProyecto.PassAcc.Substring(29, NuevoProyecto.PassAcc.Length - 30);
                            else
                                ClsConf.PassAcc = String.Empty;
                        }
                        if (ClsConf.EscribirConf() == -1)
                            MessageBox.Show(StrMenFic);
                        MessageBox.Show(StrMenCorrec);
                    }
                }
                Close();
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
                ButCrear.Content = "Create";

                //Label
                LblHost.Content = "Server";
                LblUsu.Content = "User";
                LblPassSql.Content = "Password";
                LblBDSql.Content = "Database";
                LblPortSql.Content = "Port";

                LblRuta.Content = "Path";
                LblNomBD.Content = "Name";
                LblPassBD.Content = "Password";

                //Check
                ChkCreBase.Content = "Create database?";

                //Window
                Title = "New project";

                //Mensajes
                StrConf = "Confirmation";
                StrMenPar = "There are empty parameters";
                StrMenExi = "A file with this name already exists";
                StrMenCon = "Could not connect to server";
                StrMenPre = "Do you want to predetermine the new project?";
                StrMenFic = "Unexpected error saving configuration";
                StrMenCorrec = "Project created with success";
            }
            else
            {
                //Botones
                ButRuta.Content = "Examinar...";
                ButCrear.Content = "Crear";

                //Label
                LblHost.Content = "Servidor";
                LblUsu.Content = "Usuario";
                LblPassSql.Content = "Contaseña";
                LblBDSql.Content = "Base de datos";
                LblPortSql.Content = "Puerto";

                LblRuta.Content = "Ruta";
                LblNomBD.Content = "Nombre";
                LblPassBD.Content = "Contaseña";

                //Check
                ChkCreBase.Content = "¿Crear base de datos?";

                //Window
                Title = "Nuevo Proyecto";

                //Mensajes
                StrConf = "Confirmación";
                StrMenExi = "Ya existe un archivo con este nombre";
                StrMenPar = "Hay parámetros vacíos";
                StrMenCon = "No se pudo conectar al servidor";
                StrMenPre = "¿Desea predeterminar el nuevo proyecto?";
                StrMenFic = "Error inesperado al guardar la configuración";
                StrMenCorrec = "Proyecto creado con exito";
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
                if ((ctrl.Name == "TxtPortMS") && (string.IsNullOrEmpty(TxtPortMS.Text) == false)) ButCrear.Focus();
                if (ctrl.Name == "TxtNomBD" && (string.IsNullOrEmpty(TxtNomBD.Text) == false)) TxtPassBD.Focus();
                if (ctrl.Name == "TxtPassBD" && (string.IsNullOrEmpty(TxtPassBD.Password) == false)) ButCrear.Focus();
            }
        }
    }
}