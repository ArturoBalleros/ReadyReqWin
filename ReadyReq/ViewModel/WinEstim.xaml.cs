using ReadyReq.Model;
using ReadyReq.Util;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReadyReq.ViewModel
{
    /// <summary>
    /// Lógica de interacción para WinEstim.xaml
    /// </summary>
    public partial class WinEstim : Window
    {
        Control ctrl;
        TextBlock ctrlTxt;
        ClsEstim Estim = new ClsEstim();

        string strTecFac;
        string strEnvFac;
        string strUUCP;
        string strAW;

        string strSim;
        string strAve;
        string strCom;

        string strMenGuar;

        string strMenDSR;
        string strMenRTII;
        string strMenEUE;
        string strMenCIPR;

        string strMenRCMBAF;
        string strMenIE;
        string strMenU;
        string strMenCPS;

        string strMenETC;
        string strMenHC;
        string strMenCS;
        string strMenDOTPC;
        string strMenUT;

        string strMenFWTP;
        string strMenAE;
        string strMenOOPE;
        string strMenLAC;

        string strMenM;
        string strMenSR;
        string strMenPTS;
        string strMenDPL;

        public WinEstim()
        {
            InitializeComponent();
        }
        private void WLoaded(object sender, RoutedEventArgs e)
        {
            Idioma();
            CargarValores();
        }
        private void LblMouseDown(object sender, MouseButtonEventArgs e)
        {
            ctrlTxt = (TextBlock)sender;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //TCF
                if (ctrlTxt.Name.Equals("LblDSR")) LblHelp.Text = strMenDSR;
                if (ctrlTxt.Name.Equals("LblRTII")) LblHelp.Text = strMenRTII;
                if (ctrlTxt.Name.Equals("LblEUE")) LblHelp.Text = strMenEUE;
                if (ctrlTxt.Name.Equals("LblCIPR")) LblHelp.Text = strMenCIPR;
                if (ctrlTxt.Name.Equals("LblRCMBAF")) LblHelp.Text = strMenRCMBAF;
                if (ctrlTxt.Name.Equals("LblIE")) LblHelp.Text = strMenIE;
                if (ctrlTxt.Name.Equals("LblU")) LblHelp.Text = strMenU;
                if (ctrlTxt.Name.Equals("LblCPS")) LblHelp.Text = strMenCPS;
                if (ctrlTxt.Name.Equals("LblETC")) LblHelp.Text = strMenETC;
                if (ctrlTxt.Name.Equals("LblHC")) LblHelp.Text = strMenHC;
                if (ctrlTxt.Name.Equals("LblCS")) LblHelp.Text = strMenCS;
                if (ctrlTxt.Name.Equals("LblDOTPC")) LblHelp.Text = strMenDOTPC;
                if (ctrlTxt.Name.Equals("LblUT")) LblHelp.Text = strMenUT;

                //EF
                if (ctrlTxt.Name.Equals("LblFWTP")) LblHelp.Text = strMenFWTP;
                if (ctrlTxt.Name.Equals("LblAE")) LblHelp.Text = strMenAE;
                if (ctrlTxt.Name.Equals("LblOOPE")) LblHelp.Text = strMenOOPE;
                if (ctrlTxt.Name.Equals("LblLAC")) LblHelp.Text = strMenLAC;
                if (ctrlTxt.Name.Equals("LblM")) LblHelp.Text = strMenM;
                if (ctrlTxt.Name.Equals("LblSR")) LblHelp.Text = strMenSR;
                if (ctrlTxt.Name.Equals("LblPTS")) LblHelp.Text = strMenPTS;
                if (ctrlTxt.Name.Equals("LblDPL")) LblHelp.Text = strMenDPL;

                GridHelp.Visibility = Visibility.Visible;
            }
        }
        private void LblMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) GridHelp.Visibility = Visibility.Hidden;
        }
        public void Presionar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (!string.IsNullOrEmpty(TxtRatio.Text))
                {
                    Estim.Ratio = Convert.ToDouble(TxtRatio.Text.Replace(".", ","));
                    Estim.CalcValores();
                    LblVHE.Content = Math.Round(Estim.HE);
                }
        }
        private void EvalTextInput(object sender, TextCompositionEventArgs e)
        {
            int carac = Convert.ToInt32(Convert.ToChar(e.Text));
            if (((48 <= carac && carac <= 57) ? true : false) || ((carac == 46) ? true : false))
            {
                if (TxtRatio.Text.Contains("."))
                {
                    if (!((carac == 46) ? true : false)) e.Handled = false;
                    else e.Handled = true;
                }
                else
                {
                    if ((carac == 46) ? true : false)
                        if (!string.IsNullOrEmpty(TxtRatio.Text)) e.Handled = false;
                        else e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else e.Handled = true;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ctrl = (Control)sender;

            //TCF
            if (ctrl.Name.Equals("SldDSR"))
            {
                Estim.DSR = Convert.ToInt32(SldDSR.Value);
                if (!Estim.GuardarDSR()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldRTII"))
            {
                Estim.RTII = Convert.ToInt32(SldRTII.Value);
                if (!Estim.GuardarRTII()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldEUE"))
            {
                Estim.EUE = Convert.ToInt32(SldEUE.Value);
                if (!Estim.GuardarEUE()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldCIPR"))
            {
                Estim.CIPR = Convert.ToInt32(SldCIPR.Value);
                if (!Estim.GuardarCIPR()) MessageBox.Show(strMenGuar);
            }

            if (ctrl.Name.Equals("SldRCMBAF"))
            {
                Estim.RCMBAF = Convert.ToInt32(SldRCMBAF.Value);
                if (!Estim.GuardarRCMBAF()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldIE"))
            {
                Estim.IE = Convert.ToInt32(SldIE.Value);
                if (!Estim.GuardarIE()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldU"))
            {
                Estim.U = Convert.ToInt32(SldU.Value);
                if (!Estim.GuardarU()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldCPS"))
            {
                Estim.CPS = Convert.ToInt32(SldCPS.Value);
                if (!Estim.GuardarCPS()) MessageBox.Show(strMenGuar);
            }

            if (ctrl.Name.Equals("SldETC"))
            {
                Estim.ETC = Convert.ToInt32(SldETC.Value);
                if (!Estim.GuardarETC()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldHC"))
            {
                Estim.HC = Convert.ToInt32(SldHC.Value);
                if (!Estim.GuardarHC()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldCS"))
            {
                Estim.CS = Convert.ToInt32(SldCS.Value);
                if (!Estim.GuardarCS()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldDOTPC"))
            {
                Estim.DOTPC = Convert.ToInt32(SldDOTPC.Value);
                if (!Estim.GuardarDOTPC()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldUT"))
            {
                Estim.UT = Convert.ToInt32(SldUT.Value);
                if (!Estim.GuardarUT()) MessageBox.Show(strMenGuar);
            }

            //EF
            if (ctrl.Name.Equals("SldFWTP"))
            {
                Estim.FWTP = Convert.ToInt32(SldFWTP.Value);
                if (!Estim.GuardarFWTP()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldAE"))
            {
                Estim.AE = Convert.ToInt32(SldAE.Value);
                if (!Estim.GuardarAE()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldOOPE"))
            {
                Estim.OOPE = Convert.ToInt32(SldOOPE.Value);
                if (!Estim.GuardarOOPE()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldLAC"))
            {
                Estim.LAC = Convert.ToInt32(SldLAC.Value);
                if (!Estim.GuardarLAC()) MessageBox.Show(strMenGuar);
            }

            if (ctrl.Name.Equals("SldM"))
            {
                Estim.M = Convert.ToInt32(SldM.Value);
                if (!Estim.GuardarM()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldSR"))
            {
                Estim.SR = Convert.ToInt32(SldSR.Value);
                if (!Estim.GuardarSR()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldPTS"))
            {
                Estim.PTS = Convert.ToInt32(SldPTS.Value);
                if (!Estim.GuardarPTS()) MessageBox.Show(strMenGuar);
            }
            if (ctrl.Name.Equals("SldDPL"))
            {
                Estim.DPL = Convert.ToInt32(SldDPL.Value);
                if (!Estim.GuardarDPL()) MessageBox.Show(strMenGuar);
            }

            Estim.CalcValores();

            LblVTec.Content = Math.Round(Estim.TCF, 3);
            LblVEnv.Content = Math.Round(Estim.EF, 3);
            LblVUUCP.Content = Estim.UUCP;
            LblVAW.Content = Estim.AW;

            LblVUCP.Content = Math.Round(Estim.UCP);
            LblVHE.Content = Math.Round(Estim.HE);
        }
        private void Idioma()
        {
            if (ClsConf.Idioma.Equals(DefValues.Ingles))
            {
                strTecFac = Ingles.TecFac;
                LblDSR.Text = Ingles.DSR;
                LblRTII.Text = Ingles.RTII;
                LblEUE.Text = Ingles.EUE;
                LblCIPR.Text = Ingles.CIPR;
                LblRCMBAF.Text = Ingles.RCMBAF;
                LblIE.Text = Ingles.IE;
                LblU.Text = Ingles.U;
                LblCPS.Text = Ingles.CPS;
                LblETC.Text = Ingles.ETC;
                LblHC.Text = Ingles.HC;
                LblCS.Text = Ingles.CS;
                LblDOTPC.Text = Ingles.DOTPC;
                LblUT.Text = Ingles.UT;
                strEnvFac = Ingles.EnvFac;
                LblFWTP.Text = Ingles.FWTP;
                LblAE.Text = Ingles.AE;
                LblOOPE.Text = Ingles.OOPE;
                LblLAC.Text = Ingles.LAC;
                LblM.Text = Ingles.M;
                LblSR.Text = Ingles.SR;
                LblPTS.Text = Ingles.PTS;
                LblDPL.Text = Ingles.DPL;
                strUUCP = Ingles.UUCP;
                strAW = Ingles.AW;
                strSim = Ingles.Sim;
                strAve = Ingles.Ave;
                strCom = Ingles.Com;
                LblTiFC.Content = Ingles.TiFC;
                LblUCP.Text = Ingles.UCP;
                LblRatio.Text = Ingles.Ratio;
                LblHE.Text = Ingles.HE;
                strMenGuar = Ingles.MenGuar;
                strMenDSR = Ingles.MenDSR;
                strMenRTII = Ingles.MenRTII;
                strMenEUE = Ingles.MenEUE;
                strMenCIPR = Ingles.MenCIPR;
                strMenRCMBAF = Ingles.MenRCMBAF;
                strMenIE = Ingles.MenIE;
                strMenU = Ingles.MenU;
                strMenCPS = Ingles.MenCPS;
                strMenETC = Ingles.MenETC;
                strMenHC = Ingles.MenHC;
                strMenCS = Ingles.MenCS;
                strMenDOTPC = Ingles.MenDOTPC;
                strMenUT = Ingles.MenUT;
                strMenFWTP = Ingles.MenFWTP;
                strMenAE = Ingles.MenAE;
                strMenOOPE = Ingles.MenOOPE;
                strMenLAC = Ingles.MenLAC;
                strMenM = Ingles.MenM;
                strMenSR = Ingles.MenSR;
                strMenPTS = Ingles.MenPTS;
                strMenDPL = Ingles.MenDPL;
            }
            else
            {
                strTecFac = Español.TecFac;
                LblDSR.Text = Español.DSR;
                LblRTII.Text = Español.RTII;
                LblEUE.Text = Español.EUE;
                LblCIPR.Text = Español.CIPR;
                LblRCMBAF.Text = Español.RCMBAF;
                LblIE.Text = Español.IE;
                LblU.Text = Español.U;
                LblCPS.Text = Español.CPS;
                LblETC.Text = Español.ETC;
                LblHC.Text = Español.HC;
                LblCS.Text = Español.CS;
                LblDOTPC.Text = Español.DOTPC;
                LblUT.Text = Español.UT;
                strEnvFac = Español.EnvFac;
                LblFWTP.Text = Español.FWTP;
                LblAE.Text = Español.AE;
                LblOOPE.Text = Español.OOPE;
                LblLAC.Text = Español.LAC;
                LblM.Text = Español.M;
                LblSR.Text = Español.SR;
                LblPTS.Text = Español.PTS;
                LblDPL.Text = Español.DPL;
                strUUCP = Español.UUCP;
                strAW = Español.AW;
                strSim = Español.Sim;
                strAve = Español.Ave;
                strCom = Español.Com;
                LblTiFC.Content = Español.TiFC;
                LblUCP.Text = Español.UCP;
                LblRatio.Text = Español.Ratio;
                LblHE.Text = Español.HE;
                strMenGuar = Español.MenGuar;
                strMenDSR = Español.MenDSR;
                strMenRTII = Español.MenRTII;
                strMenEUE = Español.MenEUE;
                strMenCIPR = Español.MenCIPR;
                strMenRCMBAF = Español.MenRCMBAF;
                strMenIE = Español.MenIE;
                strMenU = Español.MenU;
                strMenCPS = Español.MenCPS;
                strMenETC = Español.MenETC;
                strMenHC = Español.MenHC;
                strMenCS = Español.MenCS;
                strMenDOTPC = Español.MenDOTPC;
                strMenUT = Español.MenUT;
                strMenFWTP = Español.MenFWTP;
                strMenAE = Español.MenAE;
                strMenOOPE = Español.MenOOPE;
                strMenLAC = Español.MenLAC;
                strMenM = Español.MenM;
                strMenSR = Español.MenSR;
                strMenPTS = Español.MenPTS;
                strMenDPL = Español.MenDPL;
            }

            LblTiTec.Content = strTecFac;
            LblTiEnv.Content = strEnvFac;
            LblTiUUCP.Content = strUUCP;
            LblTiAW.Content = strAW;

            LblUUCPSim.Content = strSim;
            LblUUCPAve.Content = strAve;
            LblUUCPCom.Content = strCom;

            LblAWSim.Content = strSim;
            LblAWAve.Content = strAve;
            LblAWCom.Content = strCom;

            LblTec.Content = strTecFac;
            LblEnv.Content = strEnvFac;
            LblUUCP.Text = strUUCP;
            LblAW.Text = strAW;
        }
        private void CargarValores()
        {
            Estim.CargarValores();

            //TCF
            SldDSR.Value = Estim.DSR;
            SldRTII.Value = Estim.RTII;
            SldEUE.Value = Estim.EUE;
            SldCIPR.Value = Estim.CIPR;

            SldRCMBAF.Value = Estim.RCMBAF;
            SldIE.Value = Estim.IE;
            SldU.Value = Estim.U;
            SldCPS.Value = Estim.CPS;

            SldETC.Value = Estim.ETC;
            SldHC.Value = Estim.HC;
            SldCS.Value = Estim.CS;
            SldDOTPC.Value = Estim.DOTPC;
            SldUT.Value = Estim.UT;

            //EF
            SldFWTP.Value = Estim.FWTP;
            SldAE.Value = Estim.AE;
            SldOOPE.Value = Estim.OOPE;
            SldLAC.Value = Estim.LAC;

            SldM.Value = Estim.M;
            SldSR.Value = Estim.SR;
            SldPTS.Value = Estim.PTS;
            SldDPL.Value = Estim.DPL;

            //UUCP
            LblVUUCPSim.Content = Estim.UUCPSim;
            LblVUUCPAve.Content = Estim.UUCPMed;
            LblVUUCPCom.Content = Estim.UUCPMax;

            //AW
            LblVAWSim.Content = Estim.AWSim;
            LblVAWAve.Content = Estim.AWMed;
            LblVAWCom.Content = Estim.AWMax;

            Estim.CalcValores();

            LblVTec.Content = Math.Round(Estim.TCF, 3);
            LblVEnv.Content = Math.Round(Estim.EF, 3);
            LblVUUCP.Content = Estim.UUCP;
            LblVAW.Content = Estim.AW;

            LblVUCP.Content = Math.Round(Estim.UCP);
            LblVHE.Content = Math.Round(Estim.HE);
        }
    }
}
