﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;

namespace Warehouse
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            cbx_Level.SelectedIndex = 0;
            dataGridView1.AutoGenerateColumns = false;
            BindLevel();    // 自定义函数

            if (!Global.IsAdmin)
            {
                dataGridView1.Columns["cModity"].Visible = false;
                dataGridView1.Columns["cDel"].Visible = false;
            }
            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);
            BindDGV("");    // 自定义函数
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Agent model = new Agent();  // 本项目的 Service 文件夹内的 Agent 类（属于 Model 类）

            string _name = txt_Name.Text.Trim();
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("客户名称不能为空!");
                txt_Name.Focus();
                return;
            }
            if (model.Exists(_name))
            {
                MessageBox.Show("该客户名称已存在!");
                txt_Name.Focus();
                return;
            }
            model.Name = _name;
            model.LevelName = cbx_Level.SelectedValue.ToString();
            model.Contact = txt_Contact.Text.Trim();
            model.Phone = txt_Phone.Text.Trim();
            model.Address = txt_Address.Text.Trim();
            model.Tel = txt_Tel.Text.Trim();
            model.Fox = txt_Fox.Text.Trim();
            int re = model.Add();
            if (re > 0)
            {
                MessageBox.Show("添加成功!");
                txt_Name.Text = "";
                cbx_Level.SelectedIndex = 0;
                txt_Name.Focus();
                txt_Contact.Text = "";
                txt_Phone.Text = "";
                txt_Address.Text = "";
                txt_Fox.Text = "";
                txt_Tel.Text = "";
                BindDGV("");
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
        }

        /// <summary>
        /// “查询”按钮功能实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>只实现了以“名字”为条件查询</remarks>
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string _name = txt_Name.Text.Trim();
            BindDGV(" Name like'%" + _name + "%'");
            //if (string.IsNullOrEmpty(_name))
            //{
            //    BindDGV("");
            //}
            //else
            //{
            //    BindDGV(" Name like'%" + _name + "%'");
            //}
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
                e.Value = "修改";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = " 删除";
            }
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
                        Agent no = new Agent();
                        if (no.IsRelation(v.ToString()))
                        {
                            MessageBox.Show("该记录已被使用，禁止删除！");
                            return;
                        }
                        bool re = no.Delete(v.ToString());
                        if (re)
                        {
                            MessageBox.Show("删除成功!");
                            BindDGV("");
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
                if (v != null)
                {
                    frmCustomerUpdate f = new frmCustomerUpdate(v.ToString());  // 本项目的 frmCustomerUpdate 窗体
                    f.ShowDialog();
                    if (f._isOK)
                    {
                        BindDGV("");
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

        /// <summary>
        /// 控件 pagerControl1 的 OnPageChanged 事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            BindDGV("");
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        /// <param name="where"></param>
        private void BindDGV(string where)
        {
            int _count = 0;
            DataSet ds = new Agent().GetPageList(pagerControl1.PageSize, pagerControl1.PageIndex, where, out _count);
            dataGridView1.DataSource = ds.Tables[0];
            pagerControl1.DrawControl(_count);
        }

        /// <summary>
        /// “代理商”下拉列表的数据获取/更新
        /// </summary>
        private void BindLevel()
        {
            DataSet ds = new Level().GetList("");
            cbx_Level.DataSource = ds.Tables[0];
            cbx_Level.ValueMember = "LevelName";
            cbx_Level.DisplayMember = "LevelName";
        }
    }
}