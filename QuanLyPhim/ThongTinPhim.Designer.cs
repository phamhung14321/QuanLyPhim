namespace QuanLyPhim
{
    partial class ThongTinPhim
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtxtThongTinPhim = new System.Windows.Forms.RichTextBox();
            this.txtTenPhim = new System.Windows.Forms.TextBox();
            this.txtDienVien = new System.Windows.Forms.TextBox();
            this.txtHang = new System.Windows.Forms.TextBox();
            this.txtNamRaMat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTheLoai = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(173, 122);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(309, 402);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // rtxtThongTinPhim
            // 
            this.rtxtThongTinPhim.Location = new System.Drawing.Point(726, 112);
            this.rtxtThongTinPhim.Name = "rtxtThongTinPhim";
            this.rtxtThongTinPhim.Size = new System.Drawing.Size(1610, 333);
            this.rtxtThongTinPhim.TabIndex = 3;
            this.rtxtThongTinPhim.Text = "";
            // 
            // txtTenPhim
            // 
            this.txtTenPhim.Location = new System.Drawing.Point(186, 570);
            this.txtTenPhim.Name = "txtTenPhim";
            this.txtTenPhim.Size = new System.Drawing.Size(295, 38);
            this.txtTenPhim.TabIndex = 5;
            // 
            // txtDienVien
            // 
            this.txtDienVien.Location = new System.Drawing.Point(923, 575);
            this.txtDienVien.Name = "txtDienVien";
            this.txtDienVien.Size = new System.Drawing.Size(1413, 38);
            this.txtDienVien.TabIndex = 6;
            // 
            // txtHang
            // 
            this.txtHang.Location = new System.Drawing.Point(923, 619);
            this.txtHang.Name = "txtHang";
            this.txtHang.Size = new System.Drawing.Size(1413, 38);
            this.txtHang.TabIndex = 6;
            // 
            // txtNamRaMat
            // 
            this.txtNamRaMat.Location = new System.Drawing.Point(332, 665);
            this.txtNamRaMat.Name = "txtNamRaMat";
            this.txtNamRaMat.Size = new System.Drawing.Size(176, 38);
            this.txtNamRaMat.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(739, 531);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Thể loại";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(740, 575);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "Diễn viên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 665);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "Năm ra mắt";
            // 
            // txtTheLoai
            // 
            this.txtTheLoai.Location = new System.Drawing.Point(923, 531);
            this.txtTheLoai.Name = "txtTheLoai";
            this.txtTheLoai.Size = new System.Drawing.Size(1413, 38);
            this.txtTheLoai.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(740, 622);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 32);
            this.label4.TabIndex = 9;
            this.label4.Text = "Studio";
            // 
            // ThongTinPhim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2594, 1538);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNamRaMat);
            this.Controls.Add(this.txtTheLoai);
            this.Controls.Add(this.txtHang);
            this.Controls.Add(this.txtDienVien);
            this.Controls.Add(this.txtTenPhim);
            this.Controls.Add(this.rtxtThongTinPhim);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ThongTinPhim";
            this.Text = "ThongTinPhim";
            this.Load += new System.EventHandler(this.ThongTinPhim_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox rtxtThongTinPhim;
        private System.Windows.Forms.TextBox txtTenPhim;
        private System.Windows.Forms.TextBox txtDienVien;
        private System.Windows.Forms.TextBox txtHang;
        private System.Windows.Forms.TextBox txtNamRaMat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTheLoai;
        private System.Windows.Forms.Label label4;
    }
}