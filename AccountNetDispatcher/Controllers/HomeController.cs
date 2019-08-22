using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountNetDispatcher.Models;

namespace AccountNetDispatcher.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var list = new List<NetDispatcherModel> { };

            string oradb = "Data Source=CAD930; User Id=SMAS930; Password=xxxx;";
            using (OracleConnection conn = new OracleConnection(oradb))
            using (OracleCommand cmd = new OracleCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select (SELECT TO_CHAR (SYSDATE, 'MM-DD-YYYY HH24:MI:SS') NOW FROM DUAL) as report_time, empid, Fname, IPT_DTS2HUMANDATA(ldts) as login_at " +
                    "from persl " +
                    "where  LOGGED_ON = 'T' and term = TO_CHAR(empid) and ldts like '" + @DateTime.Now.ToString("yyyyMMdd") + "%'" +
                    "order by login_at asc";
                cmd.CommandType = CommandType.Text;
                ViewBag.aaa = cmd.CommandText;
                using (OracleDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    if (dr.HasRows == true)
                    {
                        do
                        {
                            var netDispatcherModel = new NetDispatcherModel();
                            netDispatcherModel.Report_Time = Convert.ToDateTime(dr["report_time"]);
                            netDispatcherModel.EmpId = dr["empid"].ToString();
                            netDispatcherModel.Fname = dr["Fname"].ToString();
                            netDispatcherModel.Login_At = dr["login_at"].ToString();
                            list.Add(netDispatcherModel);
                        }
                        while (dr.Read());
                    }
                }
            }
            return View(list);
        }
    }
}