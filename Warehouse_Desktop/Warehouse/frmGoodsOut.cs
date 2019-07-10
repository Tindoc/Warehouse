using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Common;
using SqlServerDAL;

namespace Warehouse
{
    public partial class frmGoodsOut : Form
    {
        IList<InW> allOut = new List<InW>();    // InW 是 本项目的 Service 文件夹里的类（属于 Model 层）
        // allOut 用来保存条码记录对应的 InW 对象，在输入条码的时候判断是否重复或已出仓，如果没有的话则添加到 allOut 中，见txt_Barcode_TextChanged() 和 link_Upload_Click()
        public decimal _price = 0;

        public frmGoodsOut()
        {
            InitializeComponent();
        }

        private void frmGoodsOut_Load(object sender, EventArgs e)
        {
            lab_Error.Tag = "9999"; // 该控件在 txtBarcode 后面
            lab_Error.ForeColor = Color.Red;

            dataGridView1.AutoGenerateColumns = false;
            txt_Operator.Text = Global.userName;
            BindCBX();  // 自定义函数
            BindCbxLevel(); // 自定义函数
            cbx_Agent.SelectedIndex = -1;   // “选择客户”标签后的 cbx（-1 为不选，0为第一个数据）
            cbx_Level.SelectedIndex = -1;   // “选择级别”标签后的 cbx
            panel_Time.Visible = true;      // “即时扫描”标签后的 panel
            rb_Time.Checked = true;         // “即时扫描” RatioButton
            link_Upload.Visible = false;    // “上传” Button
            rb_Batch.Checked = false;       // “批量上传” RationButton
        }

        /// <summary>
        /// 生成供货单的 button 点击事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbx_Agent.SelectedValue == null)
            {
                MessageBox.Show("请先选择客户！");
                cbx_Agent.Focus();
                return;
            }
            if (cbx_Level.SelectedValue == null)
            {
                MessageBox.Show("请先选择级别！");
                cbx_Level.Focus();
                return;
            }
            if (allOut.Count <= 0)
            {
                MessageBox.Show("没有要出货的成品！");
                return;
            }
            if (MessageBox.Show("确定要生成供货单吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            List<SupplyDetail> list = new List<SupplyDetail>(); // 本项目的 Service 文件夹里的 SupplyDetail 类（属于 Model 层）
            foreach (InW i in allOut)   // 本项目 Service 文件夹里的 InW 类（属于 Model 层）
            {
                SupplyDetail s = new SupplyDetail();
                s.Barcode = i.Barcode;
                s.NormName = i.NormName;
                s.SumMoney = i.SumPrice;
                s.Length = i.Length;
                s.Model = i.Model;
                list.Add(s);
            }

            Supply m = new Supply();    // 本项目 Service 文件夹里的 Supply 类（属于 Model 层） @与SupplyDetail 的区别？？？
            m.SupplyID = GenSupplyID();
            m.AgentName = cbx_Agent.SelectedValue.ToString();
            m.Price = _price;
            m.Operator = Global.userName;
            m.SumPrice = decimal.Parse(lab_Sum.Text.Replace("元", "").Trim());

            int re = m.Add(list);
            if (re > 0)
            {
                //MessageBox.Show("生成供应单成功！");
               
                // 生成成功后重置相关内容
                _price = 0;
                cbx_Agent.SelectedIndex = -1;
                cbx_Level.SelectedIndex = -1;
                txt_Price.Text = "";
                txt_Barcode.Text = "";
                allOut = new List<InW>();
                BindDGV();  // 自定义函数

                frmSupplyReport ff = new frmSupplyReport(m.SupplyID);   // 本项目的 frmSupplyReport 窗体
                ff.Text = "生成供应单成功";
                ff.ShowDialog();
            }
            else
            {
                MessageBox.Show("生成供应单失败！");
            }
        }

        private void cbx_Level_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_Level.SelectedValue != null)
            {
                string _name = cbx_Level.SelectedValue.ToString();
                Level m = new Level();  // 本项目 Service 文件夹内的 Level 类（属于 Model 类）
                m.GetModel(_name);
                if (!string.IsNullOrEmpty(m.LevelName))
                {
                    _price = m.Price;
                    txt_Price.Text = m.Price.ToString("0.00");
                    BindDGV();  // 自定义函数
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbx_Agent.SelectedValue != null)
            //{
            //    string _name = cbx_Agent.SelectedValue.ToString();
            //    Agent m = new Agent(_name);
            //    if (!string.IsNullOrEmpty(m.LevelName))
            //    {
            //        _price = m.Price;
            //        txt_Price.Text = m.Price.ToString("0.00");
            //        BindDGV();
            //    }
            //}
        }

