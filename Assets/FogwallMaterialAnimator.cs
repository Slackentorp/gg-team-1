using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogwallMaterialAnimator : MonoBehaviour {


	Material myMaterial;

	[Range(0.0f, 1.0f)]
	public float dissolveAmount;

	public string fogwallObjectName;


	// Use this for initialization
	void Start ()
	{
		myMaterial = GameObject.Find(fogwallObjectName).GetComponent<Renderer>().material;
		GameObject.Find(fogwallObjectName).GetComponent<Renderer>().material = myMaterial;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(myMaterial != null)
		{
			myMaterial.SetFloat("_DissolveAmount", dissolveAmount);
		}
		
	}


}
