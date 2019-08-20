using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Common;
using SqlServerDAL;

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

            // 测试用
            //this.txt_AutoCode.Text = "自动生成";//DateTime.Now.ToString("yyyyMMdd" + "XXXX");
            //DataTable dt = GeneralDataTable();
            //dataGridView1.DataSource = dt;

            txt_Length.Text = "100";    // “米数”label 后的 textBox
            BindCbx();  // 自定义函数
            BindNorm(); // 自定义函数
            txt_Operator.Text = Global.userName;    // “操作员”label 后的 textBox
            dtp_InTime.Value = CommonService.GetServerTime();   // dtp_time 为日期时间选择器控件
            
            if (!Global.IsAdmin)
            {
                //dataGridView1.Columns["cModity"].Visible = false;
                dataGridView1.Columns["cDel"].Visible = false;  // 非“admin”用户不能删除
                dtp_InTime.Enabled = false; // 非“admin”用户不能选择入仓时间，只能为当天
            }
            else
            {
                dtp_InTime.Enabled = true;  // “admin”用户可选择入仓时间
            }

            // pagerControl1 不是默认控件，需要引用 WinFormPager.dll 在工具箱鼠标右键“选择项-浏览-找到DLL”，再拖放到窗体即可
            pagerControl1.OnPageChanged += new EventHandler(pagerControl1_OnPageChanged);   // 为控件添加事件处理函数
            BindDGV();  // 自定义函数
        }

        /// <summary>
        /// 控件 pagerControl1 的 OnPageChanged 事件的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>不是在窗体设计界面中添加本函数，使用代码添加，见 frmGoodsIn_Load() 函数</remarks>
        void pagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            BindDGV();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string modelStr = txt_Model.Text.Trim();
            string macStr = cbx_Machine.Text;
            string bigStr = cbx_Big.Text;
            string cntStr = cbx_Cnt.Text;
            string lenStr = txt_Length.Text.Trim();
            if (string.IsNullOrEmpty(cntStr) || string.IsNullOrEmpty(bigStr) || string.IsNullOrEmpty(macStr))
            {
                MessageBox.Show("请输入机器或大卷或件数!");
                cbx_Cnt.Focus();
                return;
            }
            if (string.IsNullOrEmpty(modelStr))
            {
                MessageBox.Show("请输入成品型号!");
                txt_Model.Focus();
                return;
            }
            if (!ValidateService.IsNumber(lenStr))
            {
                MessageBox.Show("米数不正确!");
                txt_Length.Focus();
                return;
            }
            try
            {
                InW m = new InW();
                m.Model = modelStr;
                m.NormName = cbx_Norm.SelectedValue.ToString();
                m.Machine = int.Parse(macStr);
                m.BigCnt = int.Parse(bigStr);
                m.Cnt = int.Parse(cntStr);
                m.Length = int.Parse(lenStr);
                m.Batch = GenBatchNO(); // 自定义函数
                List<string> barList = GenBarcode(m.Machine, m.BigCnt, m.Cnt);  // 自定义函数
                m.Barcode = barList[0] + "~" + barList[barList.Count - 1];
                m.Operator = Global.userName;
                m.InTime = dtp_InTime.Value;

                int re = m.Add(barList);
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
            catch (Exception ex)
            {
                if (ex.Message.Contains("99"))
                {
                    MessageBox.Show("每天同一规格成品入仓数量不能大于99件!");
                    //txt_Cnt.Focus();
                }
                else
                {
                    MessageBox.Show("系统异常! 详细:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 单击单元格的内容时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>主要用来实现“打印条形码”或“删除”列的功能，即点击时能实现功能</remarks>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["cPrint"].Index) // cPrint 为 dateGridView1 中显示“打印条形码”的列，这句的作用是判断是否点击了某行的该列
            {
                object v = dataGridView1.Rows[e.RowIndex].Cells["cBatch"].Value;
                if (v != null)
                {
                    frmInDetail f = new frmInDetail(v.ToString());  // 本项目的 frmInDetail 窗体
                    f.ShowDialog();
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)  // 判断是否点击了某行的“删除”列
            {
                if (MessageBox.Show(this, "删除后数据不能恢复，是否继续删除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    object v = dataGridView1.Rows[e.RowIndex].Cells["cBatch"].Value;
                    if (v != null)
                    {
                        InW no = new InW();
                        if (no.IsRelation(v.ToString()))
                        {
                            MessageBox.Show("该批条码已被出仓，禁止删除！");
                            return;
                        }
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
        /// 需要设置单元格内容的显示格式时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>为数据行中没有内容的列添加内容，即在“删除”列显示“删除”</remarks>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 因为“打印条形码”和“删除”列没有数据库里面的内容对应，所以需要在生成 dataGridView1 时生成这些列的显示内容
            if (e.ColumnIndex == dataGridView1.Columns["cPrint"].Index)
            {
                e.Value = "打印条形码";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["cDel"].Index)
            {
                e.Value = " 删除";
            }
            //else if (e.ColumnIndex == dataGridView1.Columns["cModity"].Index)
            //{
            //    e.Value = " 修改";
            //}
        }

        /// <summary>
        /// 在发生所有单元格绘制之后，执行行级绘制时发生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewService.VisibleRowOrder(dataGridView1, e);  // 项目 Common 的 DataGridService 类，作用：设置单元格样式
        }

        /// <summary>
        /// “规格”下拉列表的数据获取/更新
        /// </summary>
        private void BindNorm()
        {
            Norm n = new Norm();
            DataSet ds = n.GetList("");
            if (ds != null)
            {
                // 设置下列列表 cbx_Norm
                cbx_Norm.DataSource = ds.Tables[0];
                cbx_Norm.DisplayMember = "NormName";    // 显示的值和实际的值可以是不同
                cbx_Norm.ValueMember = "NormName";
            }
            else
            {
                MessageBox.Show("请先设置产品规格!");
                btn_Add.Enabled = false;
            }
        }

        /// <summary>
        /// “件数”下拉列表的数据获取/更新
        /// </summary>
        private void BindCbx()
        {
            for (int i = 0; i < 100; i++)
            {
                //cbx_Big.Items.Add(i);
                cbx_Cnt.Items.Add(i);
            }
        }

        /// <summary>
        /// dataGridView1 的数据获取/更新
        /// </summary>
        private void BindDGV()
        {
            int _count = 0;
            DataSet ds = new InW().GetPageList(pagerControl1.PageSize, pagerControl1.PageIndex, "", out _count); // 本项目的 Service 文件夹内的 InW 类（属于 Model 层）
            
            dataGridView1.DataSource = ds.Tables[0];
            pagerControl1.DrawControl(_count);
        }

        /// <summary>
        /// 设置“规格”的格式，用于生成流水位，最终生成批号、条码等
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetNormFormat(string name)
        {
            string re = (float.Parse(name) * 1000).ToString("0000.00");
            return re.Substring(0, 4);  // 变成了“xxxx.xx”的字符串但最终只取前4位
        }

        /// <summary>
        /// 生成流水位十位
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 生成规则：
        /// 规格(扩增为4位)+日期(yyMMdd,6位)
        /// </remarks>
        private string GetTodayNO(bool isBarcode)
        {
            DateTime date = dtp_InTime.Enabled ? dtp_InTime.Value : CommonService.GetServerTime();  // “admin”账户可以任意设置时间，其他账户只能使用服务器时间（非 “admin” 用户对选择时间控件不可操作，frmGoodsIn_Load()中设置的）
            string batchNO = "";
            batchNO += GetNormFormat(cbx_Norm.SelectedValue.ToString());    //4位
            batchNO += date.ToString("yyMMdd"); // 6位
            return batchNO;
        }

        /// <summary>
        /// 获取数据库中同一批货物批号的最大批号的后三位
        /// </summary>
        /// <param name="front"></param>
        /// <returns></returns>
        /// <remarks>
        /// 同一批货物的判断：根据批号前11位判断
        /// “P(1)+规格(4)+时间(6)”(共11位)相同则批号后三位累增，如果不同则批号后三位从001开始
        /// </remarks>
        private string GetTopBatch(string front)
        {
            string sql = "SELECT TOP 1 RIGHT(Batch,3) FROM InW WHERE LEFT(Batch,11)='"+front+"' ORDER BY RIGHT(Batch,3) DESC";
            object obj = DbHelperSQL.GetSingle(sql);    // 项目 SqlServer 的 DbHelperSQL 类，需要 using SqlServerDAL;
            if (obj != DBNull.Value && obj != null)
            {
                return (Convert.ToInt32(obj) + 1).ToString("000");
            }
            else
            {
                return "001";
            }
        }

        /// <summary>
        /// 获取数据库中同一批货物条码的最大条码的后两位
        /// </summary>
        /// <param name="front"></param>
        /// <returns></returns>
        /// <remarks>
        /// 同一批货物的判断：根据批号前12位判断
        /// “规格(4)+时间(6)+机器(1)+大卷(1)”(共12位)相同则条码后两位累增，如果不同则条码后两位从01开始
        /// </remarks>
        private string GetTopBarcode(string front)
        {
            string sql = "SELECT TOP 1 RIGHT(Barcode,2) FROM InWDetail WHERE LEFT(Barcode,12)='" + front + "' ORDER BY RIGHT(Barcode,2) DESC";
            object obj = DbHelperSQL.GetSingle(sql);
            if (obj != DBNull.Value && obj != null)
            {
                return (Convert.ToInt32(obj) + 1).ToString("00");
            }
            else
            {
                return "01";
            }
        }

        /// <summary>
        /// 生成入仓批号(11位)
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 生成规则：P(1位) + 流水位(10位)
        /// </remarks>
        private string GenBatchNO()
        {
            string front = "P" + GetTodayNO(false);
            front += GetTopBatch(front);
            return front;
        }

        /// <summary>
        /// 生成货物条码(14位)
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 生成规则：规格(4位)+年月日(6位)+机器(1位)+大卷(1位)+件数(2位，所以同一批最多只能添加99件)
        /// </remarks>
        private List<string> GenBarcode(int machine,int bigCnt,int cnt)
        {
            string _today = GetTodayNO(true);
            string _front = _today + machine.ToString() + bigCnt.ToString();

            int _base = int.Parse(GetTopBarcode(_front));
            if ((_base + cnt)>99)
            {
                throw new Exception("不能大于99");
            }
            List<string> list = new List<string>();
            for (int i = 0; i < cnt;i++ )
            {
                string s = _front + (_base + i).ToString("00");
                list.Add(s);
            }
            return list;
        }

        /// <summary>
        /// 测试用数据
        /// </summary>
        /// <returns></returns>
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

            tblDatas.Rows.Add(new object[] { null, "20141114AAAA", "8G", "1000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114BBBB", "64G", "2000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114CCCC", "16G", "1000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114DDDD", "64G", "5000", "打印条码" });
            tblDatas.Rows.Add(new object[] { null, "20141114EEEE", "8G", "1000", "打印条码" });

            return tblDatas;
        }
    }
}