using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfAppTest.Behaivors
{
    public class DragInCanvas : Behavior<UIElement>
    {
        private Point _StartPoint;
        private Canvas _Canvas;
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((_Canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null)
                return;

            _StartPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var obj = AssociatedObject;
            var current_pos = e.GetPosition(_Canvas);

            var delta = current_pos - _StartPoint;

            obj.SetValue(Canvas.LeftProperty, delta.X);
            obj.SetValue(Canvas.TopProperty, delta.Y);
        }
    }
}
