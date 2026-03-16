using System.Data.SqlClient;

namespace QuanLySinhVienVaLopHoc
{
    public class DatabaseHelper
    {
        // Thay dấu . bằng tên Server của bạn nếu dùng Server khác
        private static string strConn = @"Data Source=MSI\HACKMINH;Initial Catalog=QuanLyTruongHoc;User ID=sa;Password=03102005;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(strConn);
        }
    }
}