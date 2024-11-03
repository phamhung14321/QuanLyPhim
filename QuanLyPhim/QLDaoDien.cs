using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhim
{
    public partial class QLDaoDien : Form
    {
        private readonly DirectorService directorService;
        public QLDaoDien()
        {
            InitializeComponent();
            directorService = new DirectorService();
            LoadDirectors();
        }

        private void LoadDirectors()
        {
            dgvThongTinDaoDien.DataSource = directorService.GetAllDirectors();
            dgvThongTinDaoDien.Columns["DirectorId"].HeaderText = "ID";       
            dgvThongTinDaoDien.Columns["FullName"].HeaderText = "Tên đạo diễn"; 
            dgvThongTinDaoDien.Columns["BirthDate"].HeaderText = "Năm sinh";
            // Ẩn cột GenreId nếu không cần hiển thị
            dgvThongTinDaoDien.Columns["DirectorId"].Visible = false; 
            if (dgvThongTinDaoDien.Columns.Contains("Movies"))
            {
                dgvThongTinDaoDien.Columns["Movies"].Visible = false;
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            var fullName = txtTenDaoDien.Text.Trim();

            // Kiểm tra nếu tên đạo diễn đã tồn tại
            if (directorService.DirectorExists(fullName))
            {
                MessageBox.Show("Đạo diễn đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var director = new Directors
            {
                FullName = fullName,
                BirthDate = dateTimePicker1.Value
            };
            directorService.AddDirector(director);
            LoadDirectors();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvThongTinDaoDien.CurrentRow == null) return;

            var director = (Directors)dgvThongTinDaoDien.CurrentRow.DataBoundItem;
            var fullName = txtTenDaoDien.Text.Trim();

            // Kiểm tra nếu tên đạo diễn đã tồn tại (trừ tên hiện tại)
            if (directorService.DirectorExists(fullName) && fullName != director.FullName)
            {
                MessageBox.Show("Đạo diễn đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            director.FullName = fullName;
            director.BirthDate = dateTimePicker1.Value;
            directorService.UpdateDirector(director);
            LoadDirectors();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThongTinDaoDien.CurrentRow == null) return;

            var director = (Directors)dgvThongTinDaoDien.CurrentRow.DataBoundItem;
            directorService.DeleteDirector(director.DirectorId); // Sử dụng hàm DeleteDirector
            LoadDirectors();
        }
    }
}
