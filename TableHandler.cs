using System;
using System.Collections;
using System.Collections.Generic;
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

                lastX = x;
                lastY = y;
                lastZ = z;
            }
        }

        public void transfer3Dto2D(DataTable table, DataTable table_3D)
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

        public void init2_5R_Table(DataTable table, DataTable table_2_5F, DataGridView dataGridView,
            DataTable table_scale)
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

        public void init3D_scale_Table(DataTable table, DataTable table_3D, DataGridView dataGridView,
            DataTable table_scale)
        {
            table.Columns.Add("x", typeof(double));
            table.Columns.Add("y", typeof(double));
            table.Columns.Add("z", typeof(double));

            double x = 0, y = 0, z = 0;
            for (int i = 0; i < table_3D.Rows.Count; i++)
            {
                if (Double.TryParse(table_3D.Rows[i][0].ToString(), out x))
                {
                    Double.TryParse(table_3D.Rows[i][1].ToString(), out y);
                    Double.TryParse(table_3D.Rows[i][2].ToString(), out z);

                    if (Double.TryParse(table_scale.Rows[i][0].ToString(), out double scale))
                    {
                        Double.TryParse(table_3D.Rows[i - 1][0].ToString(), out double last3D_X);
                        Double.TryParse(table_3D.Rows[i - 1][1].ToString(), out double last3D_Y);
                        Double.TryParse(table_3D.Rows[i - 1][2].ToString(), out double last3D_Z);
                        
                        Double.TryParse(table.Rows[i - 1][0].ToString(), out double last3D_scale_X);
                        Double.TryParse(table.Rows[i - 1][1].ToString(), out double last3D_scale_Y);
                        Double.TryParse(table.Rows[i - 1][2].ToString(), out double last3D_scale_Z);
                        
                        double changed_X = last3D_scale_X - last3D_X;
                        double changed_Y = last3D_scale_Y - last3D_Y;
                        double changed_Z = last3D_scale_Z - last3D_Z;

                        x += changed_X;
                        y += changed_Y;
                        z += changed_Z;

                        double dx = x - last3D_scale_X;
                        double dy = y - last3D_scale_Y;
                        double dz = z - last3D_scale_Z;
                        
                        dx *= scale;
                        dy *= scale;
                        dz *= scale;

                        table.Rows.Add(last3D_scale_X + dx, last3D_scale_Y + dy, last3D_scale_Z + dz);
                    }
                    else
                    {
                        table.Rows.Add(x, y, z);
                    }

                }
                else
                {
                    table.Rows.Add();
                    if (int.TryParse(dataGridView.Rows[i + 1].Cells[0].Value.ToString(), out int rng))
                    {
                        Double.TryParse(table.Rows[rng - 1][0].ToString(), out x);
                        Double.TryParse(table.Rows[rng - 1][1].ToString(), out y);
                        Double.TryParse(table.Rows[rng - 1][2].ToString(), out z);
                        table.Rows.Add(x, y, z);
                        i++;
                    }
                }
            }
        }
