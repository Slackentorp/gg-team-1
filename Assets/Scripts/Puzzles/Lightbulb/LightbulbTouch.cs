using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LightbulbTouch : MonoBehaviour
{
    [HideInInspector]
    public LightBulbPuzzleController controller;
    
    public bool isCopy;
    public GameObject correctContact;

    [HideInInspector]
    public GameObject copyOf;

    [SerializeField]
    private float scaleConstant;
    private GameObject instantiatedCopy;
    public Vector3 startPos;
    private Quaternion startRotation;
    private Vector3 startSize;
    private GameObject solutionRack;
    private Vector3 distanceWorldPos;
    private BoxCollider boxCollider;

    public Vector3 rackUp, rackDirection, rackNormal;
    public bool isColliding;

    // Use this for initialization
    void Start ()
    {
        if (!isCopy)
        {
        //    pool = Instantiate(gameObject, new Vector3(0, -100, 0), Quaternion.identity);
            //pool.transform.parent = transform.parent;
          //  pool.GetComponent<LightbulbTouch>().isCopy = true;
        }
        
        boxCollider = GetComponent<BoxCollider>();
        startRotation = transform.rotation;
        startPos = transform.position;
        startSize = boxCollider.size;

        if (controller != null)
	    {
	        solutionRack = controller.SolutionRack;
	        controller.CheckForSolution(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (isCopy)
	    {
	        Debug.DrawLine(startPos, startPos + rackUp, Color.blue);
	        Debug.DrawLine(startPos, startPos + rackDirection.normalized, Color.green);
	        Debug.DrawLine(startPos, rackNormal, Color.red);
	        
	    }

	    /*if (controller != null) {
	        controller.CheckForSolution(this);
	    }*/

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RackWall"))
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("RackWall")) {
            isColliding = false;
        }
    }

    private ProjectedVectors ComputeProjectedPosition(Vector3 newPosition)
    {
        rackDirection = solutionRack.transform.position - startPos;
        Vector3 tmp = startPos;
        tmp.y = solutionRack.transform.position.y;
        tmp.x = 0;
        tmp.z = 0;
        rackUp = startPos + tmp;
        rackUp = Quaternion.AngleAxis(90, Vector3.right) * rackDirection;

        rackNormal = Quaternion.AngleAxis(90, Vector3.up) * rackUp;
        Plane pl = new Plane(startPos, solutionRack.transform.position, Vector3.Lerp(startPos, solutionRack.transform.position, .5f).WithY(0.25f));

        Vector3 projection = pl.ClosestPointOnPlane(newPosition);

        ProjectedVectors pv = new ProjectedVectors();
        pv.Projection = projection;
        pv.Direction = rackDirection;

        return pv;
    }

    private struct ProjectedVectors
    {
        public Vector3 Projection;
        public Vector3 Direction;
    }
}
