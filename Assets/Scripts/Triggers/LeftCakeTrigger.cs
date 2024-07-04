using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCakeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (pm != null)
            {
              //  pm.transform.position = Vector3.zero;
                pm.GotoSpawn();
          
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on the target object.");
            }
        }
    }
}
