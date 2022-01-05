using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace WpfApp.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {    
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            var handlers = PropertyChanged;
            if (handlers is null) return;

            var invokation_list = handlers.GetInvocationList();

            var arg = new PropertyChangedEventArgs(PropertyName);
            foreach (var action in invokation_list)
            {
                if (action.Target is DispatcherObject disp_object)
                {
                    disp_object.Dispatcher.Invoke(action, this, arg);
                }
                else
                    action.DynamicInvoke(this, arg);
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            var value_target_service = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_service = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            OnInitialized(
                value_target_service?.TargetObject,
                value_target_service?.TargetProperty,
                root_object_service?.RootObject
                );

            return this;
        }

        private WeakReference _TargetRef;
        private WeakReference _RootRef;

        public object TargetObject => _TargetRef.Target;

        public object RootObject => _RootRef.Target;

        protected virtual void OnInitialized(object Target, object Property ,object Root)
        {
            _TargetRef = new WeakReference(Target);
            _RootRef = new WeakReference(Root);
        }

        //~ViewModel()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) return;
            _disposed = true;
            // Освобождение управляемых ресурсов
        }
    }
}
