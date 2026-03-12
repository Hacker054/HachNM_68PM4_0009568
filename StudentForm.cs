using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    public partial class StudentForm : Form
    {
        // Danh sách gốc để lưu trữ toàn bộ dữ liệu sinh viên (phục vụ tìm kiếm)
        private List<ListViewItem> allStudents = new List<ListViewItem>();

        public StudentForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Sinh Viên";

            // Thiết lập mặc định cho giới tính
            if (rdbNam != null) rdbNam.Checked = true;
        }

        // Hàm xóa trắng ô nhập liệu và reset các control
        private void ClearInput()
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            txtLop.Clear();
            txtSearch.Clear();
            rdbNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now;
            txtMaSV.Focus();
        }

        // 1. Chức năng THÊM
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên sinh viên!");
                return;
            }

            // Lấy giá trị từ các control mới
            string gioiTinh = rdbNam.Checked ? "Nam" : "Nữ";
            string ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");

            ListViewItem item = new ListViewItem(txtTenSV.Text); // Cột 0: Tên
            item.SubItems.Add(txtMaSV.Text);                   // Cột 1: Mã SV
            item.SubItems.Add(txtLop.Text);                     // Cột 2: Lớp
            item.SubItems.Add(gioiTinh);                        // Cột 3: Giới tính
            item.SubItems.Add(ngaySinh);                        // Cột 4: Ngày sinh

            lsvSinhVien.Items.Add(item);
            allStudents.Add((ListViewItem)item.Clone()); // Lưu bản sao vào danh sách gốc

            ClearInput();
        }

        // 2. Chức năng SỬA
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem itemUI = lsvSinhVien.SelectedItems[0];
                string maSVHienTai = itemUI.Text;

                // Cập nhật thông tin trên giao diện (UI)
                itemUI.Text = txtMaSV.Text;
                itemUI.SubItems[1].Text = txtTenSV.Text;
                itemUI.SubItems[2].Text = txtLop.Text;
                itemUI.SubItems[3].Text = rdbNam.Checked ? "Nam" : "Nữ";
                itemUI.SubItems[4].Text = dtpNgaySinh.Value.ToString("dd/MM/yyyy");

                // Cập nhật thông tin trong danh sách gốc (allStudents)
                var studentInList = allStudents.FirstOrDefault(s => s.Text == maSVHienTai);
                if (studentInList != null)
                {
                    studentInList.Text = txtMaSV.Text;
                    studentInList.SubItems[1].Text = txtTenSV.Text;
                    studentInList.SubItems[2].Text = txtLop.Text;
                    studentInList.SubItems[3].Text = rdbNam.Checked ? "Nam" : "Nữ";
                    studentInList.SubItems[4].Text = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
                }

                MessageBox.Show("Cập nhật thành công!");
                ClearInput();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên trong danh sách để sửa!");
            }
        }

        // 3. Chức năng XÓA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    ListViewItem selectedItem = lsvSinhVien.SelectedItems[0];
                    string maSVCanXoa = selectedItem.Text;

                    lsvSinhVien.Items.Remove(selectedItem);
                    allStudents.RemoveAll(s => s.Text == maSVCanXoa);

                    MessageBox.Show("Đã xóa dữ liệu thành công!");
                    ClearInput();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
            }
        }

        // 4. Chức năng TÌM KIẾM
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string searchType = cboSearchType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(keyword))
            {
                RefreshListView(allStudents);
                return;
            }

            lsvSinhVien.Items.Clear();

            foreach (ListViewItem item in allStudents)
            {
                bool isMatch = false;

                if (searchType == "Mã SV")
                    isMatch = item.Text.ToLower().Equals(keyword);
                else if (searchType == "Họ và tên")
                    isMatch = item.SubItems[1].Text.ToLower().Contains(keyword);
                else if (searchType == "Lớp")
                    isMatch = item.SubItems[2].Text.ToLower().Equals(keyword);

                if (isMatch)
                {
                    lsvSinhVien.Items.Add((ListViewItem)item.Clone());
                }
            }

            if (lsvSinhVien.Items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!");
            }
        }

        private void RefreshListView(List<ListViewItem> source)
        {
            lsvSinhVien.Items.Clear();
            foreach (var item in source)
            {
                lsvSinhVien.Items.Add((ListViewItem)item.Clone());
            }
        }

        // Sự kiện khi click vào một dòng trong ListView thì hiện ngược lên các Textbox/Control
        private void lsvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvSinhVien.SelectedItems[0];
                txtMaSV.Text = item.Text;
                txtTenSV.Text = item.SubItems[1].Text;
                txtLop.Text = item.SubItems[2].Text;

                // Hiển thị lại Giới tính
                if (item.SubItems[3].Text == "Nam")
                    rdbNam.Checked = true;
                else
                    rdbNu.Checked = true;

                // Hiển thị lại Ngày sinh
                DateTime dt;
                if (DateTime.TryParseExact(item.SubItems[4].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    dtpNgaySinh.Value = dt;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Các sự kiện trống (có thể xóa nếu không dùng)
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}