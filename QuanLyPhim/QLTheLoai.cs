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
            dgvTheLoai.Columns["GenreId"].HeaderText = "ID";       // Thiết lập tiêu đề cột GenreId
            dgvTheLoai.Columns["GenreName"].HeaderText = "Thể Loại"; // Thiết lập tiêu đề cột GenreName
            // Ẩn cột GenreId nếu không cần hiển thị
            dgvTheLoai.Columns["GenreId"].Visible = false; // Chỉnh sửa thành false nếu bạn muốn ẩn cột
            if (dgvTheLoai.Columns.Contains("Movies"))
            {
                dgvTheLoai.Columns["Movies"].Visible = false; // Ẩn cột Movies
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            var genreName = txtTheLoai.Text.Trim();

            // Kiểm tra nếu tên thể loại đã tồn tại
            if (genreService.GenreExists(genreName))
            {
                MessageBox.Show("Thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Clear(); // Xóa trường nhập
                txtTheLoai.Focus(); // Đặt tiêu điểm vào trường nhập
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

            // Kiểm tra nếu tên thể loại đã tồn tại (trừ tên hiện tại)
            if (genreService.GenreExists(genreName) && genreName != genre.GenreName)
            {
                MessageBox.Show("Thể loại đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Clear(); // Xóa trường nhập
                txtTheLoai.Focus(); // Đặt tiêu điểm vào trường nhập
                return;
            }

            genre.GenreName = genreName;
            genreService.UpdateGenre(genre);
            LoadGenres();
            txtTheLoai.Clear();

        }
    }
}
