using System;
using System.Windows;

namespace SeMOEditor.Utilities.Helpers
{
    public static class MathHelper
    {
        /// <summary>
        /// Вычисляет расстояние между двумя точками
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double Distance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        /// Вычисляет расстояние между двумя точками
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double Distance(Point point1, Point point2)
        {
            return Distance(point1.X, point2.X, point1.Y, point2.Y);
        }
    }
}
