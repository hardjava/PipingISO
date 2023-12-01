using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Microsoft.Office.Core;

namespace PipingISO
{
    public class DrawExcel
    {
        private DataTable table2_5F;
        private DataTable table2_5R;
        private DataTable table_notScaled_2_5R;

        public DrawExcel(DataTable table2_5F, DataTable table2_5R, DataTable table_notScaled_2_5R)
        {
            this.table2_5F = table2_5F;
            this.table2_5R = table2_5R;
            this.table_notScaled_2_5R = table_notScaled_2_5R;
        }

        public void draw()
        {
            if (table2_5F == null || table2_5R == null)
            {
                MessageBox.Show("Value is Null or not enough data points to draw.");
            }
            else
            {
                Excel.Application application = new Excel.Application();
                Excel.Workbook workBook = application.Workbooks.Add();

                Excel.Worksheet iso_2_5F_Sheet = (Excel.Worksheet)workBook.Sheets[1];
                Excel.Worksheet iso_2_5R_Sheet = workBook.Sheets.Add(After: workBook.Worksheets.Item[workBook.Worksheets.Count]);

                iso_2_5F_Sheet.Name = "ISO_2.5F";
                iso_2_5R_Sheet.Name = "ISO_2.5R";
                
                drawLines(iso_2_5F_Sheet, table2_5F);
                drawLines(iso_2_5R_Sheet, table2_5R);
                drawIncheBox(iso_2_5R_Sheet, table_notScaled_2_5R);

                foreach (Excel.Worksheet sheet in workBook.Sheets)
                {
                    sheet.Activate();
                    application.ActiveWindow.DisplayGridlines = false;
                }
                
                application.Visible = true;
                releaseComObjects(new object[] { iso_2_5R_Sheet, iso_2_5F_Sheet, workBook });
            }
        }

        private void drawLines(Excel.Worksheet worksheet, DataTable table)
        {
            if (table == null || table.Rows.Count < 2)
            {
                MessageBox.Show("Value is Null or not enough data points to draw.");
                return;
            }

            float scale = 12;
            int worksheetHeight = 80;  // Adjust this value according to your needs
            int offsetX = 40;  // This is the amount by which we will shift the lines to the right

            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;
            
            for (int i = 0; i < table.Rows.Count - 1; i++)
            {
                if (table.Rows[i][0] != DBNull.Value && table.Rows[i][1] != DBNull.Value &&
                    table.Rows[i + 1][0] != DBNull.Value && table.Rows[i + 1][1] != DBNull.Value)
                {
                    double startX = Convert.ToDouble(table.Rows[i][0]);
                    double startY = Convert.ToDouble(table.Rows[i][1]);
                    double endX = Convert.ToDouble(table.Rows[i + 1][0]);
                    double endY = Convert.ToDouble(table.Rows[i + 1][1]);
                    
                    // Flip the Y coordinates
                    startY = (worksheetHeight - startY) * scale;
                    endY = (worksheetHeight - endY) * scale;

                    // Apply the horizontal offset
                    startX = (startX + offsetX) * scale;
                    endX = (endX + offsetX) * scale;

                    minX = Math.Min(minX, Math.Min(startX, endX));
                    maxX = Math.Max(maxX, Math.Max(startX, endX));
                    minY = Math.Min(minY, Math.Min(startY, endY));
                    maxY = Math.Max(maxY, Math.Max(startY, endY));
                    
                    var line = worksheet.Shapes.AddLine((float)startX, (float)startY, (float)endX, (float)endY);
                    line.Line.ForeColor.RGB = (int)Excel.XlRgbColor.rgbBlack;
                }
            }
            
            // shape 그리기
            float shapeScale = 1.3f;
            
            float width = (float)(maxX - minX) * shapeScale;
            float height = (float)(maxY - minY) * shapeScale;
            float left = (float)minX - (float)(maxX - minX) * (shapeScale - 1) / 2;
            float top = (float)minY - (float)(maxY - minY) * (shapeScale - 1) / 2;
            drawShapeWithImage(left, top, width, height, worksheet);
        }

        private void drawShapeWithImage(float left, float top, float width, float height, Excel.Worksheet worksheet)
        {
            // 사각형 그리기
            var rectangle = worksheet.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, left, top, width, height);
            rectangle.Line.ForeColor.RGB = (int)Excel.XlRgbColor.rgbNavy;
            rectangle.Fill.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            rectangle.Fill.Transparency = 1.0f;
            
