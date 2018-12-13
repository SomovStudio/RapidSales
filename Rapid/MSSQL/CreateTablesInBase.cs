/*
 * Created by SharpDevelop.
 * User: Catfish
 * Date: 02.12.2018
 * Time: 13:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Rapid.MSSQL
{
	public class CreateTablesInBase
	{
		private SqlConnection _MsSql_Connection;
		private SqlCommand _MsSql_Command;
		private String _server;
		private String _database;
		private String _userid;
		private String _pass;
		private String _SqlCommand;
		
		//конструктор -----------------
		public CreateTablesInBase()
		{
			_MsSql_Connection = new SqlConnection();
			_MsSql_Command = new SqlCommand("", _MsSql_Connection);
		}
		
		//свойства ---------------------
		public String Server
		{
			get {return _server;}
			set {_server = value;}
		}
		public String DataBase
		{
			get {return _database;}
			set {_database = value;}
		}
		public String UserID
		{
			get {return _userid;}
			set {_userid = value;}
		}
		public String Pass
		{
			get {return _pass;}
			set {_pass = value;}
		}
		
		//методы --------------------------
		public bool CreateTables()
		{
			try{
				//Создание базы данных ===================================
				_MsSql_Connection.ConnectionString = "Server=" + _server + ";Database=;User Id=" + _userid + ";Password=" + _pass;
				_MsSql_Connection.Open();
				_SqlCommand = "CREATE DATABASE " + _database;
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_MsSql_Connection.Close();
				
				//Создание таблиц в базе данных ===========================
				_MsSql_Connection.ConnectionString = "Server=" + _server + ";Database=" + _database + ";User Id=" + _userid + ";Password=" + _pass;
				_MsSql_Connection.Open();
				
				/*Создать таблицу "Полный журнал документов" (journal)
				 * id_journal			- идентификатор
				 * journal_id_doc		- идентификатор документа (для табличной части и операции)
				 * journal_date			- дата документа
				 * journal_number		- номер документа
				 * journal_user_autor	- пользователь (автор документа) +
				 * journal_type			- тип документа (приход / расход / заказ / перемещение)
				 * journal_store		- склад +
				 * journal_firm_buyer	- покупатель +
				 * journal_firm_buyer_details			- реквизиты покупателя +
				 * journal_firm_seller	- продавец +
				 * journal_firm_seller_details			- реквизиты продавца +
				 * journal_staff_trade_representative	- торговый представитель +
				 * journal_typeTax		- вид налога +
				 * journal_sum			- сумма
				 * journal_tax			- налог
				 * journal_total		- итого
				 * journal_delete		- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE journal (" +
					"id_journal INT NOT NULL IDENTITY(1,1), " +
					"journal_id_doc VARCHAR(250) DEFAULT '' UNIQUE, " +
					"journal_date DATE NOT NULL, " +
					"journal_number VARCHAR(250) DEFAULT '', " +
					"journal_user_autor VARCHAR(250) DEFAULT '', " +
					"journal_type VARCHAR(100) DEFAULT '', " +
					"journal_store VARCHAR(250) DEFAULT '', " +
					"journal_firm_buyer VARCHAR(250) DEFAULT '', " +
					"journal_firm_buyer_details TEXT DEFAULT '', " +
					"journal_firm_seller VARCHAR(250) DEFAULT '', " +
					"journal_firm_seller_details TEXT DEFAULT '', " +
					"journal_staff_trade_representative VARCHAR(250) DEFAULT '', " +
					"journal_typeTax VARCHAR(250) DEFAULT '', " +
					"journal_sum FLOAT DEFAULT 0, " +
					"journal_tax FLOAT DEFAULT 0, " +
					"journal_total FLOAT DEFAULT 0, " +
					"journal_delete SMALLINT DEFAULT 0, " +
					"PRIMARY KEY (id_journal, journal_firm_buyer, journal_firm_seller, journal_typeTax, journal_store)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Табличная часть Документа" (tabularSection)
				 * id_tabularSection	- идентификатор
				 * tabularSection_tmc	- ТМЦ
				 * tabularSection_units	- ед. измерения
				 * tabularSection_number- количество
				 * tabularSection_price	- цена
				 * tabularSection_NDS	- НДС
				 * tabularSection_sum	- сумма
				 * tabularSection_total	- всего
				 * tabularSection_id_doc- документ (внешний ключ)
				 */
				_SqlCommand = "CREATE TABLE tabularSection ("+
					"id_tabularSection INT NOT NULL IDENTITY(1,1), " +
					"tabularSection_tmc VARCHAR(250) DEFAULT '', " +
					"tabularSection_units VARCHAR(250) DEFAULT '', " +
					"tabularSection_number FLOAT DEFAULT 0, " +
					"tabularSection_price FLOAT DEFAULT 0, " +
					"tabularSection_NDS FLOAT DEFAULT 0, " +
					"tabularSection_sum FLOAT DEFAULT 0, " +
					"tabularSection_total FLOAT DEFAULT 0, " +
					"tabularSection_id_doc VARCHAR(250) NOT NULL, " +
					"PRIMARY KEY (id_tabularSection, tabularSection_tmc), "+
					"FOREIGN KEY (tabularSection_id_doc) REFERENCES journal(journal_id_doc) ON UPDATE CASCADE ON DELETE NO ACTION" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "ТМЦ" (tmc)
				 * id_tmc				- идентификатор
				 * tmc_name				- наименование
				 * tmc_type_tax			- вид налога +
				 * tmc_units			- единицы измерения +
				 * tmc_buy				- цена покупки
				 * tmc_sale				- цена продажи
				 * tmc_store			- склад хранения
				 * tmc_additionally		- дополнительно
				 * tmc_type				- флаг тип записи (папка / запись)
				 * tmc_folder			- имя родительской папки
				 * tmc_delete			- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE tmc (" + 
					"id_tmc INT NOT NULL IDENTITY(1,1), " +
					"tmc_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"tmc_type_tax VARCHAR(250) DEFAULT '', " +
					"tmc_units VARCHAR(250) DEFAULT '', " +
					"tmc_buy FLOAT DEFAULT 0, " +
					"tmc_sale FLOAT DEFAULT 0, " +
					"tmc_store VARCHAR(250) DEFAULT '', " +
					"tmc_additionally TEXT DEFAULT '', " +
					"tmc_type SMALLINT DEFAULT 0, " +
					"tmc_folder VARCHAR(250) DEFAULT '', " +
					"tmc_delete SMALLINT DEFAULT 0, " +
					"INDEX tmc_name NONCLUSTERED (tmc_name), " +
					"PRIMARY KEY (id_tmc)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Единицы измерения" (units)
				 * id_units				- идентификатор
				 * units_name			- наименование
				 * units_additionally	- дополнительно
				 * units_delete			- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE units ("+ 
					"id_units INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"units_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"units_additionally TEXT DEFAULT '', " +
					"units_delete SMALLINT DEFAULT 0, " +
					"INDEX units_name NONCLUSTERED (units_name)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('шт.', 'Штуки.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('кг.', 'Килограммы.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('м.', 'Метры.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('см.', 'Сантиметны.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('л.', 'Литры.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO units (units_name, units_additionally, units_delete) VALUES ('г.', 'Граммы.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				
				/*Создать таблицу "Склады" (store)
				 * id_store				- идентификатор
				 * store_name			- наименование
				 * store_additionally	- дополнительно
				 * store_delete			- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE store ("+ 
					"id_store INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"store_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"store_additionally TEXT DEFAULT '', " +
					"store_delete SMALLINT DEFAULT 0, " +
					"INDEX store_name NONCLUSTERED (store_name)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO store (store_name, store_additionally, store_delete) VALUES ('Основной', 'Основной склад по умолчанию.', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Остатки" (balance)
				 * id_balance			- идентификатор
				 * balance_tmc			- ТМЦ
				 * balance_date			- дата
				 * balance_number		- количество
				 */
				_SqlCommand = "CREATE TABLE balance (" + 
					"id_balance INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"balance_tmc VARCHAR(250) DEFAULT '', " +
					"balance_date DATE NOT NULL, " +
					"balance_number FLOAT DEFAULT 0, " +
					"FOREIGN KEY (balance_tmc) REFERENCES tmc(tmc_name) ON UPDATE CASCADE ON DELETE CASCADE" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Фирмы" (firms)
				 * id_firm						- идентификатор
				 * firm_name					- наименование
				 * firm_details					- реквизиты
				 * firm_address_phone			- адрес и телефон
				 * firm_trade_representative	- сотрудник (торг. представитель) +
				 * firm_additionally			- дополнительно
				 * firm_type					- флаг тип записи (папка / запись)
				 * firm_folder					- имя родительской папки
				 * firm_delete					- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE firms ("+
					"id_firm INT NOT NULL IDENTITY(1,1), " +
					"firm_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"firm_details TEXT DEFAULT '', " +
					"firm_address_phone TEXT DEFAULT '', " +
					"firm_trade_representative VARCHAR(250) DEFAULT '', " +
					"firm_additionally TEXT DEFAULT '', " +
					"firm_type SMALLINT DEFAULT 0, " +
					"firm_folder VARCHAR(250) DEFAULT '', " +
					"firm_delete SMALLINT DEFAULT 0, " +
					"PRIMARY KEY (id_firm)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO firms (firm_name, firm_details, firm_address_phone, firm_trade_representative, firm_additionally, firm_type, firm_folder, firm_delete) VALUES ('Поставщики', '', '', '', '', 1, '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO firms (firm_name, firm_details, firm_address_phone, firm_trade_representative, firm_additionally, firm_type, firm_folder, firm_delete) VALUES ('Покупатели', '', '', '', '', 1, '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO firms (firm_name, firm_details, firm_address_phone, firm_trade_representative, firm_additionally, firm_type, firm_folder, firm_delete) VALUES ('Наша Фирма', '', '', '', '', 0, '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Вид налога" (typeTax)
				 * id_typeTax			- идентификатор
				 * typeTax_name			- наименование
				 * typeTax_rating		- ставка
				 * typeTax_additionally	- дополнительно
				 * typeTax_delete		- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE typeTax ("+
					"id_typeTax INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"typeTax_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"typeTax_rating FLOAT DEFAULT 0, " +
					"typeTax_additionally TEXT DEFAULT '', " +
					"typeTax_delete SMALLINT DEFAULT 0, " +
					"INDEX typeTax_name NONCLUSTERED (typeTax_name)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO typeTax (typeTax_name, typeTax_rating, typeTax_additionally, typeTax_delete) VALUES ('Налог 20%', 20, 'Ставка НДС 20%', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO typeTax (typeTax_name, typeTax_rating, typeTax_additionally, typeTax_delete) VALUES ('Налог 0%', 0, 'Ставка НДС 0% или без НДС', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу  "Сотрудники" (staff)
				 * id_staff				-идентификатор
				 * staff_name			- ф.и.о.
				 * staff_details		- реквизиты
				 * staff_address_phone	- адрес и телефон
				 * staff_date_hired		- нанят на работу
				 * staff_date_fired		- уволен с работы
				 * staff_fired			- флаг увольнения
				 * staff_salary			- зарплата
				 * staff_additionally	- дополнительно
				 * staff_type			- флаг тип записи (папка / запись)
				 * staff_folder			- имя родительской папки
				 * staff_delete			- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE staff ("+
					"id_staff INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"staff_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"staff_details TEXT DEFAULT '', " +
					"staff_address_phone TEXT DEFAULT '', " +
					"staff_date_hired DATE, staff_date_fired DATE, " +
					"staff_fired SMALLINT DEFAULT 0, " +
					"staff_salary FLOAT DEFAULT 0, " +
					"staff_additionally TEXT DEFAULT '', " +
					"staff_type SMALLINT DEFAULT 0, " +
					"staff_folder VARCHAR(250) DEFAULT '', " +
					"staff_delete SMALLINT DEFAULT 0" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				
				/*Создание таблицы "Пользователи" (users)
				 * id_user				- идентификатор
				 * user_name			- имя пользователя
				 * user_pass			- пароль
				 * user_right			- права
				 * user_additionally	- дополнительно
				 */
				_SqlCommand = "CREATE TABLE users ("+
					"id_user INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"user_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"user_pass VARCHAR(250) DEFAULT '', " +
					"user_right VARCHAR(100) DEFAULT '', " +
					"user_additionally TEXT DEFAULT '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO users (user_name, user_pass, user_right, user_additionally) VALUES ('Администратор', '', 'admin', 'Администратор конфигурации')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO users (user_name, user_pass, user_right, user_additionally) VALUES ('Пользователь', '', 'user', 'Пользователь конфигурации')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создать таблицу "Операции" (operations)
				 * id_operations		- идентификатор
				 * operations_date		- дата
				 * operations_id_doc	- идентификатор документа
				 * operations_DT		- счет ДТ
				 * operations_KT		- счет КД
				 * operations_sum		- сумма
				 * operations_specification - описание
				 */
				_SqlCommand = "CREATE TABLE operations ("+
					"id_operations INT NOT NULL IDENTITY(1,1), " +
					"operations_date DATE NOT NULL, " +
					"operations_id_doc VARCHAR(250) DEFAULT '', " +
					"operations_DT INT DEFAULT 0, " +
					"operations_KT INT DEFAULT 0, " +
					"operations_sum FLOAT DEFAULT 0, " +
					"operations_specification VARCHAR(50) DEFAULT '', " +
					"PRIMARY KEY (id_operations, operations_DT, operations_KT)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				
				/*Создать таблицу "План счетов" (planAccounts)
				 * id_planAccounts		- идентификатор
				 * planAccounts_name	- наименование
				 * planAccounts_account	- счёт
				 * planAccounts_type	- тип
				 * planAccounts_delete	- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE planAccounts ("+
					"id_planAccounts INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"planAccounts_name VARCHAR(250) DEFAULT '', " +
					"planAccounts_account INT DEFAULT 0 UNIQUE, " +
					"planAccounts_type VARCHAR(5) DEFAULT 'АП', " +
					"planAccounts_delete SMALLINT DEFAULT 0, " +
					"INDEX planAccounts_account NONCLUSTERED (planAccounts_account)" +
					")";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Основные средства', 10, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочиe необоротные материальные активы', 11, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Нематериальные активы', 12, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Износ (амортизация) неoборотных активов', 13, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные финансовые инвестиции', 14, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Капитальные инвестиции', 15, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные биологические активы', 16, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Отсроченные налоговые aктивы', 17, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочнaя дебиторская задолженность и проч. необоротные активы', 18, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Гудвилл', 19, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Производственные зaпасы', 20, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Текущие биологические активы', 21, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Малоценные быстроизнашивающиеся предметы', 22, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Производство', 23, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Брак в производстве', 24, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Полуфабрикаты', 25, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Готовая продукция', 26, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Продукция сельскохозяйственного производства', 27, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Товары', 28, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Касса', 30, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Счета в банках', 31, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Пpoчие денежные средства', 33, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Краткосрочные векселя полученные', 34, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Текущие финансовые инвестиции', 35, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты с покупателями, заказчиками', 36, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты с рaзными дебиторами', 37, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Резерв сомнительных долгoв', 38, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расходы будущих периодов', 39, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Уставный капитaл', 40, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Паевой капитал', 41, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Дополнительный капитал', 42, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Резервный капитал', 43, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Нераспредeлeнные прибыли (непокрытые убытки)', 44, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Изъятый капитал', 45, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Неоплаченный капитал', 46, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Обеспечение предстоящиx расходов и платежей', 47, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Целевое финансирование и цeлевые поступления', 48, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Страховые резервы', 49, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные займы', 50, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные векселя выданные', 51, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные обязатeльcтва по облигациям', 52, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Долгосрочные обязaтельства по аренде', 53, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Отсроченные налоговые обязательства', 54, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие долгосрочные обязательства', 55, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Краткосрочные зaймы', 60, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Тeкущая задолженность по долгосрочным обязательствам', 61, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Краткосрочные векселя выданные', 62, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты с поставщиками, подрядчиками', 63, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты по налогам и плaтежам', 64, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты по страхованию', 65, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты по выплaтам работникам', 66, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты с участниками', 67, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расчеты по дpугим операциям', 68, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Доходы будущих периодов', 69, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Доходы от реализации', 70, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочий операционный доход ', 71, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Доход от учаcтия в капитале', 72, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие финансовые доходы', 73, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие доходы', 74, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Чрезвычайные доходы', 75, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Страховые платежи', 76, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Финансовые результаты', 79, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Материальные затраты', 80, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Затраты на оплату труда', 81, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Отчисления на социальные мерoприятия', 82, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Амортизация', 83, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие операционные затраты', 84, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие затраты', 85, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Себестоимость реализации', 90, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Общепроизводственные расходы', 91, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Административные расходы', 92, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Расходы на сбыт', 93, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Пpочиe расходы операционной деятельности', 94, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Финансовые расходы', 95, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Потери от учacтия в капитале', 96, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Прочие расходы', 97, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Налог на прибыль', 98, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO planAccounts (planAccounts_name, planAccounts_account, planAccounts_type, planAccounts_delete) VALUES ('Чрезвычайные расходы', 99, 'АП', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*Создание таблицы "Константы" (constants)
				 * id_const				- идентификатор
				 * const_name			- наименование
				 * const_value			- значение
				 * const_additionally	- дополнительно
				 * const_delete			- флаг удаления
				 */
				_SqlCommand = "CREATE TABLE constants ("+
					"id_const INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"const_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"const_value VARCHAR(250) DEFAULT '', " +
					"const_additionally TEXT DEFAULT '', " +
					"const_delete SMALLINT DEFAULT 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Наша фирма', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Поставщик', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Покупатель', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Вид НДС', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Основной склад', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Ед. измерения', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Директор', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO constants (const_name, const_value, const_additionally, const_delete) VALUES ('Главный бухгалтер', '', '', 0)";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				
				/*Создать таблицу "История Обновлений" (historyUpdate)
				 * id_history			- идентификатор
				 * history_table_name	- имя таблицы
				 * history_table_represent - представление имени таблиц
				 * history_datetime		- дата и время обновления
				 * history_error		- сообщение об ошибке
				 * history_user			- пользователь
				 * history_client		- клиент
				 * history_action		- активность
				 * history_additionally - дополнительно
				 */
				_SqlCommand = "CREATE TABLE historyUpdate ("+
					"id_history INT NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
					"history_table_name VARCHAR(250) DEFAULT '' UNIQUE, " +
					"history_table_represent VARCHAR(250) DEFAULT '', " +
					"history_datetime VARCHAR(250) DEFAULT '', " +
					"history_error VARCHAR(250) DEFAULT '', " +
					"history_user VARCHAR(250) DEFAULT '', " +
					"history_client VARCHAR(250) DEFAULT '', " +
					"history_action VARCHAR(250) DEFAULT '', " +
					"history_additionally TEXT DEFAULT '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('users', 'Пользователи', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса			
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('constants', 'Константы', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('firms', 'Фирмы', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('tmc', 'ТМЦ', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('store', 'Склады', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('units', 'Единицы измерения', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('typeTax', 'Вид НДС', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('staff', 'Сотрудники', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('journal', 'Журнал документов', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('tabularSection', 'Табличная часть документа', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('balance', 'Остатки', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('operations', 'Операции', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				_SqlCommand = "INSERT INTO historyUpdate (history_table_name, history_table_represent, history_datetime, history_error, history_user, history_client, history_action, history_additionally) VALUES ('planAccounts', 'План счетов', '" + DateTime.Now.ToString() + "', '', '', '', '', '')";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				
				/*Создание связей ========================================*/
				
				/* Связь tableSection и tmc */
				_SqlCommand = "ALTER TABLE tabularSection ADD FOREIGN KEY (tabularSection_tmc) REFERENCES dbo.tmc (tmc_name) ON DELETE CASCADE ON UPDATE CASCADE";
				_MsSql_Command.CommandText = _SqlCommand;
				_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/* Связь journal и firms */
				//_SqlCommand = "ALTER TABLE journal ADD FOREIGN KEY (journal_firm_buyer) REFERENCES dbo.firms (firm_name) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				//_SqlCommand = "ALTER TABLE journal ADD FOREIGN KEY (journal_firm_seller) REFERENCES dbo.firms (firm_name) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/* Связь journal и typeTax */
				//_SqlCommand = "ALTER TABLE journal ADD FOREIGN KEY (journal_typeTax) REFERENCES dbo.typeTax (typeTax_name) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/* Связь journal и store */
				//_SqlCommand = "ALTER TABLE journal ADD FOREIGN KEY (journal_store) REFERENCES dbo.store (store_name) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/* Связь operation и planAccounts */
				//_SqlCommand = "ALTER TABLE operations ADD FOREIGN KEY (operations_DT) REFERENCES dbo.planAccounts (planAccounts_account) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				//_SqlCommand = "ALTER TABLE operations ADD FOREIGN KEY (operations_KT) REFERENCES dbo.planAccounts (planAccounts_account) ON DELETE NO ACTION ON UPDATE CASCADE";
				//_MsSql_Command.CommandText = _SqlCommand;
				//_MsSql_Command.ExecuteNonQuery();	//выполнение запроса
				
				/*=========================================================*/
				
				
				_MsSql_Connection.Close();
				return true;
			}catch(Exception ex){
				_MsSql_Connection.Close();
				MessageBox.Show(ex.ToString());	//Сообщение об ошибке
				return false;
			}
		}
	}
}
