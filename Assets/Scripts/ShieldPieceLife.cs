using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPieceLife : MonoBehaviour
{
    private int degradationLevel = 4;

    private void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        degradationLevel--;
        if (degradationLevel <= 0)
        {
            Destroy(gameObject);
        }
    }
}
