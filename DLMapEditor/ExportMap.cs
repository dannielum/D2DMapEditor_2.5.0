using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace D2DMapEditor
{
    public enum ExportLayer
    {
        SelectedOnly = 1,
        VisibleOnly = 2,
        All = 3
    }

    public partial class ExportMap : Form
    {
        public ExportLayer LayerOption;
        public ProgrammingLanguage LanguageOption;
        public ImageFormat TileOption;

        public ExportMap()
        {
            InitializeComponent();
            rbLayerAll.Checked = true;
            rbLanguageCPP.Checked = true;
            rbTileBMP.Checked = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (rbLayerSelected.Checked)
                LayerOption = ExportLayer.SelectedOnly;
            else if (rbLayerVisible.Checked)
                LayerOption = ExportLayer.VisibleOnly;
            else if (rbLayerAll.Checked)
                LayerOption = ExportLayer.All;

            if (rbLanguageCPP.Checked)
                LanguageOption = ProgrammingLanguage.CPP;
            else if (rbLanguageCS.Checked)
                LanguageOption = ProgrammingLanguage.CSharp;
            else if (rbLanguageActionScript.Checked)
                LanguageOption = ProgrammingLanguage.ActionScript;
            else if (rbLanguageXML.Checked)
                LanguageOption = ProgrammingLanguage.XML;
            else if (rbLanguageXMLLite.Checked)
                LanguageOption = ProgrammingLanguage.XMLLite;

            if (rbTileBMP.Checked)
                TileOption = ImageFormat.Bmp;
            else if (rbTileJPEG.Checked)
                TileOption = ImageFormat.Jpeg;
            else if (rbTileGIF.Checked)
                TileOption = ImageFormat.Gif;
            else if (rbTilePNG.Checked)
                TileOption = ImageFormat.Png;
        }
    }
}