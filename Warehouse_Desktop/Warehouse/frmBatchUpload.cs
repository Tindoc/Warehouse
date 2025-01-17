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
    public partial class frmBatchUpload : Form
    {
        public List<string> batchList = new List<string>(); // 用来保存 TextBox 中输入的条码，回传到 frmGoodsOut 窗体

        public frmBatchUpload()
        {
            InitializeComponent();
        }

        private void frmBatchUpload_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string txt = richTextBox1.Text.Trim().Replace("\n", "");
            if (!ValidateService.IsAllNumber(txt))  // 项目 Common 的 ValidateService 类
            {
                MessageBox.Show("包含非数字字符，请认真核对！");
                return;
            }
            //else if(txt))
            //{
            //    MessageBox.Show("内容为空！");
            //    return;
            //}
            else
            {
                int x = txt.Length / 14;
                for (int i = 0; i < x; i++)
                {
                    string s = txt.Substring(0, 14);
                    txt = txt.Remove(0, 14);
                    batchList.Add(s);
                }
                this.Close();
            }
        }

        /// <summary>
        /// 实时统计 richTextBox 中输入的条码数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = richTextBox1.Text.Trim();
            if (txt.Length % 14 == 0)
            {
                lab_Cnt.Text = "共 " + txt.Length / 14 + " 条";
            }
        }
    }
}