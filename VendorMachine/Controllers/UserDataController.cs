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
using VendorMachine.Common;

namespace VendorMachine.Controllers
{
    public class UserDataController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Vendormachine"].ToString();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IEnumerable<vendorData> GetAllUserData()
        {
            List<vendorData> Datauser = new List<vendorData>();
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
                                vendorData user = new vendorData();
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
            vendorData user = new vendorData();
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
            userData user = new userData();
            int recordsAffected = 1;
            userData val = null;

            try
            {
                val=DBLogic.InsertCard(connectionString, card, date, item, price, serial, site, status, time);
            }
            catch(Exception ex)
            {
                logger.Error("getuserdata", ex);
            }

            //HelperClass.SendEmail("alkesh.naik@bentley.com", "Alkesh Naik", "a1", "00006FAACB");

            if (val != null)
            {
                string name = val.FirstName + " " + val.LastName;
                HelperClass.SendEmail(val.Email,name,val.Item,card);
                return Ok(recordsAffected + " Success");
            }
            else
            {
                //HelperClass.SendEmail();
                return Ok("Data already present");                
            }
            //return Json
        }
    }
}
