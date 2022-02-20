using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.IdentityModel.Protocols;

namespace DatatablesNetCoreApi
{

   
    

    public class DataUtility
	{
        SqlConnection con= new SqlConnection();

        public string strConnectionString;
		public DataUtility(string connectionstring)
		{if (connectionstring == "")
                strConnectionString ="";
            else strConnectionString = connectionstring;
            con = new SqlConnection(strConnectionString);
            
		}
		public void OpenConnection()
		{
            
			if (con.State != ConnectionState.Open)
			{
                con =new SqlConnection(strConnectionString);
				con.Open();
			}        
		}
        public DataSet getDatasetText(string ProcName, SqlParameter[] prams)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandText = ProcName;
                cmd.CommandType = CommandType.Text;
                OpenConnection();
                cmd.Connection = con;
                cmd.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                System.GC.Collect();
                CloseConnection();
            }

        }
        public void AddParametersToCommand(ref SqlCommand cmd, SqlParameter[] Params)
        {
            foreach (SqlParameter para in Params)
            {
                cmd.Parameters.Add(para);
            }
        }
		public void CloseConnection()
		{
			if (con.State == ConnectionState.Open)
			{
				con.Close();
				con.Dispose();
			}
		}
		public DataSet getDataset(string ProcName)
		{
			DataSet ds = new DataSet();
			try
			{
                SqlDataAdapter da = new SqlDataAdapter(ProcName, strConnectionString);
				da.Fill(ds);
				return ds;
			}
			catch (Exception ex)
			{
				return ds;
			}
			finally
			{
				System.GC.Collect();
			}

        }
		public DataSet getDataset(string ProcName,SqlParameter []prams)
		{
			DataSet ds = new DataSet();
			try
			{
				SqlCommand cmd = new SqlCommand(); 
				//cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
				cmd.CommandText = ProcName;
				cmd.CommandType = CommandType.StoredProcedure;
                OpenConnection();
				cmd.Connection = con;
                cmd.CommandTimeout = 240;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
				return ds;
			}
			catch(Exception ex)
			{
				return ds;
			}
			finally
			{
				System.GC.Collect();
                CloseConnection();
			}

		}

        public int executequery(string procname)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                // AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet getDataset(string ProcName, SqlParameter prams)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(prams);
                cmd.CommandText = ProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                OpenConnection();
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                System.GC.Collect();
            }

        }
		public DataTable getDataTable(string ProcName)
		{
			DataTable dt = new DataTable();
			try
			{
				SqlCommand cmd = new SqlCommand();          
				cmd.CommandText = ProcName;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = con;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				return dt;
			}
			catch
			{
				return dt;
			}
			finally
			{
				System.GC.Collect();
			}

		}
        public DataTable getDataTable(string ProcName, SqlParameter prams)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(prams);
                cmd.CommandText = ProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
            finally
            {
                System.GC.Collect();
            }

        }
		public DataTable getDataTable(string ProcName,SqlParameter []prams)
		{
			DataTable dt = new DataTable();
            try
			{
				SqlCommand cmd = new SqlCommand();
				//cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
				cmd.CommandText = ProcName;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = con;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				return dt;
			}
			catch
			{
				return dt;
			}
			finally
			{
				System.GC.Collect();
			}

		}
		public string getDataText(string procname)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();
						
				cmd.CommandType = CommandType.StoredProcedure;				
				cmd.CommandText = procname;
                OpenConnection();
				cmd.Connection = con;
				
				object x = cmd.ExecuteScalar();			
				return x.ToString();
			}
			catch
            {
				return "";
			}
			finally
			{
				CloseConnection();
			}
		}
		public string getDataText(string procname,SqlParameter prams)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Parameters.Add(prams);		
				cmd.CommandType = CommandType.StoredProcedure;				
				cmd.CommandText = procname;				
				OpenConnection();
                cmd.Connection = con;
				object x = cmd.ExecuteScalar();			
				return x.ToString();
			}
			catch
			{
				return "";
			}
			finally
			{
				CloseConnection();
			}
		}
        public string getDataText(string procname, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                object x = cmd.ExecuteScalar();
                return x.ToString();
            }
            catch
            {
                return "";
            }
            finally
            {
                CloseConnection();
            }
        }
		public int getDataValue(string procname)
		{
			try
			{
				SqlCommand cmd = new SqlCommand();						
				cmd.CommandType = CommandType.StoredProcedure;				
				cmd.CommandText = procname;				
				OpenConnection();
                cmd.Connection = con;
				object x = cmd.ExecuteScalar();			
				return Convert.ToInt32(x);
			}
			catch
			{
				return 0;
			}
			finally
			{
				CloseConnection();
			}
		}


        public string UpdateOrInsertReturnID(string procname, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                return "0";
            }
            finally
            {
                CloseConnection();
            }
        }



        public int UpdateOrInsertData(string procname, SqlParameter prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(prams);
                //AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;               
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int UpdateOrInsertData(string procname, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                return cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int getDataValue(string procname,SqlParameter prams)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(prams);	
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                object x = cmd.ExecuteScalar();
                return Convert.ToInt32(x);
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int getDataValue(string procname, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                object x = cmd.ExecuteScalar();
                return Convert.ToInt32(x);
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public Decimal getDataValuef(string procname, SqlParameter[] prams)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(prams);
                AddParametersToCommand(ref cmd, prams);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                object x = cmd.ExecuteScalar();
                return Convert.ToDecimal(x);
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        
        /// <summary>
        /// This function used to insert/update transaction by passing  store procedure name and parametrized values. 
        /// </summary>
        /// <param name="sp_name">Store procedure name</param>
        /// <param name="values">Values of store procedure parameters in array form.</param>
        /// <param name="names">Names of store procedure parameters in array form.</param>
        /// <param name="types">Types of store procedure parameters in array form.</param>
        /// <returns>Number of records to be effected.</returns>
        public int ExecuteTransaction(string sp_name, ArrayList values, ArrayList names, ArrayList types)
        {
            int ret = 0;
            //SqlConnection Conn = DataAccess.Connect();

            try
            {
                OpenConnection();
                SqlCommand sqlCmd = new SqlCommand();
                for (int i = 0; i < Convert.ToInt32(values.Count); i++)
                {
                    SqlParameter IntPara = sqlCmd.Parameters.AddWithValue(names[i].ToString(), types[i]);
                    IntPara.Direction = ParameterDirection.Input;
                    IntPara.Value = values[i];
                }
                //Commented by akhlesh
               // sqlCmd.Connection = _MyConnection;
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = sp_name;

                // if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
                ret = sqlCmd.ExecuteNonQuery();

                sqlCmd.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return ret;
        }

        /// <summary>
        /// This function used to delete transaction by passing  store procedure name and parametrized values . 
        /// </summary>
        /// <param name="sp_name">Store procedure name</param>
        /// <param name="values">Values of store procedure parameters in array form.</param>
        /// <param name="names">Names of store procedure parameters in array form.</param>
        /// <param name="types">Types of store procedure parameters in array form.</param>
        /// <returns>Number of records to be effected.</returns>
        public int DeleteTransaction(string NameOfsp, ArrayList values, ArrayList names, ArrayList types)
        {
            OpenConnection();
            SqlCommand cmdDObject = new SqlCommand();
            int ret;
            //commented by akhlesh
            //cmdDObject.Connection = _MyConnection;
            cmdDObject.Connection = con;

            cmdDObject.CommandType = CommandType.StoredProcedure;
            cmdDObject.CommandText = NameOfsp;
            try
            {
                //if (Conn.State == ConnectionState.Closed) { Conn.Open(); }
                for (int i = 0; i < Convert.ToInt32(values.Count); i++)
                {
                    SqlParameter IntPara = cmdDObject.Parameters.AddWithValue(names[i].ToString(), types[i]);
                    IntPara.Direction = ParameterDirection.Input;
                    IntPara.Value = values[i];
                }
                ret = cmdDObject.ExecuteNonQuery();
                cmdDObject.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally { CloseConnection(); }
            return ret;
        }

        /// <summary>
        /// Function to GetDataSet from Stored Procedure.
        /// </summary>
        /// <param name="CommandText"></param>
        /// <param name="values"></param>
        /// <param name="names"></param>
        /// <param name="types"></param>
        /// <returns>DataRow</returns>
        public DataSet getDataset(string CommandText, ArrayList values, ArrayList names, ArrayList types)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdPSelect = new SqlCommand();
                int i = 0;
                for (i = 0; i < Convert.ToInt32(values.Count); i++)
                {
                    SqlParameter IntPara = cmdPSelect.Parameters.AddWithValue(names[i].ToString(), types[i]);
                    IntPara.Direction = ParameterDirection.Input;
                    IntPara.Value = values[i];
                }

                cmdPSelect.Connection = con;
                cmdPSelect.CommandType = CommandType.StoredProcedure;
                cmdPSelect.CommandText = CommandText;

                SqlDataAdapter DASelect = new SqlDataAdapter(cmdPSelect);
                DataSet DSSelect = new DataSet();
                DASelect.Fill(DSSelect);
                return DSSelect;
            }
            catch
            {
                throw;
            }
            finally { CloseConnection(); }

        }
      
        public int NonExecutedTransaction(string procname)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = procname;
                OpenConnection();
                cmd.Connection = con;
                object x = cmd.ExecuteNonQuery();
                return Convert.ToInt32(x);
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
    }

      	
}

