using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCollision : MonoBehaviour
{
    public AudioSource coinSound; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (pm != null)
            {
                Destroy(gameObject);
                if (coinSound != null) // ���������, ��� ����� �������� ��� ����������
                {
                    coinSound.mute = false;
                    coinSound.Play(); // ������������� ���� ������� �������
                }
                pm.MoveDisability(other);
                
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on the target object.");
            }
        }
    }
}
