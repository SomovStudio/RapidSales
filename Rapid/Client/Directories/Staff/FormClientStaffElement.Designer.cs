﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 24.02.2014
 * Время: 9:49
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Rapid
{
	partial class FormClientStaffElement
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClientStaffElement));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(97, 6);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(370, 20);
			this.textBox1.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Наименование:";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(392, 278);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 9;
			this.button2.Text = "Отмена.";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(311, 278);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "ОК.";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(12, 35);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(455, 237);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button4);
			this.tabPage1.Controls.Add(this.button3);
			this.tabPage1.Controls.Add(this.textBox4);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.dateTimePicker2);
			this.tabPage1.Controls.Add(this.checkBox1);
			this.tabPage1.Controls.Add(this.dateTimePicker1);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.textBox3);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.textBox2);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(447, 211);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Основные данные.";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(416, 171);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(25, 23);
			this.button4.TabIndex = 11;
			this.button4.Text = "Х";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(389, 171);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(25, 23);
			this.button3.TabIndex = 10;
			this.button3.Text = "...";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// textBox4
			// 
			this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox4.Location = new System.Drawing.Point(67, 173);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(320, 20);
			this.textBox4.TabIndex = 9;
			this.textBox4.Text = "0.00";
			this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBox4.TextChanged += new System.EventHandler(this.TextBox4TextChanged);
			this.textBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox4KeyDown);
			this.textBox4.LostFocus += new System.EventHandler(this.TextBox4TextLostFocus);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "Зарплата:";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker2.Enabled = false;
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker2.Location = new System.Drawing.Point(276, 142);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(94, 20);
			this.dateTimePicker2.TabIndex = 7;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(214, 140);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(74, 24);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Уволен:";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(102, 142);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(94, 20);
			this.dateTimePicker1.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Принят на работу:";
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Location = new System.Drawing.Point(6, 85);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(435, 49);
			this.textBox3.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Адрес и телефон:";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(6, 26);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(435, 42);
			this.textBox2.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Реквизиты:";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.textBox5);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(447, 211);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Дополнительно.";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// textBox5
			// 
			this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox5.Location = new System.Drawing.Point(6, 26);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(435, 179);
			this.textBox5.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(3, 12);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(188, 23);
			this.label6.TabIndex = 0;
			this.label6.Text = "Дополнительная информация:";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.comboBox1);
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(447, 211);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Разное.";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(122, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(319, 21);
			this.comboBox1.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 15);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(125, 23);
			this.label7.TabIndex = 0;
			this.label7.Text = "Родительская папка:";
			// 
			// FormClientStaffElement
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 313);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormClientStaffElement";
			this.Text = "Элемент";
			this.Closed += new System.EventHandler(this.FormClientStaffElementClosed);
			this.Load += new System.EventHandler(this.FormClientStaffElementLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		public System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
	}
}
