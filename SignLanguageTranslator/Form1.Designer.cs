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
            this.imagePathLabel = new System.Windows.Forms.Label();
            this.translationResultLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultImageBox)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.loadedPictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.resultImageBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.translationResultLabel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 445);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // loadedPictureBox
            // 
            this.loadedPictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.loadedPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadedPictureBox.Location = new System.Drawing.Point(3, 43);
            this.loadedPictureBox.Name = "loadedPictureBox";
            this.loadedPictureBox.Size = new System.Drawing.Size(269, 399);
            this.loadedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadedPictureBox.TabIndex = 2;
            this.loadedPictureBox.TabStop = false;
            // 
            // resultImageBox
            // 
            this.resultImageBox.BackColor = System.Drawing.SystemColors.Window;
            this.resultImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultImageBox.Location = new System.Drawing.Point(278, 43);
            this.resultImageBox.Name = "resultImageBox";
            this.resultImageBox.Size = new System.Drawing.Size(269, 399);
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
            this.tableLayoutPanel3.Controls.Add(this.imagePathLabel, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(269, 34);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // openAndExeButton
            // 
            this.openAndExeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openAndExeButton.Location = new System.Drawing.Point(3, 3);
            this.openAndExeButton.Name = "openAndExeButton";
            this.openAndExeButton.Size = new System.Drawing.Size(128, 28);
            this.openAndExeButton.TabIndex = 0;
            this.openAndExeButton.Text = "Start";
            this.openAndExeButton.UseVisualStyleBackColor = true;
            // 
            // imagePathLabel
            // 
            this.imagePathLabel.AutoSize = true;
            this.imagePathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePathLabel.Location = new System.Drawing.Point(137, 0);
            this.imagePathLabel.Name = "imagePathLabel";
            this.imagePathLabel.Size = new System.Drawing.Size(129, 34);
            this.imagePathLabel.TabIndex = 1;
            this.imagePathLabel.Text = "No File Loaded";
            this.imagePathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // translationResultLabel
            // 
            this.translationResultLabel.AutoSize = true;
            this.translationResultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.translationResultLabel.Location = new System.Drawing.Point(278, 0);
            this.translationResultLabel.Name = "translationResultLabel";
            this.translationResultLabel.Size = new System.Drawing.Size(269, 40);
            this.translationResultLabel.TabIndex = 4;
            this.translationResultLabel.Text = "No Sigh Detected";
            this.translationResultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 445);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "mainForm";
            this.Text = "SighLanguageTranslator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultImageBox)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button openAndExeButton;
        private System.Windows.Forms.Label imagePathLabel;
        private Emgu.CV.UI.ImageBox loadedPictureBox;
        private Emgu.CV.UI.ImageBox resultImageBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label translationResultLabel;
    }
}

