namespace Warehouse
{
    partial class frmUserUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbx_Pwd = new System.Windows.Forms.CheckBox();
            this.btn_Modity = new System.Windows.Forms.Button();
            this.lab_ID = new System.Windows.Forms.Label();
            this.cbx_Position = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbx_Pwd
            // 
            this.cbx_Pwd.AutoSize = true;
            this.cbx_Pwd.Location = new System.Drawing.Point(155, 175);
            this.cbx_Pwd.Name = "cbx_Pwd";
            this.cbx_Pwd.Size = new System.Drawing.Size(108, 16);
            this.cbx_Pwd.TabIndex = 30;
            this.cbx_Pwd.Text = "恢复为默认密码";
            this.cbx_Pwd.UseVisualStyleBackColor = true;
            // 
            // btn_Modity
            // 
            this.btn_Modity.Location = new System.Drawing.Point(176, 244);
            this.btn_Modity.Name = "btn_Modity";
            this.btn_Modity.Size = new System.Drawing.Size(75, 23);
            this.btn_Modity.TabIndex = 29;
            this.btn_Modity.Text = "确认修改";
            this.btn_Modity.UseVisualStyleBackColor = true;
            this.btn_Modity.Click += new System.EventHandler(this.btn_Modity_Click);
            // 
            // lab_ID
            // 
            this.lab_ID.AutoSize = true;
            this.lab_ID.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_ID.Location = new System.Drawing.Point(177, 67);
            this.lab_ID.Name = "lab_ID";
            this.lab_ID.Size = new System.Drawing.Size(20, 20);
            this.lab_ID.TabIndex = 28;
            this.lab_ID.Text = "1";
            // 
            // cbx_Position
            // 
            this.cbx_Position.FormattingEnabled = true;
            this.cbx_Position.Items.AddRange(new object[] {
            "普通用户",
            "管理员"});
            this.cbx_Position.Location = new System.Drawing.Point(176, 120);
            this.cbx_Position.Name = "cbx_Position";
            this.cbx_Position.Size = new System.Drawing.Size(107, 20);
            this.cbx_Position.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "职位：";
            // 
            // frmUserUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 334);
            this.Controls.Add(this.cbx_Pwd);
            this.Controls.Add(this.btn_Modity);
            this.Controls.Add(this.lab_ID);
            this.Controls.Add(this.cbx_Position);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "frmUserUpdate";
            this.Text = "修改";
            this.Load += new System.EventHandler(this.frmUserUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbx_Pwd;
        private System.Windows.Forms.Button btn_Modity;
        private System.Windows.Forms.Label lab_ID;
        private System.Windows.Forms.ComboBox cbx_Position;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}