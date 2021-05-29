using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PredmetniZadatak3.Controllers
{
    public class Painter
    {
        public static void DrawAllEntities(Model3DGroup model3DGroup, HashSet<PowerEntity> powerEntities, HashSet<LineEntity> lineEntities)
        {
            foreach (PowerEntity powerEntity in powerEntities)
            {
                GeometryModel3D model = CreatePowerEntity(powerEntity, model3DGroup);
                model3DGroup.Children.Add(model);
            }

            foreach (LineEntity lineEntity in lineEntities)
            {
                GeometryModel3D model = CreateLineEntity(lineEntity, model3DGroup);
                model3DGroup.Children.Add(model);
            }
        }

        public static GeometryModel3D CreatePowerEntity(PowerEntity powerEntity, Model3DGroup model3DGroup)
        {
            double pEntityZ = 0.05; // sta je ovo tacno
            double SIZE = 0.06;

            Point3DCollection positionsCollection = new Point3DCollection();

            
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY + SIZE, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY + SIZE, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY , pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY , pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY + SIZE, pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY + SIZE, pEntityZ + SIZE));

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = positionsCollection;
            
            // TO DO: NASTAVI, A PRE TOGA PROVERI ZA KOORDINATE DA LI JE DOBRO UNETO (GORE)

            GeometryModel3D model = new GeometryModel3D();
            return model;
        }

        public static GeometryModel3D CreateLineEntity(LineEntity lineEntity, Model3DGroup model3DGroup)
        {


            GeometryModel3D model = new GeometryModel3D();
            return model;
        }
    }
}
