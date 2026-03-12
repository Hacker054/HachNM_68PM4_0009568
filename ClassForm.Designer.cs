using System;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    partial class ClassForm
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
            this.txtLop = new System.Windows.Forms.TextBox();
            this.txtKhoa = new System.Windows.Forms.TextBox();
            this.txtMaLop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lsvLopHoc = new System.Windows.Forms.ListView();
            this.MaLop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Khoa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLop
            // 
            this.txtLop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLop.Location = new System.Drawing.Point(70, 251);
            this.txtLop.Margin = new System.Windows.Forms.Padding(4);
            this.txtLop.Name = "txtLop";
            this.txtLop.Size = new System.Drawing.Size(306, 27);
            this.txtLop.TabIndex = 0;
            // 
            // txtKhoa
            // 
            this.txtKhoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtKhoa.Location = new System.Drawing.Point(70, 425);
            this.txtKhoa.Margin = new System.Windows.Forms.Padding(4);
            this.txtKhoa.Name = "txtKhoa";
            this.txtKhoa.Size = new System.Drawing.Size(306, 27);
            this.txtKhoa.TabIndex = 1;
            // 
            // txtMaLop
            // 
            this.txtMaLop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMaLop.Location = new System.Drawing.Point(70, 338);
            this.txtMaLop.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaLop.Name = "txtMaLop";
            this.txtMaLop.Size = new System.Drawing.Size(306, 27);
            this.txtMaLop.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Location = new System.Drawing.Point(66, 222);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Lớp:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.Location = new System.Drawing.Point(66, 309);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mã lớp:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.Location = new System.Drawing.Point(66, 396);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Khoa:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.BackColor = System.Drawing.Color.Yellow;
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAdd.Location = new System.Drawing.Point(70, 483);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 56);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.BackColor = System.Drawing.Color.Blue;
            this.btnEdit.Location = new System.Drawing.Point(241, 483);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(135, 56);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.BackColor = System.Drawing.Color.Lime;
            this.btnDelete.Location = new System.Drawing.Point(70, 557);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 56);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(241, 557);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 56);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lsvLopHoc
            // 
            this.lsvLopHoc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lsvLopHoc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MaLop,
            this.Lop,
            this.Khoa});
            this.lsvLopHoc.FullRowSelect = true;
            this.lsvLopHoc.GridLines = true;
            this.lsvLopHoc.HideSelection = false;
            this.lsvLopHoc.Location = new System.Drawing.Point(456, 156);
            this.lsvLopHoc.Name = "lsvLopHoc";
            this.lsvLopHoc.Size = new System.Drawing.Size(746, 457);
            this.lsvLopHoc.TabIndex = 11;
            this.lsvLopHoc.UseCompatibleStateImageBehavior = false;
            this.lsvLopHoc.View = System.Windows.Forms.View.Details;
            this.lsvLopHoc.SelectedIndexChanged += new System.EventHandler(this.lsvLopHoc_SelectedIndexChanged);
            // 
            // MaLop
            // 
            this.MaLop.Text = "Mã lớp";
            this.MaLop.Width = 365;
            // 
            // Lop
            // 
            this.Lop.Text = "Lớp";
            this.Lop.Width = 186;
            // 
            // Khoa
            // 
            this.Khoa.Text = "Khoa";
            this.Khoa.Width = 319;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSearch.Location = new System.Drawing.Point(456, 107);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(474, 27);
            this.txtSearch.TabIndex = 12;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Location = new System.Drawing.Point(1107, 107);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 27);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(450, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 45);
            this.label4.TabIndex = 14;
            this.label4.Text = "Danh sách lớp học";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::QuanLySinhVienVaLopHoc.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(139, 91);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // cboSearchType
            // 
            this.cboSearchType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchType.FormattingEnabled = true;
            this.cboSearchType.Items.AddRange(new object[] {
            "Lớp",
            "Mã lớp",
            "Khoa"});
            this.cboSearchType.Location = new System.Drawing.Point(950, 106);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(140, 28);
            this.cboSearchType.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(81, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(272, 37);
            this.label5.TabIndex = 17;
            this.label5.Text = "Quản Lý Lớp Học";
            // 
            // ClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 651);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboSearchType);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lsvLopHoc);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMaLop);
            this.Controls.Add(this.txtKhoa);
            this.Controls.Add(this.txtLop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1262, 698);
            this.Name = "ClassForm";
            this.Text = "ClassManagement";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtLop;
        private TextBox txtKhoa;
        private TextBox txtMaLop;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnExit;
        private ListView lsvLopHoc;
        private TextBox txtSearch;
        private Button btnSearch;
        private Label label4;
        private ColumnHeader MaLop;
        private ColumnHeader Lop;
        private ColumnHeader Khoa;
        private PictureBox pictureBox1;
        private ComboBox cboSearchType;
        private Label label5;
    }
}