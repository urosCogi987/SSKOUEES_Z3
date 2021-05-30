using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak3.Models
{
    public class SubstationEntity : PowerEntity
    {
        private Brush color;
        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }


        public SubstationEntity()
        {
            Color = Brushes.Pink;
        }
    }
}
