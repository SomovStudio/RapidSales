/*
 * Сделано в SharpDevelop.
 * Пользователь: Catfish
 * Дата: 18.03.2014
 * Время: 14:04
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Rapid
{
	partial class FormAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(354, 272);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.linkLabel2);
			this.tabPage1.Controls.Add(this.linkLabel1);
			this.tabPage1.Controls.Add(this.label9);
			this.tabPage1.Controls.Add(this.label8);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(346, 246);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Автор";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// linkLabel2
			// 
			this.linkLabel2.Location = new System.Drawing.Point(52, 220);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(217, 23);
			this.linkLabel2.TabIndex = 15;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "https://somovstudio.github.io";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(52, 201);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(217, 23);
			this.linkLabel1.TabIndex = 14;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "somov.studio@gmail.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(9, 220);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(260, 19);
			this.label9.TabIndex = 13;
			this.label9.Text = "Сайт:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(9, 201);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(260, 19);
			this.label8.TabIndex = 12;
			this.label8.Text = "Почта:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9, 155);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(334, 23);
			this.label6.TabIndex = 5;
			this.label6.Text = "Дата: 13.12.2018";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(9, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(276, 23);
			this.label4.TabIndex = 3;
			this.label4.Text = "Лиценция: GNU";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(9, 109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(320, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Разработчик: Somov Studio";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(191, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Версия программы: 1.0";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(300, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование программы:  Rapid";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.richTextBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(346, 246);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Лицензия GNU";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(3, 6);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(337, 234);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// label5
			// 
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label5.Location = new System.Drawing.Point(9, 3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(331, 46);
			this.label5.TabIndex = 16;
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(378, 296);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormAbout";
			this.Text = "О программе.";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
	}
}
