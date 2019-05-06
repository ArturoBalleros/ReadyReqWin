using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq
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
            for (int i = 1; (i <= 10); i++)
                CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
        }
        private void WClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Activo == true)
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    return;
                else
                    e.Cancel = true;
        }
        private void Click(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "ButBusc")
            {
                Paquete.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Paquete.Buscador.DefaultView;
            }
            if (ctrl.Name == "ButAcep")
            {
                if (string.IsNullOrEmpty(TxtNom.Text) == false)
                {
                    Paquete.Nombre = TxtNom.Text;
                    Paquete.Categoria = int.Parse(CmbCat.Text);
                    Paquete.Comentario = TxtCom.Text;
                    int resultado = Paquete.Guardar();
                    if (resultado == -1) MessageBox.Show(StrMenEMod);
                    if (resultado == -2) MessageBox.Show(StrMenEGuar);
                    VaciarInterfaz();
                }
                else
                {
                    MessageBox.Show(StrMenGuar);
                }
            }
            if (ctrl.Name == "ButBorr")
            {
                if (Base == true)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        Paquete.Borrar();
                VaciarInterfaz();
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Activo == true && DGBuscar.SelectedIndex > -1)
            {
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    CargarPaquete();
            }
            else
            {
                CargarPaquete();
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = ((Control)sender);
            if ((ctrl.Name == "TxtNom") && (Activo == false))
                Activo = true;
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtNom") && (string.IsNullOrEmpty(TxtNom.Text) == false)) TxtCom.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
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
            if (ClsConf.Idioma == "Ingles")
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Packages";

                //Botones
                ButBusc.Content = "Search";
                ButAcep.Content = "Save";
                ButBorr.Content = "Delete";

                //Label
                LblNom.Content = "Name";
                LblCat.Content = "Category";
                LblCom.Content = "Commentary";
                LblBus.Text = "Search Engine";

                //Window
                Title = "Project Packages";

                //Mensajes
                StrConf = "Confirmation";
                StrMenGuar = "The package must have an assigned name";
                StrMenBorr = "The package will be deleted, do you wish to continue?";
                StrMenPrev = "The unsaved progress will be deleted, do you wish to continue?";
                StrMenEGuar = "The package could not be saved";
                StrMenEMod = "The package could not be modified, so it was deleted so as not to cause instability in the database";
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Paquetes";

                //Botones
                ButBusc.Content = "Buscar";
                ButAcep.Content = "Guardar";
                ButBorr.Content = "Borrar";

                //Label
                LblNom.Content = "Nombre";
                LblCat.Content = "Categoría";
                LblCom.Content = "Comentario";
                LblBus.Text = "Buscador";

                //Window
                Title = "Paquetes del Proyecto";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El paquete debe de tener un nombre asignado";
                StrMenBorr = "Se borrará el paquete, ¿Desea continuar?";
                StrMenPrev = "Se borrará el progreso no guardado, ¿Desea continuar?";
                StrMenEGuar = "No se pudo guardar el paquete";
                StrMenEMod = "No se pudo modificar el paquete, por lo que se eliminó para no ocasionar inestabilidad en la base de datos";
            }
        }
    }
}