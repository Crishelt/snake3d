using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoundary : MonoBehaviour
{
    Environment environment;

    private void Start()
    {
        environment = FindObjectOfType<Environment>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Wall) return;

        if (other.transform.forward == transform.forward * -1)
        {
            TeleportTargetToOppositeWall(other.transform.parent.transform);
        }
    }

    private void TeleportTargetToOppositeWall(Transform target)
    {
        float distance = (Vector3.Distance(transform.position, Vector3.zero) * 1.95f);
        target.position = target.position + (transform.forward * distance);
    }

}
