using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    // Reference to bullet pool in parent object. Shared between all instances of AlienScript
    private BulletPool bulletPool;
    private Transform bulletSpawnTransform;

    void Start()
    {
        bulletPool = GetComponentInParent<BulletPool>();
        bulletSpawnTransform = transform.Find("BulletSpawn");
        StartCoroutine(PeriodicallyFireBullet());
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;

        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        BulletScript bulletScript = other.GetComponent<BulletScript>();
        if (bulletScript.sourceType == BulletSourceType.Alien)
        {
            return;
        }

        Die();
    }

    private IEnumerator PeriodicallyFireBullet()
    {
        while (true)
        {
            float waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(waitTime);

            bulletPool.FireBullet(bulletSpawnTransform.position);
        }
    }
}
