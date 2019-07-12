﻿namespace Warehouse
{
    partial class frmStatistics
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.btn_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNowTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOutMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNorm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cDel = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dtp_End);
            this.groupBox1.Controls.Add(this.dtp_Start);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 78);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出仓查询";
            // 
            // dtp_End
            // 
            this.dtp_End.Location = new System.Drawing.Point(371, 34);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(129, 21);
            this.dtp_End.TabIndex = 17;
            // 
            // dtp_Start
            // 
            this.dtp_Start.Location = new System.Drawing.Point(139, 33);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(129, 21);
            this.dtp_Start.TabIndex = 16;
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(586, 32);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(59, 23);
            this.btn_Search.TabIndex = 0;
            this.btn_Search.Text = "查 询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始日期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束日期：";
            // 
            // cIn
            // 
            this.cIn.DataPropertyName = "InCnt";
            this.cIn.HeaderText = "入仓数量";
            this.cIn.Name = "cIn";
            this.cIn.ReadOnly = true;
            this.cIn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cOut
            // 
            this.cOut.DataPropertyName = "OutCnt";
            this.cOut.HeaderText = "出仓数量";
            this.cOut.Name = "cOut";
            this.cOut.ReadOnly = true;
            this.cOut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cCnt
            // 
            this.cCnt.DataPropertyName = "StoreCnt";
            this.cCnt.HeaderText = "当前库存";
            this.cCnt.Name = "cCnt";
            this.cCnt.ReadOnly = true;
            this.cCnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cCnt.Visible = false;
            this.cCnt.Width = 200;
            // 
            // cNowTime
            // 
            this.cNowTime.DataPropertyName = "NowTime";
            this.cNowTime.HeaderText = "截止时间";
            this.cNowTime.Name = "cNowTime";
            this.cNowTime.ReadOnly = true;
            this.cNowTime.Visible = false;
            this.cNowTime.Width = 150;
            // 
            // cOutMoney
            // 
            this.cOutMoney.DataPropertyName = "OutMoney";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.cOutMoney.DefaultCellStyle = dataGridViewCellStyle1;
            this.cOutMoney.HeaderText = "出仓金额";
            this.cOutMoney.Name = "cOutMoney";
            this.cOutMoney.ReadOnly = true;
            this.cOutMoney.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cNorm
            // 
            this.cNorm.DataPropertyName = "NormName";
            this.cNorm.HeaderText = "规格(单位:米)";
            this.cNorm.Name = "cNorm";
            this.cNorm.ReadOnly = true;
            this.cNorm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cNorm.Width = 200;
            // 
            // cModel
            // 
            this.cModel.DataPropertyName = "Model";
            this.cModel.HeaderText = "型号";
            this.cModel.Name = "cModel";
            this.cModel.ReadOnly = true;
            this.cModel.Width = 150;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cModel,
            this.cNorm,
            this.cIn,
            this.cOut,
            this.cCnt,
            this.cNowTime,
            this.cOutMoney,
            this.cDel});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(8, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(885, 500);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // cDel
            // 
            this.cDel.ActiveLinkColor = System.Drawing.Color.Blue;
            this.cDel.HeaderText = "";
            this.cDel.Name = "cDel";
            this.cDel.ReadOnly = true;
            this.cDel.Visible = false;
            this.cDel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.cDel.Width = 60;
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 602);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmStatistics";
            this.Text = "frmStatistics";
            this.Load += new System.EventHandler(this.frmStatistics_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNowTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOutMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNorm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cModel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewLinkColumn cDel;
    }
}