        /// <summary>
        /// 单击单元格的内容时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["cDel"].Index == e.ColumnIndex)
            {
                string v = dataGridView1.Rows[e.RowIndex].Cells["cBarcode"].Value.ToString();
                InW _w = null;
                foreach (InW i in allOut)
                {
                    if (i.Barcode == v)
                    {
                        _w = i;
                        break;
                    }
                }
                if (_w != null)
                {
                    allOut.Remove(_w);
                    BindDGV();
                }
            }
        }

        /// <summary>
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = "移除";
            }
        }

        /// <summary>
        /// 在发生所有单元格绘制之后，执行行级绘制时引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewService.VisibleRowOrder(dataGridView1, e);  // 项目 Common 内的 DataGridService 类
        }
        
        /// <summary>
        /// 批量上传输入条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void link_Upload_Click(object sender, EventArgs e)
        {
            frmBatchUpload f = new frmBatchUpload();    // 本项目的 frmBatchUpload 窗体
            f.ShowDialog();
            if (f.batchList.Count > 0)  // batchList 为 frmBatchUpload 的数据成员
            {
                string _error = "";
                foreach (string _barcode in f.batchList)
                {
                    bool _isContain = false;
                    foreach (InW i in allOut)   // 本项目 Service 文件夹内的 InW 类（属于 Model 层）
                    {
                        if (i.Barcode == _barcode)
                        {
                            _isContain = true;
                            break;
                        }
                    }
                    if (_isContain)
                    {
                        _error += _barcode + "不能重复录入!\n";
                        continue;
                    }

                    if (!InWDetail.Exists(_barcode))
                    {
                        _error += _barcode + "该条码不存在!\n";
                        continue;
                    }
                    if (SupplyDetail.Exists(_barcode))
                    {
                        _error += _barcode + "该条码已出仓,不能重复出仓!\n";
                        continue;
                    }
                    InW w = new InW().GetModelByBarcode(_barcode);
                    allOut.Add(w);  // 如果该条码既不重复又还没出仓则添加到 allOut 中
                }
                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                }
                BindDGV();  // 自定义函数

                // 重置相关内容
                txt_Barcode.Text = "";
                lab_Error.Text = "";
            }
        }

        /// <summary>
        /// 即时扫描输入条码（应为“即时输入”）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Barcode_TextChanged(object sender, EventArgs e)
        {
            //if (cbx_Agent.SelectedValue == null)
            //{
            //    MessageBox.Show("请先选择客户！");
            //    cbx_Agent.Focus();
            //    return;
            //}
            if (txt_Barcode.Text.Trim().Length >= 14)
            {
                string _barcode = txt_Barcode.Text.Trim();

                bool _isContain = false;
                foreach (InW i in allOut)
                {
                    if (i.Barcode == _barcode)
                    {
                        _isContain = true;
                        break;
                    }
                }
                if (_isContain)
                {
                    lab_Error.Text = "不能重复录入!";
                    return;
                }

                if (!InWDetail.Exists(_barcode))
                {
                    lab_Error.Text = "该条码不存在!";
                    return;
                }
                if (SupplyDetail.Exists(_barcode))
                {
                    lab_Error.Text = "该条码已出仓,不能重复出仓!";
                    return;
                }
                InW w = new InW().GetModelByBarcode(_barcode);
                allOut.Add(w);  // 如果该条码既不重复又还没出仓则添加到 allOut 中
                BindDGV();  // 自定义函数

                // 重置相关内容
                txt_Barcode.Text = "";
                lab_Error.Text = "";
            }
            else
            {
                lab_Error.Text = "";
            }
        }

        private void txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Barcode_TextChanged(null, null);
            }
        }

        /// <summary>
        /// 每当 checked 属性改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_Batch_CheckedChanged(object sender, EventArgs e)
        {
            link_Upload.Visible = rb_Batch.Checked;
        }

        /// <summary>
        /// 当选择了“即时扫描”时，显示后面的 Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_Time_CheckedChanged(object sender, EventArgs e)
        {
            panel_Time.Visible = rb_Time.Checked;
        }

        /// <summary>
        /// 获取数据库中同一时间的供货单的最大单号的后四位（即流水号）
        /// </summary>
        /// <param name="front"></param>
        /// <returns></returns>
        /// <remarks>
        /// 同一个时间的供货单的判断：供货单号前6位
        /// “时间(6位)”(共6位)相同则供货单号后四位累增，如果不同则供货单号后三位从0001开始
        /// </remarks>
        private string GetTopSupplyID(string front)
        {
            string sql = "SELECT TOP 1 RIGHT(SupplyID,4) FROM Supply WHERE LEFT(SupplyID,6)='" + front + "' ORDER BY RIGHT(SupplyID,4) DESC";
            object obj = DbHelperSQL.GetSingle(sql);    // 项目 SqlServerDAL 的 DbHelper 类
            if (obj != DBNull.Value && obj != null)
            {
                return (Convert.ToInt32(obj) + 1).ToString("0000");
            }
            else
            {
                return "0001";
            }
        }

        /// <summary>
        /// 生成供货单号
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 生成规则：
        /// 时间(6位)+流水号(4位)
        /// </remarks>
        private string GenSupplyID()
        {
            string supplyID = CommonService.GetServerTime().ToString("yyyyMM"); // 项目 Common 的 CommonService 类
            supplyID += GetTopSupplyID(supplyID);
            return supplyID;
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
                //DataRow dr = dt.NewRow();
                //dr["Name"] = "请选择";
                //dt.Rows.InsertAt(dr,0);
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
        /// “级别”下拉列表的数据获取/更新
        /// </summary>
        private void BindCbxLevel()
        {
            DataSet ds = new Level().GetList("");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //DataRow dr = dt.NewRow();
                //dr["Name"] = "请选择";
                //dt.Rows.InsertAt(dr,0);
                cbx_Level.DataSource = dt;
                cbx_Level.DisplayMember = "LevelName";
                cbx_Level.ValueMember = "LevelName";
            }
            else
            {
                MessageBox.Show("请先添加级别资料！");

                return;
            }
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        public void BindDGV()
        {
            decimal sumMoney = 0;
            foreach (InW w in allOut)
            {
                w.Price = _price;
                w.SumPrice = decimal.Parse(w.NormName) * decimal.Parse(w.Length.ToString()) * _price;
                sumMoney += w.SumPrice;
            }
            lab_Cnt.Text = allOut.Count.ToString();
            lab_Sum.Text = sumMoney.ToString("0.00") + "元";

            dataGridView1.DataSource = null;
            if (allOut.Count > 0)
            {
                dataGridView1.DataSource = allOut;
            }
        }

        /// <summary>
        /// 当添加条码时更新 dataGridView 的数据
        /// </summary>
        /// <param name="barcode"></param>
        /// <remarks>被 frmStartScan 调用，本窗体中没有调用；大概是选择“即时扫描”时弹出 frmStartScan 窗体来扫描的，而现在变成了“即时输入”</remarks>
        public void AddDGV(string barcode)
        {
            InW m = new InW().GetModelByBarcode(barcode);
            if (m != null)
            {
                allOut.Add(m);
                BindDGV();
            }
        }
    }
}
