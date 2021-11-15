using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlatformController : MonoBehaviour
{
    [Header("Movement")] 
    public GameObject platform;
    public List<Transform> pathList;
    public float speed;
    public float distanceOffset;
    public bool isLooping;

    private bool isMoving;
    private float timer;
    private int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        timer = 0.0f;
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformAlongPath();

        if (isLooping)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            UpdateTimer();
        }
    }

    private void MovePlatformAlongPath()
    {
        var nextPoint = currentPoint + 1;
        if (nextPoint == pathList.Count)
        {
            nextPoint = 0;
        }

        var newPosition = Vector3.MoveTowards(pathList[currentPoint].position, pathList[nextPoint].position, timer * speed);

        if ((!isLooping) && (currentPoint == pathList.Count - 1))
        {
            isMoving = false;
        }

        var difference = pathList[nextPoint].position - newPosition;

        if (difference.magnitude <= distanceOffset )
        {
            currentPoint++;
            timer = 0;
            if (currentPoint == pathList.Count)
            {
                currentPoint = 0;
            }
        }

        platform.transform.position = newPosition;
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}
