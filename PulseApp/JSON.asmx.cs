using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using PulseApp.APP_CODE;

namespace PulseApp
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class JSONservice : System.Web.Services.WebService
//    {

//        [WebMethod]
//        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
//        public string TestJSON()
//        {
//            getNews[] e = new getNews[2];
//            e[0] = new getNews();
//            e[0].NewsRecordID = Convert.ToInt32("1001");
//            e[0].NewsHeader = "Ajay Singh";
//            e[0].NewsContent = "Birlasoft Ltd.";
//            e[0].DateTimeAdded = "LosAngeles California";

//            e[1] = new getNews();
//            e[1].NewsRecordID = Convert.ToInt32("1002");
//            e[1].NewsHeader = "Ajay Singh";
//            e[1].NewsContent = "Birlasoft Ltd.";
//            e[1].DateTimeAdded = "D-195 Sector Noida";
//            return new JavaScriptSerializer().Serialize(e);
//        }
//        [WebMethod]
//        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
//        public getNews[] TestXML()
//        {
//            getNews[] e = new getNews[2];
//            e[0] = new getNews();
//            e[0].NewsHeader = "Ajay Singh";
//            e[0].NewsContent = "Birlasoft Ltd.";
//            e[0].DateTimeAdded = "LosAngeles California";

//            e[1] = new getNews();
//            e[1].NewsHeader = "Ajay Singh";
//            e[1].NewsContent = "Birlasoft Ltd.";
//            e[1].DateTimeAdded = "D-195 Sector Noida";
//            return e;
//        }
//    }
//}
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public void GetPostings(int NewsRecordID, string callback)
        {

            using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString))
            {
                const string SQL = "SELECT [NewsRecordID], [NewsHeader],[NewsContent],[DateTimeAdded] FROM [Pulse].[dbo].[News]";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = myCommand;

                DataSet DSet = new DataSet();
                dataAdapter.Fill(DSet);

                List<getNews> getNewsData = new List<getNews>();
                //foreach (DataRow row in DSet.Tables[0].Rows)
                //{
                //    getNews gn = new getNews();
                //    gn.NewsRecordID = Convert.ToInt32(row["NewsRecordID"].ToString());
                //    gn.NewsHeader = row["NewsHeader"].ToString();
                //    gn.NewsContent = row["NewsContent"].ToString();
                //    gn.DateTimeAdded = row["DateTimeAdded"].ToString();
                //    getNewsData.Add(gn);
                //}


                foreach (DataRow row in DSet.Tables[0].Rows)
                {
                StringBuilder sb = new StringBuilder();
                JavaScriptSerializer js = new JavaScriptSerializer();
                //getNews gn = new getNews();
                sb.Append(callback + "(");
                sb.Append(js.Serialize(getNewsData));
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();
                

                //gn.NewsRecordID = Convert.ToInt32(row["NewsRecordID"].ToString());
                //gn.NewsHeader = row["NewsHeader"].ToString();
                //gn.NewsContent = row["NewsContent"].ToString();
                //gn.DateTimeAdded = row["DateTimeAdded"].ToString();
                //getNewsData.Add(gn);
                //Context.Response.Write(js.Serialize(getNewsData));
                }
                //return getNewsData;
            }
        }

        public class HelloWorldData
        {
            public int NewsRecordID { get; set; }
            public string NewsHeader { get; set; }
            public string NewsContent { get; set; }
            public string DateTimeAdded { get; set; }
        }
    }
}
