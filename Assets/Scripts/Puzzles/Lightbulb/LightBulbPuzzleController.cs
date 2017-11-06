using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class LightBulbPuzzleController : BasePuzzle
{

    public GameObject SolutionRack
    {
        private set { _solutionRack = value; }
        get { return _solutionRack; }
    }
    public string OnPickupWwiseEvent
    {
        private set { onPicupWwiseEvent = value; }
        get { return onPicupWwiseEvent; }
    }

    public string OnIncorrectPlacementWwiseEvent
    {
        private set { onIncorrectPlacementWwiseEvent = value; }
        get { return onIncorrectPlacementWwiseEvent; }
    }

    [SerializeField]
    private GameObject _solutionRack;

    [SerializeField]
    private float mercyDistance = 1;
    [SerializeField]
    private string onCorrectPlacementWwiseEvent;

    private string onPicupWwiseEvent, onIncorrectPlacementWwiseEvent;
    

    private LightbulbTouch[] lightbulbs;
    private readonly GameObject solutionRack;

    void OnEnable() {
        lightbulbs = GetComponentsInChildren<LightbulbTouch>();
        foreach (var bulb in lightbulbs) {
            bulb.controller = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (isSolved) {
	        return;
	    }

        int enabledBulbs = lightbulbs.Count(o => o != null);
	    if (enabledBulbs == 0)
        {
	        OnSolved();

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
}
