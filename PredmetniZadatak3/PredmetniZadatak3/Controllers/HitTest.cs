using PredmetniZadatak3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PredmetniZadatak3.Controllers
{
    public class HitTest
    {
        Viewport3D viewport;
        Window window;
        Model3DGroup model3DGroup;
        HashSet<GeometryModel3D> highlightedEntities;

        private ToolTip toolTip = new ToolTip();


        public HitTest(Viewport3D viewport, Model3DGroup model3DGroup, Window window)
        {
            highlightedEntities = new HashSet<GeometryModel3D>();
            this.viewport = viewport;
            this.window = window;
            this.model3DGroup = model3DGroup;

            InitManipulation();
        }

        private void InitManipulation()
        {
            viewport.MouseRightButtonDown += MouseRightButtonDownEvent;
        }

        private HitTestResultBehavior HTResult(HitTestResult result)
        {
            CancelHighlight();

            RayHitTestResult hitResult = result as RayHitTestResult;
            var value = hitResult?.ModelHit.GetValue(FrameworkElement.TagProperty);

            if (value is PowerEntity)
            {
                toolTip.Content = ((PowerEntity)value).ToolTip;
                toolTip.IsOpen = true;
            }
            else if (value is LineEntity)
            {                
                LineEntity line = value as LineEntity;

                GeometryModel3D startNode = (GeometryModel3D)model3DGroup.Children.FirstOrDefault(node => (node.GetValue(FrameworkElement.TagProperty) as PowerEntity)?.Id == line.FirstEnd);
                GeometryModel3D endNode = (GeometryModel3D)model3DGroup.Children.FirstOrDefault(node => (node.GetValue(FrameworkElement.TagProperty) as PowerEntity)?.Id == line.SecondEnd);

                if (startNode != null)
                {
                    startNode.Material = new DiffuseMaterial(Brushes.Purple);
                    highlightedEntities.Add(startNode);
                }
                if (endNode != null)
                {
                    endNode.Material = new DiffuseMaterial(Brushes.Purple);
                    highlightedEntities.Add(endNode);
                }
            }

            return HitTestResultBehavior.Stop;
        }

        private void CancelHighlight()
        {
            foreach (var model in highlightedEntities)
            {
                PowerEntity value = (PowerEntity)model.GetValue(FrameworkElement.TagProperty);
                model.Material = new DiffuseMaterial(GetColor(value));
            }
        }

        private Brush GetColor(PowerEntity powerEntity)
        {
            switch (powerEntity.TypeEntity)
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

        private void MouseRightButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            toolTip.IsOpen = false;
            Point mousePosition = e.GetPosition(viewport);
            Point3D testpoint3D = new Point3D(mousePosition.X, mousePosition.Y, 0);
            Vector3D testDirection = new Vector3D(mousePosition.X, mousePosition.Y, 10);

            PointHitTestParameters pointparams = new PointHitTestParameters(mousePosition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testDirection);

            VisualTreeHelper.HitTest(viewport, null, HTResult, pointparams);
        }
    }
}
