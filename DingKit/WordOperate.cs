using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using DingKit;
namespace DingKit
{
    public class WordOperate
    {


        /// <summary>
        /// 将Word文档转化为HTML
        /// </summary>
        /// <param name="WordFileDir"></param>
        /// <returns></returns>
        public static string CreateWordToHtml(string WordFileDir)
        {
            string Extension = System.IO.Path.GetExtension(WordFileDir).ToLower();
            string FileName =  System.IO.Path.GetFileNameWithoutExtension(WordFileDir);
            //string FilePath = CConfig.GetValueByKey("Officalfolder"); ;
            string FilePath = CConfig.GetValueByKey("HTMLfolder"); ;
            string HtmlPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + FilePath;
            if (Extension == ".doc" || Extension == ".docx")
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(WordFileDir);

                    //转换成html
                    doc.Save(HtmlPath + "\\" + FileName + ".html", Aspose.Words.SaveFormat.Html);
                    string strTemp = File.ReadAllText(HtmlPath + "\\" + FileName + ".html").Replace("img src=\"", "img src=\"../" + FilePath + "/");
                    strTemp = strTemp.Replace("?", "&nbsp;");
                    //doc.SaveToPdf(HtmlPath + "\\" + FileName + ".pdf");
                    //if (Extension == ".doc")
                    //{
                    //    doc.Save(HtmlPath + "\\ToPdf\\" + FileName + Extension, Aspose.Words.SaveFormat.Doc);
                    //}
                    //else
                    //{
                    //    doc.Save(HtmlPath + "\\ToPdf\\" + FileName + Extension, Aspose.Words.SaveFormat.Docx);
                    //}

                    //doc.Save(HtmlPath + "\\" + FileName + ".pdf", Aspose.Words.SaveFormat.Pdf);

                    //转换成图片
                    //doc.SaveToImage(0, doc.PageCount, CFetch.MapPath(FileName + ".tiff"), new Aspose.Words.Rendering.ImageOptions());
                    //this.TB_Content.Text = "<img src=" + CAppSettings.SitePath + FileName + ".tiff" + " />";

                    //File.Delete(FileName + Extension);
                    File.Delete(HtmlPath + "\\" + FileName + ".html");
                    return strTemp;
                }
                catch (Exception e)
                { 
                    string msg= e.Message;
                    return "";
                }

            }
            else
            {
                return "";
            }

            //StringBuilder sb = new StringBuilder();
            //Microsoft.Office.Interop.Word.ApplicationClass appclass = new Microsoft.Office.Interop.Word.ApplicationClass();//实例化一个Word 
            //Type wordtype = appclass.GetType();
            //Microsoft.Office.Interop.Word.Documents docs = appclass.Documents;//获取Document 
            //Type docstype = docs.GetType();
            //object filename = WordFileDir;//Word文件的路径 
            //Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docstype.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new object[] { filename, true, true });//打开文件 
            //Type doctype = doc.GetType();
            ////object savefilename =  @"C:\bb.html";//生成HTML的路径和名子 
            //object savefilename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + @"htmltemp\templ" + CreateID() + ".html";
            //doctype.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { savefilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML });//另存为Html格式 
            //wordtype.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, appclass, null);//退出 

            ////Thread.Sleep(3000);//为了使退出完全，这里阻塞3秒 
            //StreamReader objreader =null;
            //bool isChange = true;
            //int i = 0;
            //do
            //{
            //    System.Threading.Thread.Sleep(3000);
            //    try
            //    {
            //        objreader = new StreamReader(savefilename.ToString(), System.Text.Encoding.GetEncoding("GB2312"));    //以下内容是为了在Html中加入对本身Word文件的下载                
            //        isChange = false;
            //    }
            //    catch
            //    {
            //        isChange = true;
            //    }
            //    i++;
            //    if (i > 10)
            //    {
            //        isChange = false;
            //    }
            //}
            //while (isChange);

            //FileStream fs = new FileStream(savefilename.ToString().Split('.').GetValue(0).ToString() + "$.html", FileMode.Create);

            //StreamWriter streamHtmlHelp = new System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312"));

