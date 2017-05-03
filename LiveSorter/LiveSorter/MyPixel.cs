using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSorter
{
    public class MyPixel : IComparable
    {
        public Color mColor;
        public double mBrightness;

        public MyPixel()
        {
            mColor = Color.Black;
            mBrightness = 0f;
        }

        public MyPixel(Color c)
        {
            mColor = c;
            // Stolen formula. 
            mBrightness = ((0.2126 * c.R) + (0.7152 * c.G) + (0.0722 * c.B));
        }

        public int CompareTo(object p)
        {
            if (!(p is MyPixel))
            {
                throw new NotImplementedException();
            }
            return ((MyPixel)p).mBrightness.CompareTo(mBrightness);
        }
    }
}
