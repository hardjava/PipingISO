using System;
using System.Data;
using System.Windows.Forms;

namespace PipingISO
{
    public class TableHandler
    {
        public void init3D_Table(DataTable table, DataGridView dataGridView)
        {
            table.Columns.Add("x", typeof(double));
            table.Columns.Add("y", typeof(double));
            table.Columns.Add("z", typeof(double));

            double lastX = 0, lastY = 0, lastZ = 0;
            bool isBreakPoint = false;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                double x = 0, y = 0, z = 0;
                if (!isBreakPoint)
                {
                    if (!Double.TryParse(dataGridView.Rows[i].Cells[1].Value.ToString(), out x))
                    {
                        if (!Double.TryParse(dataGridView.Rows[i].Cells[2].Value.ToString(), out y))
                        {
                            if (!Double.TryParse(dataGridView.Rows[i].Cells[3].Value.ToString(), out z))
                            {
                                isBreakPoint = true;
                            }
                        }
                    }
                }
                else
                {
                    int.TryParse(dataGridView.Rows[i].Cells[0].Value.ToString(), out int idx);
                    Double.TryParse(table.Rows[idx - 1][0].ToString(), out x);
                    Double.TryParse(table.Rows[idx - 1][1].ToString(), out y);
                    Double.TryParse(table.Rows[idx - 1][2].ToString(), out z);
                    isBreakPoint = false;
                }

                if (!isBreakPoint)
                {
                    x += lastX;
                    y += lastY;
                    z += lastZ;
                    table.Rows.Add(x, y, z);
                }
                else
                {
                    table.Rows.Add();
                }
                lastX = x; lastY = y; lastZ = z;
            }
        }

        public void init2_5F_Table(DataTable table, DataTable table_3D) 
        {
            table.Columns.Add("x", typeof(double));
            table.Columns.Add("y", typeof(double));

            double x = 0, y = 0, z = 0;
            for (int i = 0; i < table_3D.Rows.Count; i++)
            {

                if (!Double.TryParse(table_3D.Rows[i][0].ToString(), out x))
                {
                    table.Rows.Add();

                }
                else
                {
                    Double.TryParse(table_3D.Rows[i][1].ToString(), out y);
                    Double.TryParse(table_3D.Rows[i][2].ToString(), out z);
                    table.Rows.Add(transfer3D_To_2_5F_X(x, y, z), transfer3D_To_2_5F_Y(x, y, z));
                }
            }
        }

        private double transfer3D_To_2_5F_X(double x, double y, double z)
        {
            double newX = 0.866025404 * x + (-0.866025404) * y + 0 * z;
            return newX;
        }

        private double transfer3D_To_2_5F_Y(double x, double y, double z)
        {
            double newY = 0.5 * x + 0.5 * y + 1 * z;
            return newY;
        }

        public void init2_5R_Table(DataTable table, DataTable table_2_5F, DataGridView dataGridView, DataTable table_scale) 
        {
            table.Columns.Add("x", typeof(double));
            table.Columns.Add("y", typeof(double));

            double x = 0, y = 0;
            for (int i = 0; i < table_2_5F.Rows.Count; i++)
            {
                if (Double.TryParse(table_2_5F.Rows[i][0].ToString(), out x))
                {
                    Double.TryParse(table_2_5F.Rows[i][1].ToString(), out y);

                    if (Double.TryParse(table_scale.Rows[i][0].ToString(), out double scale))
                    {
                        Double.TryParse(table_2_5F.Rows[i - 1][0].ToString(), out double last2D_X);
                        Double.TryParse(table_2_5F.Rows[i - 1][1].ToString(), out double last2D_Y);
                        Double.TryParse(table.Rows[i - 1][0].ToString(), out double last2_5R_X);
                        Double.TryParse(table.Rows[i - 1][1].ToString(), out double last2_5R_Y);

                        double changed_X = last2_5R_X - last2D_X;
                        double changed_Y = last2_5R_Y - last2D_Y;

                        x += changed_X;
                        y += changed_Y;

                        double dx = x - last2_5R_X;
                        double dy = y - last2_5R_Y;
                        dx *= scale;
                        dy *= scale;

                        table.Rows.Add(last2_5R_X + dx, last2_5R_Y + dy);
                    }
                    else
                    {
                        table.Rows.Add(x, y);
                    }

                }
                else
                {
                    table.Rows.Add();
                    if (int.TryParse(dataGridView.Rows[i + 1].Cells[0].Value.ToString(), out int rng))
                    {
                        Double.TryParse(table.Rows[rng - 1][0].ToString(), out x);
                        Double.TryParse(table.Rows[rng - 1][1].ToString(), out y);
                        table.Rows.Add(x, y);
                        i++;
                    }
                }
            }

        }

        public void initScale_Table(DataTable table, DataGridView dataGridView)
        {
            table.Columns.Add("scale", typeof(double));
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (Double.TryParse(dataGridView.Rows[i].Cells[5].Value.ToString(), out double scale))
                {

                    table.Rows.Add(scale);
                }
                else
                {
                    table.Rows.Add();
                }
            }
        }
    }
}
