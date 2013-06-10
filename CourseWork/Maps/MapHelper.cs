using System;
using System.Windows;
using CourseWork.Templates;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace CourseWork.Maps
{
    public class MapHelper
    {
        private static MapHelper _instance;

        public static MapHelper SetInstance(GMapControl map)
        {
            return _instance ?? (_instance = new MapHelper(map));
        }

        public static MapHelper Instance
        {
            get
            {
                if (_instance == null) throw new Exception("Используй инициализацию с параметром");
                return _instance;
            }
        }

        private static GMapControl _map;
        private MapHelper(GMapControl map)
        {
            _map = map;
        }

        /// <summary>
        /// Обновить положение элемента (с LatLng)
        /// </summary>
        /// <param name="item"></param>
        public void UpdateLatLngPoses(DiagramItem item)
        {
            item.PositionLatLng = _map.FromLocalToLatLng(Convert.ToInt32(item.CenterPoint.X),
                                                         Convert.ToInt32(item.CenterPoint.Y));
        }
    }
}
