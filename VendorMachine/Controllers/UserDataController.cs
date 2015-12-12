using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VendorMachine.Models;
using System.Configuration;
using VendorMachine.Helper;

namespace VendorMachine.Controllers
{
    public class UserDataController : ApiController
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Vendormachine"].ToString();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<UserData> GetAllUserData()
        {
                List<UserData> Datauser = new List<UserData>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    //Microsoft.TeamFoundation.Release.MonitorServices.Actions.NormalizedStoreCreatorAction e = new Microsoft.TeamFoundation.Release.MonitorServices.Actions.NormalizedStoreCreatorAction();
                    //Microsoft.TeamFoundation.Release.MonitorServices.Dsc.OnPrem.OnPremDeploymentActions f = new Microsoft.TeamFoundation.Release.MonitorServices.Dsc.OnPrem.OnPremDeploymentActions();
                    //Microsoft.TeamFoundation.Release.Data.Model.DscComponent d=new Microsoft.TeamFoundation.Release.Data.Model.DscComponent();
                    //d.ResourceName
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    using (SqlCommand command = new SqlCommand("select ID,CardNumber,Item,Serial,Site,Status,Price,Timestamp from VendorData", con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserData user = new UserData();
                                user.ID = reader.GetInt32(0);
                                user.CardNumber = reader.GetString(1);
                                user.Item = reader.GetString(2);
                                user.Serial = reader.GetString(3);
                                user.Site = reader.GetString(4);
                                user.Status = reader.GetString(5);
                                user.Price = reader.GetInt32(6);
                                user.TimeStamp = reader.GetDateTime(7);
                                Datauser.Add(user);
                            }
                        }
                    }

                    //HelperClass.SendEmail();
                }
            }
            catch(Exception ex)
            {
                logger.Error("GetalluserData", ex);
            }
            return Datauser;
        }

        public IHttpActionResult GetUserData(int id)
        {
            UserData user = new UserData();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("select ID,CardNumber,Item,Serial,Site,Status,Price,Timestamp from VendorData where id=" + id, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user.ID = reader.GetInt32(0);
                            user.CardNumber = reader.GetString(1);
                            user.Item = reader.GetString(2);
                            user.Serial = reader.GetString(3);
                            user.Site = reader.GetString(4);
                            user.Status = reader.GetString(5);
                            user.Price = reader.GetInt32(6);
                            user.TimeStamp = reader.GetDateTime(7);
                        }
                    }
                }
            }
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult GetUserData(string card, string date, string item, int price, string serial, string site, string status, string time)
        {
            UserData user = new UserData();
            int recordsAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    string query = "Insert into VendorData(CardNumber,date,Item,Serial,Site,Status,Price,time,Timestamp) values ('" + card + "','" + date + "','" + item + "','" + serial + "','" + site + "','" + status + "'," + price + ",'" + time + "','" + DateTime.Now.ToString() + "')";
                    //string query = "Insert into Employees_CardNumber(CardNumber) values ('" + card +"')";
                    logger.Info("the string " + query);
//                    using (SqlCommand command = new SqlCommand("Insert into VendorData(CardNumber,date,Item,Serial,Site,Status,Price,time,Timestamp) values ('" + card + "','" + date + "','" + item + "','" + serial + "','" + site + "','" + status + "'," + price + ",'" + time + "','" + DateTime.Now.ToString() + "')", con))
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        recordsAffected = command.ExecuteNonQuery();
                    }
                    //using (SqlDataReader reader = command.ExecuteNonQuery())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        user.ID = reader.GetInt32(0);
                    //        user.card = reader.GetString(1);
                    //       // Console.WriteLine("{0} {1} {2}",
                    //       // reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    //    }
                    //}
                }
            }
            catch(Exception ex)
            {
                logger.Error("getuserdata", ex);
            }

            
            return Ok(recordsAffected+" Success");
            //return Json
        }
    }
}
