using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Alien")
        {
            GameController.GameOver();
        }
    }
}
