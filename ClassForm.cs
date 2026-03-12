using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    public partial class ClassForm : Form
    {
        // Danh sách gốc để lưu trữ dữ liệu lớp học
        private List<ListViewItem> allClasses = new List<ListViewItem>();

        public ClassForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Lớp Học";

            // Đấu dây sự kiện Click cho các nút (nếu bạn chưa làm trên Design)
            this.btnAdd.Click += btnAdd_Click;
            this.btnEdit.Click += btnEdit_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.btnSearch.Click += btnSearch_Click;
            this.btnExit.Click += btnExit_Click;
            this.lsvLopHoc.SelectedIndexChanged += lsvLopHoc_SelectedIndexChanged;
        }

        private void ClearInput()
        {
            txtLop.Clear();
            txtMaLop.Clear();
            txtKhoa.Clear();
            txtSearch.Clear();
            txtLop.Focus(); // Ưu tiên con trỏ vào ô Tên lớp trước
        }

        // 1. Chức năng THÊM
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLop.Text) || string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên lớp và Mã lớp!", "Thông báo");
                return;
            }

            // Theo UI của bạn: Cột 1 là Lớp, Cột 2 là Mã lớp, Cột 3 là Khoa
            ListViewItem item = new ListViewItem(txtLop.Text); // Cột đầu tiên (Index 0)
            item.SubItems.Add(txtMaLop.Text);                 // Cột thứ hai (Index 1)
            item.SubItems.Add(txtKhoa.Text);                  // Cột thứ ba (Index 2)

            lsvLopHoc.Items.Add(item);
            allClasses.Add((ListViewItem)item.Clone()); // Lưu vào danh sách gốc để tìm kiếm
            ClearInput();
        }

        // 2. Chức năng SỬA
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count > 0)
            {
                ListViewItem itemUI = lsvLopHoc.SelectedItems[0];
                // Dùng Mã lớp cũ để định danh trong danh sách gốc (Cột index 1)
                string maLopCu = itemUI.SubItems[1].Text;

                // Cập nhật giao diện
                itemUI.Text = txtLop.Text;
                itemUI.SubItems[1].Text = txtMaLop.Text;
                itemUI.SubItems[2].Text = txtKhoa.Text;

                // Cập nhật danh sách gốc
                var classInList = allClasses.FirstOrDefault(c => c.SubItems[1].Text == maLopCu);
                if (classInList != null)
                {
                    classInList.Text = txtLop.Text;
                    classInList.SubItems[1].Text = txtMaLop.Text;
                    classInList.SubItems[2].Text = txtKhoa.Text;
                }

                MessageBox.Show("Cập nhật thông tin lớp học thành công!");
                ClearInput();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa!");
            }
        }

        // 3. Chức năng XÓA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    ListViewItem selectedItem = lsvLopHoc.SelectedItems[0];
                    string maLopCanXoa = selectedItem.SubItems[1].Text;

                    lsvLopHoc.Items.Remove(selectedItem);
                    allClasses.RemoveAll(c => c.SubItems[1].Text == maLopCanXoa);

                    MessageBox.Show("Đã xóa lớp học thành công!");
                    ClearInput();
                }
            }
        }

        // 4. Chức năng TÌM KIẾM
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string searchType = cboSearchType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(keyword))
            {
                RefreshListView(allClasses);
                return;
            }

            lsvLopHoc.Items.Clear();

            foreach (ListViewItem item in allClasses)
            {
                bool isMatch = false;
                if (searchType == "Lớp")
                {
                    // Mã lớp: Khớp hoàn toàn (Cột 2 - Index 1)
                    isMatch = item.SubItems[1].Text.ToLower().Equals(keyword);
                }
                else if (searchType == "Mã lớp")
                {
                    // Tên lớp: Chứa một phần (Cột 1 - Index 0)
                    isMatch = item.Text.ToLower().Contains(keyword);
                }
                else if (searchType == "Khoa")
                {
                    // Khoa: Khớp hoàn toàn (Cột 3 - Index 2)
                    isMatch = item.SubItems[2].Text.ToLower().Equals(keyword);
                }

                if (isMatch)
                {
                    lsvLopHoc.Items.Add((ListViewItem)item.Clone());
                }
            }
        }

        private void RefreshListView(List<ListViewItem> source)
        {
            lsvLopHoc.Items.Clear();
            foreach (var item in source)
            {
                lsvLopHoc.Items.Add((ListViewItem)item.Clone());
            }
        }

        // Sự kiện đổ ngược dữ liệu khi click vào ListView
        private void lsvLopHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvLopHoc.SelectedItems[0];
                txtLop.Text = item.Text;               // Cột Lớp
                txtMaLop.Text = item.SubItems[1].Text; // Cột Mã lớp
                txtKhoa.Text = item.SubItems[2].Text;  // Cột Khoa
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}