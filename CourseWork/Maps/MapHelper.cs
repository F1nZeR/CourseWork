using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using SeMOEditor.Manager;
using SeMOEditor.Properties;
using SeMOEditor.Templates;
using SeMOEditor.Templates.Elements;
using SeMOEditor.Utilities.Helpers;

namespace SeMOEditor.Maps
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
            map.Loaded += (o, args) => ReDrawElements();
            map.SizeChanged += (o, args) => ReDrawElements();
            map.OnMapZoomChanged += ReDrawElements;
            map.OnMapDrag += ReDrawElements;
        }

        public double MapZoom { get { return _map.Zoom; } }

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

            // приближаем - двигаем к курсору; отодвигаем - оставляем на месте
            if (delta > 0)
            {
                _map.Position = _map.FromLocalToLatLng(Convert.ToInt32(Mouse.GetPosition(_map).X),
                                                         Convert.ToInt32(Mouse.GetPosition(_map).Y));
            }
            _map.Zoom += delta;
        }

        /// <summary>
        /// Избавляемся от предыдущих групп
        /// </summary>
        public void RemovePreviousGroups()
        {
            foreach (var groupDevicese in DiagramItemManager.Instance.GroupDeviceses)
            {
                foreach (var connectionArrow in groupDevicese.ConnectionArrows)
                {
                    DiagramItemManager.Instance.Remove(connectionArrow);
                }
                groupDevicese.Decompose();
                DiagramItemManager.Instance.Remove(groupDevicese);
            }

            foreach (var diagramItem in DiagramItemManager.Instance.Items)
            {
                diagramItem.ConnectionArrows.RemoveAll(x =>
                    x.TargetItem.GetType() == typeof (GroupItem) ||
                    x.FromItem.GetType() == typeof (GroupItem));
            }
        }

        /// <summary>
        /// Объединение близлежащих элементов
        /// </summary>
        private void CheckDistance()
        {
            RemovePreviousGroups();

            // начало
            const int replDistance = 40;
            _lookableItems =
                DiagramItemManager.Instance.Items.Where(x => x.Visibility == Visibility.Visible).ToList();

            _distMatrix = new int[_lookableItems.Count, _lookableItems.Count];
            _chMatrix = new int[_lookableItems.Count, _lookableItems.Count];
            for (int i = 0; i < _lookableItems.Count - 1; i++)
            {
                for (int j = i + 1; j < _lookableItems.Count; j++)
                {
                    var distance = MathHelper.Distance(_lookableItems[i].CenterPoint, _lookableItems[j].CenterPoint);
                    _distMatrix[i, j] = distance < replDistance ? 1 : 0;
                }
            }

            for (int i = 0; i < _lookableItems.Count; i++)
            {
                var currentGroup = new List<DiagramItem>();
                DFS(i, currentGroup);

                // текущую группу необходимо сгруппировать
                if (currentGroup.Count > 1)
                {
                    var group = DiagramItemManager.Instance.AddNewItemGroupItem(new Point());
                    currentGroup.ForEach(group.Add);
                    group.Compose();
                    group.MakeCentered();
                }
            }
        }

        private int[,] _distMatrix, _chMatrix;
        private List<DiagramItem> _lookableItems;
        /// <summary>
        /// Просмотр в глубину матрицы весов расстояний
        /// </summary>
        /// <param name="i"></param>
        /// <param name="currentGroup"></param>
        private void DFS(int i, List<DiagramItem> currentGroup)
        {
            for (int j = 0; j < _distMatrix.GetLength(0); j++)
            {
                if (_chMatrix[i, j] == 1) continue;

                _chMatrix[i, j] = 1;
                _chMatrix[j, i] = 1;

                if (_distMatrix[i, j] == 1)
                {
                    if (!currentGroup.Contains(_lookableItems[i])) currentGroup.Add(_lookableItems[i]);
                    if (!currentGroup.Contains(_lookableItems[j])) currentGroup.Add(_lookableItems[j]);

                    DFS(j, currentGroup);
                }
            }
        }

        private void UpdateGroupsConnections()
        {
            var tuples = new List<Tuple<DiagramItem, DiagramItem>>();
            foreach (var group in DiagramItemManager.Instance.GroupDeviceses.Where(x => x.Visibility == Visibility.Visible))
            {
                foreach (var diagramItem in group.GetItems())
                {
                    foreach (var connectionArrow in diagramItem.ConnectionArrows.
                        Where(x => x.Visibility != Visibility.Collapsed))
                    {
                        // связь не внутри одной группы
                        if (group.GetItems().Contains(connectionArrow.TargetItem) &&
                            group.GetItems().Contains(connectionArrow.FromItem)) continue;

                        // связь соединяет блоки в группах
                        if (connectionArrow.TargetItem.Visibility == Visibility.Hidden &&
                            connectionArrow.FromItem.Visibility == Visibility.Hidden)
                        {
                            var item1 = DiagramItemManager.Instance.GroupDeviceses.FirstOrDefault(
                                x => x.GetItems().Contains(connectionArrow.FromItem));
                            var item2 = DiagramItemManager.Instance.GroupDeviceses.FirstOrDefault(
                                x => x.GetItems().Contains(connectionArrow.TargetItem));

                            if (item1 == null || item2 == null) continue;
                            tuples.Add(new Tuple<DiagramItem, DiagramItem>(item1, item2));
                        }
                        else
                        {
                            // связь между группой и отдельным блоком
                            var fGroup =
                                DiagramItemManager.Instance.GroupDeviceses.FirstOrDefault(
                                    x => x.GetItems().Contains(connectionArrow.FromItem));
                            var item = connectionArrow.TargetItem;
                            if (fGroup == null)
                            {
                                fGroup = DiagramItemManager.Instance.GroupDeviceses.FirstOrDefault(
                                    x => x.GetItems().Contains(connectionArrow.TargetItem));
                                item = connectionArrow.FromItem;
                                tuples.Add(new Tuple<DiagramItem, DiagramItem>(item, fGroup));
                            }
                            else
                            {
                                tuples.Add(new Tuple<DiagramItem, DiagramItem>(fGroup, item));
                            }
                        }
                    }
                }
            }

            var result = tuples.Distinct().ToList();
            result.RemoveAll(x => x.Item1.Visibility == Visibility.Hidden ||
                                  x.Item2.Visibility == Visibility.Hidden);
            foreach (var tuple in result)
            {
                DiagramItemManager.Instance.AddNewLink(tuple.Item1, tuple.Item2, 0, true);
            }
        }

        public void GroupElements()
        {
            if (Settings.Default.AutoGrouping)
            {
                CheckDistance();
            }
            else
            {
                foreach (var groupDevicese in DiagramItemManager.Instance.GroupDeviceses)
                {
                    foreach (var connectionArrow in groupDevicese.ConnectionArrows)
                    {
                        DiagramItemManager.Instance.Remove(connectionArrow);
                    }
                    groupDevicese.ConnectionArrows.Clear();

                    groupDevicese.UpdateCurrentView(_map.Zoom);
                    groupDevicese.MakeCentered();
                }
            }

            UpdateGroupsConnections();
        }
    }
}
