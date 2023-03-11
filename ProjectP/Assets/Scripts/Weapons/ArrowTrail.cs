using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float progress;

    [SerializeField]
    private float speed = 20f;

    void Start()
    {
        startPosition = transform.position.WithAxis(Axis.Z, -1);
    }

    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
    }

    public void SetTargetPosition(Vector3 targetPos) 
    {
        targetPosition = targetPos.WithAxis(Axis.Z, -1);
    }
}
