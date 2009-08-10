using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit2DEngine
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public static Size Empty 
        { 
            get
            { 
                return new Size(0,0);
            }
        }

        public Size()
        {
            new Size(0, 0);
        }

        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public static Size operator +(Size s1, Size s2)
        {
            Size newSize = new Size(s1.Width + s2.Width, s1.Height + s2.Height);
            return newSize;
        }

        public static Size operator -(Size s1, Size s2)
        {
            Size newSize = new Size(s1.Width - s2.Width, s1.Height - s2.Height);
            return newSize;
        }
    }
}
