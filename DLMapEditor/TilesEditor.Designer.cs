namespace D2DMapEditor
{
    partial class ImportTilesEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbTilesMap = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbMapProperties = new System.Windows.Forms.GroupBox();
            this.lblTileFormat = new System.Windows.Forms.Label();
            this.cbConstrainProportions = new System.Windows.Forms.CheckBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.nudImageHeight = new System.Windows.Forms.NumericUpDown();
            this.nudImageWidth = new System.Windows.Forms.NumericUpDown();
            this.lblImageHeight = new System.Windows.Forms.Label();
            this.lblImageWidth = new System.Windows.Forms.Label();
            this.nudTileHeight = new System.Windows.Forms.NumericUpDown();
            this.nudTileWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblTileSetName = new System.Windows.Forms.Label();
            this.nudVerticalGrid = new System.Windows.Forms.NumericUpDown();
            this.nudHorizontalGrid = new System.Windows.Forms.NumericUpDown();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.tbLoadImage = new System.Windows.Forms.TextBox();
            this.saveTilesDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.rbBMP = new System.Windows.Forms.RadioButton();
            this.rbJPEG = new System.Windows.Forms.RadioButton();
            this.rbGIF = new System.Windows.Forms.RadioButton();
            this.rbPNG = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilesMap)).BeginInit();
            this.gbMapProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbTilesMap);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 652);
            this.panel1.TabIndex = 0;
            // 
            // pbTilesMap
            // 
            this.pbTilesMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbTilesMap.Location = new System.Drawing.Point(3, 3);
            this.pbTilesMap.Name = "pbTilesMap";
            this.pbTilesMap.Size = new System.Drawing.Size(687, 644);
            this.pbTilesMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbTilesMap.TabIndex = 0;
            this.pbTilesMap.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(713, 641);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbMapProperties
            // 
            this.gbMapProperties.Controls.Add(this.rbPNG);
            this.gbMapProperties.Controls.Add(this.rbGIF);
            this.gbMapProperties.Controls.Add(this.rbJPEG);
            this.gbMapProperties.Controls.Add(this.rbBMP);
            this.gbMapProperties.Controls.Add(this.lblTileFormat);
            this.gbMapProperties.Controls.Add(this.cbConstrainProportions);
            this.gbMapProperties.Controls.Add(this.btnUpdate);
            this.gbMapProperties.Controls.Add(this.btnReset);
            this.gbMapProperties.Controls.Add(this.nudImageHeight);
            this.gbMapProperties.Controls.Add(this.nudImageWidth);
            this.gbMapProperties.Controls.Add(this.lblImageHeight);
            this.gbMapProperties.Controls.Add(this.lblImageWidth);
            this.gbMapProperties.Controls.Add(this.nudTileHeight);
            this.gbMapProperties.Controls.Add(this.nudTileWidth);
            this.gbMapProperties.Controls.Add(this.lblTileHeight);
            this.gbMapProperties.Controls.Add(this.lblTileWidth);
            this.gbMapProperties.Controls.Add(this.tbName);
            this.gbMapProperties.Controls.Add(this.lblTileSetName);
            this.gbMapProperties.Controls.Add(this.nudVerticalGrid);
            this.gbMapProperties.Controls.Add(this.nudHorizontalGrid);
            this.gbMapProperties.Controls.Add(this.lblMapHeight);
            this.gbMapProperties.Controls.Add(this.lblMapWidth);
            this.gbMapProperties.Location = new System.Drawing.Point(713, 12);
            this.gbMapProperties.Name = "gbMapProperties";
            this.gbMapProperties.Size = new System.Drawing.Size(169, 479);
            this.gbMapProperties.TabIndex = 2;
            this.gbMapProperties.TabStop = false;
            this.gbMapProperties.Text = "Tile Set Properties";
            // 
            // lblTileFormat
            // 
            this.lblTileFormat.AutoSize = true;
            this.lblTileFormat.Location = new System.Drawing.Point(6, 366);
            this.lblTileFormat.Name = "lblTileFormat";
            this.lblTileFormat.Size = new System.Drawing.Size(59, 13);
            this.lblTileFormat.TabIndex = 28;
            this.lblTileFormat.Text = "Tile Format";
            // 
            // cbConstrainProportions
            // 
            this.cbConstrainProportions.AutoSize = true;
            this.cbConstrainProportions.Checked = true;
            this.cbConstrainProportions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConstrainProportions.Location = new System.Drawing.Point(9, 334);
            this.cbConstrainProportions.Name = "cbConstrainProportions";
            this.cbConstrainProportions.Size = new System.Drawing.Size(126, 17);
            this.cbConstrainProportions.TabIndex = 26;
            this.cbConstrainProportions.Text = "Constrain Proportions";
            this.cbConstrainProportions.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(88, 450);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 450);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(45, 23);
            this.btnReset.TabIndex = 24;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // nudImageHeight
            // 
            this.nudImageHeight.DecimalPlaces = 2;
            this.nudImageHeight.Location = new System.Drawing.Point(9, 308);
            this.nudImageHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudImageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageHeight.Name = "nudImageHeight";
            this.nudImageHeight.Size = new System.Drawing.Size(80, 20);
            this.nudImageHeight.TabIndex = 23;
            this.nudImageHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageHeight.ValueChanged += new System.EventHandler(this.nudImageHeight_ValueChanged);
            // 
            // nudImageWidth
            // 
            this.nudImageWidth.DecimalPlaces = 2;
            this.nudImageWidth.Location = new System.Drawing.Point(9, 268);
            this.nudImageWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudImageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageWidth.Name = "nudImageWidth";
            this.nudImageWidth.Size = new System.Drawing.Size(80, 20);
            this.nudImageWidth.TabIndex = 22;
            this.nudImageWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageWidth.ValueChanged += new System.EventHandler(this.nudImageWidth_ValueChanged);
            // 
            // lblImageHeight
            // 
            this.lblImageHeight.AutoSize = true;
            this.lblImageHeight.Location = new System.Drawing.Point(6, 291);
            this.lblImageHeight.Name = "lblImageHeight";
            this.lblImageHeight.Size = new System.Drawing.Size(114, 13);
            this.lblImageHeight.TabIndex = 21;
            this.lblImageHeight.Text = "Imported Image Height";
            // 
            // lblImageWidth
            // 
            this.lblImageWidth.AutoSize = true;
            this.lblImageWidth.Location = new System.Drawing.Point(6, 252);
            this.lblImageWidth.Name = "lblImageWidth";
            this.lblImageWidth.Size = new System.Drawing.Size(111, 13);
            this.lblImageWidth.TabIndex = 20;
            this.lblImageWidth.Text = "Imported Image Width";
            // 
            // nudTileHeight
            // 
            this.nudTileHeight.DecimalPlaces = 2;
            this.nudTileHeight.Location = new System.Drawing.Point(9, 214);
            this.nudTileHeight.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nudTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileHeight.Name = "nudTileHeight";
            this.nudTileHeight.ReadOnly = true;
            this.nudTileHeight.Size = new System.Drawing.Size(80, 20);
            this.nudTileHeight.TabIndex = 17;
            this.nudTileHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTileWidth
            // 
            this.nudTileWidth.DecimalPlaces = 2;
            this.nudTileWidth.Location = new System.Drawing.Point(9, 175);
            this.nudTileWidth.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nudTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileWidth.Name = "nudTileWidth";
            this.nudTileWidth.ReadOnly = true;
            this.nudTileWidth.Size = new System.Drawing.Size(80, 20);
            this.nudTileWidth.TabIndex = 15;
            this.nudTileWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(6, 198);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(58, 13);
            this.lblTileHeight.TabIndex = 18;
            this.lblTileHeight.Text = "Tile Height";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(6, 159);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(55, 13);
            this.lblTileWidth.TabIndex = 16;
            this.lblTileWidth.Text = "Tile Width";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(9, 32);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(154, 20);
            this.tbName.TabIndex = 11;
            // 
            // lblTileSetName
            // 
            this.lblTileSetName.AutoSize = true;
            this.lblTileSetName.Location = new System.Drawing.Point(6, 16);
            this.lblTileSetName.Name = "lblTileSetName";
            this.lblTileSetName.Size = new System.Drawing.Size(74, 13);
            this.lblTileSetName.TabIndex = 14;
            this.lblTileSetName.Text = "Tile Set Name";
            // 
            // nudVerticalGrid
            // 
            this.nudVerticalGrid.Location = new System.Drawing.Point(9, 122);
            this.nudVerticalGrid.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVerticalGrid.Name = "nudVerticalGrid";
            this.nudVerticalGrid.Size = new System.Drawing.Size(80, 20);
            this.nudVerticalGrid.TabIndex = 13;
            this.nudVerticalGrid.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudHorizontalGrid
            // 
            this.nudHorizontalGrid.Location = new System.Drawing.Point(9, 83);
            this.nudHorizontalGrid.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHorizontalGrid.Name = "nudHorizontalGrid";
            this.nudHorizontalGrid.Size = new System.Drawing.Size(80, 20);
            this.nudHorizontalGrid.TabIndex = 12;
            this.nudHorizontalGrid.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Location = new System.Drawing.Point(6, 106);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(64, 13);
            this.lblMapHeight.TabIndex = 10;
            this.lblMapHeight.Text = "Vertical Grid";
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Location = new System.Drawing.Point(6, 67);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(76, 13);
            this.lblMapWidth.TabIndex = 9;
            this.lblMapWidth.Text = "Horizontal Grid";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(807, 641);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Import Tiles";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(807, 523);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadImage.TabIndex = 4;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // tbLoadImage
            // 
            this.tbLoadImage.Location = new System.Drawing.Point(713, 497);
            this.tbLoadImage.Name = "tbLoadImage";
            this.tbLoadImage.ReadOnly = true;
            this.tbLoadImage.Size = new System.Drawing.Size(169, 20);
            this.tbLoadImage.TabIndex = 5;
            // 
            // rbBMP
            // 
            this.rbBMP.AutoSize = true;
            this.rbBMP.Checked = true;
            this.rbBMP.Location = new System.Drawing.Point(9, 383);
            this.rbBMP.Name = "rbBMP";
            this.rbBMP.Size = new System.Drawing.Size(48, 17);
            this.rbBMP.TabIndex = 29;
            this.rbBMP.TabStop = true;
            this.rbBMP.Text = "BMP";
            this.rbBMP.UseVisualStyleBackColor = true;
            // 
            // rbJPEG
            // 
            this.rbJPEG.AutoSize = true;
            this.rbJPEG.Location = new System.Drawing.Point(9, 406);
            this.rbJPEG.Name = "rbJPEG";
            this.rbJPEG.Size = new System.Drawing.Size(52, 17);
            this.rbJPEG.TabIndex = 30;
            this.rbJPEG.TabStop = true;
            this.rbJPEG.Text = "JPEG";
            this.rbJPEG.UseVisualStyleBackColor = true;
            // 
            // rbGIF
            // 
            this.rbGIF.AutoSize = true;
            this.rbGIF.Location = new System.Drawing.Point(88, 383);
            this.rbGIF.Name = "rbGIF";
            this.rbGIF.Size = new System.Drawing.Size(42, 17);
            this.rbGIF.TabIndex = 31;
            this.rbGIF.TabStop = true;
            this.rbGIF.Text = "GIF";
            this.rbGIF.UseVisualStyleBackColor = true;
            // 
            // rbPNG
            // 
            this.rbPNG.AutoSize = true;
            this.rbPNG.Location = new System.Drawing.Point(88, 406);
            this.rbPNG.Name = "rbPNG";
            this.rbPNG.Size = new System.Drawing.Size(48, 17);
            this.rbPNG.TabIndex = 32;
            this.rbPNG.TabStop = true;
            this.rbPNG.Text = "PNG";
            this.rbPNG.UseVisualStyleBackColor = true;
            // 
            // ImportTilesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 676);
            this.Controls.Add(this.tbLoadImage);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.gbMapProperties);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportTilesEditor";
            this.ShowInTaskbar = false;
            this.Text = "Create Tile Set";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTilesMap)).EndInit();
            this.gbMapProperties.ResumeLayout(false);
            this.gbMapProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHorizontalGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbTilesMap;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbMapProperties;
        public System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblTileSetName;
        public System.Windows.Forms.NumericUpDown nudVerticalGrid;
        public System.Windows.Forms.NumericUpDown nudHorizontalGrid;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.Label lblMapWidth;
        public System.Windows.Forms.NumericUpDown nudTileHeight;
        public System.Windows.Forms.NumericUpDown nudTileWidth;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.Label lblImageHeight;
        private System.Windows.Forms.Label lblImageWidth;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.TextBox tbLoadImage;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.NumericUpDown nudImageHeight;
        private System.Windows.Forms.NumericUpDown nudImageWidth;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox cbConstrainProportions;
        private System.Windows.Forms.FolderBrowserDialog saveTilesDialog;
        private System.Windows.Forms.Label lblTileFormat;
        private System.Windows.Forms.RadioButton rbPNG;
        private System.Windows.Forms.RadioButton rbGIF;
        private System.Windows.Forms.RadioButton rbJPEG;
        private System.Windows.Forms.RadioButton rbBMP;
    }
}