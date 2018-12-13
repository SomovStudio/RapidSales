﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 10.03.2014
 * Время: 17:09
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Data;
using System.Windows.Forms;
using Rapid.MSSQL;

namespace Rapid
{
	/// <summary>
	/// Description of ClassBalance.
	/// </summary>
	public static class ClassBalance
	{
		/* Ввод нового товара в остатки */
		public static void BalanceNew(String NameTMC)
		{
			MsSQLShort SqlCommandBalance = new MsSQLShort();
			String DateInsert = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
			SqlCommandBalance.SqlCommand = "INSERT INTO balance (balance_tmc, balance_date, balance_number) VALUE ('" + NameTMC + "', '" + DateInsert + "', 0)";
			if(SqlCommandBalance.ExecuteNonQuery()){
				ClassForms.Rapid_Client.MessageConsole("Остатки: Новая запись успешно добавлена.", false);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка ввода новой записи в таблицу 'Остатки'", true);
		}
		
		/* Изменение имени в карточке товара остатков */
		public static void BalanceEdit(String NewNameTMC, String NameTMC)
		{
			MsSQLShort SqlCommandBalance = new MsSQLShort();
			SqlCommandBalance.SqlCommand = "UPDATE balance SET balance_tmc = '" + NewNameTMC + "' WHERE (balance_tmc = '"+NameTMC+"')";
			if(SqlCommandBalance.ExecuteNonQuery()){
				ClassForms.Rapid_Client.MessageConsole("Остатки: успешное изменение имени ТМЦ в остатках.", false);
			}else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка изменений в таблице 'Остатки', переименование тмц.", true);
						
		}
		
		/* Показать остаток на складе */
		public static String BalanceShow(String _tmcName, String _actualDate)
		{
			MsSQLFull balanceMySQL = new MsSQLFull();
			DataSet balanceDataSet = new DataSet();
			balanceDataSet.Clear();
			balanceDataSet.DataSetName = "balance";
			balanceMySQL.SelectSqlCommand = "SELECT * FROM balance WHERE (balance_tmc = '" + _tmcName + "' AND balance_date <= '" + _actualDate + "')";
			if(balanceMySQL.ExecuteFill(balanceDataSet, "balance")){
				DataTable table = balanceDataSet.Tables["balance"];
				if(table.Rows.Count > 0){
					return table.Rows[0]["balance_number"].ToString();
				} else return "--";
			} else return "--";
		}
		
		/* Обновление данных в остатках ----------------------------------------*/
		/* Увеличение остатков */
		public static void BalancePlus(DataSet ResourceDS)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "balance";
			_mySql.SelectSqlCommand = "SELECT id_balance, balance_tmc, balance_date, balance_number FROM balance";
			_mySql.InsertSqlCommand = "INSERT INTO balance (balance_tmc, balance_date, balance_number) VALUE (@balance_tmc, @balance_date, @balance_number)";
			_mySql.InsertParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateSqlCommand = "UPDATE balance SET balance_tmc = @balance_tmc, balance_date = @balance_date, balance_number = @balance_number WHERE (id_balance = @id_balance)";
			_mySql.UpdateParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			_mySql.DeleteSqlCommand = "DELETE FROM balance WHERE (id_balance = @id_balance)";
			_mySql.DeleteParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			if(_mySql.ExecuteFill(_dataSet, "balance")){
				
				// Ввод остатков
				foreach (DataRow rowTS in ResourceDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(rowTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) + ClassConversion.StringToDouble(rowTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
								
				if(_mySql.ExecuteUpdate(_dataSet, "balance")){
					ClassForms.Rapid_Client.MessageConsole("Остатки: Успешное обновление остатков.", false);
				}else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка ввод и сохранения новых остатков.", true);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к остаткам.", true);
		}
		
		/* Уменьшение остатков */
		public static void BalanceMinus(DataSet ResourceDS)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "balance";
			_mySql.SelectSqlCommand = "SELECT id_balance, balance_tmc, balance_date, balance_number FROM balance";
			_mySql.InsertSqlCommand = "INSERT INTO balance (balance_tmc, balance_date, balance_number) VALUE (@balance_tmc, @balance_date, @balance_number)";
			_mySql.InsertParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateSqlCommand = "UPDATE balance SET balance_tmc = @balance_tmc, balance_date = @balance_date, balance_number = @balance_number WHERE (id_balance = @id_balance)";
			_mySql.UpdateParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			_mySql.DeleteSqlCommand = "DELETE FROM balance WHERE (id_balance = @id_balance)";
			_mySql.DeleteParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			if(_mySql.ExecuteFill(_dataSet, "balance")){
				
				// Ввод остатков
				foreach (DataRow rowTS in ResourceDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(rowTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) - ClassConversion.StringToDouble(rowTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
								
				if(_mySql.ExecuteUpdate(_dataSet, "balance")){
					ClassForms.Rapid_Client.MessageConsole("Остатки: Успешное обновление остатков.", false);
				}else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка ввод и сохранения новых остатков.", true);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к остаткам.", true);
		}
		
		/* Увеличение остатков после изменений табличной части документа */
		public static void BalanceUpdatePlus(DataSet OldDS, DataSet NewDS)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "balance";
			_mySql.SelectSqlCommand = "SELECT id_balance, balance_tmc, balance_date, balance_number FROM balance";
			_mySql.InsertSqlCommand = "INSERT INTO balance (balance_tmc, balance_date, balance_number) VALUE (@balance_tmc, @balance_date, @balance_number)";
			_mySql.InsertParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateSqlCommand = "UPDATE balance SET balance_tmc = @balance_tmc, balance_date = @balance_date, balance_number = @balance_number WHERE (id_balance = @id_balance)";
			_mySql.UpdateParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			_mySql.DeleteSqlCommand = "DELETE FROM balance WHERE (id_balance = @id_balance)";
			_mySql.DeleteParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			if(_mySql.ExecuteFill(_dataSet, "balance")){
				
				// Восстановление старыми данными
				foreach (DataRow oldTS in OldDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(oldTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) - ClassConversion.StringToDouble(oldTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
				
				// Обновление новыми данными
				foreach (DataRow newTS in NewDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(newTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) + ClassConversion.StringToDouble(newTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
				
								
				if(_mySql.ExecuteUpdate(_dataSet, "balance")){
					ClassForms.Rapid_Client.MessageConsole("Остатки: Успешное обновление остатков.", false);
				}else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка ввод и сохранения новых остатков.", true);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к остаткам.", true);
		}
		
		/* Уменьшение остатков после изменений табличной части документа */		
		public static void BalanceUpdateMinus(DataSet OldDS, DataSet NewDS)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "balance";
			_mySql.SelectSqlCommand = "SELECT id_balance, balance_tmc, balance_date, balance_number FROM balance";
			_mySql.InsertSqlCommand = "INSERT INTO balance (balance_tmc, balance_date, balance_number) VALUE (@balance_tmc, @balance_date, @balance_number)";
			_mySql.InsertParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.InsertParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateSqlCommand = "UPDATE balance SET balance_tmc = @balance_tmc, balance_date = @balance_date, balance_number = @balance_number WHERE (id_balance = @id_balance)";
			_mySql.UpdateParametersAdd("@balance_tmc", SqlDbType.VarChar, 250, "balance_tmc", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_date", SqlDbType.Date, 10, "balance_date", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@balance_number", SqlDbType.Float, 10, "balance_number", UpdateRowSource.None);
			_mySql.UpdateParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			_mySql.DeleteSqlCommand = "DELETE FROM balance WHERE (id_balance = @id_balance)";
			_mySql.DeleteParametersAdd("@id_balance", SqlDbType.SmallInt, 11, "id_balance", UpdateRowSource.None);
			if(_mySql.ExecuteFill(_dataSet, "balance")){
				
				// Восстановление старыми данными
				foreach (DataRow oldTS in OldDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(oldTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) + ClassConversion.StringToDouble(oldTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
				
				// Обновление новыми данными
				foreach (DataRow newTS in NewDS.Tables["tabularsection"].Rows)
        		{
					foreach (DataRow rowBalance in _dataSet.Tables["balance"].Rows){
						if(newTS["tabularSection_tmc"].ToString() == rowBalance["balance_tmc"].ToString()){
							double sum = ClassConversion.StringToDouble(rowBalance["balance_number"].ToString()) - ClassConversion.StringToDouble(newTS["tabularSection_number"].ToString());
							rowBalance["balance_number"] = sum;
						}
					}
				}
				
								
				if(_mySql.ExecuteUpdate(_dataSet, "balance")){
					ClassForms.Rapid_Client.MessageConsole("Остатки: Успешное обновление остатков.", false);
				}else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка ввод и сохранения новых остатков.", true);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к остаткам.", true);
		}
		
		/* Щстатки после восстановления документа */
		public static void BalanceRecovery(String docID, bool actionPlus)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "tabularsection";
			_mySql.SelectSqlCommand = "SELECT tabularsection.*, journal.* FROM tabularsection, journal WHERE (journal.id_journal = " + docID + " AND tabularsection.tabularSection_id_doc = journal.journal_id_doc)";
			if(_mySql.ExecuteFill(_dataSet, "tabularsection")){
				if(actionPlus) BalancePlus(_dataSet);
				else BalanceMinus(_dataSet);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к табличной части документа.", true);
		}
		
		/* Остатки после удаления документа */
		public static void BalanceRemoval(String docID, bool actionPlus)
		{
			MsSQLFull _mySql = new MsSQLFull();
			DataSet _dataSet = new DataSet();
			_dataSet.Clear();
			_dataSet.DataSetName = "tabularsection";
			_mySql.SelectSqlCommand = "SELECT tabularsection.*, journal.* FROM tabularsection, journal WHERE (journal.id_journal = " + docID + " AND tabularsection.tabularSection_id_doc = journal.journal_id_doc)";
			if(_mySql.ExecuteFill(_dataSet, "tabularsection")){
				if(actionPlus) BalancePlus(_dataSet);
				else BalanceMinus(_dataSet);
			} else ClassForms.Rapid_Client.MessageConsole("Остатки: Ошибка обращения к табличной части документа.", true);
		}
	}
}
