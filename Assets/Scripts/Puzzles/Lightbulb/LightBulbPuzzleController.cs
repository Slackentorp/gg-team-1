using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class LightBulbPuzzleController : BasePuzzle
{

    public GameObject SolutionRack
    {
        private set { _solutionRack = value; }
        get { return _solutionRack; }
    }

    [SerializeField]
    private GameObject _solutionRack;

    [SerializeField]
    private float mercyDistance = 1;
    [SerializeField]
    private string onCorrectPlacementWwiseEvent;

    private LightbulbTouch[] lightbulbs;
    private Transform mainCamera;
    private Transform activeBulb;
    private Vector3 bulbStartPos;
    private Quaternion bulbStartRotation;
    private Plane activePlane;
    private LightbulbTouch activeLightBulbComponent;

    void OnEnable() {
        lightbulbs = GetComponentsInChildren<LightbulbTouch>();
        foreach (var bulb in lightbulbs) {
            bulb.controller = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update () {
	    if (isSolved) {
	        return;
	    }

        int enabledBulbs = lightbulbs.Count(o => o != null);
	    int correctBulbs = lightbulbs.Length - enabledBulbs;
	    AkSoundEngine.SetState("BULB_STATE", "BULB_" + correctBulbs);

	    if (enabledBulbs == 0)
        {
	        OnSolved();
        }

	    if (Input.GetMouseButtonDown(0))
	    {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
	        {
	            if (hit.transform.CompareTag("Bulb"))
	            {
	                activeBulb = hit.transform;
	                bulbStartPos = activeBulb.position;
	                bulbStartRotation = activeBulb.rotation;
                    activePlane = new Plane(bulbStartPos, SolutionRack.transform.position, Vector3.Lerp(bulbStartPos, SolutionRack.transform.position, .5f).WithY(0.25f));
	                CameraController.isMouseTouchingObject = true;
	                activeLightBulbComponent =
	                    activeBulb.GetComponent<LightbulbTouch>();
	                AkSoundEngine.PostEvent(onPickupWwiseEvent,
	                    activeBulb.gameObject);

	            }
	        }
	        // Raycast to bulb
            // If hit, set activeBulb to raycast.hit
	    }
	    if (activeLightBulbComponent == null)
	    {
	        activeBulb = null;
	    }
        // #Opkast
        if (Input.GetMouseButton(0) && activeBulb != null)
	    {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        float rayDistance;
	        if (activePlane.Raycast(ray, out rayDistance))
	        {
                Vector3 nextPos = ray.GetPoint(rayDistance);
                    activeBulb.position = nextPos;

	            float distanceTraveled = (transform.position - SolutionRack.transform.position).sqrMagnitude;
	            float travelLength = (bulbStartPos - SolutionRack.transform.position).sqrMagnitude;
	            float lerpVal = 1 - (distanceTraveled / travelLength);

	            activeBulb.rotation = Quaternion.Slerp(bulbStartRotation, Quaternion.LookRotation(SolutionRack.transform.position - bulbStartPos) * Quaternion.Euler(0, 135, 0), lerpVal);
	            CheckForSolution(activeLightBulbComponent);
            }

        }
	    if (Input.GetMouseButtonUp(0))
	    {
	        if (activeLightBulbComponent != null)
	        {
	            AkSoundEngine.PostEvent(onIncorrectPlacementWwiseEvent,
	                activeLightBulbComponent.gameObject);
            }
	        activeBulb = null;
	        CameraController.isMouseTouchingObject = false;
        }
    }

    public override void CheckForSolution(Component sender)
    {
        var lbt = sender as LightbulbTouch;
        if (lbt != null)
        {
            float distance = Vector3.SqrMagnitude(
                lbt.correctContact.transform.position -
                lbt.transform.position);
            if (distance <= mercyDistance)
            {
                lbt.transform.position = lbt.correctContact.transform.position;
                lbt.transform.rotation = Quaternion.Euler(0, 90, 0) *
                                         Quaternion.LookRotation(lbt
                                             .correctContact.transform
                                             .position);
                if (lbt.copyOf != null)
                {
                    Destroy(lbt.copyOf);
                }
                AkSoundEngine.PostEvent(onCorrectPlacementWwiseEvent,
                    lbt.gameObject);
                Collider col = lbt.gameObject.GetComponent<Collider>();
                Destroy(lbt);
                Destroy(col);
            } 
        }
    }

    private ProjectedVectors ComputeProjectedPosition(Vector3 newPosition) {
        Vector3 rackDirection = SolutionRack.transform.position - bulbStartPos;
        Vector3 rackUp = Quaternion.AngleAxis(90, Vector3.right) * rackDirection;

        Plane pl = new Plane(bulbStartPos, SolutionRack.transform.position, Vector3.Lerp(bulbStartPos, SolutionRack.transform.position, .5f).WithY(0.25f));

        Vector3 projection = pl.ClosestPointOnPlane(newPosition);

        ProjectedVectors pv = new ProjectedVectors();
        pv.Projection = projection;
        pv.Direction = rackDirection;

        return pv;
    }

    private struct ProjectedVectors {
        public Vector3 Projection;
        public Vector3 Direction;
    }

}
