using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataEntery
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = int.Parse(Request.QueryString["userId"]);



            string getData = "SELECT UserName as 'User Name',CusName AS Account,DataAmt as Debit, DataamtCr as Credit , DataDate as Date, DatakeyDate as 'Transaction Date' FROM Data LEFT OUTER JOIN Customer on CusId = DataAcc left outer join Users ON DataUser = UserId WHERE DataUser = "+userId;
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getData, conn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            GridView1.DataSource = ds;
            GridView1.DataBind();
            conn.Close();
        }
    }
}