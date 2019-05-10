﻿using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    public partial class WinActores : Window
    {
        ClsActor Actor = new ClsActor();
        DataRow Fila;
        Control ctrl;
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenEGuar;
        string StrMenEMod;
        public WinActores()
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
                Actor.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Actor.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
            {
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    Actor.Nombre = TxtNom.Text;
                    Actor.Descripcion = TxtDesc.Text;
                    if (RBCB.IsChecked == true) Actor.Complejidad = 1;
                    else if (RBCM.IsChecked == true) Actor.Complejidad = 2;
                    else if (RBCA.IsChecked == true) Actor.Complejidad = 3;
                    Actor.DescComplejidad = TxtDescCom.Text;
                    Actor.Categoria = int.Parse(CmbCat.Text);
                    Actor.Comentario = TxtCom.Text;
                    int resultado = Actor.Guardar();
                    if (resultado == -1) MessageBox.Show(StrMenEMod);
                    if (resultado == -2) MessageBox.Show(StrMenEGuar);
                    VaciarInterfaz();
                }
                else MessageBox.Show(StrMenGuar);

            }
            if (ctrl.Name.Equals("ButBorr"))
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Actor.Borrar();
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
                    if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarActor();
                }
                else CargarActor();
            }
            if (ctrl.Name.Equals("DGGruAut"))
            {
                bool existe = false;
                if (Actor.Autores.Rows.Count > 0)
                    for (int i = 0; i <= (Actor.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Actor.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Actor.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Actor.Autores.Rows.Add(Fila);
                }
                Actor.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGAutores"))
            {
                Fila = Actor.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Actor.BGrupo.Rows.Add(Fila);
                Actor.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGGruFuen"))
            {
                bool existe = false;
                if (Actor.Fuentes.Rows.Count > 0)
                    for (int i = 0; i <= (Actor.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Actor.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }

                if (!existe)
                {
                    Fila = Actor.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Actor.Fuentes.Rows.Add(Fila);
                }
                Actor.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGFuentes"))
            {
                Fila = Actor.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Actor.BFuentes.Rows.Add(Fila);
                Actor.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("TxtNom") && !Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text)) TxtDesc.Focus();
                if (ctrl.Name.Equals("TxtDescCom") && !string.IsNullOrEmpty(TxtDescCom.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            TAB.SelectedIndex = 0;
            TxtNom.Text = string.Empty;
            TxtDesc.Text = string.Empty;
            RBCM.IsChecked = true;
            TxtDescCom.Text = string.Empty;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            Activo = false;
            Base = false;
            Actor.IniciarValores();
            IniciarTablas();
        }
        private void CargarActor()
        {
            Actor.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            TxtNom.Text = Actor.Nombre;
            TxtDesc.Text = Actor.Descripcion.ToString();
            if (Actor.Complejidad == 1) RBCB.IsChecked = true;
            else if (Actor.Complejidad == 2) RBCM.IsChecked = true;
            else if (Actor.Complejidad == 3) RBCA.IsChecked = true;
            TxtDescCom.Text = Actor.DescComplejidad;
            CmbCat.Text = Actor.Categoria.ToString();
            TxtCom.Text = Actor.Comentario;
            DGFuentes.ItemsSource = Actor.Fuentes.DefaultView;
            DGAutores.ItemsSource = Actor.Autores.DefaultView;
            DGGruAut.ItemsSource = Actor.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Actor.BFuentes.DefaultView;
            Activo = true;
            Base = true;
            Actor.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                //DataGrid
                DGBuscar.Columns[0].Header = Ingles.Actors;
                DGGruAut.Columns[0].Header = Ingles.WorkGrup;
                DGAutores.Columns[0].Header = Ingles.Authors;
                DGGruFuen.Columns[0].Header = Ingles.WorkGrup;
                DGFuentes.Columns[0].Header = Ingles.Sources;

                //Botones
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;

                //Label
                LblNom.Content = Ingles.Name;
                LblDes.Content = Ingles.Description;
                LblComp.Content = Ingles.Complexity;
                LblDesCom.Content = Ingles.Desc_Compl;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblBus.Text = Ingles.Search_Engine;

                //RadioButton
                RBCB.Content = Ingles.Low;
                RBCM.Content = Ingles.Medium;
                RBCA.Content = Ingles.High;

                //Window
                Title = Ingles.ProAct;

                //TabItem
                TabDat.Header = Ingles.Data;
                TabAut.Header = Ingles.Authors;
                TabFue.Header = Ingles.Sources;

                //Mensajes
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.ActMenGuar;
                StrMenBorr = Ingles.ActMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.ActMenEGuar;
                StrMenEMod = Ingles.ActMenEMod;
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = Español.Actores;
                DGGruAut.Columns[0].Header = Español.TrabGrup;
                DGAutores.Columns[0].Header = Español.Autores;
                DGGruFuen.Columns[0].Header = Español.TrabGrup;
                DGFuentes.Columns[0].Header = Español.Fuentes;

                //Botones
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;

                //Label
                LblNom.Content = Español.Nombre;
                LblDes.Content = Español.Descripción;
                LblComp.Content = Español.Complejidad;
                LblDesCom.Content = Español.Desc_Compl;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblBus.Text = Español.Buscador;

                //RadioButton
                RBCB.Content = Español.Baja;
                RBCM.Content = Español.Media;
                RBCA.Content = Español.Alta;

                //Window
                Title = Español.ActPro;

                //TabItem
                TabDat.Header = Español.Datos;
                TabAut.Header = Español.Autores;
                TabFue.Header = Español.Fuentes;

                //Mensajes
                StrConf = Español.Confirmación;
                StrMenGuar = Español.ActMenGuar;
                StrMenBorr = Español.ActMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.ActMenEGuar;
                StrMenEMod = Español.ActMenEMod;
            }
        }
        private void IniciarTablas()
        {
            Actor.CargarTablas();
            DGGruAut.ItemsSource = Actor.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Actor.BFuentes.DefaultView;
            DGFuentes.ItemsSource = Actor.Fuentes.DefaultView;
            DGAutores.ItemsSource = Actor.Autores.DefaultView;
        }
    }
}