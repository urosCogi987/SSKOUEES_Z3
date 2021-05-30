using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak3.Models
{
    public class NodeEntity : PowerEntity 
    {
        private Brush color;
        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }


        public NodeEntity()
        {
            Color = Brushes.LightBlue;
        }
    }
}
