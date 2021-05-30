using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PredmetniZadatak3.Models
{
    public class SwitchEntity : PowerEntity
    {
        private Brush color;
        private string status;

        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        public SwitchEntity()
        {
            Color = Brushes.Red;
        }
    }
}
