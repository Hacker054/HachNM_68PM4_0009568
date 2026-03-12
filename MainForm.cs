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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Đăng ký sự kiện Load: Khi Form hiện lên thì tự gọi hàm mở StudentForm
            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Gọi hàm nạp Form sinh viên vào Panel ngay khi MainForm vừa mở
            OpenChildForm(new StudentForm());
        }

        public void OpenChildForm(Form childForm)
        {
            if (pnlContent.Controls.Count > 0)
                pnlContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;

            // Gán Dock = Fill để form con lấp đầy panel
            childForm.Dock = DockStyle.Fill;

            // 1. Chỉnh kích thước Panel bằng kích thước gốc của Form con
            // (Giả sử StudentForm của bạn là 1244, 651)
            pnlContent.Size = childForm.Size;

            // 2. Ép MainForm phải to ra để chứa vừa cái Panel đó + Menu phía trên
            // Bạn nên cộng thêm khoảng 40 pixel chiều cao cho Menu và tiêu đề
            this.Size = new Size(childForm.Width + 20, childForm.Height + menuStrip1.Height + 40);

            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentForm());
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ClassForm());
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
