using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LightbulbTouch : MonoBehaviour, ITouchInput
{
    [HideInInspector]
    public LightBulbPuzzleController controller;
    
    public bool isCopy;
    public GameObject correctContact;

    [HideInInspector]
    public GameObject copyOf, pool;

    [SerializeField]
    private float scaleConstant;
    private GameObject instantiatedCopy;
    private Vector3 startPos;
    private Quaternion startRotation;
    private Vector3 startSize;
    private GameObject solutionRack;
    private Vector3 distanceWorldPos;
    private BoxCollider boxCollider;


    // Use this for initialization
    void Start ()
    {
        if (!isCopy)
        {
            pool = Instantiate(gameObject, new Vector3(0, -100, 0), Quaternion.identity);
            pool.GetComponent<LightbulbTouch>().isCopy = true;
        }

        boxCollider = GetComponent<BoxCollider>();
        

        if (controller != null)
	    {
	        solutionRack = controller.SolutionRack;
	        controller.CheckForSolution(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void OnTap(Touch finger)
    {
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
        distanceWorldPos = worldPos - transform.position;
        if (isCopy)
        {
            return;
        }
        if (controller != null)
        {
            controller.OnBeginSolving();
        }

        ProjectedVectors pv = ComputeProjectedPosition(worldPos);
        

        pool = poolInstantiate(pool, pv.Projection, transform.rotation);
        pool.GetComponent<LightbulbTouch>().copyOf = gameObject;
        AkSoundEngine.PostEvent(controller.OnPickupWwiseEvent, pool);

    }

    public void OnTouchUp(Touch finger)
    {
        if (isCopy)  
        {
            poolDestroy(gameObject);
            AkSoundEngine.PostEvent(controller.OnIncorrectPlacementWwiseEvent,
                gameObject);
        }
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {
        if (!isCopy)
        {
            return;
        }
        Vector3 newPosition = worldPos - distanceWorldPos;

        ProjectedVectors pv = ComputeProjectedPosition(newPosition);
        float distanceTraveled = (transform.position - solutionRack.transform.position).sqrMagnitude;
        float travelLength = (startPos - solutionRack.transform.position).sqrMagnitude;
        float lerpVal = 1 - (distanceTraveled / travelLength);

        transform.rotation = Quaternion.Slerp(startRotation, Quaternion.LookRotation(pv.Direction) * Quaternion.Euler(0,-90,0), lerpVal);
        transform.position = pv.Projection;

        // Scale collider by screen space
        boxCollider.size = startSize * (1 + lerpVal + scaleConstant);

        if (controller != null)
        {
            controller.CheckForSolution(this);
        }
    }

    public void OnTouchExit()
    {
        if (isCopy) {
           // Destroy(gameObject);
        }
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    private ProjectedVectors ComputeProjectedPosition(Vector3 newPosition)
    {
        Vector3 rackDirection = startPos - solutionRack.transform.position;
        Vector3 tmp = startPos;
        tmp.y = solutionRack.transform.position.y;
        tmp.x = 0;
        tmp.z = 0;
        Vector3 rackUp = startPos + tmp;

        Vector3 rackNormal = Vector3.Cross(rackDirection.normalized, rackUp.normalized);
        Vector3 projection = Vector3.ProjectOnPlane(newPosition, rackNormal.normalized);

        ProjectedVectors pv = new ProjectedVectors();
        pv.Projection = projection;
        pv.Direction = rackDirection;

        return pv;
    }

    GameObject poolInstantiate(GameObject gameobject, Vector3 position, Quaternion rotation)
    {
        gameobject.transform.position = position;
        gameobject.transform.rotation = rotation;

        LightbulbTouch lbt = gameobject.GetComponent<LightbulbTouch>();
        lbt.startRotation = rotation;
        lbt.startPos = position;
        lbt.startSize = boxCollider.size;

        return gameobject;
    }

    void poolDestroy(GameObject gameobject) {
        gameobject.transform.position = new Vector3(0,-100,0);
        gameobject.transform.rotation = Quaternion.identity;
        InputManager.Instance.RefreshDictionary(gameobject);
    }

    private struct ProjectedVectors
    {
        public Vector3 Projection;
        public Vector3 Direction;
    }
}
