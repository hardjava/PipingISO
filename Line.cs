namespace PipingISO
{
    public class Line
    {
        private Point point1;
        private Point point2;

        public Line(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public bool isLineIntersect(Line comparisonLine, out double t, out double s)
        {
            // 기준 선분의 점
            double x1 = point1.getX();
            double y1 = point1.getY();
            double x2 = point2.getX();
            double y2 = point2.getY();
            
            // 비교 선분의 점
            double x3 = comparisonLine.point1.getX();
            double y3 = comparisonLine.point1.getY();
            double x4 = comparisonLine.point2.getX();
            double y4 = comparisonLine.point2.getY();
            
            double denominator = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            if (denominator == 0)
            {
                t = 0;
                s = 0;
                return false; // 선분이 평행하거나 일치하는 경우
            }

          t = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / denominator;
            
          s = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / denominator;

            // t 와 s 가 모두 0이면, 선분이 일치하는 경우
            if (t == 0 && s == 0)
            {
                return false;
            }
            
            // t와 s가 0 과 1 사이에 있으면, 선분이 교차하는 경우로 판별
            return (t >= 0 && t <= 1 && s >= 0 && s <= 1);
        }

        private double getDepth(Point point)
        {
            return -0.57735 * point.getX() - 0.57735 * point.getY() + 0.57735 * point.getZ();
        }

        public bool isBehindLine(Line comparisonLine, Point standard3dP1, Point standard3dP2, Point comparison3dP1, Point comparison3dP2, double t, double s)
        {
            double x3dt = (1 - t) * standard3dP1.getX() + t * standard3dP2.getX();
            double y3dt = (1 - t) * standard3dP1.getY() + t * standard3dP2.getY();
            double z3dt = (1 - t) * standard3dP1.getZ() + t * standard3dP2.getZ();

            double x3ds = (1 - s) * comparison3dP1.getX() + t * comparison3dP2.getX();
            double y3ds = (1 - s) * comparison3dP1.getY() + t * comparison3dP2.getY();
            double z3ds = (1 - s) * comparison3dP1.getZ() + t * comparison3dP2.getZ();
        
            Point tPoint = new Point(x3dt, y3dt, z3dt);
            Point sPoint = new Point(x3ds, y3ds, z3ds);

            return getDepth(tPoint) < getDepth(sPoint);
        }
    }
}