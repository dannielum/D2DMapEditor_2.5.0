using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace D2DMapEditor
{
    class Map
    {
        public string MapFileName;
        public int TileWidth;
        public int TileHeight;
        public List<Layer> Layers = new List<Layer>();
                
        public Map()
        {
            ResetMap();
        }

        public int GetMapWidth()
        {
            int width = -10000;

            foreach (Layer layer in Layers)
                width = (int)Math.Max(width, layer.Width);

            return width;
        }

        public int GetMapHeight()
        {
            int height = -10000;

            foreach (Layer layer in Layers)
                height = (int)Math.Max(height, layer.Height);

            return height;
        }

        public int GetMapWidthInPixels()
        {
            return GetMapWidth() * TileWidth;
        }

        public int GetMapHeightInPixels()
        {
            return GetMapHeight() * TileHeight;
        }

        public Boolean IsWithinArea(int index)
        {
            return !(InputHelper.MapPosition.X < 0 || InputHelper.MapPosition.Y < 0 || InputHelper.MapPosition.X >= Layers[index].Width || InputHelper.MapPosition.Y >= Layers[index].Height);
        }

        public void ForceWithinArea(int index)
        {
            if (InputHelper.MapPosition.X < 0)
                InputHelper.MapPosition.X = 0;
            else if (InputHelper.MapPosition.X >= Layers[index].Width)
                InputHelper.MapPosition.X = Layers[index].Width;

            if (InputHelper.MapPosition.Y < 0)
                InputHelper.MapPosition.Y = 0;
            else if (InputHelper.MapPosition.Y >= Layers[index].Height)
                InputHelper.MapPosition.Y = Layers[index].Height;
        }

        public void ResetMap()  
        {
            MapFileName = "Untitled.d2d";
            TileWidth = 32;
            TileHeight = 32;
            Layers = new List<Layer>();

            //add first default layer
            Layers.Add(new Layer());
        }

        public void RenderMap(ref Tile[] tileLibrary, MapInfo mapInfo, ListBox lbLayers, PictureBox pbMap, PictureBox pbMapSmall)
        {   // render map
            if (lbLayers.SelectedItem == null)
                return;

            Bitmap bmp = new Bitmap(pbMap.Width, pbMap.Height);
            pbMap.Image = bmp;
            pbMapSmall.Image = bmp;

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            gfx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

            int index = 0;
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                index = i;
                //int index = lbLayers.SelectedIndex;

                if (Layers[index].Visible == false)
                    continue;

                pbMap.Width = TileWidth * GetMapWidth();
                pbMap.Height = TileHeight * GetMapHeight();

                for (int x = 0; x < Layers[index].Width; x++)
                {
                    for (int y = 0; y < Layers[index].Height; y++)
                    {
                        if (Layers[index].LayerData[x, y] != -1)
                        {
                            if (tileLibrary != null && tileLibrary.Length > Layers[index].LayerData[x, y])
                            {
                                PictureBox tile = tileLibrary[Layers[index].LayerData[x, y]].TilePictureBox;
                                if (tile != null)
                                {
                                    if (Layers[index].Alpha == 100)
                                    {
                                        gfx.DrawImage(tile.Image,
                                                      x * TileWidth,
                                                      y * TileHeight,
                                                      TileWidth,
                                                      TileHeight);
                                    }
                                    else if (Layers[index].Alpha > 0)
                                    {   // alpha 1-99
                                        ColorMatrix cm = new ColorMatrix();
                                        cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix44 = 1;
                                        cm.Matrix33 = Layers[index].GetLayerAlphaInPercent();

                                        ImageAttributes AlphaImageAttribute = new ImageAttributes();
                                        AlphaImageAttribute.SetColorMatrix(cm);

                                        Bitmap imageBitmap = new Bitmap(tile.Image);
                                        gfx.DrawImage(imageBitmap, new Rectangle(x * TileWidth,
                                                                                 y * TileHeight,
                                                                                 TileWidth,
                                                                                 TileHeight),
                                                      0, 0, imageBitmap.Width, imageBitmap.Height, GraphicsUnit.Pixel,
                                                      AlphaImageAttribute);
                                    }
                                    /*
                                    // draw alpha
                                    if (Layers[index].Alpha < 100)
                                    {
                                        gfx.FillRectangle(new SolidBrush(Color.FromArgb(Layers[index].GetLayerAlphaIn8Bits(), Color.LightGray)),
                                                          x * TileWidth,
                                                          y * TileHeight,
                                                          TileWidth,
                                                          TileHeight);
                                    }
                                    */

                                    if (mapInfo.ShowWalkableOn && !tileLibrary[Layers[index].LayerData[x, y]].TileWalkable)
                                    {   // show walkble tile
                                        gfx.FillRectangle(new SolidBrush(Color.FromArgb(128, 50, 50, 255)),
                                                          x * TileWidth + 2,
                                                          y * TileHeight + 2,
                                                          TileWidth - 4, TileHeight - 4);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (GetMapWidth() > GetMapHeight())
            {
                int mh = GetMapHeight() * 150 / GetMapWidth();
                pbMapSmall.Width = 150;
                pbMapSmall.Height = mh;
            }
            else if (GetMapWidth() < GetMapHeight())
            {
                int mw = GetMapWidth() * 150 / GetMapHeight();
                pbMapSmall.Width = mw;
                pbMapSmall.Height = 150;
            }
            else
            {
                pbMapSmall.Width = 150;
                pbMapSmall.Height = 150;
            }

            pbMapSmall.Refresh();

            if (mapInfo.ShowGridOn)
            {   // draw grids
                for (int a = 1; a < GetMapWidth(); a++)
                {   // draw vertical lines
                    gfx.DrawLine(Pens.Gray, a * TileWidth, 0, a * TileWidth, GetMapHeight() * TileHeight);
                }

                for (int b = 1; b < GetMapHeight(); b++)
                {   // draw horizontal lines
                    gfx.DrawLine(Pens.Gray, 0, b * TileHeight, GetMapWidth() * TileWidth, b * TileHeight);
                }
            }

            if (mapInfo.Selection.BottomRightX > GetMapWidth())
                mapInfo.Selection.BottomRightX = GetMapWidth();
            if (mapInfo.Selection.BottomRightY > GetMapHeight())
                mapInfo.Selection.BottomRightY = GetMapHeight();

            Rectangle marquee = new Rectangle(mapInfo.Selection.TopLeftX * TileWidth,
                                              mapInfo.Selection.TopLeftY * TileHeight,
                                              (mapInfo.Selection.BottomRightX - mapInfo.Selection.TopLeftX) * TileWidth,
                                              (mapInfo.Selection.BottomRightY - mapInfo.Selection.TopLeftY) * TileHeight);

            Pen mypen = new Pen(new SolidBrush(Color.Blue));
            mypen.Color = Color.Blue;
            mypen.Width = 2;
            mypen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            mypen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            gfx.DrawRectangle(mypen, marquee);
            gfx.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Aqua)), marquee);

            gfx.Dispose();
            pbMap.Refresh();
        }

        public void SetupMap(string mapName, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            MapFileName = mapName + ".d2d";
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Layers = new List<Layer>();
            Layers.Add(new Layer("Layer1", mapWidth, mapHeight, 100, 0));
        }

        public void SaveMap(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlTextWriter w = new XmlTextWriter(fs, null);

            w.WriteStartDocument();
            w.WriteComment("This map is generated by D2D Map Editor");
            w.WriteStartElement("D2DMap");
            w.WriteAttributeString("MapName", Path.GetFileNameWithoutExtension(MapFileName));
            w.WriteAttributeString("TileWidth", TileWidth.ToString());
            w.WriteAttributeString("TileHeight", TileHeight.ToString());
            w.WriteAttributeString("TotalLayers", Layers.Count.ToString());
            w.WriteAttributeString("Version", "Beta2.5");

            for (int index = 0; index < Layers.Count; index++)
            {
                w.WriteStartElement("Layer");
                w.WriteAttributeString("LayerName", Layers[index].Name.ToString());
                w.WriteAttributeString("LayerPosition", index.ToString());
                w.WriteAttributeString("LayerWidth", Layers[index].Width.ToString());
                w.WriteAttributeString("LayerHeight", Layers[index].Height.ToString());
                w.WriteAttributeString("LayerAlpha", Layers[index].Alpha.ToString());

                for (int row = 0; row < Layers[index].Height; row++)
                {
                    // write the first row of the map
                    w.WriteStartElement("Row");
                    w.WriteAttributeString("Position", row.ToString());

                    for (int col = 0; col < Layers[index].Width; col++)
                    {
                        w.WriteStartElement("Column");
                        w.WriteAttributeString("Position", col.ToString());
                        w.WriteString(Layers[index].LayerData[col, row].ToString());
                        w.WriteEndElement();
                    }

                    w.WriteEndElement();
                }

                w.WriteEndElement();
            }

            // close the root element
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Close();
            fs.Close();

            MapFileName = fileName;
        }

        public void OpenMap(string fileName, Map map)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlTextReader r = new XmlTextReader(fs);

            int row = -1;
            int col = -1;
            int layerPosition = -1;

            // parse file and read each node
            while (r.Read())
            {
                if (r.NodeType.ToString() == "Element")
                {   // element node
                    if (r.Name == "D2DMap")
                    {   // header node
                        string version = r.GetAttribute("Version");
                        if (version != "Beta2.5")
                        {
                            r.Close();
                            openMapOld2_0(fileName);
                            return;
                        }

                        Layers.Clear();
                        MapFileName = r.GetAttribute("MapName") + ".d2d";
                        TileWidth = Convert.ToInt32(r.GetAttribute("TileWidth"));
                        TileHeight = Convert.ToInt32(r.GetAttribute("TileHeight"));
                    }
                    else if (r.Name == "Layer")
                    {
                        string layerName = r.GetAttribute("LayerName");
                        int layerWidth = Convert.ToInt32(r.GetAttribute("LayerWidth"));
                        int layerHeight = Convert.ToInt32(r.GetAttribute("LayerHeight"));
                        int layerAlpha = Convert.ToInt32(r.GetAttribute("LayerAlpha"));
                        layerPosition = Convert.ToInt32(r.GetAttribute("LayerPosition"));

                        AddLayer(layerName, layerWidth, layerHeight, layerAlpha, layerPosition);
                        Layers[layerPosition].LayerData = new int[layerWidth, layerHeight];

                        // initialized layer
                        for (int x = 0; x < Layers[layerPosition].Width; x++)
                            for (int y = 0; y < Layers[layerPosition].Height; y++)
                                Layers[layerPosition].LayerData[x, y] = -1;
                    }
                    else if (r.Name == "Row")
                    {
                        row = Convert.ToInt32(r.GetAttribute("Position"));
                    }
                    else if (r.Name == "Column")
                    {
                        col = Convert.ToInt32(r.GetAttribute("Position"));
                    }
                }
                else if (r.NodeType.ToString() == "Text")
                {
                    // add to the cell of last layer added
                    Layers[layerPosition].LayerData[col, row] = int.Parse(r.Value);
                }
            }

            r.Close();
            fs.Close();
        }
        
        private void openMapOld2_0(string fileName)
        {
            MessageBox.Show("This is an older D2D 2.0.x file", "Older D2D file detected");

            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlTextReader r = new XmlTextReader(fs);

            int row = -1;
            int col = -1;

            // parse file and read each node
            while (r.Read())
            {
                if (r.NodeType.ToString() == "Element")
                {   // element node
                    if (r.Name == "D2DMap")
                    {   // header node
                        Layers.Clear();

                        string layerName = r.GetAttribute("MapName");
                        int layerWidth = Convert.ToInt32(r.GetAttribute("MapWidth"));
                        int layerHeight = Convert.ToInt32(r.GetAttribute("MapHeight"));

                        MapFileName = r.GetAttribute("MapName") + ".d2d";
                        TileWidth = Convert.ToInt32(r.GetAttribute("TileWidth"));
                        TileHeight = Convert.ToInt32(r.GetAttribute("TileHeight")); 
                        AddLayer(layerName, layerWidth, layerHeight, 100, 0);
                        Layers[0].LayerData = new int[layerWidth, layerHeight];

                        // initialized _map
                        for (int x = 0; x < Layers[0].Width; x++)
                            for (int y = 0; y < Layers[0].Height; y++)
                                Layers[0].LayerData[x, y] = -1;
                    }
                    else if (r.Name == "Row")
                    {
                        row = Convert.ToInt32(r.GetAttribute("Position"));
                    }
                    else if (r.Name == "Column")
                    {
                        col = Convert.ToInt32(r.GetAttribute("Position"));
                    }
                }
                else if (r.NodeType.ToString() == "Text")
                {
                    Layers[0].LayerData[col, row] = int.Parse(r.Value);
                }
            }

            r.Close();
            fs.Close();

            MessageBox.Show("Please save this file to convert it to current D2D version map", "Older D2D file opened successfully");
        }

        public Boolean ExportMap(ref Tile[] tileLibrary, string path, ListBox lbLayers, ExportLayer exportLayer, ProgrammingLanguage language, ImageFormat format)
        {
            path = path + "\\D2D_" + Path.GetFileNameWithoutExtension(MapFileName);
            Directory.CreateDirectory(path);
            StreamWriter sw;
            string layerPath;

            switch (exportLayer)
            {  
                case ExportLayer.SelectedOnly:
                    int index = FindLayerIndexWithName(lbLayers.SelectedItem.ToString());
                    if (index == -1)
                    {
                        MessageBox.Show("No layer selected.", "Export Failed");
                        return false;
                    }
                    layerPath = path + "\\" + Layers[index].Name + ".txt";
                    sw = new StreamWriter(layerPath);
                    sw.Write(CodesGenerator.GenerateCodes(index, this, format, tileLibrary, language));
                    sw.Flush();
                    sw.Close();
                    break;
                case ExportLayer.VisibleOnly:
                    foreach (Layer layer in Layers)
                    {
                        if (layer.Visible)
                        {
                            layerPath = path + "\\" + layer.Name + ".txt";
                            sw = new StreamWriter(layerPath);
                            sw.Write(CodesGenerator.GenerateCodes(FindLayerIndexWithId(layer.LayerId), this, format, tileLibrary, language));
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    break;
                case ExportLayer.All:
                    foreach (Layer layer in Layers)
                    {
                        layerPath = path + "\\" + layer.Name + ".txt";
                        sw = new StreamWriter(layerPath);
                        sw.Write(CodesGenerator.GenerateCodes(FindLayerIndexWithId(layer.LayerId), this, format, tileLibrary, language));
                        sw.Flush();
                        sw.Close();
                    }
                    break;
                default:
                    break;
            }

            // save tiles
            path = path + "\\" + Path.GetFileNameWithoutExtension(MapFileName);
            Directory.CreateDirectory(path);
            for (int i = 0; i < tileLibrary.Length; i++)
                TilesManagement.SaveTileImage(ref tileLibrary, i, path, format);

            return true;
        }

        #region Layer Utilities
        public int FindLayerIndexWithId(int layerId)
        {
            int index = -1;
            int i = 0;
            foreach (Layer layer in Layers)
            {
                if (layer.LayerId == layerId)
                {
                    index = i;
                    break;
                }

                i++;
            }

            return index;
        }

        public int FindLayerIndexWithName(string layerName)
        {
            int index = -1;
            int i = 0;
            foreach (Layer layer in Layers)
            {
                if (layer.Name == layerName)
                {
                    index = i;
                    break;
                }

                i++;
            }

            return index;
        }

        public int GetLastLayerId()
        {
            int maxIndex = -1;
            foreach (Layer layer in Layers)
            {
                if (layer.LayerId > maxIndex)
                {
                    maxIndex = layer.LayerId;
                }
            }

            return maxIndex;
        }

        public string GetNewLayerDeafultName()
        {
            int index = GetLastLayerId();
            if (index < 1)
                index = 1;

            string layername = "Layer" + index.ToString();
            int isFound = FindLayerIndexWithName(layername);

            while (isFound != -1)
            {
                index++;
                layername = "Layer" + index.ToString();
                isFound = FindLayerIndexWithName(layername);
            }

            return layername;
        }

        public void AddLayer()
        {
            Layers.Add(new Layer());
        }

        public void AddLayer(Layer layer, int index)
        {
            Layers.Insert(index, layer);
        }

        public void AddLayer(string name, int width, int height, int alpha, int layerId)
        {
            Layers.Add(new Layer(name, width, height, alpha, layerId));
        }

        public void SwapLayers(Map map, int index1, int index2)
        {
            Layer temp = map.Layers[index1];
            map.Layers[index1] = map.Layers[index2];
            map.Layers[index2] = temp;
        }
        #endregion
    }
}
