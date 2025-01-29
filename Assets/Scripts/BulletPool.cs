using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize;
    [SerializeField] BulletSourceType sourceType;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    public void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.sourceType = sourceType;
            bulletScript.direction = transform.forward;
            bulletScript.OnBulletDie += ReturnBullet;
            bulletPool.Enqueue(bullet);
        }
    }

    public bool FireBullet(Vector3 spawnPos)
    {
        if (bulletPool.Count <= 0) return false;

        GameObject bullet = bulletPool.Dequeue();
        bullet.transform.position = spawnPos;
        bullet.SetActive(true);

        return true;
    }

    private void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

}
