using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (pm != null)
            {
                Destroy(gameObject);
                pm.AddCrouchAbility(other);
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on the target object.");
            }
        }
    }
}
