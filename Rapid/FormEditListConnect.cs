/*
 * Сделано в SharpDevelop.
 * Пользователь: Сомов Евгений Павлович
 * Дата: 14.09.2013
 * Время: 13:09
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Rapid.MSSQL;

namespace Rapid
{
	/// <summary>
	/// Description of FormEditListConnect.
	/// </summary>
	public partial class FormEditListConnect : Form
	{
		public FormEditListConnect()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public String SelectBase = "0";
		
		/* Сохранение новой или изменённой информации о конфигурации */
		void SaveRecord()
		{
			/*
			if(panel1.Visible == false){
				//Контроль безопасности
				if(textBoxAdminPass.Text  == "12345"){
					groupBox1.Visible = false;
					panel1.Visible = true;
					panel2.Visible = true;
				}else{
					MessageBox.Show("Вы ввели не верный пароль!");
				}
			}else{
				//Сохранение новых данных
				if(this.Text == "Добавить запись"){
					ClassForms.Rapid_SelectLoad.TreeInsert(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
					ClassForms.Rapid_SelectLoad.TreeUpdateInit();
					Close();
				}
				//Сохранить изменённые данные
				if(this.Text == "Изменить запись"){
					ClassForms.Rapid_SelectLoad.TreeUpdate(SelectBase, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
					ClassForms.Rapid_SelectLoad.TreeUpdateInit();
					Close();
				}
			}
			*/
			//Сохранение новых данных
			if(this.Text == "Добавить запись"){
				ClassForms.Rapid_SelectLoad.TreeInsert(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
				ClassForms.Rapid_SelectLoad.TreeUpdateInit();
				Close();
			}
			//Сохранить изменённые данные
			if(this.Text == "Изменить запись"){
				ClassForms.Rapid_SelectLoad.TreeUpdate(SelectBase, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
				ClassForms.Rapid_SelectLoad.TreeUpdateInit();
				Close();
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			SaveRecord();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
		
		/* Проверка соединения */
		void CheckConnect()
		{
			SqlConnection MsSql_Connection = new SqlConnection();
			//проверка подключения к базе данных
			try{
				panel3.Visible = true;
				this.Update();
				MsSql_Connection.ConnectionString = "Server=" + textBox2.Text + 
					";Database=" + textBox3.Text + 
					";User Id=" + textBox4.Text + 
					";Password=" + textBox5.Text;
				MsSql_Connection.Open();
				MessageBox.Show("Проверка прошла успешно.");
				MsSql_Connection.Close();
				panel3.Visible = false;
			}catch(Exception ex){
				MessageBox.Show("Ошибка!!! Нет соединения с такой базой данных.");
				MsSql_Connection.Close();
				panel3.Visible = false;
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			CheckConnect(); // проверка соединения
		}
		
		/* Создать конфигурацию (создание базы данных на сервере */
		void CreateConfiguration()
		{
			panel3.Visible = true;
			this.Update();
			
			//Создание конфигурации
			CreateTablesInBase CreateTableInBase = new CreateTablesInBase();
			CreateTableInBase.Server = textBox2.Text;
			CreateTableInBase.DataBase = textBox3.Text;
			CreateTableInBase.UserID = textBox4.Text;
			CreateTableInBase.Pass = textBox5.Text;
			if(CreateTableInBase.CreateTables()){
				MessageBox.Show("Конфигурация успешно создана.", "Сообщение:");
				panel3.Visible = false;
			}else{
				MessageBox.Show("Ошибка!!! Конфигурация не создана.", "Сообщение:");
				panel3.Visible = false;
			}
		}
		void Button4Click(object sender, EventArgs e)
		{
			CreateConfiguration(); // создание конфигурации.
		}
		
		void FormEditListConnectLoad(object sender, EventArgs e)
		{
			panel1.Visible = true;
			panel2.Visible = true;
		}
	}
}
