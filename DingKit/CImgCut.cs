using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Threading;


namespace DingKit
{
    public class CImgCut
    {

        /// @从视频文件截图,生成在视频文件所在文件夹
        /// 在Web.Config 中需要两个前置配置项:
        /// 1.ffmpeg.exe文件的路径
        /// 
        /// 2.截图的尺寸大小
        /// 
        /// 3.视频处理程序ffmpeg.exe
        /// 
        /// 视频文件地址,如:/Web/FlvFile/User1/00001.Flv
        /// 成功:返回图片虚拟地址; 失败:返回空字符串
        public string CatchImg(string vFileName, string imgPath)
        {
            //取得ffmpeg.exe的路径,路径配置在Web.Config中,如:

            string ffmpeg = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "bin/ffmpeg.exe"; //CConfig.GetValueByKey("ffmpeg");
            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(vFileName)))
            {
                return "";
            }
            //获得图片相对路径/最后存储到数据库的路径,如:/Web/FlvFile/User1/00001.jpg
            string flv_img = System.IO.Path.ChangeExtension(vFileName, ".jpg");
            //图片绝对路径,如:D:\Video\Web\FlvFile\User1\0001.jpg
            string imgName = CFun.Create12ID() + CFile.GetFileName(flv_img);
            string flv_img_p = imgPath + "/" + imgName;
            //截图的尺寸大小,配置在Web.Config中,如:
            string FlvImgSize = CConfig.GetValueByKey("CatchFlvImgSize");
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //此处组合成ffmpeg.exe文件需要的参数即可,此处命令在ffmpeg 0.4.9调试通过
            startInfo.Arguments = " -i " + vFileName + " -y -f image2 -t 0.001 -s " + FlvImgSize + " " + flv_img_p;
            try
            {
                System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                return "";
            }
            ///注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长;
            ///这儿需要延时后再检测,我服务器延时8秒,即如果超过8秒图片仍不存在,认为截图失败;
            ///此处略去延时代码.如有那位知道如何捕捉ffmpeg.exe截图失败消息,请告知,先谢过!
            while (System.IO.File.Exists(flv_img_p) == false)
            {
                Thread.Sleep(2000);
            }
            return imgName;
            //if (System.IO.File.Exists(flv_img_p))
            //{
            //    return imgName;
            //}
           // return "";
        }

    }
}
