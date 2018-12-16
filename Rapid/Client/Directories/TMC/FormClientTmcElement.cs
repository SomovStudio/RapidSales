﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 15.02.2014
 * Время: 11:11
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Rapid.MSSQL;
using Rapid.Service;

namespace Rapid
{
	/// <summary>
	/// Description of FormClientTmcElement.
	/// </summary>
	public partial class FormClientTmcElement : Form
	{
		/* Глобальные переменные */
		public String ActionID;
		public FormClientTmc Rapid_ClientTmc;
		private MsSQLFull _tmcMySQL = new MsSQLFull();
		private DataSet _tmcDataSet = new DataSet();
		private String editName = "";
		
		public FormClientTmcElement()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		/* ЗАГРУЗКА: Загрузка окна */
		void WindowLoad() // Загрузка окна
		{
			// Загрузка списка доступных папок
			_tmcDataSet.Clear();
			_tmcDataSet.DataSetName = "tmc";
			_tmcMySQL.SelectSqlCommand = "SELECT * FROM tmc WHERE (tmc_type = 1 AND tmc_delete = 0) ORDER BY tmc_name ASC";
			if(_tmcMySQL.ExecuteFill(_tmcDataSet, "tmc")){
				DataTable table = _tmcDataSet.Tables["tmc"];
				foreach(DataRow row in table.Rows)
        		{
					comboBox1.Items.Add(row["tmc_name"].ToString());
				}
			} else ClassForms.Rapid_Client.MessageConsole("ТМЦ: Ошибка при выгрузке перечня доступных папок.", true);
			// При создании новой записи
			if(this.Text == "Новая запись."){
				// Загружаем информацию из констант
				textBox2.Text = ClassSelectConst.constantValue("Вид НДС");
				textBox3.Text = ClassSelectConst.constantValue("Ед. измерения");
				textBox7.Text = ClassSelectConst.constantValue("Основной склад");
				ClassForms.Rapid_Client.MessageConsole("ТМЦ: Создание новой записи.", false);
			}
			// При изменении записи
			if(this.Text == "Изменить запись."){
				_tmcDataSet.Clear();
				_tmcDataSet.DataSetName = "tmc";
				_tmcMySQL.SelectSqlCommand = "SELECT * FROM tmc WHERE (id_tmc = " + ActionID + ")";
				if(_tmcMySQL.ExecuteFill(_tmcDataSet, "tmc")){
					DataTable table = _tmcDataSet.Tables["tmc"];
					textBox1.Text = table.Rows[0]["tmc_name"].ToString();
					textBox2.Text = table.Rows[0]["tmc_type_tax"].ToString();
					textBox3.Text = table.Rows[0]["tmc_units"].ToString();
					textBox4.Text = ClassConversion.StringToMoney(table.Rows[0]["tmc_buy"].ToString());
					textBox5.Text = ClassConversion.StringToMoney(table.Rows[0]["tmc_sale"].ToString());
					textBox6.Text = table.Rows[0]["tmc_additionally"].ToString();
					textBox7.Text = table.Rows[0]["tmc_store"].ToString();
					comboBox1.Text = table.Rows[0]["tmc_folder"].ToString();
					ClassForms.Rapid_Client.MessageConsole("ТМЦ: запись №" + ActionID + " успешно открыта для редактирования.", false);
				}else ClassForms.Rapid_Client.MessageConsole("ТМЦ: Ошибка выполнения запроса к таблице 'ТМЦ' обращение к записи с идентификатором " + ActionID + " тип записи 'Запись'.", true);
				editName = textBox1.Text;
			}
		}
		
		void FormClientTmcElementLoad(object sender, EventArgs e)
		{
			WindowLoad(); // Загрузка окна			
		}
		/*----------------------------------------------------------------*/
		
		/* Закрываем окно */
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void FormClientTmcElementClosed(object sender, EventArgs e)
		{
			ClassForms.Rapid_Client.MessageConsole("ТМЦ: закрыто окно обработки записи.", false);
		}
		/*----------------------------------------------------------------*/
		
