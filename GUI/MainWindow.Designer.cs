namespace FaceRecognizer
{
    partial class MainWindow
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbLoad = new System.Windows.Forms.Button();
            this.btTrain = new System.Windows.Forms.Button();
            this.btFaceDetect = new System.Windows.Forms.Button();
            this.btPrev = new System.Windows.Forms.Button();
            this.lbAvgImg = new System.Windows.Forms.Label();
            this.lbEigenFaces = new System.Windows.Forms.Label();
            this.eigenFacesBox = new System.Windows.Forms.PictureBox();
            this.avgImg = new System.Windows.Forms.PictureBox();
            this.btNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eigenFacesBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.avgImg)).BeginInit();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(551, 365);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbLoad);
            this.panel1.Controls.Add(this.btTrain);
            this.panel1.Controls.Add(this.btFaceDetect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 377);
            this.panel1.TabIndex = 1;
            // 
            // tbLoad
            // 
            this.tbLoad.Location = new System.Drawing.Point(12, 99);
            this.tbLoad.Name = "tbLoad";
            this.tbLoad.Size = new System.Drawing.Size(139, 35);
            this.tbLoad.TabIndex = 5;
            this.tbLoad.Text = "Load Training";
            this.tbLoad.UseVisualStyleBackColor = true;
            this.tbLoad.Click += new System.EventHandler(this.tbLoad_Click);
            // 
            // btTrain
            // 
            this.btTrain.Location = new System.Drawing.Point(12, 55);
            this.btTrain.Name = "btTrain";
            this.btTrain.Size = new System.Drawing.Size(139, 38);
            this.btTrain.TabIndex = 4;
            this.btTrain.Text = "Start Training";
            this.btTrain.UseVisualStyleBackColor = true;
            this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
            // 
            // btFaceDetect
            // 
            this.btFaceDetect.Location = new System.Drawing.Point(12, 12);
            this.btFaceDetect.Name = "btFaceDetect";
            this.btFaceDetect.Size = new System.Drawing.Size(139, 37);
            this.btFaceDetect.TabIndex = 2;
            this.btFaceDetect.Text = "Detect Faces";
            this.btFaceDetect.UseVisualStyleBackColor = true;
            this.btFaceDetect.Click += new System.EventHandler(this.btFaceDetect_Click);
            // 
            // btPrev
            // 
            this.btPrev.Image = global::FaceRecognizer.Properties.Resources.arrowLeft;
            this.btPrev.Location = new System.Drawing.Point(558, 171);
            this.btPrev.Name = "btPrev";
            this.btPrev.Size = new System.Drawing.Size(65, 25);
            this.btPrev.TabIndex = 2;
            this.btPrev.UseVisualStyleBackColor = true;
            this.btPrev.Visible = false;
            this.btPrev.Click += new System.EventHandler(this.btPrev_Click);
            // 
            // lbAvgImg
            // 
            this.lbAvgImg.AutoSize = true;
            this.lbAvgImg.Location = new System.Drawing.Point(557, 209);
            this.lbAvgImg.Name = "lbAvgImg";
            this.lbAvgImg.Size = new System.Drawing.Size(79, 13);
            this.lbAvgImg.TabIndex = 8;
            this.lbAvgImg.Text = "Average Image";
            this.lbAvgImg.Visible = false;
            // 
            // lbEigenFaces
            // 
            this.lbEigenFaces.AutoSize = true;
            this.lbEigenFaces.Location = new System.Drawing.Point(555, 9);
            this.lbEigenFaces.Name = "lbEigenFaces";
            this.lbEigenFaces.Size = new System.Drawing.Size(60, 13);
            this.lbEigenFaces.TabIndex = 7;
            this.lbEigenFaces.Text = "Eigenfaces";
            this.lbEigenFaces.Visible = false;
            // 
            // eigenFacesBox
            // 
            this.eigenFacesBox.Location = new System.Drawing.Point(558, 25);
            this.eigenFacesBox.Name = "eigenFacesBox";
            this.eigenFacesBox.Size = new System.Drawing.Size(140, 140);
            this.eigenFacesBox.TabIndex = 6;
            this.eigenFacesBox.TabStop = false;
            // 
            // avgImg
            // 
            this.avgImg.Location = new System.Drawing.Point(558, 225);
            this.avgImg.Name = "avgImg";
            this.avgImg.Size = new System.Drawing.Size(140, 140);
            this.avgImg.TabIndex = 3;
            this.avgImg.TabStop = false;
            // 
            // btNext
            // 
            this.btNext.Image = global::FaceRecognizer.Properties.Resources.arrowRight;
            this.btNext.Location = new System.Drawing.Point(633, 171);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(65, 25);
            this.btNext.TabIndex = 9;
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Visible = false;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 377);
            this.Controls.Add(this.lbAvgImg);
            this.Controls.Add(this.btNext);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btPrev);
            this.Controls.Add(this.avgImg);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.eigenFacesBox);
            this.Controls.Add(this.lbEigenFaces);
            this.Name = "MainWindow";
            this.Text = "FaceRec";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eigenFacesBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.avgImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btFaceDetect;
        private System.Windows.Forms.PictureBox avgImg;
        private System.Windows.Forms.Button btTrain;
        private System.Windows.Forms.Button tbLoad;
        private System.Windows.Forms.Label lbEigenFaces;
        private System.Windows.Forms.PictureBox eigenFacesBox;
        private System.Windows.Forms.Label lbAvgImg;
        private System.Windows.Forms.Button btPrev;
        private System.Windows.Forms.Button btNext;
    }
}

