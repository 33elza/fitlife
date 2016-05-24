using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace FitLife.Infrastructure
{
    public class ImageResizer
    {
        /// 

        /// A class to resize uploaded images
        ///

            public int TargetSize { get; set; }
            public ImageResizer(int targetSize)
            {
                TargetSize = targetSize;
            }

            public Image CropAndResize(Image sourceImage)
            {
                Bitmap src = new Bitmap(sourceImage);
                int size = sourceImage.Width < sourceImage.Height ? sourceImage.Width : sourceImage.Height;
                Bitmap crop = src.Clone(new Rectangle(0, 0, size, size), src.PixelFormat);
                return new Bitmap(crop, this.TargetSize, this.TargetSize);
            }

        }
    
}