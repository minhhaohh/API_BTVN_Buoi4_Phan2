using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranMinhHao_5951071021
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-92V95VD\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        public void GetStudentsRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgvStudent.DataSource = dt;
        }

        public bool IsValidData()
        {
            if (txtFatherName.Text == string.Empty || txtName.Text == string.Empty || txtAddress.Text == string.Empty
                || string.IsNullOrEmpty(txtMobile.Text) || string.IsNullOrEmpty(txtRollNumber.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        int studentID;
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            studentID = Convert.ToInt32(dgvStudent.Rows[0].Cells[0].Value);
            txtName.Text = dgvStudent.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = dgvStudent.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNumber.Text = dgvStudent.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvStudent.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = dgvStudent.SelectedRows[0].Cells[5].Value.ToString();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES (@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Text = null;
            txtFatherName.Text = null;
            txtRollNumber.Text = null;
            txtAddress.Text = null;
            txtMobile.Text = null;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (studentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @Name, FatherName = @FatherName, RollNumber = @RollNumber," +
                    " Address = @Address, Mobile = @Mobile WHERE StudentId = @StudentId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("StudentId", this.studentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (studentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentId = @StudentId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("StudentId", this.studentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
            }
            else
            {
                MessageBox.Show("Xóa bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
