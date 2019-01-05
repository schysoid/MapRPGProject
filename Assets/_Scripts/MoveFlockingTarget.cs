using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlockingTarget : MonoBehaviour {

    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float ChangeDestInterval=5;
    public float SmoothLerpSteps = 50;
    public float maxSpeed;

    private float startTime;
    private Vector2 originPos;
    private Vector2 destPos;
    private float deltaTime;
    private Vector2 currentVelocity;

    // Use this for initialization
    void Start () {
        MinX = transform.position.x - 1;
        MaxX = transform.position.x + 1;
        MinY = transform.position.y - 1;
        MaxY = transform.position.y + 1;

        startTime = Time.time;
        originPos = transform.position;



        InvokeRepeating("ChangeDestination", 0, ChangeDestInterval);
        InvokeRepeating("SmoothLerp", 0, ChangeDestInterval/ SmoothLerpSteps);
    }

    void SmoothLerp() {
        float t = (Time.time - startTime) / ChangeDestInterval;
        transform.position = Vector2.SmoothDamp(transform.position,  destPos, ref currentVelocity, ChangeDestInterval,  maxSpeed = Mathf.Infinity,  deltaTime = Time.deltaTime);

    }

    private void ChangeDestination()
    {
        startTime = Time.time;
        originPos = transform.position;
            float randX = Mathf.Clamp( Random.value - 0.5f + transform.position.x,MinX,MaxX);
            float randY = Mathf.Clamp(Random.value - 0.5f + transform.position.y,MinY,MaxY);
            destPos = new Vector2(randX, randY);

    }
}