		/* СОХРАНЕНИЕ: сохранение данных в таблицу */
		void SaveData() // созранение данных
		{
			MsSQLShort SQlCommand = new MsSQLShort();
			
			// При сохранении новой записи
			if(this.Text == "Новая запись."){
				SQlCommand.SqlCommand = "INSERT INTO tmc (tmc_name, tmc_type_tax, tmc_units, tmc_buy, tmc_sale, tmc_store, tmc_additionally, tmc_type, tmc_folder, tmc_delete) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', " + textBox4.Text + ", " + textBox5.Text + ", '" + textBox7.Text + "', '" + textBox6.Text + "', 0, '" + comboBox1.Text + "', 0)";
				if(SQlCommand.ExecuteNonQuery()){
					// Создание записи в остатках
					ClassBalance.BalanceNew(textBox1.Text);
					
					// ИСТОРИЯ: Запись в журнал истории обновлений
					ClassServer.SaveUpdateInBase(4, DateTime.Now.ToString(), "", "Создание новой записи.", "");
					ClassForms.Rapid_Client.MessageConsole("ТМЦ: успешное создание новой записи.", false);
					Close();
				} else ClassForms.Rapid_Client.MessageConsole("ТМЦ: Ошибка выполнения запроса к таблице 'ТМЦ' при создании новой записи.", true);
			}
			// При сохранении измененной записи
			if(this.Text == "Изменить запись."){
				if(ClassConfig.Rapid_Client_UserRight == "admin"){
					SQlCommand.SqlCommand = "UPDATE tmc SET tmc_name = '" + textBox1.Text + "', tmc_type_tax = '" + textBox2.Text + "', tmc_units = '" + textBox3.Text + "', tmc_buy = " + textBox4.Text + ", tmc_sale = " + textBox5.Text + ", tmc_store = '" + textBox7.Text + "', tmc_additionally = '" + textBox6.Text + "', tmc_folder = '" + comboBox1.Text + "' WHERE (id_tmc = " + ActionID + ") ";
					if(SQlCommand.ExecuteNonQuery()){
						// Изменение записей в остатках
						ClassBalance.BalanceEdit(textBox1.Text, editName);
						
						// ИСТОРИЯ: Запись в журнал истории обновлений
						ClassServer.SaveUpdateInBase(4, DateTime.Now.ToString(), "", "Изменение записи.", "");
						ClassForms.Rapid_Client.MessageConsole("ТМЦ: успешное изменение записи.", false);
						Close();
					} else ClassForms.Rapid_Client.MessageConsole("ТМЦ: Ошибка выполнения запроса к таблице 'ТМЦ' при изменении записи.", true);
				}else{
					MessageBox.Show("Извините но вы '" + ClassConfig.Rapid_Client_UserName + "' не обладаете достаточными правами для ввода изменений.","Сообщение");
					ClassForms.Rapid_Client.MessageConsole("Сотрудники: у вас недостаточно прав для ввода изменений.", false);
				}
			}
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if(textBox1.Text != "") SaveData(); // созранение данных
			else MessageBox.Show("Вы не ввели значение наименование!","Сообщение",MessageBoxButtons.OK);			
		}
		/*----------------------------------------------------------------*/
		
		/* Цена покупки --------------------------------------------------*/
		/* При потере фокуса */
		void TextBox4TextLostFocus(object sender, EventArgs e)
		{
			String Money = textBox4.Text;
			textBox4.Clear();
			textBox4.Text = ClassConversion.StringToMoney(Money);
			if(textBox4.Text == "" || ClassConversion.checkString(textBox4.Text) == false) textBox4.Text = "0.00";
		}
		
		/* При вводе значения */
		void TextBox4TextChanged(object sender, EventArgs e)
		{
			if(textBox4.Text == "" || ClassConversion.checkString(textBox4.Text) == false) textBox4.Text = "0.00";
		}
		
