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
    public Vector3 direction;
    public delegate void BulletDieHandler(GameObject bullet);
    public event BulletDieHandler OnBulletDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void Die()
    {
        OnBulletDie?.Invoke(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;
        if (other.CompareTag(sourceType.ToString()) || other.CompareTag("Empty"))
        {
            return;
        }

        // If it collided with something that is not of the same source type, destroy the bullet.
        Die();
    }
}
