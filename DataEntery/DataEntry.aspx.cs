using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DataEntery
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int userId;
        int getDebitAmt = 0;
        int getCreditAmt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userName"] != null && Session["userId"] != null)
            {
                Label1.Text = Session["userName"].ToString();
                userId = int.Parse(Session["userId"].ToString());
            }
            if (!IsPostBack)
            {
                
                var listofBank = new ArrayList();
                listofBank = getListOfBank();

                foreach (string bankName in listofBank)
                {
                    dropDownBank.Items.Add(bankName);
                }

                var listofAccount = new ArrayList();
                listofAccount = getListOfAccounts();

                foreach (string account in listofAccount)
                {
                    dropDownAcc.Items.Add(account);
                }


                textDataNo.Text = getMaxDataNo().ToString();
                textDataNo.Enabled = false;

                textCredit.Text = "0";
                textDebit.Text = "0";

                cleanDataTempTable(userId);

            }
        }

        public ArrayList getListOfBank()
        {
            var listofBank = new ArrayList(); // recommended 
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            String getListOfBank = "SELECT CusName FROM Customer WHERE CusType = 1";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getListOfBank, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    listofBank.Add(dataReader["CusName"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Record Not found");
            }
            dataReader.Close();
            conn.Close();
            return listofBank;
        }

        public int getMaxDataNo()
        {
            int newDataNO = 0;
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            String getListOfBank = "SELECT COALESCE(MAX(DataNo),0)+1 as DataNo FROM Data";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getListOfBank, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    newDataNO =  int.Parse(dataReader["DataNo"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Record Not found");
            }
            dataReader.Close();
            conn.Close();
            return newDataNO;
        }


        public int getCustId(string selectedBank)
        {
            int custId = 0;
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            String getCustId = "SELECT CusId FROM Customer WHERE CusName = '" + selectedBank + " ';";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getCustId, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    custId =  int.Parse(dataReader["CusId"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Record Not found");
            }
            dataReader.Close();
            conn.Close();

            return custId;
        }


        public ArrayList getListOfAccounts()
        {
            var listofAccount = new ArrayList(); // recommended 
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            String getListOfBank = "SELECT CusName FROM Customer WHERE CusType = 2";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getListOfBank, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    listofAccount.Add(dataReader["CusName"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Record Not found");
            }
            dataReader.Close();
            conn.Close();
            return listofAccount;
        }

        public void cleanDataTempTable(int userId)
        {

            string connetionString;
            SqlConnection conn;
            SqlCommand command;

            String cleanData = "DELETE FROM TempData where DataUser = "+userId+"";
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(cleanData, conn);
            int deleted = command.ExecuteNonQuery();
            if (deleted > 0)
            {
                Console.WriteLine("Record is deleted");
            }
            conn.Close();
        }

        public int getDataId(int userId)
        {
            int dataId = 0;
            string getDataId = "SELECT max(DataId) as DataId FROM TempData where DataUser = " + userId+" and DataAmt = 0 and DataamtCr = 0";
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;
            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand(getDataId, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dataId = int.Parse(dataReader["data"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Record Not found");
            }
            dataReader.Close();
            conn.Close();
            return dataId;
        }

        public Dictionary<string,int> getNextImmedidateAmt(int dataId, int userId)
        {
            Dictionary<String,int> dict =  new Dictionary<String,int>();
            string connetionString;
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader dataReader;

            string getFieldName = "";
            int amt = 0;

            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();

            String getAmt = "select case when DataAmt = 0 then DataamtCr else DataAmt end as amt, case when DataAmt = 0 then 'DataamtCr' else 'DataAmt' end as column_has_amt from TempData where DataId = "+dataId+" and DataUser = '"+userId+"'";

            command = new SqlCommand(getAmt, conn);
            dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    getFieldName = dataReader["column_has_amt"].ToString();
                    amt = int.Parse(dataReader["amt"].ToString());
                }
                dict.Add(getFieldName,amt);
            }
            else
            {
                Console.WriteLine("Record Not found");
            }

            dataReader.Close();
            conn.Close();

            return dict;
        }

        protected void buttonProceed1_Click(object sender, EventArgs e)
        {

            String selectedDate = datePicker.Text;
            int DataNo = int.Parse(textDataNo.Text);
            int custId = getCustId(dropDownBank.Text);
            String dataRemark = inputRemark.Text;

            DateTime DataDate = DateTime.ParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);


            string connetionString;
            SqlConnection conn;
            SqlCommand command;

            connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
            conn = new SqlConnection(connetionString);
            conn.Open();
            command = new SqlCommand("insertBasicDataTemp", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DataDate", DataDate);
            command.Parameters.AddWithValue("@DataNo", DataNo);
            command.Parameters.AddWithValue("@DataAmt", 0);
            command.Parameters.AddWithValue("@DataAmtCr", 0);
            command.Parameters.AddWithValue("@DataAcc", custId);
            command.Parameters.AddWithValue("@DataRem", dataRemark);
            command.Parameters.AddWithValue("@DataUser", userId);

            int i = command.ExecuteNonQuery();

            if (i != 0)
            {
                Console.WriteLine("Record Inserted");
                //MessageBox.Show(i + "Data Saved");
            }

            conn.Close();
        }

        protected void buttonProceed2_Click(object sender, EventArgs e)
        {
            getCreditAmt = int.Parse(textCredit.Text);
            getDebitAmt = int.Parse(textDebit.Text);
            if (getCreditAmt > 0 && getDebitAmt > 0)
            {
                MessageBox.Show("Please enter either credit or debit amount");
            }
            else
            {
                String selectedDate = datePicker.Text;
                int DataNo = int.Parse(textDataNo.Text);
                int custId = getCustId(dropDownAcc.Text);
                String dataRemark = inputRemark.Text;
                DateTime DataDate = DateTime.ParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string connetionString;
                SqlConnection conn;
                SqlCommand command;

                connetionString = @"Data Source=DILEEP834;Initial Catalog=Test_Dileep;Integrated Security=True";
                conn = new SqlConnection(connetionString);
                conn.Open();
                command = new SqlCommand("insertBasicDataTemp", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DataDate", DataDate);
                command.Parameters.AddWithValue("@DataNo", DataNo);
                command.Parameters.AddWithValue("@DataAmt", getDebitAmt);
                command.Parameters.AddWithValue("@DataAmtCr", getCreditAmt);
                command.Parameters.AddWithValue("@DataAcc", custId);
                command.Parameters.AddWithValue("@DataRem", dataRemark);
                command.Parameters.AddWithValue("@DataUser", userId);

                int i = command.ExecuteNonQuery();

                if (i != 0)
                {
                    Console.WriteLine("Record Inserted");
                    //MessageBox.Show(i + "Data Saved");
                }

                conn.Close();

            }

        }
    }
}