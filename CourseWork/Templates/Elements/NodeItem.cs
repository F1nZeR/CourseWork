using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseWork.Templates.Elements
{
    public class NodeItem : DiagramItem
    {
        public NodeItem(string name, DiagramItemType type) : base(name, type)
        {}

        public NodeItem(string name, DiagramItemType type, double x = 0, double y = 0) : base(name, type, x, y)
        {}
    }
}
