using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using DingKit;

namespace ZWL.Common
{
    public class HtmlOperate
    {
       
        public DataTable CreateDataTableFromHTML(string strHTML)
        {
            DataTable dt = new DataTable("TBData");

            DataColumn dc = new DataColumn("ÐòºÅ");
            dc.DataType = typeof(string);
            dt.Columns.Add(dc);


            string strTemp = "";
            StringBuilder sb = new StringBuilder();
            do
            {
                strTemp = Regex.Match(strHTML, @"<TEXTAREA[\s\S]*?</TEXTAREA>", RegexOptions.IgnoreCase).Value;
                strHTML = Regex.Replace(strHTML, strTemp, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (strHTML == "" || strTemp=="")
                {
                    break;
                }
                strHTML = strHTML.Replace(strTemp, "");
                string strId = Regex.Match(strTemp, @"name=[\s\S]*?( |>)", RegexOptions.IgnoreCase).Value;
                if (strId.IndexOf('=') > 0)
                {
                    strId = strId.Substring(8, strId.Length - 8 - 1);
                }
               
                if (strId != "")
                {
                    dc = new DataColumn(strId);
                    dc.DataType = typeof(string);
                    dt.Columns.Add(dc);
                }
            }
            while (strTemp != "");

            return dt;
        }


        public DataTable GetDataFromHTML(string strHTML,string strID,DataTable dt)
        {
            string strTemp = "";
            DataRow dr = dt.NewRow();
            dr["ÐòºÅ"] = strID;

            StringBuilder sb = new StringBuilder();
            do
            {
                strTemp = Regex.Match(strHTML, @"<TEXTAREA[\s\S]*?</TEXTAREA>", RegexOptions.IgnoreCase).Value;
                strHTML = Regex.Replace(strHTML, strTemp, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (strHTML == "" || strTemp == "")
                {
                    break;
                }
                strHTML = strHTML.Replace(strTemp, "");
                string strId = Regex.Match(strTemp, @"name=[\s\S]*?( |>)", RegexOptions.IgnoreCase).Value;
                if (strId.IndexOf('=') > 0)
                {
                    strId = strId.Substring(8, strId.Length - 8 - 1);
                }
                string strValue = Regex.Match(strTemp, @">[\s\S]*?</TEXTAREA>", RegexOptions.IgnoreCase).Value;
        
                strValue = strValue.Replace("</TEXTAREA>", "");
                int j = strValue.LastIndexOf('>');
                if (j >= 0)
                {
                    strValue = CFun.Right(strValue, strValue.Length - j - 1);
                    //strValue = strValue.Substring(j+1, strValue.Length-j);
                }
                else
                {
                    strValue = "";
                }

                //if (strValue.IndexOf('/') > 0)
                //{
                //    strValue = strValue.Substring(1, strValue.Length - 11 - 1);
                //}
                //else
                //{
                //    strValue = "";
                //}

                if (strId != "")
                {
                    dr[strId] = strValue;
                }
                
            }
            while (strTemp != "");
            dt.Rows.Add(dr);
            return dt;
        }
    }
}
