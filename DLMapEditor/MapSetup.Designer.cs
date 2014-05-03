namespace D2DMapEditor
{
    partial class MapSetup
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
            this.nudNewMapHeight = new System.Windows.Forms.NumericUpDown();
            this.nudNewMapWidth = new System.Windows.Forms.NumericUpDown();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.lblNewMapName = new System.Windows.Forms.Label();
            this.gbNewMapSize = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudNewTileHeight = new System.Windows.Forms.NumericUpDown();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.nudNewTileWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.btnCreateNewMap = new System.Windows.Forms.Button();
            this.btnCancelNewMap = new System.Windows.Forms.Button();
            this.tbNewMapName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewMapWidth)).BeginInit();
            this.gbNewMapSize.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewTileWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // nudNewMapHeight
            // 
            this.nudNewMapHeight.Location = new System.Drawing.Point(93, 59);
            this.nudNewMapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewMapHeight.Name = "nudNewMapHeight";
            this.nudNewMapHeight.Size = new System.Drawing.Size(80, 20);
            this.nudNewMapHeight.TabIndex = 3;
            this.nudNewMapHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudNewMapWidth
            // 
            this.nudNewMapWidth.Location = new System.Drawing.Point(93, 29);
            this.nudNewMapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewMapWidth.Name = "nudNewMapWidth";
            this.nudNewMapWidth.Size = new System.Drawing.Size(80, 20);
            this.nudNewMapWidth.TabIndex = 2;
            this.nudNewMapWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Location = new System.Drawing.Point(28, 61);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(62, 13);
            this.lblMapHeight.TabIndex = 5;
            this.lblMapHeight.Text = "Map Height";
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Location = new System.Drawing.Point(28, 31);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(59, 13);
            this.lblMapWidth.TabIndex = 4;
            this.lblMapWidth.Text = "Map Width";
            // 
            // lblNewMapName
            // 
            this.lblNewMapName.AutoSize = true;
            this.lblNewMapName.Location = new System.Drawing.Point(12, 9);
            this.lblNewMapName.Name = "lblNewMapName";
            this.lblNewMapName.Size = new System.Drawing.Size(59, 13);
            this.lblNewMapName.TabIndex = 8;
            this.lblNewMapName.Text = "Map Name";
            // 
            // gbNewMapSize
            // 
            this.gbNewMapSize.Controls.Add(this.lblMapWidth);
            this.gbNewMapSize.Controls.Add(this.lblMapHeight);
            this.gbNewMapSize.Controls.Add(this.nudNewMapWidth);
            this.gbNewMapSize.Controls.Add(this.nudNewMapHeight);
            this.gbNewMapSize.Location = new System.Drawing.Point(12, 43);
            this.gbNewMapSize.Name = "gbNewMapSize";
            this.gbNewMapSize.Size = new System.Drawing.Size(210, 100);
            this.gbNewMapSize.TabIndex = 10;
            this.gbNewMapSize.TabStop = false;
            this.gbNewMapSize.Text = "Map Size";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudNewTileHeight);
            this.groupBox1.Controls.Add(this.lblTileWidth);
            this.groupBox1.Controls.Add(this.nudNewTileWidth);
            this.groupBox1.Controls.Add(this.lblTileHeight);
            this.groupBox1.Location = new System.Drawing.Point(12, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 102);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tile Size";
            // 
            // nudNewTileHeight
            // 
            this.nudNewTileHeight.Location = new System.Drawing.Point(93, 61);
            this.nudNewTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewTileHeight.Name = "nudNewTileHeight";
            this.nudNewTileHeight.Size = new System.Drawing.Size(80, 20);
            this.nudNewTileHeight.TabIndex = 5;
            this.nudNewTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(28, 33);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(55, 13);
            this.lblTileWidth.TabIndex = 9;
            this.lblTileWidth.Text = "Tile Width";
            // 
            // nudNewTileWidth
            // 
            this.nudNewTileWidth.Location = new System.Drawing.Point(93, 31);
            this.nudNewTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewTileWidth.Name = "nudNewTileWidth";
            this.nudNewTileWidth.Size = new System.Drawing.Size(80, 20);
            this.nudNewTileWidth.TabIndex = 4;
            this.nudNewTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(28, 63);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(58, 13);
            this.lblTileHeight.TabIndex = 11;
            this.lblTileHeight.Text = "Tile Height";
            // 
            // btnCreateNewMap
            // 
            this.btnCreateNewMap.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateNewMap.Location = new System.Drawing.Point(144, 268);
            this.btnCreateNewMap.Name = "btnCreateNewMap";
            this.btnCreateNewMap.Size = new System.Drawing.Size(75, 23);
            this.btnCreateNewMap.TabIndex = 7;
            this.btnCreateNewMap.Text = "OK";
            this.btnCreateNewMap.UseVisualStyleBackColor = true;
            this.btnCreateNewMap.Click += new System.EventHandler(this.btnCreateNewMap_Click);
            // 
            // btnCancelNewMap
            // 
            this.btnCancelNewMap.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelNewMap.Location = new System.Drawing.Point(12, 268);
            this.btnCancelNewMap.Name = "btnCancelNewMap";
            this.btnCancelNewMap.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewMap.TabIndex = 6;
            this.btnCancelNewMap.Text = "Cancel";
            this.btnCancelNewMap.UseVisualStyleBackColor = true;
            this.btnCancelNewMap.Click += new System.EventHandler(this.btnCancelNewMap_Click);
            // 
            // tbNewMapName
            // 
            this.tbNewMapName.Location = new System.Drawing.Point(77, 6);
            this.tbNewMapName.Name = "tbNewMapName";
            this.tbNewMapName.Size = new System.Drawing.Size(142, 20);
            this.tbNewMapName.TabIndex = 1;
            // 
            // MapSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 303);
            this.Controls.Add(this.btnCancelNewMap);
            this.Controls.Add(this.btnCreateNewMap);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbNewMapSize);
            this.Controls.Add(this.tbNewMapName);
            this.Controls.Add(this.lblNewMapName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapSetup";
            this.ShowInTaskbar = false;
            this.Text = "MapSetup";
            ((System.ComponentModel.ISupportInitialize)(this.nudNewMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewMapWidth)).EndInit();
            this.gbNewMapSize.ResumeLayout(false);
            this.gbNewMapSize.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewTileWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudNewMapHeight;
        private System.Windows.Forms.NumericUpDown nudNewMapWidth;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.Label lblMapWidth;
        private System.Windows.Forms.Label lblNewMapName;
        private System.Windows.Forms.GroupBox gbNewMapSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudNewTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.NumericUpDown nudNewTileWidth;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Button btnCreateNewMap;
        private System.Windows.Forms.Button btnCancelNewMap;
        private System.Windows.Forms.TextBox tbNewMapName;
    }
}