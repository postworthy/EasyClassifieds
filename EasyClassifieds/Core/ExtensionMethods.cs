using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyClassifieds.Model.Base;

namespace EasyClassifieds.Core
{
    public static class ExtensionMethods
    {
        public static object GetDefault(this Type type)
        {
            return typeof(ExtensionMethods).GetMethod("GetDefaultImp").MakeGenericMethod(type).Invoke(null, new Type[0]);
        }
        public static T GetDefaultImp<T>()
        {
            return default(T);
        }

        public static System.Drawing.Image ResizeImage(this System.Drawing.Image FullsizeImage, int NewWidth, int MaxHeight, bool OnlyResizeIfWider = true)
        {
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
            if (NewHeight > MaxHeight)
            {
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            FullsizeImage.Dispose();

            return NewImage;
        }
    }
}