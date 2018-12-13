/*
 * Created by SharpDevelop.
 * User: Catfish
 * Date: 02.12.2018
 * Time: 12:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Rapid.MSSQL
{
	public class MsSQLFull
	{
		private SqlConnection _MsSql_Connection;
		private SqlCommand _MsSql_Select_Command;
		private SqlCommand _MsSql_Update_Command;
		private SqlCommand _MsSql_Insert_Command;
		private SqlCommand _MsSql_Delete_Command;
		private SqlDataAdapter _MsSql_DataAdapter;
		private String _Select_SqlCommand;
		private String _Update_SqlCommand;
		private String _Insert_SqlCommand;
		private String _Delete_SqlCommand;
		
		//конструктор ---------------------
		public MsSQLFull()
		{
			_MsSql_Connection = new SqlConnection();
			_MsSql_Connection.ConnectionString = "Server=" + 
				ClassConfig.Rapid_Run_Server + ";Database=" + 
				ClassConfig.Rapid_Run_DataBase + ";User Id=" + 
				ClassConfig.Rapid_Run_Uid + ";Password=" + 
				ClassConfig.Rapid_Run_Pwd;
			_MsSql_Select_Command = new SqlCommand("", _MsSql_Connection);
			_MsSql_Update_Command = new SqlCommand("", _MsSql_Connection);
			_MsSql_Insert_Command = new SqlCommand("", _MsSql_Connection);
			_MsSql_Delete_Command = new SqlCommand("", _MsSql_Connection);
			_MsSql_DataAdapter = new SqlDataAdapter();
		}
		
		//свойства ------------------------
		public String SelectSqlCommand
		{
			get {return _Select_SqlCommand;}
			set {_Select_SqlCommand = value;}
		}
		public String UpdateSqlCommand
		{
			get {return _Update_SqlCommand;}
			set {_Update_SqlCommand = value;}
		}
		public String InsertSqlCommand
		{
			get {return _Insert_SqlCommand;}
			set {_Insert_SqlCommand = value;}
		}
		public String DeleteSqlCommand
		{
			get {return _Delete_SqlCommand;}
			set {_Delete_SqlCommand = value;}
		}
		
		//методы --------------------------
		public bool ExecuteFill(DataSet _DataSet, String _TableName){
			try{
				_MsSql_Connection.Open();
				
				_MsSql_Select_Command.CommandText = _Select_SqlCommand;
				_MsSql_Update_Command.CommandText = _Update_SqlCommand;
				_MsSql_Insert_Command.CommandText = _Insert_SqlCommand;
				_MsSql_Delete_Command.CommandText = _Delete_SqlCommand;
				_MsSql_DataAdapter.SelectCommand = _MsSql_Select_Command;
				_MsSql_DataAdapter.UpdateCommand = _MsSql_Update_Command;
				_MsSql_DataAdapter.InsertCommand = _MsSql_Insert_Command;
				_MsSql_DataAdapter.DeleteCommand = _MsSql_Delete_Command;
				_MsSql_DataAdapter.Fill(_DataSet, _TableName);
				
				_MsSql_Connection.Close();
				return true;
			}catch(Exception ex){
				_MsSql_Connection.Close();
				return false; //произошла ошибка.
				/*if(MessageBox.Show("Ошибка выполнения SQL запроса." + System.Environment.NewLine + "Показать полное сообщение?","Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
				{
					MessageBox.Show(ex.ToString());
				}
				*/
			}
		}
		
		public bool ExecuteUpdate(DataSet _DataSet, String _TableName){
			try{
				_MsSql_Connection.Open();
				
				_MsSql_Select_Command.CommandText = _Select_SqlCommand;
				_MsSql_Update_Command.CommandText = _Update_SqlCommand;
				_MsSql_Insert_Command.CommandText = _Insert_SqlCommand;
				_MsSql_Delete_Command.CommandText = _Delete_SqlCommand;
				_MsSql_DataAdapter.SelectCommand = _MsSql_Select_Command;
				_MsSql_DataAdapter.UpdateCommand = _MsSql_Update_Command;
				_MsSql_DataAdapter.InsertCommand = _MsSql_Insert_Command;
				_MsSql_DataAdapter.DeleteCommand = _MsSql_Delete_Command;
				_MsSql_DataAdapter.Update(_DataSet, _TableName);
				
				_MsSql_Connection.Close();
				return true;
			}catch(Exception ex){
				_MsSql_Connection.Close();
				if(MessageBox.Show("Ошибка выполнения SQL запроса." + System.Environment.NewLine + "Показать полное сообщение?","Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)	//Сообщение об ошибке
				{
					MessageBox.Show(ex.ToString());
				}
				return false; //произошла ошибка.
			}
		}
		
		public void SelectParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			_MsSql_Select_Command.Parameters.Add(parameterName, dbType, size, sourceColumn);
		}
		
		public void InsertParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			_MsSql_Insert_Command.Parameters.Add(parameterName, dbType, size, sourceColumn);
		}
		
		public void UpdateParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			_MsSql_Update_Command.Parameters.Add(parameterName, dbType, size, sourceColumn);
		}
		
		public void DeleteParametersAdd(String parameterName, SqlDbType dbType, int size, String sourceColumn, UpdateRowSource urs)
		{
			_MsSql_Delete_Command.Parameters.Add(parameterName, dbType, size, sourceColumn);
		}
		
	}
}
