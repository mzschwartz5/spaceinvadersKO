using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [HideInInspector] public bool isAlive = true;

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
        if (!isAlive) return;

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
        transform.parent.GetComponent<AlienSpawner>().PlayDeathSound();
        GameController.AddScore(pointValue);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
        isAlive = false;
        StopAllCoroutines();

        // Check if this is the last alien in the level (Destroy marks the object for deletion, but it is not immediately removed)
        if (transform.parent.childCount <= 1)
        {
            GameController.NextLevel();
        }

        transform.parent = null;
    }

    /**
     * Aliens start as triggers. When they "die" they become colliders and fall to the ground / interact with physics.
     */

    void OnTriggerEnter(Collider collider)
    {
        if (!isAlive) return;
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
        while (isAlive)
        {
            float waitTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(waitTime);

            bulletPool.FireBullet(bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        }
    }
}
