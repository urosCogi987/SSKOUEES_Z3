using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak3.Models
{
    public class PowerEntity
    {
        private long id;
        private string  name;
        private double x;
        private double y;        
        private double pointX;
        private double pointY;
        private string toolTip;
        private TypeEntity typeEntity;        



        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public double PointX
        {
            get { return pointX; }
            set { pointX = value; }
        }
        public double PointY
        {
            get { return pointY; }
            set { pointY = value; }
        }
        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }
        public TypeEntity TypeEntity
        {
            get { return typeEntity; }
            set { typeEntity = value; }
        }


        public  PowerEntity() { }
    }
}
