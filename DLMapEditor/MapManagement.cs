using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;

namespace D2DMapEditor
{
    public partial class D2DMapEditor
    {
        public void ResetMap()
        {   // reset map
            _map = new Map();

            if (_tile_library != null)
            {
                Array.Clear(_tile_library, 0, _tile_library.Length);
                Array.Resize(ref _tile_library, 0);
            }

            pbMap.Width = _map.GetMapWidthInPixels();
            pbMap.Height = _map.GetMapHeightInPixels();
        }

        public void SetupMap(string mapName, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            _map.SetupMap(mapName, mapWidth, mapHeight, tileWidth, tileHeight);

            TilesManagement.RenderTiles(_map, ref _tile_library, pnlTileLibrary);
            RenderMap();
            ClearSelectedTile();
            tbMapName.Text = Path.GetFileNameWithoutExtension(_map.MapFileName);

            _history.ResetHistory();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        public void OpenMap(string fileName)
        {   // open saved map
            _map.OpenMap(fileName, _map);
            ClearSelectedTile();

            TilesManagement.LoadTiles(this, _map, ref _tile_library, pnlTileLibrary, fileName);
            tbMapName.Text = Path.GetFileNameWithoutExtension(_map.MapFileName);

            _history.ResetHistory();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
        }

        public void SaveMap(string fileName)
        {   // save map
            Cursor.Current = Cursors.WaitCursor;
            _map.SaveMap(fileName);
            TilesManagement.SaveTiles(ref _tile_library, fileName);
            tbMapName.Text = Path.GetFileNameWithoutExtension(_map.MapFileName);
            Cursor.Current = Cursors.Default;
        }
        
        public void RenderMap()
        {   // render map
            _map.RenderMap(ref _tile_library, _map_info, lbLayers, pbMap, pbMapSmall);
        }

        public void AddTiles(string folderName)
        {   // add tiles to tile library
            TilesManagement.AddTiles(this, _map, ref _tile_library, pnlTileLibrary, folderName);
        }

        public void ClearTiles()
        {   // clear all tiles in the tile library
            TilesManagement.ClearTiles(this, _map, ref _tile_library, pnlTileLibrary);
        }

        public void ClearSelectedTile()
        {   // deselect tile
            gbTileProperties.Enabled = false;
            _map_info.SelectedTile = null;
            lblTileIDValue.Text = "";
            tbTileName.Text = "";
            pbSelectedTile.Image = null;
            pbSelectedTile.Name = "";
        }

        public void SelectTool(ToolType tool)
        {   // select tool
            // end dragging
            _map_info.Selection.IsDragging = false;

            if (tool == ToolType.selection)
            {
                _map_info.SelectedTool = ToolType.selection;
                _map_info.Cursor = CursorType.selection;
                btnToolSelection.FlatStyle = FlatStyle.Flat;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.brush)
            {
                _map_info.SelectedTool = ToolType.brush;
                _map_info.Cursor = CursorType.brush;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Flat;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.fill)
            {
                _map_info.SelectedTool = ToolType.fill;
                _map_info.Cursor = CursorType.fill;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Flat;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.selectTile)
            {
                _map_info.SelectedTool = ToolType.selectTile;
                _map_info.Cursor = CursorType.selectColor;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Flat;
                btnToolEraser.FlatStyle = FlatStyle.Popup;
            }
            else if (tool == ToolType.eraser)
            {
                _map_info.SelectedTool = ToolType.eraser;
                _map_info.Cursor = CursorType.eraser;
                btnToolSelection.FlatStyle = FlatStyle.Popup;
                btnToolBrush.FlatStyle = FlatStyle.Popup;
                btnToolFill.FlatStyle = FlatStyle.Popup;
                btnToolSelectTile.FlatStyle = FlatStyle.Popup;
                btnToolEraser.FlatStyle = FlatStyle.Flat;
            }

            // change mouse cursor
            if (_map_info.Cursor == CursorType.selection)
            {
                pbMap.Cursor = Cursors.Cross;
            }
            else if (_map_info.Cursor == CursorType.brush)
            {
                String filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\System\\Graphics\\Cursors\\brush.png";
                if (File.Exists(filename))
                {
                    CustomCursor selectionCursor = new CustomCursor(filename, 3, 18);
                    pbMap.Cursor = selectionCursor.CursorGraphic;
                }
                else
                {
                    pbMap.Cursor = Cursors.Cross;
                }
            }
            else if (_map_info.Cursor == CursorType.fill)
            {
                String filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\System\\Graphics\\Cursors\\fill.png";
                if (File.Exists(filename))
                {
                    CustomCursor selectionCursor = new CustomCursor(filename, 10, 10);
                    pbMap.Cursor = selectionCursor.CursorGraphic;
                }
                else
                {
                    pbMap.Cursor = Cursors.Cross;
                }
            }
            else if (_map_info.Cursor == CursorType.selectColor)
            {
                String filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\System\\Graphics\\Cursors\\selecttile.png";
                if (File.Exists(filename))
                {
                    CustomCursor selectionCursor = new CustomCursor(filename, 3, 18);
                    pbMap.Cursor = selectionCursor.CursorGraphic;
                }
                else
                {
                    pbMap.Cursor = Cursors.Cross;
                }
            }
            else if (_map_info.Cursor == CursorType.eraser)
            {
                String filename = Path.GetDirectoryName(Application.ExecutablePath) + "\\System\\Graphics\\Cursors\\eraser.png";
                if (File.Exists(filename))
                {
                    CustomCursor selectionCursor = new CustomCursor(filename, 3, 15);
                    pbMap.Cursor = selectionCursor.CursorGraphic;
                }
                else
                {
                    pbMap.Cursor = Cursors.Cross;
                }
            }
        }
    }
}
