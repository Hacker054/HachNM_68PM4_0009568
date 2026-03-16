using QuanLySinhVienVaLopHoc;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient; // Thêm thư viện này để dùng SqlConnection

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
            this.btnLogin.Click += btnLogin_Click;
            this.btnExit.Click += btnExit_Click;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý - Đăng Nhập";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "hieuLX";
            txtPassword.Text = "msv";
            btnLogin.BackColor = Color.LightBlue;
            btnExit.BackColor = Color.LightGray;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 1. KIỂM TRA KẾT NỐI SQL SERVER TRƯỚC
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open(); // Thử mở kết nối
                    // Nếu đến đây không lỗi thì kết nối SQL đã OK
                }
            }
            catch (Exception ex)
            {
                // Nếu SQL lỗi (sai Server MSI\HACKMINH, sai pass sa...), nó sẽ hiện ở đây
                MessageBox.Show("LỖI KẾT NỐI DATABASE:\n" + ex.Message, "Lỗi Hệ Thống",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại luôn, không cho đăng nhập tiếp
            }

            // 2. LOGIC KIỂM TRA ĐĂNG NHẬP (Sau khi SQL đã thông suốt)
            if (username == "hieuLX" && password == "msv")
            {
                MessageBox.Show("Chào mừng " + username + " đã quay trở lại!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                MainForm main = new MainForm();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}