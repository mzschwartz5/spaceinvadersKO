using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPieceLife : MonoBehaviour
{
    private int degradationLevel = 4;
    private Renderer shieldRenderer;

    void Start()
    {
        shieldRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        degradationLevel--;
        UpdateShieldColor();

        if (degradationLevel <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateShieldColor()
    {
        // Change color based on degradation level
        switch (degradationLevel)
        {
            case 3:
                shieldRenderer.material.color = Color.gray;
                break;
            case 2:
                shieldRenderer.material.color = Color.Lerp(Color.gray, Color.black, 0.5f); // darker gray
                break;
            case 1:
                shieldRenderer.material.color = Color.black;
                break;
            default:
                break;
        }
    }
}
