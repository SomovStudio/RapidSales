/*
 * Сделано в SharpDevelop.
 * Пользователь: Сомов Евгений Павлович
 * Дата: 12.09.2013
 * Время: 14:24
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Rapid
{
	/// <summary>
	/// Description of FormSelectUser.
	/// </summary>
	public partial class FormSelectUser : Form
	{
		public FormSelectUser()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		//Глобальные переменные =================================
		public bool ProgramClose = true;//флаг закрытия приложения
		//public MySqlConnection MySql_Connection = new MySqlConnection();
		//public MySqlCommand MySql_CommandSelect;
		//public MySqlDataAdapter MySql_DataAdapter = new MySqlDataAdapter();
		//public System.Data.DataSet MySql_DataSet = new System.Data.DataSet();
		//public System.Data.DataTable MySql_DataTable = new System.Data.DataTable();
		
		public SqlConnection MsSql_Connection = new SqlConnection();
		public SqlCommand MsSql_CommandSelect;
		public SqlDataAdapter MsSql_DataAdapter= new SqlDataAdapter();
		public DataSet MsSql_DataSet = new DataSet();
		public DataTable MsSql_DataTable = new DataTable();
		
		//=======================================================
		
		//Глобальные процедуры и функции ========================
		public void Connect(){
			//Подключение к базе данных
			try{
				MsSql_Connection.ConnectionString = "Server=" + ClassConfig.Rapid_Run_Server + ";Database=" + ClassConfig.Rapid_Run_DataBase + ";User Id=" + ClassConfig.Rapid_Run_Uid + ";Password=" + ClassConfig.Rapid_Run_Pwd;
				MsSql_Connection.Open();
				//Создание таблицы
				MsSql_DataTable.Clear();
				MsSql_DataTable.CaseSensitive = false;
				MsSql_DataTable.Columns.Add("user_name", Type.GetType("System.String"));
				MsSql_DataTable.Columns.Add("user_pass", Type.GetType("System.String"));
				MsSql_DataTable.Columns.Add("user_right", Type.GetType("System.String"));	
				//Вставка таблицы в DataSet
				MsSql_DataSet.Clear();
				MsSql_DataSet.Tables.Add(MsSql_DataTable);
				MsSql_DataSet.DataSetName = "users";
				
				MsSql_CommandSelect = new SqlCommand("SELECT * FROM users", MsSql_Connection);
				MsSql_DataAdapter.SelectCommand = MsSql_CommandSelect;
				MsSql_DataAdapter.Fill(MsSql_DataSet, "users");
				
				MsSql_Connection.Close();
				
				comboBox1.Items.Clear();
				foreach(DataTable table in MsSql_DataSet.Tables)
    			{
					foreach(DataRow row in table.Rows)
        			{
						comboBox1.Items.Add(row["user_name"].ToString());
					}
   			 	}
				
			}catch(Exception ex){
				if(MessageBox.Show("Конфигурация не найдена. Показать сообщение об ошибке?", "Сообщение:", MessageBoxButtons.YesNo) == DialogResult.Yes)
					MessageBox.Show(ex.ToString());
				MsSql_Connection.Close();
			}
		}
		
		
		void Button2Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void FormSelectUserLoad(object sender, EventArgs e)
		{
			Connect();	//Соединение с выбранной базой данных (MySQL)
		}
		
		void FormSelectUserClosed(object sender, EventArgs e){
			if(ProgramClose)
				Application.Exit();
		}
		
		/* Проверка учётной записи и вход в систему */
		void CheckUser()
		{
			//Проверка логина и пароля
			try{
			if(comboBox1.Text != "" && comboBox1.Text != "admin"){
				String Login = MsSql_DataSet.Tables["users"].Rows[comboBox1.SelectedIndex]["user_name"].ToString();
				String Pass = MsSql_DataSet.Tables["users"].Rows[comboBox1.SelectedIndex]["user_pass"].ToString();
				String Right = MsSql_DataSet.Tables["users"].Rows[comboBox1.SelectedIndex]["user_right"].ToString();
				if(Login == comboBox1.Text && Pass == textBox1.Text){
					if(ClassConfig.Rapid_Run_Type == "Клиент"){
						ClassConfig.Rapid_Client_UserName = Login; // имя пользователя клиентом
						ClassConfig.Rapid_Client_UserRight = Right; // права пользователя клиентом
						//Открываем главную форму клиента
						ClassForms.Rapid_Client = new FormClient();
						ClassForms.Rapid_Client.Show();
						ProgramClose = false;
						this.Close();	//закрываем окно ввода логина и пароля.
					}
					if(ClassConfig.Rapid_Run_Type == "Администратор"){ //права администратора
						if (Right == "admin"){
						ClassForms.Rapid_Administrator = new FormAdministrator();
						ClassForms.Rapid_Administrator.Show();
						ClassConfig.Rapid_Run_UserName = comboBox1.Text; // АДМИНИСТРАТОР
						ProgramClose = false;
						this.Close();	//закрываем окно ввода логина и пароля.
						}else
							MessageBox.Show("Недостаточно прав.","Сообщение:");
					}
				}else{
					MessageBox.Show("Вы ввели не верный пароль или логин!","Сообщение:");
				}
			}else{
				if(comboBox1.Text == "admin" && textBox1.Text == "12345" && ClassConfig.Rapid_Run_Type == "Администратор"){	//АДМИНИСТРАТОР
					ClassForms.Rapid_Administrator = new FormAdministrator();
					ClassForms.Rapid_Administrator.Show();
					ClassConfig.Rapid_Run_UserName = comboBox1.Text; // имя пользователя
					ProgramClose = false;
					this.Close();	//закрываем окно ввода логина и пароля.
				}else{
					if(ClassConfig.Rapid_Run_Type == "Администратор"){
						MessageBox.Show("Вы ввели не верный логин и пароль адвинистратора.","Сообщение:");
					}else
						MessageBox.Show("Вы не выбрали пользователя","Сообщение:");
				}
			}
			}catch{
				MessageBox.Show("Ошибка ввода логина и пароля.","Сообщение:");
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			CheckUser(); // проверка пользователя и вход
		}
	}
}
