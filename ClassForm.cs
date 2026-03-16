using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLySinhVienVaLopHoc
{
    public partial class ClassForm : Form
    {
        public ClassForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            lsvLopHoc.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM LopHoc";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Sửa lại thứ tự: Cột 0 là Mã lớp, Cột 1 là Tên lớp
                        ListViewItem item = new ListViewItem(reader["MaLop"].ToString());
                        item.SubItems.Add(reader["TenLop"].ToString());
                        item.SubItems.Add(reader["Khoa"].ToString());
                        lsvLopHoc.Items.Add(item);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã lớp và Tên lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO LopHoc VALUES (@ma, @ten, @khoa)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@khoa", txtKhoa.Text.Trim());

                    cmd.ExecuteNonQuery();

                    // THÔNG BÁO THÀNH CÔNG
                    MessageBox.Show("Thêm lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp học để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE LopHoc SET TenLop=@ten, Khoa=@khoa WHERE MaLop=@ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtLop.Text.Trim());
                    cmd.Parameters.AddWithValue("@khoa", txtKhoa.Text.Trim());

                    cmd.ExecuteNonQuery();

                    // THÔNG BÁO THÀNH CÔNG
                    MessageBox.Show("Cập nhật thông tin lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy mã lớp để hiển thị trong thông báo xác nhận
            string ma = lsvLopHoc.SelectedItems[0].Text;

            // THÔNG BÁO XÁC NHẬN XÓA
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp học có mã {ma} không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM LopHoc WHERE MaLop=@ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ma", ma);
                        cmd.ExecuteNonQuery();

                        // THÔNG BÁO THÀNH CÔNG
                        MessageBox.Show("Đã xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadData();
                        ClearInput();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể xóa lớp này vì đang có dữ liệu sinh viên liên kết!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void lsvLopHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvLopHoc.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvLopHoc.SelectedItems[0];
                // Sửa lại index theo thứ tự cột: 0: Mã lớp, 1: Tên lớp, 2: Khoa
                txtMaLop.Text = item.Text;
                txtLop.Text = item.SubItems[1].Text;
                txtKhoa.Text = item.SubItems.Count > 2 ? item.SubItems[2].Text : "";
            }
        }

        // --- CHỨC NĂNG TÌM KIẾM ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            // Nếu ô tìm kiếm trống, load lại toàn bộ danh sách
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            // Kiểm tra xem người dùng đã chọn kiểu tìm kiếm chưa (Mã lớp, Tên lớp...)
            if (cboSearchType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm!", "Thông báo");
                return;
            }

            string searchType = cboSearchType.SelectedItem.ToString();
            string column = "";

            // Ánh xạ từ lựa chọn ComboBox sang tên cột trong SQL
            if (searchType == "Mã lớp") column = "MaLop";
            else if (searchType == "Lớp") column = "TenLop";
            else if (searchType == "Khoa") column = "Khoa";

            lsvLopHoc.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Sử dụng LIKE để tìm kiếm tương đối (%keyword%)
                    string sql = $"SELECT * FROM LopHoc WHERE {column} LIKE @key";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["MaLop"].ToString());
                        item.SubItems.Add(reader["TenLop"].ToString());
                        item.SubItems.Add(reader["Khoa"].ToString());
                        lsvLopHoc.Items.Add(item);
                    }

                    if (lsvLopHoc.Items.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Tìm kiếm");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        private void ClearInput()
        {
            txtLop.Clear();
            txtMaLop.Clear();
            txtKhoa.Clear();
            txtMaLop.Focus(); // Nên focus vào Mã lớp để nhập mới
        }
    }
}