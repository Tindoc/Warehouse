using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SqlServerDAL;

namespace Warehouse
{
    public partial class frmWarehouseView : Form
    {
        public frmWarehouseView()
        {
            InitializeComponent();
        }

        private void frmWarehouseView_Load(object sender, EventArgs e)
        {
            BindDGV();  // 自定义函数
        }

        /// <summary>
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cRemark"].Index)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["cNorm"].Value.ToString() != "总计")
                {
                    e.Value = "详细";
                }
            }
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
        }

        /// <summary>
        /// 单击单元格内容时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cRemark"].Index)
            {
                string _model = dataGridView1.Rows[e.RowIndex].Cells["cModel"].Value.ToString();
                string _norm = dataGridView1.Rows[e.RowIndex].Cells["cNorm"].Value.ToString();
                frmViewDetail fr = new frmViewDetail(_norm, _model);
                fr.ShowDialog();
            }
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        private void BindDGV()
        {
            //string sql = "SELECT NormName,((SELECT ISNULL(SUM(I.Cnt),0) FROM InWDetail I WHERE NormName=N.NormName) - (SELECT ISNULL(SUM(O.Cnt),0) FROM SupplyDetail O WHERE NormName=N.NormName)) AS StoreCnt,GETDATE() AS NowTime FROM Norm N";
            //string sql = "SELECT Model,NormName,COUNT(Barcode) as SumCnt,SUM(isnull(Length,0)) as SumLength,getdate() as NowTime  FROM InWDetail WHERE Barcode NOT IN(SELECT Barcode FROM SupplyDetail) GROUP BY NormName,Model";// SQL Server 用
            string sql = "SELECT Model,NormName,COUNT(Barcode) as SumCnt,SUM(iif(isnull(Length),0,Length)) as SumLength,date() as NowTime  FROM InWDetail WHERE Barcode NOT IN(SELECT Barcode FROM SupplyDetail) GROUP BY NormName,Model";   // Access 用
            DataSet ds = DbHelperSQL.Query(sql);    // 项目 SqlServer 的 DbHelperSQL 类
            DataTable dt = ds.Tables[0];
            int _inCnt = 0, _inLength = 0;
            foreach (DataRow r in dt.Rows)
            {
                _inCnt += int.Parse(r["SumCnt"].ToString());
                _inLength += int.Parse(r["SumLength"].ToString());
            }
            DataRow dr = dt.NewRow();
            dr["NormName"] = "总计";
            dr["SumCnt"] = _inCnt;
            dr["SumLength"] = _inLength;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;
        }
    }
}