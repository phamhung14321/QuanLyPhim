using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using BUS;
using DAL.Entities;
using static System.Net.WebRequestMethods;
using System.Linq;

namespace QuanLyPhim
{
    public partial class QLPhim : Form
    {
        private MovieService movieService;
        private GenreService genreService;
        private ActorService actorService;
        private DirectorService directorService;
        private StudioService studioService;
        private string selectedImagePath;
        

        public QLPhim()
        {
            InitializeComponent();
            movieService = new MovieService();
            genreService = new GenreService();
            actorService = new ActorService();
            directorService = new DirectorService();
            studioService = new StudioService();
            LoadMovies();
            LoadDirectors();
            LoadGenres();  
            LoadActors();
            LoadStudio();
        }
        private void LoadGenres()
        {
            var genres = genreService.GetAllGenres(); // Fetch genres from the service
            clbTheLoai.Items.Clear(); // Clear previous items if any
            foreach (var genre in genres)
            {
                clbTheLoai.Items.Add(genre.GenreName); // Add genre names to CheckedListBox
            }
        }
        private void LoadStudio()
        {
            var studios = studioService.GetAllStudio(); 
            clbHang.Items.Clear(); 
            foreach (var studio in studios)
            {
                clbHang.Items.Add(studio.StudioName); 
            }
        }
        private void LoadDirectors()
        {
            var directors = directorService.GetAllDirectors(); // Lấy danh sách từ dịch vụ
            clbDaoDien.Items.Clear(); // Xóa mục cũ nếu có
            foreach (var director in directors)
            {
                clbDaoDien.Items.Add(director.FullName); // Thêm tên đạo diễn vào CheckedListBox
            }
        }
        private void LoadActors()
        {
            var actors = actorService.GetActors(); // Fetch actors from the service
            clbDienVien.Items.Clear(); // Clear previous items if any
            foreach (var actor in actors)
            {
                clbDienVien.Items.Add(actor.FullName); // Add actor names to CheckedListBox
            }
        }

