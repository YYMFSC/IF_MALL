using QC.PCL;
using QC.PCL.Utility;
using QC.Right;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WULMS.BLL.Repair;
using WULMS.BLL.Room;
using WULMS.BLL.Sys;

namespace WULMS.Web
{
    public class WeiPageBase : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (Request.Path.IndexOf("JP") > 0)
            {
                IsJump = true;
            }

            CheckWeiChat();
            Page.Header.Title = AppConfig.Get("SystemName");
        }

        #region 验证

        public string CheckMessage = "";

        public void CheckPower()
        {

        }

        bool IsJump = false;

        //检查微信登录
        public void CheckWeiChat()
        {
            string errPage = "~/Wei/ErrShow.aspx?msg={0}";
            var url = Request.Url.ToString().ToUpper();

            if (string.IsNullOrEmpty(UserOpenId) || string.IsNullOrEmpty(UserNo) || url.Contains("/JP") || url.Contains("DEFAULT.ASPX") || url.Contains("BINDSUCCESS.ASPX"))
            {
                if (string.IsNullOrEmpty(GetQueryString("code")) && string.IsNullOrEmpty(WeichatCode))
                {
                    Response.Redirect(string.Format(errPage, HttpUtility.UrlEncode("登录失败，请关闭后重新进入系统。", Encoding.UTF8)));
                }
                else
                {
                    if (string.IsNullOrEmpty(WeichatCode))
                        WeichatCode = GetQueryString("code");

                    //保存openid
                    if (string.IsNullOrEmpty(UserOpenId))
                    {
                        UserOpenId = WeiChartHelper.GetHelper().GetOpenId();
                    }

                    if (!string.IsNullOrEmpty(UserOpenId))
                    {
                        //判断登录不同的主页
                        var tch = OrgService.GetService().GetTeacherInfoByWei(UserOpenId);
                        if (tch != null)
                        {
                            var tchwkr = OrgService.GetService().CheckTchAndWkr(tch.TchId);
                            if (tchwkr == "1")
                            {
                                if (!IsJump)
                                {
                                    UserType = "Admin";
                                    SetSessionAdmin();
                                    Response.Redirect("~/Wei/MultiEntry.aspx");
                                }
                            }

                            switch (tch.TchType)
                            {
                                case "公寓":
                                    UserType = "Admin";
                                    SetSessionAdmin();
                                    if (!IsJump)
                                    {
                                        Response.Redirect("~/Wei/AdminIndex.aspx");
                                    }
                                    break;
                                case "教师":
                                    UserType = "Think";
                                    SetSessionThink();
                                    if (!IsJump)
                                    {
                                        Response.Redirect("~/Wei/ThinkIndex.aspx");
                                    }
                                    break;
                                default:
                                    if (!IsJump)
                                    {
                                        Response.Redirect("~/Wei/Index.aspx");
                                        IndexUrl = AppConfig.Domain + "/Wei/ThinkIndex.aspx";
                                    }
                                    break;
                            }
                        }

                        var wkr = OrgService.GetService().GetWorkerByWei(UserOpenId);
                        if (wkr != null)
                        {
                            UserType = "Worker";
                            SetSessionWorker();
                            if (!IsJump)
                            {
                                Response.Redirect("~/Wei/WorkerIndex.aspx");
                            }
                        }

                        var stu = OrgService.GetService().GetStudentInfoByWei(UserOpenId);
                        if (stu != null)
                        {
                            UserType = "Student";
                            SetSessionStudent();
                            if (!IsJump)
                            {
                                Response.Redirect("~/Wei/StuIndex.aspx");
                            }
                        }

                        LogHelper.Info("UserOpenId: " + UserOpenId);
                        LogHelper.Info("IsJump: " + IsJump.ToString());
                        LogHelper.Info("UserNo: " + UserNo);
                        //如果全没有，则跳转角色选择页面
                        if (!IsJump || string.IsNullOrEmpty(UserNo))
                        {
                            Response.Redirect("~/Wei/BindCheck.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect(string.Format(errPage, HttpUtility.UrlEncode("登录失败，请联系管理员。错误：找不到openID")));
                    }
                }
            }

            var flag = OrgService.GetService().CheckTchAndWkr(UserNo);
            if (flag == "1" && url.Contains("WORKERINDEX.ASPX"))
            {
                UserType = "Worker";
                SetSessionWorker();
            }
        }

        public void SetSessionAdmin()
        {
            IndexUrl = AppConfig.Domain + "/Wei/AdminIndex.aspx";

            var tch = OrgService.GetService().GetTeacherInfoByWei(UserOpenId);
            SessionHelper.SaveSession("TeacherInfo", JsonHelper.Serialize<OrgTeacher>(tch));

            UserNo = tch.TchId;
            UserName = tch.TchName;
            UserType = "Admin";

            var list = RoomService.GetService().GetBuildManagerListByUser(tch.TchId);
            var bid = string.Join(",", list.Select(s => s.Id));
            UserDataLimit = string.IsNullOrEmpty(bid) ? "NULL" : bid;
        }

        public void SetSessionThink()
        {
            IndexUrl = AppConfig.Domain + "/Wei/ThinkIndex.aspx";

            var tch = OrgService.GetService().GetTeacherInfoByWei(UserOpenId);
            SessionHelper.SaveSession("TeacherInfo", JsonHelper.Serialize<OrgTeacher>(tch));

            UserNo = tch.TchId;
            UserName = tch.TchName;
            UserType = "Think";
            //var rid = string.Join(",", RoomService.GetService().GetRIdByTeacher(tch.Department));
            UserDataLimit = tch.Department;//string.IsNullOrEmpty(rid) ? "NULL" : rid;
        }

        public void SetSessionStudent()
        {
            IndexUrl = AppConfig.Domain + "/Wei/StuIndex.aspx";

            var stu = OrgService.GetService().GetStudentInfoByWei(UserOpenId);
            SessionHelper.SaveSession("StudentInfo", JsonHelper.Serialize<OrgStudent>(stu));

            SessionHelper.SaveSession("UserNo", stu.StuId);
            SessionHelper.SaveSession("UserName", stu.StuName);
            UserType = "Student";

            SessionHelper.SaveSession("College", stu.College);
            SessionHelper.SaveSession("Specialty", stu.Specialty);
            SessionHelper.SaveSession("Grade", stu.Grade);
            SessionHelper.SaveSession("StuClass", stu.StuClass);

            var bed = RoomService.GetService().GetStuBedInfoByStuId(stu.StuId);

            SessionHelper.SaveSession("AId", bed.AId);
            SessionHelper.SaveSession("BId", bed.BId);
            SessionHelper.SaveSession("FId", bed.FName.ToString());
            SessionHelper.SaveSession("RId", bed.RId);
            SessionHelper.SaveSession("BedId", bed.Bed.ToString());

            SessionHelper.SaveSession("AName", bed.AName);
            SessionHelper.SaveSession("BName", bed.BName);
            SessionHelper.SaveSession("FName", bed.FName.ToString());
            SessionHelper.SaveSession("RName", bed.RName);
            SessionHelper.SaveSession("BedName", bed.Bed.ToString());
        }

        public void SetSessionWorker()
        {
            IndexUrl = AppConfig.Domain + "/Wei/WorkerIndex.aspx";

            var wkr = OrgService.GetService().GetWorkerByWei(UserOpenId);

            WorkerInfo = wkr;
            UserNo = wkr.WkrCode;
            UserName = wkr.WkrName;
            UserType = "Worker";

            var rprTypeMap = RepairService.GetService().GetRprWkrTypeMapList(wkr.WkrCode).Select(s => s.RprType).ToList();
            var bulidMap = RepairService.GetService().GetRprWkrBuildMapList(wkr.WkrCode).Select(s => s.BId).ToList();

            UserDataLimit = (rprTypeMap.Count == 0 ? "NULL" : string.Join(",", rprTypeMap))
                          + "&"
                          + (bulidMap.Count == 0 ? "NULL" : string.Join(",", bulidMap));
        }

        #endregion

        #region 用户信息

        #region 对象信息

        /// <summary>
        /// 学生
        /// </summary>
        protected OrgStudent StudentInfo
        {
            get
            {
                var stu = JsonHelper.Deserialize<OrgStudent>(SessionHelper.GetSession("StudentInfo"));
                if (stu == null)
                {
                    stu = new OrgStudent();
                }
                return stu;
            }
        }

        /// <summary>
        /// 教师
        /// </summary>
        protected OrgTeacher TeacherInfo
        {
            get
            {
                var tch = JsonHelper.Deserialize<OrgTeacher>(SessionHelper.GetSession("TeacherInfo"));
                if (tch == null)
                {
                    tch = new OrgTeacher();
                }
                return tch;
            }
        }

        /// <summary>
        /// 维修人员
        /// </summary>
        protected OrgWorkerInfo WorkerInfo
        {
            get
            {
                var wkr = JsonHelper.Deserialize<OrgWorkerInfo>(SessionHelper.GetSession("WorkerInfo"));
                if (wkr == null)
                {
                    wkr = new OrgWorkerInfo();
                }
                return wkr;
            }
            set
            {
                SessionHelper.SaveSession("WorkerInfo", JsonHelper.Serialize<OrgWorkerInfo>(value));
            }
        }

        #endregion

        /// <summary>
        /// 用户OpenId
        /// </summary>
        protected string UserOpenId
        {
            get
            {
                return SessionHelper.GetSession("UserOpenId");
            }
            set
            {
                SessionHelper.SaveSession("UserOpenId", value);
            }
        }

        /// <summary>
        /// 微信Code
        /// </summary>
        protected string WeichatCode
        {
            get
            {
                return SessionHelper.GetSession("WeichatCode");
            }
            set
            {
                SessionHelper.SaveSession("WeichatCode", value);
            }
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        protected string UserNo
        {
            get
            {
                return SessionHelper.GetSession("UserNo");
            }
            set
            {
                SessionHelper.SaveSession("UserNo", value);
            }
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        protected string UserName
        {
            get
            {
                return SessionHelper.GetSession("UserName");
            }
            set
            {
                SessionHelper.SaveSession("UserName", value);
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        protected string UserType
        {
            get
            {
                return SessionHelper.GetSession("UserType");
            }
            set
            {
                SessionHelper.SaveSession("UserType", value);
            }
        }

        /// <summary>
        /// 用户数据权限
        /// </summary>
        protected string UserDataLimit
        {
            get
            {
                return SessionHelper.GetSession("UserDataLimit");
            }
            set
            {
                SessionHelper.SaveSession("UserDataLimit", value);
            }
        }

        /// <summary>
        /// 用户寝室
        /// </summary>
        protected Tuple<string, string, string, string, string> StuRoomId
        {
            get
            {
                var aId = SessionHelper.GetSession("AId");//园区
                var bId = SessionHelper.GetSession("BId");//楼宇
                var fId = SessionHelper.GetSession("FId");//楼层
                var rId = SessionHelper.GetSession("RId");//房间
                var bed = SessionHelper.GetSession("BedId");//床

                var room = new Tuple<string, string, string, string, string>(aId, bId, fId, rId, bed);

                return room;
            }
        }

        /// <summary>
        /// 用户寝室
        /// </summary>
        protected Tuple<string, string, string, string, string> StuRoomName
        {
            get
            {
                var aId = SessionHelper.GetSession("AName");//园区
                var bId = SessionHelper.GetSession("BName");//楼宇
                var fId = SessionHelper.GetSession("FName") + "楼";//楼层
                var rId = SessionHelper.GetSession("RName") + "室";//房间
                var bed = SessionHelper.GetSession("BedName") + "床";//床

                var room = new Tuple<string, string, string, string, string>(aId, bId, fId, rId, bed);

                return room;
            }
        }

        /// <summary>
        /// 用户班级
        /// </summary>
        protected Tuple<string, string, string, string> UserClassName
        {
            get
            {
                var college = SessionHelper.GetSession("College");
                var specialty = SessionHelper.GetSession("Specialty");
                var grade = SessionHelper.GetSession("Grade");
                var stuClass = SessionHelper.GetSession("StuClass");

                var room = new Tuple<string, string, string, string>(college, specialty, grade, stuClass);

                return room;
            }
        }

        /// <summary>
        /// 初始密码
        /// </summary>
        protected string InitPassword
        {
            get
            {
                return "888888";
            }
        }

        /// <summary>
        /// 主页URL
        /// </summary>
        protected string IndexUrl
        {
            get
            {
                return SessionHelper.GetSession("IndexUrl");
            }
            set
            {
                SessionHelper.SaveSession("IndexUrl", value);
            }
        }

        #endregion

        #region 页面方法

        /// <summary>
        /// 获取指定QueryString的值
        /// </summary>
        /// <param name="paramName">指定QueryString的名称</param>
        /// <returns>相应的值</returns>
        public string GetQueryString(string paramName, string nullVal = "")
        {
            if (Request.Params[paramName] == null)
                return nullVal;

            return Quote(Request.Params[paramName]).Trim();
        }

        /// <summary>
        /// 获取URl参数(int)
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="nullVal">空值</param>
        /// <returns></returns>
        public int GetQueryInt(string paramName, int nullVal = 0)
        {
            if (Request.Params[paramName] == null)
                return nullVal;

            int val = nullVal;
            if (!int.TryParse(Quote(Request.Params[paramName]).Trim(), out val))
                val = nullVal;

            return val;
        }

        /// <summary>
        /// 获取URl参数(decimal)
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="nullVal">空值</param>
        /// <returns></returns>
        public decimal GetQueryDecimal(string paramName, decimal nullVal = 0)
        {
            if (Request.Params[paramName] == null)
                return nullVal;

            decimal val = nullVal;
            if (!decimal.TryParse(Quote(Request.Params[paramName]), out val))
                val = nullVal;

            return val;
        }

        /// <summary>
        /// 参数检测-防止恶意代码嵌入
        /// </summary>
        /// <param name="param">待检测参数</param>
        /// <returns>处理过的参数</returns>
        public string Quote(string param)
        {
            if (param == null || param.Length == 0)
            {
                return "";
            }
            else
            {
                //防止恶意代码嵌入
                string str = param.Replace("'", "");
                str = str.Replace("--", "");
                return str;
            }
        }

        /// <summary>
        /// 计算起始终止行号(索引0：起始行号；索引1：终止行号)
        /// </summary>
        /// <param name="rows">pageSize</param>
        /// <param name="page">pageNumber</param>
        /// <returns></returns>
        public Tuple<int, int> GetPageRowNum(string rows, string page)
        {
            rows = rows == "0" ? "1" : rows;
            page = page == "0" ? "1" : page;

            //解析行数和页数
            int _page = 1; int _rows = 1;
            if (!int.TryParse(page, out  _page) || _page < 1)
            {
                _page = 1;
            }

            if (!int.TryParse(rows, out  _rows))
            {
                _rows = 1;
            }

            //计算起始终止行号
            var begNum = (_page - 1) * _rows + 1;
            var endNum = begNum + _rows - 1;

            //返回
            return new Tuple<int, int>(begNum, endNum);
        }

        #endregion
    }
}