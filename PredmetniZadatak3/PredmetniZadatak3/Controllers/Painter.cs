using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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
            double pEntityZ = 0.06; // sta je ovo tacno
            double SIZE = 0.06;
            double HEIGHT = 0.1;

            Point3DCollection positionsCollection = new Point3DCollection();


            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY + SIZE, pEntityZ));
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY + SIZE, pEntityZ));

            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY, pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY, pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX + SIZE, powerEntity.PointY + SIZE, pEntityZ + SIZE));
            positionsCollection.Add(new Point3D(powerEntity.PointX, powerEntity.PointY + SIZE, pEntityZ + SIZE));

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions = positionsCollection;

            foreach (var item in model3DGroup.Children)
            {
                while (mesh.Bounds.IntersectsWith(item.Bounds))
                {
                    for (int i = 0; i < mesh.Positions.Count; i++)
                    {
                        mesh.Positions[i] = new Point3D(mesh.Positions[i].X,
                                                        mesh.Positions[i].Y,
                                                        mesh.Positions[i].Z + HEIGHT);
                    }
                }
            }

            Int32Collection triangleIndices = new Int32Collection();
            // Map is standing upright so Z = height, not Y.

            // front
            triangleIndices.Add(1);
            triangleIndices.Add(0);
            triangleIndices.Add(3);

            triangleIndices.Add(1);
            triangleIndices.Add(3);
            triangleIndices.Add(2);

            // back
            triangleIndices.Add(5);
            triangleIndices.Add(6);
            triangleIndices.Add(7);

            triangleIndices.Add(5);
            triangleIndices.Add(7);
            triangleIndices.Add(4);

            // right
            triangleIndices.Add(1);
            triangleIndices.Add(2);
            triangleIndices.Add(5);

            triangleIndices.Add(5);
            triangleIndices.Add(2);
            triangleIndices.Add(6);

            // lower
            triangleIndices.Add(0);
            triangleIndices.Add(1);
            triangleIndices.Add(4);

            triangleIndices.Add(1);
            triangleIndices.Add(5);
            triangleIndices.Add(4);
            
            // left
            triangleIndices.Add(0);
            triangleIndices.Add(4);
            triangleIndices.Add(3);

            triangleIndices.Add(4);
            triangleIndices.Add(7);
            triangleIndices.Add(3);

            // upper
            triangleIndices.Add(7);
            triangleIndices.Add(6);
            triangleIndices.Add(2);

            triangleIndices.Add(7);
            triangleIndices.Add(2);
            triangleIndices.Add(3);


            mesh.TriangleIndices = triangleIndices;
            DiffuseMaterial material = new DiffuseMaterial(GetColor(powerEntity));            
            GeometryModel3D model = new GeometryModel3D(mesh, material);

            model.SetValue(FrameworkElement.TagProperty, powerEntity);

            return model;
        }

        private static Brush GetColor(PowerEntity powerEntity)
        {
            switch(powerEntity.TypeEntity)
            {
                case TypeEntity.Substation:
                    return Brushes.Green;

                case TypeEntity.Node:
                    return Brushes.Blue;

                case TypeEntity.Switch:
                    return Brushes.Orange;

                default:
                    return Brushes.White;
            }
        }

        public static GeometryModel3D CreateLineEntity(LineEntity lineEntity, Model3DGroup model3DGroup)
        {


            GeometryModel3D model = new GeometryModel3D();
            return model;
        }

        
    }
}
