using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FogwallMaterialAnimator : MonoBehaviour
{
	[Range(0.0f, 1.0f)]
	public float dissolveAmount;
	[SerializeField]
	private string fogwallObjectName;

	private Material myMaterial;
	private int dissolveAmountID;

	void OnEnable()
	{
		SceneManager.sceneLoaded += HandleApartmentLoad;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= HandleApartmentLoad;
	}

	void HandleApartmentLoad(Scene scene, LoadSceneMode mode){
		Scene appartment = SceneManager.GetSceneByName("Apartment");
		if(scene == appartment)
		{
			Start();
		}
	}

	// Use this for initialization
	void Start ()
	{
		dissolveAmountID = Shader.PropertyToID("_DissolveAmount");
		GameObject parent = GameObject.Find("FogWalls");
		if(parent == null) return;
		foreach (Transform item in parent.transform)
		{
			if(item.GetChild(0).gameObject.name == fogwallObjectName)
			{
				myMaterial = item.GetChild(0).gameObject.GetComponent<Renderer>().material;
				item.GetChild(0).gameObject.GetComponent<Renderer>().material = myMaterial;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(myMaterial != null)
		{
			myMaterial.SetFloat(dissolveAmountID, dissolveAmount);
		}
	}
}
