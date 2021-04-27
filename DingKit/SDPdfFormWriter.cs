using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Web;

namespace DingKit
{

    public class FieldInfo
    {
         public string FieldName
        {
            get;
            set;
        }
        public string FieldValue
        {
            get;
            set;
        }
        public FieldInfo(string fName,string fValue)
        {
            FieldName = fName;
            FieldValue = fValue;
        }

        public FieldInfo()
        {

        }
    }

    public class SDPdfFormWriter
    {

        /// <summary>
        /// 根据填充内容获得一页的PDF文件内容
        /// </summary>
        /// <param name="pdf1">模板文件</param>
        /// <param name="lfi">数据LIST</param>
        /// <param name="buf">返回的BUFFER</param>
        /// <returns>0为不成功</returns>
        public static int Get1PageBuf(string pdf1, List<FieldInfo> lfi, out byte[] buf)
        {
             int iret =0;
             MemoryStream ms = new MemoryStream();
          
             PdfReader reader = new PdfReader(pdf1);
             PdfStamper stamp1 = new PdfStamper(reader, ms);

            //不关闭流
             stamp1.Writer.CloseStream = false;

             AcroFields form1 = stamp1.AcroFields;
 
            //BaseFont.AddToResourceSearch("iTextAsian.dll");
            //BaseFont.AddToResourceSearch("iTextAsianCmaps.dll");
            BaseFont.AddToResourceSearch(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\bin\\iTextAsian.dll");
            BaseFont.AddToResourceSearch(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\bin\\iTextAsianCmaps.dll");


            BaseFont font = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.EMBEDDED);

             //以下为填写表单部分
             for (int i = 0; i < lfi.Count; i++)
             {
                 form1.SetFieldProperty(lfi[i].FieldName, "textfont", font, null);
                 form1.SetField(lfi[i].FieldName, lfi[i].FieldValue);
             }

            //一下仅用于进度条start
             CProcessInfo info = HttpContext.Current.Session["ProcessInfo"] as CProcessInfo;
             info.Message = lfi[0].FieldValue+"创建打印文件完成";
             info.Current++;
             //一下仅用于进度条end

             stamp1.FormFlattening = true;
             stamp1.Close();

             buf = ms.GetBuffer();
             iret = 1;
             return iret;
        }

        /// <summary>
        /// 得到多页的PDF文件
        /// </summary>
        /// <param name="pdf1">模板</param>
        /// <param name="llfi"></param>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static int GetNPagesBuf(string pdf1, List<List<FieldInfo>> llfi, out byte[] buf)
        {
            //一下仅用于进度条start
            CProcessInfo info = HttpContext.Current.Session["ProcessInfo"] as CProcessInfo;
            info.Message = "PDF文件生成开始";
            info.TotalCount = llfi.Count;
            //一下仅用于进度条start


            int iret = 0;
            List<byte[]> lbs = new List<byte[]>();
            for (int i = 0; i < llfi.Count; i++)
            {
                byte[] b = null;
                Get1PageBuf(pdf1, llfi[i], out b);
                lbs.Add(b);
        
            }
            iret = MergePDFPages(lbs, out buf);
            return iret;
        }

        /// <summary>
        /// 获得多页的PDF文件
        /// </summary>
        /// <param name="pdf1">模板文件</param>
        /// <param name="llfi">按页内容LIST文件</param>
        /// <param name="pdfout">输出文件</param>
        /// <returns>页数</returns>
        public static int GetNPagesBuf(string pdf1, List<List<FieldInfo>> llfi, string pdfout)
        {
            //一下仅用于进度条start
            CProcessInfo info = HttpContext.Current.Session["ProcessInfo"] as CProcessInfo;
            int iret = 0;
            byte[] bout = null;
            //一下仅用于进度条start
            info.Message = "PDF数据流加载";
            iret = GetNPagesBuf(pdf1, llfi, out bout);
            //一下仅用于进度条start
            info.Message = "PDF数据流写入开始";
            File.WriteAllBytes(pdfout, bout);
            //一下仅用于进度条start
            info.Message = "PDF数据流写入完成";
            return iret;
        }

  
        /// <summary>
        /// 合并PDF，每页以byte字节流的方式
        /// </summary>
        /// <param name="listBytes">页字节流LIST</param>
        /// <param name="buf">返回的多页PDF的字节流</param>
        /// <returns>页数</returns>
        public static int MergePDFPages( List<byte[]> listBytes, out byte[] buf)
        {
            int iret = 0;
            buf = null;
            MemoryStream ms = new MemoryStream();
            SDPdfMergeManager pm = new SDPdfMergeManager(ms);

            for (int i = 0; i < listBytes.Count; i++)
            {
                pm.MergeFile(listBytes[i]);
            }
            pm.FinishedMerge();
            buf = ms.GetBuffer();
            return iret;

        }

        /// <summary>
        /// 合并PDF文件
        /// </summary>
        /// <param name="pdfout">输出的PDF文件目录</param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static int MergePDFPages( List<byte[]> listBytes, string pdfout)
        {
            int iret = 0;
            byte[] bout = null;
            iret = MergePDFPages(listBytes, out bout);
            File.WriteAllBytes(pdfout, bout);
            return iret;
        }

    }
}
