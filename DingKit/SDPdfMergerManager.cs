using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace DingKit
{
    /// <summary>
    /// pdf文件合并处理类
    /// </summary>
    public class SDPdfMergeManager
    {
        private PdfWriter pw;
        private PdfReader reader;
        private Document document;
        private PdfContentByte cb;
        private PdfImportedPage newPage;
        /// <summary>
        /// 通过输出文件来构建合并管理，合并到新增文件中,合并完成后调用FinishedMerge方法
        /// </summary>
        /// <param name="sOutFiles"></param>
        public SDPdfMergeManager(string sOutFiles)
        {
            document = new Document(PageSize.A4);
            pw = PdfWriter.GetInstance(document, new FileStream(sOutFiles, FileMode.Create));
            document.Open();
            cb = pw.DirectContent;
        }
        /// <summary>
        /// 通过文件流来合并文件，合并到当前的可写流中，合并完成后调用FinishedMerge方法
        /// </summary>
        /// <param name="sm"></param>
        public SDPdfMergeManager(Stream sm)
        {
            document = new Document();
            pw = PdfWriter.GetInstance(document, sm);
            document.Open();
            cb = pw.DirectContent;
        }
        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="sFiles">需要合并的文件路径名称</param>
        /// <returns></returns>
        public bool MergeFile(string sFiles)
        {
            reader = new PdfReader(sFiles);
            {
                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {

                    newPage = pw.GetImportedPage(reader, j);
                    Rectangle r = reader.GetPageSize(j);
                    document.SetPageSize(r);
                    document.NewPage();
                    cb.AddTemplate(newPage, 0, 0);
                }

            }
            reader.Close();
            return true;
        }
        /// <summary>
        /// 通过字节数据合并文件
        /// </summary>
        /// <param name="pdfIn">PDF字节数据</param>
        /// <returns></returns>
        public bool MergeFile(byte[] pdfIn)
        {
            reader = new PdfReader(pdfIn);
            {
                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {
                    newPage = pw.GetImportedPage(reader, j);
                    Rectangle r = reader.GetPageSize(j);
                    document.SetPageSize(r);
                    document.NewPage();
                    cb.AddTemplate(newPage, 0, 0);
                }
            }
            reader.Close();
            return true;
        }
        
        /// <summary>
        /// 完成合并
        /// </summary>
        public void FinishedMerge()
        {
            try
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (pw != null)
                {
                    pw.Flush();
                    pw.Close();
                }
                if (document.IsOpen())
                {
                    document.Close();
                }
            }
            catch
            {
            }
        }
    }
}