        private void LoadMovies(string keyword = "")
        {
            var movies = string.IsNullOrEmpty(keyword) ? movieService.SearchMovies("") : movieService.SearchMovies(keyword);

            var movieDisplayList = movies.Select(movie => new
            {
                movie.MovieId,
                movie.Title,
                movie.ReleaseYear,
                movie.Description,
                Directors = string.Join(", ", movie.Directors.Select(d => d.FullName)),
                Genres = string.Join(", ", movie.Genres.Select(g => g.GenreName)),
                Actors = string.Join(", ", movie.Actors.Select(a => a.FullName)),
                Studios = string.Join(", ", movie.Studios.Select(s => s.StudioName)),
                movie.ImagePath
            }).ToList();

            dgvDanhSachPhim.DataSource = movieDisplayList;

            // Hide MovieId column
            dgvDanhSachPhim.Columns["MovieId"].Visible = false;
            dgvDanhSachPhim.Columns["ImagePath"].Visible = false;

            // Set Vietnamese headers
            dgvDanhSachPhim.Columns["Title"].HeaderText = "Tên phim";
            dgvDanhSachPhim.Columns["ReleaseYear"].HeaderText = "Năm ra mắt";
            dgvDanhSachPhim.Columns["Description"].HeaderText = "Mô tả";
            dgvDanhSachPhim.Columns["Directors"].HeaderText = "Đạo diễn";
            dgvDanhSachPhim.Columns["Genres"].HeaderText = "Thể loại";
            dgvDanhSachPhim.Columns["Actors"].HeaderText = "Diễn viên";
            dgvDanhSachPhim.Columns["Studios"].HeaderText = "Hãng phim";
        }


        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtTenPhim.Text))
            {
                MessageBox.Show("Tên phim không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(txtNamRaMat.Text, out _))
            {
                MessageBox.Show("Năm ra mắt phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void btnChonAnh_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    pbPosterPhim.Image = Image.FromFile(selectedImagePath);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            var movie = new Movies
            {
                Title = txtTenPhim.Text,
                ReleaseYear = string.IsNullOrEmpty(txtNamRaMat.Text) ? (int?)null : int.Parse(txtNamRaMat.Text),
                Description = rtxtMoTa.Text,
                Directors = GetDirectorsFromSelectedItems(clbDaoDien), // Get directors
                Genres = GetGenresFromSelectedItems(clbTheLoai), // Get genres
                Actors = GetActorsFromSelectedItems(clbDienVien), // Get actors
                Studios = GetStudioFromSelectedItems(clbHang)
            };

            try
            {
                movieService.AddMovie(movie, selectedImagePath);
                LoadMovies();
                MessageBox.Show("Thêm phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhim.CurrentRow == null || !ValidateForm()) return;

            dynamic selectedMovieData = dgvDanhSachPhim.CurrentRow.DataBoundItem;

            var updatedMovie = new Movies
        {
            MovieId = selectedMovieData.MovieId,
            Title = txtTenPhim.Text,
            ReleaseYear = string.IsNullOrEmpty(txtNamRaMat.Text) ? (int?)null : int.Parse(txtNamRaMat.Text),
            Description = rtxtMoTa.Text,
            Directors = GetDirectorsFromSelectedItems(clbDaoDien), 
            Genres = GetGenresFromSelectedItems(clbTheLoai),
            Actors = GetActorsFromSelectedItems(clbDienVien), 
            Studios = GetStudioFromSelectedItems(clbHang)
        };

        try
        {
            movieService.UpdateMovie(updatedMovie, selectedImagePath);
            LoadMovies();
            MessageBox.Show("Cập nhật phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
        private void btnXoa_Click(object sender, EventArgs e)
        {      
            if (dgvDanhSachPhim.CurrentRow == null) return;
            dynamic selectedMovie = dgvDanhSachPhim.CurrentRow.DataBoundItem;

            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phim này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int movieId = selectedMovie.MovieId;
                    movieService.DeleteMovie(movieId);
                    LoadMovies();
                    ResetForm();
                    MessageBox.Show("Xóa phim thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            


    }

        private void ResetForm()
        {
            txtTenPhim.Clear();
            txtNamRaMat.Clear();
            rtxtMoTa.Clear();
            clbDaoDien.ClearSelected();
            clbTheLoai.ClearSelected();
            clbDienVien.ClearSelected();
            pbPosterPhim.Image = null;
            selectedImagePath = null;
        }

        private List<Directors> GetDirectorsFromSelectedItems(CheckedListBox checkedListBox)
        {
            var selectedDirectors = new List<Directors>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                var director = movieService.FindDirectorByName(item.ToString());
                if (director != null)
                {
                    selectedDirectors.Add(director);
                }
            }
            return selectedDirectors; 
        }

        private List<Genres> GetGenresFromSelectedItems(CheckedListBox checkedListBox)
        {
            var selectedGenres = new List<Genres>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                var genre = movieService.FindGenreByName(item.ToString());
                if (genre != null)
                {
                    selectedGenres.Add(genre);
                }
            }
            return selectedGenres;
        }
        private List<Studios> GetStudioFromSelectedItems(CheckedListBox checkedListBox)
        {
            var selectedStudio = new List<Studios>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                var studio = movieService.FindStudioByName(item.ToString());
                if (studio != null)
                {
                    selectedStudio.Add(studio);
                }
            }
            return selectedStudio;
        }
        private List<Actors> GetActorsFromSelectedItems(CheckedListBox checkedListBox)
        {
            var selectedActors = new List<Actors>();
            foreach (var item in checkedListBox.CheckedItems)
            {
                var actor = movieService.FindActorByName(item.ToString());
                if (actor != null)
                {
                    selectedActors.Add(actor);
                }
            }
            return selectedActors;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadMovies(txtSearch.Text);
        }
        private void FilterCheckedListBox(CheckedListBox listBox, string filterText)
        {
            var allItems = listBox == clbDaoDien ? directorService.GetAllDirectors().Select(d => d.FullName).ToList() :
                          listBox == clbTheLoai ? genreService.GetAllGenres().Select(g => g.GenreName).ToList() :
                          listBox == clbDienVien ? actorService.GetActors().Select(a => a.FullName).ToList() :
                          listBox == clbHang ? studioService.GetAllStudio().Select(s => s.StudioName).ToList():
                          new List<string>() ;

            listBox.Items.Clear();
            listBox.Items.AddRange(allItems
                .Where(item => item.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray());
        }

        private void txtDaoDien_TextChanged(object sender, EventArgs e)
        {
            FilterCheckedListBox(clbDaoDien, txtDaoDien.Text);
        }

        private void txtTheLoai_TextChanged(object sender, EventArgs e)
        {
            FilterCheckedListBox(clbTheLoai, txtTheLoai.Text);
        }

        private void txtDienVien_TextChanged(object sender, EventArgs e)
        {
            FilterCheckedListBox(clbDienVien, txtDienVien.Text);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterCheckedListBox(clbHang, txtHang.Text);
        }
        private void dgvDanhSachPhim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                // Sử dụng dynamic để lấy đối tượng ẩn danh
                dynamic selectedMovie = dgvDanhSachPhim.CurrentRow.DataBoundItem;

                // Điền thông tin vào các trường nhập liệu
                txtTenPhim.Text = selectedMovie.Title;
                txtNamRaMat.Text = selectedMovie.ReleaseYear?.ToString();
                rtxtMoTa.Text = selectedMovie.Description;

                // Đẩy hình ảnh vào PictureBox
                if (!string.IsNullOrEmpty(selectedMovie.ImagePath))
                {
                    pbPosterPhim.Image = Image.FromFile(selectedMovie.ImagePath);
                }
                else
                {
                    pbPosterPhim.Image = null;
                }

                // Đặt các giá trị đã chọn trong CheckedListBox cho đạo diễn, thể loại, diễn viên
                SetCheckedItems(clbDaoDien, selectedMovie.Directors);
                SetCheckedItems(clbTheLoai, selectedMovie.Genres);
                SetCheckedItems(clbDienVien, selectedMovie.Actors);
                SetCheckedItems(clbHang, selectedMovie.Studios);
            }

        }
        private void SetCheckedItems(CheckedListBox checkedListBox, string items)
        {
            foreach (var index in Enumerable.Range(0, checkedListBox.Items.Count))
            {
                checkedListBox.SetItemChecked(index, false);
            }

            var selectedItems = items.Split(new[] { ", " }, StringSplitOptions.None);
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (selectedItems.Contains(checkedListBox.Items[i].ToString()))
                {
                    checkedListBox.SetItemChecked(i, true);
                }
            }
        }


    }
}
