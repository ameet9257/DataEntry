using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DataEntery
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login_button_Click(object sender, EventArgs e)
        {

            string userName = inputUserName.Text;
            string userPwd = inputUserPwd.Text;

            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            String getUserName_pwd = "SELECT * FROM Users WHERE UserName = '" + userName + "' and UserPwd = '" + userPwd + "'";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getUserName_pwd, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Session["userName"] = userName;
                    Session["userId"] = dataReader["UserId"].ToString();
                }

                
                Response.Redirect("DataEntry.aspx");
            }
            else
            {
                MessageBox.Show("Invaild UserName and Password");
            }
            dataReader.Close();
            conn.Close();

        }
    }
}