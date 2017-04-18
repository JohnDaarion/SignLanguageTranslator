namespace SignLanguageTranslator
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.loadedPictureBox = new Emgu.CV.UI.ImageBox();
            this.resultImageBox = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.openAndExeButton = new System.Windows.Forms.Button();
            this.dialogLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.galleryButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelForProcents = new System.Windows.Forms.Label();
            this.labelForrResult = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.CameraButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelFilterOptions = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ColorsLabel = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultImageBox)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.Controls.Add(this.loadedPictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.resultImageBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 345);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // loadedPictureBox
            // 
            this.loadedPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.loadedPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadedPictureBox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.loadedPictureBox.Location = new System.Drawing.Point(3, 43);
            this.loadedPictureBox.Name = "loadedPictureBox";
            this.loadedPictureBox.Size = new System.Drawing.Size(279, 299);
            this.loadedPictureBox.TabIndex = 2;
            this.loadedPictureBox.TabStop = false;
            this.loadedPictureBox.Click += new System.EventHandler(this.loadedPictureBox_Click);
            this.loadedPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.loadedPictureBox_Paint);
            this.loadedPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loadedPictureBox_MouseDown);
            this.loadedPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.loadedPictureBox_MouseUp);
            // 
            // resultImageBox
            // 
            this.resultImageBox.BackColor = System.Drawing.SystemColors.Window;
            this.resultImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultImageBox.Location = new System.Drawing.Point(288, 43);
            this.resultImageBox.Name = "resultImageBox";
            this.resultImageBox.Size = new System.Drawing.Size(279, 299);
            this.resultImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.resultImageBox.TabIndex = 2;
            this.resultImageBox.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.openAndExeButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dialogLabel, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(279, 34);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // openAndExeButton
            // 
            this.openAndExeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openAndExeButton.Location = new System.Drawing.Point(3, 3);
            this.openAndExeButton.Name = "openAndExeButton";
            this.openAndExeButton.Size = new System.Drawing.Size(133, 28);
            this.openAndExeButton.TabIndex = 0;
            this.openAndExeButton.Text = "Load Picture To Check";
            this.openAndExeButton.UseVisualStyleBackColor = true;
            this.openAndExeButton.Click += new System.EventHandler(this.openAndExeButton_Click);
            // 
            // dialogLabel
            // 
            this.dialogLabel.AutoSize = true;
            this.dialogLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogLabel.Location = new System.Drawing.Point(142, 0);
            this.dialogLabel.Name = "dialogLabel";
            this.dialogLabel.Size = new System.Drawing.Size(134, 34);
            this.dialogLabel.TabIndex = 1;
            this.dialogLabel.Text = "No File Loaded";
            this.dialogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel2.Controls.Add(this.openFolderButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.galleryButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(288, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(279, 34);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openFolderButton.Location = new System.Drawing.Point(3, 3);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(129, 28);
            this.openFolderButton.TabIndex = 1;
            this.openFolderButton.Text = "Make New Sigh Base";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // galleryButton
            // 
            this.galleryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.galleryButton.Location = new System.Drawing.Point(138, 3);
            this.galleryButton.Name = "galleryButton";
            this.galleryButton.Size = new System.Drawing.Size(162, 28);
            this.galleryButton.TabIndex = 2;
            this.galleryButton.Text = "Make Gallery";
            this.galleryButton.UseVisualStyleBackColor = true;
            this.galleryButton.Click += new System.EventHandler(this.galleryButton_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.labelForProcents, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelForrResult, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.button1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.CameraButton, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(573, 43);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(96, 299);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // labelForProcents
            // 
            this.labelForProcents.AutoSize = true;
            this.labelForProcents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelForProcents.Location = new System.Drawing.Point(3, 0);
            this.labelForProcents.Name = "labelForProcents";
            this.labelForProcents.Size = new System.Drawing.Size(90, 53);
            this.labelForProcents.TabIndex = 5;
            this.labelForProcents.Text = "No Sigh Detected";
            this.labelForProcents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelForrResult
            // 
            this.labelForrResult.AutoSize = true;
            this.labelForrResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelForrResult.Location = new System.Drawing.Point(3, 53);
            this.labelForrResult.Name = "labelForrResult";
            this.labelForrResult.Size = new System.Drawing.Size(90, 51);
            this.labelForrResult.TabIndex = 4;
            this.labelForrResult.Text = "No Sigh Detected";
            this.labelForrResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 53);
            this.button1.TabIndex = 6;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CameraButton
            // 
            this.CameraButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CameraButton.Location = new System.Drawing.Point(3, 166);
            this.CameraButton.Name = "CameraButton";
            this.CameraButton.Size = new System.Drawing.Size(90, 130);
            this.CameraButton.TabIndex = 7;
            this.CameraButton.Text = "Use Camera";
            this.CameraButton.UseVisualStyleBackColor = true;
            this.CameraButton.Click += new System.EventHandler(this.CameraButton_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.labelFilterOptions, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown1, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.ColorsLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown2, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown3, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown4, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown5, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown6, 0, 7);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(675, 43);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(103, 299);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // labelFilterOptions
            // 
            this.labelFilterOptions.AutoSize = true;
            this.labelFilterOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFilterOptions.Location = new System.Drawing.Point(3, 0);
            this.labelFilterOptions.Name = "labelFilterOptions";
            this.labelFilterOptions.Size = new System.Drawing.Size(97, 26);
            this.labelFilterOptions.TabIndex = 0;
            this.labelFilterOptions.Text = "Filter Options";
            this.labelFilterOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(3, 44);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // ColorsLabel
            // 
            this.ColorsLabel.AutoSize = true;
            this.ColorsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColorsLabel.Location = new System.Drawing.Point(3, 26);
            this.ColorsLabel.Name = "ColorsLabel";
            this.ColorsLabel.Size = new System.Drawing.Size(97, 15);
            this.ColorsLabel.TabIndex = 2;
            this.ColorsLabel.Text = "Colors";
            this.ColorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Location = new System.Drawing.Point(3, 70);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown3.Location = new System.Drawing.Point(3, 98);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown3.TabIndex = 4;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown4.Location = new System.Drawing.Point(3, 124);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown4.TabIndex = 5;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown5.Location = new System.Drawing.Point(3, 147);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown5.TabIndex = 6;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown6.Location = new System.Drawing.Point(3, 170);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown6.TabIndex = 7;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDown6_ValueChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "fileToOpen";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 345);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "mainForm";
            this.Text = "SighLanguageTranslator";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultImageBox)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button openAndExeButton;
        private System.Windows.Forms.Label dialogLabel;
        private Emgu.CV.UI.ImageBox loadedPictureBox;
        private Emgu.CV.UI.ImageBox resultImageBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelForrResult;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label labelForProcents;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label labelFilterOptions;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label ColorsLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button CameraButton;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Button galleryButton;
    }
}

