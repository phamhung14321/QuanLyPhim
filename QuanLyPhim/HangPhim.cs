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
            dgvHang.Columns["StudioId"].HeaderText = "ID";       
            dgvHang.Columns["StudioName"].HeaderText = "Hãng"; 
            dgvHang.Columns["StudioId"].Visible = false; 
            if (dgvHang.Columns.Contains("Movies"))
            {
                dgvHang.Columns["Movies"].Visible = false; 
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            var studioName = txtHang.Text.Trim();
            if (string.IsNullOrEmpty(studioName))
            {
                MessageBox.Show("Vui lòng nhập tên studio!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (studioService.StudioExists(studioName))
            {
                MessageBox.Show("Studio đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var studio = new Studios { StudioName = studioName };

            try
            {
                studioService.AddStudio(studio); 
                LoadStudios();
                txtHang.Clear(); 
                MessageBox.Show("Studio đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm studio: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int studioId = (int)selectedRow.Cells["StudioId"].Value;
            var studioName = txtHang.Text.Trim();

            if (string.IsNullOrEmpty(studioName))
            {
                MessageBox.Show("Vui lòng nhập tên studio!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (studioService.StudioExists(studioName) && studioId != (int)selectedRow.Cells["StudioId"].Value)
            {
                MessageBox.Show("Studio đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var studio = new Studios { StudioId = studioId, StudioName = studioName };

            try
            {
                studioService.UpdateStudio(studio);
                LoadStudios(); 
                txtHang.Clear();
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
            var studioId = (int)selectedRow.Cells["StudioId"].Value;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa studio này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                studioService.DeleteStudio(studioId); 
                LoadStudios(); 
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
                var studioId = (int)selectedRow.Cells["StudioId"].Value; 
                var studioName = selectedRow.Cells["StudioName"].Value.ToString(); 

                txtHang.Text = studioName; 
            }
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvHang.Rows[e.RowIndex];
                if (selectedRow.Cells["StudioName"].Value != null)
                {
                    var studioName = selectedRow.Cells["StudioName"].Value.ToString();
                    txtHang.Text = studioName;
                }
            }
        }
    }
    }
    

