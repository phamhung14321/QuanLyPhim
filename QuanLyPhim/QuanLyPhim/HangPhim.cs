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
    public partial class HangPhim : Form
    {
        private readonly StudioService studioService;
        public HangPhim()
        {
            InitializeComponent();
            studioService = new StudioService();
            LoadStudios();
        }
        private void LoadStudios()
        {
            dgvHang.DataSource = studioService.GetAllStudio();
            dgvHang.Columns["StudioId"].HeaderText = "ID";       // Thiết lập tiêu đề cột GenreId
            dgvHang.Columns["StudioName"].HeaderText = "Hãng"; // Thiết lập tiêu đề cột GenreName
            // Ẩn cột GenreId nếu không cần hiển thị
            dgvHang.Columns["StudioId"].Visible = false; // Chỉnh sửa thành false nếu bạn muốn ẩn cột
            if (dgvHang.Columns.Contains("Movies"))
            {
                dgvHang.Columns["Movies"].Visible = false; // Ẩn cột Movies
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            var studioName = txtHang.Text.Trim();

            // Kiểm tra nếu tên studio đã được nhập
            if (string.IsNullOrEmpty(studioName))
            {
                MessageBox.Show("Vui lòng nhập tên studio!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu tên studio đã tồn tại
            if (studioService.StudioExists(studioName))
            {
                MessageBox.Show("Studio đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var studio = new Studios { StudioName = studioName };

            try
            {
                studioService.AddStudio(studio); // Thêm studio vào cơ sở dữ liệu
                LoadStudios(); // Tải lại danh sách studio sau khi thêm
                txtHang.Clear(); // Xóa ô nhập
                MessageBox.Show("Studio đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm studio: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Kiểm tra thêm thông tin chi tiết nếu có
                if (ex.InnerException != null)
                {
                    MessageBox.Show("Chi tiết lỗi: " + ex.InnerException.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HangPhim_Load(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một studio để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvHang.SelectedRows[0];
            if (selectedRow.Cells["StudioId"].Value == null)
            {
                MessageBox.Show("Không tìm thấy ID của studio được chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int studioId = (int)selectedRow.Cells["StudioId"].Value; // Lấy ID của studio
            var studioName = txtHang.Text.Trim();

            if (string.IsNullOrEmpty(studioName))
            {
                MessageBox.Show("Vui lòng nhập tên studio!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu tên studio đã tồn tại
            if (studioService.StudioExists(studioName) && studioId != (int)selectedRow.Cells["StudioId"].Value)
            {
                MessageBox.Show("Studio đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var studio = new Studios { StudioId = studioId, StudioName = studioName };

            try
            {
                studioService.UpdateStudio(studio); // Sửa studio
                LoadStudios(); // Tải lại danh sách studio sau khi sửa
                txtHang.Clear(); // Xóa ô nhập
                MessageBox.Show("Studio đã được sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa studio: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một studio để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvHang.SelectedRows[0];
            var studioId = (int)selectedRow.Cells["StudioId"].Value; // Lấy ID của studio được chọn

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa studio này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return; // Nếu không xác nhận thì không thực hiện xóa
            }

            try
            {
                studioService.DeleteStudio(studioId); // Xóa studio
                LoadStudios(); // Tải lại danh sách studio sau khi xóa
                MessageBox.Show("Studio đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa studio: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHang.SelectedRows.Count > 0)
            {
                var selectedRow = dgvHang.SelectedRows[0];
                var studioId = (int)selectedRow.Cells["StudioId"].Value; // Lấy ID của studio
                var studioName = selectedRow.Cells["StudioName"].Value.ToString(); // Lấy tên studio

                txtHang.Text = studioName; // Hiển thị tên studio trong ô nhập
            }
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvHang.Rows[e.RowIndex];

                // Check if the selected row has the required data
                if (selectedRow.Cells["StudioName"].Value != null)
                {
                    // Get the studio name from the selected row
                    var studioName = selectedRow.Cells["StudioName"].Value.ToString();
                    // Display the studio name in the TextBox
                    txtHang.Text = studioName;
                }
            }
        }
    }
    }
    

