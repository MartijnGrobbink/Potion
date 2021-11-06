using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjection : MonoBehaviour
{
    PotionController potionController;
    LineRenderer lineRenderer;

    public int amountPoints = 50;

    public float timeBetweenPoints = 0.1f;

    public LayerMask CollisionLayer;
    // Start is called before the first frame update
    void Start()
    {
        potionController = GetComponent<PotionController>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = amountPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = potionController.ThrowPoint.position;
        Vector3 startingVelocity = potionController.ThrowPoint.up * potionController.throwPower;
        for (float i = 0; i < amountPoints; i += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + i * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * i + Physics.gravity.y/2f * i * i;
            points.Add(newPoint);

            if(Physics.OverlapSphere(newPoint, 2, CollisionLayer).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());
    }
}
