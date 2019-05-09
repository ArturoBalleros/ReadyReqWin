using System;

namespace ReadyReq.Model
{
    public sealed class ClsEstim
    {
        //Propiedades
        public int DSR { get; set; }
        public int RTII { get; set; }
        public int EUE { get; set; }
        public int CIPR { get; set; }
        public int RCMBAF { get; set; }
        public int IE { get; set; }
        public int U { get; set; }
        public int CPS { get; set; }
        public int ETC { get; set; }
        public int HC { get; set; }
        public int CS { get; set; }
        public int DOTPC { get; set; }
        public int UT { get; set; }
        public int FWTP { get; set; }
        public int AE { get; set; }
        public int OOPE { get; set; }
        public int LAC { get; set; }
        public int M { get; set; }
        public int SR { get; set; }
        public int PTS { get; set; }
        public int DPL { get; set; }
        public int UUCPSim { get; set; }
        public int UUCPMed { get; set; }
        public int UUCPMax { get; set; }
        public int AWSim { get; set; }
        public int AWMed { get; set; }
        public int AWMax { get; set; }
        public double TCF { get; set; }
        public double EF { get; set; }
        public int UUCP { get; set; }
        public int AW { get; set; }
        public double UCP { get; set; }
        public double Ratio { get; set; }
        public double HE { get; set; }

        //Métodos
        public void CalcValores()
        {
            //Calcular TCF
            TCF = DSR * 2;
            TCF += RTII;
            TCF += EUE;
            TCF += CIPR;
            TCF += RCMBAF;
            TCF += (IE * 0.5);
            TCF += (U * 0.5);
            TCF += (CPS * 2);
            TCF += ETC;
            TCF += HC;
            TCF += CS;
            TCF += DOTPC;
            TCF += UT;
            TCF /= 100;
            TCF += 0.6;

            //Calcular EF
            EF = FWTP * 1.5;
            EF += (AE * 0.5);
            EF += OOPE;
            EF += (LAC * 0.5);
            EF += M;
            EF += (SR * 2);
            EF += (PTS * -1);
            EF += (DPL * -1);
            EF *= 0.03;
            EF -= 1.4;
            EF *= -1;

            //Calcular UUCP
            UUCP = (UUCPSim * 5) + (UUCPMed * 10) + (UUCPMax * 15);

            //Calcular AW
            AW = AWSim + (AWMed * 2) + (AWMax * 3);

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
            UUCPSim = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 1")); //Simple
            UUCPMed = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 2")); //Media
            UUCPMax = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From ReqFun where Complejidad = 3")); //Alta

            //AW
            AWSim = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 1")); //Simple
            AWMed = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 2")); //Media
            AWMax = Convert.ToInt32(ClsBaseDatos.BDDouble("Select Count(*) From Actores where Complejidad = 3")); //Alta
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
