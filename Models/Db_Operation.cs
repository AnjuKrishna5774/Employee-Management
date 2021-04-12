using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Models
{
    public class Db_Operation
    {
        string connectionstring = "server=localhost;port=3306;database=employee_schema;user=root;password=123456";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionstring);
        }
        public void InsertEmployee(EmployeeModel model)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlParameter[] prm = new MySqlParameter[10];
            try
            {
          
                prm[0] = new MySqlParameter("argempl_id", MySqlDbType.Int32);
                prm[0].Value = model.empl_id;
                prm[1] = new MySqlParameter("argempl_fst_name", MySqlDbType.VarChar);
                prm[1].Value = model.empl_fst_name;
                prm[2] = new MySqlParameter("argempl_lst_name", MySqlDbType.VarChar);
                prm[2].Value = model.empl_lst_name;
                prm[3] = new MySqlParameter("argempl_email", MySqlDbType.VarChar);
                prm[3].Value = model.empl_email;
                prm[4] = new MySqlParameter("argempl_dob", MySqlDbType.Date);
                prm[4].Value = model.empl_dob;
                prm[5] = new MySqlParameter("argempl_gender", MySqlDbType.Int32);
                if (model.empl_gender == "Male")
                {
                    prm[5].Value = "0";
                }
                else if (model.empl_gender == "Female")
                {
                    prm[5].Value = "1";
                }
                else
                {
                    prm[5].Value = "2";
                }
                prm[6] = new MySqlParameter("argempl_password", MySqlDbType.VarBinary);
                prm[6].Value = System.Text.Encoding.UTF8.GetBytes(model.empl_password);
                cmd.Parameters.Add(prm[0]);
                cmd.Parameters.Add(prm[1]);
                cmd.Parameters.Add(prm[2]);
                cmd.Parameters.Add(prm[3]);
                cmd.Parameters.Add(prm[4]);
                cmd.Parameters.Add(prm[5]);
                cmd.Parameters.Add(prm[6]);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insert_employee";
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateEmployee(EmployeeModel model)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlParameter[] prm = new MySqlParameter[10];
            try
            {

                prm[0] = new MySqlParameter("argempl_id", MySqlDbType.Int32);
                prm[0].Value = model.empl_id;
                prm[1] = new MySqlParameter("argempl_fst_name", MySqlDbType.VarChar);
                prm[1].Value = model.empl_fst_name;
                prm[2] = new MySqlParameter("argempl_lst_name", MySqlDbType.VarChar);
                prm[2].Value = model.empl_lst_name;
                prm[3] = new MySqlParameter("argempl_email", MySqlDbType.VarChar);
                prm[3].Value = model.empl_email;
                prm[4] = new MySqlParameter("argempl_dob", MySqlDbType.Date);
                prm[4].Value = model.empl_dob;
                prm[5] = new MySqlParameter("argempl_gender", MySqlDbType.Int32);
                if (model.empl_gender == "Male")
                {
                    prm[5].Value = "0";
                }
                else if (model.empl_gender == "Female")
                {
                    prm[5].Value = "1";
                }
                else
                {
                    prm[5].Value = "2";
                }
                
                cmd.Parameters.Add(prm[0]);
                cmd.Parameters.Add(prm[1]);
                cmd.Parameters.Add(prm[2]);
                cmd.Parameters.Add(prm[3]);
                cmd.Parameters.Add(prm[4]);
                cmd.Parameters.Add(prm[5]);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_update_employee";
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<EmployeeModel> GetAllEmployee()
            {
             List<EmployeeModel> list = new List<EmployeeModel>();
             EmployeeModel model = new EmployeeModel();
             MySqlCommand cmd = new MySqlCommand();
             try
             {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_get_all_employee";
                using(MySqlConnection con=GetConnection())
                {
                    con.Open();
                    cmd.Connection = con;
                    using(var reader=cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new EmployeeModel();
                            model.empl_id = Convert.ToInt32(reader["empl_id"]);
                            model.empl_fst_name = reader["empl_fst_name"].ToString();
                            model.empl_lst_name = reader["empl_lst_name"].ToString();
                            model.empl_email = reader["empl_email"].ToString();
                            model.empl_dob = Convert.ToDateTime(reader["empl_dob"]);
                            var gender = Convert.ToInt32(reader["empl_gender"]);
                            if (gender==0)
                            {
                                model.empl_gender = "Male";
                            }
                            else if (gender == 1)
                            {
                                model.empl_gender = "Female";
                            }
                            else
                            {
                                model.empl_gender = "Other";
                            }
                            list.Add(model);
                        
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
                return list;

            }
        public EmployeeModel GetOneEmployee(int empl_id)
        {
            List<EmployeeModel> list = new List<EmployeeModel>();
            EmployeeModel model = new EmployeeModel();
            MySqlCommand cmd = new MySqlCommand();
            MySqlParameter[] prm = new MySqlParameter[1];
            prm[0] = new MySqlParameter("argempl_id", MySqlDbType.Int32);
            prm[0].Value = empl_id;
            cmd.Parameters.Add(prm[0]);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_get_one_employee";
                using (MySqlConnection con = GetConnection())
                {
                    con.Open();
                    cmd.Connection = con;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new EmployeeModel();
                            model.empl_id = Convert.ToInt32(reader["empl_id"]);
                            model.empl_fst_name = reader["empl_fst_name"].ToString();
                            model.empl_lst_name = reader["empl_lst_name"].ToString();
                            model.empl_email = reader["empl_email"].ToString();
                            model.empl_dob = Convert.ToDateTime(reader["empl_dob"]);
                            var gender = Convert.ToInt32(reader["empl_gender"]);
                            if (gender == 0)
                            {
                                model.empl_gender = "Male";
                            }
                            else if (gender == 1)
                            {
                                model.empl_gender = "Female";
                            }
                            else
                            {
                                model.empl_gender = "Other";
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return model;

        }
        public int CheckEmailUniqueness(int empl_id, string empl_gmail)
        {
            int count = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlParameter[] prm = new MySqlParameter[4];
                prm[0] = new MySqlParameter("argempl_id", MySqlDbType.Int32);
                prm[1] = new MySqlParameter("argempl_gmail", MySqlDbType.VarChar);
                prm[0].Value = empl_id;
                prm[1].Value = empl_gmail;
                cmd.Parameters.Add(prm[0]);
                cmd.Parameters.Add(prm[1]);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_employee_email_count";
                using (MySqlConnection con = GetConnection())
                {

                    con.Open();
                    cmd.Connection = con;
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {

            }
            return count;


        }
        public void DeleteEmployee(int empl_id)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlParameter[] prm = new MySqlParameter[1];
            try
            {
                EmployeeModel model = new EmployeeModel();
                prm[0] = new MySqlParameter("argempl_id", MySqlDbType.Int32);
                prm[0].Value = empl_id;
                cmd.Parameters.Add(prm[0]);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_delete_user";
                using(MySqlConnection con=GetConnection())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {

            }

        }

    }
}
