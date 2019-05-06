using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ReadyReq
{
    public partial class WinReqFun : Window
    {
        int FilaPrevN = -1;
        int FilaPrevE = -1;
        ClsReqFun Requisito = new ClsReqFun();
        DataRow Fila;
        Control ctrl;
        int TipoReq = 3;
        int LinSNor = -1;
        int LinSExc = -1;
        bool Activo = false;
        bool Base = false;
        string StrConf;
        string StrMenGuar;
        string StrMenBorr;
        string StrMenPrev;
        string StrMenDrop;
        string StrMenEGuar;
        string StrMenEMod;
        public WinReqFun()
        {
            InitializeComponent();
            this.DGSecNor.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DGPreviewMouseLeftButtonDown);
            this.DGSecNor.Drop += new DragEventHandler(DGDrop);
            this.DGSecExc.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DGPreviewMouseLeftButtonDown);
            this.DGSecExc.Drop += new DragEventHandler(DGDrop);
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            IniciarTablas();
            for (int i = 1; (i <= 10); i++)
                CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
            for (int i = 0; i <= (Requisito.BPaquete.Rows.Count - 1); i++)
            {
                Fila = Requisito.BPaquete.Rows[i];
                CmbPaquete.Items.Add(Fila[0].ToString());
            }
            try { CmbPaquete.Text = CmbPaquete.Items[0].ToString(); }
            catch { CmbPaquete.Text = ""; }
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
                    Requisito.Precondicion = TxtPreCond.Text;
                    Requisito.Postcondicion = TxtPostCond.Text;
                    Requisito.Paquete = CmbPaquete.Text;
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
            if (ctrl.Name == "ButBorrLinN")
            {
                LinSNor = -1;
                TxtSecNor.Text = string.Empty;
            }
            if (ctrl.Name == "ButBorrLinE")
            {
                LinSExc = -1;
                TxtSecExc.Text = string.Empty;
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
            if (ctrl.Name == "DGBActor")
            {
                bool existe = false;
                if (Requisito.Actores.Rows.Count > 0)
                {
                    for (int i = 0; i <= (Requisito.Actores.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Actores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                if (existe == false)
                {
                    Fila = Requisito.Actores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Actores.Rows.Add(Fila);
                }
                Requisito.BActores.Rows.RemoveAt(DGBActor.SelectedIndex);
            }
            if (ctrl.Name == "DGActores")
            {
                Fila = Requisito.BActores.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGActores.Items[DGActores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGActores.Items[DGActores.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BActores.Rows.Add(Fila);
                Requisito.Actores.Rows.RemoveAt(DGActores.SelectedIndex);
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
                if ((ctrl.Name == "TxtPreCond") && (string.IsNullOrEmpty(TxtPreCond.Text) == false)) TxtSecNor.Focus();
                if ((ctrl.Name == "TxtPostCond") && (string.IsNullOrEmpty(TxtPostCond.Text) == false)) TxtSecExc.Focus();
                if (ctrl.Name == "TxtBus") ButBusc.Focus();
                if ((ctrl.Name == "TxtSecNor") && (string.IsNullOrEmpty(TxtSecNor.Text) == false))
                {
                    if (LinSNor == -1)
                        Requisito.SecNormal.Add(new ClsDatDG() { Descrip = TxtSecNor.Text });
                    else
                        Requisito.SecNormal.Insert(LinSNor, new ClsDatDG() { Descrip = TxtSecNor.Text });

                    LinSNor = -1;
                    TxtSecNor.Text = string.Empty;
                }
                if ((ctrl.Name == "TxtSecExc") && (string.IsNullOrEmpty(TxtSecExc.Text) == false))
                {
                    if (LinSExc == -1)
                        Requisito.SecExcepc.Add(new ClsDatDG() { Descrip = TxtSecExc.Text });
                    else
                        Requisito.SecExcepc.Insert(LinSExc, new ClsDatDG() { Descrip = TxtSecExc.Text });

                    LinSExc = -1;
                    TxtSecExc.Text = string.Empty;
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
            ctrl = ((Control)sender);
            if (ctrl.Name == "DGSecNor")
            {
                if (string.IsNullOrEmpty(TxtSecNor.Text) && DGSecNor.SelectedIndex > -1)
                {
                    TxtSecNor.Text = Requisito.SecNormal[DGSecNor.SelectedIndex].Descrip;
                    LinSNor = DGSecNor.SelectedIndex;
                    Requisito.SecNormal.RemoveAt(DGSecNor.SelectedIndex);
                    DGSecNor.SelectedIndex = -1;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(TxtSecExc.Text) && DGSecExc.SelectedIndex > -1)
                {
                    TxtSecExc.Text = Requisito.SecExcepc[DGSecExc.SelectedIndex].Descrip;
                    LinSExc = DGSecExc.SelectedIndex;
                    Requisito.SecExcepc.RemoveAt(DGSecExc.SelectedIndex);
                    DGSecExc.SelectedIndex = -1;
                }
            }
        }
        private void DGDrop(object sender, DragEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "DGSecNor")
            {
                if (FilaPrevN < 0)
                    return;

                int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);

                if (index < 0)
                    return;
                if (index == FilaPrevN)
                    return;
                if (index == DGSecNor.Items.Count - 1)
                {
                    MessageBox.Show(StrMenDrop);
                    return;
                }

                ClsDatDG movedEmps = Requisito.SecNormal[FilaPrevN];
                Requisito.SecNormal.RemoveAt(FilaPrevN);
                Requisito.SecNormal.Insert(index, movedEmps);
            }
            else
            {
                if (FilaPrevE < 0)
                    return;

                int index = this.GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);

                if (index < 0)
                    return;
                if (index == FilaPrevE)
                    return;
                if (index == DGSecExc.Items.Count - 1)
                {
                    MessageBox.Show(StrMenDrop);
                    return;
                }

                ClsDatDG movedEmps = Requisito.SecExcepc[FilaPrevE];
                Requisito.SecExcepc.RemoveAt(FilaPrevE);
                Requisito.SecExcepc.Insert(index, movedEmps);
            }
        }
        private void DGPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ctrl = ((Control)sender);
            if (ctrl.Name == "DGSecNor")
            {
                FilaPrevN = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);

                if (FilaPrevN < 0)
                    return;
                DGSecNor.SelectedIndex = FilaPrevN;

                ClsDatDG selectedEmp = DGSecNor.Items[FilaPrevN] as ClsDatDG;

                if (selectedEmp == null)
                    return;

                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(DGSecNor, selectedEmp, dragdropeffects) != DragDropEffects.None)
                    DGSecNor.SelectedItem = selectedEmp;
            }
            else
            {
                FilaPrevE = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);

                if (FilaPrevE < 0)
                    return;
                DGSecExc.SelectedIndex = FilaPrevE;

                ClsDatDG selectedEmp = DGSecExc.Items[FilaPrevE] as ClsDatDG;

                if (selectedEmp == null)
                    return;

                DragDropEffects dragdropeffects = DragDropEffects.Move;

                if (DragDrop.DoDragDrop(DGSecExc, selectedEmp, dragdropeffects) != DragDropEffects.None)
                    DGSecExc.SelectedItem = selectedEmp;

            }
        }
        private bool IsTheMouseOnTargetRow(Visual theTarget, GetDragDropPosition pos)
        {
            Rect posBounds = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point theMousePos = pos((IInputElement)theTarget);
            return posBounds.Contains(theMousePos);
        }
        private DataGridRow GetDataGridRowItem(int index, Control ctrl)
        {
            if (ctrl.Name == "DGSecNor")
            {
                if (DGSecNor.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    return null;

                return DGSecNor.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }
            else
            {
                if (DGSecExc.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    return null;

                return DGSecExc.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }
        }
        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos, Control ctrl)
        {
            int curIndex = -1;
            if (ctrl.Name == "DGSecNor")
            {
                for (int i = 0; i < DGSecNor.Items.Count; i++)
                {
                    DataGridRow itm = GetDataGridRowItem(i, ctrl);
                    if (IsTheMouseOnTargetRow(itm, pos))
                    {
                        curIndex = i;
                        break;
                    }
                }
                return curIndex;
            }
            else
            {
                for (int i = 0; i < DGSecExc.Items.Count; i++)
                {
                    DataGridRow itm = GetDataGridRowItem(i, ctrl);
                    if (IsTheMouseOnTargetRow(itm, pos))
                    {
                        curIndex = i;
                        break;
                    }
                }
                return curIndex;
            }
        }
        private void VaciarInterfaz()
        {
            TAB.SelectedIndex = 0;
            TxtNom.Text = string.Empty;
            TxtDesc.Text = string.Empty;
            RBCM.IsChecked = true;
            RBPM.IsChecked = true;
            RBUM.IsChecked = true;
            RBEM.IsChecked = true;
            RBVer.IsChecked = true;
            TxtPreCond.Text = string.Empty;
            TxtSecNor.Text = string.Empty;
            TxtPostCond.Text = string.Empty;
            TxtSecExc.Text = string.Empty;
            CmbCat.Text = CmbCat.Items[0].ToString();
            CmbPaquete.Text = CmbPaquete.Items[0].ToString();
            TxtCom.Text = string.Empty;
            RBReqFun.IsChecked = true;
            LinSNor = 0;
            LinSExc = 0;
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

            CmbPaquete.Text = Requisito.Paquete;
            RadioButtonValor(false);
            TxtPreCond.Text = Requisito.Precondicion;
            TxtPostCond.Text = Requisito.Postcondicion;
            CmbCat.Text = Requisito.Categoria.ToString();
            TxtCom.Text = Requisito.Comentario;

            DGObjetivos.ItemsSource = Requisito.Objetivos.DefaultView;
            DGFuentes.ItemsSource = Requisito.Fuentes.DefaultView;
            DGAutores.ItemsSource = Requisito.Autores.DefaultView;
            DGReqRel.ItemsSource = Requisito.Requisitos.DefaultView;
            DGActores.ItemsSource = Requisito.Actores.DefaultView;

            DGGruAut.ItemsSource = Requisito.BGrupo.DefaultView;
            DGGruFuen.ItemsSource = Requisito.BFuentes.DefaultView;
            DGObjObj.ItemsSource = Requisito.BObjetivos.DefaultView;
            DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;
            DGBActor.ItemsSource = Requisito.BActores.DefaultView;

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
                DGBActor.Columns[0].Header = "Actors";
                DGActores.Columns[0].Header = "Related actors";
                DGSecNor.Columns[0].Header = "Normal sequence";
                DGSecExc.Columns[0].Header = "Sequence of exceptions";

                //Botones
                ButBusc.Content = "Search";
                ButAcep.Content = "Save";
                ButBorr.Content = "Delete";

                //Label
                LblNom.Content = "Name";
                LblDes.Content = "Description";
                LblComp.Content = "Complexity";
                LblPri.Content = "Priority";
                LblUrg.Content = "Urgency";
                LblEst.Content = "Stability";
                LblEsta.Content = "State";
                LblCat.Content = "Category";
                LblCom.Content = "Commentary";
                LblPre.Content = "Precondition";
                LblPost.Content = "Postcondition";
                LblPaq.Content = "Package";
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
                RBCB.Content = "Low";
                RBCM.Content = "Medium";
                RBCA.Content = "High";

                //Window
                Title = "Functional Project Requirements";

                //TabItem
                TabDat.Header = "Data";
                TabAut.Header = "Authors";
                TabFue.Header = "Sources";
                TabObj.Header = "Objectives";
                TabReq.Header = "Requirem.";
                TabAct.Header = "Actors";
                TabSNor.Header = "Normal Seq.";
                TabSExc.Header = "Seq. Exc.";

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
                DGBActor.Columns[0].Header = "Actores";
                DGActores.Columns[0].Header = "Actores relacionados";
                DGSecNor.Columns[0].Header = "Secuencia normal";
                DGSecExc.Columns[0].Header = "Secuencia de excepciones";

                //Botones
                ButBusc.Content = "Buscar";
                ButAcep.Content = "Guardar";
                ButBorr.Content = "Borrar";

                //Label
                LblNom.Content = "Nombre";
                LblDes.Content = "Descripción";
                LblComp.Content = "Complejidad";
                LblPri.Content = "Prioridad";
                LblUrg.Content = "Urgencia";
                LblEst.Content = "Estabilidad";
                LblEsta.Content = "Estado";
                LblCat.Content = "Categoría";
                LblCom.Content = "Comentario";
                LblPre.Content = "Precondición";
                LblPost.Content = "Postcondición";
                LblPaq.Content = "Paquete";
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
                RBCB.Content = "Baja";
                RBCM.Content = "Media";
                RBCA.Content = "Alta";

                //Window
                Title = "Requisitos Funcionales del Proyecto";

                //TabItem
                TabDat.Header = "Datos";
                TabAut.Header = "Autores";
                TabFue.Header = "Fuentes";
                TabObj.Header = "Objetivos";
                TabReq.Header = "Requisitos";
                TabAct.Header = "Actores";
                TabSNor.Header = "Sec. Normal";
                TabSExc.Header = "Sec. Exc.";

                //Mensajes
                StrConf = "Confirmación";
                StrMenGuar = "El requisito debe de tener un nombre asignado";
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
            DGBActor.ItemsSource = Requisito.BActores.DefaultView;

            DGAutores.ItemsSource = Requisito.Autores.DefaultView;
            DGFuentes.ItemsSource = Requisito.Fuentes.DefaultView;
            DGObjetivos.ItemsSource = Requisito.Objetivos.DefaultView;
            DGReqRel.ItemsSource = Requisito.Requisitos.DefaultView;
            DGActores.ItemsSource = Requisito.Actores.DefaultView;

            Requisito.SecNormal = Resources["SecNor"] as ClsDatDGCollection;
            Requisito.SecExcepc = Resources["SecExc"] as ClsDatDGCollection;
        }
        private void RadioButtonValor(bool ValorRB)
        {
            if (ValorRB == true)
            {
                //Complejidad
                if (RBCB.IsChecked == true)
                    Requisito.Complejidad = 1;
                else if (RBCM.IsChecked == true)
                    Requisito.Complejidad = 2;
                else if (RBCA.IsChecked == true)
                    Requisito.Complejidad = 3;
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
                //Complejidad
                if (Requisito.Complejidad == 1)
                    RBCB.IsChecked = true;
                else if (Requisito.Complejidad == 2)
                    RBCM.IsChecked = true;
                else if (Requisito.Complejidad == 3)
                    RBCA.IsChecked = true;
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