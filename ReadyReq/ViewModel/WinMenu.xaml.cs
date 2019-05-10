using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ReadyReq.ViewModel
{
    public partial class WinMenu : Window
    {
        Control ctrl = new Control();
        WinGrupo Grupo = new WinGrupo();
        WinActores Actores = new WinActores();
        WinPaq Paquetes = new WinPaq();
        WinObjetivos Objetivos = new WinObjetivos();
        WinReqInfo ReqInfo = new WinReqInfo();
        WinReqNFun ReqNFun = new WinReqNFun();
        WinReqFun ReqFun = new WinReqFun();
        WinEstim Estim = new WinEstim();
        WinExpor Exportar = new WinExpor();
        WinImpor Importar = new WinImpor();
        WinWord Word = new WinWord();
        WinNuePro NuevoPro = new WinNuePro();
        WinConf Configuracion = new WinConf();
        bool Activo = false;
        public WinMenu()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {

            if ((Convert.ToDateTime("01/06/2019") - Convert.ToDateTime(DateTime.Now.ToShortDateString())).TotalDays >= 0)
            {

                //  if ((String.Compare(Environment.MachineName, "Asuka") == 0) && ((Convert.ToDateTime("01/08/2018") - Convert.ToDateTime(DateTime.Now.ToShortDateString())).TotalDays >= 0))            {
                if (ClsConf.Iniciar() == false)
                    TxtCon.Text = "Configuración\nConfiguration";
                else
                {
                    Activo = true;
                    Idioma();
                }


            }
            else { MessageBox.Show("O no es tu PC o se te han pasado los dias, asiq jodete y llamame. (TUS DATOS NO SE HAN BORRADO DE LA BASE DE DATOS)"); }

        }
        private void Idioma()
        {
            if (ClsConf.Idioma == DefValues.Ingles)
            {
                Title = Ingles.Menu;

                //Label
                TxtGru.Text = Ingles.Workgroup;
                TxtPaq.Text = Ingles.Packages;
                TxtObj.Text = Ingles.Objectives;
                TxtAct.Text = Ingles.Actors;
                TxtRIn.Text = Ingles.ReqInfo;
                TxtRFu.Text = Ingles.ReqFun;
                TxtRNF.Text = Ingles.ReqNFun;
                TxtCon.Text = Ingles.Configuration;
                TxtNuP.Text = Ingles.New_Proj;
                TxtImp.Text = Ingles.Import_DB;
                TxtExp.Text = Ingles.Export_File;
                TxtWord.Text = Ingles.Export_Word;
                TxtEstim.Text = Ingles.Estimates;
            }
            else
            {
                Title = Español.Menu;

                //Label
                TxtGru.Text = Español.Grupo_Trabajo;
                TxtPaq.Text = Español.Paquetes;
                TxtObj.Text = Español.Objetivos;
                TxtAct.Text = Español.Actores;
                TxtRIn.Text = Español.ReqInfo;
                TxtRFu.Text = Español.ReqFun;
                TxtRNF.Text = Español.ReqNFun;
                TxtCon.Text = Español.Configuración;
                TxtNuP.Text = Español.Nue_Proy;
                TxtImp.Text = Español.Importar_BD;
                TxtExp.Text = Español.Exportar_Fich;
                TxtWord.Text = Español.Esportar_Word;
                TxtEstim.Text = Español.Estimaciones;
            }
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name.Equals("ButGru") && !Grupo.IsLoaded && Activo)
            {
                Grupo = new WinGrupo();
                Grupo.Owner = this;
                Grupo.Show();
            }

            if (ctrl.Name.Equals("ButPaq") && !Paquetes.IsLoaded && Activo)
            {
                Paquetes = new WinPaq();
                Paquetes.Owner = this;
                Paquetes.Show();
            }

            if (ctrl.Name.Equals("ButObj") && !Objetivos.IsLoaded && Activo)
            {
                Objetivos = new WinObjetivos();
                Objetivos.Owner = this;
                Objetivos.Show();
            }

            if (ctrl.Name.Equals("ButAct") && !Actores.IsLoaded && Activo)
            {
                Actores = new WinActores();
                Actores.Owner = this;
                Actores.Show();
            }

            if (ctrl.Name.Equals("ButRIn") && !ReqInfo.IsLoaded && Activo)
            {
                ReqInfo = new WinReqInfo();
                ReqInfo.Owner = this;
                ReqInfo.Show();
            }

            if (ctrl.Name.Equals("ButRFu") && !ReqFun.IsLoaded && Activo)
            {
                ReqFun = new WinReqFun();
                ReqFun.Owner = this;
                ReqFun.Show();
            }

            if (ctrl.Name.Equals("ButRNF") && !ReqNFun.IsLoaded && Activo)
            {
                ReqNFun = new WinReqNFun();
                ReqNFun.Owner = this;
                ReqNFun.Show();
            }

            if (ctrl.Name.Equals("ButCon") && !Configuracion.IsLoaded)
            {
                Configuracion = new WinConf();
                Configuracion.Owner = this;
                Configuracion.ShowDialog();
                Activo = true;
                if (ClsConf.TipoBD == DefValues.MySql) ClsConf.ConexionMySql();
                Idioma();
            }

            if (ctrl.Name.Equals("ButNuP") && !NuevoPro.IsLoaded && Activo)
            {
                NuevoPro = new WinNuePro();
                NuevoPro.Owner = this;
                NuevoPro.ShowDialog();
            }

            if (ctrl.Name.Equals("ButImp") && !Importar.IsLoaded && Activo)
            {
                Importar = new WinImpor();
                Importar.Owner = this;
                Importar.ShowDialog();
            }

            if (ctrl.Name.Equals("ButExp") && !Exportar.IsLoaded && Activo)
            {
                Exportar = new WinExpor();
                Exportar.Owner = this;
                Exportar.ShowDialog();
            }

            if (ctrl.Name.Equals("ButWord") && !Word.IsLoaded && Activo)
            {
                Word = new WinWord();
                Word.Owner = this;
                Word.ShowDialog();
            }

            if (ctrl.Name.Equals("ButEstim") && !Estim.IsLoaded && Activo)
            {
                Estim = new WinEstim();
                Estim.Owner = this;
                Estim.ShowDialog();
            }

        }
        private void WClosed(object sender, EventArgs e)
        {
            foreach (Window Win in Application.Current.Windows)
                Win.Close();
        }
    }
}