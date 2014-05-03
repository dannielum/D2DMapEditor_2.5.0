using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace D2DMapEditor
{
    public enum ToolType
    {
        brush = 1,
        selectTile = 2,
        eraser = 3,
        selection = 4,
        fill = 5
    }

    public enum CursorType
    {
        arrow = 1,
        wait = 2,
        brush = 3,
        selectColor = 4,
        eraser = 5,
        selection = 6,
        fill = 7
    }

    class MapInfo
    {
        public PictureBox SelectedTile;
        public ToolType SelectedTool;
        public CursorType Cursor;
        public bool ShowGridOn;
        public bool ShowWalkableOn;
        public SelectionTool Selection;
        public ImageFormat Format;
        public ProgrammingLanguage Language;

        public MapInfo()
        {
            Init();
        }

        private void Init()
        {
            // initialized some variables
            ShowGridOn = true;
            ShowWalkableOn = false;
            SelectedTile = null;
            Selection = new SelectionTool();
            Format = ImageFormat.Bmp;
            Language = ProgrammingLanguage.CPP;
        }

        public string GetTileExtension()
        {
            string formatExtension = ".bmp";
            if (Format == ImageFormat.Bmp)
                formatExtension = ".bmp";
            else if (Format == ImageFormat.Jpeg)
                formatExtension = ".jpg";
            else if (Format == ImageFormat.Gif)
                formatExtension = ".gif";
            else if (Format == ImageFormat.Png)
                formatExtension = ".png";

            return formatExtension;
        }

        public static string GetTileExtension(ImageFormat format)
        {
            string formatExtension = ".bmp";
            if (format == ImageFormat.Bmp)
                formatExtension = ".bmp";
            else if (format == ImageFormat.Jpeg)
                formatExtension = ".jpg";
            else if (format == ImageFormat.Gif)
                formatExtension = ".gif";
            else if (format == ImageFormat.Png)
                formatExtension = ".png";

            return formatExtension;
        }
    }
}
