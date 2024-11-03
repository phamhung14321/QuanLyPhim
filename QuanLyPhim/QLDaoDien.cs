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
            dgvThongTinDaoDien.Columns["DirectorId"].Visible = false; 
            if (dgvThongTinDaoDien.Columns.Contains("Movies"))
            {
                dgvThongTinDaoDien.Columns["Movies"].Visible = false;
            }
        }
        private void ClearInputFields()
        {
            txtTenDaoDien.Clear();
            dateTimePicker1.Value = DateTime.Now; 
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            var fullName = txtTenDaoDien.Text.Trim();

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
            ClearInputFields();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvThongTinDaoDien.CurrentRow == null) return;

            var director = (Directors)dgvThongTinDaoDien.CurrentRow.DataBoundItem;
            var fullName = txtTenDaoDien.Text.Trim();
            if (directorService.DirectorExists(fullName) && fullName != director.FullName)
            {
                MessageBox.Show("Đạo diễn đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            director.FullName = fullName;
            director.BirthDate = dateTimePicker1.Value;
            directorService.UpdateDirector(director);
            LoadDirectors();
            ClearInputFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThongTinDaoDien.CurrentRow == null) return;

            var director = (Directors)dgvThongTinDaoDien.CurrentRow.DataBoundItem;
            directorService.DeleteDirector(director.DirectorId); 
            LoadDirectors();
            ClearInputFields();
        }

        private void dgvThongTinDaoDien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvThongTinDaoDien.Rows.Count)
            {
                var director = (Directors)dgvThongTinDaoDien.Rows[e.RowIndex].DataBoundItem;
                txtTenDaoDien.Text = director.FullName;
                if (director.BirthDate.HasValue)
                {
                    dateTimePicker1.Value = director.BirthDate.Value; 
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now; 
                }
            }
        }
    }
}
