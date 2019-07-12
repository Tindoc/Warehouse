﻿namespace Warehouse
{
    partial class frmInDetail
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
            this.button1 = new System.Windows.Forms.Button();
            this.cbx_All = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cSel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrintCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_Batch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 539);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "打印选中条码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbx_All
            // 
            this.cbx_All.AutoSize = true;
            this.cbx_All.Location = new System.Drawing.Point(72, 68);
            this.cbx_All.Name = "cbx_All";
            this.cbx_All.Size = new System.Drawing.Size(48, 16);
            this.cbx_All.TabIndex = 13;
            this.cbx_All.Text = "全选";
            this.cbx_All.UseVisualStyleBackColor = true;
            this.cbx_All.CheckedChanged += new System.EventHandler(this.cbx_All_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cSel,
            this.cBarcode,
            this.cPrintCnt});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(2, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(606, 451);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // cSel
            // 
            this.cSel.Frozen = true;
            this.cSel.HeaderText = "";
            this.cSel.Name = "cSel";
            // 
            // cBarcode
            // 
            this.cBarcode.DataPropertyName = "Barcode";
            this.cBarcode.Frozen = true;
            this.cBarcode.HeaderText = "条形码";
            this.cBarcode.Name = "cBarcode";
            this.cBarcode.ReadOnly = true;
            this.cBarcode.Width = 300;
            // 
            // cPrintCnt
            // 
            this.cPrintCnt.DataPropertyName = "PrintCnt";
            this.cPrintCnt.Frozen = true;
            this.cPrintCnt.HeaderText = "已打印次数";
            this.cPrintCnt.Name = "cPrintCnt";
            this.cPrintCnt.ReadOnly = true;
            // 
            // lab_Batch
            // 
            this.lab_Batch.AutoSize = true;
            this.lab_Batch.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Batch.Location = new System.Drawing.Point(173, 21);
            this.lab_Batch.Name = "lab_Batch";
            this.lab_Batch.Size = new System.Drawing.Size(246, 20);
            this.lab_Batch.TabIndex = 11;
            this.lab_Batch.Text = "入仓批号：20141114XXXX";
            // 
            // frmInDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 603);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbx_All);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lab_Batch);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "frmInDetail";
            this.Text = "条形码打印";
            this.Load += new System.EventHandler(this.frmInDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbx_All;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lab_Batch;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cSel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrintCnt;
    }
}