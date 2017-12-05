using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogwallMaterialAnimator : MonoBehaviour {


	Material myMaterial;

	[Range(0.0f, 1.0f)]
	public float dissolveAmount;


	// Use this for initialization
	void Start () {

	myMaterial = GetComponent<Renderer>().material;
	GetComponent<Renderer>().material = myMaterial;
		
	}
	
	// Update is called once per frame
	void Update () {

		myMaterial.SetFloat("_DissolveAmount", dissolveAmount);
		
	}


}
