using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PredmetniZadatak3.Models
{
    public class LineEntity
    {
        private long id;
        private string name;        
        private bool isUnderground;
        private float r;
        private string conductorMaterial;
        private string lineType;
        private long thermalConstantHeat;
        private long firstEnd;
        private long secondEnd;
        private string tooltip;
        private List<Point3D> vertices;
        private List<Point3D> convertedVertices;        
        private Brush color;


        
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
        public bool IsUnderground
        {
            get { return isUnderground; }
            set { isUnderground = value; }
        }
        public float R
        {
            get { return r; }
            set { r = value; }
        }
        public string ConductorMaterial
        {
            get { return conductorMaterial; }
            set { conductorMaterial = value; }
        }
        public string LineType
        {
            get { return lineType; }
            set { lineType = value; }
        }
        public long ThermalConstantHeat
        {
            get { return thermalConstantHeat; }
            set { thermalConstantHeat = value; }
        }
        public long FirstEnd
        {
            get { return firstEnd; }
            set { firstEnd = value; }
        }
        public long SecondEnd
        {
            get { return secondEnd; }
            set { secondEnd = value; }
        }
        public string Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }
        public List<Point3D> Vertices
        {
            get { return vertices; }
            set { vertices = value; }
        }
        public List<Point3D> ConvertedVertices
        {
            get { return convertedVertices; }
            set { convertedVertices = value; }
        }
        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }


        public LineEntity()
        {
            Color = Brushes.LightBlue;
        }

    }
}
