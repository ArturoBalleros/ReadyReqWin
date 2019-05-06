using ReadyReq.Model;
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
        public void Click(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "ButBusc")
            {
                Trabajador.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Trabajador.Buscador.DefaultView;
            }
            if (ctrl.Name == "ButAcep")
            {
                if (string.IsNullOrEmpty(TxtNom.Text) == false)
                {
                    Trabajador.Nombre = TxtNom.Text;
                    Trabajador.Organizacion = TxtOrg.Text;
                    Trabajador.Rol = TxtRol.Text;
                    if (RBSi.IsChecked == true) Trabajador.Desarrollador = true; else Trabajador.Desarrollador = false;
                    Trabajador.Categoria = int.Parse(CmbCat.Text);
                    Trabajador.Comentario = TxtCom.Text;
                    int resultado = Trabajador.Guardar();
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
                        Trabajador.Borrar();
                VaciarInterfaz();
            }
        }
        public void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            if (Activo == true && DGBuscar.SelectedIndex > -1)
            {
                if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    CargarPersona();
            }
            else
            {
                CargarPersona();
            }
            //  Expander_Collapsed(sender, e);
            // System.Windows.Controls.SelectedCellsChangedEventArgs
            //   System.Windows.Controls.RoutedEventArgs
        }
        public void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = ((Control)sender);
            if ((ctrl.Name == "TxtNom") && (Activo == false))
                Activo = true;
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtNom") && (string.IsNullOrEmpty(TxtNom.Text) == false)) TxtOrg.Focus();
                if ((ctrl.Name == "TxtOrg") && (string.IsNullOrEmpty(TxtOrg.Text) == false)) TxtRol.Focus();
                if ((ctrl.Name == "TxtRol") && (string.IsNullOrEmpty(TxtRol.Text) == false)) TxtCom.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
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
            if (Trabajador.Desarrollador == true) RBSi.IsChecked = true; else RBNo.IsChecked = true;
            CmbCat.Text = Trabajador.Categoria.ToString();
            TxtCom.Text = Trabajador.Comentario;
            Activo = true;
            Base = true;
            Trabajador.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma == "Ingles")
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Workers";

                //Botones
                ButBusc.Content = "Search";
                ButAcep.Content = "Save";
                ButBorr.Content = "Delete";

                //Label
                LblNom.Content = "Name";
                LblOrg.Content = "Organization";
                LblRol.Content = "Role";
                LblDes.Content = "Is Developer";
                LblCat.Content = "Category";
                LblCom.Content = "Commentary";
                LblBus.Text = "Search Engine";

                //RadioButton
                RBSi.Content = "Yes";
                RBNo.Content = "No";

                //Window
                Title = "Workgroup";

                //Mensajes
                StrConf = "Confirmation";
                StrMenGuar = "The worker must have an assigned name";
                StrMenBorr = "The worker will be deleted, do you wish to continue?";
                StrMenPrev = "The unsaved progress will be deleted, do you wish to continue?";
                StrMenEGuar = "The worker could not be saved";
                StrMenEMod = "The worker could not be modified, so it was deleted so as not to cause instability in the database";
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Trabajadores";

                //Botones
                ButBusc.Content = "Buscar";
                ButAcep.Content = "Guardar";
                ButBorr.Content = "Borrar";

                //Label
                LblNom.Content = "Nombre";
                LblOrg.Content = "Organización";
                LblRol.Content = "Rol";
                LblDes.Content = "Es Desarrollador";
                LblCat.Content = "Categoría";
                LblCom.Content = "Comentario";
                LblBus.Text = "Buscador";

                //RadioButton
                RBSi.Content = "Si";
                RBNo.Content = "No";

                //Window
                Title = "Grupo de Trabajo";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El trabajador debe de tener un nombre asignado";
                StrMenBorr = "Se borrará el trabajador, ¿Desea continuar?";
                StrMenPrev = "Se borrará el progreso no guardado, ¿Desea continuar?";
                StrMenEGuar = "No se pudo guardar el trabajador";
                StrMenEMod = "No se pudo modificar el trabajador, por lo que se eliminó para no ocasionar inestabilidad en la base de datos";
            }
        }
    }
}