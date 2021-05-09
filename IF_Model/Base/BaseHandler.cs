using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace IF_Model
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        //实例化数据库上下文类
        public mallEntities mall = new mallEntities();
        public virtual void ProcessRequest(HttpContext context)
        {
            string methodName = context.Request["method"];
            //判断方法名是否存在
            if (string.IsNullOrEmpty(methodName))
            {
                context.Response.AppendHeader("sessionstatus", "methodNotExists");
                return;
            }

            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            method.Invoke(this, new object[] { context });
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void WriteSuccess(HttpContext context, object systemResultsSuccessObj = null, bool isEnd = false)
        {
            var obj = new SystemResults().returnSuccess(systemResultsSuccessObj);
            context.Response.Write(JSON.Encode(obj));
            if (isEnd)
                context.Response.End();
        }
        public void WriteFailure(HttpContext context, string systemResultsFailureStr = "", bool isEnd = false)
        {
            var obj = new SystemResults().returnFailure(systemResultsFailureStr);
            context.Response.Write(JSON.Encode(obj));
            if (isEnd)
                context.Response.End();
        }
        public void WriteInfo(HttpContext context, object successObj = null, bool isEnd = false)
        {
            context.Response.Write(JSON.Encode(successObj));
            if (isEnd)
                context.Response.End();
        }

        public IEnumerable<M> GetPagination<M>(IF_SQLPager pager, IQueryable<M> linq) where M : class
        {
            pager.count = linq.Count();
            OrderModelField orderModelField = new OrderModelField();
            orderModelField.PropertyName = pager.sortField;
            orderModelField.IsDESC = string.Compare("desc", pager.sortOrder, true) == 0;
            linq = EFCommonSort.CommonSort(linq, new OrderModelField[] { orderModelField });
            var lists = linq.Skip((pager.currentPage - 1) * pager.pageSize).Take(pager.pageSize).ToList();
            pager.datalist = lists;
            return lists;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        public void Upload(HttpContext context)
        {
            string res = "{\"code\":\"error\",\"msg\":\"上传失败\"}";
            string type = context.Request["type"];
            string Commurl = "../uploadfile/attached/" + type + "/";//
            string imgurl = "";
            try
            {
                int chunk = context.Request.Params["chunk"] != null ? int.Parse(context.Request.Params["chunk"]) : 0;
                int total = Convert.ToInt32(context.Request["chunks"]);
                HttpPostedFile uploadFile;
                if (context.Request.Files.Count == 1)
                    uploadFile = context.Request.Files[0];
                else
                {
                    res = "{\"code\":\"error\",\"msg\":\"只允许单文件上传\"}";
                    context.Response.Write(res);
                    return;
                }
                string fname = Guid.NewGuid().ToString() + "." + uploadFile.FileName.Split('.')[1];
                Byte[] buffer = ReadBuffer(context);
                bool isAllow = IsAllowedExtension(buffer);
                if (!isAllow)
                {
                    res = "{\"code\":\"error\",\"msg\":\"该文件格式不允许上传\"}";
                    context.Response.Write(res);
                    return;
                }
                string savePath = HttpContext.Current.Server.MapPath(Commurl);
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                using (FileStream fs = new FileStream(savePath + fname, chunk == 0 ? FileMode.OpenOrCreate : FileMode.Append))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    if (total - 1 == chunk || total == 0)
                    {
                        imgurl = Commurl + fname;
                        res = "{\"code\":\"ok\",\"msg\":\"上传成功\",\"src\":\"" + imgurl + "\"}";
                    }
                    else
                    {
                        imgurl = Commurl + fname;
                        res = "{\"code\":\"ok\",\"msg\":\"分片上传成功\",\"chunk\":\"" + chunk + "\"}";
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                res = "{\"code\":\"error\",\"msg\":\"上传失败\",\"memo\":\"" + ex.Message + "\"}";
            }
            context.Response.Write(res);
        }
        /// <summary>
        /// 从流中读取字符序列
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private byte[] ReadBuffer(HttpContext context)
        {
            Byte[] buffer = null;
            if (context.Request.ContentType == "application/octet-stream" && context.Request.ContentLength > 0)
            {
                buffer = new Byte[context.Request.InputStream.Length];
                context.Request.InputStream.Read(buffer, 0, buffer.Length);
            }
            else if (context.Request.ContentType.Contains("multipart/form-data")
              && context.Request.Files.Count > 0 && context.Request.Files[0].ContentLength > 0)
            {
                buffer = new Byte[context.Request.Files[0].InputStream.Length];
                context.Request.Files[0].InputStream.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        private bool IsAllowedExtension(byte[] buffers)
        {
            MemoryStream ms = new MemoryStream(buffers);
            BinaryReader r = new BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
                return false;
            }
            //r.Dispose();
            //fs.Close();
            /*文件扩展名说明
             *4946/104116 txt
             *7173        gif 
             *255216      jpg
             *13780       png
             *6677        bmp
             *239187      txt,aspx,asp,sql
             *208207      xls.doc.ppt
             *6063        xml
             *6033        htm,html
             *4742        js
             *8075        xlsx,zip,pptx,mmap,zip
             *8297        rar   
             *01          accdb,mdb
             *7790        exe,dll           
             *5666        psd 
             *255254      rdp 
             *10056       bt种子 
             *64101       bat 
             *4059        sgf
             */
            string[] allowExtension = new string[] { "7173", "255216", "13780", "6677" };
            if (allowExtension.Contains(fileclass))
                return true;
            else
                return false;
        }
    }
}
