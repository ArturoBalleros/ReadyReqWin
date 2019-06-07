using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ReadyReq.ViewModel
{
    public partial class WinReqFun : Window
    {
        int FilaPrevN = -1;
        int FilaPrevE = -1;
        ClsReqFun Requisito = new ClsReqFun();
        DataRow Fila;
        Control ctrl;
        int TipoReq = DefValues.ReqFun;
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
        string StrMenEFec;
        string StrMenEVer;
        public WinReqFun()
        {
            InitializeComponent();
            DGSecNor.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DGPreviewMouseLeftButtonDown);
            DGSecNor.Drop += new DragEventHandler(DGDrop);
            DGSecExc.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(DGPreviewMouseLeftButtonDown);
            DGSecExc.Drop += new DragEventHandler(DGDrop);
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            TxtVer.Text = "1.0";
            TxtFec.Text = DateTime.Today.ToShortDateString();
            IniciarTablas();
            for (int i = 1; (i <= 10); i++) CmbCat.Items.Add(i);
            CmbCat.Text = CmbCat.Items[0].ToString();
            for (int i = 0; i <= (Requisito.BPaquete.Rows.Count - 1); i++)
            {
                Fila = Requisito.BPaquete.Rows[i];
                CmbPaquete.Items.Add(Fila[0].ToString());
            }
            try { CmbPaquete.Text = CmbPaquete.Items[0].ToString(); }
            catch { CmbPaquete.Text = ""; }
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
                Requisito.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Requisito.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
            {
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text))
                    {
                        if (ClsFunciones.TryConvertToDouble(TxtVer.Text))
                        {
                            Requisito.Nombre = TxtNom.Text;
                            Requisito.Version = ClsFunciones.StringToDouble(TxtVer.Text);
                            Requisito.Fecha = DateTime.Parse(TxtFec.Text);
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
                        else MessageBox.Show(StrMenEVer);
                    }
                    else MessageBox.Show(StrMenEFec);
                }
                else MessageBox.Show(StrMenGuar);
            }
            if (ctrl.Name.Equals("ButBorr"))
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Requisito.Borrar();
                VaciarInterfaz();
            }
            if (ctrl.Name.Equals("ButBorrLinN"))
            {
                LinSNor = -1;
                TxtSecNor.Text = string.Empty;
            }
            if (ctrl.Name.Equals("ButBorrLinE"))
            {
                LinSExc = -1;
                TxtSecExc.Text = string.Empty;
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("DGBuscar"))
            {
                if (Activo && DGBuscar.SelectedIndex > -1)
                {
                    if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarRequisito();
                }
                else CargarRequisito();
            }
            if (ctrl.Name.Equals("DGGruAut"))
            {
                bool existe = false;
                if (Requisito.Autores.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Autores.Rows.Add(Fila);
                }
                Requisito.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGAutores"))
            {
                Fila = Requisito.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BGrupo.Rows.Add(Fila);
                Requisito.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGGruFuen"))
            {
                bool existe = false;
                if (Requisito.Fuentes.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Fuentes.Rows.Add(Fila);
                }
                Requisito.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGFuentes"))
            {
                Fila = Requisito.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BFuentes.Rows.Add(Fila);
                Requisito.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGObjObj"))
            {
                bool existe = false;
                if (Requisito.Objetivos.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Objetivos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Objetivos.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Objetivos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Objetivos.Rows.Add(Fila);
                }
                Requisito.BObjetivos.Rows.RemoveAt(DGObjObj.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGObjetivos"))
            {
                Fila = Requisito.BObjetivos.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGObjetivos.Items[DGObjetivos.SelectedIndex]).Row.ItemArray[0]);
                Requisito.BObjetivos.Rows.Add(Fila);
                Requisito.Objetivos.Rows.RemoveAt(DGObjetivos.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGRequi"))
            {
                bool existe = false;
                if (Requisito.Requisitos.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Requisitos.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Requisitos.Rows[i];
                        if ((Fila[0].ToString() == Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0])) && (int.Parse(Fila[1].ToString()) == TipoReq))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Requisitos.NewRow();
                    Fila[2] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[1]);
                    Fila[1] = TipoReq;
                    Fila[0] = Convert.ToString(((DataRowView)DGRequi.Items[DGRequi.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Requisitos.Rows.Add(Fila);
                }
                Requisito.BRequisitos.Rows.RemoveAt(DGRequi.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGReqRel"))
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
            if (ctrl.Name.Equals("DGBActor"))
            {
                bool existe = false;
                if (Requisito.Actores.Rows.Count > 0)
                    for (int i = 0; i <= (Requisito.Actores.Rows.Count - 1); i++)
                    {
                        Fila = Requisito.Actores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Requisito.Actores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGBActor.Items[DGBActor.SelectedIndex]).Row.ItemArray[0]);
                    Requisito.Actores.Rows.Add(Fila);
                }
                Requisito.BActores.Rows.RemoveAt(DGBActor.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGActores"))
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
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("TxtNom") && Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text))
                {
                    int idExiste = Requisito.ComprobarExistencia(TxtNom.Text);
                    if (idExiste != -1) CargarRequisito(idExiste);
                    TxtVer.Focus();
                }
                if (ctrl.Name.Equals("TxtVer") && !string.IsNullOrEmpty(TxtVer.Text))
                {
                    if (ClsFunciones.TryConvertToDouble(TxtVer.Text)) TxtFec.Focus();
                    else MessageBox.Show(StrMenEVer);
                }
                if (ctrl.Name.Equals("TxtFec") && !string.IsNullOrEmpty(TxtFec.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text)) TxtDesc.Focus();
                    else MessageBox.Show(StrMenEFec);
                }
                if (ctrl.Name.Equals("TxtDesc") && !string.IsNullOrEmpty(TxtDesc.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtPreCond") && !string.IsNullOrEmpty(TxtPreCond.Text)) TxtSecNor.Focus();
                if (ctrl.Name.Equals("TxtPostCond") && !string.IsNullOrEmpty(TxtPostCond.Text)) TxtSecExc.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
                if (ctrl.Name.Equals("TxtSecNor") && !string.IsNullOrEmpty(TxtSecNor.Text))
                {
                    if (LinSNor == -1) Requisito.SecNormal.Add(new ClsDatDG() { Descrip = TxtSecNor.Text });
                    else Requisito.SecNormal.Insert(LinSNor, new ClsDatDG() { Descrip = TxtSecNor.Text });
                    LinSNor = -1;
                    TxtSecNor.Text = string.Empty;
                }
                if (ctrl.Name.Equals("TxtSecExc") && !string.IsNullOrEmpty(TxtSecExc.Text))
                {
                    if (LinSExc == -1) Requisito.SecExcepc.Add(new ClsDatDG() { Descrip = TxtSecExc.Text });
                    else Requisito.SecExcepc.Insert(LinSExc, new ClsDatDG() { Descrip = TxtSecExc.Text });
                    LinSExc = -1;
                    TxtSecExc.Text = string.Empty;
                }
            }
        }
        private void Checked(object sender, RoutedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("RBReqInf")) TipoReq = DefValues.ReqInfo;
            if (ctrl.Name.Equals("RBReqNFun")) TipoReq = DefValues.ReqNFun;
            if (ctrl.Name.Equals("RBReqFun")) TipoReq = DefValues.ReqFun;
            try
            {
                Requisito.CargarTablaReqRel(TipoReq);
                DGRequi.ItemsSource = Requisito.BRequisitos.DefaultView;
            }
            catch { }
        }
        private void GridDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("DGSecNor"))
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
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("DGSecNor"))
            {
                if (FilaPrevN < 0) return;
                int index = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);
                if (index < 0) return;
                if (index == FilaPrevN) return;
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
                if (FilaPrevE < 0) return;
                int index = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);
                if (index < 0) return;
                if (index == FilaPrevE) return;
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
            ctrl = (Control)sender;
            if (ctrl.Name == "DGSecNor")
            {
                FilaPrevN = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);
                if (FilaPrevN < 0) return;
                DGSecNor.SelectedIndex = FilaPrevN;
                ClsDatDG selectedEmp = DGSecNor.Items[FilaPrevN] as ClsDatDG;
                if (selectedEmp == null) return;
                DragDropEffects dragdropeffects = DragDropEffects.Move;
                if (DragDrop.DoDragDrop(DGSecNor, selectedEmp, dragdropeffects) != DragDropEffects.None) DGSecNor.SelectedItem = selectedEmp;
            }
            else
            {
                FilaPrevE = GetDataGridItemCurrentRowIndex(e.GetPosition, ctrl);
                if (FilaPrevE < 0) return;
                DGSecExc.SelectedIndex = FilaPrevE;
                ClsDatDG selectedEmp = DGSecExc.Items[FilaPrevE] as ClsDatDG;
                if (selectedEmp == null) return;
                DragDropEffects dragdropeffects = DragDropEffects.Move;
                if (DragDrop.DoDragDrop(DGSecExc, selectedEmp, dragdropeffects) != DragDropEffects.None) DGSecExc.SelectedItem = selectedEmp;
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
            if (ctrl.Name.Equals("DGSecNor"))
            {
                if (DGSecNor.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return null;
                return DGSecNor.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }
            else
            {
                if (DGSecExc.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return null;
                return DGSecExc.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            }
        }
        private int GetDataGridItemCurrentRowIndex(GetDragDropPosition pos, Control ctrl)
        {
            int curIndex = -1;
            if (ctrl.Name.Equals("DGSecNor"))
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
            TxtVer.Text = "1.0";
            TxtFec.Text = DateTime.Today.ToShortDateString();
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
        private void CargarRequisito(int id = -1)
        {
            if (id == -1) Requisito.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])), TipoReq);
            else Requisito.Cargar(id);
            TxtNom.Text = Requisito.Nombre;
            TxtVer.Text = ClsFunciones.DoubleToString(Requisito.Version);
            TxtFec.Text = Requisito.Fecha.ToShortDateString();
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
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                DGBuscar.Columns[0].Header = DGRequi.Columns[0].Header = Ingles.Requirements;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Ingles.WorkGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Ingles.Authors;
                DGFuentes.Columns[0].Header = TabFue.Header = Ingles.Sources;
                DGObjObj.Columns[0].Header = TabObj.Header = Ingles.Objectives;
                DGObjetivos.Columns[0].Header = Ingles.RelObjet;
                DGReqRel.Columns[0].Header = Ingles.RelRequi;
                DGBActor.Columns[0].Header = TabAct.Header = Ingles.Actors;
                DGActores.Columns[0].Header = Ingles.RelAct;
                DGSecNor.Columns[0].Header = Ingles.SecNor;
                DGSecExc.Columns[0].Header = Ingles.SecExc;
                ButBusc.Content = Ingles.Search;
                ButAcep.Content = Ingles.Save;
                ButBorr.Content = Ingles.Delete;
                LblNom.Content = Ingles.Name;
                LblVer.Content = Ingles.Version;
                LblFec.Content = Ingles.Date;
                LblDes.Content = Ingles.Description;
                LblPri.Content = Ingles.Priority;
                LblUrg.Content = Ingles.Urgency;
                LblEst.Content = Ingles.Stability;
                LblEsta.Content = Ingles.State;
                LblCat.Content = Ingles.Category;
                LblCom.Content = Ingles.Commentary;
                LblComp.Content = Ingles.Complexity;
                LblPre.Content = Ingles.Precondición;
                LblPost.Content = Ingles.Postcondición;
                LblPaq.Content = Ingles.Paquete;
                LblBus.Text = Ingles.Search_Engine;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Ingles.VLow;
                RBPB.Content = RBUB.Content = RBEB.Content = RBCB.Content = Ingles.Low;
                RBPM.Content = RBUM.Content = RBEM.Content = RBCM.Content = Ingles.Medium;
                RBPA.Content = RBUA.Content = RBEA.Content = RBCA.Content = Ingles.High;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Ingles.VHigh;
                RBVer.Content = Ingles.Verified;
                RBNVer.Content = Ingles.NVerified;
                RBReqInf.Content = Ingles.RBReqInfo;
                RBReqNFun.Content = Ingles.RBReqNFun;
                RBReqFun.Content = Ingles.RBReqFun;
                Title = Español.ProReqFun;
                TabDat.Header = Ingles.Data;
                TabReq.Header = Ingles.TabReq;
                TabSNor.Header = Ingles.TabSecNor;
                TabSExc.Header = Ingles.TabSecExc;
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.ReqMenGuar;
                StrMenBorr = Ingles.ReqMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenDrop = Ingles.MenDrop;
                StrMenEGuar = Ingles.ReqMenEGuar;
                StrMenEMod = Ingles.ReqMenEMod;
                StrMenEFec = Ingles.MenEFec;
                StrMenEVer = Ingles.MenEVer;
            }
            else
            {
                DGBuscar.Columns[0].Header = DGRequi.Columns[0].Header = TabReq.Header = Español.Requisitos;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Español.TrabGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Español.Autores;
                DGFuentes.Columns[0].Header = TabFue.Header = Español.Fuentes;
                DGObjObj.Columns[0].Header = TabObj.Header = Español.Objetivos;
                DGObjetivos.Columns[0].Header = Español.RelObjet;
                DGReqRel.Columns[0].Header = Español.RelRequi;
                DGBActor.Columns[0].Header = TabAct.Header = Español.Actores;
                DGActores.Columns[0].Header = Español.RelAct;
                DGSecNor.Columns[0].Header = Español.SecNor;
                DGSecExc.Columns[0].Header = Español.SecExc;
                ButBusc.Content = Español.Buscar;
                ButAcep.Content = Español.Guardar;
                ButBorr.Content = Español.Borrar;
                LblNom.Content = Español.Nombre;
                LblVer.Content = Español.Version;
                LblFec.Content = Español.Fecha;
                LblDes.Content = Español.Descripción;
                LblPri.Content = Español.Prioridad;
                LblUrg.Content = Español.Urgencia;
                LblEst.Content = Español.Estabilidad;
                LblEsta.Content = Español.Estado;
                LblCat.Content = Español.Categoría;
                LblCom.Content = Español.Comentario;
                LblComp.Content = Español.Complejidad;
                LblPre.Content = Español.Precondición;
                LblPost.Content = Español.Postcondición;
                LblPaq.Content = Español.Paquete;
                LblBus.Text = Español.Buscador;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Español.MBaja;
                RBPB.Content = RBUB.Content = RBEB.Content = RBCB.Content = Español.Baja;
                RBPM.Content = RBUM.Content = RBEM.Content = RBCM.Content = Español.Media;
                RBPA.Content = RBUA.Content = RBEA.Content = RBCA.Content = Español.Alta;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Español.MAlta;
                RBVer.Content = Español.Verificado;
                RBNVer.Content = Español.NVerificado;
                RBReqInf.Content = Español.RBReqInfo;
                RBReqNFun.Content = Español.RBReqNFun;
                RBReqFun.Content = Español.RBReqFun;
                Title = Español.ProReqFun;
                TabDat.Header = Español.Datos;
                TabSNor.Header = Español.TabSecNor;
                TabSExc.Header = Español.TabSecExc;
                StrConf = Español.Confirmación;
                StrMenGuar = Español.ReqMenGuar;
                StrMenBorr = Español.ReqMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenDrop = Español.MenDrop;
                StrMenEGuar = Español.ReqMenEGuar;
                StrMenEMod = Español.ReqMenEMod;
                StrMenEFec = Español.MenEFec;
                StrMenEVer = Español.MenEVer;
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
            if (ValorRB)
            {
                //Complejidad
                if (RBCB.IsChecked == true) Requisito.Complejidad = 1;
                else if (RBCM.IsChecked == true) Requisito.Complejidad = 2;
                else if (RBCA.IsChecked == true) Requisito.Complejidad = 3;
                //Prioridad
                if (RBPMB.IsChecked == true) Requisito.Prioridad = 1;
                else if (RBPB.IsChecked == true) Requisito.Prioridad = 2;
                else if (RBPM.IsChecked == true) Requisito.Prioridad = 3;
                else if (RBPA.IsChecked == true) Requisito.Prioridad = 4;
                else if (RBPMA.IsChecked == true) Requisito.Prioridad = 5;
                //Urgencia
                if (RBUMB.IsChecked == true) Requisito.Urgencia = 1;
                else if (RBUB.IsChecked == true) Requisito.Urgencia = 2;
                else if (RBUM.IsChecked == true) Requisito.Urgencia = 3;
                else if (RBUA.IsChecked == true) Requisito.Urgencia = 4;
                else if (RBUMA.IsChecked == true) Requisito.Urgencia = 5;
                //Estabilidad
                if (RBEMB.IsChecked == true) Requisito.Estabilidad = 1;
                else if (RBEB.IsChecked == true) Requisito.Estabilidad = 2;
                else if (RBEM.IsChecked == true) Requisito.Estabilidad = 3;
                else if (RBEA.IsChecked == true) Requisito.Estabilidad = 4;
                else if (RBEMA.IsChecked == true) Requisito.Estabilidad = 5;
                //Estado
                if (RBVer.IsChecked == true) Requisito.Estado = true;
                else Requisito.Estado = false;
            }
            else
            {
                //Complejidad
                if (Requisito.Complejidad == 1) RBCB.IsChecked = true;
                else if (Requisito.Complejidad == 2) RBCM.IsChecked = true;
                else if (Requisito.Complejidad == 3) RBCA.IsChecked = true;
                //Prioridad
                if (Requisito.Prioridad == 1) RBPMB.IsChecked = true;
                else if (Requisito.Prioridad == 2) RBPB.IsChecked = true;
                else if (Requisito.Prioridad == 3) RBPM.IsChecked = true;
                else if (Requisito.Prioridad == 4) RBPA.IsChecked = true;
                else if (Requisito.Prioridad == 5) RBPMA.IsChecked = true;
                //Urgencia
                if (Requisito.Urgencia == 1) RBUMB.IsChecked = true;
                else if (Requisito.Urgencia == 2) RBUB.IsChecked = true;
                else if (Requisito.Urgencia == 3) RBUM.IsChecked = true;
                else if (Requisito.Urgencia == 4) RBUA.IsChecked = true;
                else if (Requisito.Urgencia == 5) RBUMA.IsChecked = true;
                //Estabilidad
                if (Requisito.Estabilidad == 1) RBEMB.IsChecked = true;
                else if (Requisito.Estabilidad == 2) RBEB.IsChecked = true;
                else if (Requisito.Estabilidad == 3) RBEM.IsChecked = true;
                else if (Requisito.Estabilidad == 4) RBEA.IsChecked = true;
                else if (Requisito.Estabilidad == 5) RBEMA.IsChecked = true;
                //Estado
                if (Requisito.Estado) RBVer.IsChecked = true;
                else RBNVer.IsChecked = true;
            }
        }
    }
}