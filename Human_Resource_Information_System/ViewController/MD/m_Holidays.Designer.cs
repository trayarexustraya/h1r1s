﻿namespace Human_Resource_Information_System
{
    partial class m_Holidays
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbcntrl_option = new System.Windows.Forms.TabControl();
            this.tpg_opt_1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbo_searchby = new System.Windows.Forms.ComboBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_additem = new System.Windows.Forms.Button();
            this.btn_delitem = new System.Windows.Forms.Button();
            this.btn_upditem = new System.Windows.Forms.Button();
            this.tpg_opt_2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbcntrl_main = new System.Windows.Forms.TabControl();
            this.tpg_list = new System.Windows.Forms.TabPage();
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.tpg_info = new System.Windows.Forms.TabPage();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_holiday_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_holiday = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_description = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tbcntrl_option.SuspendLayout();
            this.tpg_opt_1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tpg_opt_2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbcntrl_main.SuspendLayout();
            this.tpg_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.tpg_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.tbcntrl_option);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 505);
            this.panel1.TabIndex = 10;
            // 
            // tbcntrl_option
            // 
            this.tbcntrl_option.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tbcntrl_option.Controls.Add(this.tpg_opt_1);
            this.tbcntrl_option.Controls.Add(this.tpg_opt_2);
            this.tbcntrl_option.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_option.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_option.Name = "tbcntrl_option";
            this.tbcntrl_option.SelectedIndex = 0;
            this.tbcntrl_option.Size = new System.Drawing.Size(221, 505);
            this.tbcntrl_option.TabIndex = 49;
            this.tbcntrl_option.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_option_Selecting);
            // 
            // tpg_opt_1
            // 
            this.tpg_opt_1.Controls.Add(this.panel3);
            this.tpg_opt_1.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_1.Name = "tpg_opt_1";
            this.tpg_opt_1.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_1.Size = new System.Drawing.Size(213, 477);
            this.tpg_opt_1.TabIndex = 0;
            this.tpg_opt_1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 471);
            this.panel3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbo_searchby);
            this.groupBox2.Controls.Add(this.txt_search);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Location = new System.Drawing.Point(11, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 165);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Option";
            // 
            // cbo_searchby
            // 
            this.cbo_searchby.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_searchby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_searchby.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_searchby.FormattingEnabled = true;
            this.cbo_searchby.Location = new System.Drawing.Point(12, 33);
            this.cbo_searchby.Name = "cbo_searchby";
            this.cbo_searchby.Size = new System.Drawing.Size(160, 23);
            this.cbo_searchby.TabIndex = 10;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(12, 72);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(160, 21);
            this.txt_search.TabIndex = 9;
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_search.Location = new System.Drawing.Point(12, 100);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(160, 55);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "Search Now";
            this.btn_search.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_search.UseVisualStyleBackColor = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_print);
            this.groupBox7.Controls.Add(this.btn_additem);
            this.groupBox7.Controls.Add(this.btn_delitem);
            this.groupBox7.Controls.Add(this.btn_upditem);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox7.Location = new System.Drawing.Point(11, 13);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 273);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Main Option";
            // 
            // btn_print
            // 
            this.btn_print.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_print.Location = new System.Drawing.Point(12, 204);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(160, 55);
            this.btn_print.TabIndex = 9;
            this.btn_print.Text = "Print List";
            this.btn_print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_print.UseVisualStyleBackColor = false;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_additem
            // 
            this.btn_additem.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_additem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_additem.Location = new System.Drawing.Point(12, 21);
            this.btn_additem.Name = "btn_additem";
            this.btn_additem.Size = new System.Drawing.Size(160, 55);
            this.btn_additem.TabIndex = 8;
            this.btn_additem.Text = "Add New";
            this.btn_additem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_additem.UseVisualStyleBackColor = false;
            this.btn_additem.Click += new System.EventHandler(this.btn_additem_Click);
            // 
            // btn_delitem
            // 
            this.btn_delitem.BackColor = System.Drawing.Color.Maroon;
            this.btn_delitem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delitem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_delitem.Location = new System.Drawing.Point(12, 143);
            this.btn_delitem.Name = "btn_delitem";
            this.btn_delitem.Size = new System.Drawing.Size(160, 55);
            this.btn_delitem.TabIndex = 7;
            this.btn_delitem.Text = "Delete";
            this.btn_delitem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_delitem.UseVisualStyleBackColor = false;
            this.btn_delitem.Click += new System.EventHandler(this.btn_delitem_Click);
            // 
            // btn_upditem
            // 
            this.btn_upditem.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_upditem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_upditem.Location = new System.Drawing.Point(12, 82);
            this.btn_upditem.Name = "btn_upditem";
            this.btn_upditem.Size = new System.Drawing.Size(160, 55);
            this.btn_upditem.TabIndex = 1;
            this.btn_upditem.Text = "Update";
            this.btn_upditem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_upditem.UseVisualStyleBackColor = false;
            this.btn_upditem.Click += new System.EventHandler(this.btn_upditem_Click);
            // 
            // tpg_opt_2
            // 
            this.tpg_opt_2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tpg_opt_2.Controls.Add(this.panel4);
            this.tpg_opt_2.Location = new System.Drawing.Point(4, 4);
            this.tpg_opt_2.Name = "tpg_opt_2";
            this.tpg_opt_2.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_opt_2.Size = new System.Drawing.Size(213, 477);
            this.tpg_opt_2.TabIndex = 1;
            this.tpg_opt_2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SteelBlue;
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 471);
            this.panel4.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_save);
            this.groupBox3.Controls.Add(this.btn_back);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox3.Location = new System.Drawing.Point(10, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 130);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Option";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_save.Location = new System.Drawing.Point(12, 21);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(160, 46);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "Save";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Location = new System.Drawing.Point(12, 73);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(160, 46);
            this.btn_back.TabIndex = 1;
            this.btn_back.Text = "Back";
            this.btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbcntrl_main);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(221, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(519, 505);
            this.panel2.TabIndex = 11;
            // 
            // tbcntrl_main
            // 
            this.tbcntrl_main.Controls.Add(this.tpg_list);
            this.tbcntrl_main.Controls.Add(this.tpg_info);
            this.tbcntrl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcntrl_main.Location = new System.Drawing.Point(0, 0);
            this.tbcntrl_main.Name = "tbcntrl_main";
            this.tbcntrl_main.SelectedIndex = 0;
            this.tbcntrl_main.Size = new System.Drawing.Size(519, 505);
            this.tbcntrl_main.TabIndex = 7;
            this.tbcntrl_main.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbcntrl_main_Selecting);
            // 
            // tpg_list
            // 
            this.tpg_list.Controls.Add(this.dgv_list);
            this.tpg_list.Location = new System.Drawing.Point(4, 24);
            this.tpg_list.Name = "tpg_list";
            this.tpg_list.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_list.Size = new System.Drawing.Size(511, 477);
            this.tpg_list.TabIndex = 1;
            this.tpg_list.Text = "Holidays List";
            this.tpg_list.UseVisualStyleBackColor = true;
            // 
            // dgv_list
            // 
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.id,
            this.description,
            this.type,
            this.code});
            this.dgv_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_list.Location = new System.Drawing.Point(3, 3);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.ReadOnly = true;
            this.dgv_list.RowHeadersWidth = 25;
            this.dgv_list.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_list.Size = new System.Drawing.Size(505, 471);
            this.dgv_list.TabIndex = 1;
            // 
            // tpg_info
            // 
            this.tpg_info.BackColor = System.Drawing.SystemColors.Info;
            this.tpg_info.Controls.Add(this.txt_id);
            this.tpg_info.Controls.Add(this.label5);
            this.tpg_info.Controls.Add(this.label3);
            this.tpg_info.Controls.Add(this.cbo_holiday_type);
            this.tpg_info.Controls.Add(this.label2);
            this.tpg_info.Controls.Add(this.dtp_holiday);
            this.tpg_info.Controls.Add(this.label1);
            this.tpg_info.Controls.Add(this.label4);
            this.tpg_info.Controls.Add(this.txt_description);
            this.tpg_info.Controls.Add(this.label8);
            this.tpg_info.Controls.Add(this.label10);
            this.tpg_info.Location = new System.Drawing.Point(4, 24);
            this.tpg_info.Name = "tpg_info";
            this.tpg_info.Padding = new System.Windows.Forms.Padding(3);
            this.tpg_info.Size = new System.Drawing.Size(511, 477);
            this.tpg_info.TabIndex = 2;
            this.tpg_info.Text = "Holidays Info";
            this.tpg_info.Click += new System.EventHandler(this.tpg_info_Click);
            // 
            // txt_id
            // 
            this.txt_id.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_id.Location = new System.Drawing.Point(111, 8);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(62, 21);
            this.txt_id.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 67;
            this.label5.Text = "Code";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(335, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 66;
            this.label3.Text = "*";
            // 
            // cbo_holiday_type
            // 
            this.cbo_holiday_type.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cbo_holiday_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_holiday_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_holiday_type.FormattingEnabled = true;
            this.cbo_holiday_type.Location = new System.Drawing.Point(111, 87);
            this.cbo_holiday_type.Name = "cbo_holiday_type";
            this.cbo_holiday_type.Size = new System.Drawing.Size(193, 23);
            this.cbo_holiday_type.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "Holiday Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtp_holiday
            // 
            this.dtp_holiday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_holiday.Location = new System.Drawing.Point(111, 35);
            this.dtp_holiday.Name = "dtp_holiday";
            this.dtp_holiday.Size = new System.Drawing.Size(193, 21);
            this.dtp_holiday.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(335, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(335, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 46;
            this.label4.Text = "*";
            // 
            // txt_description
            // 
            this.txt_description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_description.Location = new System.Drawing.Point(111, 62);
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(193, 21);
            this.txt_description.TabIndex = 36;
            this.txt_description.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Date";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 15);
            this.label10.TabIndex = 33;
            this.label10.Text = "Description";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // date
            // 
            this.date.HeaderText = "DATE";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.HeaderText = "DESCRIPTION";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // type
            // 
            this.type.HeaderText = "HOLIDAY TYPE";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 120;
            // 
            // code
            // 
            this.code.HeaderText = "CODE";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Visible = false;
            // 
            // m_Holidays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(740, 505);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "m_Holidays";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Holidays";
            this.Load += new System.EventHandler(this.m_Holidays_Load);
            this.panel1.ResumeLayout(false);
            this.tbcntrl_option.ResumeLayout(false);
            this.tpg_opt_1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tpg_opt_2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tbcntrl_main.ResumeLayout(false);
            this.tpg_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.tpg_info.ResumeLayout(false);
            this.tpg_info.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbcntrl_option;
        private System.Windows.Forms.TabPage tpg_opt_1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbo_searchby;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_additem;
        private System.Windows.Forms.Button btn_delitem;
        private System.Windows.Forms.Button btn_upditem;
        private System.Windows.Forms.TabPage tpg_opt_2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tbcntrl_main;
        private System.Windows.Forms.TabPage tpg_list;
        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.TabPage tpg_info;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_description;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_holiday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_holiday_type;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;

    }
}