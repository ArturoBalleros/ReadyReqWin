using ReadyReq.Model;
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
            for (int i = 1; (i <= 10); i++)
                CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
            IniciarTablas();
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
                Actor.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Actor.Buscador.DefaultView;
            }
            if ((ctrl.Name == "ButAcep"))
            {
                if (string.IsNullOrEmpty(TxtNom.Text) == false)
                {
                    Actor.Nombre = TxtNom.Text;
                    Actor.Descripcion = TxtDesc.Text;
                    if (RBCB.IsChecked == true)
                        Actor.Complejidad = 1;
                    else if (RBCM.IsChecked == true)
                        Actor.Complejidad = 2;
                    else if (RBCA.IsChecked == true)
                        Actor.Complejidad = 3;
                    Actor.DescComplejidad = TxtDescCom.Text;
                    Actor.Categoria = int.Parse(CmbCat.Text);
                    Actor.Comentario = TxtCom.Text;
                    int resultado = Actor.Guardar();
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
                        Actor.Borrar();
                VaciarInterfaz();
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "DGBuscar")
            {
                if (Activo == true && DGBuscar.SelectedIndex > -1)
                {
                    if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        CargarActor();
                }
                else
                {
                    CargarActor();
                }
            }
            if (ctrl.Name == "DGGruAut")
            {
                bool existe = false;
                if (Actor.Autores.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Actor.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Actor.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Actor.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Actor.Autores.Rows.Add(Fila);
                }
                Actor.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name == "DGAutores")
            {
                Fila = Actor.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Actor.BGrupo.Rows.Add(Fila);
                Actor.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name == "DGGruFuen")
            {
                bool existe = false;
                if (Actor.Fuentes.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Actor.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Actor.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Actor.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Actor.Fuentes.Rows.Add(Fila);
                }
                Actor.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name == "DGFuentes")
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
            ctrl = ((Control)sender);
            if ((ctrl.Name == "TxtNom") && (Activo == false))
                Activo = true;
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtNom") && (string.IsNullOrEmpty(TxtNom.Text) == false)) TxtDesc.Focus();
                if ((ctrl.Name == "TxtDescCom") && (string.IsNullOrEmpty(TxtDescCom.Text) == false)) TxtCom.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
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
            if (Actor.Complejidad == 1)
                RBCB.IsChecked = true;
            else if (Actor.Complejidad == 2)
                RBCM.IsChecked = true;
            else if (Actor.Complejidad == 3)
                RBCA.IsChecked = true;
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
            if (ClsConf.Idioma == "Ingles")
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Actors";
                DGGruAut.Columns[0].Header = "Workers of the Working Group";
                DGAutores.Columns[0].Header = "Authors";
                DGGruFuen.Columns[0].Header = "Workers of the Working Group";
                DGFuentes.Columns[0].Header = "Sources";

                //Botones
                ButBusc.Content = "Search";
                ButAcep.Content = "Save";
                ButBorr.Content = "Delete";

                //Label
                LblNom.Content = "Name";
                LblDes.Content = "Description";
                LblComp.Content = "Complexity";
                LblDesCom.Content = "Desc. Comple.";
                LblCat.Content = "Category";
                LblCom.Content = "Commentary";
                LblBus.Text = "Search Engine";

                //RadioButton
                RBCB.Content = "Low";
                RBCM.Content = "Medium";
                RBCA.Content = "High";

                //Window
                Title = "Project Actors";

                //TabItem
                TabDat.Header = "Data";
                TabAut.Header = "Authors";
                TabFue.Header = "Sources";

                //Mensajes
                StrConf = "Confirmation";
                StrMenGuar = "The actor must have an assigned name";
                StrMenBorr = "The actor will be deleted, do you wish to continue?";
                StrMenPrev = "The unsaved progress will be deleted, do you wish to continue?";
                StrMenEGuar = "The actor could not be saved";
                StrMenEMod = "The actor could not be modified, so it was deleted so as not to cause instability in the database";
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Actores";
                DGGruAut.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGAutores.Columns[0].Header = "Autores";
                DGGruFuen.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGFuentes.Columns[0].Header = "Fuentes";

                //Botones
                ButBusc.Content = "Buscar";
                ButAcep.Content = "Guardar";
                ButBorr.Content = "Borrar";

                //Label
                LblNom.Content = "Nombre";
                LblDes.Content = "Descripción";
                LblComp.Content = "Complejidad";
                LblDesCom.Content = "Desc. Comple.";
                LblCat.Content = "Categoría";
                LblCom.Content = "Comentario";
                LblBus.Text = "Buscador";

                //RadioButton
                RBCB.Content = "Baja";
                RBCM.Content = "Media";
                RBCA.Content = "Alta";

                //Window
                Title = "Actores del Proyecto";

                //TabItem
                TabDat.Header = "Datos";
                TabAut.Header = "Autores";
                TabFue.Header = "Fuentes";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El actor debe de tener un nombre asignado";
                StrMenBorr = "Se borrará el actor, ¿Desea continuar?";
                StrMenPrev = "Se borrará el progreso no guardado, ¿Desea continuar?";
                StrMenEGuar = "No se pudo guardar el actor";
                StrMenEMod = "No se pudo modificar el actor, por lo que se eliminó para no ocasionar inestabilidad en la base de datos";
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