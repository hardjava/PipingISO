using System;
using System.Data;
using System.Windows.Forms;

namespace PipingISO
{
    public partial class Form1 : Form
    {
        private DataTable table_3D; // 3D 데이터 저장
        private DataTable table_3D_scale; // scale 후의 3D 데이터 저장
        private DataTable table_2_5F; // ISO_2.5F 그리는 데이터 저장
        private DataTable table_2_5R; // ISO_2.5R 그리는 데이터 저장
        private DataTable table_scale; // scale 값이 저장되어 있는 데이터 저장
        private DataTable newTable_2_5R;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Clear_Button_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Load_Button_Click(object sender, EventArgs e)
        {
            clear();

            Load load = new Load();
            if (load.read(dataGridView1))
            {
                initTable();
            }
        }

        private void clear()
        {
            dataGridView1.Rows.Clear();
            table_3D = null;
            table_2_5F = null;
            table_2_5R = null;
            table_scale = null;
            table_3D_scale = null;
        }

        private void initTable()
        {
            TableHandler tableHandler = new TableHandler();
            table_3D = new DataTable();
            table_3D_scale = new DataTable();
            table_2_5F = new DataTable();
            table_2_5R = new DataTable();
            table_scale = new DataTable();
            newTable_2_5R = new DataTable();

            tableHandler.init3D_Table(table_3D, dataGridView1);
            tableHandler.initScale_Table(table_scale, dataGridView1);
            tableHandler.init2_5F_Table(table_2_5F, table_3D);
            tableHandler.init2_5R_Table(table_2_5R, table_2_5F, dataGridView1, table_scale);
            tableHandler.init3D_scale_Table(table_3D_scale, table_3D, dataGridView1, table_scale);
            tableHandler.removeBackLine(newTable_2_5R, table_3D_scale, table_2_5R);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView2.DataSource = null;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                dataGridView2.DataSource = table_3D;
                dataGridView2.AllowUserToAddRows = false;
            }

            if (comboBox1.SelectedIndex == 2)
            {
                dataGridView2.DataSource = table_2_5F;
                dataGridView2.AllowUserToAddRows = false;
            }

            if (comboBox1.SelectedIndex == 3)
            {
                dataGridView2.DataSource = table_scale;
                dataGridView2.AllowUserToAddRows = false;
            }

            if (comboBox1.SelectedIndex == 4)
            {
                dataGridView2.DataSource = table_2_5R;
                dataGridView2.AllowUserToAddRows = false;
            }

            if (comboBox1.SelectedIndex == 5)
            {
                dataGridView2.DataSource = table_3D_scale;
                dataGridView2.AllowUserToAddRows = false;
            }

            if (comboBox1.SelectedIndex == 6)
            {
                dataGridView2.DataSource = newTable_2_5R;
                dataGridView2.AllowUserToAddRows = false;
            }
            
        }

        private void Draw_Button_Click(object sender, EventArgs e)
        {
            DrawExcel drawExcel = new DrawExcel(table_2_5F, table_2_5R);
            drawExcel.draw();
        }
  
        private void Daw_WinForm_Button_Click(object sender, EventArgs e)
        {
            DrawWinForm drawWinForm = new DrawWinForm(table_2_5R);
            drawWinForm.draw();
        }
    }
}