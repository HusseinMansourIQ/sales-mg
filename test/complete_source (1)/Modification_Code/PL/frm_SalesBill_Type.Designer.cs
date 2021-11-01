namespace Sales_Management_2.PL
{
    partial class frm_SalesBill_Type
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
            this.rbn_NoName = new System.Windows.Forms.RadioButton();
            this.rdbn_NameFromDataBase = new System.Windows.Forms.RadioButton();
            this.rdbn_NoSave = new System.Windows.Forms.RadioButton();
            this.rdbn_Jumla = new System.Windows.Forms.RadioButton();
            this.rdbn_Mufrad = new System.Windows.Forms.RadioButton();
            this.txt_NoSave = new System.Windows.Forms.TextBox();
            this.txt_NameFromDataBase = new System.Windows.Forms.TextBox();
            this.btn_continue = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbn_NoName
            // 
            this.rbn_NoName.AutoSize = true;
            this.rbn_NoName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbn_NoName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbn_NoName.Location = new System.Drawing.Point(619, 2);
            this.rbn_NoName.Margin = new System.Windows.Forms.Padding(2);
            this.rbn_NoName.Name = "rbn_NoName";
            this.rbn_NoName.Size = new System.Drawing.Size(114, 34);
            this.rbn_NoName.TabIndex = 2;
            this.rbn_NoName.TabStop = true;
            this.rbn_NoName.Text = "بيع قائمة بدون اسم";
            this.rbn_NoName.UseVisualStyleBackColor = true;
            this.rbn_NoName.CheckedChanged += new System.EventHandler(this.rbn_NoName_CheckedChanged);
            // 
            // rdbn_NameFromDataBase
            // 
            this.rdbn_NameFromDataBase.AutoSize = true;
            this.rdbn_NameFromDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbn_NameFromDataBase.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbn_NameFromDataBase.Location = new System.Drawing.Point(619, 2);
            this.rdbn_NameFromDataBase.Margin = new System.Windows.Forms.Padding(2);
            this.rdbn_NameFromDataBase.Name = "rdbn_NameFromDataBase";
            this.rdbn_NameFromDataBase.Size = new System.Drawing.Size(114, 34);
            this.rdbn_NameFromDataBase.TabIndex = 1;
            this.rdbn_NameFromDataBase.TabStop = true;
            this.rdbn_NameFromDataBase.Text = "بيع الى زبون محفوظ";
            this.rdbn_NameFromDataBase.UseVisualStyleBackColor = true;
            this.rdbn_NameFromDataBase.CheckedChanged += new System.EventHandler(this.rdbn_NameFromDataBase_CheckedChanged);
            // 
            // rdbn_NoSave
            // 
            this.rdbn_NoSave.AutoSize = true;
            this.rdbn_NoSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbn_NoSave.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbn_NoSave.Location = new System.Drawing.Point(619, 2);
            this.rdbn_NoSave.Margin = new System.Windows.Forms.Padding(2);
            this.rdbn_NoSave.Name = "rdbn_NoSave";
            this.rdbn_NoSave.Size = new System.Drawing.Size(114, 34);
            this.rdbn_NoSave.TabIndex = 2;
            this.rdbn_NoSave.TabStop = true;
            this.rdbn_NoSave.Text = "بيع مباشر ";
            this.rdbn_NoSave.UseVisualStyleBackColor = true;
            this.rdbn_NoSave.CheckedChanged += new System.EventHandler(this.rdbn_NoSave_CheckedChanged);
            // 
            // rdbn_Jumla
            // 
            this.rdbn_Jumla.AutoSize = true;
            this.rdbn_Jumla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbn_Jumla.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbn_Jumla.Location = new System.Drawing.Point(242, 2);
            this.rdbn_Jumla.Margin = new System.Windows.Forms.Padding(2);
            this.rdbn_Jumla.Name = "rdbn_Jumla";
            this.rdbn_Jumla.Size = new System.Drawing.Size(155, 20);
            this.rdbn_Jumla.TabIndex = 1;
            this.rdbn_Jumla.TabStop = true;
            this.rdbn_Jumla.Text = "جملة";
            this.rdbn_Jumla.UseVisualStyleBackColor = true;
            this.rdbn_Jumla.CheckedChanged += new System.EventHandler(this.rdbn_Jumla_CheckedChanged);
            // 
            // rdbn_Mufrad
            // 
            this.rdbn_Mufrad.AutoSize = true;
            this.rdbn_Mufrad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbn_Mufrad.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbn_Mufrad.Location = new System.Drawing.Point(401, 2);
            this.rdbn_Mufrad.Margin = new System.Windows.Forms.Padding(2);
            this.rdbn_Mufrad.Name = "rdbn_Mufrad";
            this.rdbn_Mufrad.Size = new System.Drawing.Size(155, 20);
            this.rdbn_Mufrad.TabIndex = 0;
            this.rdbn_Mufrad.TabStop = true;
            this.rdbn_Mufrad.Text = "مفرد";
            this.rdbn_Mufrad.UseVisualStyleBackColor = true;
            this.rdbn_Mufrad.CheckedChanged += new System.EventHandler(this.rdbn_Mufrad_CheckedChanged);
            // 
            // txt_NoSave
            // 
            this.txt_NoSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_NoSave.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NoSave.Location = new System.Drawing.Point(398, 2);
            this.txt_NoSave.Margin = new System.Windows.Forms.Padding(2);
            this.txt_NoSave.Name = "txt_NoSave";
            this.txt_NoSave.Size = new System.Drawing.Size(154, 26);
            this.txt_NoSave.TabIndex = 3;
            this.txt_NoSave.TextChanged += new System.EventHandler(this.txt_NoSave_TextChanged);
            this.txt_NoSave.Enter += new System.EventHandler(this.txt_NoSave_Enter);
            this.txt_NoSave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_NoSave_KeyPress);
            this.txt_NoSave.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_NoSave_KeyUp);
            // 
            // txt_NameFromDataBase
            // 
            this.txt_NameFromDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_NameFromDataBase.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NameFromDataBase.Location = new System.Drawing.Point(398, 2);
            this.txt_NameFromDataBase.Margin = new System.Windows.Forms.Padding(2);
            this.txt_NameFromDataBase.Name = "txt_NameFromDataBase";
            this.txt_NameFromDataBase.Size = new System.Drawing.Size(154, 26);
            this.txt_NameFromDataBase.TabIndex = 4;
            this.txt_NameFromDataBase.EnabledChanged += new System.EventHandler(this.txt_NameFromDataBase_EnabledChanged);
            this.txt_NameFromDataBase.TextChanged += new System.EventHandler(this.txt_NameFromDataBase_TextChanged);
            this.txt_NameFromDataBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_NameFromDataBase_KeyDown);
            // 
            // btn_continue
            // 
            this.btn_continue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_continue.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btn_continue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_continue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_continue.Location = new System.Drawing.Point(500, 2);
            this.btn_continue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_continue.Name = "btn_continue";
            this.btn_continue.Size = new System.Drawing.Size(95, 35);
            this.btn_continue.TabIndex = 5;
            this.btn_continue.Text = "متابعة";
            this.btn_continue.UseVisualStyleBackColor = true;
            this.btn_continue.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(202, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 35);
            this.button2.TabIndex = 6;
            this.button2.Text = "رجوع";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(319, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 38);
            this.label1.TabIndex = 9;
            this.label1.Text = "سوف يتم حفظ وطباعة القائمة بدون اسم";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 38);
            this.label2.TabIndex = 10;
            this.label2.Text = "سوف يتم حفظ وطباعة القائمة باسم الزبون ولا يتم ادراجه في قائمة الزبائن الدائميين";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 38);
            this.label3.TabIndex = 11;
            this.label3.Text = "سوف يتم اختيار الزبون من قائمة الزبائن الدائميين";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.407407F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.81481F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(802, 409);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.rdbn_Mufrad, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rdbn_Jumla, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 48);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(796, 24);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 78);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(796, 221);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbn_NoName, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(790, 38);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel5.Controls.Add(this.rdbn_NoSave, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.txt_NoSave, 3, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 95);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(790, 38);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 7;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel6.Controls.Add(this.rdbn_NameFromDataBase, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.txt_NameFromDataBase, 3, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 163);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(790, 38);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 5;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Controls.Add(this.button2, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.btn_continue, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 305);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(796, 39);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // frm_SalesBill_Type
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(802, 409);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_SalesBill_Type";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "واجهة اختيار الزبون";
            this.Load += new System.EventHandler(this.frm_SalesBill_Type_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbn_NoName;
        private System.Windows.Forms.RadioButton rdbn_NameFromDataBase;
        private System.Windows.Forms.RadioButton rdbn_NoSave;
        private System.Windows.Forms.RadioButton rdbn_Jumla;
        private System.Windows.Forms.RadioButton rdbn_Mufrad;
        private System.Windows.Forms.TextBox txt_NoSave;
        private System.Windows.Forms.TextBox txt_NameFromDataBase;
        private System.Windows.Forms.Button btn_continue;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    }
}