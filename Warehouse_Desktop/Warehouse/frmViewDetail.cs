using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SqlServerDAL;
using Common;

namespace Warehouse
{
    public partial class frmViewDetail : Form
    {
        string _norm = "", _model = "";

        public frmViewDetail()
        {
            InitializeComponent();
        }
        public frmViewDetail(string norm, string model) // 带参数的构造函数
        {
            InitializeComponent();
            this._norm = norm;
            this._model = model;
        }

        private void frmViewDetail_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Model,NormName,Barcode,Length,CreateTime FROM InWDetail WHERE Barcode NOT IN(SELECT Barcode FROM SupplyDetail) AND NormName = '" + _norm + "' AND Model = '" + _model + "'";
            DataSet ds = DbHelperSQL.Query(sql);
            dataGridView1.DataSource = ds.Tables[0];
        }

        /// <summary>
        /// 在发生所有单元格绘制之后，执行行级绘制时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewService.VisibleRowOrder(dataGridView1, e);  // 项目 Common 的 DataGridViewService 类
        }
    }
}
