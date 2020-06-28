using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
using System.Runtime.CompilerServices;
//using Ini;


  public  class  CSBaseDALMS
    {
        private SqlConnection cn = new SqlConnection();
        public  SqlCommand cmd = new SqlCommand();
        private  SqlDataAdapter da = new SqlDataAdapter();
        //private  OdbcDataReader rd;
        //private  long rtrnVal;

        private string constr = Report_PagIBIG.Properties.Settings.Default.ConnectionString; 
        public virtual DataTable GetDatatable(string strCmd, CommandType  cmdType)
        {
            DataTable dt = new DataTable();
            cn = new SqlConnection(constr);
            try
            {
               cmd.CommandText = strCmd;
                cmd.CommandType = cmdType ;
                cmd.Connection = cn;
                da.SelectCommand = cmd ;
               cmd.CommandTimeout = 0;
                cn.Open();
                da.Fill(dt);
            return dt;
            }
            catch (Exception e)
            { 
                    //MessageBox.Show(e.Message );
                    return null;
            }
            finally
            {
            da.Dispose();
                if(cn.State == System.Data.ConnectionState.Open)
                {
                cn.Close();
                }
            }
        }


    public virtual void Execute(string strCmd, CommandType cmdType)
    {
        cn = new SqlConnection(constr);   
        try
        {
            cmd.CommandText = strCmd ;
            cmd.Connection = cn;
            cmd.CommandType = cmdType;
            cn.Open();
            cmd.ExecuteScalar();
         }
        catch(Exception e)
        {
        // MessageBox.Show(e.Message );    
        }
            finally
            {
            da.Dispose();
                if(cn.State == System.Data.ConnectionState.Open)
                {
                cn.Close();
                }
            }
    }

        public virtual string GetValue(string strCmd, CommandType cmdType)
        {
            string rtrnVal = "";
            cn = new SqlConnection(constr);   
        try
        {   
            cmd.CommandText = strCmd ;
            cmd.Connection = cn;
            cmd.CommandType = cmdType;
            cn.Open();
            rtrnVal = cmd.ExecuteScalar().ToString() ;
            if (rtrnVal == null)
            {
            rtrnVal = "";
            }
            return rtrnVal;
         }
        catch(Exception e)
        {
         //MessageBox.Show(e.Message );
         return null;
        }
            finally
            {
            da.Dispose();
                if(cn.State == System.Data.ConnectionState.Open)
                {
                cn.Close();
                }
            }         
        }

  }

