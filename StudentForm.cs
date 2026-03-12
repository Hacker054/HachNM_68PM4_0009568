using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    public partial class StudentForm : Form
    {
        // Thêm dòng này vào ngay dưới phần khai báo class
        private List<ListViewItem> allStudents = new List<ListViewItem>();
        public StudentForm()
        {
            InitializeComponent();
            // Căn giữa màn hình khi mở
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Sinh Viên";
        }
        private void ClearInput()
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            txtLop.Clear();
            txtSearch.Clear(); // Reset luôn cả ô tìm kiếm nếu bạn muốn
            txtMaSV.Focus();   // Đưa con trỏ chuột về ô đầu tiên để tiện nhập tiếp
        }
        // 1. Chức năng THÊM (Cập nhật để lưu vào list gốc)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên sinh viên!");
                return;
            }

            ListViewItem item = new ListViewItem(txtMaSV.Text);
            item.SubItems.Add(txtTenSV.Text);
            item.SubItems.Add(txtLop.Text);

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

                // 1. Cập nhật trên giao diện (UI)
                itemUI.Text = txtMaSV.Text;
                itemUI.SubItems[1].Text = txtTenSV.Text;
                itemUI.SubItems[2].Text = txtLop.Text;

                // 2. Cập nhật trong danh sách gốc (allStudents)
                var studentInList = allStudents.FirstOrDefault(s => s.Text == maSVHienTai);
                if (studentInList != null)
                {
                    studentInList.Text = txtMaSV.Text;
                    studentInList.SubItems[1].Text = txtTenSV.Text;
                    studentInList.SubItems[2].Text = txtLop.Text;
                }

                MessageBox.Show("Cập nhật thành công!");
                ClearInput();
            }
        }

        // 3. Chức năng XÓA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    // 1. Lấy ra Item đang được chọn
                    ListViewItem selectedItem = lsvSinhVien.SelectedItems[0];
                    string maSVCanXoa = selectedItem.Text; // Giả sử cột 0 là Mã SV

                    // 2. Xóa khỏi hiển thị trên ListView
                    lsvSinhVien.Items.Remove(selectedItem);

                    // 3. QUAN TRỌNG: Xóa khỏi danh sách gốc (allStudents)
                    // Tìm sinh viên trong list gốc có Mã SV trùng với dòng vừa chọn để xóa
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

        // 4. Chức năng TÌM KIẾM & LỌC DỮ LIỆU
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string searchType = cboSearchType.SelectedItem?.ToString(); // Lấy kiểu tìm kiếm đã chọn

            // Nếu để trống ô tìm kiếm, hiển thị lại toàn bộ
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
                {
                    // Mã SV: Phải khớp hoàn toàn
                    isMatch = item.Text.ToLower().Equals(keyword);
                }
                else if (searchType == "Họ và tên")
                {
                    // Tên: Chứa một phần
                    isMatch = item.SubItems[1].Text.ToLower().Contains(keyword);
                }
                else if (searchType == "Lớp")
                {
                    // Lớp: Phải khớp hoàn toàn
                    isMatch = item.SubItems[2].Text.ToLower().Equals(keyword);
                }

                if (isMatch)
                {
                    lsvSinhVien.Items.Add((ListViewItem)item.Clone());
                }
            }

            if (lsvSinhVien.Items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp với " + searchType + " này!");
            }
        }

        // Hàm hỗ trợ nạp lại danh sách
        private void RefreshListView(List<ListViewItem> source)
        {
            lsvSinhVien.Items.Clear();
            foreach (var item in source)
            {
                lsvSinhVien.Items.Add((ListViewItem)item.Clone());
            }
        }

        // Sự kiện khi click vào một dòng trong ListView thì hiện lên Textbox
        private void lsvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvSinhVien.SelectedItems[0];
                txtMaSV.Text = item.Text;
                txtTenSV.Text = item.SubItems[1].Text;
                txtLop.Text = item.SubItems[2].Text;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}