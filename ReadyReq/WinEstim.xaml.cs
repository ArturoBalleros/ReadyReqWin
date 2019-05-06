using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ReadyReq
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
            ctrlTxt = ((TextBlock)sender);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //TCF
                if (ctrlTxt.Name.Equals("LblDSR"))
                    LblHelp.Text = strMenDSR;
                if (ctrlTxt.Name.Equals("LblRTII"))
                    LblHelp.Text = strMenRTII;
                if (ctrlTxt.Name.Equals("LblEUE"))
                    LblHelp.Text = strMenEUE;
                if (ctrlTxt.Name.Equals("LblCIPR"))
                    LblHelp.Text = strMenCIPR;

                if (ctrlTxt.Name.Equals("LblRCMBAF"))
                    LblHelp.Text = strMenRCMBAF;
                if (ctrlTxt.Name.Equals("LblIE"))
                    LblHelp.Text = strMenIE;
                if (ctrlTxt.Name.Equals("LblU"))
                    LblHelp.Text = strMenU;
                if (ctrlTxt.Name.Equals("LblCPS"))
                    LblHelp.Text = strMenCPS;

                if (ctrlTxt.Name.Equals("LblETC"))
                    LblHelp.Text = strMenETC;
                if (ctrlTxt.Name.Equals("LblHC"))
                    LblHelp.Text = strMenHC;
                if (ctrlTxt.Name.Equals("LblCS"))
                    LblHelp.Text = strMenCS;
                if (ctrlTxt.Name.Equals("LblDOTPC"))
                    LblHelp.Text = strMenDOTPC;
                if (ctrlTxt.Name.Equals("LblUT"))
                    LblHelp.Text = strMenUT;

                //EF
                if (ctrlTxt.Name.Equals("LblFWTP"))
                    LblHelp.Text = strMenFWTP;
                if (ctrlTxt.Name.Equals("LblAE"))
                    LblHelp.Text = strMenAE;
                if (ctrlTxt.Name.Equals("LblOOPE"))
                    LblHelp.Text = strMenOOPE;
                if (ctrlTxt.Name.Equals("LblLAC"))
                    LblHelp.Text = strMenLAC;

                if (ctrlTxt.Name.Equals("LblM"))
                    LblHelp.Text = strMenM;
                if (ctrlTxt.Name.Equals("LblSR"))
                    LblHelp.Text = strMenSR;
                if (ctrlTxt.Name.Equals("LblPTS"))
                    LblHelp.Text = strMenPTS;
                if (ctrlTxt.Name.Equals("LblDPL"))
                    LblHelp.Text = strMenDPL;

                GridHelp.Visibility = Visibility.Visible;
            }
        }
        private void LblMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
                GridHelp.Visibility = Visibility.Hidden;
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

                    if (!((carac == 46) ? true : false))
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
                {
                    if ((carac == 46) ? true : false)
                        if (!string.IsNullOrEmpty(TxtRatio.Text))
                            e.Handled = false;
                        else
                            e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
            else
                e.Handled = true;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ctrl = ((Control)sender);

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
            if (ClsConf.Idioma == "Ingles")
            {
                strTecFac = "Technical Factors";

                LblDSR.Text = "Distributed system required";
                LblRTII.Text = "Response time is important";
                LblEUE.Text = "End user efficiency";
                LblCIPR.Text = "Complex internal processing required";

                LblRCMBAF.Text = "Reusable code must be a focus";
                LblIE.Text = "Installation ease";
                LblU.Text = "Usability";
                LblCPS.Text = "Cross-Platform support";

                LblETC.Text = "Easy to change";
                LblHC.Text = "Highly concurrent";
                LblCS.Text = "Custom security";
                LblDOTPC.Text = "Dependence on third-party code";
                LblUT.Text = "User training";

                strEnvFac = "Environmental Factors";

                LblFWTP.Text = "Familiarity with the project";
                LblAE.Text = "Application experience";
                LblOOPE.Text = "OO programming experience";
                LblLAC.Text = "Lead analyst capability";

                LblM.Text = "Motivation";
                LblSR.Text = "Stable requirements";
                LblPTS.Text = "Part time staff";
                LblDPL.Text = "Difficult programming language";

                strUUCP = "Unadjusted Use Case Points";
                strAW = "Actor Weighting";

                strSim = "Simple";
                strAve = "Average";
                strCom = "Complex";

                LblTiFC.Content = "Final Calculations";

                LblUCP.Text = "Use case points";
                LblRatio.Text = "Hours of effort per use case point";
                LblHE.Text = "Hours of Effort";

                strMenGuar = "Could not save";

                strMenDSR = "Higher numbers represent a more complex architecture.";
                strMenRTII = "Higher numbers represent an increasing importance of response time.";
                strMenEUE = "Higher numbers represent projects that depend more on the application, to improve user efficiency.";
                strMenCIPR = "Complex algorithms have higher numbers.";

                strMenRCMBAF = "The higher numbers represent the level of reuse, the lower the number.";
                strMenIE = "The higher numbers represent the level of competence of the users, the lower the number.";
                strMenU = "The greater the importance of usability, the greater the number.";
                strMenCPS = "The more platforms that have to be supported, the greater the value.";

                strMenETC = "The more personalization is required in the future, the greater the value.";
                strMenHC = "The more attention you have to dedicate to resolving conflicts in the data or the application, the greater the value.";
                strMenCS = "The more custom security work you have to perform, the greater the value.";
                strMenDOTPC = "The more third-party code, the smaller the number.";
                strMenUT = "The later the users cross the suction threshold, the higher the value.";

                strMenFWTP = "Higher levels of experience get a higher number.";
                strMenAE = "Higher numbers represent more experience.";
                strMenOOPE = "Higher numbers represent more POO experience.";
                strMenLAC = "Higher numbers represent greater skill and knowledge.";

                strMenM = "Higher numbers represent more motivation.";
                strMenSR = "Higher numbers represent more changes.";
                strMenPTS = "The multiplier for this number is negative, the higher numbers reflect members of the team that are not dedicated full-time to the project or are external to the entity.";
                strMenDPL = "This multiplier is also negative, the harder languages ​​represent higher numbers.";
            }
            else
            {
                strTecFac = "Factores Técnicos";

                LblDSR.Text = "Sistema distribuido requerido";
                LblRTII.Text = "El tiempo de respuesta es importante";
                LblEUE.Text = "Eficiencia del usuario final";
                LblCIPR.Text = "Procesamiento interno complejo requerido";

                LblRCMBAF.Text = "El código reutilizable debe ser un enfoque";
                LblIE.Text = "Facilidad de instalación";
                LblU.Text = "Usabilidad";
                LblCPS.Text = "Soporte multiplataforma";

                LblETC.Text = "Fácil de cambiar";
                LblHC.Text = "Altamente concurrente";
                LblCS.Text = "Seguridad personalizada";
                LblDOTPC.Text = "Dependencia del código de terceros";
                LblUT.Text = "Entrenamiento de usuario";

                strEnvFac = "Factores de Entorno";

                LblFWTP.Text = "Familiaridad con el proyecto";
                LblAE.Text = "Experiencia de aplicación";
                LblOOPE.Text = "Experiencia en programación OO";
                LblLAC.Text = "Capacidad de analista líder";

                LblM.Text = "Motivación";
                LblSR.Text = "Requisitos estables";
                LblPTS.Text = "Personal a tiempo parcial";
                LblDPL.Text = "Lenguaje de programación difícil";

                strUUCP = "Puntos de Casos de Uso No Ajustados";
                strAW = "Ponderación de Actores";

                strSim = "Simple";
                strAve = "Medio";
                strCom = "Complejo";

                LblTiFC.Content = "Cálculos Finales";

                LblUCP.Text = "Puntos de casos de uso";
                LblRatio.Text = "Horas de esfuerzo por UCP";
                LblHE.Text = "Horas de Esfuerzo";

                strMenGuar = "No se pudo guardar";

                strMenDSR = "Los números más altos representan una arquitectura más compleja.";
                strMenRTII = "Los números más altos representan una importancia cada vez mayor del tiempo de respuesta.";
                strMenEUE = "Los números más altos representan proyectos que dependen más de la aplicación, para mejorar la eficiencia del usuario.";
                strMenCIPR = "Los algoritmos complejos tienen números más altos.";

                strMenRCMBAF = "Los números más altos representan el nivel de reutilización, menor será el número.";
                strMenIE = "Los números más altos representan el nivel de competencia de los usuarios, menor será el número.";
                strMenU = "Cuanto mayor es la importancia de la usabilidad, mayor es el número.";
                strMenCPS = "Cuantas más plataformas haya que admitir, mayor será el valor.";

                strMenETC = "Cuanto más personalización se requiera en el futuro, mayor será el valor.";
                strMenHC = "Cuanta más atención tenga que dedicar a resolver conflictos en los datos o la aplicación, mayor será el valor.";
                strMenCS = "Cuanto más trabajo de seguridad personalizado tenga que realizar, mayor será el valor.";
                strMenDOTPC = "Cuanto más código de terceros, menor será el número.";
                strMenUT = "Cuanto más tarde a los usuarios cruzar el umbral de succión, mayor será el valor.";

                strMenFWTP = "Los niveles más altos de experiencia obtienen un número más alto.";
                strMenAE = "Los números más altos representan más experiencia.";
                strMenOOPE = "Los números más altos representan más experiencia POO.";
                strMenLAC = "Los números más altos representan una mayor habilidad y conocimiento.";

                strMenM = "Los números más altos representan más motivación.";
                strMenSR = "Los números más altos representan más cambios.";
                strMenPTS = "El multiplicador para este número es negativo. Los números más altos reflejan miembros del equipo que no se dedican a tiempo completo al proyecto o son externos a la entidad.";
                strMenDPL = "Este multiplicador también es negativo. Los idiomas más duros representan números más altos.";
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
