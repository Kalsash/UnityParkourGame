using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class StandOnFakeCube : MonoBehaviour
{
    private bool isTriggerActivated = false;
    private async void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject myObject = GameObject.Find("FakeCube");
            if (isTriggerActivated)
            {
                transform.position = new Vector3(-2.8f, -2.14f, 85.23f);

                myObject.transform.position = new Vector3(-2.8f, -2.14f, 85.23f);
                isTriggerActivated = false;
            }
            else
            {
                await Task.Delay(100);

                transform.position = new Vector3(0, 0, 80);

                myObject.transform.position = new Vector3(0, 0, 80);
                isTriggerActivated = true;
            }

        }
        
    }

}
