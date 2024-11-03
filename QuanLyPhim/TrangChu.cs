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
        private int selectedMovieId; 

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
            cmbTheLoai.SelectedIndex = -1; 
            textBox1.Clear(); 
            LoadMoviesGrid(); 
 
        }

        private void LoadComboBoxes()
        {
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

            if (dgvDanhSachPhim.Columns.Contains("MovieId"))
            {
                dgvDanhSachPhim.Columns["MovieId"].Visible = false;
            }

 
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
                    pbPoster.Image = null; 
                }
            }

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
            Movies selectedMovie = null; 

            if (selectedMovieId > 0) 
            {
                selectedMovie = movieService.GetMovieById(selectedMovieId);
            }

            if (selectedMovie == null)
            {
                var allMovies = movieService.GetAllMovies();
                if (allMovies.Any()) 
                {
                    selectedMovie = allMovies.First();
                }
                else
                {
                    MessageBox.Show("Không có phim nào để xem chi tiết."); 
                    return; 
                }
            }

            ThongTinPhim thongTinPhim = new ThongTinPhim(selectedMovie.MovieId); 
            thongTinPhim.Show();    
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            QLPhim quanlyphim = new QLPhim();

            quanlyphim.FormClosed += (s, args) =>
            {
                LoadMoviesGrid(); 
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
                LoadMoviesGrid(filteredMovies);
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
