using ReadyReq.Model;
using ReadyReq.ViewModel;
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
            Title = "Menu";
            if (ClsConf.Idioma == "Ingles")
            {
                //Label
                TxtGru.Text = "Workgroup";
                TxtPaq.Text = "Packages";
                TxtObj.Text = "Objectives";
                TxtAct.Text = "Actors";
                TxtRIn.Text = "Information Requirements";
                TxtRFu.Text = "Functional Requirements";
                TxtRNF.Text = "Non-functional requirements";
                TxtCon.Text = "Configuration";
                TxtNuP.Text = "New Project";
                TxtImp.Text = "Import to Database";
                TxtExp.Text = "Export to File";
                TxtWord.Text = "Export to Word";
                TxtEstim.Text = "Estimates";
            }
            else
            {
                //Label
                TxtGru.Text = "Grupo de trabajo";
                TxtPaq.Text = "Paquetes";
                TxtObj.Text = "Objetivos";
                TxtAct.Text = "Actores";
                TxtRIn.Text = "Requisitos de Información";
                TxtRFu.Text = "Requisitos Funcionales";
                TxtRNF.Text = "Requisitos no Funcionales";
                TxtCon.Text = "Configuración";
                TxtNuP.Text = "Nuevo Proyecto";
                TxtImp.Text = "Importar a Base de Datos";
                TxtExp.Text = "Exportar a Fichero";
                TxtWord.Text = "Exportar a Word";
                TxtEstim.Text = "Estimaciones";
            }
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "ButGru")
            {
                if (Grupo.IsLoaded == false && Activo == true)
                {
                    Grupo = new WinGrupo();
                    Grupo.Owner = this;
                    Grupo.Show();
                }
            }
            if (ctrl.Name == "ButPaq")
            {
                if (Paquetes.IsLoaded == false && Activo == true)
                {
                    Paquetes = new WinPaq();
                    Paquetes.Owner = this;
                    Paquetes.Show();
                }
            }
            if (ctrl.Name == "ButObj")
            {
                if (Objetivos.IsLoaded == false && Activo == true)
                {
                    Objetivos = new WinObjetivos();
                    Objetivos.Owner = this;
                    Objetivos.Show();
                }
            }
            if (ctrl.Name == "ButAct")
            {
                if (Actores.IsLoaded == false && Activo == true)
                {
                    Actores = new WinActores();
                    Actores.Owner = this;
                    Actores.Show();
                }
            }
            if (ctrl.Name == "ButRIn")
            {
                if (ReqInfo.IsLoaded == false && Activo == true)
                {
                    ReqInfo = new WinReqInfo();
                    ReqInfo.Owner = this;
                    ReqInfo.Show();
                }
            }
            if (ctrl.Name == "ButRFu")
            {
                if (ReqFun.IsLoaded == false && Activo == true)
                {
                    ReqFun = new WinReqFun();
                    ReqFun.Owner = this;
                    ReqFun.Show();
                }
            }
            if (ctrl.Name == "ButRNF")
            {
                if (ReqNFun.IsLoaded == false && Activo == true)
                {
                    ReqNFun = new WinReqNFun();
                    ReqNFun.Owner = this;
                    ReqNFun.Show();
                }
            }
            if (ctrl.Name == "ButCon")
            {
                if (Configuracion.IsLoaded == false)
                {
                    Configuracion = new WinConf();
                    Configuracion.Owner = this;
                    Configuracion.ShowDialog();
                    Activo = true;
                    if (ClsConf.TipoBD == "MySql") ClsConf.ConexionMySql();
                    Idioma();
                }
            }
            if (ctrl.Name == "ButNuP")
            {
                if (NuevoPro.IsLoaded == false && Activo == true)
                {
                    NuevoPro = new WinNuePro();
                    NuevoPro.Owner = this;
                    NuevoPro.ShowDialog();
                }
            }
            if (ctrl.Name == "ButImp")
            {
                if (Importar.IsLoaded == false && Activo == true)
                {
                    Importar = new WinImpor();
                    Importar.Owner = this;
                    Importar.ShowDialog();
                }
            }
            if (ctrl.Name == "ButExp")
            {
                if (Exportar.IsLoaded == false && Activo == true)
                {
                    Exportar = new WinExpor();
                    Exportar.Owner = this;
                    Exportar.ShowDialog();
                }
            }
            if (ctrl.Name == "ButWord")
            {
                if (Word.IsLoaded == false && Activo == true)
                {
                    Word = new WinWord();
                    Word.Owner = this;
                    Word.ShowDialog();
                }
            }
            if (ctrl.Name == "ButEstim")
            {
                if (Estim.IsLoaded == false && Activo == true)
                {
                    Estim = new WinEstim();
                    Estim.Owner = this;
                    Estim.ShowDialog();
                }
            }
        }
        private void WClosed(object sender, EventArgs e)
        {
            foreach (Window Win in Application.Current.Windows)
                Win.Close();
        }
    }
}