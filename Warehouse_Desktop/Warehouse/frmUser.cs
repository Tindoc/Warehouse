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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            // 测试用
            //DataTable dt = CommonService.GeneralDataTable();
            //dataGridView1.DataSource = dt;

            cbx_Position.SelectedIndex = 0;
            dataGridView1.AutoGenerateColumns = false;
            BindDGV();  // 自定义函数
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            User user = new User(); // 本项目的 Service 文件夹的 User 类（属于 Model 层）

            string _name = txt_Name.Text.Trim();
            if (string.IsNullOrEmpty(_name))
            {
                MessageBox.Show("用户名不能为空!");
                txt_Name.Focus();
                return;
            }
            if (user.Exists(_name))
            {
                MessageBox.Show("该用户名已存在!");
                txt_Name.Focus();
                return;
            }
            user.UserName = _name;
            user.Position = cbx_Position.SelectedItem.ToString();
            int re = user.Add();
            if (re > 0)
            {
                MessageBox.Show("添加成功!");
                txt_Name.Text = "";
                cbx_Position.SelectedIndex = 0;
                txt_Name.Focus();
                BindDGV();
            }
            else
            {
                MessageBox.Show("添加失败!");
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string _name = txt_Name.Text.Trim();
            string _position = cbx_Position.SelectedItem.ToString();
            string sql = "";
            if (_position != "不限")
            {
                sql += " [Position] = '" + _position + "' AND";
            }
            if (_name != string.Empty)
            {
                sql += " UserName LIKE'%" + _name + "%' AND";
            }
            if (sql.Trim() != string.Empty)
            {
                sql = sql.Substring(0, sql.Length - 3);
            }
            DataSet ds = new User().GetList(sql);
            if (ds != null)
            {
                dataGridView1.DataSource = ds.Tables[0];
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
                    object v = dataGridView1.Rows[e.RowIndex].Cells["cUserName"].Value;
                    if (v != null)
                    {
                        if (v.ToString() == "admin")
                        {
                            MessageBox.Show("不能删除系统管理员!");
                            return;
                        }

                        User no = new User();
                        no.UserName = v.ToString();

                        bool re = no.Delete();
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
                object v = dataGridView1.Rows[e.RowIndex].Cells["cUserName"].Value;
                if (v != null)
                {
                    if (v.ToString() == "admin")
                    {
                        MessageBox.Show("不能修改系统管理员!");
                        return;
                    }

                    User no = new User();
                    no.GetModel(v.ToString());
                    frmUserUpdate f = new frmUserUpdate(no);    // 本项目的 frmUserUpdate 窗体
                    f.ShowDialog();
                    if (f._isTrue)
                    {
                        BindDGV();  // 自定义函数
                    }
                }
            }
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        private void BindDGV()
        {
            DataSet ds = new User().GetList("");
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}