using UnityEngine;
using System.Collections;

public class ParallaxScroller : MonoBehaviour
{
    public float scrollSpeed = 0.9f;
  
    private Camera mainCamera;
    private Vector3 startPosition;
    private float diff;
    public float drift = 0;
    private float drifted;

    void Start()
    {
       
        startPosition = transform.position;
        mainCamera = Camera.main;
        diff = startPosition.x - mainCamera.transform.position.x;
    }

    void Update()
    {
        drifted += drift;
        Vector3 posX = new Vector3();
        posX.x = (  mainCamera.transform.position.x +diff) * scrollSpeed;
        transform.position = new Vector3(posX.x+drifted, startPosition.y, startPosition.z);
    }
}