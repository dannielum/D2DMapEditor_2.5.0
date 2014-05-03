/*
 * Programmer: (Danny) Lum,Chun-Ming
 * Project: D2D Map Editor
 * Creation Date: 7/18/2007
 * Last Modified: 10/26/2008
 * 
 * 
 * 10/26/2008
 * Beta Version 2.5.0 Released
 * 
 * 10/3/2008
 * Beta Version 2.0.2 Released
 * 
 * 9/12/2008
 * Beta Version 2.0.1 Released
 * 
 * 8/27/2008
 * Beta Version 2.0.0 Released
 * 
 * 4/21/2008
 * Beta Version 1.1.0 Released
 * 
 * 8/4/2007
 * Beta Version 1.0.0 Released
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace D2DMapEditor
{
    public partial class D2DMapEditor : Form
    {
        #region private members

        private Map _map;
        private MapInfo _map_info;
        private History _history;
        private Clipboard _clipboard;
        private Tile[] _tile_library;

        #endregion

        #region member functions

        public D2DMapEditor()
        {
            InitializeComponent();
            Init();
            _map = new Map();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {   // open file
                String d2dFileName = Convert.ToString(args[1]);

                if (Path.GetExtension(d2dFileName) != ".d2d")
                {
                    MessageBox.Show(d2dFileName + " is not a d2d file", "Cannot Load File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        OpenMap(d2dFileName);
                        TilesManagement.RenderTiles(_map, ref _tile_library, pnlTileLibrary);
                        // switch to design view
                        tctrlDesign.SelectedTab = tpgDesign;

                        Cursor.Current = Cursors.Default;

                        saveMapToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open: " + d2dFileName + "\n" + ex.Message);
                    }
                }
            }
            else
            {
                // reset and render map
                ResetMap();
            }

            tbMapName.Text = Path.GetFileNameWithoutExtension(_map.MapFileName);
            ClearSelectedTile();
            ReloadLayers(0);
            RenderMap();
        }

        private void btnUpdateLayer_Click(object sender, EventArgs e)
        {   // update layer
            if (tbLayerName.Text == "")
            {
                MessageBox.Show("Enter the layer name", "Update Layer Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int index = lbLayers.SelectedIndex;
                SetupLayer(index);
                ReloadLayers(index);
            }
        }

        private void btnUpdateTile_Click(object sender, EventArgs e)
        {   // update tile
            _tile_library[Convert.ToInt32(pbSelectedTile.Name)].TileName = tbTileName.Text;
            _tile_library[Convert.ToInt32(pbSelectedTile.Name)].TileWalkable = cbTileWalkable.Checked;

            RenderMap();
        }

        public void tilePicBox_MouseClick(object sender, MouseEventArgs e)
        {   // select tile
            _map_info.SelectedTile = (PictureBox)sender;
            lblTileIDValue.Text = _map_info.SelectedTile.Name;
            tbTileName.Text = _tile_library[Convert.ToInt32(_map_info.SelectedTile.Name)].TileName;
            cbTileWalkable.Checked = _tile_library[Convert.ToInt32(_map_info.SelectedTile.Name)].TileWalkable;
            pbSelectedTile.Image = _map_info.SelectedTile.Image;
            pbSelectedTile.Name = _map_info.SelectedTile.Name;
            gbTileProperties.Enabled = true;

            if (e.Button == MouseButtons.Right)
            {   // show pop up menu
                toolStripMenuItemWalkable.Checked = _tile_library[Convert.ToInt32(_map_info.SelectedTile.Name)].TileWalkable;
                contextMenuStripTile.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void mapPicBox_MouseDown(object sender, MouseEventArgs e)
        {   // mouse click over map
            if (lbLayers.SelectedItem == null)
                return;

            int index = lbLayers.SelectedIndex;

            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 4;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 22;
            int oldX = InputHelper.TrackMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 4;
            int oldY = InputHelper.TrackMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 22;

            InputHelper.MapPosition = new Point(clickedX / _map.TileWidth, clickedY / _map.TileHeight);
            InputHelper.OldMapPosition = new Point(oldX / _map.TileWidth, oldY / _map.TileHeight);
            
            if (!_map.IsWithinArea(index))
                return;

            this.lblMapCoordinate.Text = "Map Coordinate: " + InputHelper.MapPosition.X + ", " + InputHelper.MapPosition.Y;
            this.lblMouse.Text = "Mouse Position: " + clickedX + ", " + clickedY;

            if (e.Button == MouseButtons.Left)
            {
                if (_map_info.SelectedTool == ToolType.selection)
                {   // selection tool
                    EditingTool.Selection(this, _map, _map_info, index, SelectionState.StartSelection);
                }
                else if (_map_info.SelectedTool == ToolType.brush)
                {   // brush tool
                    if (pnlTileLibrary.Controls.ContainsKey(pbSelectedTile.Name) == true)
                    {
                        EditingTool.Brush(this, _map, _history, index, Convert.ToInt32(pbSelectedTile.Name));
                    }
                }
                else if (_map_info.SelectedTool == ToolType.fill)
                {   // fill tool
                    if (pnlTileLibrary.Controls.ContainsKey(pbSelectedTile.Name) == true)
                    {
                        EditingTool.Fill(_map, _map_info, _history, index, Convert.ToInt32(pbSelectedTile.Name));
                    }
                }
                else if (_map_info.SelectedTool == ToolType.selectTile)
                {   // select color tool
                    if (_map.Layers[index].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y] > -1)
                    {
                        Tile selectedTile = _tile_library[_map.Layers[index].LayerData[InputHelper.MapPosition.X, InputHelper.MapPosition.Y]];

                        _map_info.SelectedTile = selectedTile.TilePictureBox;
                        lblTileIDValue.Text = _map_info.SelectedTile.Name;
                        tbTileName.Text = selectedTile.TileName;
                        cbTileWalkable.Checked = selectedTile.TileWalkable;
                        pbSelectedTile.Image = _map_info.SelectedTile.Image;
                        pbSelectedTile.Name = _map_info.SelectedTile.Name;
                        gbTileProperties.Enabled = true;
                    }
                    else
                    {
                        ClearSelectedTile();
                    }
                }
                else if (_map_info.SelectedTool == ToolType.eraser)
                {   // eraser tool
                    EditingTool.Eraser(_map, _map_info, _history, index);
                }

                RenderMap();
            }
            else if (e.Button == MouseButtons.Right)
            {
                _map_info.Selection = new SelectionTool();
                RenderMap();
            }

            if (_history.Undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void mapPicBox_MouseMove(object sender, MouseEventArgs e)
        {   // mouse move over map
            if (lbLayers.SelectedItem == null)
                return;

            int index = lbLayers.SelectedIndex;

            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 4;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 22;

            InputHelper.MapPosition.X = clickedX / _map.TileWidth;
            InputHelper.MapPosition.Y = clickedY / _map.TileHeight;

            if (_map_info.SelectedTool != ToolType.selection && !_map.IsWithinArea(index))
                return;

            _map.ForceWithinArea(index);

            this.lblMapCoordinate.Text = "Map Coordinate: " + InputHelper.MapPosition.X + ", " + InputHelper.MapPosition.Y;
            this.lblMouse.Text = "Mouse Position: " + clickedX + ", " + clickedY;
            
            if (e.Button == MouseButtons.Left)
            {
                if (_map_info.SelectedTool == ToolType.selection)
                {   // selection tool
                    EditingTool.Selection(this, _map, _map_info, index, SelectionState.Dragging);
                }
                else if (_map_info.SelectedTool == ToolType.brush)
                {   // brush tool
                    mapPicBox_MouseDown(sender, e);
                }
                else if (_map_info.SelectedTool == ToolType.selectTile)
                {   // select color tool
                    mapPicBox_MouseDown(sender, e);
                }
                else if (_map_info.SelectedTool == ToolType.eraser)
                {   // eraser tool
                    mapPicBox_MouseDown(sender, e);
                }
            }

            InputHelper.TrackMouse.X = myMouse.X;
            InputHelper.TrackMouse.Y = myMouse.Y;
        }

        private void mapPicBox_MouseUp(object sender, MouseEventArgs e)
        {   // mouse release over map
            if (lbLayers.SelectedItem == null)
                return;

            int index = lbLayers.SelectedIndex;

            Point myMouse = PointToClient(MousePosition);
            int clickedX = myMouse.X - pnlDesign.Location.X - pbMap.Location.X + pnlDesign.AutoScrollPosition.X - 5;
            int clickedY = myMouse.Y - pnlDesign.Location.Y - pbMap.Location.Y + pnlDesign.AutoScrollPosition.Y - 22;

            InputHelper.MapPosition.X = clickedX / _map.TileWidth;
            InputHelper.MapPosition.Y = clickedY / _map.TileHeight;

            _map.ForceWithinArea(index);

            if (_map_info.SelectedTool == ToolType.selection && _map_info.Selection.IsDragging)
            {   // selection tool
                EditingTool.Selection(this, _map, _map_info, index, SelectionState.EndSelection);
            }
        }

        private void d2dMapEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbLayers.SelectedItem == null)
                return;

            int index = lbLayers.SelectedIndex;
            EditingTool.KeyboardShortCut(this, _map, _map_info, _history, index, e);
        }

        private void rbCPP_CheckedChanged(object sender, EventArgs e)
        {   // radio button for C++ is selected
            selectedLanguageToolStripMenuItem.Text = "Generate C++";
            _map_info.Language = ProgrammingLanguage.CPP;
            GenerateCode(_map_info.Language);

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbCS_CheckedChanged(object sender, EventArgs e)
        {   // radio button for C# is selected
            selectedLanguageToolStripMenuItem.Text = "Generate C#";
            _map_info.Language = ProgrammingLanguage.CSharp;
            GenerateCode(_map_info.Language);

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbAS_CheckedChanged(object sender, EventArgs e)
        {   // radio button for ActionScript is selected
            selectedLanguageToolStripMenuItem.Text = "Generate ActionScript";
            _map_info.Language = ProgrammingLanguage.ActionScript;
            GenerateCode(_map_info.Language);

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbXML_CheckedChanged(object sender, EventArgs e)
        {   // radio button for XML is selected
            selectedLanguageToolStripMenuItem.Text = "Generate XML";
            _map_info.Language = ProgrammingLanguage.XML;
            GenerateCode(_map_info.Language);

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void rbXMLLite_CheckedChanged(object sender, EventArgs e)
        {
            selectedLanguageToolStripMenuItem.Text = "Generate XML Lite";
            _map_info.Language = ProgrammingLanguage.XMLLite;
            GenerateCode(_map_info.Language);

            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void tctrlDesign_SelectedIndexChanged(object sender, EventArgs e)
        {   // panel's tab switched
            if (tctrlDesign.SelectedTab.Text == "Designer")
            {
                designViewToolStripMenuItem.Checked = true;
                codeViewToolStripMenuItem.Checked = false;
            }
            else if (tctrlDesign.SelectedTab.Text == "Code")
            {
                codeViewToolStripMenuItem.Checked = true;
                designViewToolStripMenuItem.Checked = false;

                if (rbCPP.Checked)
                    GenerateCode(ProgrammingLanguage.CPP);
                else if (rbCS.Checked)
                    GenerateCode(ProgrammingLanguage.CSharp);
                else if (rbAS.Checked)
                    GenerateCode(ProgrammingLanguage.ActionScript);
                else if (rbXML.Checked)
                    GenerateCode(ProgrammingLanguage.XML);
                else if (rbXMLLite.Checked)
                    GenerateCode(ProgrammingLanguage.XMLLite);
            }
        }

        private void tbMapName_TextChanged(object sender, EventArgs e)
        {
            if (!CodesDictionary.IsValidNameFormat(tbMapName.Text))
            {
                MessageBox.Show("\"" + tbMapName.Text + "\" is not a valid name", "Map Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbMapName.Text = Path.GetFileNameWithoutExtension(_map.MapFileName);
            }
            else
            {
                _map.MapFileName = tbMapName.Text + ".d2d";
            }
        }

        private void D2DMapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {   // exit map editor
            if (!ConfirmCloseMap("Quit D2D Map Editor"))
            {
                e.Cancel = true;
            }
        }

        private void Init()
        {
            // add event handlers
            pbMap.MouseDown += new MouseEventHandler(mapPicBox_MouseDown);
            pbMap.MouseMove += new MouseEventHandler(mapPicBox_MouseMove);
            pbMap.MouseUp += new MouseEventHandler(mapPicBox_MouseUp);
            this.KeyDown += new KeyEventHandler(d2dMapEditor_KeyDown);
            this.KeyPreview = true;
            FormClosing += new FormClosingEventHandler(D2DMapEditor_FormClosing);

            // create tooltips for tools
            ToolTip toolTips = new ToolTip();
            toolTips.AutoPopDelay = 5000;
            toolTips.InitialDelay = 500;
            toolTips.ReshowDelay = 500;
            toolTips.ShowAlways = true;
            toolTips.SetToolTip(btnToolSelection, "Selection(S)");
            toolTips.SetToolTip(btnToolBrush, "Brush(B)");
            toolTips.SetToolTip(btnToolEraser, "Eraser(E)");
            toolTips.SetToolTip(btnToolFill, "Fill(F)");
            toolTips.SetToolTip(btnToolSelectTile, "Select Tile(T)");
            toolTips.SetToolTip(btnAddLayer, "Add Layer");
            toolTips.SetToolTip(btnDeleteLayer, "Delete Layer");
            toolTips.SetToolTip(btnLayerMoveUp, "Move Up");
            toolTips.SetToolTip(btnLayerMoveDown, "Move Down");

            _map_info = new MapInfo();

            // select brush as default tool
            SelectTool(ToolType.brush);

            _history = new History();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;

            _clipboard = new Clipboard();
            pasteToolStripMenuItem.Enabled = false;

            saveMapToolStripMenuItem.Enabled = false;

            _tile_library = new Tile[0];
        }
        #endregion
    }
}