/*
        public void removeBackLine(DataTable table, DataTable table_3D_scale)
        {
            table.Columns.Add("x", typeof(double));
            table.Columns.Add("y", typeof(double));
            // 기준 선분
            int i;
            for (i = 1; i < table_3D_scale.Rows.Count; i++)
            {
                if(Double.TryParse(table_3D_scale.Rows[i][0].ToString(), out double standardX2)){
                    if(Double.TryParse(table_3D_scale.Rows[i - 1][0].ToString(), out double standardX1))
                    {
                        Double.TryParse(table_3D_scale.Rows[i][1].ToString(), out double standardY2);
                        Double.TryParse(table_3D_scale.Rows[i][2].ToString(), out double standardZ2);

                        Double.TryParse(table_3D_scale.Rows[i - 1][1].ToString(), out double standardY1);
                        Double.TryParse(table_3D_scale.Rows[i - 1][2].ToString(), out double standardZ1);

                        Point standardP1 = new Point(standardX1, standardY1, standardZ1);
                        Point standardP2 = new Point(standardX2, standardY2, standardZ2);

                        Line standardLine = new Line(standardP1, standardP2); // 기준 라인

                        //대상 라인
                        for (int j = 1; j < table_3D_scale.Rows.Count; j++)
                        {
             if (j != i + 1 && j != i - 1 && j != i + 2 && j != i - 2)
               {
                                if (Double.TryParse(table_3D_scale.Rows[j][0].ToString(), out double comparisonX2))
                                {
                                    if (Double.TryParse(table_3D_scale.Rows[j - 1][0].ToString(),
                                            out double comparisonX1))
                                    {
                                        Double.TryParse(table_3D_scale.Rows[j][1].ToString(), out double comparisonY2);
                                        Double.TryParse(table_3D_scale.Rows[j][2].ToString(), out double comparisonZ2);

                                        Double.TryParse(table_3D_scale.Rows[j - 1][1].ToString(),
                                            out double comparisonY1);
                                        Double.TryParse(table_3D_scale.Rows[j - 1][2].ToString(),
                                            out double comparisonZ1);

                                        Point comparisonP1 = new Point(comparisonX1, comparisonY1, comparisonZ1);
                                        Point comparisonP2 = new Point(comparisonX2, comparisonY2, comparisonZ2);

                                        Line comparisonLine = new Line(comparisonP1, comparisonP2); // 비교 라인

                                        if (standardLine.isLineIntersectAndBehind(comparisonLine))
                                        {
                                            MessageBox.Show(i + " " + j);
                                            // 겹치고 뒤에 있는 선이면 반 갈라서 넣기
                                            }
                                        }
                                }
                            }
                        } // 비교 끝남
                        //그냥 넣기
                        
                    }
                }
            }
        }
        */

      public void removeBackLine(DataTable table_3D_scale, DataTable table_2_5R)
        {
            // 기준 선분
            int i;
            for (i = 1; i < table_2_5R.Rows.Count; i++)
            {
                if(Double.TryParse(table_2_5R.Rows[i][0].ToString(), out double standardX2)){
                    if(Double.TryParse(table_2_5R.Rows[i - 1][0].ToString(), out double standardX1))
                    {
                        Double.TryParse(table_2_5R.Rows[i][1].ToString(), out double standardY2);

                        Double.TryParse(table_2_5R.Rows[i - 1][1].ToString(), out double standardY1);

                        Point standardP1 = new Point(standardX1, standardY1);
                        Point standardP2 = new Point(standardX2, standardY2);

                        Line standardLine = new Line(standardP1, standardP2); // 기준 라인

                        //대상 라인
                        for (int j = 1; j < table_2_5R.Rows.Count; j++)
                        {
                            if (j != i + 1 && j != i - 1)
                            {
                                if (Double.TryParse(table_2_5R.Rows[j][0].ToString(), out double comparisonX2))
                                {
                                    if (Double.TryParse(table_2_5R.Rows[j - 1][0].ToString(), out double comparisonX1))
                                    {
                                        Double.TryParse(table_2_5R.Rows[j][1].ToString(), out double comparisonY2);

                                        Double.TryParse(table_2_5R.Rows[j - 1][1].ToString(), out double comparisonY1);

                                        Point comparisonP1 = new Point(comparisonX1, comparisonY1);
                                        Point comparisonP2 = new Point(comparisonX2, comparisonY2);

                                        Line comparisonLine = new Line(comparisonP1, comparisonP2); // 비교 라인
                                        double t, s;
                                        if (!comparisonP1.isSameValue(standardP2) && !comparisonP2.isSameValue(standardP1) && standardLine.isLineIntersect(comparisonLine,out t,out s))
                                        {
                                            Double.TryParse(table_3D_scale.Rows[i][0].ToString(), out double standard3dX2);
                                            Double.TryParse(table_3D_scale.Rows[i - 1][0].ToString(), out double standard3dX1);
                                            Double.TryParse(table_3D_scale.Rows[i][1].ToString(), out double standard3dY2);
                                            Double.TryParse(table_3D_scale.Rows[i - 1][1].ToString(), out double standard3dY1);
                                            Double.TryParse(table_3D_scale.Rows[i][2].ToString(), out double standard3dZ2);
                                            Double.TryParse(table_3D_scale.Rows[i - 1][2].ToString(), out double standard3dZ1);

                                            Point standard3dP1 = new Point(standard3dX1, standard3dY1, standard3dZ1); // 기준 라인의 3D 좌표를 이용한 점
                                            Point standard3dP2 = new Point(standard3dX2, standard3dY2, standard3dZ2);

                                            Double.TryParse(table_3D_scale.Rows[j][0].ToString(), out double comparison3dX2);
                                            Double.TryParse(table_3D_scale.Rows[j - 1][0].ToString(), out double comparison3dX1);
                                            Double.TryParse(table_3D_scale.Rows[j][1].ToString(), out double comparison3dY2);
                                            Double.TryParse(table_3D_scale.Rows[j - 1][1].ToString(), out double comparison3dY1);
                                            Double.TryParse(table_3D_scale.Rows[j][2].ToString(), out double comparison3dZ2);
                                            Double.TryParse(table_3D_scale.Rows[j - 1][2].ToString(), out double comparison3dZ1);

                                            Point comparison3dP1 = new Point(comparison3dX1, comparison3dY1, comparison3dZ1);
                                            Point comparison3dP2 = new Point(comparison3dX2, comparison3dY2, comparison3dZ2);

                                            if (standardLine.isBehindLine(comparisonLine, standard3dP1, standard3dP2,
                                                    comparison3dP1, comparison3dP2, t, s))
                                            {
                                                //standardLine 이 BehindLine 이면 Cut
                                                // standardX1
                                                // standardX2
                                                // standardY1
                                                // standardY2
                                                
                                                double cuttedSclae = 0.5;
                                                double tX = (1 - t) * standardX1 +
                                                             t * standardX2;
                                                double tY = (1 - t) * standardY1 +
                                                            t * standardY2;

                                                // 선분의 방향 벡터 계산
                                                double directionX = standardX2 - standardX1;
                                                double directionY = standardY2 - standardY1;
                                                
                                                // 방향 벡터의 길이(유클리드 거리)를 계산.
                                                double length = Math.Sqrt(directionX * directionX + directionY * directionY);

                                                // 방향 벡터를 정규화 (길이가 1이 되도록 만든다).
                                                double normalizedDirectionX = directionX / length;
                                                double normalizedDirectionY = directionY / length;
                                                
                                                // t에 대한 교차점에서 cuttedScale만큼 이동하여 새로운 점을 계산
                                                double cuttedX1 = tX - cuttedSclae * normalizedDirectionX;
                                                double cuttedY1 = tY - cuttedSclae * normalizedDirectionY;

                                                double cuttedX2 = tX + cuttedSclae * normalizedDirectionX;
                                                double cuttedY2 = tY + cuttedSclae * normalizedDirectionY;
                                                
                                                DataRow newRow1 = table_2_5R.NewRow(); 
                                                table_2_5R.Rows.InsertAt(newRow1, i);
                                                table_2_5R.Rows[i]["x"] = cuttedX1;
                                                table_2_5R.Rows[i]["y"] = cuttedY1;

                                                DataRow newRow2 = table_2_5R.NewRow();
                                                table_2_5R.Rows.InsertAt(newRow2, i + 1);
                                                
                                                DataRow newRow3 = table_2_5R.NewRow(); 
                                                table_2_5R.Rows.InsertAt(newRow3, i + 2);
                                                table_2_5R.Rows[i + 2]["x"] = cuttedX2;
                                                table_2_5R.Rows[i + 2]["y"] = cuttedY2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
  
    }
}