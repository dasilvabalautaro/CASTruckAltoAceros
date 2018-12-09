using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace CASTruck
{
    class Constants
{
        #region constantes
        public const string TITLE_APP = "Control de Transporte Pesado";
        public const string PATH_KEY = "SOFTWARE\\CAS\\CASTRUCK";
        public const string NAME_KEY_DATABASE = "Database";
        public const string INTEGRATED_SECURITY_KEY = "Integrated_Security";
        public const string PASSWORD_KEY = "Password";
        public const string SERVER_NAME_KEY = "ServerName";
        public const string USER_ID_KEY = "UserID";
        public const string FOLDER_SQL = "SQL";
        public const string FILE_IMAGES = "Vehicles";
        public const string INSERT_PARAMETERS_FILE = "INSERT_REGISTRIES_PARAMS.sql";
        public const string CREATE_DATABASE_FILE = "CREATEDB.Log";
        public const string CREATE_TABLES_PROCEDURE = "CREATETABLESDB";
        public const string INSERT_PARAMETERS_PROCEDURE = "INSERTPARAMSDB";

        public const string TABLE_LOGSHARDWARE = "LOGSHARDWARE";
        public const string TABLE_LOGSDATABASE = "LOGSDATABASE";
        public const string TABLE_CONFIGURATION = "PVEHICLECONFIGURATION";
        public const string TABLE_WEIGHING = "WEIGHING";
        public const string TABLE_WEIGHINGCHILD = "WEIGHTCHILD";
        public const string TABLE_TYPE_USERS = "PUSERSTYPE";
        public const string TABLE_USERS = "USERS";
        public const string FIELD_USERID = "USERID";
        #endregion

        #region variables
        public static DatabaseServer dbs = new DatabaseServer();
        public static DataTable dtPermittedWeightsAndDimensions = new DataTable();
        public static CriptoUtil cu = new CriptoUtil();
        public static string USERID_NOW = "1";
        public static string NAMES_NOW = string.Empty;
        public static string SURNAMES_NOW = string.Empty;

        #endregion

    }
}
