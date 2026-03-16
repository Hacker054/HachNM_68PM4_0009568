using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            this.Load += (s, e) => {
                LoadData();
                if (cboSearchType.Items.Count > 0) cboSearchType.SelectedIndex = 0;
            };
        }

        // --- LOAD DỮ LIỆU TỪ SQL ---
        private void LoadData()
        {
            lsvSinhVien.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM SinhVien";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["HoTen"].ToString());
                        item.SubItems.Add(reader["MaSV"].ToString());
                        item.SubItems.Add(reader["MaLop"].ToString());
                        item.SubItems.Add(reader["GioiTinh"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy"));
                        lsvSinhVien.Items.Add(item);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- CHỨC NĂNG TÌM KIẾM ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadData(); return; }

            if (cboSearchType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm!", "Thông báo");
                return;
            }

            string searchType = cboSearchType.SelectedItem.ToString();
            string column = (searchType == "Mã SV") ? "MaSV" : "HoTen"; // Ánh xạ cột SQL

            lsvSinhVien.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = $"SELECT * FROM SinhVien WHERE {column} LIKE @key";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["HoTen"].ToString());
                        item.SubItems.Add(reader["MaSV"].ToString());
                        item.SubItems.Add(reader["MaLop"].ToString());
                        item.SubItems.Add(reader["GioiTinh"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy"));
                        lsvSinhVien.Items.Add(item);
                    }
                    if (lsvSinhVien.Items.Count == 0) MessageBox.Show("Không tìm thấy sinh viên nào!", "Kết quả");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        // --- CHỨC NĂNG THÊM ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên sinh viên!", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO SinhVien (MaSV, HoTen, MaLop, GioiTinh, NgaySinh) VALUES (@ma, @ten, @lop, @gt, @ns)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtTenSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@lop", txtLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@gt", rdbNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- CHỨC NĂNG SỬA ---
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn sinh viên cần sửa từ danh sách!", "Thông báo");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE SinhVien SET HoTen=@ten, MaLop=@lop, GioiTinh=@gt, NgaySinh=@ns WHERE MaSV=@ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtTenSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@lop", txtLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@gt", rdbNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- CHỨC NĂNG XÓA ---
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count == 0) return;

            string ma = lsvSinhVien.SelectedItems[0].SubItems[1].Text;
            string ten = lsvSinhVien.SelectedItems[0].Text;

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên {ten} (Mã: {ma})?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM SinhVien WHERE MaSV=@ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa sinh viên thành công!", "Thông báo");
                        LoadData();
                        ClearInput();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi khi xóa: " + ex.Message); }
            }
        }

        private void lsvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvSinhVien.SelectedItems[0];
                txtTenSV.Text = item.Text;
                txtMaSV.Text = item.SubItems[1].Text;
                txtLop.Text = item.SubItems[2].Text;
                if (item.SubItems[3].Text == "Nam") rdbNam.Checked = true; else rdbNu.Checked = true;
                DateTime dt;
                if (DateTime.TryParseExact(item.SubItems[4].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                    dtpNgaySinh.Value = dt;
            }
        }

        private void ClearInput()
        {
            txtMaSV.Clear(); txtTenSV.Clear(); txtLop.Clear();
            rdbNam.Checked = true; dtpNgaySinh.Value = DateTime.Now;
            txtMaSV.Focus();
        }
    }
}