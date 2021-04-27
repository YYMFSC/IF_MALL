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
        /// ��Word�ĵ�ת��ΪHTML
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

                    //ת����html
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

                    //ת����ͼƬ
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
            //Microsoft.Office.Interop.Word.ApplicationClass appclass = new Microsoft.Office.Interop.Word.ApplicationClass();//ʵ����һ��Word 
            //Type wordtype = appclass.GetType();
            //Microsoft.Office.Interop.Word.Documents docs = appclass.Documents;//��ȡDocument 
            //Type docstype = docs.GetType();
            //object filename = WordFileDir;//Word�ļ���·�� 
            //Microsoft.Office.Interop.Word.Document doc = (Microsoft.Office.Interop.Word.Document)docstype.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new object[] { filename, true, true });//���ļ� 
            //Type doctype = doc.GetType();
            ////object savefilename =  @"C:\bb.html";//����HTML��·�������� 
            //object savefilename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + @"htmltemp\templ" + CreateID() + ".html";
            //doctype.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { savefilename, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML });//���ΪHtml��ʽ 
            //wordtype.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, appclass, null);//�˳� 

            ////Thread.Sleep(3000);//Ϊ��ʹ�˳���ȫ����������3�� 
            //StreamReader objreader =null;
            //bool isChange = true;
            //int i = 0;
            //do
            //{
            //    System.Threading.Thread.Sleep(3000);
            //    try
            //    {
            //        objreader = new StreamReader(savefilename.ToString(), System.Text.Encoding.GetEncoding("GB2312"));    //����������Ϊ����Html�м���Ա���Word�ļ�������                
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
        /// ��Word�ĵ�ת��ΪHTML
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

                    //ת����html
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
        /// ��Word�ĵ�ת��ΪHTML(����)
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

                    //ת����html
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

                    //ת����ͼƬ
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


        ////����WordFileDir�ڵ�*.doc�ļ�
        //private static void DealWithWordFile(string WordFileDir)
        //{
        //    //�������鱣��Դ�ļ����µ��ļ���
        //    string[] strFiles = Directory.GetFiles(WordFileDir, "*.doc");
        //    for (int i = 0; i < strFiles.Length; i++)
        //    {
        //        WordToHtmlFile(strFiles[i]);
        //    }

        //    DirectoryInfo dirInfo = new DirectoryInfo(WordFileDir);
        //    //ȡ��Դ�ļ����µ��������ļ�������
        //    DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
        //    for (int j = 0; j < ZiPath.Length; j++)
        //    {
        //        //��ȡ�������ļ�����
        //        string strZiPath = WordFileDir + "\\" + ZiPath[j].ToString();
        //        //�ѵõ������ļ��е����µ�Դ�ļ��У���ͷ��ʼ��һ�ֵ�����
        //        DealWithWordFile(strZiPath);
        //    }
        //}


        ////ת��
        //private static void WordToHtmlFile(string WordFilePath)
        //{
        //    try
        //    {
        //        Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();
        //        // ָ��ԭ�ļ���Ŀ���ļ�
        //        object Source = WordFilePath;
        //        string SaveHtmlPath = WordFilePath.Substring(0, WordFilePath.Length - 3) + "html";
        //        object Target = SaveHtmlPath;

        //        // ȱʡ����  
        //        object Unknown = Type.Missing;

        //        //Ϊ�˱���,ֻ����ʽ��
        //        object readOnly = true;

        //        // ��doc�ļ�
        //        Microsoft.Office.Interop.Word.Document doc = newApp.Documents.Open(ref Source, ref Unknown,
        //             ref readOnly, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown, ref Unknown,
        //             ref Unknown, ref Unknown);

        //        // ָ�����Ϊ��ʽ(rtf)
        //        object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
        //        // ת����ʽ
        //        doc.SaveAs(ref Target, ref format,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown, ref Unknown,
        //                ref Unknown, ref Unknown);

        //        // �ر��ĵ���Word����
        //        doc.Close(ref Unknown, ref Unknown, ref Unknown);
        //        newApp.Quit(ref Unknown, ref Unknown, ref Unknown);
        //    }
        //    catch (Exception e)
        //    {
        //        //System.Windows.Forms.MessageBox.Show(e.Message);
        //    }
        //}

        /// <summary>
        /// ����16λ�������
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