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
        /// ��ȡͼƬ��
        /// </summary>
        /// <param name="url">ͼƬURL</param>
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
        /// ��ȡͼ���������������������Ϣ
        /// </summary>
        /// <param name="mimeType">��������������Ķ���;�����ʼ�����Э�� (MIME) ���͵��ַ���</param>
        /// <returns>����ͼ���������������������Ϣ</returns>
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
        /// �ü�ͼƬ������
        /// </summary>
        /// <param name="url">ͼƬurl</param>
        /// <param name="newFileName">����ͼ·��������·����</param>
        /// <param name="maxWidth">����ͼ���</param>
        /// <param name="maxHeight">����ͼ�߶�</param>
        /// <param name="cropWidth">�ü����</param>
        /// <param name="cropHeight">�ü��߶�</param>
        /// <param name="X">X��</param>
        /// <param name="Y">Y��</param>
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
                                //���ø�������ֵ��
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                //���ø�����,���ٶȳ���ƽ���̶�
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                //��ջ�������͸������ɫ���
                                g.Clear(Color.Transparent);
                                //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������
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
        /// ����ͼƬ
        /// </summary>
        /// <param name="image">Image ����</param>
        /// <param name="savePath">����·��</param>
        /// <param name="ici">ָ����ʽ�ı�������</param>
        private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //���� ԭͼƬ ����� EncoderParameters ����
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(Encoder.Quality, ((long)100));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /// <summary>
        /// �õ�ͼƬ��ʽ
        /// </summary>
        /// <param name="name">�ļ�����</param>
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
