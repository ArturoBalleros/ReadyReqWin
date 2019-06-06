using ReadyReq.Model;
using ReadyReq.Util;
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
        string StrMenEFec;
        string StrMenEVer;
        public WinObjetivos()
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
            IniciarTablas();
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
                Objetivo.Buscar(TxtBus.Text);
                TxtBus.Text = string.Empty;
                DGBuscar.ItemsSource = Objetivo.Buscador.DefaultView;
            }
            if (ctrl.Name.Equals("ButAcep"))
                if (!string.IsNullOrEmpty(TxtNom.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text))
                    {
                        if (ClsFunciones.TryConvertToDouble(TxtVer.Text))
                        {
                            Objetivo.Nombre = TxtNom.Text;
                            Objetivo.Version = ClsFunciones.StringToDouble(TxtVer.Text);
                            Objetivo.Fecha = DateTime.Parse(TxtFec.Text);
                            Objetivo.Descripcion = TxtDes.Text;
                            RadioButtonValor(true);
                            Objetivo.Categoria = int.Parse(CmbCat.Text);
                            Objetivo.Comentario = TxtCom.Text;
                            int resultado = Objetivo.Guardar();
                            if (resultado == -1) MessageBox.Show(StrMenEMod);
                            if (resultado == -2) MessageBox.Show(StrMenEGuar);
                            VaciarInterfaz();
                        }
                        else MessageBox.Show(StrMenEVer);
                    }
                    else MessageBox.Show(StrMenEFec);
                }
                else MessageBox.Show(StrMenGuar);
            if (ctrl.Name.Equals("ButBorr"))
            {
                if (Base)
                    if (MessageBox.Show(StrMenBorr, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) Objetivo.Borrar();
                VaciarInterfaz();
            }
        }
        private void Seleccionar(object sender, SelectedCellsChangedEventArgs e)
        {
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("DGBuscar"))
            {
                if (Activo && DGBuscar.SelectedIndex > -1)
                {
                    if (MessageBox.Show(StrMenPrev, StrConf, MessageBoxButton.YesNo) == MessageBoxResult.Yes) CargarObjetivo();
                }
                else CargarObjetivo();
            }
            if (ctrl.Name.Equals("DGGruAut"))
            {
                bool existe = false;
                if (Objetivo.Autores.Rows.Count > 0)
                    for (int i = 0; i <= (Objetivo.Autores.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Autores.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Objetivo.Autores.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruAut.Items[DGGruAut.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Autores.Rows.Add(Fila);
                }
                Objetivo.BGrupo.Rows.RemoveAt(DGGruAut.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGAutores"))
            {
                Fila = Objetivo.BGrupo.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGAutores.Items[DGAutores.SelectedIndex]).Row.ItemArray[0]);
                Objetivo.BGrupo.Rows.Add(Fila);
                Objetivo.Autores.Rows.RemoveAt(DGAutores.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGGruFuen"))
            {
                bool existe = false;
                if (Objetivo.Fuentes.Rows.Count > 0)
                    for (int i = 0; i <= (Objetivo.Fuentes.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Fuentes.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Objetivo.Fuentes.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGGruFuen.Items[DGGruFuen.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Fuentes.Rows.Add(Fila);
                }
                Objetivo.BFuentes.Rows.RemoveAt(DGGruFuen.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGFuentes"))
            {
                Fila = Objetivo.BFuentes.NewRow();
                Fila[1] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[1]);
                Fila[0] = Convert.ToString(((DataRowView)DGFuentes.Items[DGFuentes.SelectedIndex]).Row.ItemArray[0]);
                Objetivo.BFuentes.Rows.Add(Fila);
                Objetivo.Fuentes.Rows.RemoveAt(DGFuentes.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGObjObj"))
            {
                bool existe = false;
                if (Objetivo.Objetivos.Rows.Count > 0)
                    for (int i = 0; i <= (Objetivo.Objetivos.Rows.Count - 1); i++)
                    {
                        Fila = Objetivo.Objetivos.Rows[i];
                        if (Fila[0].ToString() == Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]))
                        {
                            existe = true;
                            break;
                        }
                    }
                if (!existe)
                {
                    Fila = Objetivo.Objetivos.NewRow();
                    Fila[1] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[1]);
                    Fila[0] = Convert.ToString(((DataRowView)DGObjObj.Items[DGObjObj.SelectedIndex]).Row.ItemArray[0]);
                    Objetivo.Objetivos.Rows.Add(Fila);
                }
                Objetivo.BObjetivos.Rows.RemoveAt(DGObjObj.SelectedIndex);
            }
            if (ctrl.Name.Equals("DGSubobj"))
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
            ctrl = (Control)sender;
            if (ctrl.Name.Equals("TxtNom") && !Activo) Activo = true;
            if (e.Key == Key.Enter)
            {
                if (ctrl.Name.Equals("TxtNom") && !string.IsNullOrEmpty(TxtNom.Text))
                {
                    int idExiste = Objetivo.ComprobarExistencia(TxtNom.Text);
                    if (idExiste != -1) CargarObjetivo(idExiste);
                    TxtVer.Focus();
                }
                if (ctrl.Name.Equals("TxtVer") && !string.IsNullOrEmpty(TxtVer.Text))
                {
                    if (ClsFunciones.TryConvertToDouble(TxtVer.Text)) TxtFec.Focus();
                    else MessageBox.Show(StrMenEVer);
                }
                if (ctrl.Name.Equals("TxtFec") && !string.IsNullOrEmpty(TxtFec.Text))
                {
                    if (ClsFunciones.TryConvertToDate(TxtFec.Text)) TxtDes.Focus();
                    else MessageBox.Show(StrMenEFec);
                }
                if (ctrl.Name.Equals("TxtDes") && !string.IsNullOrEmpty(TxtDes.Text)) TxtCom.Focus();
                if (ctrl.Name.Equals("TxtBus")) ButBusc.Focus();
            }
        }
        private void VaciarInterfaz()
        {
            Tab.SelectedIndex = 0;
            TxtNom.Text = string.Empty;
            TxtVer.Text = "1.0";
            TxtFec.Text = DateTime.Today.ToShortDateString();
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
        private void CargarObjetivo(int id = -1)
        {
            if (id == -1) Objetivo.Cargar(int.Parse(Convert.ToString(((DataRowView)DGBuscar.Items[DGBuscar.SelectedIndex]).Row.ItemArray[1])));
            else Objetivo.Cargar(id);
            TxtNom.Text = Objetivo.Nombre;
            TxtVer.Text = ClsFunciones.DoubleToString(Objetivo.Version);
            TxtFec.Text = Objetivo.Fecha.ToShortDateString();
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
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                DGBuscar.Columns[0].Header = DGObjObj.Columns[0].Header = Ingles.Objectives;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Ingles.WorkGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Ingles.Authors;
                DGFuentes.Columns[0].Header = TabFue.Header = Ingles.Sources;
                DGSubobj.Columns[0].Header = TabSubObj.Header = Ingles.Subobjectives;
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
                LblBus.Text = Ingles.Search_Engine;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Ingles.VLow;
                RBPB.Content = RBUB.Content = RBEB.Content = Ingles.Low;
                RBPM.Content = RBUM.Content = RBEM.Content = Ingles.Medium;
                RBPA.Content = RBUA.Content = RBEA.Content = Ingles.High;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Ingles.VHigh;
                RBVer.Content = Ingles.Verified;
                RBNVer.Content = Ingles.NVerified;
                Title = Ingles.ObjPro;
                TabDat.Header = Ingles.Data;
                StrConf = Ingles.Confirmation;
                StrMenGuar = Ingles.ObjMenGuar;
                StrMenBorr = Ingles.ObjMenBorr;
                StrMenPrev = Ingles.MenPrev;
                StrMenEGuar = Ingles.ObjMenEGuar;
                StrMenEMod = Ingles.ObjMenEMod;
                StrMenEFec = Ingles.MenEFec;
                StrMenEVer = Ingles.MenEVer;
            }
            else
            {
                DGBuscar.Columns[0].Header = DGObjObj.Columns[0].Header = Español.Objetivos;
                DGGruAut.Columns[0].Header = DGGruFuen.Columns[0].Header = Español.TrabGrup;
                DGAutores.Columns[0].Header = TabAut.Header = Español.Autores;
                DGFuentes.Columns[0].Header = TabFue.Header = Español.Fuentes;
                DGSubobj.Columns[0].Header = TabSubObj.Header = Español.Subobjetivos;
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
                LblBus.Text = Español.Buscador;
                RBPMB.Content = RBUMB.Content = RBEMB.Content = Español.MBaja;
                RBPB.Content = RBUB.Content = RBEB.Content = Español.Baja;
                RBPM.Content = RBUM.Content = RBEM.Content = Español.Media;
                RBPA.Content = RBUA.Content = RBEA.Content = Español.Alta;
                RBPMA.Content = RBUMA.Content = RBEMA.Content = Español.MAlta;
                RBVer.Content = Español.Verificado;
                RBNVer.Content = Español.NVerificado;
                Title = Español.ObjPro;
                TabDat.Header = Español.Datos;
                StrConf = Español.Confirmación;
                StrMenGuar = Español.ObjMenGuar;
                StrMenBorr = Español.ObjMenBorr;
                StrMenPrev = Español.MenPrev;
                StrMenEGuar = Español.ObjMenEGuar;
                StrMenEMod = Español.ObjMenEMod;
                StrMenEFec = Español.MenEFec;
                StrMenEVer = Español.MenEVer;
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
            if (ValorRB)
            {
                //Prioridad
                if (RBPMB.IsChecked == true) Objetivo.Prioridad = 1;
                else if (RBPB.IsChecked == true) Objetivo.Prioridad = 2;
                else if (RBPM.IsChecked == true) Objetivo.Prioridad = 3;
                else if (RBPA.IsChecked == true) Objetivo.Prioridad = 4;
                else if (RBPMA.IsChecked == true) Objetivo.Prioridad = 5;
                //Urgencia
                if (RBUMB.IsChecked == true) Objetivo.Urgencia = 1;
                else if (RBUB.IsChecked == true) Objetivo.Urgencia = 2;
                else if (RBUM.IsChecked == true) Objetivo.Urgencia = 3;
                else if (RBUA.IsChecked == true) Objetivo.Urgencia = 4;
                else if (RBUMA.IsChecked == true) Objetivo.Urgencia = 5;
                //Estabilidad
                if (RBEMB.IsChecked == true) Objetivo.Estabilidad = 1;
                else if (RBEB.IsChecked == true) Objetivo.Estabilidad = 2;
                else if (RBEM.IsChecked == true) Objetivo.Estabilidad = 3;
                else if (RBEA.IsChecked == true) Objetivo.Estabilidad = 4;
                else if (RBEMA.IsChecked == true) Objetivo.Estabilidad = 5;
                //Estado
                if (RBVer.IsChecked == true) Objetivo.Estado = true;
                else Objetivo.Estado = false;
            }
            else
            {
                //Prioridad
                if (Objetivo.Prioridad == 1) RBPMB.IsChecked = true;
                else if (Objetivo.Prioridad == 2) RBPB.IsChecked = true;
                else if (Objetivo.Prioridad == 3) RBPM.IsChecked = true;
                else if (Objetivo.Prioridad == 4) RBPA.IsChecked = true;
                else if (Objetivo.Prioridad == 5) RBPMA.IsChecked = true;
                //Urgencia
                if (Objetivo.Urgencia == 1) RBUMB.IsChecked = true;
                else if (Objetivo.Urgencia == 2) RBUB.IsChecked = true;
                else if (Objetivo.Urgencia == 3) RBUM.IsChecked = true;
                else if (Objetivo.Urgencia == 4) RBUA.IsChecked = true;
                else if (Objetivo.Urgencia == 5) RBUMA.IsChecked = true;
                //Estabilidad
                if (Objetivo.Estabilidad == 1) RBEMB.IsChecked = true;
                else if (Objetivo.Estabilidad == 2) RBEB.IsChecked = true;
                else if (Objetivo.Estabilidad == 3) RBEM.IsChecked = true;
                else if (Objetivo.Estabilidad == 4) RBEA.IsChecked = true;
                else if (Objetivo.Estabilidad == 5) RBEMA.IsChecked = true;
                //Estado
                if (Objetivo.Estado) RBVer.IsChecked = true;
                else RBNVer.IsChecked = true;
            }
        }
    }
}