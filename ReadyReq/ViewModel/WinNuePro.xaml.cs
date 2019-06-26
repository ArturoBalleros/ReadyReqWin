/*
 * Autor: Arturo Balleros Albillo
 */
using ReadyReq.Model;
using ReadyReq.Util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace ReadyReq.ViewModel
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
            VisibleGrid(DefValues.MySql);
            Idioma();
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
                WinForms.FolderBrowserDialog browse = new WinForms.FolderBrowserDialog();
                browse.ShowDialog();
                LblRutaBD.Content = browse.SelectedPath;
            }
            if (ctrl.Name.Equals("ButCrear"))
            {
                if (RBSql.IsChecked == true)
                {
                    if (string.IsNullOrEmpty(TxtHost.Text) || string.IsNullOrEmpty(TxtUsu.Text) || string.IsNullOrEmpty(TxtPassMS.Password) || string.IsNullOrEmpty(TxtBDMS.Text) || string.IsNullOrEmpty(TxtPortMS.Text))
                    {
                        MessageBox.Show(StrMenPar);
                        return;
                    }
                    else
                    {
                        NuevoProyecto.TipoBD = DefValues.MySql;
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
                    if (string.IsNullOrEmpty(RutaBD))
                    {
                        MessageBox.Show(StrMenPar);
                        return;
                    }
                    else
                    {
                        NuevoProyecto.TipoBD = DefValues.Access;
                        NuevoProyecto.RutaAcc = RutaBD + "\\" + TxtNomBD.Text + ".accdb";
                        if (!string.IsNullOrEmpty(TxtPassBD.Password))
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
                            if (NuevoProyecto.FlgPass) ClsConf.PassAcc = NuevoProyecto.PassAcc.Substring(29, NuevoProyecto.PassAcc.Length - 30);
                            else ClsConf.PassAcc = string.Empty;
                        }
                        if (ClsConf.EscribirConf() == -1) MessageBox.Show(StrMenFic);
                        MessageBox.Show(StrMenCorrec);
                    }
                }
                Close();
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
                ButRuta.Content = Ingles.Browse;
                ButCrear.Content = Ingles.Create;
                LblHost.Content = Ingles.Server;
                LblUsu.Content = Ingles.User;
                LblPassSql.Content = LblPassBD.Content = Ingles.Password;
                LblBDSql.Content = Ingles.Database;
                LblPortSql.Content = Ingles.Port;
                LblRuta.Content = Ingles.Path;
                LblNomBD.Content = Ingles.Name;
                ChkCreBase.Content = Ingles.CreaData;
                Title = Ingles.New_Proj;
                StrConf = Ingles.Confirmation;
                StrMenPar = Ingles.NPMenPar;
                StrMenExi = Ingles.NPMenExi;
                StrMenCon = Ingles.NPMenCon;
                StrMenPre = Ingles.NPMenPre;
                StrMenFic = Ingles.NPMenFic;
                StrMenCorrec = Ingles.NPMenCorrec;
            }
            else
            {
                ButRuta.Content = Español.Examinar;
                ButCrear.Content = Español.Crear;
                LblHost.Content = Español.Servidor;
                LblUsu.Content = Español.Usuario;
                LblPassSql.Content = LblPassBD.Content = Español.Contaseña;
                LblBDSql.Content = Español.Base_Datos;
                LblPortSql.Content = Español.Puerto;
                LblRuta.Content = Español.Ruta;
                LblNomBD.Content = Español.Nombre;
                ChkCreBase.Content = Español.CreaBase;
                Title = Español.Nue_Proy;
                StrConf = Español.Confirmación;
                StrMenPar = Español.NPMenPar;
                StrMenExi = Español.NPMenExi;
                StrMenCon = Español.NPMenCon;
                StrMenPre = Español.NPMenPre;
                StrMenFic = Español.NPMenFic;
                StrMenCorrec = Español.NPMenCorrec;
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
                if (ctrl.Name.Equals("TxtPortMS") && !string.IsNullOrEmpty(TxtPortMS.Text)) ButCrear.Focus();
                if (ctrl.Name.Equals("TxtNomBD") && !string.IsNullOrEmpty(TxtNomBD.Text)) TxtPassBD.Focus();
                if (ctrl.Name.Equals("TxtPassBD") && !string.IsNullOrEmpty(TxtPassBD.Password)) ButCrear.Focus();
            }
        }
    }
}