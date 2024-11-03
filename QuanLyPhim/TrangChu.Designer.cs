namespace QuanLyPhim
{
    partial class TrangChu
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.QLPhim = new System.Windows.Forms.ToolStripLabel();
            this.QLTheLoai = new System.Windows.Forms.ToolStripLabel();
            this.QLHang = new System.Windows.Forms.ToolStripLabel();
            this.QLDienVien = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dgvDanhSachPhim = new System.Windows.Forms.DataGridView();
            this.pbPoster = new System.Windows.Forms.PictureBox();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.cmbTheLoai = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbNamRaMat = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QLPhim,
            this.QLTheLoai,
            this.QLHang,
            this.QLDienVien,
            this.toolStripLabel6,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(3453, 48);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // QLPhim
            // 
            this.QLPhim.Name = "QLPhim";
            this.QLPhim.Size = new System.Drawing.Size(196, 41);
            this.QLPhim.Text = "Quản lý phim";
            this.QLPhim.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // QLTheLoai
            // 
            this.QLTheLoai.Name = "QLTheLoai";
            this.QLTheLoai.Size = new System.Drawing.Size(226, 41);
            this.QLTheLoai.Text = "Quản lý thể loại";
            this.QLTheLoai.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // QLHang
            // 
            this.QLHang.Name = "QLHang";
            this.QLHang.Size = new System.Drawing.Size(195, 41);
            this.QLHang.Text = "Quản lý hãng";
            this.QLHang.Click += new System.EventHandler(this.QLHang_Click);
            // 
            // QLDienVien
            // 
            this.QLDienVien.Name = "QLDienVien";
            this.QLDienVien.Size = new System.Drawing.Size(248, 41);
            this.QLDienVien.Text = "Quản lý diễn viên";
            this.QLDienVien.Click += new System.EventHandler(this.QLDienVien_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(0, 41);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(245, 41);
            this.toolStripLabel1.Text = "Quản lý đạo diễn";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // dgvDanhSachPhim
            // 
            this.dgvDanhSachPhim.AllowUserToAddRows = false;
            this.dgvDanhSachPhim.AllowUserToDeleteRows = false;
            this.dgvDanhSachPhim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachPhim.Location = new System.Drawing.Point(670, 210);
            this.dgvDanhSachPhim.Name = "dgvDanhSachPhim";
            this.dgvDanhSachPhim.ReadOnly = true;
            this.dgvDanhSachPhim.RowHeadersWidth = 102;
            this.dgvDanhSachPhim.RowTemplate.Height = 40;
            this.dgvDanhSachPhim.Size = new System.Drawing.Size(2416, 651);
            this.dgvDanhSachPhim.TabIndex = 1;
            this.dgvDanhSachPhim.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachPhim_CellClick);
            // 
            // pbPoster
            // 
            this.pbPoster.Location = new System.Drawing.Point(260, 210);
            this.pbPoster.Name = "pbPoster";
            this.pbPoster.Size = new System.Drawing.Size(307, 351);
            this.pbPoster.TabIndex = 2;
            this.pbPoster.TabStop = false;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(327, 962);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(281, 74);
            this.btnXemChiTiet.TabIndex = 3;
            this.btnXemChiTiet.Text = "Xem chi tiết phim";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // cmbTheLoai
            // 
            this.cmbTheLoai.FormattingEnabled = true;
            this.cmbTheLoai.Location = new System.Drawing.Point(327, 759);
            this.cmbTheLoai.Name = "cmbTheLoai";
            this.cmbTheLoai.Size = new System.Drawing.Size(294, 39);
            this.cmbTheLoai.TabIndex = 4;
            this.cmbTheLoai.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 766);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thể loại";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 849);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Năm ra mắt";
            // 
            // cmbNamRaMat
            // 
            this.cmbNamRaMat.FormattingEnabled = true;
            this.cmbNamRaMat.Location = new System.Drawing.Point(327, 849);
            this.cmbNamRaMat.Name = "cmbNamRaMat";
            this.cmbNamRaMat.Size = new System.Drawing.Size(294, 39);
            this.cmbNamRaMat.TabIndex = 4;
            this.cmbNamRaMat.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(1623, 928);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(609, 38);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3453, 1486);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbNamRaMat);
            this.Controls.Add(this.cmbTheLoai);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.pbPoster);
            this.Controls.Add(this.dgvDanhSachPhim);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TrangChu";
            this.Text = "FormMain";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachPhim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPoster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel QLPhim;
        private System.Windows.Forms.ToolStripLabel QLTheLoai;
        private System.Windows.Forms.DataGridView dgvDanhSachPhim;
        private System.Windows.Forms.PictureBox pbPoster;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.ComboBox cmbTheLoai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbNamRaMat;
        private System.Windows.Forms.ToolStripLabel QLHang;
        private System.Windows.Forms.ToolStripLabel QLDienVien;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TextBox txtSearch;
    }
}

