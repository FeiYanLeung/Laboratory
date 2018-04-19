using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Laboratory.CropTest
{
    public class Thumbnail
    {

        /// <summary>
        /// 获取图片流
        /// </summary>
        /// <param name="url">图片URL</param>
        /// <returns></returns>
        private static Stream GetRemoteImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentLength = 0;
            request.Timeout = 20000;
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return response.GetResponseStream();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }

        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        /// <param name="url">图片url</param>
        /// <param name="newFileName">缩略图路径（绝对路径）</param>
        /// <param name="maxWidth">缩略图宽度</param>
        /// <param name="maxHeight">缩略图高度</param>
        /// <param name="cropWidth">裁剪宽度</param>
        /// <param name="cropHeight">裁剪高度</param>
        /// <param name="X">X轴</param>
        /// <param name="Y">Y轴</param>
        public static bool MakeThumbnailImage(string url, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            try
            {
                using (Stream stream = GetRemoteImage(url))
                {
                    if (stream != null)
                    {
                        using (Image originalImage = Image.FromStream(stream))
                        {
                            Bitmap b = new Bitmap(cropWidth, cropHeight);
                            using (Graphics g = Graphics.FromImage(b))
                            {
                                //设置高质量插值法
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                //设置高质量,低速度呈现平滑程度
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                //清空画布并以透明背景色填充
                                g.Clear(Color.Transparent);
                                //在指定位置并且按指定大小绘制原图片的指定部分
                                g.DrawImage(originalImage, new Rectangle(0, 0, cropWidth, cropHeight), X, Y, cropWidth, cropHeight, GraphicsUnit.Pixel);
                                Image displayImage = new Bitmap(b, maxWidth, maxHeight);
                                SaveImage(displayImage, newFileName, GetCodecInfo("image/" + GetFormat(newFileName).ToString().ToLower()));
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)100));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

    }
}
