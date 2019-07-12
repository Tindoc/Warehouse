using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Common;

namespace Warehouse
{
    public partial class frmLogin : Form
    {
        public static Sunisoft.IrisSkin.SkinEngine se = null;   // 引用动态链接库 IrisSkin2，需要在项目的“引用”中添加

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // 设置皮肤
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinFile = Application.StartupPath + @"\Skin\MP10.ssk";

            // 读取上次登录名
            INIService ini = new INIService();  // 项目 Common 的 INIService 类（作用：用于初始化内容，例如读取上次的登录名）
            string _userName = ini.IniReadValue("LoginUser", "UserName", Application.StartupPath + "\\LastLogin.ini");
            
            txt_UserName.Text = _userName;
            txt_UserName.Focus();   // 光标聚焦在用户名输入处

            #region 测试用，跳过登录窗体
            //Global.userName = "admin";
            //Global.IsAdmin = true;
            //frmIndex f = new frmIndex();
            //this.Hide();
            //f.ShowDialog();
            #endregion
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                // 客户端使用控制（当服务器时间大于 2015-6-1 时停止使用程序）
                //DateTime serverTime = CommonService.GetServerTime();
                //if (serverTime >= DateTime.Parse("2015-6-1"))
                //{
                //    MessageBox.Show("系统异常，请联系开发人员!");
                //    Application.Exit();
                //    return;
                //}

                string _name = txt_UserName.Text.Trim();    // Trim() 去除前、后的空白字符
                string _pwd = txt_UserPwd.Text.Trim();

                User user = new User(); // 本项目的 Service 文件夹内的 User 类（属于 Model 层）
                if (user.Login(_name, _pwd))
                {
                    user.GetModel(_name);
                    if (user.Position == "管理员" || user.Position == "系统管理员")
                    {
                        Global.IsAdmin = true;  // 项目 Common 的 Global 类（作用：使用 Global 类内数据成员来保存本次程序的不变量）
                    }
                    Global.userName = _name;

                    // 写入本次登录名
                    INIService ini = new INIService();  // 项目 Common 的 INIService 类
                    ini.IniWriteValue("LoginUser", "UserName", _name, Application.StartupPath + "\\LastLogin.ini");

                    // 跳转 frmIndex 窗体和隐藏本窗体
                    frmIndex f = new frmIndex();
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("登录失败，用户名或者密码错误!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MyLog.WriteLog(ex.Message); // 项目 Common 的 MyLog 类（作用：保存日志）
                MessageBox.Show("登录失败，请检查服务器是否正常运行！");
            }
        }

        private void txt_UserPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Login_Click(null, null);
        }
    }
}
