﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace Warehouse
{
    public partial class frmGoodsNorm : Form
    {
        public frmGoodsNorm()
        {
            InitializeComponent();
        }

        private void frmGoodsNorm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            BindDGV();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Norm user = new Norm();
            string _name = txt_Name.Text.Trim();

            if (!ValidateService.IsNumber(_name))   // 项目 Common 的 ValidateService 类
            {
                MessageBox.Show("必须为数字!");
                txt_Name.Focus();
                return;
            }
            if (user.Exists(_name))
            {
                MessageBox.Show("该规格已存在!");
                txt_Name.Focus();
                return;
            }

            user.NormName = _name;
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
        /// 获取 dataGridView 的数据
        /// </summary>
        private void BindDGV()
        {
            DataSet ds = new Norm().GetList("");
            dataGridView1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 需要设置显示单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cModity"].Index)
            {
                e.Value = "修改";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = " 删除";
            }
            //else if (e.ColumnIndex == dataGridView1.Columns["cName"].Index)
            //{
            //    //e.Value = e.Value;
            //}
        }

        /// <summary>
        /// 点击单元格的内容时发生
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
                        Norm no = new Norm();
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
                object v = dataGridView1.Rows[e.RowIndex].Cells["cID"].Value;
                if (ValidateService.IsNumber(v))
                {
                    Norm no = new Norm();
                    no.GetModel((int)v);
                    frmGoodsNormUpdate f = new frmGoodsNormUpdate(no);
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
            DataGridViewService.VisibleRowOrder(dataGridView1, e);  // 项目 Common 的 DataGridViewService 类
        }
    }
}
