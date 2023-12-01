using System.Data;

namespace PipingISO
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rngConn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.연번 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Load_Button = new System.Windows.Forms.Button();
            this.Clear_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Draw_Button = new System.Windows.Forms.Button();
            this.Draw_WinForm_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rngConn,
            this.point_x,
            this.point_y,
            this.point_z,
            this.연번,
            this.scale});
            this.dataGridView1.Location = new System.Drawing.Point(92, 118);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1303, 318);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // rngConn
            // 
            this.rngConn.HeaderText = "rngConn";
            this.rngConn.MinimumWidth = 8;
            this.rngConn.Name = "rngConn";
            this.rngConn.Width = 150;
            // 
            // point_x
            // 
            this.point_x.HeaderText = "x[m]";
            this.point_x.MinimumWidth = 8;
            this.point_x.Name = "point_x";
            this.point_x.Width = 150;
            // 
            // point_y
            // 
            this.point_y.HeaderText = "y[m]";
            this.point_y.MinimumWidth = 8;
            this.point_y.Name = "point_y";
            this.point_y.Width = 150;
            // 
            // point_z
            // 
            this.point_z.HeaderText = "z[m]";
            this.point_z.MinimumWidth = 8;
            this.point_z.Name = "point_z";
            this.point_z.Width = 150;
            // 
            // 연번
            // 
            this.연번.HeaderText = "Num";
            this.연번.MinimumWidth = 8;
            this.연번.Name = "연번";
            this.연번.Width = 150;
            // 
            // scale
            // 
            this.scale.HeaderText = "scale";
            this.scale.MinimumWidth = 8;
            this.scale.Name = "scale";
            this.scale.Width = 150;
            // 
            // Load_Button
            // 
            this.Load_Button.Location = new System.Drawing.Point(92, 62);
            this.Load_Button.Name = "Load_Button";
            this.Load_Button.Size = new System.Drawing.Size(120, 50);
            this.Load_Button.TabIndex = 1;
            this.Load_Button.Text = "Load";
            this.Load_Button.UseVisualStyleBackColor = true;
            this.Load_Button.Click += new System.EventHandler(this.Load_Button_Click);
            // 
            // Clear_Button
            // 
            this.Clear_Button.Location = new System.Drawing.Point(218, 62);
            this.Clear_Button.Name = "Clear_Button";
            this.Clear_Button.Size = new System.Drawing.Size(120, 50);
            this.Clear_Button.TabIndex = 2;
            this.Clear_Button.Text = "Clear";
            this.Clear_Button.UseVisualStyleBackColor = true;
            this.Clear_Button.Click += new System.EventHandler(this.Clear_Button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(92, 442);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1303, 379);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 59);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(1303, 320);
            this.dataGridView2.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "3D_Data",
            "3D_Scale_Data",
            "2_5F_Data",
            "2_5R_Data",
            "Scale_Data",
            "table_notScaled_2_5R"
            });
            this.comboBox1.Location = new System.Drawing.Point(6, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 26);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Draw_Button
            // 
            this.Draw_Button.Location = new System.Drawing.Point(344, 62);
            this.Draw_Button.Name = "Draw_Button";
            this.Draw_Button.Size = new System.Drawing.Size(127, 50);
            this.Draw_Button.TabIndex = 6;
            this.Draw_Button.Text = "Draw_EXCEL";
            this.Draw_Button.UseVisualStyleBackColor = true;
            this.Draw_Button.Click += new System.EventHandler(this.Draw_Button_Click);
            // 
            // Draw_WinForm_Button
            // 
            this.Draw_WinForm_Button.Location = new System.Drawing.Point(477, 62);
            this.Draw_WinForm_Button.Name = "Draw_WinForm_Button";
            this.Draw_WinForm_Button.Size = new System.Drawing.Size(140, 50);
            this.Draw_WinForm_Button.TabIndex = 7;
            this.Draw_WinForm_Button.Text = "Draw_WinForm";
            this.Draw_WinForm_Button.UseVisualStyleBackColor = true;
            this.Draw_WinForm_Button.Click += new System.EventHandler(this.Daw_WinForm_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1509, 833);
            this.Controls.Add(this.Draw_WinForm_Button);
            this.Controls.Add(this.Draw_Button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Clear_Button);
            this.Controls.Add(this.Load_Button);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Load_Button;
        private System.Windows.Forms.Button Clear_Button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button Draw_Button;
        private System.Windows.Forms.DataGridViewTextBoxColumn rngConn;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_x;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_y;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_z;
        private System.Windows.Forms.DataGridViewTextBoxColumn 연번;
        private System.Windows.Forms.DataGridViewTextBoxColumn scale;
        private System.Windows.Forms.Button Draw_WinForm_Button;
    }
}

