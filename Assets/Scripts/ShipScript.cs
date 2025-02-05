using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    public float speed = 5f;
    private BulletPool bulletPool;
    private Transform bulletSpawnTransform;
    private Vector3 originalPos;
    private Vector3 cameraOriginalPos;
    private AudioSource shootAudio;
    private AudioSource dieAudio;
    private AudioSource shieldSound;
    void Start()
    {
        bulletPool = GetComponentInChildren<BulletPool>();
        bulletSpawnTransform = transform.Find("BulletSpawn");
        originalPos = transform.position;
        cameraOriginalPos = Camera.main.transform.position;
        shootAudio = GetComponents<AudioSource>()[0];
        dieAudio = GetComponents<AudioSource>()[1];
        shieldSound = GetComponents<AudioSource>()[2];
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bulletPool.FireBullet(bulletSpawnTransform.position, bulletSpawnTransform.rotation))
            {
                shootAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Camera.main.orthographic = !Camera.main.orthographic;

            if (Camera.main.orthographic)
            {
                Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0); // Forward direction is -y axis
                Camera.main.transform.SetParent(null); // Unparent the camera
                Camera.main.transform.position = cameraOriginalPos; // Reset to original position
            }
            else
            {
                Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0); // Forward direction is +z axis
                Camera.main.transform.SetParent(transform); // Parent the camera to the ship
                Camera.main.transform.localPosition = new Vector3(0, 5, -2); // Set the camera behind the ship
                Vector3 lookAtPoint = transform.position + transform.forward * 10;
                Camera.main.transform.LookAt(lookAtPoint); // Look at the ship
            }
        }
    }

    void Die()
    {
        // Reinstantiate at starting position
        transform.position = originalPos;
        GameController.LoseLife();
        dieAudio.Play();
        GetComponent<ParticleSystem>().Play();
    }

    IEnumerator ActivateShield(float shieldDuration)
    {
        Transform shield = transform.Find("PlayerShield");
        shield.gameObject.SetActive(true);

        yield return new WaitForSeconds(shieldDuration);

        shield.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject other = collider.gameObject;

        if (other.CompareTag("powerup"))
        {
            float duration = other.GetComponent<ShieldController>().duration;
            StartCoroutine(ActivateShield(duration));
            shieldSound.Play();
            return;
        }

        if (!other.CompareTag("Bullet"))
        {
            return;
        }

        if (transform.Find("PlayerShield").gameObject.activeSelf)
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
