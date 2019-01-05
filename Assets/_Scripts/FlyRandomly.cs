using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRandomly : MonoBehaviour {

    public MoveFlockingTarget flockingTarget;
    public float maxDistanceFromTarget = 1;
    private Vector2 target;
    public float speed = 0.3f;
    private Vector2 prevTargetPos;
    private Vector2 currentTargetPos;
    private Vector2 currentVelocity;

    void Start() {

        prevTargetPos = new Vector2(flockingTarget.transform.position.x, flockingTarget.transform.position.y);
        InvokeRepeating("affectTarget", 0, Random.Range(1.5f,3));

    }

    void affectTarget() {
       
        float randX = Random.Range(-1*maxDistanceFromTarget,maxDistanceFromTarget) ;
        float randY = Random.Range(-1 * maxDistanceFromTarget, maxDistanceFromTarget);
        target = new Vector2(flockingTarget.transform.position.x+ randX, flockingTarget.transform.position.y+ randY);
    }
    private void Update()
    {
        currentTargetPos = new Vector2(flockingTarget.transform.position.x, flockingTarget.transform.position.y);
        if (prevTargetPos != currentTargetPos)
        {
            affectTarget();
            prevTargetPos = currentTargetPos;
        }

            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.SmoothDamp(transform.position, target, ref currentVelocity, speed,  Mathf.Infinity, Time.deltaTime);

        //transform.position = Vector2.Lerp(currentPos, target, speed * Time.deltaTime);
        currentPos = new Vector2(transform.position.x, transform.position.y);
    }

}
