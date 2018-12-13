﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Сомов Евгений Павлович
 * Дата: 03.02.2014
 * Время: 12:23
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
	/// Description of ClassServer.
	/// </summary>
	public static class ClassServer
	{
		/* Массив хранения обновлений базы на сервере
		 * Поля:
		 * 		0. идентификатор
		 * 		1. имя таблицы
		 * 		2. представление таблицы
		 * 		3. дата и время обновления
		 * 		4. сообщение об ошибке
		 * 		5. пользователь
		 * 		6. клиент
		 * 		7. активность
		 * 		8. дополнительно 		
		*/
		public static String[,] TableUpdate = new string[13,9]; //13 - строк, 9 - полей
		
		/* ПРОВЕРКА ОБНОВЛЕНИЙ НА СЕРВЕРЕ */
		private static MsSQLFull _serverMySQL = new MsSQLFull();
		private static DataSet _serverDataSet = new DataSet();
		public static bool CheckBaseUpdate()
		{
			_serverDataSet.Clear();
			_serverDataSet.DataSetName = "historyupdate";
			_serverMySQL.SelectSqlCommand = "SELECT * FROM historyupdate";
			if(_serverMySQL.ExecuteFill(_serverDataSet, "historyupdate") == false){
				ClassForms.Rapid_Client.MessageConsole("Сервер: Ошибка выполнения обращения к журналу истории обновлений.", true);
				return false;
			}
			DataTable _table = _serverDataSet.Tables["historyupdate"];
			
			for (int i = 0; i < 13; i++){
				if(_table.Rows[i]["history_datetime"].ToString() != ClassServer.TableUpdate[i,3]){
					ClassServer.TableUpdate[i,0] = _table.Rows[i]["id_history"].ToString();
					ClassServer.TableUpdate[i,1] = _table.Rows[i]["history_table_name"].ToString();
					ClassServer.TableUpdate[i,2] = _table.Rows[i]["history_table_represent"].ToString();
					ClassServer.TableUpdate[i,3] = _table.Rows[i]["history_datetime"].ToString();
					ClassServer.TableUpdate[i,4] = _table.Rows[i]["history_error"].ToString();
					ClassServer.TableUpdate[i,5] = _table.Rows[i]["history_user"].ToString();
					ClassServer.TableUpdate[i,6] = _table.Rows[i]["history_client"].ToString();
					ClassServer.TableUpdate[i,7] = _table.Rows[i]["history_action"].ToString();
					ClassServer.TableUpdate[i,8] = _table.Rows[i]["history_additionally"].ToString();
					ShowUpdateInTable(Int32.Parse(_table.Rows[i]["id_history"].ToString()));
					if(ClassForms.LoadAdministrator == false) ClassForms.Rapid_Client.MessageConsole("Сервер: обновление журнала таблица: '" + ClassServer.TableUpdate[i,2] + "'  (дата и время: " + ClassServer.TableUpdate[i,3] + ").", false);
					else ClassForms.Rapid_Administrator.MessageConsole("Сервер: обновление журнала таблица: '" + ClassServer.TableUpdate[i,2] + "'  (дата и время: " + ClassServer.TableUpdate[i,3] + ").", false, _table.Rows[i]["history_client"].ToString());
				}	
			}
			return true;
		}
		
		
		
		/* СОХРАНЕНИЕ ИЗМЕНЕНИЙ В ЖУРНАЛ ИСТОРИИ */
		public static void SaveUpdateInBase(int TableID, String history_datetime, String history_error, String history_action, String history_additionally)
		{
			String clientHost = System.Net.Dns.GetHostName(); //ip адрес клиента
			//String clientIP = System.Net.Dns.GetHostByName(clientHost).AddressList[0].ToString();
			String clientIP = System.Net.Dns.GetHostEntry(clientHost).AddressList[0].ToString();
			
			MsSQLShort SQlCommand = new MsSQLShort();
			SQlCommand.SqlCommand = "UPDATE historyupdate SET history_datetime = '" + history_datetime + "', history_error = '" + history_error + "', history_action = '" + history_action + "', history_user = '" + ClassConfig.Rapid_Client_UserName + "', history_client = '" + clientIP + "', history_additionally = '" + history_additionally + "' WHERE (id_history = " + TableID + ")";
			if(!SQlCommand.ExecuteNonQuery()){
				ClassForms.Rapid_Client.MessageConsole("Сервер: Ошибка записи в журнал", true);
			}
		}
		
		/* ОБНОВЛЕНИЕ ОТКРЫТЫХ ТАБЛИЦ */
		public static void ShowUpdateInTable(int idTable){
			// обновление справочника "пользователи"
			if(idTable == 1 && ClassForms.OpenCloseFormUser) ClassForms.Rapid_Users.TableUpdate();
			// обновление справочника "константы"
			if(idTable == 2 && ClassForms.OpenCloseFormConst) ClassForms.Rapid_ClientConst.TableUpdate();
			// обновление справочника "фирмы"
			if(idTable == 3 && ClassForms.OpenCloseFormFirms) ClassForms.Rapid_ClientFirms.TableUpdate(ClassForms.Rapid_ClientFirms.openFolder);
			// обновление справочника "тмс"
			if(idTable == 4 && ClassForms.OpenCloseFormTmc)	ClassForms.Rapid_ClientTmc.TableUpdate(ClassForms.Rapid_ClientTmc.openFolder);
			// обновление справочника "склады"
			if(idTable == 5 && ClassForms.OpenCloseFormStore) ClassForms.Rapid_ClientStore.TableUpdate();
			// обновление справочника "ед. изм."
			if(idTable == 6 && ClassForms.OpenCloseFormUnits) ClassForms.Rapid_ClientUnits.TableUpdate();
			// обновление справочника "вид налога"
			if(idTable == 7 && ClassForms.OpenCloseFormTypeTax) ClassForms.Rapid_ClientTypeTax.TableUpdate();
			// обновление справлчник "Сотрудники"
			if(idTable == 8 && ClassForms.OpenCloseFormStaff) ClassForms.Rapid_ClientStaff.TableUpdate(ClassForms.Rapid_ClientStaff.openFolder);
			// обновление журнала "Полный журнал"
			if(idTable == 9 && ClassForms.OpenCloseFormJournalDoc) ClassForms.Rapid_ClientJournalDoc.TableUpdate();
			// обновление журнала "Журнал Заказов"
			if(idTable == 9 && ClassForms.OpenCloseFormJournalOrder) ClassForms.Rapid_ClientJournalOrder.TableUpdate();
			// обновление журнала "Журнал Приходных накладных"
			if(idTable == 9 && ClassForms.OpenCloseFormJournalComing) ClassForms.Rapid_ClientJournalComing.TableUpdate();
			// обновление журнала "Журнал Расходных накладных"
			if(idTable == 9 && ClassForms.OpenCloseFormJournalExpense) ClassForms.Rapid_ClientJournalExpense.TableUpdate();
			// обновление журнала "Бухгалтерские операции"
			if(idTable == 12 && ClassForms.OpenCloseFormJournalOperations) ClassForms.Rapid_ClientJournalOperations.TableUpdate();
			// обновление справлчник "План счетов"
			if(idTable == 13 && ClassForms.OpenCloseFormPlanAccounts) ClassForms.Rapid_ClientPlanAccounts.TableUpdate();
			
		}
	}
}
