using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    [SerializeField] int pointValue;
    [SerializeField] float animationPeriod;
    [SerializeField] Mesh nextMesh;
    private Mesh currentMesh;
    private float lastMoveTime = 0;
    // Reference to bullet pool in parent object. Shared between all instances of AlienScript
    private BulletPool bulletPool;
    private Transform bulletSpawnTransform;
    private ParticleSystem particleSys;

    void Start()
    {
        currentMesh = GetComponent<MeshFilter>().mesh;
        bulletPool = GetComponentInParent<BulletPool>();
        bulletSpawnTransform = transform.Find("BulletSpawn");
        StartCoroutine(PeriodicallyFireBullet());
        particleSys = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time - lastMoveTime < animationPeriod)
        {
            return;
        }

        lastMoveTime = Time.time;
        GetComponent<MeshFilter>().mesh = nextMesh;
        nextMesh = currentMesh;
        currentMesh = GetComponent<MeshFilter>().mesh;
    }

    void Die()
    {
        particleSys.Play();
        Destroy(gameObject, particleSys.main.duration + particleSys.main.startLifetime.constant);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;

        transform.parent.GetComponent<AlienSpawner>().PlayDeathSound();
        GameController.AddScore(pointValue);

        // Check if this is the last alien in the level (Destroy marks the object for deletion, but it is not immediately removed)
        if (transform.parent.childCount <= 1)
        {
            GameController.NextLevel();
        }
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

            bulletPool.FireBullet(bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        }
    }
}