		/* При нажатии на Интер*/
		void TextBox4KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox4.Text;
				textBox4.Clear();
				textBox4.Text = ClassConversion.StringToMoney(Value);
				if(textBox4.Text == "" || ClassConversion.checkString(textBox4.Text) == false) textBox4.Text = "0.00";
			}
		}
				
		/* Калькулятор */
		void Button5Click(object sender, EventArgs e)
		{
			FormServiceCalculator Calc = new FormServiceCalculator(true);
			Calc.TextBoxReturnValue = this.textBox4;
			Calc.MdiParent = ClassForms.Rapid_Client;
			Calc.Show();
		}
		/*----------------------------------------------------------------*/
		
		/* Цена покупки --------------------------------------------------*/
		/* При потере фокуса */
		void TextBox5TextLostFocus(object sender, EventArgs e)
		{
			String Money = textBox5.Text;
			textBox5.Clear();
			textBox5.Text = ClassConversion.StringToMoney(Money);
			if(textBox5.Text == "" || ClassConversion.checkString(textBox5.Text) == false) textBox5.Text = "0.00";
		}
		
		/* При вводе значения */
		void TextBox5TextChanged(object sender, EventArgs e)
		{
			if(textBox5.Text == "" || ClassConversion.checkString(textBox5.Text) == false) textBox5.Text = "0.00";
		}
		
		/* При нажатии на Интер*/
		void TextBox5KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab){
				String Value = textBox5.Text;
				textBox5.Clear();
				textBox5.Text = ClassConversion.StringToMoney(Value);
				if(textBox5.Text == "" || ClassConversion.checkString(textBox5.Text) == false) textBox5.Text = "0.00";
			}
		}
		
		/* Калькулятор */
		void Button6Click(object sender, EventArgs e)
		{
			FormServiceCalculator Calc = new FormServiceCalculator(true);
			Calc.TextBoxReturnValue = this.textBox5;
			Calc.MdiParent = ClassForms.Rapid_Client;
			Calc.Show();
		}
		/*----------------------------------------------------------------*/
		
		void Button8Click(object sender, EventArgs e)
		{
			textBox4.Text = "0.00";			
		}
		
		void Button9Click(object sender, EventArgs e)
		{
			textBox5.Text = "0.00";				
		}
		/*----------------------------------------------------------------*/
		
		/* Обращение к справочнику "Склад" */
		void SelectStore() // выбрать склад
		{
			ClassForms.Rapid_ClientStore = new FormClientStore();
			ClassForms.Rapid_ClientStore.ShowMenuReturnValue();
			ClassForms.Rapid_ClientStore.MdiParent = ClassForms.Rapid_Client;
			ClassForms.Rapid_ClientStore.TextBoxReturnValue = textBox7;
			ClassForms.Rapid_ClientStore.Show();
		}
		
		void Button7Click(object sender, EventArgs e)
		{
			SelectStore();
		}
		/*----------------------------------------------------------------*/
		
		/* Обращение к справочнику "Вид налога" */
		void SelectTax() // выбрать налога
		{
			ClassForms.Rapid_ClientTypeTax = new FormClientTypeTax();
			ClassForms.Rapid_ClientTypeTax.ShowMenuReturnValue();
			ClassForms.Rapid_ClientTypeTax.MdiParent = ClassForms.Rapid_Client;
			ClassForms.Rapid_ClientTypeTax.TextBoxReturnValue = textBox2;
			ClassForms.Rapid_ClientTypeTax.Show();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			SelectTax();
		}
		/*----------------------------------------------------------------*/
		
		/* Обращение к справочнику "Ед. изм." */
		void SelectUnits() // выбрать ед. изм.
		{
			ClassForms.Rapid_ClientUnits = new FormClientUnits();
			ClassForms.Rapid_ClientUnits.ShowMenuReturnValue();
			ClassForms.Rapid_ClientUnits.MdiParent = ClassForms.Rapid_Client;
			ClassForms.Rapid_ClientUnits.TextBoxReturnValue = textBox3;
			ClassForms.Rapid_ClientUnits.Show();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			SelectUnits();
		}
		
	}
}
