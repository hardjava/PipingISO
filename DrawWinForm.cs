using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace PipingISO
{
    public class DrawWinForm : Form
    {
        private PictureBox pictureBox;
        private DataTable table;

        public DrawWinForm(DataTable table)
        {
            this.table = table;
        }

        public void draw()
        {

            if (table == null || table.Rows.Count < 2)
            {
                // 데이터가 충분하지 않으면 그리지 않음
                MessageBox.Show("Value is Null or not enough data points to draw.");
                return;
            }
            else
            {
                pictureBox = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                // Paint 이벤트에 대한 핸들러 연결
                pictureBox.Paint += PictureBox_Paint;

                // PictureBox를 폼에 추가
                Controls.Add(pictureBox);

                // 폼의 기본 설정
                Width = 1200;
                Height = 800;

                ShowDialog();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Graphics 개체를 가져와서 선을 그림
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            float scale = 7;
            int worksheetHeight = 80; // Adjust this value according to your needs
            int offsetX = 60; // This is the amount by which we will shift the lines to the right

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

                    g.DrawLine(pen, (float)startX, (float)startY, (float)endX, (float)endY);
                }
            }
            
            pen.Dispose();   
            // shape 그리기
            float shapeScale = 1.3f;

            float width = (float)(maxX - minX) * shapeScale;
            float height = (float)(maxY - minY) * shapeScale;
            float left = (float)minX - (float)(maxX - minX) * (shapeScale - 1) / 2;
            float top = (float)minY - (float)(maxY - minY) * (shapeScale - 1) / 2;
            drawShapeWithImage(left, top, width, height, e);
        }

        private void drawShapeWithImage(float left, float top, float width, float height, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            // 사각형 그리기를 위한 펜 생성
            Pen pen = new Pen(Color.Navy, 2);
            
            // 사각형 그리기
            g.DrawRectangle(pen, left, top, width, height);
            
            // 이미지의 절대 경로 얻기
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = System.IO.Path.Combine(currentDirectory, "images", "Direction.png");
            
            // 이미지 삽입
            using (Image image = Image.FromFile(imagePath))
            {
                // 조절할 이미지의 크기를 설정
                int newImageWidth = 50;   // 새 너비
                int newImageHeight = 50;  // 새 높이

                // 사각형의 우측 상단에 이미지를 배치하기 위한 위치 계산
                float imageLeft = left + width - newImageWidth;
                float imageTop = top;

                // 이미지의 새 위치와 크기를 정의
                Rectangle destRect = new Rectangle((int)imageLeft, (int)imageTop, newImageWidth, newImageHeight);

                // 조절된 크기로 이미지를 그림
                g.DrawImage(image, destRect);
            }
            
            pen.Dispose();
        }
    }
}