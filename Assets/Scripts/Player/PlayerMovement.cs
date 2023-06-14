using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace Sokoban
{
    
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _stepSpeed = 0.5f;
         public void Move(Vector2 direction, Action<bool> callback)
         {
            
             var newPosition = new Vector2(transform.position.x, transform.position.y ) + direction;
             
             bool moveIsFinished = true;
             transform.DOMove(newPosition, _stepSpeed).SetEase(Ease.Linear).OnComplete( () => callback(moveIsFinished));
         }
    }
}