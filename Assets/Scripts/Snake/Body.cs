using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Body : MonoBehaviour
{
    [SerializeField] float distanceBtwnBodyParts = 1.1f;
    public bool ignorePlayer = false;
    // Part of the snake that this Body is following
    public GameObject partToFollow;
    // Part of the snake that this Body is being followed by (if null is because is the last)
    public GameObject partBeingFollowedBy;
    // Position where partToFollow did a rotation change
    Transform positionToRotate;
    Queue<Transform> positionsToRotate = new Queue<Transform>();

    //enter the range to rotate when distance is <= to 1
    bool isInRange = false;
    //when entering the range the distance between current position and rotation position is above it
    bool isAboveTreshold = true;
    //treshold to tell the body now is time to rotate
    float treshold = 0.1f;

    public void MoveForward(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        LookForPositionToRotate();
        Rotate();
    }

    public void AddpositionToRotate(Transform positionToRotate)
    {
        positionsToRotate.Enqueue(positionToRotate);
    }

    public void PlaceBehindPartToFollow()
    {
        if (positionToRotate == null)
        {
            positionToRotate = partToFollow.transform;
        }
        //looking at the same way than the partToFollow
        transform.rotation = positionToRotate.rotation;
        //place this body part behind positionToRotate.forward
        transform.position = positionToRotate.position - positionToRotate.forward * distanceBtwnBodyParts;
        positionToRotate = null;
    }

    private void LookForPositionToRotate()
    {
        if (positionToRotate == null)
        {
            Transform newPositionToRotate;
            if (positionsToRotate.TryDequeue(out newPositionToRotate))
            {
                positionToRotate = newPositionToRotate;
            }
        }
    }

    private bool NeedToRotate()
    {
        float distanceRange = 0f;

        if (transform.forward == Vector3.left || transform.forward == Vector3.right)
        {
            distanceRange = MathF.Abs((transform.position.x - positionToRotate.position.x));
        }
        else if (transform.forward == Vector3.up || transform.forward == Vector3.down)
        {
            distanceRange = MathF.Abs((transform.position.y - positionToRotate.position.y));
        }
        else if (transform.forward == Vector3.forward || transform.forward == Vector3.back)
        {
            distanceRange = MathF.Abs((transform.position.z - positionToRotate.position.z));
        }

        isInRange = distanceRange <= 1f;
        isAboveTreshold = distanceRange > treshold;
        return isInRange && !isAboveTreshold;
    }

    private void Rotate()
    {
        if (positionToRotate == null) return;

        if (NeedToRotate())
        {
            NotifyRotation();
            PlaceBehindPartToFollow();
            isAboveTreshold = true;
        }
    }

    private void NotifyRotation()
    {
        if (partBeingFollowedBy)
        {
            partBeingFollowedBy.GetComponent<Body>().AddpositionToRotate(transform);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == Tags.Player && !ignorePlayer)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
