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
    public partial class ImportTilesEditor : Form
    {
        private D2DMapEditor _parent_form;
        private Boolean _image_loaded;
        public ImageFormat Format;

        public ImportTilesEditor(D2DMapEditor parentForm)
        {
            InitializeComponent();
            _parent_form = parentForm;
            nudImageWidth.ValueChanged += new EventHandler(nudImageWidth_ValueChanged);
            nudImageHeight.ValueChanged += new EventHandler(nudImageHeight_ValueChanged);
            _image_loaded = false;
        }

        public void LoadFromParentForm()
        {   // load variables from parent form
            try
            {
                this.nudHorizontalGrid.Value = 10;  //((D2DMapEditor)this.ParentForm).nudMapWidth.Value;
                this.nudVerticalGrid.Value = 10;    //((D2DMapEditor)this.ParentForm).nudMapHeight.Value;
                this.nudTileWidth.Value = _parent_form.nudTileWidth.Value;
                this.nudTileHeight.Value = _parent_form.nudTileHeight.Value;
                this.tbName.Text = "myTileSet";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {   // open image file
            DialogResult loadImage = this.openImageDialog.ShowDialog();
            if (loadImage == DialogResult.OK)
            {   // load image
                string bmpImageName = this.openImageDialog.FileName;

                LoadImage(bmpImageName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {   // cancel
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   // update
            if (pbTilesMap.Image != null)
            {
                float newTileWidth = Convert.ToSingle(nudImageWidth.Value / nudHorizontalGrid.Value);
                float newTileHeight = Convert.ToSingle(nudImageHeight.Value / nudVerticalGrid.Value);

                if (Convert.ToDecimal(newTileWidth) > nudTileWidth.Maximum)
                {
                    if (MessageBox.Show("Maximum tile width(" + nudTileWidth.Maximum.ToString() + ") exceeded", "Tile Width", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        return;
                }
                else if (newTileWidth > 1)
                    nudTileWidth.Value = Convert.ToDecimal(newTileWidth);
                else
                    nudTileWidth.Value = 1;

                if (Convert.ToDecimal(newTileHeight) > nudTileHeight.Maximum)
                {
                    if (MessageBox.Show("Maximum tile height(" + nudTileHeight.Maximum.ToString() + ") exceeded", "Tile Height", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        return;
                }
                else if (newTileHeight > 1)
                    nudTileHeight.Value = Convert.ToDecimal(newTileHeight);
                else
                    nudTileHeight.Value = 1;

                pbTilesMap.Width = Convert.ToInt32(nudImageWidth.Value);
                pbTilesMap.Height = Convert.ToInt32(nudImageHeight.Value);
                pbTilesMap.SizeMode = PictureBoxSizeMode.StretchImage;
                RenderImage(true);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {   // reset
            if (pbTilesMap.Image != null)
            {
                if (MessageBox.Show("Reset to original image size?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string bmpImageName = tbLoadImage.Text;
                    pbTilesMap.SizeMode = PictureBoxSizeMode.Normal;

                    LoadImage(bmpImageName);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {   // create tiles
            if (tbLoadImage.Text == "")
            {   // image file not selected
                MessageBox.Show("No tiles sheet is loaded", "Create Tiles", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult saveTiles = this.saveTilesDialog.ShowDialog();
            if (saveTiles == DialogResult.OK)
            {   // save tiles
                string folderName = this.saveTilesDialog.SelectedPath + "\\" + tbName.Text;
                DirectoryInfo di = Directory.CreateDirectory(folderName);

                Cursor.Current = Cursors.WaitCursor;
                CreateTiles(folderName);
                Cursor.Current = Cursors.Default;

                if (MessageBox.Show("Load to Tile Library now?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {   // load tiles?
                    _parent_form.AddTiles(folderName);
                }

                this.Close();
            }
        }

        private void LoadImage(string fileName)
        {   // load image
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                _image_loaded = false;
                
                pbTilesMap.SizeMode = PictureBoxSizeMode.AutoSize;
                pbTilesMap.WaitOnLoad = true;
                pbTilesMap.Load(fileName);
                tbLoadImage.Text = fileName;

                nudImageWidth.Value = pbTilesMap.Width - 2;
                nudImageHeight.Value = pbTilesMap.Height - 2;

                nudTileWidth.Value = nudImageWidth.Value / nudHorizontalGrid.Value;
                nudTileHeight.Value = nudImageHeight.Value / nudVerticalGrid.Value;

                RenderImage(true);

                _image_loaded = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open: " + fileName + "\n" + ex.Message);
            }
        }

        private void RenderImage(bool gridOn)
        {   // render image
            int newWidth = pbTilesMap.Width;
            if (newWidth < 1) newWidth = 1;
            int newHeight = pbTilesMap.Height;
            if (newHeight < 1) newHeight = 1;

            Bitmap bmp = new Bitmap(newWidth, newHeight);
            pbTilesMap.Image = bmp;

            Graphics gfx = Graphics.FromImage(bmp);

            Pen myPen = new Pen(Color.DarkBlue);
            
            Image curImage = Image.FromFile(tbLoadImage.Text);
            gfx.DrawImage(curImage, new Rectangle(0, 0, pbTilesMap.Image.Width, pbTilesMap.Image.Height));

            if (gridOn)
            {
                // draw grids
                for (int a = 1; a < pbTilesMap.Width; a++)
                {   // draw vertical lines
                    gfx.DrawLine(myPen, Convert.ToSingle(a * nudTileWidth.Value), 0, Convert.ToSingle(a * nudTileWidth.Value), Convert.ToSingle(pbTilesMap.Height * nudTileHeight.Value));
                }

                for (int b = 1; b < pbTilesMap.Height; b++)
                {   // draw horizontal lines
                    gfx.DrawLine(myPen, 0, Convert.ToSingle(b * nudTileHeight.Value), Convert.ToSingle(nudHorizontalGrid.Value * nudTileWidth.Value), Convert.ToSingle(b * nudTileHeight.Value));
                }
            }

            gfx.Dispose();

            pbTilesMap.Refresh();
        }

        private void CreateTiles(string folderName)
        {   // create tiles and save them to selected folder
            RenderImage(false);

            string tileFileName = "";
            int mapWidth = Convert.ToInt32(nudHorizontalGrid.Value);
            int mapHeight = Convert.ToInt32(nudVerticalGrid.Value);
            int tileWidth = Convert.ToInt32(nudTileWidth.Value);
            int tileHeight = Convert.ToInt32(nudTileHeight.Value);

            string formatExtension = ".bmp";
            if (rbBMP.Checked)
            {
                Format = ImageFormat.Bmp;
                formatExtension = ".bmp";
            }
            else if (rbJPEG.Checked)
            {
                Format = ImageFormat.Jpeg;
                formatExtension = ".jpg";
            }
            else if (rbGIF.Checked)
            {
                Format = ImageFormat.Gif;
                formatExtension = ".gif";
            }
            else if (rbPNG.Checked)
            {
                Format = ImageFormat.Png;
                formatExtension = ".png";
            }

            int tileID = 0;
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Bitmap bmp = (Bitmap)pbTilesMap.Image; 
                    Bitmap cropped = new Bitmap(tileWidth, tileHeight);
                    Graphics g = Graphics.FromImage(cropped);

                    g.DrawImage(bmp, new Rectangle(0, 0, cropped.Width, cropped.Height), tileWidth * x, tileHeight * y, cropped.Width, cropped.Height, GraphicsUnit.Pixel);

                    g.Dispose();

                    tileFileName = folderName;

                    if (tileID < 10)
                        tileFileName += "\\00" + tileID.ToString() + formatExtension;
                    else if (tileID < 100)
                        tileFileName += "\\0" + tileID.ToString() + formatExtension;
                    else
                        tileFileName += tileID.ToString() + formatExtension;

                    try
                    {
                        cropped.Save(tileFileName, ImageFormat.Bmp);
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(tileFileName + "\n" + exc.ToString(), "Fail to Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    tileID++;
                }
            }

            RenderImage(true);
        }

        void nudImageWidth_ValueChanged(object sender, EventArgs e)
        {
            if (!cbConstrainProportions.Checked || !_image_loaded)
                return;

            if (pbTilesMap.Image != null)
            {   // not loading image
                float oldWidth = Convert.ToSingle(nudHorizontalGrid.Value * nudTileWidth.Value);
                float newWidth = Convert.ToSingle(nudImageWidth.Value);
                float oldHeight = Convert.ToSingle(nudVerticalGrid.Value * nudTileHeight.Value);
                float newHeight = (newWidth * oldHeight) / oldWidth;

                //MessageBox.Show("old: " + oldWidth.ToString() + " x " + oldHeight.ToString() + " \nnew: " + newWidth.ToString() + " x " + newHeight.ToString());
                nudImageHeight.Value = Convert.ToDecimal(newHeight);
            }
        }

        void nudImageHeight_ValueChanged(object sender, EventArgs e)
        {
            if (!cbConstrainProportions.Checked || !_image_loaded)
                return;

            if (pbTilesMap.Image != null)
            {   // not loading image
                float oldHeight = Convert.ToSingle(nudVerticalGrid.Value * nudTileHeight.Value);
                float newHeight = Convert.ToSingle(nudImageHeight.Value);
                float oldWidth = Convert.ToSingle(nudHorizontalGrid.Value * nudTileWidth.Value);
                float newWidth = (oldWidth * newHeight) / oldHeight;

                //MessageBox.Show("old: " + oldWidth.ToString() + " x " + oldHeight.ToString() + " \nnew: " + newWidth.ToString() + " x " + newHeight.ToString());
                nudImageWidth.Value = Convert.ToDecimal(newWidth);
            }
        }
    }
}