using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] int rows;
    [SerializeField] int columns;
    [SerializeField] GameObject smallAlienPrefab;
    [SerializeField] GameObject mediumAlienPrefab;
    [SerializeField] GameObject largeAlienPrefab;
    AudioSource deathAudio;

    void Start()
    {
        deathAudio = GetComponent<AudioSource>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Vector3 min = boxCollider.bounds.min;
        Vector3 max = boxCollider.bounds.max;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject alien;
                if (i < 2)
                {

                    alien = Instantiate(smallAlienPrefab, transform);
                }
                else if (i < 4)
                {
                    alien = Instantiate(mediumAlienPrefab, transform);
                }
                else
                {
                    alien = Instantiate(largeAlienPrefab, transform);
                }

                float xPos = Mathf.Lerp(min.x, max.x, (float)j / (columns - 1));
                float zPos = Mathf.Lerp(min.z, max.z, (float)i / (rows - 1));
                alien.transform.position = new Vector3(xPos, transform.position.y, zPos);
            }
        }
    }

    // Put on the spawner because once the aliens are destroyed on death, they can't play the sound.
    public void PlayDeathSound()
    {
        deathAudio.Play();
    }
}
