namespace D2DMapEditor
{
    partial class ExportMap
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
            this.gbLayerOption = new System.Windows.Forms.GroupBox();
            this.rbLayerAll = new System.Windows.Forms.RadioButton();
            this.rbLayerVisible = new System.Windows.Forms.RadioButton();
            this.rbLayerSelected = new System.Windows.Forms.RadioButton();
            this.gbLanguageOption = new System.Windows.Forms.GroupBox();
            this.rbLanguageXML = new System.Windows.Forms.RadioButton();
            this.rbLanguageActionScript = new System.Windows.Forms.RadioButton();
            this.rbLanguageCS = new System.Windows.Forms.RadioButton();
            this.rbLanguageCPP = new System.Windows.Forms.RadioButton();
            this.gbTilesOption = new System.Windows.Forms.GroupBox();
            this.rbTilePNG = new System.Windows.Forms.RadioButton();
            this.rbTileGIF = new System.Windows.Forms.RadioButton();
            this.rbTileJPEG = new System.Windows.Forms.RadioButton();
            this.rbTileBMP = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.exportMapDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.rbLanguageXMLLite = new System.Windows.Forms.RadioButton();
            this.gbLayerOption.SuspendLayout();
            this.gbLanguageOption.SuspendLayout();
            this.gbTilesOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLayerOption
            // 
            this.gbLayerOption.Controls.Add(this.rbLayerAll);
            this.gbLayerOption.Controls.Add(this.rbLayerVisible);
            this.gbLayerOption.Controls.Add(this.rbLayerSelected);
            this.gbLayerOption.Location = new System.Drawing.Point(12, 12);
            this.gbLayerOption.Name = "gbLayerOption";
            this.gbLayerOption.Size = new System.Drawing.Size(237, 91);
            this.gbLayerOption.TabIndex = 0;
            this.gbLayerOption.TabStop = false;
            this.gbLayerOption.Text = "Layers";
            // 
            // rbLayerAll
            // 
            this.rbLayerAll.AutoSize = true;
            this.rbLayerAll.Checked = true;
            this.rbLayerAll.Location = new System.Drawing.Point(16, 66);
            this.rbLayerAll.Name = "rbLayerAll";
            this.rbLayerAll.Size = new System.Drawing.Size(70, 17);
            this.rbLayerAll.TabIndex = 2;
            this.rbLayerAll.TabStop = true;
            this.rbLayerAll.Text = "All Layers";
            this.rbLayerAll.UseVisualStyleBackColor = true;
            // 
            // rbLayerVisible
            // 
            this.rbLayerVisible.AutoSize = true;
            this.rbLayerVisible.Location = new System.Drawing.Point(16, 42);
            this.rbLayerVisible.Name = "rbLayerVisible";
            this.rbLayerVisible.Size = new System.Drawing.Size(107, 17);
            this.rbLayerVisible.TabIndex = 1;
            this.rbLayerVisible.TabStop = true;
            this.rbLayerVisible.Text = "Visible layers only";
            this.rbLayerVisible.UseVisualStyleBackColor = true;
            // 
            // rbLayerSelected
            // 
            this.rbLayerSelected.AutoSize = true;
            this.rbLayerSelected.Location = new System.Drawing.Point(16, 19);
            this.rbLayerSelected.Name = "rbLayerSelected";
            this.rbLayerSelected.Size = new System.Drawing.Size(114, 17);
            this.rbLayerSelected.TabIndex = 0;
            this.rbLayerSelected.TabStop = true;
            this.rbLayerSelected.Text = "Selected layer only";
            this.rbLayerSelected.UseVisualStyleBackColor = true;
            // 
            // gbLanguageOption
            // 
            this.gbLanguageOption.Controls.Add(this.rbLanguageXMLLite);
            this.gbLanguageOption.Controls.Add(this.rbLanguageXML);
            this.gbLanguageOption.Controls.Add(this.rbLanguageActionScript);
            this.gbLanguageOption.Controls.Add(this.rbLanguageCS);
            this.gbLanguageOption.Controls.Add(this.rbLanguageCPP);
            this.gbLanguageOption.Location = new System.Drawing.Point(12, 109);
            this.gbLanguageOption.Name = "gbLanguageOption";
            this.gbLanguageOption.Size = new System.Drawing.Size(237, 89);
            this.gbLanguageOption.TabIndex = 1;
            this.gbLanguageOption.TabStop = false;
            this.gbLanguageOption.Text = "Language";
            // 
            // rbLanguageXML
            // 
            this.rbLanguageXML.AutoSize = true;
            this.rbLanguageXML.Location = new System.Drawing.Point(121, 20);
            this.rbLanguageXML.Name = "rbLanguageXML";
            this.rbLanguageXML.Size = new System.Drawing.Size(47, 17);
            this.rbLanguageXML.TabIndex = 3;
            this.rbLanguageXML.TabStop = true;
            this.rbLanguageXML.Text = "XML";
            this.rbLanguageXML.UseVisualStyleBackColor = true;
            // 
            // rbLanguageActionScript
            // 
            this.rbLanguageActionScript.AutoSize = true;
            this.rbLanguageActionScript.Location = new System.Drawing.Point(16, 67);
            this.rbLanguageActionScript.Name = "rbLanguageActionScript";
            this.rbLanguageActionScript.Size = new System.Drawing.Size(82, 17);
            this.rbLanguageActionScript.TabIndex = 2;
            this.rbLanguageActionScript.TabStop = true;
            this.rbLanguageActionScript.Text = "ActionScript";
            this.rbLanguageActionScript.UseVisualStyleBackColor = true;
            // 
            // rbLanguageCS
            // 
            this.rbLanguageCS.AutoSize = true;
            this.rbLanguageCS.Location = new System.Drawing.Point(16, 44);
            this.rbLanguageCS.Name = "rbLanguageCS";
            this.rbLanguageCS.Size = new System.Drawing.Size(39, 17);
            this.rbLanguageCS.TabIndex = 1;
            this.rbLanguageCS.Text = "C#";
            this.rbLanguageCS.UseVisualStyleBackColor = true;
            // 
            // rbLanguageCPP
            // 
            this.rbLanguageCPP.AutoSize = true;
            this.rbLanguageCPP.Checked = true;
            this.rbLanguageCPP.Location = new System.Drawing.Point(16, 20);
            this.rbLanguageCPP.Name = "rbLanguageCPP";
            this.rbLanguageCPP.Size = new System.Drawing.Size(44, 17);
            this.rbLanguageCPP.TabIndex = 0;
            this.rbLanguageCPP.TabStop = true;
            this.rbLanguageCPP.Text = "C++";
            this.rbLanguageCPP.UseVisualStyleBackColor = true;
            // 
            // gbTilesOption
            // 
            this.gbTilesOption.Controls.Add(this.rbTilePNG);
            this.gbTilesOption.Controls.Add(this.rbTileGIF);
            this.gbTilesOption.Controls.Add(this.rbTileJPEG);
            this.gbTilesOption.Controls.Add(this.rbTileBMP);
            this.gbTilesOption.Location = new System.Drawing.Point(12, 204);
            this.gbTilesOption.Name = "gbTilesOption";
            this.gbTilesOption.Size = new System.Drawing.Size(236, 70);
            this.gbTilesOption.TabIndex = 2;
            this.gbTilesOption.TabStop = false;
            this.gbTilesOption.Text = "Tiles";
            // 
            // rbTilePNG
            // 
            this.rbTilePNG.AutoSize = true;
            this.rbTilePNG.Location = new System.Drawing.Point(120, 43);
            this.rbTilePNG.Name = "rbTilePNG";
            this.rbTilePNG.Size = new System.Drawing.Size(48, 17);
            this.rbTilePNG.TabIndex = 3;
            this.rbTilePNG.TabStop = true;
            this.rbTilePNG.Text = "PNG";
            this.rbTilePNG.UseVisualStyleBackColor = true;
            // 
            // rbTileGIF
            // 
            this.rbTileGIF.AutoSize = true;
            this.rbTileGIF.Location = new System.Drawing.Point(120, 19);
            this.rbTileGIF.Name = "rbTileGIF";
            this.rbTileGIF.Size = new System.Drawing.Size(42, 17);
            this.rbTileGIF.TabIndex = 2;
            this.rbTileGIF.TabStop = true;
            this.rbTileGIF.Text = "GIF";
            this.rbTileGIF.UseVisualStyleBackColor = true;
            // 
            // rbTileJPEG
            // 
            this.rbTileJPEG.AutoSize = true;
            this.rbTileJPEG.Location = new System.Drawing.Point(15, 43);
            this.rbTileJPEG.Name = "rbTileJPEG";
            this.rbTileJPEG.Size = new System.Drawing.Size(52, 17);
            this.rbTileJPEG.TabIndex = 1;
            this.rbTileJPEG.TabStop = true;
            this.rbTileJPEG.Text = "JPEG";
            this.rbTileJPEG.UseVisualStyleBackColor = true;
            // 
            // rbTileBMP
            // 
            this.rbTileBMP.AutoSize = true;
            this.rbTileBMP.Checked = true;
            this.rbTileBMP.Location = new System.Drawing.Point(15, 19);
            this.rbTileBMP.Name = "rbTileBMP";
            this.rbTileBMP.Size = new System.Drawing.Size(48, 17);
            this.rbTileBMP.TabIndex = 0;
            this.rbTileBMP.TabStop = true;
            this.rbTileBMP.Text = "BMP";
            this.rbTileBMP.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(11, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnExport.Location = new System.Drawing.Point(173, 280);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rbLanguageXMLLite
            // 
            this.rbLanguageXMLLite.AutoSize = true;
            this.rbLanguageXMLLite.Location = new System.Drawing.Point(121, 44);
            this.rbLanguageXMLLite.Name = "rbLanguageXMLLite";
            this.rbLanguageXMLLite.Size = new System.Drawing.Size(67, 17);
            this.rbLanguageXMLLite.TabIndex = 3;
            this.rbLanguageXMLLite.TabStop = true;
            this.rbLanguageXMLLite.Text = "XML Lite";
            this.rbLanguageXMLLite.UseVisualStyleBackColor = true;
            // 
            // ExportMap
            // 
            this.AcceptButton = this.btnExport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(261, 310);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbTilesOption);
            this.Controls.Add(this.gbLanguageOption);
            this.Controls.Add(this.gbLayerOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportMap";
            this.Text = "Export Map";
            this.gbLayerOption.ResumeLayout(false);
            this.gbLayerOption.PerformLayout();
            this.gbLanguageOption.ResumeLayout(false);
            this.gbLanguageOption.PerformLayout();
            this.gbTilesOption.ResumeLayout(false);
            this.gbTilesOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLayerOption;
        private System.Windows.Forms.RadioButton rbLayerSelected;
        private System.Windows.Forms.RadioButton rbLayerVisible;
        private System.Windows.Forms.RadioButton rbLayerAll;
        private System.Windows.Forms.GroupBox gbLanguageOption;
        private System.Windows.Forms.RadioButton rbLanguageXML;
        private System.Windows.Forms.RadioButton rbLanguageActionScript;
        private System.Windows.Forms.RadioButton rbLanguageCS;
        private System.Windows.Forms.RadioButton rbLanguageCPP;
        private System.Windows.Forms.GroupBox gbTilesOption;
        private System.Windows.Forms.RadioButton rbTileJPEG;
        private System.Windows.Forms.RadioButton rbTileBMP;
        private System.Windows.Forms.RadioButton rbTileGIF;
        private System.Windows.Forms.RadioButton rbTilePNG;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog exportMapDialog;
        private System.Windows.Forms.RadioButton rbLanguageXMLLite;
    }
}