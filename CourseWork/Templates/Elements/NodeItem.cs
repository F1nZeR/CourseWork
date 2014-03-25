using System;
using System.Windows.Media.Imaging;

namespace SeMOEditor.Templates.Elements
{
    public class NodeItem : DiagramItem
    {
        public NodeItem(string name, double x = 0, double y = 0) : base(name, x, y)
        {}

        protected override void UpdateImage()
        {
            imgNavigate.Source = new BitmapImage(new Uri("../Images/Persist.png", UriKind.RelativeOrAbsolute));
        }
    }
}
