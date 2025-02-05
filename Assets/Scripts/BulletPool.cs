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
            bullet.layer = (sourceType == BulletSourceType.Player) ? LayerMask.NameToLayer("PlayerBullet") : LayerMask.NameToLayer("Default");
            bullet.SetActive(false);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.sourceType = sourceType;
            bulletScript.OnBulletDie += ReturnBullet;
            bulletPool.Enqueue(bullet);
        }
    }

    public bool FireBullet(Vector3 spawnPos, Quaternion rotation)
    {
        if (bulletPool.Count <= 0) return false;

        GameObject bullet = bulletPool.Dequeue();
        if (bullet == null) return false;

        bullet.SetActive(true);
        bullet.transform.position = spawnPos;
        bullet.transform.rotation = rotation;
        bullet.GetComponent<BulletScript>().Fire();
        return true;
    }

    private void ReturnBullet(GameObject bullet)
    {
        bulletPool.Enqueue(bullet);
    }

}
