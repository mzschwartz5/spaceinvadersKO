using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMoveController : MonoBehaviour
{

    [SerializeField] float movePeriod;
    [SerializeField] float horizontalMoveInterval;
    [SerializeField] float verticalMoveInterval;
    [SerializeField] int horizontalMoveSteps;

    int currentHorizontalMoveStep = 0;
    float horizDirection = 1;
    float lastMoveTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastMoveTime < movePeriod)
        {
            return;
        }
            
        lastMoveTime = Time.time;

        if (currentHorizontalMoveStep >= horizontalMoveSteps)
        {
            currentHorizontalMoveStep = 0;
            horizDirection *= -1;
            transform.position += new Vector3(0, 0, -verticalMoveInterval);
        }
        else
        {
            transform.position += transform.right * (horizDirection * horizontalMoveInterval);
            currentHorizontalMoveStep++;
        }

    }
}
