using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Sokoban
{
    
    public class Chest : MonoBehaviour, IMovable
    {
        [SerializeField] private float _stepSpeed = 0.5f;

        public Transform Transform => transform;

        
        public void Push(Vector2 direction, Action callback)
        {
            if (Mathf.Abs(direction.x) < 0.5)
            {
                direction.x = 0;
            }
            else
            {
                direction.y = 0;
            }
         
            if (direction.x > 0.5)
            {
                direction.x = 1;
            }
         
            if (direction.y > 0.5)
            {
                direction.y = 1;
            }
         
            var newPosition = new Vector2(transform.position.x, transform.position.y ) + direction;
            transform.DOMove(newPosition, _stepSpeed).SetEase(Ease.Linear)
                .OnComplete( () => callback());
        }

        
    }

}
