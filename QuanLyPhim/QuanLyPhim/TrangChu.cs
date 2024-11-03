using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhim
{
    public partial class TrangChu : Form
    {
        private  MovieService movieService = new MovieService();
        private GenreService genreService = new GenreService();
        private int selectedMovieId; // Biến để lưu ID phim đã chọn

        public TrangChu()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadMoviesGrid();
            cmbTheLoai.Leave += (s, e) => ResetFilters();
            textBox1.Leave += (s, e) => ResetFilters();
        }
        private void ResetFilters()
        {
            cmbTheLoai.SelectedIndex = -1; // Xóa lựa chọn trong ComboBox
            textBox1.Clear(); // Xóa nội dung trong TextBox
            LoadMoviesGrid(); // Tải lại danh sách phim mặc định trong DataGridView
 
        }

        private void LoadComboBoxes()
        {
            // Tải danh sách thể loại vào ComboBox
            cmbTheLoai.DataSource = genreService.GetAllGenres();
            cmbTheLoai.DisplayMember = "GenreName";
            cmbTheLoai.ValueMember = "GenreId";
            cmbTheLoai.SelectedIndex = -1; 
            
        }
        private void LoadMoviesGrid(List<Movies> movies = null)
        {
            var movieList = movies ?? movieService.GetAllMovies();
            var displayList = movieList.Select(m => new
            {
                m.MovieId,
                m.Title,
                m.ReleaseYear,
                m.Description,
                m.ImagePath,
                Image = LoadImage(m.ImagePath)
            }).ToList();

            dgvDanhSachPhim.DataSource = displayList;

            dgvDanhSachPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn cột ID (MovieId)
            if (dgvDanhSachPhim.Columns.Contains("MovieId"))
            {
                dgvDanhSachPhim.Columns["MovieId"].Visible = false;
            }

            // Kiểm tra và hiển thị hình ảnh của phim đầu tiên
            if (movieList.Any())
            {
                var firstMovie = movieList.First();
                string imagePath = firstMovie.ImagePath;
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    pbPoster.Image = Image.FromFile(imagePath);
                    pbPoster.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pbPoster.Image = null; // Đặt hình ảnh là null nếu không có hình
                }
            }

            // Kiểm tra và thêm cột hình ảnh nếu chưa có
            if (!dgvDanhSachPhim.Columns.Contains("Image"))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                {
                    HeaderText = "Hình Ảnh",
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Name = "Hình ảnh"
                };
                dgvDanhSachPhim.Columns.Add(imageColumn);
            }

            if (dgvDanhSachPhim.Columns.Contains("ImagePath"))
            {
                dgvDanhSachPhim.Columns["ImagePath"].Visible = false;
            }
            dgvDanhSachPhim.Columns["Image"].HeaderText = "Ảnh minh họa";

            // Set Vietnamese headers
            dgvDanhSachPhim.Columns["Title"].HeaderText = "Tên phim";
            dgvDanhSachPhim.Columns["ReleaseYear"].HeaderText = "Năm ra mắt";
            dgvDanhSachPhim.Columns["Description"].HeaderText = "Mô tả";
        }


        private Image LoadImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                return Image.FromFile(imagePath);
            }
            return null; 
        }


        private void dgvDanhSachPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedMovieId = (int)dgvDanhSachPhim.Rows[e.RowIndex].Cells["MovieId"].Value;
                string imagePath = dgvDanhSachPhim.Rows[e.RowIndex].Cells["ImagePath"].Value?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    pbPoster.Image = Image.FromFile(imagePath);
                    pbPoster.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pbPoster.Image = null; 
                }
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            Movies selectedMovie = null; // Khởi tạo biến selectedMovie là null

            if (selectedMovieId > 0) // Nếu đã chọn phim
            {
                selectedMovie = movieService.GetMovieById(selectedMovieId);
            }

            // Nếu không có phim nào được chọn, lấy phim đầu tiên trong danh sách
            if (selectedMovie == null)
            {
                var allMovies = movieService.GetAllMovies();
                if (allMovies.Any()) // Kiểm tra có phim không
                {
                    selectedMovie = allMovies.First(); // Lấy phim đầu tiên
                }
                else
                {
                    MessageBox.Show("Không có phim nào để xem chi tiết."); // Không có phim
                    return; // Nếu không có phim, kết thúc
                }
            }

            // Tạo và hiển thị form ThongTinPhim, truyền vào đối tượng selectedMovie
            ThongTinPhim thongTinPhim = new ThongTinPhim(selectedMovie.MovieId); // Gửi ID phim
            thongTinPhim.Show(); // Hiển thị form chi tiết     
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            QLPhim quanlyphim = new QLPhim();
            // Đăng ký sự kiện FormClosed
            quanlyphim.FormClosed += (s, args) =>
            {
                LoadMoviesGrid(); // Refresh data khi form QLPhim đóng
            };
            quanlyphim.ShowDialog();

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            QLTheLoai quanlyTL = new QLTheLoai();
            quanlyTL.ShowDialog();
            
        }

        private void QLHang_Click(object sender, EventArgs e)
        {
            HangPhim quanlyHang = new HangPhim();
            quanlyHang.ShowDialog();
            
        }

        private void QLDienVien_Click(object sender, EventArgs e)
        {
            QLDienVien quanlyDV = new QLDienVien();
            quanlyDV.ShowDialog();
            
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            QLDaoDien qLDaoDien = new QLDaoDien();
            qLDaoDien.ShowDialog();
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchKeyword))
            {
                LoadMoviesGrid();
            }
            else
            {
                List<Movies> filteredMovies = movieService.SearchMovies(searchKeyword);
                LoadMoviesGrid(filteredMovies);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTheLoai.SelectedValue is int genreId && genreId > 0)
            {
                List<Movies> filteredMovies = movieService.GetMoviesByGenre(genreId);
                LoadMoviesGrid(filteredMovies); // Tải lại DataGridView theo thể loại được chọn
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int year))
            {
                List<Movies> filteredMovies = movieService.GetMoviesByYear(year);
                LoadMoviesGrid(filteredMovies); 
            }
            else if (string.IsNullOrEmpty(textBox1.Text))
            {
                LoadMoviesGrid(); 
            }
        }
    }
}
