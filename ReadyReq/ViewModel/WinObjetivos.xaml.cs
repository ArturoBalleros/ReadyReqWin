using ReadyReq.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    public partial class WinObjetivos : Window
    {
        ClsObjetivo Objetivo = new ClsObjetivo();
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
        public WinObjetivos()
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
                Objetivo.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Objetivo.Buscador.DefaultView;
            }
            if (ctrl.Name == "ButAcep")
            {
                if (string.IsNullOrEmpty(TxtNom.Text) == false)
                {
                    Objetivo.Nombre = TxtNom.Text;
                    Objetivo.Descripcion = TxtDes.Text;
                    RadioButtonValor(true);
                    Objetivo.Categoria = int.Parse(CmbCat.Text);
                    Objetivo.Comentario = TxtCom.Text;
                    int resultado = Objetivo.Guardar();
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
                        Objetivo.Borrar();
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
                        CargarObjetivo();
                }
                else
                {
                    CargarObjetivo();
                }
            }
            if (ctrl.Name == "DGGruAut")
            {
                bool existe = false;
                if (Objetivo.Autores.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Objetivo.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Objetivo.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Autores.Rows.Add(Fila);
                }
                Objetivo.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name == "DGAutores")
            {
                Fila = Objetivo.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Objetivo.BGrupo.Rows.Add(Fila);
                Objetivo.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name == "DGGruFuen")
            {
                bool existe = false;
                if (Objetivo.Fuentes.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Objetivo.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Objetivo.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Fuentes.Rows.Add(Fila);
                }
                Objetivo.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name == "DGFuentes")
            {
                Fila = Objetivo.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Objetivo.BFuentes.Rows.Add(Fila);
                Objetivo.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
            if (ctrl.Name == "DGObjObj")
            {
                bool existe = false;
                if (Objetivo.Objetivos.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Objetivo.Objetivos.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Objetivos.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Objetivo.Objetivos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Objetivos.Rows.Add(Fila);
                }
                Objetivo.BObjetivos.Rows.RemoveAt(DGObjObj.SelectedIndex);
            }
            if (ctrl.Name == "DGSubobj")
            {
                Fila = Objetivo.BObjetivos.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGSubobj.Items[DGSubobj.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGSubobj.Items[DGSubobj.SelectedIndex]).Row.ItemArray[0]);
                Objetivo.BObjetivos.Rows.Add(Fila);
                Objetivo.Objetivos.Rows.RemoveAt(DGSubobj.SelectedIndex);
            }
        }
        private void Presionar(object sender, KeyEventArgs e)
        {
            ctrl = ((Control)sender);
            if ((ctrl.Name == "TxtNom") && (Activo == false))
                Activo = true;
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtNom") && (string.IsNullOrEmpty(TxtNom.Text) == false)) TxtDes.Focus();
                if ((ctrl.Name == "TxtDes") && (string.IsNullOrEmpty(TxtDes.Text) == false)) TxtCom.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            Tab.SelectedIndex = 0;
            TxtNom.Text = string.Empty;
            TxtDes.Text = string.Empty;
            RBPM.IsChecked = true;
            RBUM.IsChecked = true;
            RBEM.IsChecked = true;
            RBVer.IsChecked = true;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            Activo = false;
            Base = false;
            Objetivo.IniciarValores();
            IniciarTablas();
        }
        private void CargarObjetivo()
        {
            Objetivo.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            TxtNom.Text = Objetivo.Nombre;
            TxtDes.Text = Objetivo.Descripcion;
            RadioButtonValor(false);
            CmbCat.Text = Objetivo.Categoria.ToString();
            TxtCom.Text = Objetivo.Comentario;

            DGSubobj.ItemsSource = Objetivo.Objetivos.DefaultView;
            DGFuentes.ItemsSource = Objetivo.Fuentes.DefaultView;
            DGAutores.ItemsSource = Objetivo.Autores.DefaultView;

            DGGruAut.ItemsSource = Objetivo.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Objetivo.BFuentes.DefaultView;
            DGObjObj.ItemsSource = Objetivo.BObjetivos.DefaultView;

            Activo = true;
            Base = true;

            Objetivo.Buscador.Rows.Clear();
        }
        private void Idioma()
        {
            if (ClsConf.Idioma == "Ingles")
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Objectives";
                DGGruAut.Columns[0].Header = "Workers of the Working Group";
                DGAutores.Columns[0].Header = "Authors";
                DGGruFuen.Columns[0].Header = "Workers of the Working Group";
                DGFuentes.Columns[0].Header = "Sources";
                DGObjObj.Columns[0].Header = "Objectives";
                DGSubobj.Columns[0].Header = "Subobjectives";

                //Botones
                ButBusc.Content = "Search";
                ButAcep.Content = "Save";
                ButBorr.Content = "Delete";

                //Label
                LblNom.Content = "Name";
                LblDes.Content = "Description";
                LblPri.Content = "Priority";
                LblUrg.Content = "Urgency";
                LblEst.Content = "Stability";
                LblEsta.Content = "State";
                LblCat.Content = "Category";
                LblCom.Content = "Commentary";
                LblBus.Text = "Search Engine";

                //RadioButton
                RBPMB.Content = RBUMB.Content = RBEMB.Content = "Very low";
                RBPB.Content = RBUB.Content = RBEB.Content = "Low";
                RBPM.Content = RBUM.Content = RBEM.Content = "Medium";
                RBPA.Content = RBUA.Content = RBEA.Content = "High";
                RBPMA.Content = RBUMA.Content = RBEMA.Content = "Very high";
                RBVer.Content = "Verified";
                RBNVer.Content = "Not verified";

                //Window
                Title = "Objectives of the Project";

                //TabItem
                TabDat.Header = "Data";
                TabAut.Header = "Authors";
                TabFue.Header = "Sources";
                TabSubObj.Header = "Subobjectives";

                //Mensajes
                StrConf = "Confirmation";
                StrMenGuar = "The objective must have an assigned name";
                StrMenBorr = "The objective will be deleted, do you wish to continue?";
                StrMenPrev = "The unsaved progress will be deleted, do you wish to continue?";
                StrMenEGuar = "The objective could not be saved";
                StrMenEMod = "The objective could not be modified, so it was deleted so as not to cause instability in the database";
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Objetivos";
                DGGruAut.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGAutores.Columns[0].Header = "Autores";
                DGGruFuen.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGFuentes.Columns[0].Header = "Fuentes";
                DGObjObj.Columns[0].Header = "Objetivos";
                DGSubobj.Columns[0].Header = "Subobjetivos";

                //Botones
                ButBusc.Content = "Buscar";
                ButAcep.Content = "Guardar";
                ButBorr.Content = "Borrar";

                //Label
                LblNom.Content = "Nombre";
                LblDes.Content = "Descripción";
                LblPri.Content = "Prioridad";
                LblUrg.Content = "Urgencia";
                LblEst.Content = "Estabilidad";
                LblEsta.Content = "Estado";
                LblCat.Content = "Categoría";
                LblCom.Content = "Comentario";
                LblBus.Text = "Buscador";

                //RadioButton
                RBPMB.Content = RBUMB.Content = RBEMB.Content = "Muy baja";
                RBPB.Content = RBUB.Content = RBEB.Content = "Baja";
                RBPM.Content = RBUM.Content = RBEM.Content = "Media";
                RBPA.Content = RBUA.Content = RBEA.Content = "Alta";
                RBPMA.Content = RBUMA.Content = RBEMA.Content = "Muy Alta";
                RBVer.Content = "Verificado";
                RBNVer.Content = "No verificado";

                //Window
                Title = "Objetivos del Proyecto";

                //TabItem
                TabDat.Header = "Datos";
                TabAut.Header = "Autores";
                TabFue.Header = "Fuentes";
                TabSubObj.Header = "Subobjetivos";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El objetivo debe de tener un nombre asignado";
                StrMenBorr = "Se borrará el objetivo, ¿Desea continuar?";
                StrMenPrev = "Se borrará el progreso no guardado, ¿Desea continuar?";
                StrMenEGuar = "No se pudo guardar el objetivo";
                StrMenEMod = "No se pudo modificar el objetivo, por lo que se eliminó para no ocasionar inestabilidad en la base de datos";
            }
        }
        private void IniciarTablas()
        {
            Objetivo.CargarTablas();
            DGGruAut.ItemsSource = Objetivo.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Objetivo.BFuentes.DefaultView;
            DGObjObj.ItemsSource = Objetivo.BObjetivos.DefaultView;
            DGSubobj.ItemsSource = Objetivo.Objetivos.DefaultView;
            DGFuentes.ItemsSource = Objetivo.Fuentes.DefaultView;
            DGAutores.ItemsSource = Objetivo.Autores.DefaultView;
        }
        private void RadioButtonValor(bool ValorRB)
        {
            if (ValorRB == true)
            {
                //Prioridad
                if (RBPMB.IsChecked == true)
                    Objetivo.Prioridad = 1;
                else if (RBPB.IsChecked == true)
                    Objetivo.Prioridad = 2;
                else if (RBPM.IsChecked == true)
                    Objetivo.Prioridad = 3;
                else if (RBPA.IsChecked == true)
                    Objetivo.Prioridad = 4;
                else if (RBPMA.IsChecked == true)
                    Objetivo.Prioridad = 5;
                //Urgencia
                if (RBUMB.IsChecked == true)
                    Objetivo.Urgencia = 1;
                else if (RBUB.IsChecked == true)
                    Objetivo.Urgencia = 2;
                else if (RBUM.IsChecked == true)
                    Objetivo.Urgencia = 3;
                else if (RBUA.IsChecked == true)
                    Objetivo.Urgencia = 4;
                else if (RBUMA.IsChecked == true)
                    Objetivo.Urgencia = 5;
                //Estabilidad
                if (RBEMB.IsChecked == true)
                    Objetivo.Estabilidad = 1;
                else if (RBEB.IsChecked == true)
                    Objetivo.Estabilidad = 2;
                else if (RBEM.IsChecked == true)
                    Objetivo.Estabilidad = 3;
                else if (RBEA.IsChecked == true)
                    Objetivo.Estabilidad = 4;
                else if (RBEMA.IsChecked == true)
                    Objetivo.Estabilidad = 5;
                //Estado
                if (RBVer.IsChecked == true)
                    Objetivo.Estado = true;
                else
                    Objetivo.Estado = false;
            }
            else
            {
                //Prioridad
                if (Objetivo.Prioridad == 1)
                    RBPMB.IsChecked = true;
                else if (Objetivo.Prioridad == 2)
                    RBPB.IsChecked = true;
                else if (Objetivo.Prioridad == 3)
                    RBPM.IsChecked = true;
                else if (Objetivo.Prioridad == 4)
                    RBPA.IsChecked = true;
                else if (Objetivo.Prioridad == 5)
                    RBPMA.IsChecked = true;
                //Urgencia
                if (Objetivo.Urgencia == 1)
                    RBUMB.IsChecked = true;
                else if (Objetivo.Urgencia == 2)
                    RBUB.IsChecked = true;
                else if (Objetivo.Urgencia == 3)
                    RBUM.IsChecked = true;
                else if (Objetivo.Urgencia == 4)
                    RBUA.IsChecked = true;
                else if (Objetivo.Urgencia == 5)
                    RBUMA.IsChecked = true;
                //Estabilidad
                if (Objetivo.Estabilidad == 1)
                    RBEMB.IsChecked = true;
                else if (Objetivo.Estabilidad == 2)
                    RBEB.IsChecked = true;
                else if (Objetivo.Estabilidad == 3)
                    RBEM.IsChecked = true;
                else if (Objetivo.Estabilidad == 4)
                    RBEA.IsChecked = true;
                else if (Objetivo.Estabilidad == 5)
                    RBEMA.IsChecked = true;
                //Estado
                if (Objetivo.Estado == true)
                    RBVer.IsChecked = true;
                else
                    RBNVer.IsChecked = true;
            }
        }
    }
}