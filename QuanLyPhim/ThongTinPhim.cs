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
    public partial class ThongTinPhim : Form
    {
        private readonly MovieService movieService = new MovieService();
        private int currentMovieId;
        public ThongTinPhim(int movieId)
        {
            InitializeComponent();
            currentMovieId = movieId;
        }
        public void LoadData()
        {
            var movie = movieService.GetMovieById(currentMovieId);
            if (movie != null)
            {
                txtTenPhim.Text = movie.Title;
                rtxtThongTinPhim.Text = movie.Description;
                txtTenPhim.ReadOnly = true;
                rtxtThongTinPhim.ReadOnly = true;
                txtTheLoai.ReadOnly = true;
                txtHang.ReadOnly = true;
                txtNamRaMat.ReadOnly = true;
                txtDienVien.ReadOnly = true;
                if (!string.IsNullOrEmpty(movie.ImagePath))
                {
                    pictureBox1.Image = Image.FromFile(movie.ImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pictureBox1.Image = null;
                }
                txtTheLoai.Text = string.Join(", ", movie.Genres.Select(g => g.GenreName)); 

                txtHang.Text = string.Join(", ", movie.Studios.Select(s => s.StudioName)); 

                txtNamRaMat.Text = movie.ReleaseYear?.ToString() ?? "Chưa xác định";
                txtDienVien.Text = string.Join(", ", movie.Actors.Select(g => g.FullName));
            }
        }
        private void ThongTinPhim_Load(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}
