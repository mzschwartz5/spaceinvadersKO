using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float speed = 5f;
    private BulletPool bulletPool;
    private Transform bulletSpawnTransform;
    private Vector3 originalPos;

    void Start()
    {
        bulletPool = GetComponentInChildren<BulletPool>();
        bulletSpawnTransform = transform.Find("BulletSpawn");
        originalPos = transform.position;
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletPool.FireBullet(bulletSpawnTransform.position);
        }
    }

    void Die()
    {
        // Reinstantiate at starting position
        transform.position = originalPos;
        GameController.LoseLife();
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;

        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        BulletScript bulletScript = other.GetComponent<BulletScript>();
        if (bulletScript.sourceType == BulletSourceType.Player)
        {
            return;
        }

        Die();
    }
}
