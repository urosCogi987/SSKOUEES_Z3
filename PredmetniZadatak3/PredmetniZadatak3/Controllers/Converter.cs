using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace PredmetniZadatak3.Controllers
{
    public class Converter
    {
		//private Point downLeftPoint = new Point(19.793909, 45.2325);
		//private Point upRightPoint = new Point(19.894459, 45.277031);

		private const double ScaleX = (MAX_LON - MIN_LON) / 10;
		private const double ScaleY = (MAX_LAT - MIN_LAT) / 10;

		private const double MIN_LAT = 45.2325;
		private const double MIN_LON = 19.793909;

		private const double MAX_LAT = 45.277031;
		private const double MAX_LON = 19.894459;


		public static void ConvertCoordinates(HashSet<LineEntity> lines, HashSet<SubstationEntity> substations, HashSet<NodeEntity> nodes, HashSet<SwitchEntity> switches, HashSet<PowerEntity> powerEntities)
        {
			double lat = 0;
			double lon = 0;

            foreach (LineEntity line in lines)
            {
                foreach (Point3D point in line.Vertices)
                {
					ToLatLon(point.X, point.Y, 34, out lat, out lon);

					if (!(lat >= MIN_LAT && lat <= MAX_LAT) || !(lon >= MIN_LON && lon <= MAX_LON))
					{
						continue;
					}

					line.ConvertedVertices.Add(new Point3D(lat, lon, 1));
				}


            }

            foreach (SubstationEntity substation in substations)
            {
				ToLatLon(substation.X, substation.Y, 34, out lat, out lon);

				if (!(lat >= MIN_LAT && lat <= MAX_LAT) || !(lon >= MIN_LON && lon <= MAX_LON))
				{
					continue;
				}

				substation.PointX = lon;
				substation.PointY = lat;

				powerEntities.Add(substation);
			}            

            foreach (NodeEntity node in nodes)
            {
				ToLatLon(node.X, node.Y, 34, out lat, out lon);

				if (!(lat >= MIN_LAT && lat <= MAX_LAT) || !(lon >= MIN_LON && lon <= MAX_LON))
				{
					continue;
				}

				node.PointX = lon;
				node.PointY = lat;

				powerEntities.Add(node);
			}

            foreach (SwitchEntity sw in switches)
            {
				ToLatLon(sw.X, sw.Y, 34, out lat, out lon);

				if (!(lat >= MIN_LAT && lat <= MAX_LAT) || !(lon >= MIN_LON && lon <= MAX_LON)) 
				{
					continue;
				}

				sw.PointX = lon;
				sw.PointY = lat;

				powerEntities.Add(sw);
			}
        }		

		public static void RescaleElements(HashSet<PowerEntity> powerEntities, HashSet<LineEntity> lineEntities)
        {
			foreach (PowerEntity entity in powerEntities)
            {
				entity.PointY = ConvertToMap(entity.PointY, MIN_LAT, ScaleX);
				entity.PointX = ConvertToMap(entity.PointX, MIN_LON, ScaleY);
            }

			foreach (LineEntity line in lineEntities)
			{

				for (int i = 0; i < line.ConvertedVertices.Count; i++)
				{
					line.ConvertedVertices[i] = new Point3D(ConvertToMap(line.ConvertedVertices[i].X, MIN_LON, ScaleX), 
														    ConvertToMap(line.ConvertedVertices[i].Y, MIN_LAT, ScaleY), 
															line.ConvertedVertices[i].Z);
				}
			}
		}

		private static double ConvertToMap(double point, double latLon, double scale)
        {
			return Math.Abs((point - latLon) / scale);
		}

		private static void ToLatLon(double utmX, double utmY, int zoneUTM, out double latitude, out double longitude)
		{
			bool isNorthHemisphere = true;

			var diflat = -0.00066286966871111111111111111111111111;
			var diflon = -0.0003868060578;

			var zone = zoneUTM;
			var c_sa = 6378137.000000;
			var c_sb = 6356752.314245;
			var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
			var e2cuadrada = Math.Pow(e2, 2);
			var c = Math.Pow(c_sa, 2) / c_sb;
			var x = utmX - 500000;
			var y = isNorthHemisphere ? utmY : utmY - 10000000;

			var s = ((zone * 6.0) - 183.0);
			var lat = y / (c_sa * 0.9996);
			var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
			var a = x / v;
			var a1 = Math.Sin(2 * lat);
			var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
			var j2 = lat + (a1 / 2.0);
			var j4 = ((3 * j2) + a2) / 4.0;
			var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
			var alfa = (3.0 / 4.0) * e2cuadrada;
			var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
			var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
			var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
			var b = (y - bm) / v;
			var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
			var eps = a * (1 - (epsi / 3.0));
			var nab = (b * (1 - epsi)) + lat;
			var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
			var delt = Math.Atan(senoheps / (Math.Cos(nab)));
			var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

			longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
			latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
		}
	}
}
