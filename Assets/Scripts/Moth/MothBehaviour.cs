using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothBehaviour
{
    [SerializeField]
    private AnimationCurve lerpCurve;
    [SerializeField]
    private float timeScale = 1;

	Transform transform;
	Camera camera;

    public float MothSpeed { get; set; }
    
    private bool inTransit;

    void OnValidate()
    {
        if (timeScale < 1)
        {
            timeScale = 1;
        }
    }

	public MothBehaviour(Transform transform, Camera camera, float speed)
	{
		this.transform = transform;
		this.camera = camera; 
		this.MothSpeed = speed; 
	}


	public void Update()
	{
		MothGoToPosition();
	}

	/*public void SetMothPosition(Vector3 position)
    {
        if (!inTransit)
        {
            inTransit = true;
            //IEnumerator lerp = SmoothLerpPosition(transform.gameObject, position);
            //StartCoroutine(lerp);
        }
    }*/


    IEnumerator SmoothLerpPosition(GameObject affect, Vector3 target)
    {
        Vector3 startPosition = affect.transform.position;
      // AkSoundEngine.PostEvent("MOTH_START_FLIGHT", gameObject);
        MothSpeed = 1f; 

        float time = 0;
        while (time < lerpCurve.keys[lerpCurve.length - 1].time)
        {
            time += Time.deltaTime / timeScale;
            affect.transform.position = Vector3.Lerp(startPosition, target,
                lerpCurve.Evaluate(time));
            yield return null;
        }

        MothSpeed = 0f;
       // AkSoundEngine.PostEvent("MOTH_END_FLIGHT", gameObject);
        inTransit = false;
    }

	private void MothGoToPosition()
	{
		MothSpeed = .4f;
		if (Input.GetMouseButton(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Vector3 mothToPoint = transform.position - hit.normal; 
				Debug.DrawRay(transform.position, mothToPoint, Color.red);
				Vector3.Lerp(transform.position, hit.normal, MothSpeed);
				
			}
		}
	}
}