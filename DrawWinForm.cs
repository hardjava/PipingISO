using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PipingISO
{
    public class DrawWinForm
    {
        private Chart chart;
        private Form form;

        public void draw(DataTable table)
        {

            if (table == null)
            {
                MessageBox.Show("Value is Null");
            }
            else
            {
                chart = new Chart();
                
                ChartArea chartArea = new ChartArea();
                chart.ChartAreas.Add(chartArea);

                Series series = new Series();
                series.ChartType = SeriesChartType.Line;
                foreach (DataRow row in table.Rows)
                {
                    if (Double.TryParse(row[0].ToString(), out double x))
                    {
                        if (Double.TryParse(row[1].ToString(), out double y))
                        {
                            series.Points.AddXY(x, y);
                        }
                    }
                    else
                    {
                        DataPoint dataPoint = new DataPoint();
                        dataPoint.IsEmpty = true;
                        series.Points.Add(dataPoint);
                    }
                }
                series.Color = Color.Black;

                chart.Series.Add(series);
              
                form = new Form();

                setSize();

                form.Controls.Add(chart);

                removeAxis();

                chart.Titles.Add(addImage());

                form.Show();
                
                form.FormClosing += Form_FormClosing;
            }
        }
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            chart.Series.Clear();
            chart.Dispose();
            form.Dispose();
        }

        private void setSize()
        {
            int height = (int)(10.48 * 37.8);
            int width = (int)(18.91 * 37.8);

            chart.Size = new Size(width, height);

            form.Size = new Size((int)(width * 1.2), (int)(height * 1.2));

            chart.Left = (form.ClientSize.Width - chart.Width) / 2;
            chart.Top = (form.ClientSize.Height - chart.Height) / 2;
        }

        private void removeAxis()
        {
            if (chart != null && chart.ChartAreas.Count > 0)
            {
                chart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;

                chart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            }
        }

        private Title addImage()
        {
            Title title = new Title();
            string imagePath = "Direction.png";
            Image image = Image.FromFile(imagePath);

            title.BackImage = imagePath;
            title.BackImageWrapMode = ChartImageWrapMode.Scaled;
            title.Position.Auto = false;
            title.DockedToChartArea = chart.ChartAreas[0].Name;
            title.IsDockedInsideChartArea = false;
            title.Docking = Docking.Right;
            title.Position.X = 95;
            title.Position.Y = 5;

            title.Position.Width = 10;
            title.Position.Height = 10;

            return title;
        }
    }
}