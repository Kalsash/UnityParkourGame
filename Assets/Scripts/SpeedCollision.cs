using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (pm != null)
            {
                Destroy(gameObject);
                pm.AddSpeedAbility(other);
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on the target object.");
            }
        }
    }
}
