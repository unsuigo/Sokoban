using System;
using UnityEngine;

public interface IMovable
{
   public void Push(Vector2 direction, Action callback);

   public Transform Transform { get; }

}
