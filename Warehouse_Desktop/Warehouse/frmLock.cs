using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Common;

namespace Warehouse
{
    public partial class frmLock : Form
    {
        /// <summary>
        /// 判断程序退出还是窗体跳转
        /// </summary>
        /// <remarks>
        /// 任何关闭本窗体的行为（包括跳转到其他窗体时的关闭）会执行 frmLock_FormClosing() 事件导致程序结束，
        /// 用来在跳转时加以判断避免程序结束
        /// </remarks>
        private bool _NeedExit = true;  // 

        public frmLock()
        {
            // 其中用户名的 textBox 设置为 ReadOnly，防止锁定之后的修改用户名再登陆
            InitializeComponent();
        }

        private void frmLock_Load(object sender, EventArgs e)
        {
            txt_UserName.Text = Global.userName;
        }

        private void frmLock_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _NeedExit)
            {
                Application.Exit();
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string _name = txt_UserName.Text.Trim();
            string _pwd = txt_UserPwd.Text.Trim();

            User user = new User();
            if (user.Login(_name, _pwd))
            {
                Owner.Show();   // 需要在跳转到本窗体的窗体设置 "fm.Owner() = this;"，作用：回到跳转到本窗体的窗体
                _NeedExit = false;
                this.Close();   // 这里使用的是 this.Close(); 而不是 this.Hide();
            }
            else
            {
                MessageBox.Show("登录失败，用户名或者密码错误!");
                return;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
