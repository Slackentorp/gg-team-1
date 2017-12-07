using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class TimelinePersistentState : MonoBehaviour
{
	public DoorWallController HallwayToKitchenFog, HallwayToLivingRoomFog, KitchenFog, KitchenToHallwayFog;
	DoorWallController[] dwcs;

	void OnEnable()
	{
		SceneManager.sceneLoaded += HandleApartmentLoad;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= HandleApartmentLoad;
	}

	void HandleApartmentLoad(Scene scene, LoadSceneMode mode){
		Scene appartment = SceneManager.GetSceneByName("Apartment");
		if(scene == appartment && (dwcs == null || dwcs.Length == 0))
		{
			Awake();
		}
	}

	private void Awake()
	{
		GameObject fogWalls = GameObject.Find("FogWalls");
		if(fogWalls == null) return;
		dwcs = fogWalls.GetComponentsInChildren<DoorWallController>(true);
		if(dwcs.Length == 0) return;
		HallwayToKitchenFog = dwcs.FirstOrDefault(d => d.fogIdentifier == "HallwayToKitchenFog");
		HallwayToLivingRoomFog = dwcs.FirstOrDefault(d => d.fogIdentifier == "HallwayToLivingRoomFog");
		KitchenFog = dwcs.FirstOrDefault(d => d.fogIdentifier == "KitchenFog");
		KitchenToHallwayFog = dwcs.FirstOrDefault(d => d.fogIdentifier == "KitchenToHallwayFog");
	}

	public void EnableHallwayToKitchenFog()
	{
		HallwayToKitchenFog.gameObject.SetActive(true);
	}
	public void EnableKitchenToHallwayFog()
	{
		KitchenToHallwayFog.gameObject.SetActive(true);
	}
	public void EnableHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.gameObject.SetActive(true);
	}
	public void EnableKitchenFog()
	{
		KitchenFog.gameObject.SetActive(true);
	}
	public void EnableAll()
	{
		foreach (var item in dwcs)
		{
			item.gameObject.SetActive(true);
		}
	}

	
	public void DisableHallwayToKitchenFog()
	{
		HallwayToKitchenFog.gameObject.SetActive(false);
	}
	public void DisableHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.gameObject.SetActive(false);
	}
	public void DisableKitchenToHallwayFog()
	{
		KitchenToHallwayFog.gameObject.SetActive(false);
	}
	public void DisableKitchenFog()
	{
		KitchenFog.gameObject.SetActive(false);
	}
	public void DisableAll()
	{
		foreach (var item in dwcs)
		{
			item.gameObject.SetActive(false);
		}
	}

	// Toggle
	public void ToogleHallwayToKitchenFog()
	{
		HallwayToKitchenFog.gameObject.SetActive(!HallwayToKitchenFog.gameObject.activeInHierarchy);
	}
	public void ToggleKitchenToHallwayFog()
	{
		KitchenToHallwayFog.gameObject.SetActive(!KitchenToHallwayFog.gameObject.activeInHierarchy);
	}
	public void ToogleHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.gameObject.SetActive(!HallwayToLivingRoomFog.gameObject.activeInHierarchy);
	}
	public void ToogleKitchenFog()
	{
		KitchenFog.gameObject.SetActive(!KitchenFog.gameObject.activeInHierarchy);
	}
	public void ToggleAll()
	{
		foreach (var item in dwcs)
		{
			item.gameObject.SetActive(!item.gameObject.activeInHierarchy);
		}
	}
}
