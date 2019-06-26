/*
 * Autor: Arturo Balleros Albillo
 */
using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    public partial class WinPaq : Window
    {
        ClsPaq Paquete = new ClsPaq();
        Control ctrl = new Control();
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenEGuar;
        string StrMenEMod;
        string StrMenEFec;
        string StrMenEVer;
        public WinPaq()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            TxtVer.Text = "1.0";
            TxtFec.Text = DateTime.Today.ToShortDateString();
            for (int i = 1; (i <= 10); i++) CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtNom.Focus();
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
                Paquete.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Paquete.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text))
                    {
                        if (ClsFunciones.TryConvertToDouble(TxtVer.Text))
                        {
                            Paquete.Nombre = TxtNom.Text;
                            Paquete.Version = ClsFunciones.StringToDouble(TxtVer.Text);
                            Paquete.Fecha = DateTime.Parse(TxtFec.Text);
                            Paquete.Categoria = int.Parse(CmbCat.Text);
                            Paquete.Comentario = TxtCom.Text;
                            int resultado = Paquete.Guardar();
                            if (resultado == -1) MessageBox.Show(StrMenEMod);
                            if (resultado == -2) MessageBox.Show(StrMenEGuar);
                            VaciarInterfaz();
                            TxtNom.Focus();
                        }
                        else MessageBox.Show(StrMenEVer);
                    }
                    else MessageBox.Show(StrMenEFec);
                }
                else MessageBox.Show(StrMenGuar);
            if (ctrl.Name.Equals("ButBorr"))
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Paquete.Borrar();
                VaciarInterfaz();
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Activo && DGBuscar.SelectedIndex > -1)
            {
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarPaquete();
            }
            else CargarPaquete();
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("TxtNom") && !Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text))
                {
                    int idExiste = Paquete.ComprobarExistencia(TxtNom.Text);
                    if (idExiste != -1) CargarPaquete(idExiste);
                    TxtVer.Focus();
                }
                if (ctrl.Name.Equals("TxtVer") && !string.IsNullOrEmpty(TxtVer.Text))
                {
                    if (ClsFunciones.TryConvertToDouble(TxtVer.Text)) TxtFec.Focus();
                    else MessageBox.Show(StrMenEVer);
                }
                if (ctrl.Name.Equals("TxtFec") && !string.IsNullOrEmpty(TxtFec.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text)) TxtCom.Focus();
                    else MessageBox.Show(StrMenEFec);
                }
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            TxtNom.Text = string.Empty;
            TxtVer.Text = "1.0";
            TxtFec.Text = DateTime.Today.ToShortDateString();
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            Activo = false;
            Base = false;
            Paquete.IniciarValores();
        }
        private void CargarPaquete(int id = -1)
        {
            if (id == -1) Paquete.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            else Paquete.Cargar(id);
            TxtNom.Text = Paquete.Nombre;
            TxtVer.Text = ClsFunciones.DoubleToString(Paquete.Version);
            TxtFec.Text = Paquete.Fecha.ToShortDateString();
            CmbCat.Text = Paquete.Categoria.ToString();
            TxtCom.Text = Paquete.Comentario;
            Activo = true;
            Base = true;
            Paquete.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                DGBuscar.Columns[0].Header = Ingles.Packages;
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;
                LblNom.Content = Ingles.Name;
                LblVer.Content = Ingles.Version;
                LblFec.Content = Ingles.Date;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblBus.Text = Ingles.Search_Engine;
                Title = Ingles.ProPack;
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.PacMenGuar;
                StrMenBorr = Ingles.PacMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.PacMenEGuar;
                StrMenEMod = Ingles.PacMenEMod;
                StrMenEFec = Ingles.MenEFec;
                StrMenEVer = Ingles.MenEVer;
            }
            else
            {
                DGBuscar.Columns[0].Header = Español.Paquetes;
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;
                LblNom.Content = Español.Nombre;
                LblVer.Content = Español.Version;
                LblFec.Content = Español.Fecha;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblBus.Text = Español.Buscador;
                Title = Español.PaqPro;
                StrConf = Español.Confirmación;
                StrMenGuar = Español.PacMenGuar;
                StrMenBorr = Español.PacMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.PacMenEGuar;
                StrMenEMod = Español.PacMenEMod;
                StrMenEFec = Español.MenEFec;
                StrMenEVer = Español.MenEVer;
            }
        }
    }
}