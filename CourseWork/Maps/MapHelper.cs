using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWork.Manager;
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
        /// Обновить положение элемента относительно экранных координат
        /// </summary>
        /// <param name="item"></param>
        public void UpdateLatLngPoses(DiagramItem item)
        {
            item.PositionLatLng = _map.FromLocalToLatLng(Convert.ToInt32(item.CenterPoint.X),
                                                         Convert.ToInt32(item.CenterPoint.Y));
        }

        /// <summary>
        /// Обновить положение элемента на экрани относительно его LatLng
        /// </summary>
        /// <param name="item"></param>
        public void UpdateScreenCoords(DiagramItem item)
        {
            var point = _map.FromLatLngToLocal(item.PositionLatLng);
            item.CenterPoint = new Point(point.X, point.Y);
        }

        /// <summary>
        /// Авторазмер карты для отображения всех элементов
        /// </summary>
        public void FitMapToScreen()
        {
            var lat = DiagramItemManager.Instance.Items.Max(x => x.PositionLatLng.Lat);
            var lng = DiagramItemManager.Instance.Items.Min((x => x.PositionLatLng.Lng));
            var heightLat = lat - DiagramItemManager.Instance.Items.Min((x => x.PositionLatLng.Lat));
            var widthLng = DiagramItemManager.Instance.Items.Max(x => x.PositionLatLng.Lng) - lng;
            _map.SetZoomToFitRect(new RectLatLng(lat, lng, widthLng, heightLat));
            ReDrawElements();
        }

        /// <summary>
        /// Перерисовать элементы на карте
        /// </summary>
        public void ReDrawElements()
        {
            foreach (var diagramItem in DiagramItemManager.Instance.Items)
            {
                var point = _map.FromLatLngToLocal(diagramItem.PositionLatLng);
                diagramItem.Move(point.X, point.Y);
            }
        }

        /// <summary>
        /// Приблизить/отдалить карту
        /// </summary>
        /// <param name="delta">размер приближения</param>
        public void Zoom(int delta)
        {
            _map.Zoom += delta;

            // приближаем - двигаем к курсору; отодвигаем - оставляем на месте
            if (delta > 0)
            {
                _map.Position = _map.FromLocalToLatLng(Convert.ToInt32(Mouse.GetPosition(_map).X),
                                                         Convert.ToInt32(Mouse.GetPosition(_map).Y));
            }

            ReDrawElements();
        }
    }
}
