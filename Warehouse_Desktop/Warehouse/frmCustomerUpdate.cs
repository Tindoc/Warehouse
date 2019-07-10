using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class frmCustomerUpdate : Form
    {
        public bool _isOK = false;
        string _agentName = "";

        public frmCustomerUpdate()
        {
            InitializeComponent();
        }
        public frmCustomerUpdate(string name)   // 带参数的构造函数
        {
            InitializeComponent();
            this._agentName = name;
        }

        private void frmCustomerUpdate_Load(object sender, EventArgs e)
        {
            BindLevel();    // 自定义函数
            Agent a = new Agent();  // 本项目 Service 文件夹内的 Agent 类（属于 Model 类）
            a.GetModel(_agentName);
            txt_Name.Text = a.Name;
            txt_Phone.Text = a.Phone;
            cbx_Level.Text = a.LevelName;
            txt_Address.Text = a.Address;
            txt_Contact.Text = a.Contact;
            txt_Fox.Text = a.Fox;
            txt_Tel.Text = a.Tel;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Mod_Click(object sender, EventArgs e)
        {
            Agent a = new Agent();
            a.Name = txt_Name.Text;
            a.Phone = txt_Phone.Text;
            a.LevelName = cbx_Level.Text;
            a.Address = txt_Address.Text;
            a.Contact = txt_Contact.Text;
            a.Fox = txt_Fox.Text;
            a.Tel = txt_Tel.Text;
            bool re = a.Update();
            if (re)
            {
                MessageBox.Show("修改成功!");
                _isOK = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("修改失败!");
            }
        }

        /// <summary>
        /// “代理商级别”下拉列表数据的获取/更新
        /// </summary>
        private void BindLevel()
        {
            DataSet ds = new Level().GetList("");
            cbx_Level.DataSource = ds.Tables[0];
            cbx_Level.ValueMember = "LevelName";
            cbx_Level.DisplayMember = "LevelName";
        }
    }
}