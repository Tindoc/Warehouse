using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Common;

namespace Warehouse
{
    public partial class frmAgentNorm : Form
    {
        public frmAgentNorm()
        {
            InitializeComponent();
        }

        private void frmAgentNorm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;  // 设置 dataGridView 列的冻结属性时需要设置
            BindDGV();  // 自定义函数
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Level user = new Level();   // 本项目的 Service 文件夹内的 Level 类（属于 Model 类）

            string _name = txt_Name.Text.Trim();
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("名称不能为空!");
                txt_Name.Focus();
                return;
            }
            if (user.Exists(_name))
            {
                MessageBox.Show("该名称已存在!");
                txt_Name.Focus();
                return;
            }
            string _price = txt_Price.Text.Trim();
            if (!ValidateService.IsNumber(_price))
            {
                MessageBox.Show("价格格式不正确!");
                txt_Price.Focus();
                return;
            }

            user.LevelName = _name;
            user.Price = decimal.Parse(_price);
            int re = user.Add();
            if (re > 0)
            {
                MessageBox.Show("添加成功!");
                txt_Name.Text = "";
                txt_Name.Focus();
                BindDGV();
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
        }

        /// <summary>
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cModity"].Index)
            {
                e.Value = "修改价格";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = "删除";
            }
            //else if (e.ColumnIndex == dataGridView1.Columns["cPrice"].Index)
            //{
            //    e.Value = e.Value + "元";
            //}
        }

        /// <summary>
        /// 单击单元格内容时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                if (MessageBox.Show(this, "确定要删除吗?", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    object v = dataGridView1.Rows[e.RowIndex].Cells["cName"].Value;
                    if (v != null)
                    {
                        Level no = new Level();
                        if (no.IsRelation(v.ToString()))
                        {
                            MessageBox.Show("该记录已被使用，禁止删除！");
                            return;
                        }
                        bool re = no.Delete(v.ToString());
                        if (re)
                        {
                            MessageBox.Show("删除成功!");
                            BindDGV();
                        }
                        else
                        {
                            MessageBox.Show("删除失败!");
                        }
                    }
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cModity"].Index)
            {
                object v = dataGridView1.Rows[e.RowIndex].Cells["cName"].Value;
                if (ValidateService.IsNotEmpty(v))
                {
                    Level no = new Level();
                    no.GetModel(v.ToString());
                    frmAgentNormUpdate f = new frmAgentNormUpdate(no);  // 本项目的 frmAgentNormUpdate 窗体
                    f.ShowDialog();
                    if (f._isTrue)
                    {
                        BindDGV();
                    }
                }
            }
        }

        /// <summary>
        /// 在发生所有单元格绘制之后，执行行级绘制时引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewService.VisibleRowOrder(dataGridView1, e);
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        private void BindDGV()
        {
            DataSet ds = new Level().GetList("");
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}