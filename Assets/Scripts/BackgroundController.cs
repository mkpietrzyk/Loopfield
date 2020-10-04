using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
   public float backgroundTimer = 0f;
   private void FixedUpdate()
   {
      backgroundTimer += Time.deltaTime;
      transform.position = new Vector3(backgroundTimer, 0,0 );

      if (backgroundTimer > 32f)
      {
         backgroundTimer = -32;
      }
   }
}
