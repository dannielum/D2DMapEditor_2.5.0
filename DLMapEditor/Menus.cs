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

namespace D2DMapEditor
{
    public partial class D2DMapEditor
    {
        #region Context Menu Item functions
        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {   // delete selected tile
            if (MessageBox.Show("Delete this tile?", "Delete Tile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                TilesManagement.DeleteSelectedTile(this, _map, ref _tile_library, pnlTileLibrary, Convert.ToInt32(_map_info.SelectedTile.Name));
            }
        }

        private void toolStripMenuItemWalkable_Click(object sender, EventArgs e)
        {   // toggle walkable tile
            bool isWalkable = !_tile_library[Convert.ToInt32(_map_info.SelectedTile.Name)].TileWalkable;
            _tile_library[Convert.ToInt32(_map_info.SelectedTile.Name)].TileWalkable = isWalkable;
            cbTileWalkable.Checked = isWalkable;

            RenderMap();
        }
        #endregion

        #region Menu File
        private void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // setup new map
            if (!ConfirmCloseMap("Create New Map"))
                return;

            MapSetup MapSetupWindow = new MapSetup(this);
            MapSetupWindow.ShowDialog();

            saveMapToolStripMenuItem.Enabled = false;
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // open saved map
            openMapDialog.InitialDirectory = Application.ExecutablePath;
            DialogResult openMap = this.openMapDialog.ShowDialog();
            if (openMap == DialogResult.OK)
            {   // open Map
                string d2dFileName = this.openMapDialog.FileName;

                if (Path.GetExtension(d2dFileName) != ".d2d")
                {
                    MessageBox.Show(d2dFileName + " is not a d2d file", "Cannot Load File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        OpenMap(this.openMapDialog.FileName);
                        TilesManagement.RenderTiles(_map, ref _tile_library, pnlTileLibrary);
                        // switch to design view
                        tctrlDesign.SelectedTab = tpgDesign;

                        Cursor.Current = Cursors.Default;

                        ReloadLayers(0);
                        RenderMap();
                        saveMapToolStripMenuItem.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open: " + this.openMapDialog.FileName + "\n" + ex.Message);
                    }
                }
            }
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map
            try
            {
                SaveMap(_map.MapFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to save: " + _map.MapFileName + "\n" + ex.Message);
            }
        }

        private void saveMapAsToolStripMenuItem_Click(object sender, EventArgs e)
        {   // save map as
            PromptSaveNewMap();
        }

        private void exportMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportMap ExportMapWindow = new ExportMap();
            if (ExportMapWindow.ShowDialog() == DialogResult.OK)
            {
                DialogResult exportMap = this.folderBrowserDialogLoadTiles.ShowDialog();
                if (exportMap == DialogResult.OK)
                {   // save Map
                    try
                    {
                        if (_map.ExportMap(ref _tile_library, folderBrowserDialogLoadTiles.SelectedPath, lbLayers,
                                           ExportMapWindow.LayerOption, ExportMapWindow.LanguageOption, ExportMapWindow.TileOption))
                        {
                            MessageBox.Show(Path.GetFileNameWithoutExtension(_map.MapFileName) + " is exported successfully!", 
                                            "Export Done!", MessageBoxButtons.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fail to Export:\n" + ex.Message);
                    }
                }
            }
        }

        private void exitDLMapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {   // exit map editor
            Application.Exit();
        }
        #endregion

        #region Menu Edit
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_history.Undo.Count < 1)
                return;

            _history.DoUndo(_map, this);
            RenderMap();

            redoToolStripMenuItem.Enabled = true;
            if (_history.Undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_history.Redo.Count < 1)
                return;

            _history.DoRedo(_map, this);
            RenderMap();

            undoToolStripMenuItem.Enabled = true;
            if (_history.Redo.Count > 0)
                redoToolStripMenuItem.Enabled = true;
            else
                redoToolStripMenuItem.Enabled = false;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = lbLayers.SelectedIndex;

            if (index < 0)
                return;

            if (_clipboard.CopyToClipboard(_map, _map_info, _history, index, true))
            {
                pasteToolStripMenuItem.Enabled = true;
                RenderMap();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = lbLayers.SelectedIndex;

            if (index < 0)
                return;

            if (_clipboard.CopyToClipboard(_map, _map_info, _history, index, false))
                pasteToolStripMenuItem.Enabled = true;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = lbLayers.SelectedIndex;
            if (_clipboard.PasteClipboard(_map, _map_info, _history, index))
            {
                if (_history.Undo.Count > 0)
                    undoToolStripMenuItem.Enabled = true;
                else
                    undoToolStripMenuItem.Enabled = false;

                RenderMap();
            }
        }
        #endregion

        #region Menu View
        private void designViewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {   // switch to design view
            tctrlDesign.SelectedTab = tpgDesign;
            codeViewToolStripMenuItem.Checked = false;
            designViewToolStripMenuItem.Checked = true;
        }

        private void codeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {   // switch to code view
            tctrlDesign.SelectedTab = tpgCode;
            designViewToolStripMenuItem.Checked = false;
            codeViewToolStripMenuItem.Checked = true;
            GenerateCode(_map_info.Language);
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {   // toggle view grid 
            bool isChecked = gridToolStripMenuItem.Checked;
            if (isChecked)
            {
                gridToolStripMenuItem.Checked = false;
                _map_info.ShowGridOn = false;
            }
            else
            {
                gridToolStripMenuItem.Checked = true;
                _map_info.ShowGridOn = true;
            }

            RenderMap();
        }

        private void showWalkableToolStripMenuItem_Click(object sender, EventArgs e)
        {   // toggle view walkable
            bool isChecked = showWalkableToolStripMenuItem.Checked;
            if (isChecked)
            {
                showWalkableToolStripMenuItem.Checked = false;
                _map_info.ShowWalkableOn = false;
            }
            else
            {
                showWalkableToolStripMenuItem.Checked = true;
                _map_info.ShowWalkableOn = true;
            }

            RenderMap();
        }
        #endregion

        #region Menu Select
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tctrlDesign.SelectedTab == tpgDesign)
            {
                int index = lbLayers.SelectedIndex;
                if (index < 0)
                    return;
                EditingTool.Selection(this, _map, _map_info, index, SelectionState.SelectAll);
            }
            else if (tctrlDesign.SelectedTab == tpgCode)
            {
                tbCode.Focus();
                tbCode.SelectAll();
            }
        }

