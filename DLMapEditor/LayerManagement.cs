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
        public void SetupLayer(int index)
        {   // setup layer
            if (index == -1)
            {
                return;
            }
            else
            {
                if (tbLayerName.Text != _map.Layers[index].Name || nudLayerWidth.Value != _map.GetMapWidth() || nudLayerHeight.Value != _map.GetMapHeight() || nudTileWidth.Value != _map.TileWidth || nudTileHeight.Value != _map.TileHeight)
                {
                    DialogResult resizeDialog = MessageBox.Show("Do you want to do these changes? \nThis action cannot be undone", "Update Layer?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (resizeDialog != DialogResult.OK)
                    {
                        lblLayerId.Text = _map.Layers[index].LayerId.ToString();
                        tbLayerName.Text = _map.Layers[index].Name;
                        nudLayerWidth.Value = _map.Layers[index].Width;
                        nudLayerHeight.Value = _map.Layers[index].Height;
                        nudTileWidth.Value = _map.TileWidth;
                        nudTileHeight.Value = _map.TileHeight;
                        cbLayerVisible.Checked = _map.Layers[index].Visible;
                        tbarLayerAlpha.Value = _map.Layers[index].Alpha;
                        return;
                    }
                }
            }

            if (!CodesDictionary.IsValidName(tbLayerName.Text, _map_info.Language))
            {
                MessageBox.Show("\"" + tbLayerName.Text + "\" is not a valid name!", "Update Layer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _map.TileWidth = Convert.ToInt32(nudTileWidth.Value);
            _map.TileHeight = Convert.ToInt32(nudTileHeight.Value);
            _map.Layers[index].SetupLayer(tbLayerName.Text, Convert.ToInt32(nudLayerWidth.Value), Convert.ToInt32(nudLayerHeight.Value), tbarLayerAlpha.Value, cbLayerVisible.Checked, Convert.ToInt32(lblLayerId.Text));

            pbMap.Width = _map.Layers[index].Width * _map.GetMapWidth();
            pbMap.Height = _map.Layers[index].Height * _map.GetMapHeight();

            RenderMap();
        }

        public void ReloadLayers(int index)
        {
            int selectedIndex = lbLayers.SelectedIndex;
            lbLayers.Items.Clear();
            foreach (Layer layer in _map.Layers)
            {
                lbLayers.Items.Add(layer.Name);
            }

            if (lbLayers.Items.Count == 1)
            {
                lbLayers.SelectedIndex = 0;
            }
            else if (index == -1)
            {
                if (lbLayers.Items.Count > selectedIndex)
                    lbLayers.SelectedIndex = selectedIndex;
                else
                    lbLayers.SelectedIndex = lbLayers.Items.Count - 1;
            }
            else
            {
                lbLayers.SelectedIndex = index;
            }
        }

        private void lbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLayers.SelectedItem == null)
            {
                gbLayerProperties.Enabled = false;
                return;
            }

            int index = _map.FindLayerIndexWithName(lbLayers.SelectedItem.ToString());

            if (index != -1)
            {
                gbLayerProperties.Enabled = true;
                lblLayerId.Text = _map.Layers[index].LayerId.ToString();
                tbLayerName.Text = _map.Layers[index].Name;
                nudLayerWidth.Value = _map.Layers[index].Width;
                nudLayerHeight.Value = _map.Layers[index].Height;
                nudTileWidth.Value = _map.TileWidth;
                nudTileHeight.Value = _map.TileHeight;
                cbLayerVisible.Checked = _map.Layers[index].Visible;
                tbarLayerAlpha.Value = _map.Layers[index].Alpha;
            }

            RenderMap();
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            AddLayer();
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            DeleteLayer();
        }

        private void btnLayerMoveUp_Click(object sender, EventArgs e)
        {
            if (lbLayers.SelectedItem != null && _map.Layers.Count > 1)
            {
                int index = lbLayers.SelectedIndex;
                if (index > 0)
                {
                    _history.PushUndo(index, index - 1, ActionType.SwapLayer);
                    _map.SwapLayers(_map, index, index - 1);
                    ReloadLayers(index - 1);
                }
            }
        }

        private void btnLayerMoveDown_Click(object sender, EventArgs e)
        {
            if (lbLayers.SelectedItem != null && _map.Layers.Count > 1)
            {
                int index = lbLayers.SelectedIndex;
                if (index < _map.Layers.Count-1)
                {
                    _history.PushUndo(index, index + 1, ActionType.SwapLayer);
                    _map.SwapLayers(_map, index, index + 1);
                    ReloadLayers(index + 1);
                }
            }
        }

        private void tbarLayerAlpha_Scroll(object sender, EventArgs e)
        {
            int index = _map.FindLayerIndexWithName(lbLayers.SelectedItem.ToString());

            if (index != -1)
            {
                _map.Layers[index].Alpha = tbarLayerAlpha.Value;
                RenderMap();
            }
        }

        private void AddLayer()
        {
            string layername = _map.GetNewLayerDeafultName();
            _map.AddLayer(layername, _map.GetMapWidth(), _map.GetMapHeight(), 100, _map.GetLastLayerId() + 1);
            int index = _map.FindLayerIndexWithName(layername);
            _history.PushUndo(_map.Layers[index].LayerId, _map.Layers[index], index, ActionType.AddLayer);
            ReloadLayers(lbLayers.Items.Count);

            if (_history.Undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void DeleteLayer()
        {
            if (lbLayers.SelectedItem != null && _map.Layers.Count > 1)
            {
                if (MessageBox.Show("Delete this layer?", "Delete Layer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int index = lbLayers.SelectedIndex;
                    _history.PushUndo(_map.Layers[index].LayerId, _map.Layers[index], index, ActionType.DeleteLayer);
                    _map.Layers.RemoveAt(index);
                    ReloadLayers(index - 1);
                }
            }

            if (_history.Undo.Count > 0)
                undoToolStripMenuItem.Enabled = true;
            else
                undoToolStripMenuItem.Enabled = false;
        }

        private void GenerateCode(ProgrammingLanguage language)
        {
            int index = _map.FindLayerIndexWithName(lbLayers.SelectedItem.ToString());

            if (index == -1)
            {
                MessageBox.Show("Please select a layer", "Generate Code");
                return;
            }

            tbCode.Text = CodesGenerator.GenerateCodes(index, _map, _map_info.Format, _tile_library, language);
        }
    }
}
