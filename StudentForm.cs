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
            // Thiết lập sự kiện Load để nạp dữ liệu khi mở Form
            this.Load += StudentForm_Load;
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            LoadLopHocToComboBox(); // Nạp danh sách lớp vào ComboBox trước
            LoadData();             // Nạp danh sách sinh viên sau
            if (cboSearchType.Items.Count > 0) cboSearchType.SelectedIndex = 0;
        }

        // --- 1. LOAD DANH SÁCH LỚP VÀO COMBOBOX ---
        private void LoadLopHocToComboBox()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT MaLop, TenLop FROM LopHoc";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboLopHoc.DataSource = dt;
                    cboLopHoc.DisplayMember = "TenLop"; // Hiển thị tên lớp
                    cboLopHoc.ValueMember = "MaLop";    // Giá trị ngầm định là Mã lớp
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message);
            }
        }

        // --- 2. LOAD DỮ LIỆU SINH VIÊN (DÙNG INNER JOIN ĐỂ LẤY TÊN LỚP) ---
        private void LoadData()
        {
            lsvSinhVien.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Join với bảng LopHoc để lấy TenLop hiển thị cho đẹp
                    string sql = @"SELECT sv.*, lh.TenLop 
                                 FROM SinhVien sv 
                                 LEFT JOIN LopHoc lh ON sv.MaLop = lh.MaLop";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Cột 0: Họ tên
                        ListViewItem item = new ListViewItem(reader["HoTen"].ToString());
                        // Cột 1: Mã SV
                        item.SubItems.Add(reader["MaSV"].ToString());
                        // Cột 2: Tên lớp (Lấy từ bảng LopHoc nhờ lệnh JOIN)
                        item.SubItems.Add(reader["TenLop"].ToString());
                        // Cột 3: Giới tính
                        item.SubItems.Add(reader["GioiTinh"].ToString());
                        // Cột 4: Ngày sinh
                        item.SubItems.Add(Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy"));
                        // Lưu trữ Mã Lớp vào Tag hoặc một cột ẩn nếu cần để phục vụ việc Sửa/Xóa
                        item.Tag = reader["MaLop"].ToString();

                        lsvSinhVien.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 3. CHỨC NĂNG THÊM SINH VIÊN ---
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
                    string sql = "INSERT INTO SinhVien (MaSV, HoTen, MaLop, GioiTinh, NgaySinh) VALUES (@ma, @ten, @malop, @gt, @ns)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtTenSV.Text.Trim());
                    // Lấy giá trị MaLop từ ComboBox
                    cmd.Parameters.AddWithValue("@malop", cboLopHoc.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@gt", rdbNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInput();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: " + ex.Message); }
        }

        // --- 4. CHỨC NĂNG SỬA SINH VIÊN ---
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count == 0) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE SinhVien SET HoTen=@ten, MaLop=@malop, GioiTinh=@gt, NgaySinh=@ns WHERE MaSV=@ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtTenSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@malop", cboLopHoc.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@gt", rdbNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@ns", dtpNgaySinh.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message); }
        }

        // --- 5. CHỨC NĂNG TÌM KIẾM ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadData(); return; }

            string searchType = cboSearchType.SelectedItem?.ToString();
            string column = (searchType == "Mã SV") ? "MaSV" : "HoTen";

            lsvSinhVien.Items.Clear();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = $@"SELECT sv.*, lh.TenLop 
                                   FROM SinhVien sv 
                                   LEFT JOIN LopHoc lh ON sv.MaLop = lh.MaLop 
                                   WHERE sv.{column} LIKE @key";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["HoTen"].ToString());
                        item.SubItems.Add(reader["MaSV"].ToString());
                        item.SubItems.Add(reader["TenLop"].ToString());
                        item.SubItems.Add(reader["GioiTinh"].ToString());
                        item.SubItems.Add(Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy"));
                        lsvSinhVien.Items.Add(item);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        // --- 6. SỰ KIỆN CLICK VÀO DÒNG TRÊN LISTVIEW ---
        private void lsvSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvSinhVien.SelectedItems[0];
                txtTenSV.Text = item.Text;
                txtMaSV.Text = item.SubItems[1].Text;

                // Đổ lại giá trị vào ComboBox dựa trên MaLop được lưu ở Tag (hoặc tìm theo tên lớp)
                // Cách an toàn nhất là dùng mã lớp
                if (item.Tag != null)
                {
                    cboLopHoc.SelectedValue = item.Tag.ToString();
                }

                if (item.SubItems[3].Text == "Nam") rdbNam.Checked = true; else rdbNu.Checked = true;

                DateTime dt;
                if (DateTime.TryParseExact(item.SubItems[4].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
                    dtpNgaySinh.Value = dt;
            }
        }

        private void ClearInput()
        {
            txtMaSV.Clear();
            txtTenSV.Clear();
            if (cboLopHoc.Items.Count > 0) cboLopHoc.SelectedIndex = 0;
            rdbNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now;
            txtMaSV.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvSinhVien.SelectedItems.Count == 0) return;
            string ma = lsvSinhVien.SelectedItems[0].SubItems[1].Text;

            if (MessageBox.Show("Xác nhận xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                        LoadData();
                        ClearInput();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
            }
        }
    }
}