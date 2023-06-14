using UnityEngine;

public class PlayerEntryPoint : MonoBehaviour
{
   public Transform PlayerEntryPointTransform { get; private set; }

   private void Start()
   {
      PlayerEntryPointTransform = transform;
   }
}
