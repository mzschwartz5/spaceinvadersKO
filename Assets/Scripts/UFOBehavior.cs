using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector3 direction;
    private int points;
    void Start()
    {
        points = Random.Range(2, 6) * 50;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GameController.AddScore(points);
            Destroy(gameObject);
        }
    }
}
