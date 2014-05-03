using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace D2DMapEditor
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    class CustomCursor
    {
        private Cursor _cursor;

        public CustomCursor(String filename, int xHotSpot, int yHotSpot)
        {
            Bitmap bitmap = new Bitmap(filename);
            _cursor = CreateCursor(bitmap, xHotSpot, yHotSpot);

            bitmap.Dispose();
        }

        public Cursor CursorGraphic
        {
            get
            {
                return _cursor;
            }
            set
            {
                _cursor = value;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(
            ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon,
            ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }
    }
}
