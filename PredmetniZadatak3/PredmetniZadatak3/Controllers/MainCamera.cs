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

        private bool isRotating = false;
        private Quaternion rotDelta;
        private Quaternion rotation;
        

        public MainCamera(Viewport3D viewport, ScaleTransform3D scaleTransform, AxisAngleRotation3D rotateX, AxisAngleRotation3D rotateY, TranslateTransform3D translateTransform, Window window)
        {
            this.viewport = viewport;
            this.scale = scaleTransform;
            this.rotateX = rotateX;
            this.rotateY = rotateY;
            this.translate = translateTransform;
            this.window = window;

            rotDelta = Quaternion.Identity;
            this.rotation = new Quaternion(0,0,0,1);

            InitManipulation();
            //this.rotationdelta = Quaternion.Identity;
            //this.rotation = new Quaternion(0, 0, 0, 1);            
        }

        private void InitManipulation()
        {
            viewport.MouseWheel += MouseWheelEvent;
            viewport.MouseLeftButtonDown += MouseLeftButtonDownEvent;
            viewport.MouseLeftButtonUp += MouseLeftButtonUpEvent;            
            viewport.MouseDown += MouseMiddleButtonDownEvent;
            viewport.MouseUp += MouseMiddleButtonUpEvent;
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

        private void MouseMiddleButtonDownEvent(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                isRotating = true;
                viewport.CaptureMouse();
                startPoint = e.GetPosition(window);

                mOffset.X = translate.OffsetX;
                mOffset.Y = translate.OffsetY;
            }
        }

        private void MouseMiddleButtonUpEvent(object sender, MouseEventArgs e)
        {
            if (isRotating)
            {
                rotation = rotDelta * rotation;
            }

            if (e.MiddleButton == MouseButtonState.Released)
            {
                isRotating = false;
                viewport.ReleaseMouseCapture();
            }
        }
        

        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (viewport.IsMouseCaptured)
            {
                Point endPoint = e.GetPosition(window);
                double offsetX = endPoint.X - startPoint.X;
                double offsetY = endPoint.Y - startPoint.Y;
                double w = window.Width;
                double h = window.Height;
                double translateX = (offsetX * 100) / w;
                double translateY = -(offsetY * 100) / h;
                
                if (isRotating)
                {
                    var angleY = (rotateY.Angle + -translateX) % 360;
                    var angleX = (rotateX.Angle + translateY) % 360;
                    if (-75 < angleY && angleY < 75)
                    {
                        rotateY.Angle = angleY;
                    }
                    if (-20 < angleX && angleX < 125)
                    {
                        rotateX.Angle = angleX;
                    }

                    startPoint = endPoint;
                }
                else
                {
                    translate.OffsetX = mOffset.X + (translateX / (100 * scale.ScaleX));
                    translate.OffsetY = mOffset.Y + (translateY / (100 * scale.ScaleX));
                    translate.OffsetZ = translate.OffsetZ;
                }
            }
        }
    }
}
