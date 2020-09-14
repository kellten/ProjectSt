using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Drawing;

namespace PropertyGrid5
{
    class GameEditor: UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            string whatImage = e.Value.ToString();
            whatImage += ".bmp";
            //getting picture
            Bitmap bmp = (Bitmap)Bitmap.FromFile(whatImage);
            Rectangle destRect = e.Bounds;
            bmp.MakeTransparent();

            //and drawing it
            e.Graphics.DrawImage(bmp, destRect);
        }
    }
}
