using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;

namespace D2DMapEditor
{
    class Layer
    {
        #region properties
        public string Name;
        public int Width;
        public int Height;
        public int Alpha;
        public bool Visible;
        public int LayerId;
        public int[,] LayerData;
        public int[,] ResizeLayerData;
        #endregion
        
        public Layer()
        {
            ResetLayer();
        }

        public Layer(string name, int width, int height, int alpha, int layerId)
        {
            Name = name;
            Width = width;
            Height = height;
            Alpha = alpha;
            Visible = true;
            LayerId = layerId;

            LayerData = new int[Width, Height];

            // initialized _layer
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    LayerData[x, y] = -1;
        }

        #region public members

        public void ResetLayer()
        {
            Name = "Layer1";
            Width = 10;
            Height = 10;
            Alpha = 100;
            Visible = true;
            LayerId = 0;

            LayerData = new int[Width, Height];

            // initialized _layer
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    LayerData[x, y] = -1;
        }

        public int GetLayerAlphaIn8Bits()
        {
            return 255 - (255 * Alpha / 100);
        }

        public float GetLayerAlphaInPercent()
        {
            return (float)Alpha / 100;
        }

        public void SetupLayer(string newLayerName, int newLayerWidth, int newLayerHeight, int alpha, bool visible, int layerId)
        {
            Name = newLayerName;

            ResizeLayer(newLayerWidth, newLayerHeight);

            // update Layer size
            Width = newLayerWidth;
            Height = newLayerHeight;

            Alpha = alpha;
            Visible = visible;
            LayerId = layerId;
        }

        public void ResizeLayer(int newWidth, int newHeight)
        {
            ResizeLayerData = new int[newWidth, newHeight];

            // initialized _resize_layer
            for (int x = 0; x < newWidth; x++)
                for (int y = 0; y < newHeight; y++)
                    ResizeLayerData[x, y] = -1;

            int w = 0;
            int h = 0;

            if (newWidth < Width)
                w = newWidth;
            else
                w = Width;

            if (newHeight < Height)
                h = newHeight;
            else
                h = Height;

            // copy possible tiles to the _resize_layer
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    ResizeLayerData[x, y] = LayerData[x, y];

            // now resize the (old) _layer
            LayerData = new int[newWidth, newHeight];

            // initialized _layer
            for (int x = 0; x < newWidth; x++)
                for (int y = 0; y < newHeight; y++)
                    LayerData[x, y] = -1;

            // and finally generate (new) _layer
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    LayerData[x, y] = ResizeLayerData[x, y];
        }

        #endregion
    }
}
