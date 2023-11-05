using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PipingISO
{
    public partial class Form1 : Form
    {
        private DataTable table_3D;
        private DataTable table_2_5F;
        private DataTable table_2_5R;
        private DataTable table_scale;
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
        }

        private void initTable()
        {
            TableHandler tableHandler = new TableHandler();
            table_3D = new DataTable();
            table_2_5F = new DataTable();
            table_2_5R = new DataTable();
            table_scale = new DataTable();

            tableHandler.init3D_Table(table_3D, dataGridView1);
            tableHandler.init2_5F_Table(table_2_5F, table_3D);
            tableHandler.initScale_Table(table_scale, dataGridView1);
            tableHandler.init2_5R_Table(table_2_5R, table_2_5F, dataGridView1, table_scale);
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
        }

        private void Draw_Button_Click(object sender, EventArgs e)
        {
            DrawExcel drawExcel = new DrawExcel(table_2_5F, table_2_5R);
            drawExcel.draw();
        }
  
        private void Daw_WinForm_Button_Click(object sender, EventArgs e)
        {
            DrawWinForm drawWinForm = new DrawWinForm();
            drawWinForm.draw(table_2_5R);
        }
    }
}