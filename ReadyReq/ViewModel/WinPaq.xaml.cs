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
        public WinPaq()
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
            {
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    Paquete.Nombre = TxtNom.Text;
                    Paquete.Categoria = int.Parse(CmbCat.Text);
                    Paquete.Comentario = TxtCom.Text;
                    int resultado = Paquete.Guardar();
                    if (resultado == -1) MessageBox.Show(StrMenEMod);
                    if (resultado == -2) MessageBox.Show(StrMenEGuar);
                    VaciarInterfaz();
                }
                else MessageBox.Show(StrMenGuar);

            }
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
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            TxtNom.Text = string.Empty;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            Activo = false;
            Base = false;
            Paquete.IniciarValores();
        }
        private void CargarPaquete()
        {
            Paquete.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            TxtNom.Text = Paquete.Nombre;
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
                //DataGrid
                DGBuscar.Columns[0].Header = Ingles.Packages;

                //Botones
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;

                //Label
                LblNom.Content = Ingles.Name;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblBus.Text = Ingles.Search_Engine;

                //Window
                Title = Ingles.ProPack;

                //Mensajes
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.PacMenGuar;
                StrMenBorr = Ingles.PacMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.PacMenEGuar;
                StrMenEMod = Ingles.PacMenEMod;
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = Español.Paquetes;

                //Botones
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;

                //Label
                LblNom.Content = Español.Nombre;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblBus.Text = Español.Buscador;

                //Window
                Title = Español.PaqPro;

                //Mensajes
                StrConf = Español.Confirmación;
                StrMenGuar = Español.PacMenGuar;
                StrMenBorr = Español.PacMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.PacMenEGuar;
                StrMenEMod = Español.PacMenEMod;
            }
        }
    }
}