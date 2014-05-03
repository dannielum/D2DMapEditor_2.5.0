using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace D2DMapEditor
{
    public enum ActionType
    {
        Brush = 1,
        Fill = 2,
        Erase = 3,
        Cut = 4,
        Paste = 5,
        AddLayer = 6,
        DeleteLayer = 7,
        SwapLayer = 8
    }

    class HistoryNode : TileNode
    {
        public int Id;
        public ActionType Action;
        public Layer Layer;
        public int LayerIndex;
        public int LayerIndex2;

        public HistoryNode(int id, int layerIndex, int layerIndex2, ActionType action) : base(-1)
        {
            if (action != ActionType.SwapLayer)
            {
                MessageBox.Show("Undo/Redo Failed!", "Internal Error", MessageBoxButtons.OK);
                return;
            }

            Id = -1;
            Layer = null;
            LayerIndex = layerIndex;
            LayerIndex2 = layerIndex2;
            Action = action;
        }

        public HistoryNode(int id, int layerId, Layer layer, int layerIndex, ActionType action) : base(layerId)
        {
            if (!(action == ActionType.AddLayer || action == ActionType.DeleteLayer))
            {
                MessageBox.Show("Undo/Redo Failed!", "Internal Error", MessageBoxButtons.OK);
                return;
            }

            Id = id;
            Layer = layer;
            LayerIndex = layerIndex;
            Action = action;
        }
                
        public HistoryNode(int id, int layerId, int x, int y, int v, ActionType action) : base(layerId, x, y, v)
        {
            Id = id;
            Layer = null;
            LayerIndex = -1;
            Action = action;
        }
    }
}
