using Microsoft.Xaml.Behaviors;
using System;
using System.ComponentModel;
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

        #region PositionY : double - Вертикальное положение

        public static readonly DependencyProperty PositionYProperty =
            DependencyProperty.Register(
                nameof(PositionY),
                typeof(Double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        [Description("Горизонтальное смещение")]
        public Double PositionY
        {
            get => (Double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }

        #endregion

        #region PositionX : double - Горизонтальное положение

        public static readonly DependencyProperty PositionXProperty =
            DependencyProperty.Register(
                nameof(PositionX),
                typeof(Double),
                typeof(DragInCanvas),
                new PropertyMetadata(default(double)));

        [Description("Горизонтальное смещение")]
        public Double PositionX 
        { 
            get => (Double)GetValue(PositionXProperty);
            set => SetValue(PositionXProperty, value);
        }

        #endregion

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

            PositionX = delta.X;
            PositionY = delta.Y;
        }
    }
}
