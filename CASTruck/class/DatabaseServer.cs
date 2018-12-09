using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Win32;
namespace CASTruck
{
    class DatabaseServer:IDisposable
    {
        #region constantes
        public static string INFORMATION_TABLE_COLUMNS_DATA_DESCRIPTIONS_SCHEMA = "SELECT SCHEMA_NAME(o.schema_id) AS Esquema, " + 
                                                                                  "O.Name AS Tabla, P1.Value AS [Descripción Tabla], " + 
                                                                                  "C.Name AS Columna, T.Name AS Tipo,  C.max_length AS Longitud, " + 
                                                                                  "C.[Precision] AS Presicion, C.scale AS Escala,  C.Is_Nullable AS Col_Null, " + 
                                                                                  "P2.value AS [Descripcion], ISNULL(i.is_primary_key, 0) AS Col_KEY, " + 
                                                                                  "Columnproperty(Object_id(schema_name(o.schema_id) + '.' + O.Name), " + 
                                                                                   "C.Name,'ISIDENTITY') AS IsIdentity " + 
                                                                                  "FROM " + 
                                                                                  "sys.tables O " + 
                                                                                  "INNER JOIN sys.Columns C " + 
                                                                                    "ON O.object_id = C.object_id " + 
                                                                                  "INNER JOIN sys.Types T " + 
                                                                                    "ON C.system_type_id = T.system_type_id " + 
                                                                                    "AND C.system_type_id = T.user_type_id " + 
                                                                                  "LEFT JOIN sys.extended_properties P1 " + 
                                                                                    "ON C.object_id = P1.major_id " + 
                                                                                    "AND P1.minor_id = 0 " + 
                                                                                  "LEFT JOIN sys.extended_properties P2 " + 
                                                                                    "ON C.object_id = P2.major_id " + 
                                                                                    "AND C.Column_id = P2.minor_id " + 
                                                                                    "AND P2.Class = 1 " + 
                                                                                  "LEFT JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id " + 
                                                                                  "LEFT JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id " + 
                                                                                  "WHERE " + 
                                                                                  "O.Name LIKE ";
        #endregion
        #region variables
        string _messageError = string.Empty;
            string _databaseName = string.Empty;
            string _serverName = string.Empty;
            Int16 _persistSecurityInfo = 0;
            Int16 _integratedSecurity = 1;
            string _userID;
            string _password;        
            SqlConnection _cnn = new SqlConnection();
            string _sql = string.Empty;
        #endregion

        #region propiedades

            public string sql
            {
                get
                {
                    return _sql;
                }
                set
                {
                    _sql = value;
                }
            }
            public string password
            {
                get
                {
                    return _password;
                }
                set
                {
                    _password = value;
                }
            }
            public string userID
            {
                get
                {
                    return _userID;
                }
                set
                {
                    _userID = value;
                }
            }

            public Int16 integratedSecurity
            {
                get
                {
                    return _integratedSecurity;
                }
                set
                {
                    _integratedSecurity = value;
                }
            }
            public Int16 persistSecurityInfo
            {
                get
                {
                    return _persistSecurityInfo;
                }
                set
                {
                    _persistSecurityInfo = value;
                }
            }

            public string serverName
            {
                get
                {
                    return _serverName;
                }
                set
                {
                    _serverName = value;
                }
            }

            public string databaseName
            {
                get
                {
                    return _databaseName;
                }
                set
                {
                    _databaseName = value;
                }
            }


            public string messageError
            {
                get
                {
                    return _messageError;
                }
            }

        #endregion
        #region metodos
        