            //string str = "";
            //do
            //{
            //    str = objreader.ReadLine();
            //    sb.Append(str + "\n");
            //    //sb.Replace("<Html>", "");
            //    //sb.Replace("<body>", "");
            //    streamHtmlHelp.WriteLine(str);
            //}
            //while (str != "</html>");
            //streamHtmlHelp.Close();
            //objreader.Close();

            ////System.Threading.Thread.Sleep(10000);
            //File.Delete(savefilename.ToString());
            //File.Delete(savefilename.ToString().Split('.').GetValue(0).ToString() + "$.html");
            ////File.Move(savefilename.ToString().Split('.').GetValue(0).ToString() + "$.html", savefilename.ToString());

            //string strTemp = Regex.Match(sb.ToString(), @"<body[\s\S]*?</body>", RegexOptions.IgnoreCase).Value;
            //string body = Regex.Match(sb.ToString(), @"<body[\s\S]*?'>", RegexOptions.IgnoreCase).Value;
            //strTemp = Regex.Replace(strTemp, body, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //strTemp = Regex.Replace(strTemp, "</body>","", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //return strTemp;// sb.ToString();

        }

        /// <summary>
        /// 将Word文档转化为HTML
        /// </summary>
        /// <param name="WordFileDir"></param>
        /// <returns></returns>
        public static string CreateWordToHtmlTemp(string WordFileDir)
        {
            string Extension = System.IO.Path.GetExtension(WordFileDir).ToLower();
            string FileName = System.IO.Path.GetFileNameWithoutExtension(WordFileDir);
            //string FilePath = CConfig.GetValueByKey("Officalfolder"); ;
            string FilePath = CConfig.GetValueByKey("HTMLfolder"); ;
            string HtmlPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + FilePath;
            if (Extension == ".doc" || Extension == ".docx")
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(WordFileDir);

                    //转换成html
                    doc.Save(HtmlPath + "\\" + FileName + ".html", Aspose.Words.SaveFormat.Html);
                    string strTemp = File.ReadAllText(HtmlPath + "\\" + FileName + ".html").Replace("img src=\"", "img src=\"../" + FilePath + "/");
                    strTemp = strTemp.Replace("?", "&nbsp;");
                   
                    //File.Delete(FileName + Extension);
                    File.Delete(HtmlPath + "\\" + FileName + ".html");
                    File.Delete(WordFileDir);
                    return strTemp;
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    return "";
                }

            }
            else
            {
                return "";
            }


        }

        // <summary>
        /// 将Word文档转化为HTML(公文)
        /// </summary>
        /// <param name="WordFileDir"></param>
        /// <returns></returns>
        public static string CreateWordToHtmlTelFile(string WordFileDir)
        {
            string Extension = System.IO.Path.GetExtension(WordFileDir).ToLower();
            string FileName = System.IO.Path.GetFileNameWithoutExtension(WordFileDir);
            string FilePath = CConfig.GetValueByKey("HTMLfolder"); ;
            string HtmlPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + FilePath;

            if (Extension == ".doc" || Extension == ".docx")
            {
                try
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(WordFileDir);

                    //转换成html
                    doc.Save(HtmlPath + "\\" + FileName + ".html", Aspose.Words.SaveFormat.Html);
                    string strTemp = File.ReadAllText(HtmlPath + "\\" + FileName + ".html").Replace("img src=\"", "img src=\"../" + FilePath + "/");
                    strTemp = strTemp.Replace("?", "&nbsp;");
                    //doc.SaveToPdf(HtmlPath + "\\" + FileName + ".pdf");
                    //if (Extension == ".doc")
                    //{
                    //    doc.Save(HtmlPath + "\\ToPdf\\" + FileName + Extension, Aspose.Words.SaveFormat.Doc);
                    //}
                    //else
                    //{
                    //    doc.Save(HtmlPath + "\\ToPdf\\" + FileName + Extension, Aspose.Words.SaveFormat.Docx);
                    //}

                    //doc.Save(HtmlPath + "\\" + FileName + ".pdf", Aspose.Words.SaveFormat.Pdf);

                    //转换成图片
                    //doc.SaveToImage(0, doc.PageCount, CFetch.MapPath(FileName + ".tiff"), new Aspose.Words.Rendering.ImageOptions());
                    //this.TB_Content.Text = "<img src=" + CAppSettings.SitePath + FileName + ".tiff" + " />";

                    //File.Delete(FileName + Extension);
                    File.Delete(HtmlPath + "\\" + FileName + ".html");
                    return strTemp;
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    return "";
                }

            }
            else
            {
                return "";
            }

           

        }

        //public static void CreateWordToHtmlFile(string WordFileDir)
        //{
        //    DealWithWordFile(WordFileDir);
        //}


        ////搜索WordFileDir在的*.doc文件
        //private static void DealWithWordFile(string WordFileDir)
        //{
        //    //创建数组保存源文件夹下的文件名
        //    string[] strFiles = Directory.GetFiles(WordFileDir, "*.doc");
        //    for (int i = 0; i < strFiles.Length; i++)
        //    {
        //        WordToHtmlFile(strFiles[i]);
        //    }

        //    DirectoryInfo dirInfo = new DirectoryInfo(WordFileDir);
        //    //取得源文件夹下的所有子文件夹名称
        //    DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
        //    for (int j = 0; j < ZiPath.Length; j++)
        //    {
        //        //获取所有子文件夹名
        //        string strZiPath = WordFileDir + "\\" + ZiPath[j].ToString();
        //        //把得到的子文件夹当成新的源文件夹，从头开始新一轮的搜索
        //        DealWithWordFile(strZiPath);
        //    }
        //}


        ////转化
        //private static void WordToHtmlFile(string WordFilePath)
        //{
        //    try
        //    {
        //        Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();
        //        // 指定原文件和目标文件
        //        object Source = WordFilePath;
        //        string SaveHtmlPath = WordFilePath.Substring(0, WordFilePath.Length - 3) + "html";
        //        object Target = SaveHtmlPath;

        //        // 缺省参数  
        //        object Unknown = Type.Missing;

        //        //为了保险,只读方式打开
        //        object readOnly = true;

        //        // 打开doc文件
        //        Microsoft.Office.Interop.Word.Document doc = newApp.Documents.Open(ref Source, ref Unknown,
        //             ref readOnly, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown);

        //        // 指定另存为格式(rtf)
        //        object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
        //        // 转换格式
        //        doc.SaveAs(ref Target, ref format,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown);

        //        // 关闭文档和Word程序
        //        doc.Close(ref Unknown, ref Unknown, ref Unknown);
        //        newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
        //    }
        //    catch (Exception e)
        //    {
        //        //System.Windows.Forms.MessageBox.Show(e.Message);
        //    }
        //}

        /// <summary>
        /// 创建16位的随机数
        /// </summary>
        /// <returns></returns>
        public static string CreateID()
        {

            string TempYear, TempMonth, TempDay, TempHour, TempMinute, TempSecond, RandomFigure;
            DateTime NowTime;
            NowTime = DateTime.Now;
            TempYear = NowTime.Year.ToString();
            TempMonth = NowTime.Month.ToString();
            TempDay = NowTime.Day.ToString();
            TempHour = NowTime.Hour.ToString();
            TempMinute = NowTime.Minute.ToString();
            TempSecond = NowTime.Second.ToString();

            if (TempMonth.Length == 1)
            {
                TempMonth = "0" + TempMonth;
            }
            if (TempDay.Length == 1)
            {
                TempDay = "0" + TempDay;
            }
            if (TempHour.Length == 1)
            {
                TempHour = "0" + TempHour;
            }
            if (TempMinute.Length == 1)
            {
                TempMinute = "0" + TempMinute;
            }
            if (TempSecond.Length == 1)
            {
                TempSecond = "0" + TempSecond;
            }
            Random randObj = new Random();
            RandomFigure = randObj.Next(0, 1000).ToString();

            String str = TempYear + TempMonth + TempDay + TempHour + TempMinute + TempSecond + RandomFigure;
            return str;
        }

    }


      
}