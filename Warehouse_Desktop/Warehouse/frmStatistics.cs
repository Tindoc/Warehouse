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
    public partial class frmStatistics : Form
    {
        public frmStatistics()
        {
            InitializeComponent();
        }

        private void frmStatistics_Load(object sender, EventArgs e)
        {
            dtp_Start.Value = dtp_End.Value.AddMonths(-1);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string _start = dtp_Start.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            string _end = dtp_End.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            if (DateTime.Parse(_start) > DateTime.Parse(_end))
            {
                MessageBox.Show("开始时间不能大于结束时间!");
                return;
            }
            BindDGV(_start, _end);  // 自定义函数
        }

        /// <summary>
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void BindDGV(string start, string end)
        {
            string sql = "SELECT Model,NormName," +
               " (SELECT ISNULL(SUM(I.Cnt),0) FROM InWDetail I WHERE Model=N.Model AND NormName=N.NormName AND I.CreateTime BETWEEN '" + start + "' AND '" + end + "') AS InCnt," +
" (SELECT ISNULL(SUM(O.Cnt),0) FROM SupplyDetail O WHERE Model=N.Model AND NormName=N.NormName AND O.CreateTime BETWEEN '" + start + "' AND '" + end + "') AS OutCnt," +
" (SELECT ISNULL(SUM(X.SumMoney),0) FROM SupplyDetail X WHERE Model=N.Model AND NormName=N.NormName AND X.CreateTime BETWEEN '" + start + "' AND '" + end + "') AS OutMoney," +
" GETDATE() AS NowTime FROM InWDetail N Group BY Model,NormName ORDER BY Model ASC";
            DataSet ds = DbHelperSQL.Query(sql);    // 项目 SqlServerDAL 的 DbHelperSQL 类
            DataTable dt = ds.Tables[0];

            int _inCnt = 0, _outCnt = 0;
            decimal _outMoney = 0;
            foreach (DataRow r in dt.Rows)
            {
                _inCnt += int.Parse(r["InCnt"].ToString());
                _outCnt += int.Parse(r["OutCnt"].ToString());
                _outMoney += decimal.Parse(r["outMoney"].ToString());
            }
            DataRow dr = dt.NewRow();
            dr["NormName"] = "总计";
            dr["InCnt"] = _inCnt;
            dr["OutCnt"] = _outCnt;
            dr["OutMoney"] = _outMoney;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;
        }
    }
}