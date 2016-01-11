using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorMachine.Models;

namespace VendorMachine.Helper
{
    class DBLogic
    {
        public static userData InsertCard(string connectionString, string card, string date, string item, int price, string serial, string site, string status, string time)
        {
            bool retvalue = true;
            userData user = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertVendorData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@card", card);
                    cmd.Parameters.AddWithValue("@item", item);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@serial", serial);
                    cmd.Parameters.AddWithValue("@site", site);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@time", time);
                    con.Open();
                    var result =cmd.ExecuteReader();

                    while (result.Read())
                    {
                        //string msg = String.Format("The connectionstring is {0}", connectionString);
                        //HelperClass.SendEmail("alkesh.naik@bentley.com", "Alkesh Naik", msg, "00006FAACB");
                        user = new userData();
                        user.ID = result.GetInt32(0);
                        user.FirstName = result.GetString(1);
                        user.LastName = result.GetString(2);
                        user.Email = result.GetString(3);
                        user.AccessCardDecimal = result.GetString(4);
                        user.AccessCardHex = result.GetString(5);
                        user.Item = result.GetString(6);
                        // Console.WriteLine("{0} {1} {2}",
                        // reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }

                    //if(result == 1)
                    //{
                    //    retvalue = false;
                    //}
                }

            }
            return user;
        }
    }
}
