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
        public static void Save()
        {
            var matrixFile = new StreamWriter(Constants.MatrixPath);
            var vectorFile = new StreamWriter(Constants.VectorPath);
            var latLngFile = new StreamWriter(Constants.LatLngPath);

            // сохраняем выходные данные в vectorFile
            var inBuffers =
                DiagramItemManager.Instance.Items.Where(x => x.DiagramItemType == DiagramItemType.BufferIn).OrderBy(
                    x => x.Id).ToList();
            var outLine = new StringBuilder();
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
            
            // сохраним все оставшиеся связи
            outLine.Clear();
            foreach (var diagramItemsDevice in DiagramItemManager.Instance.DiagramItemsDevices)
            {
                var element = DiagramItemManager.Instance.DiagramItemsDevices.Single(x => x.Id == diagramItemsDevice.Id);
                foreach (var secondItemDevice in DiagramItemManager.Instance.DiagramItemsDevices)
                {
                    var elementSecondary =
                        DiagramItemManager.Instance.DiagramItemsDevices.Single(x => x.Id == secondItemDevice.Id);
                    var link = element.ConnectionArrows.FirstOrDefault(x => Equals(x.TargetItem, elementSecondary));
                    outLine.AppendFormat("{0} ", link == null ? 0 : link.Chance);
                }
                outLine.AppendLine();
            }
            matrixFile.Write(outLine.ToString());
            matrixFile.Close();
            matrixFile.Dispose();

            // сохраним координаты элементов
            outLine.Clear();
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
