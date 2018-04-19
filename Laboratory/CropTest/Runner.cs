
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Laboratory.CropTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "图片裁切"; }
        }

        public void Run()
        {
            try
            {
                string remotingUrl = @"https://auth.gfx.ms/16.000.27732.36/images/microsoft_logo.svg?x=ee5c8d9fb6248c938fd0dc19370e90bd";
                string newFileName = @"D:\crop.jpg";

                int maxWidth = 250,
                            maxHeight = 250;

                int cropWidth = 160,
                    cropHeight = 160;

                int X = 0,
                    Y = 0;

                Thumbnail.MakeThumbnailImage(remotingUrl, newFileName, maxWidth, maxHeight, cropWidth, cropHeight, X, Y);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Test(Size cropDestSize)
        {
            using (Image originalImage = Image.FromFile("/Crop/Desert.jpg"))
            {

                Bitmap originalBitmap = new Bitmap(AvatarModel.Width, AvatarModel.Height, PixelFormat.Format32bppRgb);
                using (Graphics graphics = Graphics.FromImage(originalBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.Clear(Color.Transparent);

                    graphics.DrawImage(originalImage,
                        new Rectangle(new Point(0, 0), new Size(AvatarModel.Width, AvatarModel.Height)),
                        new Rectangle(new Point(AvatarModel.X, AvatarModel.Y), new Size(AvatarModel.Width, AvatarModel.Height)),
                        GraphicsUnit.Pixel);

                    Image displayImage = cropDestSize.IsEmpty ? originalBitmap : new Bitmap(originalBitmap, cropDestSize);
                    displayImage.Save("test.jpg", ImageFormat.Jpeg);
                }
            }
        }
    }
}
