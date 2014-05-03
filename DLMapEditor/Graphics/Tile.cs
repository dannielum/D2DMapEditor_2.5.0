using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace D2DMapEditor
{
    class Tile
    {
        #region properties

        public int TileID;
        public string TileName;
        public bool TileWalkable;
        public int TileWidth;
        public int TileHeight;
        public string TilePath;
        public PictureBox TilePictureBox;

        #endregion

        public Tile()
        {
            TileWalkable = true;
            TileWidth = 0;
            TileHeight = 0;
            TilePath = "";
        }
    }
}
