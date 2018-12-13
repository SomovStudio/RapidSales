/*
 * Created by SharpDevelop.
 * User: Catfish
 * Date: 02.12.2018
 * Time: 12:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Rapid.MSSQL
{
	/// <summary>
	/// Description of MsSQLShort.
	/// </summary>
	public class MsSQLShort
	{
		private SqlConnection _MsSql_Connection;
		private SqlCommand _MsSql_Command;
		private String _SqlCommand;
		
		//конструктор ---------------------
		public MsSQLShort()
		{
			_MsSql_Connection = new SqlConnection();
			_MsSql_Connection.ConnectionString = "Server=" + 
				ClassConfig.Rapid_Run_Server + ";Database=" + 
				ClassConfig.Rapid_Run_DataBase + ";User Id=" + 
				ClassConfig.Rapid_Run_Uid + ";Password=" + 
				ClassConfig.Rapid_Run_Pwd;
			_MsSql_Command = new SqlCommand("", _MsSql_Connection);
		}
		
		//свойства ------------------------
		public String SqlCommand
		{
			get {return _SqlCommand;}
			set {_SqlCommand = value;}
		}
		
		//методы --------------------------
		public bool ExecuteNonQuery()
		{
			try{
				_MsSql_Connection.Open();
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_MsSql_Connection.Close();
				return true;
			}catch(Exception ex){
				_MsSql_Connection.Close();
				
				if(MessageBox.Show("Ошибка выполнения SQL запроса." + System.Environment.NewLine + "Показать полное сообщение?","Ошибка:", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					MessageBox.Show(ex.ToString());
				}
				
				return false; //произошла ошибка.
			}
		}
	}
}
