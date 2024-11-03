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
    public partial class QLTheLoai : Form
    {
        private readonly GenreService genreService;
        public QLTheLoai()
        {
            InitializeComponent();
            genreService = new GenreService();
            LoadGenres();
        }
        private void LoadGenres()
        {
            dgvTheLoai.DataSource = genreService.GetAllGenres();
            dgvTheLoai.Columns["GenreId"].HeaderText = "ID";      
            dgvTheLoai.Columns["GenreName"].HeaderText = "Thể Loại"; 
            dgvTheLoai.Columns["GenreId"].Visible = false; 
            if (dgvTheLoai.Columns.Contains("Movies"))
            {
                dgvTheLoai.Columns["Movies"].Visible = false; 
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            var genreName = txtTheLoai.Text.Trim();
            if (genreService.GenreExists(genreName))
            {
                MessageBox.Show("Thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Clear(); 
                txtTheLoai.Focus(); 
                return;
            }

            var genre = new Genres { GenreName = genreName };
            genreService.AddGenre(genre);
            LoadGenres();
            txtTheLoai.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTheLoai.CurrentRow == null) return;
            var genre = (Genres)dgvTheLoai.CurrentRow.DataBoundItem;
            genreService.DeleteGenre(genre.GenreId);
            LoadGenres();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTheLoai.CurrentRow == null) return;

            var genre = (Genres)dgvTheLoai.CurrentRow.DataBoundItem;
            var genreName = txtTheLoai.Text.Trim();
            if (genreService.GenreExists(genreName) && genreName != genre.GenreName)
            {
                MessageBox.Show("Thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Clear(); 
                txtTheLoai.Focus(); 
                return;
            }

            genre.GenreName = genreName;
            genreService.UpdateGenre(genre);
            LoadGenres();
            txtTheLoai.Clear();

        }

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtTheLoai.Text = dgvTheLoai.Rows[e.RowIndex].Cells["GenreName"].Value.ToString();
            }
        }
    }
}
