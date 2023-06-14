using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.Data
{
    public abstract class BaseData<T>:IObservable<T> where T : BaseData<T>
    {
        public event Action<T> OnChanged;
        public void SetChanged()
        {
            OnChanged?.Invoke((T) this);
        }
    }
}
