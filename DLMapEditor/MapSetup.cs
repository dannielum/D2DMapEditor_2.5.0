using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace D2DMapEditor
{    
    public partial class MapSetup : Form
    {
        private D2DMapEditor _parent_form;

        public MapSetup(D2DMapEditor parentForm)
        {
            InitializeComponent();
            _parent_form = parentForm;

            tbNewMapName.Text = "Untitled";
            nudNewMapWidth.Value = 10;
            nudNewMapHeight.Value = 10;
            nudNewTileWidth.Value = 32;
            nudNewTileHeight.Value = 32;
        }

        private void btnCreateNewMap_Click(object sender, EventArgs e)
        {   // create new map
            if (tbNewMapName.Text == "")
            {
                MessageBox.Show("Please provide the map name");
            }
            else if (nudNewMapWidth.Value < 1 || nudNewMapHeight.Value < 1 ||
                nudNewTileWidth.Value < 1 || nudNewTileHeight.Value < 1)
            {
                MessageBox.Show("Please set the appropriate map size");
            }
            else
            {
                _parent_form.SetupMap(tbNewMapName.Text, Convert.ToInt32(nudNewMapWidth.Value),
                                      Convert.ToInt32(nudNewMapHeight.Value),
                                      Convert.ToInt32(nudNewTileWidth.Value),
                                      Convert.ToInt32(nudNewTileHeight.Value));
                _parent_form.ClearTiles();
                _parent_form.ReloadLayers(0);
                _parent_form.RenderMap();
                this.Close();
            }
        }

        private void btnCancelNewMap_Click(object sender, EventArgs e)
        {   // cancel create new map
            this.Close();
        }
    }
}