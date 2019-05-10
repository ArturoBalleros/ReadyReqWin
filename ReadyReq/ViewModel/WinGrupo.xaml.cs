﻿using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    public partial class WinGrupo : Window
    {
        ClsGrupo Trabajador = new ClsGrupo();
        Control ctrl = new Control();
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenEGuar;
        string StrMenEMod;
        public WinGrupo()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            for (int i = 1; (i <= 10); i++) CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
        }
        private void WClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Activo)
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) return;
                else e.Cancel = true;
        }
        public void Click(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("ButBusc"))
            {
                Trabajador.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Trabajador.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
            {
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    Trabajador.Nombre = TxtNom.Text;
                    Trabajador.Organizacion = TxtOrg.Text;
                    Trabajador.Rol = TxtRol.Text;
                    Trabajador.Desarrollador = (RBSi.IsChecked == true) ? true : false;
                    Trabajador.Categoria = int.Parse(CmbCat.Text);
                    Trabajador.Comentario = TxtCom.Text;
                    int resultado = Trabajador.Guardar();
                    if (resultado == -1) MessageBox.Show(StrMenEMod);
                    if (resultado == -2) MessageBox.Show(StrMenEGuar);
                    VaciarInterfaz();
                }
                else MessageBox.Show(StrMenGuar);

            }
            if (ctrl.Name.Equals("ButBorr"))
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Trabajador.Borrar();
                VaciarInterfaz();
            }
        }
        public void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Activo && DGBuscar.SelectedIndex > -1)
            {
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarPersona();
            }
            else CargarPersona();
        }
        public void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name.Equals("TxtNom") && !Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text)) TxtOrg.Focus();
                if (ctrl.Name.Equals("TxtOrg") && !string.IsNullOrEmpty(TxtOrg.Text)) TxtRol.Focus();
                if (ctrl.Name.Equals("TxtRol") && !string.IsNullOrEmpty(TxtRol.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            TxtNom.Text = string.Empty;
            TxtOrg.Text = string.Empty;
            TxtRol.Text = string.Empty;
            RBSi.IsChecked = true;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            Activo = false;
            Base = false;
            Trabajador.IniciarValores();
        }
        private void CargarPersona()
        {
            Trabajador.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            TxtNom.Text = Trabajador.Nombre;
            TxtOrg.Text = Trabajador.Organizacion;
            TxtRol.Text = Trabajador.Rol;
            if (Trabajador.Desarrollador) RBSi.IsChecked = true; else RBNo.IsChecked = true;
            CmbCat.Text = Trabajador.Categoria.ToString();
            TxtCom.Text = Trabajador.Comentario;
            Activo = true;
            Base = true;
            Trabajador.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                //DataGrid
                DGBuscar.Columns[0].Header = Ingles.Workers;

                //Botones
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;

                //Label
                LblNom.Content = Ingles.Name;
                LblOrg.Content = Ingles.Organization;
                LblRol.Content = Ingles.Role;
                LblDes.Content = Ingles.IsDev;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblBus.Text = Ingles.Search_Engine;

                //RadioButton
                RBSi.Content = Ingles.yes;
                RBNo.Content = Ingles.No;

                //Window
                Title = Ingles.Workgroup;

                //Mensajes
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.WorMenGuar;
                StrMenBorr = Ingles.WorMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.WorMenEGuar;
                StrMenEMod = Ingles.WorMenEMod;
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = Español.Trabajadores;

                //Botones
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;

                //Label
                LblNom.Content = Español.Nombre;
                LblOrg.Content = Español.Organización;
                LblRol.Content = Español.Rol;
                LblDes.Content = Español.Es_Des;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblBus.Text = Español.Buscador;

                //RadioButton
                RBSi.Content = Español.Si;
                RBNo.Content = Español.No;

                //Window
                Title = Español.Grupo_Trabajo;

                //Mensajes
                StrConf = Español.Confirmación;
                StrMenGuar = Español.WorMenGuar;
                StrMenBorr = Español.WorMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.WorMenEGuar;
                StrMenEMod = Español.WorMenEMod;
            }
        }
    }
}