            // 그려진 사각형을 맨 뒤로 보내기
            rectangle.ZOrder(MsoZOrderCmd.msoSendToBack);

            // 이미지의 절대 경로 얻기
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine(currentDirectory, "images", "Direction.png");

            // 경로에서 이미지 파일이 존재하는지 확인
            if (!System.IO.File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image file not found", imagePath);
            }

            // 이미지 삽입
            var picture = worksheet.Shapes.AddPicture(imagePath,
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoCTrue,
                left, top, width, height);

            // 이미지 크기와 위치 조정
            picture.Width = 50;
            picture.Height = 50;
            picture.Left = rectangle.Left + rectangle.Width - picture.Width;
            picture.Top = rectangle.Top;
        }

        private void releaseComObjects(object[] comObjects)
        {
            foreach (object comObject in comObjects)
            {
                Marshal.ReleaseComObject(comObject);
            }
        }

        private void drawIncheBox(Excel.Worksheet worksheet, DataTable table_notScaled_2_5R)
        {
            float height = 20;
            float width = 100;
            float scale = 12;
            int worksheetHeight = 80;  // Adjust this value according to your needs
            int offsetX = 40;  // This is the amount by which we will shift the lines to the right

            
            
            for (int i = 1; i < table_notScaled_2_5R.Rows.Count; i++)
            {
                if (Double.TryParse(table_notScaled_2_5R.Rows[i][0].ToString(), out double x2))
                {
                    if(Double.TryParse(table_notScaled_2_5R.Rows[i - 1][0].ToString(), out double x1))
                    {
                        Double.TryParse(table_notScaled_2_5R.Rows[i][1].ToString(), out double y2);
                        Double.TryParse(table_notScaled_2_5R.Rows[i - 1][1].ToString(), out double y1);
                        Point p1 = new Point(x1, y1);
                        Point p2 = new Point(x2, y2);
                        Line line = new Line(p1, p2);
                        
                        
                        // Flip the Y coordinates
                        double startY = (worksheetHeight - y1) * scale;
                        double endY = (worksheetHeight - y2) * scale;

                        // Apply the horizontal offset
                        double startX = (x1 + offsetX) * scale;
                        double endX = (x2 + offsetX) * scale;

                        // Calculate the center point of the line
                        double centerX = (startX + endX) / 2;
                        double centerY = (startY + endY) / 2;

                        // Correct the left and top positions to account for the dimensions of the rectangle
                        // This will place the rectangle's center at the line's center
                        float left = (float)centerX - width / 2;
                        float top = (float)centerY - height / 2 + 10;
                        
                        var rectangle = worksheet.Shapes.AddShape(Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle, left, top, width, height);
                        rectangle.Fill.Visible = MsoTriState.msoFalse;
                        rectangle.Fill.Transparency = 1.0f;
                        rectangle.Line.Visible = MsoTriState.msoFalse;
                        double gradient = line.getGradient(); // 선분의 기울기 구하기
                        double rotationAngle;
                        if (gradient == 0)
                        {
                            if (line.IsSlopeHorizontal())
                            {
                                 rotationAngle = 0;
                            }
                            else
                            {
                                rotationAngle = 90;
                            }
                        }
                        else // 수직이거나 수평이 아닐때
                        {
                            double angle = getAngleFromGradient(gradient);
                            rotationAngle = angle * -1;
                        }
                        
                        
                        rectangle.Rotation = (float)rotationAngle; // rectangle 회전
                        // 도형에 텍스트 추가
                        rectangle.TextFrame.Characters().Text = "04-1/2\"-0.26\n";
                        //rectangle.TextFrame.AutoSize = true; // 텍스트에 맞게 도형 크기 자동 조절
                        
                        // 텍스트 스타일 설정 (예: 폰트 크기, 색상 등)
                        // 텍스트 폰트 색상 설정 (예: 검정색으로 설정)
                        rectangle.TextFrame2.TextRange.Font.Fill.ForeColor.RGB = (int)Excel.XlRgbColor.rgbBlack;
                        rectangle.TextFrame.Characters().Font.Size = 10; // 폰트 크기 설정
                    }
                }
            }
        }
        
        public double getAngleFromGradient(double gradient)
        {
            // Math.Atan은 라디안 값을 반환하므로, 이를 도 단위로 변환합니다.
            double angleInRadians = Math.Atan(gradient);
            double angleInDegrees = angleInRadians * (180.0 / Math.PI);
            return angleInDegrees;
        }
    }
}