using ReadyReq.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ReadyReq.ViewModel
{
    public delegate Point GetDragDropPosition(IInputElement theElement);
    public partial class WinReqInfo : Window
    {
        int FilaPrev = -1;
        ClsReqInfo Requisito = new ClsReqInfo();
        DataRow Fila;
        Control ctrl;
        int TipoReq = 3;
        int LinDatEsp = -1;
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenDrop;
        string StrMenEGuar;
        string StrMenEMod;
        public WinReqInfo()
        {
            InitializeComponent();
            this.DGDatEsp.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DGDatEsp_PreviewMouseLeftButtonDown);
            this.DGDatEsp.Drop += new DragEventHandler(DGDatEsp_Drop);
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
                Requisito.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Requisito.Buscador.DefaultView;
            }
            if (ctrl.Name == "ButAcep")
            {
                if (string.IsNullOrEmpty(TxtNom.Text) == false)
                {
                    Requisito.Nombre = TxtNom.Text;
                    Requisito.Descripcion = TxtDesc.Text;
                    RadioButtonValor(true);
                    Requisito.Categoria = int.Parse(CmbCat.Text);
                    Requisito.Comentario = TxtCom.Text;
                    Requisito.TiempoMedio = int.Parse(LblTVM.Content.ToString());
                    Requisito.TiempoMaximo = int.Parse(LblTVMX.Content.ToString());
                    Requisito.OcurreMedio = int.Parse(LblOM.Content.ToString());
                    Requisito.OcurreMaximo = int.Parse(LblOMX.Content.ToString());
                    int resultado = Requisito.Guardar();
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
                        Requisito.Borrar();
                VaciarInterfaz();
            }
            if (ctrl.Name == "ButBorrLin")
            {
                LinDatEsp = -1;
                TxtDat.Text = string.Empty;
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
                        CargarRequisito();
                }
                else
                {
                    CargarRequisito();
                }
            }
            if (ctrl.Name == "DGGruAut")
            {
                bool existe = false;
                if (Requisito.Autores.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Requisito.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Requisito.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Autores.Rows.Add(Fila);
                }
                Requisito.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name == "DGAutores")
            {
                Fila = Requisito.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BGrupo.Rows.Add(Fila);
                Requisito.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name == "DGGruFuen")
            {
                bool existe = false;
                if (Requisito.Fuentes.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Requisito.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Requisito.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Fuentes.Rows.Add(Fila);
                }
                Requisito.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name == "DGFuentes")
            {
                Fila = Requisito.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BFuentes.Rows.Add(Fila);
                Requisito.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
            if (ctrl.Name == "DGObjObj")
            {
                bool existe = false;
                if (Requisito.Objetivos.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Requisito.Objetivos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Objetivos.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Requisito.Objetivos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Objetivos.Rows.Add(Fila);
                }
                Requisito.BObjetivos.Rows.RemoveAt(DGObjObj.SelectedIndex);
            }
            if (ctrl.Name == "DGObjetivos")
            {
                Fila = Requisito.BObjetivos.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BObjetivos.Rows.Add(Fila);
                Requisito.Objetivos.Rows.RemoveAt(DGObjetivos.SelectedIndex);
            }
            if (ctrl.Name == "DGRequi")
            {
                bool existe = false;
                if (Requisito.Requisitos.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Requisito.Requisitos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Requisitos.Rows[i];
                        if ((Fila[0].ToString() == Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0])) && (int.Parse(Fila[1].ToString()) == TipoReq))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Requisito.Requisitos.NewRow();
                    Fila[2] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[1]);
                    Fila[1] = TipoReq;
                    Fila[0] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Requisitos.Rows.Add(Fila);
                }
                Requisito.BRequisitos.Rows.RemoveAt(DGRequi.SelectedIndex);
            }
            if (ctrl.Name == "DGReqRel")
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
            ctrl = ((Control)sender);
            if ((ctrl.Name == "TxtNom") && (Activo == false))
                Activo = true;
            if (e.Key == Key.Enter)
            {
                if ((ctrl.Name == "TxtNom") && (string.IsNullOrEmpty(TxtNom.Text) == false)) TxtDesc.Focus();
                if ((ctrl.Name == "TxtDesc") && (string.IsNullOrEmpty(TxtDesc.Text) == false)) TxtCom.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
                if ((ctrl.Name == "TxtDat") && (string.IsNullOrEmpty(TxtDat.Text) == false))
                {
                    if (LinDatEsp == -1)
                        Requisito.DatosEspeci.Add(new ClsDatDG() { Descrip = TxtDat.Text });
                    else
                        Requisito.DatosEspeci.Insert(LinDatEsp, new ClsDatDG() { Descrip = TxtDat.Text });

                    LinDatEsp = -1;
                    TxtDat.Text = string.Empty;
                }
            }
        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "RBReqInf") TipoReq = 1;
            if (ctrl.Name == "RBReqNFun") TipoReq = 2;
            if (ctrl.Name == "RBReqFun") TipoReq = 3;
            try
            {
                Requisito.CargarTablaReqRel(TipoReq);
                DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;
            }
            catch { }
        }
        private void GridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtDat.Text) && DGDatEsp.SelectedIndex > -1)
            {
                TxtDat.Text = Requisito.DatosEspeci[DGDatEsp.SelectedIndex].Descrip;
                LinDatEsp = DGDatEsp.SelectedIndex;
                Requisito.DatosEspeci.RemoveAt(DGDatEsp.SelectedIndex);
                DGDatEsp.SelectedIndex = -1;
            }
        }
        private void DGDatEsp_Drop(object sender, DragEventArgs e)
        {
            if (FilaPrev < 0)
                return;

            int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition);

            if (index < 0)
                return;
            if (index == FilaPrev)
                return;
            if (index == DGDatEsp.Items.Count - 1)
            {
                MessageBox.Show(StrMenDrop);
                return;
            }

            ClsDatDG movedEmps = Requisito.DatosEspeci[FilaPrev];
            Requisito.DatosEspeci.RemoveAt(FilaPrev);
            Requisito.DatosEspeci.Insert(index, movedEmps);
        }
        private void DGDatEsp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FilaPrev = GetDataGridItemCurrentRowIndex(e.GetPosition);

            if (FilaPrev < 0)
                return;
            DGDatEsp.SelectedIndex = FilaPrev;

            ClsDatDG selectedEmp = DGDatEsp.Items[FilaPrev] as ClsDatDG;

            if (selectedEmp == null)
                return;

            DragDropEffects dragdropeffects = DragDropEffects.Move;

            if (DragDrop.DoDragDrop(DGDatEsp, selectedEmp, dragdropeffects) != DragDropEffects.None)
                DGDatEsp.SelectedItem = selectedEmp;
        }
        private bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }
        private DataGridRow GetDataGridRowItem(int index)
        {
            if (DGDatEsp.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return null;

            return DGDatEsp.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }
        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < DGDatEsp.Items.Count; i++)
            {
                DataGridRow itm = GetDataGridRowItem(i);
                if (IsTheMouseOnTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
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
            TxtDat.Text = string.Empty;
            SldTVM.Value = 0;
            SldTVMX.Value = 0;
            SldOM.Value = 0;
            SldOMX.Value = 0;
            CmbCat.Text = CmbCat.Items[0].ToString();
            TxtCom.Text = string.Empty;
            TxtBus.Text = string.Empty;
            RBReqFun.IsChecked = true;
            LinDatEsp = -1;
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
            SldTVM.Value = Requisito.TiempoMedio;
            SldTVMX.Value = Requisito.TiempoMaximo;
            SldOM.Value = Requisito.OcurreMedio;
            SldOMX.Value = Requisito.OcurreMaximo;
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
            if (ClsConf.Idioma == "Ingles")
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Requirements";
                DGGruAut.Columns[0].Header = "Workers of the Working Group";
                DGAutores.Columns[0].Header = "Authors";
                DGGruFuen.Columns[0].Header = "Workers of the Working Group";
                DGFuentes.Columns[0].Header = "Sources";
                DGObjObj.Columns[0].Header = "Objectives";
                DGObjetivos.Columns[0].Header = "Related objectives";
                DGRequi.Columns[0].Header = "Requirements";
                DGReqRel.Columns[0].Header = "Related requirements";
                DGDatEsp.Columns[0].Header = "Specific dates";

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
                LblTie.Content = "Time of life";
                LblOcu.Content = "Occurrences";
                LblTM.Content = LblOcM.Content = "Medium";
                LblTMX.Content = LblOcMX.Content = "Maximum";
                LblBus.Text = "Search Engine";

                //RadioButton
                RBPMB.Content = RBUMB.Content = RBEMB.Content = "Very low";
                RBPB.Content = RBUB.Content = RBEB.Content = "Low";
                RBPM.Content = RBUM.Content = RBEM.Content = "Medium";
                RBPA.Content = RBUA.Content = RBEA.Content = "High";
                RBPMA.Content = RBUMA.Content = RBEMA.Content = "Very high";
                RBVer.Content = "Verified";
                RBNVer.Content = "Not verified";
                RBReqInf.Content = "Information Req.";
                RBReqNFun.Content = "Non-Functional Req.";
                RBReqFun.Content = "Functional Req.";

                //Window
                Title = "Project Information Requirements";

                //TabItem
                TabDat.Header = "Data";
                TabAut.Header = "Authors";
                TabFue.Header = "Sources";
                TabObj.Header = "Objectives";
                TabReq.Header = "Requirem.";
                TabDatE.Header = "Specific D.";
                TabDatN.Header = "Num. D.";

                //Mensajes
                StrConf = "Confirmation";
                StrMenGuar = "The requirement must have an assigned name";
                StrMenBorr = "The requirement will be deleted, do you wish to continue?";
                StrMenPrev = "The unsaved progress will be deleted, do you wish to continue?";
                StrMenDrop = "This row-index cannot be used for Drop Operations";
                StrMenEGuar = "The requirement could not be saved";
                StrMenEMod = "The requirement could not be modified, so it was deleted so as not to cause instability in the database";
            }
            else
            {
                //DataGrid
                DGBuscar.Columns[0].Header = "Requisitos";
                DGGruAut.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGAutores.Columns[0].Header = "Autores";
                DGGruFuen.Columns[0].Header = "Trabajadores del Grupo de Trabajo";
                DGFuentes.Columns[0].Header = "Fuentes";
                DGObjObj.Columns[0].Header = "Objetivos";
                DGObjetivos.Columns[0].Header = "Objetivos relacionados";
                DGRequi.Columns[0].Header = "Requisitos";
                DGReqRel.Columns[0].Header = "Requisitos relacionados";
                DGDatEsp.Columns[0].Header = "Datos específicos";

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
                LblTie.Content = "Tiempo de vida";
                LblOcu.Content = "Ocurrencias";
                LblTM.Content = LblOcM.Content = "Medio";
                LblTMX.Content = LblOcMX.Content = "Máximo";
                LblBus.Text = "Buscador";

                //RadioButton
                RBPMB.Content = RBUMB.Content = RBEMB.Content = "Muy baja";
                RBPB.Content = RBUB.Content = RBEB.Content = "Baja";
                RBPM.Content = RBUM.Content = RBEM.Content = "Media";
                RBPA.Content = RBUA.Content = RBEA.Content = "Alta";
                RBPMA.Content = RBUMA.Content = RBEMA.Content = "Muy Alta";
                RBVer.Content = "Verificado";
                RBNVer.Content = "No verificado";
                RBReqInf.Content = "Req. Información";
                RBReqNFun.Content = "Req. No Funcionales";
                RBReqFun.Content = "Req. Funcionales";

                //Window
                Title = "Requisitos de Información del Proyecto";

                //TabItem
                TabDat.Header = "Datos";
                TabAut.Header = "Autores";
                TabFue.Header = "Fuentes";
                TabObj.Header = "Objetivos";
                TabReq.Header = "Requisitos";
                TabDatE.Header = "Dat.Espec.";
                TabDatN.Header = "Datos N.";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El actor debe de tener un nombre asignado";
                StrMenBorr = "Se borrará el requisito, ¿Desea continuar?";
                StrMenPrev = "Se borrará el progreso no guardado, ¿Desea continuar?";
                StrMenDrop = "Este índice de fila no se puede usar para operaciones de colocación";
                StrMenEGuar = "No se pudo guardar el requisito";
                StrMenEMod = "No se pudo modificar el requisito, por lo que se eliminó para no ocasionar inestabilidad en la base de datos";
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

            Requisito.DatosEspeci = Resources["DatEsp"] as ClsDatDGCollection;
        }
        private void RadioButtonValor(bool ValorRB)
        {
            if (ValorRB == true)
            {
                //Prioridad
                if (RBPMB.IsChecked == true)
                    Requisito.Prioridad = 1;
                else if (RBPB.IsChecked == true)
                    Requisito.Prioridad = 2;
                else if (RBPM.IsChecked == true)
                    Requisito.Prioridad = 3;
                else if (RBPA.IsChecked == true)
                    Requisito.Prioridad = 4;
                else if (RBPMA.IsChecked == true)
                    Requisito.Prioridad = 5;
                //Urgencia
                if (RBUMB.IsChecked == true)
                    Requisito.Urgencia = 1;
                else if (RBUB.IsChecked == true)
                    Requisito.Urgencia = 2;
                else if (RBUM.IsChecked == true)
                    Requisito.Urgencia = 3;
                else if (RBUA.IsChecked == true)
                    Requisito.Urgencia = 4;
                else if (RBUMA.IsChecked == true)
                    Requisito.Urgencia = 5;
                //Estabilidad
                if (RBEMB.IsChecked == true)
                    Requisito.Estabilidad = 1;
                else if (RBEB.IsChecked == true)
                    Requisito.Estabilidad = 2;
                else if (RBEM.IsChecked == true)
                    Requisito.Estabilidad = 3;
                else if (RBEA.IsChecked == true)
                    Requisito.Estabilidad = 4;
                else if (RBEMA.IsChecked == true)
                    Requisito.Estabilidad = 5;
                //Estado
                if (RBVer.IsChecked == true)
                    Requisito.Estado = true;
                else
                    Requisito.Estado = false;
            }
            else
            {
                //Prioridad
                if (Requisito.Prioridad == 1)
                    RBPMB.IsChecked = true;
                else if (Requisito.Prioridad == 2)
                    RBPB.IsChecked = true;
                else if (Requisito.Prioridad == 3)
                    RBPM.IsChecked = true;
                else if (Requisito.Prioridad == 4)
                    RBPA.IsChecked = true;
                else if (Requisito.Prioridad == 5)
                    RBPMA.IsChecked = true;
                //Urgencia
                if (Requisito.Urgencia == 1)
                    RBUMB.IsChecked = true;
                else if (Requisito.Urgencia == 2)
                    RBUB.IsChecked = true;
                else if (Requisito.Urgencia == 3)
                    RBUM.IsChecked = true;
                else if (Requisito.Urgencia == 4)
                    RBUA.IsChecked = true;
                else if (Requisito.Urgencia == 5)
                    RBUMA.IsChecked = true;
                //Estabilidad
                if (Requisito.Estabilidad == 1)
                    RBEMB.IsChecked = true;
                else if (Requisito.Estabilidad == 2)
                    RBEB.IsChecked = true;
                else if (Requisito.Estabilidad == 3)
                    RBEM.IsChecked = true;
                else if (Requisito.Estabilidad == 4)
                    RBEA.IsChecked = true;
                else if (Requisito.Estabilidad == 5)
                    RBEMA.IsChecked = true;
                //Estado
                if (Requisito.Estado == true)
                    RBVer.IsChecked = true;
                else
                    RBNVer.IsChecked = true;
            }
        }
    }
}