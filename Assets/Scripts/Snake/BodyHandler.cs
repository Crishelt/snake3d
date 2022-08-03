using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerMovement))]
public class BodyHandler : MonoBehaviour
{
    //variables

    [SerializeField] int bodyCount = 1;
    [SerializeField] Body body;

    List<Body> bodyParts = new List<Body>();
    GameObject emptyObjContainer;
    PlayerMovement playerMovement;

    //public getters setters
    public int BodyCount { get { return bodyCount; } }

    //events
    public delegate void OnBodyCountChange(int bodyCount);
    public static event OnBodyCountChange onBodyCountChange;

    // Start is called before the first frame update
    void Start()
    {
        emptyObjContainer = new GameObject("SnakeBodyParts");
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSnakeLength();
        MoveBodyParts();
    }

    public void PointEated()
    {
        bodyCount++;
        SpawnBodyPart();
        onBodyCountChange(bodyCount);
    }

    private void SpawnBodyPart()
    {
        Body newBodyPart = Instantiate(body, emptyObjContainer.transform);
        Body lastBodyPart = bodyParts.LastOrDefault();

        newBodyPart.name = $"Body {bodyParts.Count}";

        if (lastBodyPart)
        {
            newBodyPart.partToFollow = lastBodyPart.gameObject;
            lastBodyPart.partBeingFollowedBy = newBodyPart.gameObject;
            newBodyPart.PlaceBehindPartToFollow();
        }
        else
        {
            //the first Body has to follow the head
            newBodyPart.partToFollow = gameObject;
            newBodyPart.ignorePlayer = true;
            newBodyPart.PlaceBehindPartToFollow();
        }

        bodyParts.Add(newBodyPart);
    }


    private void MoveBodyParts()
    {
        for (int i = 0; i < bodyCount; i++)
        {
            bodyParts[i].MoveForward(playerMovement.MovementSpeed);
        }
    }

    private void CheckSnakeLength()
    {
        if (bodyCount > bodyParts.Count)
        {
            while (bodyCount != bodyParts.Count)
            {
                SpawnBodyPart();
            }
        }
    }

    private void HeadChangedRotation()
    {
        if (bodyParts.Count > 0)
        {
            bodyParts.First().AddpositionToRotate(transform);
        }
    }
}
