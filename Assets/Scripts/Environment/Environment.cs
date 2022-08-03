using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] float xSize = 100f;
    [SerializeField] float ySize = 100f;
    [SerializeField] float zSize = 100f;
    [SerializeField] PointsObjectPool pointsObjectPool;
    [SerializeField] WallBoundary wallBoundaryPrefab;

    public float XSize { get { return xSize; } }
    public float YSize { get { return ySize; } }
    public float ZSize { get { return zSize; } }

    GameObject pointsParent;
    GameObject wallsParent;
    public Transform PointsParentTransform { get { return pointsParent.transform; } }

    private void Awake()
    {
        //ensure environment at origin
        transform.position = Vector3.zero;
        CreateEmptyParents();
        Instantiate(pointsObjectPool, transform);
    }

    private void Start()
    {
        PlaceWallBoundarys();
    }

    private void CreateEmptyParents()
    {
        pointsParent = new GameObject("Points");
        wallsParent = new GameObject("WallsBoundaries");
        pointsParent.transform.parent = transform;
        wallsParent.transform.parent = transform;

    }

    private void PlaceWallBoundarys()
    {
        //Instantiate the walls
        WallBoundary wallLeft = Instantiate(wallBoundaryPrefab, wallsParent.transform);
        WallBoundary wallRight = Instantiate(wallBoundaryPrefab, wallsParent.transform);
        WallBoundary wallUp = Instantiate(wallBoundaryPrefab, wallsParent.transform);
        WallBoundary wallDown = Instantiate(wallBoundaryPrefab, wallsParent.transform);
        WallBoundary wallForward = Instantiate(wallBoundaryPrefab, wallsParent.transform);
        WallBoundary wallBackWard = Instantiate(wallBoundaryPrefab, wallsParent.transform);

        //Place them in the world
        wallLeft.transform.position = new Vector3(-xSize / 2f, 0f, 0f);
        wallRight.transform.position = new Vector3(xSize / 2f, 0f, 0f);
        wallUp.transform.position = new Vector3(0f, ySize / 2f, 0f);
        wallDown.transform.position = new Vector3(0f, -ySize / 2f, 0f);
        wallForward.transform.position = new Vector3(0f, 0f, zSize / 2);
        wallBackWard.transform.position = new Vector3(0f, 0f, -zSize / 2);

        //look at the center
        wallLeft.transform.LookAt(Vector3.zero);
        wallRight.transform.LookAt(Vector3.zero);
        wallUp.transform.LookAt(Vector3.zero);
        wallDown.transform.LookAt(Vector3.zero);
        wallForward.transform.LookAt(Vector3.zero);
        wallBackWard.transform.LookAt(Vector3.zero);

        //Scale depending on environment size
        wallLeft.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);
        wallRight.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);
        wallUp.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);
        wallDown.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);
        wallForward.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);
        wallBackWard.transform.localScale = new Vector3(xSize / 9f, ySize / 9f, 1f);

        //place names
        wallLeft.name = "wallLeft";
        wallRight.name = "wallRight";
        wallUp.name = "wallUp";
        wallDown.name = "wallDown";
        wallForward.name = "wallForward";
        wallBackWard.name = "wallBackWard";
    }

}
