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
    public partial class frmGoodsIn : Form
    {
        public frmGoodsIn()
        {
            InitializeComponent();
        }

        private void frmGoodsIn_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            //this.txt_AutoCode.Text = "自动生成";//DateTime.Now.ToString("yyyyMMdd" + "XXXX");
            //DataTable dt = GeneralDataTable();
            //dataGridView1.DataSource = dt;
            BindNorm();
            BindDGV();
            txt_Operator.Text = Global.userName;
        }

        public static DataTable GeneralDataTable()
        {
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            tblDatas.Columns[0].AutoIncrement = true;
            tblDatas.Columns[0].AutoIncrementSeed = 1;
            tblDatas.Columns[0].AutoIncrementStep = 1;

            tblDatas.Columns.Add("A", Type.GetType("System.String"));
            tblDatas.Columns.Add("B", Type.GetType("System.String"));
            tblDatas.Columns.Add("C", Type.GetType("System.String"));
            tblDatas.Columns.Add("D", Type.GetType("System.String"));

            tblDatas.Rows.Add(new object[] { null, "20141114AAAA", "8G", "1000","打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114BBBB", "64G", "2000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114CCCC", "16G", "1000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114DDDD", "64G", "5000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114EEEE", "8G", "1000", "打印条码" });

            return tblDatas;

        }

        private void BindNorm()
        {
            Norm n = new Norm();
            DataSet ds = n.GetList("");
            if (ds != null)
            {
                cbx_Norm.DataSource = ds.Tables[0];
                cbx_Norm.DisplayMember = "NormName";
                cbx_Norm.ValueMember = "NormName";
            }
            else
            {
                MessageBox.Show("请先设置产品规格!");
                btn_Add.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cPrint"].Index)
            {
                frmInDetail f = new frmInDetail();
                f.ShowDialog();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                if (MessageBox.Show(this, "删除后数据不能恢复，是否继续删除?", "警告", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    object v = dataGridView1.Rows[e.RowIndex].Cells["cBatch"].Value;
                    if (v!=null)
                    {
                        InW no = new InW();
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
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string cntStr = txt_Cnt.Text.Trim();
            if (!ValidateService.IsNumber(cntStr))
            {
                MessageBox.Show("数量格式不正确!");
                txt_Cnt.Focus();
                return;
            }

            InW m = new InW();
            m.NormName = cbx_Norm.SelectedValue.ToString();
            m.Cnt = int.Parse(cntStr);
            m.Batch = GenBatchNO();
            m.Barcode = GenBarcode();
            m.Operator = Global.userName;
            m.InTime = dtp_InTime.Value;
            int re = m.Add();
            if (re > 0)
            {
                MessageBox.Show("入库成功!");
                BindDGV();
            }
            else
            {
                MessageBox.Show("入库失败!");
            }
        }

        private void BindDGV()
        {
            DataSet ds = new InW().GetList("");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private string GetNormFormat(string name)
        {
            return "1234";
        }

        /// <summary>
        /// 生成批号
        /// </summary>
        /// <returns></returns>
        private string GenBatchNO()
        {
            string batchNO = "";
            batchNO += GetNormFormat(cbx_Norm.SelectedItem.ToString());
            batchNO += DateTime.Now.ToString("yyMMddhhmmss");
            batchNO += "001";
            return batchNO;
        }

        /// <summary>
        /// 生成条码
        /// </summary>
        /// <returns></returns>
        private string GenBarcode()
        {
            return DateTime.Now.ToString("yyMMddhhmmss");
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cPrint"].Index)
            {
                e.Value = "打印条形码";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = " 删除";
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewService.VisibleRowOrder(dataGridView1, e);
        }


    }
}