        public bool connectDatabase(){
            bool blnReturn = true;
            string psi;
            string ise;

            _messageError = string.Empty;

            if(_cnn != null){
                if (_cnn.State == ConnectionState.Open){
                    _cnn.Close();
                }
            }

            if (_persistSecurityInfo == 1)
                psi = "true";
            else
                psi = "false";

            if (_integratedSecurity == 1)
                ise = "true";
            else
                ise = "false";

            if(_integratedSecurity == 1){

                _cnn.ConnectionString = "Persist Security Info=" + psi +
                                        ";Integrated Security=" + ise +
                                        ";Initial Catalog=" + _databaseName +
                                        ";Data Source=" + _serverName;
            }
            else{
                _cnn.ConnectionString = "Persist Security Info=" + psi +
                                        ";User ID=" + _userID +
                                        ";Password=" + _password +
                                        ";Initial Catalog=" + _databaseName +
                                        ";Data Source=" + _serverName;
            }
            try{
                _cnn.Open();
            }
            catch(SqlException e){
                _messageError = e.Message;
                blnReturn = false;
            }
            catch(InvalidOperationException ie){
                _messageError = ie.Message;
                blnReturn = false;

            }
            return blnReturn;
        }
        private List<string> GetLocalSqlServerInstanceNames()
        {
            RegistryValueDataReader registryValueDataReader = new RegistryValueDataReader();

            string[] instances64Bit = registryValueDataReader.ReadRegistryValueData(RegistryHive.Wow64,
                                                                                    Registry.LocalMachine,
                                                                                    @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                    "InstalledInstances");

            string[] instances32Bit = registryValueDataReader.ReadRegistryValueData(RegistryHive.Wow6432,
                                                                                    Registry.LocalMachine,
                                                                                    @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                    "InstalledInstances");


            List<string> localInstanceNames = new List<string>(instances64Bit);
            foreach (var item in instances32Bit)
            {
                if (!localInstanceNames.Contains(item)) localInstanceNames.Add(item);
            }


            return localInstanceNames;
        }
        public List<string> getInstancesSQLSERVER()
        {
            List<string> sqlInstances = new List<string>();
           
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            System.Data.DataTable dataTable = instance.GetDataSources();
            foreach (DataRow row in dataTable.Rows)
            {
                //string instanceName = String.Format(@"{0}\{1}", row["ServerName"].ToString(), row["InstanceName"].ToString());
                string instanceName = row["ServerName"].ToString();
               
                if (!sqlInstances.Contains(instanceName) && !instanceName.Contains(Environment.MachineName))
                {
                    sqlInstances.Add(instanceName);
                }
            }

           
            List<string> lclInstances = GetLocalSqlServerInstanceNames();
            foreach (var lclInstance in lclInstances)
            {
                //string instanceName = String.Format(@"{0}\{1}", Environment.MachineName, lclInstance);
                string instanceName = Environment.MachineName;
                if (!sqlInstances.Contains(instanceName)) sqlInstances.Add(instanceName);
            }
            sqlInstances.Sort();

            return sqlInstances;
        }

        public bool executeNonSQL(bool isProcedure, SqlParameter[] parameters)
        {
            bool blnReturn = true;
            _messageError = string.Empty;
            SqlCommand cmd = new SqlCommand(_sql, _cnn);

            foreach(SqlParameter prm in parameters)
            {
                cmd.Parameters.Add(prm);
            }

            if (isProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                _messageError = se.Message;
                blnReturn = false;
            }
            catch (InvalidOperationException ie)
            {
                _messageError = ie.Message;
                blnReturn = false;
            }

            return blnReturn;
        }

        public bool executeNonSQL(bool isProcedure)
        {
            bool blnReturn = true;
            _messageError = string.Empty;
            SqlCommand cmd = new SqlCommand(_sql, _cnn);
            if (isProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                _messageError = se.Message;
                blnReturn = false;
            }
            catch (InvalidOperationException ie)
            {
                _messageError = ie.Message;
                blnReturn = false;
            }

            return blnReturn;
        }

        public bool isCnn()
        {
            bool blnReturn = true;

            if (_cnn != null)
            {
                if (_cnn.State != ConnectionState.Open)
                {
                    blnReturn = false;
                    _messageError = "Conexión cerrada.";
                }
            }
            else
            {
                blnReturn = false;
                _messageError = "Conexión nula.";

            }

            return blnReturn;
        }
        public void Dispose()
        {
            _cnn.Dispose();
        }

        public void closeConnection()
        {
            if (_cnn != null)
            {
                if (_cnn.State == ConnectionState.Open)
                {
                    _cnn.Close();
                }
            }
        }

        public DataTable getDataTable()
        {
            _messageError = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand(_sql, _cnn);
                SqlDataAdapter adapter;
                DataTable dt = new DataTable();

                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
               
                return dt;

            }
            catch(SqlException se)
            {
                _messageError = se.Message;
            }
            catch(InvalidOperationException ie)
            {
                _messageError = ie.Message;
            }
            return null;
        }
        public bool getValuesConfig()
        {
            try
            {
                string vAux = string.Empty;
                vAux = CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, Constants.INTEGRATED_SECURITY_KEY);
                if (!string.IsNullOrEmpty(vAux))
                    _integratedSecurity = Convert.ToInt16(vAux);  
                _databaseName = CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, Constants.NAME_KEY_DATABASE);
                _password = CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, Constants.PASSWORD_KEY);
                _serverName = CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, Constants.SERVER_NAME_KEY);
                _userID = CommonMethods.getKeyValueRegistry(Constants.PATH_KEY, Constants.USER_ID_KEY);
                if (!string.IsNullOrEmpty(_databaseName))
                    return true;
                else
                    return false;
            }
            catch(NullReferenceException ne)
            {
                Console.Write(ne.Message);
                return false;
            }

        }


        #endregion

    }
}
