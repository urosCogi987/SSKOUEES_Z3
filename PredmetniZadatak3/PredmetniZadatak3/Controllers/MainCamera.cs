using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace PredmetniZadatak3.Controllers
{
    public class MainCamera
    {
        Viewport3D viewport;
        ScaleTransform3D scale;
        AxisAngleRotation3D rotateX;
        AxisAngleRotation3D rotateY;
        TranslateTransform3D translate;
        Window window;

        private int zoomCurrent = 1;
        private int zoomMax = 20;
        private int zoomMin = -10;
        private Point startPoint = new Point();
        private Point mOffset = new Point();
        

        public MainCamera(Viewport3D viewport, ScaleTransform3D scaleTransform, AxisAngleRotation3D rotateX, AxisAngleRotation3D rotateY, TranslateTransform3D translateTransform, Window window)
        {
            this.viewport = viewport;
            this.scale = scaleTransform;
            this.rotateX = rotateX;
            this.rotateY = rotateY;
            this.translate = translateTransform;
            this.window = window;            
            //this.rotationdelta = Quaternion.Identity;
            //this.rotation = new Quaternion(0, 0, 0, 1);            
        }

        public void InitManipulation()
        {
            viewport.MouseWheel += MouseWheelEvent;
            viewport.MouseLeftButtonDown += MouseLeftButtonDownEvent;
            viewport.MouseLeftButtonUp += MouseLeftButtonUpEvent;            
            viewport.MouseDown += MouseAnyDownEvent;
            viewport.MouseUp += MouseAnyUpEvent;
            viewport.MouseMove += MouseMoveEvent;
        }

        private void MouseWheelEvent(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && zoomCurrent < zoomMax)
            {
                scale.ScaleX += 0.1;
                scale.ScaleY += 0.1;
                scale.ScaleZ += 0.1;

                zoomCurrent++;
            } else if (e.Delta <= 0 && zoomCurrent > zoomMin)
            {
                scale.ScaleX -= 0.1;
                scale.ScaleY -= 0.1;
                scale.ScaleZ -= 0.1;

                zoomCurrent--;
            }

            // WOOOOOOOOOOOOOOOOOT??

            scale.CenterX = 5;
            scale.CenterY = 5;
            scale.CenterZ = 0;
        }

        private void MouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            viewport.CaptureMouse();
            startPoint = e.GetPosition(window);

            mOffset.X = translate.OffsetX;
            mOffset.Y = translate.OffsetY;
        }

        private void MouseLeftButtonUpEvent(object sender, MouseButtonEventArgs e)
        {
            viewport.ReleaseMouseCapture();
        }

        private void MouseAnyDownEvent(object sender, MouseEventArgs e)
        {
            //if (e.MiddleButton == MouseButtonState.Pressed)
            //{
            //    viewport.CaptureMouse();
            //    startPoint = e.GetPosition(viewport);
            //}
        }

        private void MouseAnyUpEvent(object sender, MouseEventArgs e)
        {

        }
        

        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {

        }
    }
}
