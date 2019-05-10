﻿using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    public partial class WinReqNFun : Window
    {
        ClsReqNFun Requisito = new ClsReqNFun();
        DataRow Fila;
        Control ctrl;
        int TipoReq = 3;
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenEGuar;
        string StrMenEMod;
        public WinReqNFun()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            for (int i = 1; (i <= 10); i++) CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
            IniciarTablas();
        }
        private void WClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Activo)
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) return;
                else e.Cancel = true;
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("ButBusc"))
            {
                Requisito.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Requisito.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    Requisito.Nombre = TxtNom.Text;
                    Requisito.Descripcion = TxtDesc.Text;
                    RadioButtonValor(true);
                    Requisito.Categoria = int.Parse(CmbCat.Text);
                    Requisito.Comentario = TxtCom.Text;
                    int resultado = Requisito.Guardar();
                    if (resultado == -1) MessageBox.Show(StrMenEMod);
                    if (resultado == -2) MessageBox.Show(StrMenEGuar);
                    VaciarInterfaz();
                }
                else MessageBox.Show(StrMenGuar);
            if (ctrl.Name == "ButBorr")
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Requisito.Borrar();
                VaciarInterfaz();
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("DGBuscar"))
            {
                if (Activo && DGBuscar.SelectedIndex > -1)
                {
                    if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarRequisito();
                }
                else CargarRequisito();
            }
            if (ctrl.Name.Equals("DGGruAut"))
            {
                bool existe = false;
                if (Requisito.Autores.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Autores.Rows.Add(Fila);
                }
                Requisito.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGAutores"))
            {
                Fila = Requisito.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BGrupo.Rows.Add(Fila);
                Requisito.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGGruFuen"))
            {
                bool existe = false;
                if (Requisito.Fuentes.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Fuentes.Rows.Add(Fila);
                }
                Requisito.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGFuentes"))
            {
                Fila = Requisito.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BFuentes.Rows.Add(Fila);
                Requisito.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGObjObj"))
            {
                bool existe = false;
                if (Requisito.Objetivos.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Objetivos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Objetivos.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Objetivos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Objetivos.Rows.Add(Fila);
                }
                Requisito.BObjetivos.Rows.RemoveAt(DGObjObj.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGObjetivos"))
            {
                Fila = Requisito.BObjetivos.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BObjetivos.Rows.Add(Fila);
                Requisito.Objetivos.Rows.RemoveAt(DGObjetivos.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGRequi"))
            {
                bool existe = false;
                if (Requisito.Requisitos.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Requisitos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Requisitos.Rows[i];
                        if ((Fila[0].ToString() == Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0])) && (int.Parse(Fila[1].ToString()) == TipoReq))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Requisitos.NewRow();
                    Fila[2] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[1]);
                    Fila[1] = TipoReq;
                    Fila[0] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Requisitos.Rows.Add(Fila);
                }
                Requisito.BRequisitos.Rows.RemoveAt(DGRequi.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGReqRel"))
            {
                if (int.Parse(Convert.ToString(((DataRowView)DGReqRel.Items[DGReqRel.SelectedIndex]).Row.ItemArray[1])) == TipoReq)
                {
                    Fila = Requisito.BRequisitos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGReqRel.Items[DGReqRel.SelectedIndex]).Row.ItemArray[2]);
                    Fila[0] = Convert.ToString(((DataRowView)DGReqRel.Items[DGReqRel.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.BRequisitos.Rows.Add(Fila);
                }
                Requisito.Requisitos.Rows.RemoveAt(DGReqRel.SelectedIndex);
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("TxtNom") && !Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text)) TxtDesc.Focus();
                if (ctrl.Name.Equals("TxtDesc") && !string.IsNullOrEmpty(TxtDesc.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("RBReqInf")) TipoReq = 1;
            if (ctrl.Name.Equals("RBReqNFun")) TipoReq = 2;
            if (ctrl.Name.Equals("RBReqFun")) TipoReq = 3;
            try
            {
                Requisito.CargarTablaReqRel(TipoReq);
                DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;
            }
            catch { }
        }
        private void VaciarInterfaz()
        {
            TAB.SelectedIndex = 0;
            TxtNom.Text = string.Empty;
            TxtDesc.Text = string.Empty;
            RBPM.IsChecked = true;
            RBUM.IsChecked = true;
            RBEM.IsChecked = true;
            RBVer.IsChecked = true;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            RBReqFun.IsChecked = true;
            Activo = false;
            Base = false;
            Requisito.IniciarValores();
            IniciarTablas();
        }
        private void CargarRequisito()
        {
            Requisito.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])), TipoReq);
            TxtNom.Text = Requisito.Nombre;
            TxtDesc.Text = Requisito.Descripcion;
            RadioButtonValor(false);
            CmbCat.Text = Requisito.Categoria.ToString();
            TxtCom.Text = Requisito.Comentario;

            DGObjetivos.ItemsSource = Requisito.Objetivos.DefaultView;
            DGFuentes.ItemsSource = Requisito.Fuentes.DefaultView;
            DGAutores.ItemsSource = Requisito.Autores.DefaultView;
            DGReqRel.ItemsSource = Requisito.Requisitos.DefaultView;

            DGGruAut.ItemsSource = Requisito.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Requisito.BFuentes.DefaultView;
            DGObjObj.ItemsSource = Requisito.BObjetivos.DefaultView;
            DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;

            Activo = true;
            Base = true;

            Requisito.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                DGBuscar.Columns[0].Header = DGRequi.Columns[0].Header = TabReq.Header = Ingles.Requirements;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Ingles.WorkGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Ingles.Authors;
                DGFuentes.Columns[0].Header = TabFue.Header = Ingles.Sources;
                DGObjObj.Columns[0].Header = TabObj.Header = Ingles.Objectives;
                DGObjetivos.Columns[0].Header = Ingles.RelObjet;
                DGReqRel.Columns[0].Header = Ingles.RelRequi;
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;
                LblNom.Content = Ingles.Name;
                LblDes.Content = Ingles.Description;
                LblPri.Content = Ingles.Priority;
                LblUrg.Content = Ingles.Priority;
                LblUrg.Content = Ingles.Urgency;
                LblEst.Content = Ingles.Stability;
                LblEsta.Content = Ingles.State;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblBus.Text = Ingles.Search_Engine;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Ingles.VLow;
                RBPB.Content = RBUB.Content = RBEB.Content = Ingles.Low;
                RBPM.Content = RBUM.Content = RBEM.Content = Ingles.Medium;
                RBPA.Content = RBUA.Content = RBEA.Content = Ingles.High;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Ingles.VHigh;
                RBVer.Content = Ingles.Verified;
                RBNVer.Content = Ingles.NVerified;
                RBReqInf.Content = Ingles.RBReqInfo;
                RBReqNFun.Content = Ingles.RBReqNFun;
                RBReqFun.Content = Ingles.RBReqFun;
                Title = Ingles.RNFunPro;
                TabDat.Header = Ingles.Data;
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.ReqMenGuar;
                StrMenBorr = Ingles.ReqMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.ReqMenEGuar;
                StrMenEMod = Ingles.ReqMenEMod;
            }
            else
            {
                DGBuscar.Columns[0].Header = DGRequi.Columns[0].Header = TabReq.Header = Español.Requisitos;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Español.TrabGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Español.Autores;
                DGFuentes.Columns[0].Header = TabFue.Header = Español.Fuentes;
                DGObjObj.Columns[0].Header = TabObj.Header = Español.Objetivos;
                DGObjetivos.Columns[0].Header = Español.RelObjet;
                DGReqRel.Columns[0].Header = Español.RelRequi;
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;
                LblNom.Content = Español.Nombre;
                LblDes.Content = Español.Descripción;
                LblPri.Content = Español.Prioridad;
                LblUrg.Content = Español.Urgencia;
                LblEst.Content = Español.Estabilidad;
                LblEsta.Content = Español.Estado;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblBus.Text = Español.Buscador;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Español.MBaja;
                RBPB.Content = RBUB.Content = RBEB.Content = Español.Baja;
                RBPM.Content = RBUM.Content = RBEM.Content = Español.Media;
                RBPA.Content = RBUA.Content = RBEA.Content = Español.Alta;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Español.MAlta;
                RBVer.Content = Español.Verificado;
                RBNVer.Content = Español.NVerificado;
                RBReqInf.Content = Español.RBReqInfo;
                RBReqNFun.Content = Español.RBReqNFun;
                RBReqFun.Content = Español.RBReqFun;
                Title = Español.RFunPro;
                TabDat.Header = Español.Datos;
                StrConf = Español.Confirmación;
                StrMenGuar = Español.ReqMenGuar;
                StrMenBorr = Español.ReqMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.ReqMenEGuar;
                StrMenEMod = Español.ReqMenEMod;
            }
        }
        private void IniciarTablas()
        {
            Requisito.CargarTablas();
            DGGruAut.ItemsSource = Requisito.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Requisito.BFuentes.DefaultView;
            DGObjObj.ItemsSource = Requisito.BObjetivos.DefaultView;
            DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;
            DGFuentes.ItemsSource = Requisito.Fuentes.DefaultView;
            DGAutores.ItemsSource = Requisito.Autores.DefaultView;
            DGObjetivos.ItemsSource = Requisito.Objetivos.DefaultView;
            DGReqRel.ItemsSource = Requisito.Requisitos.DefaultView;
        }
        private void RadioButtonValor(bool ValorRB)
        {
            if (ValorRB)
            {
                //Prioridad
                if (RBPMB.IsChecked == true) Requisito.Prioridad = 1;
                else if (RBPB.IsChecked == true) Requisito.Prioridad = 2;
                else if (RBPM.IsChecked == true) Requisito.Prioridad = 3;
                else if (RBPA.IsChecked == true) Requisito.Prioridad = 4;
                else if (RBPMA.IsChecked == true) Requisito.Prioridad = 5;
                //Urgencia
                if (RBUMB.IsChecked == true) Requisito.Urgencia = 1;
                else if (RBUB.IsChecked == true) Requisito.Urgencia = 2;
                else if (RBUM.IsChecked == true) Requisito.Urgencia = 3;
                else if (RBUA.IsChecked == true) Requisito.Urgencia = 4;
                else if (RBUMA.IsChecked == true) Requisito.Urgencia = 5;
                //Estabilidad
                if (RBEMB.IsChecked == true) Requisito.Estabilidad = 1;
                else if (RBEB.IsChecked == true) Requisito.Estabilidad = 2;
                else if (RBEM.IsChecked == true) Requisito.Estabilidad = 3;
                else if (RBEA.IsChecked == true) Requisito.Estabilidad = 4;
                else if (RBEMA.IsChecked == true) Requisito.Estabilidad = 5;
                //Estado
                if (RBVer.IsChecked == true) Requisito.Estado = true;
                else Requisito.Estado = false;
            }
            else
            {
                //Prioridad
                if (Requisito.Prioridad == 1) RBPMB.IsChecked = true;
                else if (Requisito.Prioridad == 2) RBPB.IsChecked = true;
                else if (Requisito.Prioridad == 3) RBPM.IsChecked = true;
                else if (Requisito.Prioridad == 4) RBPA.IsChecked = true;
                else if (Requisito.Prioridad == 5) RBPMA.IsChecked = true;
                //Urgencia
                if (Requisito.Urgencia == 1) RBUMB.IsChecked = true;
                else if (Requisito.Urgencia == 2) RBUB.IsChecked = true;
                else if (Requisito.Urgencia == 3) RBUM.IsChecked = true;
                else if (Requisito.Urgencia == 4) RBUA.IsChecked = true;
                else if (Requisito.Urgencia == 5) RBUMA.IsChecked = true;
                //Estabilidad
                if (Requisito.Estabilidad == 1) RBEMB.IsChecked = true;
                else if (Requisito.Estabilidad == 2) RBEB.IsChecked = true;
                else if (Requisito.Estabilidad == 3) RBEM.IsChecked = true;
                else if (Requisito.Estabilidad == 4) RBEA.IsChecked = true;
                else if (Requisito.Estabilidad == 5) RBEMA.IsChecked = true;
                //Estado
                if (Requisito.Estado == true) RBVer.IsChecked = true;
                else RBNVer.IsChecked = true;
            }
        }
    }
}