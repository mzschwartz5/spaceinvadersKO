using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCollisionDetector : MonoBehaviour
{
    private void Quit()
    {
        Application.Quit();

    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "AlienContainer")
        {
            GameController.GameOver();
        }
    }
}
