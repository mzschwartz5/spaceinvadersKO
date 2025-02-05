using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] float despawnPeriod;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnPeriod);
        Destroy(gameObject);
    }
}
