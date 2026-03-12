using QuanLySinhVienVaLopHoc;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Tự động kết nối sự kiện mà không cần thao tác ngoài Design
            this.Load += LoginForm_Load;
            this.btnLogin.Click += btnLogin_Click;
            this.btnExit.Click += btnExit_Click;

            // Chỉnh Form ra giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý - Đăng Nhập";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định theo yêu cầu của bạn
            txtUsername.Text = "hieuLX";
            txtPassword.Text = "msv";

            // Tùy chỉnh màu sắc cho "xịn" hơn một chút
            btnLogin.BackColor = Color.LightBlue;
            btnExit.BackColor = Color.LightGray;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "hieuLX" && password == "msv")
            {
                MessageBox.Show("Chào mừng " + username + " đã quay trở lại!", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 1. Ẩn LoginForm
                this.Hide();

                // 2. Mở MainForm (Cửa sổ chính chứa Panel)
                // Đảm bảo bạn đã tạo file MainForm.cs trước đó
                MainForm main = new MainForm();

                // ShowDialog sẽ giữ code ở đây cho đến khi MainForm đóng lại
                main.ShowDialog();

                // 3. Sau khi đóng MainForm thì thoát hẳn ứng dụng
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
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}