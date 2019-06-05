using MySql.Data.MySqlClient;
using ReadyReq.Util;

namespace ReadyReq.Model
{
    public sealed class ClsNuePro
    {
        MySqlConnectionStringBuilder BuilderMySql = new MySqlConnectionStringBuilder();
        public string TipoBD { get; set; }
        public string Host { get; set; }
        public string Usuario { get; set; }
        public string PassMySql { get; set; }
        public string BDMySql { get; set; }
        public string PortMySql { get; set; }
        public bool CreateMySql { get; set; }
        public string RutaAcc { get; set; }
        public bool FlgPass { get; set; }
        public string PassAcc { get; set; }
        public int CrearBase()
        {
            if (TipoBD.Equals(DefValues.MySql))
            {
                ConexionMySql();
                if (!ClsBaseDatos.BDConexion(BuilderMySql.ToString(), TipoBD)) return -1;
                if (CreateMySql)
                {
                    ClsBaseDatos.BDBool("CREATE SCHEMA " + BDMySql, BuilderMySql.ToString(), TipoBD);
                    BuilderMySql.Database = BDMySql;
                }
                ClsBaseDatos.BDBool("CREATE TABLE `Actores` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` text(500) not null, `Complejidad` int not null, `DescComple` varchar(100) not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `Grupo` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Organizacion` varchar(100) not null, `Rol` varchar(100) not null, `Desarrollador` int not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `Objetivos` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `Paquetes` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqFun` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Paquete` int not null, `Precond` varchar(100) not null, `Postcond` varchar(100) not null, `Complejidad` int not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`), foreign key(`Paquete`) references `Paquetes`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqInfo` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `TiemMed` int not null, `TiemMax` int not null, `OcuMed` int not null, `OcuMax` int not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqNFunc` (`Id` int not null auto_increment, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text(500) not null, primary key(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `Estim` (`NomEst` VARCHAR(10) NOT NULL, `ValEst` INT NOT NULL,  PRIMARY KEY(`NomEst`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("CREATE TABLE `ActAuto` (`Id` int not null auto_increment, `IdAutor` int not null, `IdAct` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdAct`) references `Actores`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ActFuen` (`Id` int not null auto_increment, `IdFuen` int not null, `IdAct` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdAct`) references `Actores`(`Id`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("CREATE TABLE `ObjAuto` (`Id` int not null auto_increment, `IdAutor` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ObjFuen` (`Id` int not null auto_increment, `IdFuen` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ObjSubobj` (`Id` int not null auto_increment, `IdSubobj` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("CREATE TABLE `ReqAct` (`Id` int not null auto_increment, `IdAct` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAct`) references `Actores`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqAuto` (`Id` int not null auto_increment, `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqFuen` (`Id` int not null auto_increment, `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqObj` (`Id` int not null auto_increment, `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqReqR` (`Id` int not null auto_increment, `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`),  foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqSecExc` (`Id` int not null auto_increment, `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqSecNor` (`Id` int not null auto_increment, `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("CREATE TABLE `ReqIAuto` (`Id` int not null auto_increment, `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqIDatEsp` (`Id` int not null auto_increment, `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqIFuen` (`Id` int not null auto_increment, `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqIObj` (`Id` int not null auto_increment, `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqIReqR` (`Id` int not null auto_increment, `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("CREATE TABLE `ReqNAuto` (`Id` int not null auto_increment, `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqNFuen` (`Id` int not null auto_increment, `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqNObj` (`Id` int not null auto_increment, `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("CREATE TABLE `ReqNReqR` (`Id` int not null auto_increment, `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("INSERT INTO Paquetes(Nombre,Categoria,Comentario) VALUES ('No Asignado', 10, '');", BuilderMySql.ToString(), TipoBD);

                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DSR', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('RTII', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('EUE', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CIPR', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('RCMBAF', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('IE', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('U', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CPS', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('ETC', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('HC', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CS', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DOTPC', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('UT', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('FWTP', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('AE', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('OOPE', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('LAC', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('M', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('SR', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('PTS', 0);", BuilderMySql.ToString(), TipoBD);
                ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DPL', 0);", BuilderMySql.ToString(), TipoBD);

                return 0;
            }
            else
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                string builder = "Provider=Microsoft.ACE.OLEDB.12.0; " + "Data Source=" + RutaAcc + "; ";
                if (FlgPass) builder += PassAcc;
                try
                {
                    cat.Create(builder);
                    if (!FlgPass) builder += "Persist Security Info = False;";

                    ClsBaseDatos.BDBool("CREATE TABLE `Actores` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` text not null, `Complejidad` int not null, `DescComple` varchar(100) not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `Grupo` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Organizacion` varchar(100) not null, `Rol` varchar(100) not null, `Desarrollador` int not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `Objetivos` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `Paquetes` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqFun` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Paquete` int not null, `Precond` varchar(100) not null, `Postcond` varchar(100) not null, `Complejidad` int not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`), foreign key(`Paquete`) references `paquetes`(`id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqInfo` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `TiemMed` int not null, `TiemMax` int not null, `OcuMed` int not null, `OcuMax` int not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqNFunc` (`Id` AUTOINCREMENT not null, `Nombre` varchar(100) unique not null, `Version` double not null default 1.0, `Fecha` date, `Descripcion` varchar(100) not null, `Prioridad` int not null, `Urgencia` int not null, `Estabilidad` int not null, `Estado` int not null, `Categoria` int not null, `Comentario` Text not null, primary key(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `Estim` (`NomEst` VARCHAR(10) NOT NULL, `ValEst` INT NOT NULL,  PRIMARY KEY(`NomEst`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("CREATE TABLE `ActAuto` (`Id` AUTOINCREMENT not null, `IdAutor` int not null, `IdAct` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdAct`) references `Actores`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ActFuen` (`Id` AUTOINCREMENT not null , `IdFuen` int not null, `IdAct` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdAct`) references `Actores`(`Id`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("CREATE TABLE `ObjAuto` (`Id` AUTOINCREMENT not null , `IdAutor` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ObjFuen` (`Id` AUTOINCREMENT not null , `IdFuen` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ObjSubobj` (`Id` AUTOINCREMENT not null , `IdSubobj` int not null, `IdObj` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("CREATE TABLE `ReqAct` (`Id` AUTOINCREMENT not null , `IdAct` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAct`) references `Actores`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqAuto` (`Id` AUTOINCREMENT not null , `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqFuen` (`Id` AUTOINCREMENT not null , `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqObj` (`Id` AUTOINCREMENT not null , `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqReqR` (`Id` AUTOINCREMENT not null , `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqSecExc` (`Id` AUTOINCREMENT not null , `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqSecNor` (`Id` AUTOINCREMENT not null , `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqFun`(`Id`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("CREATE TABLE `ReqIAuto` (`Id` AUTOINCREMENT not null , `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqIDatEsp` (`Id` AUTOINCREMENT not null , `IdReq` int not null, `Descrip` varchar(100) not null, primary key(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqIFuen` (`Id` AUTOINCREMENT not null , `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqIObj` (`Id` AUTOINCREMENT not null , `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqIReqR` (`Id` AUTOINCREMENT not null , `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdReq`) references `ReqInfo`(`Id`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("CREATE TABLE `ReqNAuto` (`Id` AUTOINCREMENT not null , `IdAutor` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdAutor`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqNFuen` (`Id` AUTOINCREMENT not null , `IdFuen` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdFuen`) references `Grupo`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqNObj` (`Id` AUTOINCREMENT not null , `IdObj` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdObj`) references `Objetivos`(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", builder, TipoBD);
                    ClsBaseDatos.BDBool("CREATE TABLE `ReqNReqR` (`Id` AUTOINCREMENT not null , `IdReqR` int not null, `TipoReq` int not null, `IdReq` int not null, primary key(`Id`), foreign key(`IdReq`) references `ReqNFunc`(`Id`));", builder, TipoBD);

                    ClsBaseDatos.BDBool("INSERT INTO Paquetes(Nombre,Categoria,Comentario) VALUES ('No Asignado', 10, '');", builder, TipoBD);

                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DSR', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('RTII', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('EUE', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CIPR', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('RCMBAF', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('IE', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('U', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CPS', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('ETC', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('HC', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('CS', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DOTPC', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('UT', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('FWTP', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('AE', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('OOPE', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('LAC', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('M', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('SR', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('PTS', 0);", builder, TipoBD);
                    ClsBaseDatos.BDBool("INSERT INTO Estim(NomEst, ValEst) VALUES('DPL', 0);", builder, TipoBD);

                }
                catch
                {
                    return -2;
                }
            }
            return 0;
        }
        private void ConexionMySql()
        {
            BuilderMySql.Server = Host;
            BuilderMySql.UserID = Usuario;
            BuilderMySql.Password = PassMySql;
            BuilderMySql.Port = uint.Parse(PortMySql);
            if (!CreateMySql) BuilderMySql.Database = BDMySql;
        }
    }
}