using System;

namespace ReadyReq
{
    public class ClsEstim
    {
        private int IntDSR;
        private int IntRTII;
        private int IntEUE;
        private int IntCIPR;
        private int IntRCMBAF;
        private int IntIE;
        private int IntU;
        private int IntCPS;
        private int IntETC;
        private int IntHC;
        private int IntCS;
        private int IntDOTPC;
        private int IntUT;

        private int IntFWTP;
        private int IntAE;
        private int IntOOPE;
        private int IntLAC;
        private int IntM;
        private int IntSR;
        private int IntPTS;
        private int IntDPL;

        private int IntUUCPSim;
        private int IntUUCPMed;
        private int IntUUCPMax;

        private int IntAWSim;
        private int IntAWMed;
        private int IntAWMax;

        private double DouTCF;
        private double DouEF;
        private int IntUUCP;
        private int IntAW;

        private double DouUCP;
        private double DouRatio;
        private double DouHE;

        public int DSR
        {
            get { return IntDSR; }
            set { IntDSR = value; }
        }
        public int RTII
        {
            get { return IntRTII; }
            set { IntRTII = value; }
        }
        public int EUE
        {
            get { return IntEUE; }
            set { IntEUE = value; }
        }
        public int CIPR
        {
            get { return IntCIPR; }
            set { IntCIPR = value; }
        }
        public int RCMBAF
        {
            get { return IntRCMBAF; }
            set { IntRCMBAF = value; }
        }
        public int IE
        {
            get { return IntIE; }
            set { IntIE = value; }
        }
        public int U
        {
            get { return IntU; }
            set { IntU = value; }
        }
        public int CPS
        {
            get { return IntCPS; }
            set { IntCPS = value; }
        }
        public int ETC
        {
            get { return IntETC; }
            set { IntETC = value; }
        }
        public int HC
        {
            get { return IntHC; }
            set { IntHC = value; }
        }
        public int CS
        {
            get { return IntCS; }
            set { IntCS = value; }
        }
        public int DOTPC
        {
            get { return IntDOTPC; }
            set { IntDOTPC = value; }
        }
        public int UT
        {
            get { return IntUT; }
            set { IntUT = value; }
        }
        public int FWTP
        {
            get { return IntFWTP; }
            set { IntFWTP = value; }
        }
        public int AE
        {
            get { return IntAE; }
            set { IntAE = value; }
        }
        public int OOPE
        {
            get { return IntOOPE; }
            set { IntOOPE = value; }
        }
        public int LAC
        {
            get { return IntLAC; }
            set { IntLAC = value; }
        }
        public int M
        {
            get { return IntM; }
            set { IntM = value; }
        }
        public int SR
        {
            get { return IntSR; }
            set { IntSR = value; }
        }
        public int PTS
        {
            get { return IntPTS; }
            set { IntPTS = value; }
        }
        public int DPL
        {
            get { return IntDPL; }
            set { IntDPL = value; }
        }
        public int UUCPSim
        {
            get { return IntUUCPSim; }
            set { IntUUCPSim = value; }
        }
        public int UUCPMed
        {
            get { return IntUUCPMed; }
            set { IntUUCPMed = value; }
        }
        public int UUCPMax
        {
            get { return IntUUCPMax; }
            set { IntUUCPMax = value; }
        }
        public int AWSim
        {
            get { return IntAWSim; }
            set { IntAWSim = value; }
        }
        public int AWMed
        {
            get { return IntAWMed; }
            set { IntAWMed = value; }
        }
        public int AWMax
        {
            get { return IntAWMax; }
            set { IntAWMax = value; }
        }
        public double TCF
        {
            get { return DouTCF; }
            set { DouTCF = value; }
        }
        public double EF
        {
            get { return DouEF; }
            set { DouEF = value; }
        }
        public int UUCP
        {
            get { return IntUUCP; }
            set { IntUUCP = value; }
        }
        public int AW
        {
            get { return IntAW; }
            set { IntAW = value; }
        }
        public double UCP
        {
            get { return DouUCP; }
            set { DouUCP = value; }
        }
        public double Ratio
        {
            get { return DouRatio; }
            set { DouRatio = value; }
        }
        public double HE
        {
            get { return DouHE; }
            set { DouHE = value; }
        }
        public void CalcValores()
        {
            //Calcular TCF
            DouTCF = IntDSR * 2;
            DouTCF += IntRTII;
            DouTCF += IntEUE;
            DouTCF += IntCIPR;
            DouTCF += IntRCMBAF;
            DouTCF += (IntIE * 0.5);
            DouTCF += (IntU * 0.5);
            DouTCF += (IntCPS * 2);
            DouTCF += IntETC;
            DouTCF += IntHC;
            DouTCF += IntCS;
            DouTCF += IntDOTPC;
            DouTCF += IntUT;
            DouTCF /= 100;
            DouTCF += 0.6;

            //Calcular EF
            DouEF = IntFWTP * 1.5;
            DouEF += (IntAE * 0.5);
            DouEF += IntOOPE;
            DouEF += (IntLAC * 0.5);
            DouEF += IntM;
            DouEF += (IntSR * 2);
            DouEF += (IntPTS * -1);
            DouEF += (IntDPL * -1);
            DouEF *= 0.03;
            DouEF -= 1.4;
            DouEF *= -1;

            //Calcular UUCP
            IntUUCP = (IntUUCPSim * 5) + (IntUUCPMed * 10) + (IntUUCPMax * 15);

            //Calcular AW
            IntAW = IntAWSim + (IntAWMed * 2) + (IntAWMax * 3);

            //Calcular UCP
            UCP = (UUCP + AW) * TCF * EF;

            //Calcular HE
            HE = UCP * Ratio;
        }
        public void CargarValores()
        {
            //TCF
            DSR = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'DSR'"));
            RTII = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'RTII'"));
            EUE = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'EUE'"));
            CIPR = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'CIPR'"));

            RCMBAF = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'RCMBAF'"));
            IE = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'IE'"));
            U = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'U'"));
            CPS = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'CPS'"));

            ETC = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'ETC'"));
            HC = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'HC'"));
            CS = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'CS'"));
            DOTPC = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'DOTPC'"));
            UT = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'UT'"));

            //EF
            FWTP = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'FWTP'"));
            AE = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'AE'"));
            OOPE = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'OOPE'"));
            LAC = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'LAC'"));

            M = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'M'"));
            SR = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'SR'"));
            PTS = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'PTS'"));
            DPL = Convert.ToInt32(ClsBaseDatos.BDDouble("Select ValEst From Estim where NomEst = 'DPL'"));

            //UUCP
            IntUUCPSim = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 1")); //Simple
            IntUUCPMed = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 2")); //Media
            IntUUCPMax = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 3")); //Alta

            //AW
            IntAWSim = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 1")); //Simple
            IntAWMed = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 2")); //Media
            IntAWMax = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 3")); //Alta
        }
        public bool GuardarDSR()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + DSR + " where NomEst = 'DSR';");
        }
        public bool GuardarRTII()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + RTII + " where NomEst = 'RTII';");
        }
        public bool GuardarEUE()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + EUE + " where NomEst = 'EUE';");
        }
        public bool GuardarCIPR()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + CIPR + " where NomEst = 'CIPR';");
        }
        public bool GuardarRCMBAF()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + RCMBAF + " where NomEst = 'RCMBAF';");
        }
        public bool GuardarIE()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + IE + " where NomEst = 'IE';");
        }
        public bool GuardarU()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + U + " where NomEst = 'U';");
        }
        public bool GuardarCPS()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + CPS + " where NomEst = 'CPS';");
        }
        public bool GuardarETC()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + ETC + " where NomEst = 'ETC';");
        }
        public bool GuardarHC()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + HC + " where NomEst = 'HC';");
        }
        public bool GuardarCS()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + CS + " where NomEst = 'CS';");
        }
        public bool GuardarDOTPC()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + DOTPC + " where NomEst = 'DOTPC';");
        }
        public bool GuardarUT()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + UT + " where NomEst = 'UT';");
        }
        public bool GuardarFWTP()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + FWTP + " where NomEst = 'FWTP';");
        }
        public bool GuardarAE()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + AE + " where NomEst = 'AE';");
        }
        public bool GuardarOOPE()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + OOPE + " where NomEst = 'OOPE';");
        }
        public bool GuardarLAC()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + LAC + " where NomEst = 'LAC';");
        }
        public bool GuardarM()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + M + " where NomEst = 'M';");
        }
        public bool GuardarSR()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + SR + " where NomEst = 'SR';");
        }
        public bool GuardarPTS()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + PTS + " where NomEst = 'PTS';");
        }
        public bool GuardarDPL()
        {
            return ClsBaseDatos.BDBool("Update Estim Set ValEst = " + DPL + " where NomEst = 'DPL';");
        }
    }
}
