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
    public partial class frmGoodsSearch : Form
    {
        string _sqlWhere = "";  // 查询 SQL 的条件部分

        public frmGoodsSearch()
        {
            InitializeComponent();
        }

        private void frmGoodsSearch_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            BindCBX();  // 自定义函数
            //BindDGV();
            cbx_Agent.SelectedIndex = 0;

            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);   // 添加控件的事件处理函数
            BindDGV();  // 自定义函数

            if (!Global.IsAdmin)
            {
                dataGridView1.Columns["cDel"].Visible = false;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string _id = txt_SupplyNo.Text.Trim();
            string _agent = cbx_Agent.SelectedValue.ToString().Trim();
            string _barcode = txt_Barcode.Text.Trim();

            string sqlWhere = "";
            if (!string.IsNullOrEmpty(_id))
            {
                sqlWhere += " SupplyID LIKE'%" + _id + "%' AND";
            }
            if (!string.IsNullOrEmpty(_agent))
            {
                sqlWhere += " AgentName='" + _agent + "' AND";
            }
            if (!string.IsNullOrEmpty(_barcode))
            {
                sqlWhere += " SupplyID IN(SELECT DISTINCT SupplyID FROM SupplyDetail WHERE Barcode LIKE'%" + _barcode + "%') AND";
            }
            if (sqlWhere.Contains("AND"))
            {
                sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 3);  // 将末尾的 And 字符串去掉
            }
            //else
            //{
            //    sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 5);
            //}

            _sqlWhere = sqlWhere;

            pagerControl1.PageIndex = 1;
            BindDGV();
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
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cDetail"].Index)
            {
                e.Value = " 详细";
            }
            if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
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
            if (e.ColumnIndex == dataGridView1.Columns["cDetail"].Index)
            {
                object v = dataGridView1.Rows[e.RowIndex].Cells["cSupplyID"].Value;
                if (v != null)
                {
                    string supplyID = v.ToString();
                    //frmSupplyDetail f = new frmSupplyDetail(supplyID);

                    frmSupplyReport f = new frmSupplyReport(supplyID);  // 本项目的 frmSupplyReport 窗体
                    f.ShowDialog();
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                if (MessageBox.Show(this, "删除后数据不能恢复，是否继续删除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    object v = dataGridView1.Rows[e.RowIndex].Cells["cSupplyID"].Value;
                    if (v != null)
                    {
                        Supply no = new Supply();
                        int re = no.Delete(v.ToString());
                        if (re > 0)
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

        /// <summary>
        /// 控件 pagerControl1 的 OnPageChanged 事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            BindDGV();
        }

        /// <summary>
        /// “客户”下拉列表的数据获取/更新
        /// </summary>
        private void BindCBX()
        {
            DataSet ds = new Agent().GetCbxList("");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr["Name"] = " ";
                dt.Rows.InsertAt(dr, 0);
                cbx_Agent.DataSource = dt;
                cbx_Agent.DisplayMember = "Name";
                cbx_Agent.ValueMember = "Name";
            }
            else
            {
                MessageBox.Show("请先添加客户资料！");
                return;
            }
        }

        /// <summary>
        /// dataGridView 的数据获取/更新
        /// </summary>
        public void BindDGV()
        {
            //int cnt = 0;
            //foreach (DataRow w in dt.Rows)
            //{
            //    sumMoney += decimal.Parse(w["SumPrice"].ToString());
            //}
            //cnt = dt.Rows.Count;

            decimal _sumMoney = 0;
            int _count = 0;
            Supply s = new Supply();
            DataSet ds = s.GetPageList(pagerControl1.PageSize, pagerControl1.PageIndex, _sqlWhere, out _count, out _sumMoney);//s.GetFilterList(sqlWhere);
            pagerControl1.DrawControl(_count);

            lab_Cnt.Text = _count.ToString();
            lab_Sum.Text = _sumMoney.ToString("0.00") + "元";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
