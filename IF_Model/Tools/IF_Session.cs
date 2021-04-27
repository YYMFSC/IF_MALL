using System;
using System.Web;

namespace IF_Model
{
    /// <summary>
    /// Session的控制
    /// </summary>
    public class IF_Session
    {
        private const string SESSION_UID_NAME = "UID";
        private const string SESSION_TOKEN_NAME = "TOKEN";
        private const string SESSION_UNID_NAME = "UNID";
        private const string SESSION_USER_NAME = "User";

        /// <summary>
        /// 登录用户的主键
        /// </summary>
        public string UserKey
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_UID_NAME] == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Session[SESSION_UID_NAME].ToString();
                }
            }

            set
            {
                HttpContext.Current.Session[SESSION_UID_NAME] = value;
            }
        }

        /// <summary>
        /// 登录用户的TokenID
        /// </summary>
        public string Token
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_TOKEN_NAME] == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Session[SESSION_TOKEN_NAME].ToString();
                }
            }

            set
            {
                HttpContext.Current.Session[SESSION_TOKEN_NAME] = value;
            }
        }

        /// <summary>
        /// 登录用户的单位ID
        /// </summary>
        public string UnitKey
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_UNID_NAME] == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Session[SESSION_UNID_NAME].ToString();
                }
            }

            set
            {
                HttpContext.Current.Session[SESSION_UNID_NAME] = value;
            }
        }

        /// <summary>
        /// 判断Session是否生效
        /// </summary>
        public bool CheckLogined()
        {
            bool res = false;
            try
            {
                if (UserKey != null)
                {
                    res = true;
                }
            }
            catch (Exception e)
            {
                res = false;
            }
            return (res);
        }

        public void ClearSession()
        {
            UserKey = null;
            Token = null;
            UnitKey = null;
            HttpContext.Current.Session[SESSION_USER_NAME] = null;
        }

    }
}
