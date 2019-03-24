using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyNhaHang
{
    public partial class DangNhap : System.Web.UI.Page
    {
        DataSet1TableAdapters.NhanVienTableAdapter nv = new DataSet1TableAdapters.NhanVienTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(@"Data Source=PHU\WOI;Initial Catalog=QLNhaHang;Integrated Security=True");
            string query = "SELECT COUNT(*) FROM NhanVien WHERE UserName = @user AND Password = @pass";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@user", txtUser.Text.Trim());
            cmd.Parameters.AddWithValue("@pas", txtPass.Text.Trim());
            if (txtUser.Text == "" && txtPass.Text == "")
            {
                lblTBLoi.Text = "Vui lòng nhập đầy đủ thông tin";
            }
            else
                if (txtUser.Text != nv.LayUser(txtUser.Text) && txtPass.Text != nv.LayPass(txtPass.Text))
                {
                    lblTBLoi.Text = "Tài khoản và mật khẩu không đúng, vui lòng nhập lại";
                }
                else
                    if (txtUser.Text != nv.LayUser(txtUser.Text))
                    {
                        lblTBLoi.Text = "Tài khoản không đúng";
                    }
                    else
                        if (txtUser.Text == nv.LayUser(txtUser.Text) &&txtPass.Text != nv.LayPass(txtPass.Text))
                        {
                            lblTBLoi.Text = "Sai mật khẩu";
                        }
                        else
                        {
                            Session["UserName"] = txtUser.Text.Trim();
                            Response.Redirect("TrangChu.aspx");
                        }
            // Viết nút nhớ mật khẩu đi nhá
        }
    }
}