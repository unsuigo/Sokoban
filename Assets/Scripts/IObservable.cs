using System;
namespace Sokoban.Data
{
    public interface IObservable<out T>
    {
        event Action<T> OnChanged;
        void SetChanged();
    }
}