        private void deselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tctrlDesign.SelectedTab == tpgDesign)
            {
                _map_info.Selection = new SelectionTool();
                RenderMap();
            }
            else if (tctrlDesign.SelectedTab == tpgCode)
            {
                tbCode.Focus();
                tbCode.DeselectAll();
            }
        }
        #endregion

        #region Menu Layer
        
        private void addLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLayer();
        }

        private void deleteLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteLayer();
        }

        #endregion
        
        #region Menu Tiles
        private void addTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {   // add tiles to tile library
            DialogResult loadTiles = this.folderBrowserDialogLoadTiles.ShowDialog();
            if (loadTiles == DialogResult.OK)
            {   // load tiles
                string folderName = this.folderBrowserDialogLoadTiles.SelectedPath;

                AddTiles(folderName);
            }
        }

        private void clearTilesToolStripMenuItem_Click(object sender, EventArgs e)
        {   // clear tile library
            Cursor.Current = Cursors.WaitCursor;

            if (MessageBox.Show("Clear the Tile Library?", "Clear Tile Library", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ClearTiles();

                TilesManagement.RenderTiles(_map, ref _tile_library, pnlTileLibrary);
                RenderMap();
            }

            Cursor.Current = Cursors.Default;
        }

        private void deleteSelectedTileToolStripMenuItem_Click(object sender, EventArgs e)
        {   // delete selected tile
            if (_map_info.SelectedTile == null)
            {
                MessageBox.Show("Select a tile!");
            }
            else
            {
                if (MessageBox.Show("Delete this tile?", "Delete Tile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    TilesManagement.DeleteSelectedTile(this, _map, ref _tile_library, pnlTileLibrary, Convert.ToInt32(_map_info.SelectedTile.Name));
                }
            }
        }

        private void tilesEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {   // import an image and cut into tiles 
            ImportTilesEditor TilesEditorWindow = new ImportTilesEditor(this);
            TilesEditorWindow.LoadFromParentForm();
            TilesEditorWindow.ShowDialog();
        }
        #endregion

        #region Menu Code
        private void selectedLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate codes with selected language 
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
        }

        private void cArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate C++ codes
            _map_info.Language = ProgrammingLanguage.CPP;
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
            rbCPP.Checked = true;
        }

        private void cArrayToolStripMenuItem1_Click(object sender, EventArgs e)
        {   // generate C# codes
            _map_info.Language = ProgrammingLanguage.CSharp;
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
            rbCS.Checked = true;
        }

        private void actionScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate AS codes
            _map_info.Language = ProgrammingLanguage.ActionScript;
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
            rbAS.Checked = true;
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate xml
            _map_info.Language = ProgrammingLanguage.XML;
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
            rbXML.Checked = true;
        }

        private void xMLLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {   // generate xml lite
            _map_info.Language = ProgrammingLanguage.XMLLite;
            GenerateCode(_map_info.Language);
            tctrlDesign.SelectedTab = tpgCode;
            rbXMLLite.Checked = true;
        }
        #endregion

        #region Menu Help
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {   // show about box
            AboutD2DMapEditor aboutBox = new AboutD2DMapEditor();
            aboutBox.ShowDialog();
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string target = "http://www.dannylum.com/D2DProject/documentation.html";
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
        #endregion

        #region Tools

        private void btnToolSelection_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.selection);
        }

        private void btnToolBrush_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.brush);
        }

        private void btnToolFill_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.fill);
        }

        private void btnToolSelectColor_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.selectTile);
        }

        private void btnToolEraser_Click(object sender, EventArgs e)
        {
            SelectTool(ToolType.eraser);
        }

        #endregion

        public Boolean ConfirmCloseMap(string title)
        {
            DialogResult result = MessageBox.Show("Do you want to save " + Path.GetFileName(_map.MapFileName) + "?", title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {   // save file before close
                try
                {
                    if (_map.MapFileName == "Untitled.d2d")
                    {
                        PromptSaveNewMap();
                    }
                    else
                    {
                        SaveMap(_map.MapFileName);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to save: " + _map.MapFileName + "\n" + ex.Message);
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return false;
            }

            return true;
        }

        public void PromptSaveNewMap()
        {
            string initialPath = Application.ExecutablePath + "\\" + "Projects";
            saveMapDialog.InitialDirectory = initialPath;
            DialogResult saveMap = this.saveMapDialog.ShowDialog();
            if (saveMap == DialogResult.OK)
            {   // save Map
                try
                {
                    SaveMap(this.saveMapDialog.FileName);
                    saveMapToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to save: " + _map.MapFileName + "\n" + ex.Message);
                }
            }
        }
    }
}
