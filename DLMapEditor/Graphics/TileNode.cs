using System;
using System.Collections.Generic;
using System.Text;

namespace D2DMapEditor
{
    class TileNode
    {
        public int LayerId;
        public int X;
        public int Y;
        public int Value;

        public TileNode(int layerId)
        {
            LayerId = layerId;
            X = -1;
            Y = -1;
            Value = -1;
        }

        public TileNode(int layerId, int x, int y, int v)
        {
            LayerId = layerId;
            X = x;
            Y = y;
            Value = v;
        }
    }
}
