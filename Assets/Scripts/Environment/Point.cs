using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Point : MonoBehaviour
{

    public void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        BodyHandler bodyHandler = other.gameObject.GetComponentInParent<BodyHandler>();
        if (bodyHandler)
        {
            bodyHandler.PointEated();
            gameObject.SetActive(false);
        }
    }

}
