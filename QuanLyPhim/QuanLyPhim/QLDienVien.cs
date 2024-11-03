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
    public partial class QLDienVien : Form
    {
        private readonly ActorService actorService;
        public QLDienVien()
        {
            InitializeComponent();
            actorService = new ActorService();
            LoadActors();
        }

        private void LoadActors()
        {
            dgvDienVien.DataSource = actorService.GetAllActors();
            dgvDienVien.Columns["ActorId"].HeaderText = "ID";       // Thiết lập tiêu đề cột GenreId
            dgvDienVien.Columns["FullName"].HeaderText = "Tên diễn viên"; // Thiết lập tiêu đề cột GenreName
            // Ẩn cột GenreId nếu không cần hiển thị
            dgvDienVien.Columns["ActorId"].Visible = false; // Chỉnh sửa thành false nếu bạn muốn ẩn cột
            if (dgvDienVien.Columns.Contains("Movies"))
            {
                dgvDienVien.Columns["Movies"].Visible = false; // Ẩn cột Movies
            }
        }
        private void ClearInputFields()
        {
            txtTenDienVien.Clear();
            dateTimePicker1.Value = DateTime.Now; // Đặt lại giá trị DateTimePicker về giá trị mặc định
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var fullName = txtTenDienVien.Text.Trim();

            // Kiểm tra nếu tên diễn viên đã tồn tại
            if (actorService.ActorExists(fullName))
            {
                MessageBox.Show("Diễn viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var actor = new Actors
            {
                FullName = fullName,
                BirthDate = dateTimePicker1.Value
            };
            actorService.AddActor(actor);
            LoadActors();
            ClearInputFields();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDienVien.CurrentRow == null) return;

            var actor = (Actors)dgvDienVien.CurrentRow.DataBoundItem;
            var fullName = txtTenDienVien.Text.Trim();

            // Kiểm tra nếu tên diễn viên đã tồn tại (trừ tên hiện tại)
            if (actorService.ActorExists(fullName) && fullName != actor.FullName)
            {
                MessageBox.Show("Diễn viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            actor.FullName = fullName;
            actor.BirthDate = dateTimePicker1.Value;
            actorService.UpdateActor(actor);
            LoadActors();
            ClearInputFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDienVien.CurrentRow == null) return;
            var actor = (Actors)dgvDienVien.CurrentRow.DataBoundItem;
            actorService.DeleteActor(actor.ActorId);
            LoadActors();
            ClearInputFields();
        }

        private void dgvDienVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDienVien.Rows.Count)
            {
                var actor = (Actors)dgvDienVien.Rows[e.RowIndex].DataBoundItem;

                txtTenDienVien.Text = actor.FullName;

                if (actor.BirthDate.HasValue)
                {
                    dateTimePicker1.Value = actor.BirthDate.Value; 
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Now; 
                }
            }
        }
    }
}
