using System.Data;
using System.Data.SqlClient;

namespace DemoWebApp.Models
{
    public class DAL
    {
        public List<User> GetUsers(IConfiguration configuration)
        {
            List<User> listUsers = new List<User>();
            using(SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TblUsers;", connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if(dataTable.Rows.Count > 0)
                {
                    for(int i = 0; i < dataTable.Rows.Count; i++) 
                    {
                        User user = new User();
                        user.Id = Convert.ToString(dataTable.Rows[i]["ID"]);
                        user.FirstName = Convert.ToString(dataTable.Rows[i]["FirstName"]);
                        user.LastName = Convert.ToString(dataTable.Rows[i]["LastName"]);
                        listUsers.Add(user);
                    }
                }
            }
            return listUsers;
        }
        public int AddUser(User user, IConfiguration configuration)
        {
            int i = 0; 
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TblUsers(FirstName, LastName) VALUES('" + user.FirstName + "' , '" + user.LastName + "')", connection);
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return i;
        }
        public User GetUser(string id, IConfiguration configuration)
        {
            User user = new User();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM TblUsers WHERE ID = '"+id+"';", connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    user.Id = Convert.ToString(dataTable.Rows[0]["ID"]);
                    user.FirstName = Convert.ToString(dataTable.Rows[0]["FirstName"]);
                    user.LastName = Convert.ToString(dataTable.Rows[0]["LastName"]);
                }
            }
            return user;
        }
        public int UpdateUser(User user, IConfiguration configuration)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand cmd = new SqlCommand("UPDATE TblUsers SET FirstName = '" + user.FirstName + "',LastName = '" + user.LastName + "' WHERE ID = '"+user.Id+"'" , connection);
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return i;
        }
        public int DeleteUser(string id, IConfiguration configuration)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TblUsers WHERE ID = '"+id+"'", connection);
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
            }
            return i;
        }
    }
}
