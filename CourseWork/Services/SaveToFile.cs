using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Manager;
using CourseWork.Templates;

namespace CourseWork.Services
{
    public static class SaveToFile
    {
        /// <summary>
        /// Сохранить текущее состояние редактора
        /// </summary>
        public static void Save()
        {
            SaveVectorChances();
            SaveMatrixChance();
            SaveLatLngCoords();
        }

        /// <summary>
        /// Сохранить данные о входных буфферах
        /// </summary>
        private static void SaveVectorChances()
        {
            var vectorFile = new StreamWriter(Constants.VectorPath);
            var outLine = new StringBuilder();

            var inBuffers =
                DiagramItemManager.Instance.Items.Where(x => x.DiagramItemType == DiagramItemType.BufferIn).OrderBy(
                    x => x.Id).ToList();
            foreach (var buffer in inBuffers)
            {
                foreach (var diagramItemsDevice in DiagramItemManager.Instance.DiagramItemsDevices)
                {
                    var link = buffer.ConnectionArrowsOut.SingleOrDefault(x => x.TargetItem.Id == diagramItemsDevice.Id);
                    outLine.AppendFormat("{0} ", link == null ? 0 : link.Chance);
                }
                outLine.AppendLine();
            }

            vectorFile.Write(outLine.ToString());
            vectorFile.Close();
            vectorFile.Dispose();
        }

        /// <summary>
        /// Сохранить связи между устройствами
        /// </summary>
        private static void SaveMatrixChance()
        {
            var matrixFile = new StreamWriter(Constants.MatrixPath);
            var outLine = new StringBuilder();

            foreach (var diagramItemsDevice in DiagramItemManager.Instance.DiagramItemsDevices)
            {
                foreach (var secondItemDevice in DiagramItemManager.Instance.DiagramItemsDevices)
                {
                    var link =
                        diagramItemsDevice.ConnectionArrowsOut.FirstOrDefault(x => x.TargetItem.Id == secondItemDevice.Id);
                    outLine.AppendFormat("{0} ", link == null ? 0 : link.Chance);
                }
                outLine.AppendLine();
            }

            matrixFile.Write(outLine.ToString());
            matrixFile.Close();
            matrixFile.Dispose();
        }

        /// <summary>
        /// Сохранить координаты всех элементов
        /// </summary>
        private static void SaveLatLngCoords()
        {
            var latLngFile = new StreamWriter(Constants.LatLngPath);
            var outLine = new StringBuilder();

            var allItems = DiagramItemManager.Instance.Items.OrderBy(x => x.Id);
            foreach (var diagramItem in allItems)
            {
                outLine.AppendFormat("{0} {1}", diagramItem.PositionLatLng.Lat, diagramItem.PositionLatLng.Lng);
                outLine.AppendLine();
            }

            latLngFile.Write(outLine.ToString());
            latLngFile.Close();
            latLngFile.Dispose();
        }
    }
}
