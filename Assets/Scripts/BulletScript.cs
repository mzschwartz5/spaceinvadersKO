using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSourceType
{
    Player,
    Alien
}

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public BulletSourceType sourceType;
    public delegate void BulletDieHandler(GameObject bullet);
    public event BulletDieHandler OnBulletDie;
    bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    void Die()
    {
        isAlive = false;
        OnBulletDie?.Invoke(gameObject);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().isTrigger = false;
    }

    public void Fire()
    {
        isAlive = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().isTrigger = true;
    }

    /*
     * Bullets start as triggers. When they "die" they become colliders and fall to the ground / interact with physics.
     */
    void OnTriggerEnter(Collider collider)
    {
        if (!isAlive) return; // guard against double calls
        GameObject other = collider.gameObject;

        if (other.CompareTag(sourceType.ToString()) || other.CompareTag("Empty"))
        {
            return;
        }

        // If it collided with something that is not of the same source type, destroy the bullet.
        Die();
    }
}
