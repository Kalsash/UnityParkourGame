using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCakeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject gameObject = GameObject.Find("FinalDoor");
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
