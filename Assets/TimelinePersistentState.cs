﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TimelinePersistentState : MonoBehaviour
{
	private GameObject HallwayToKitchenFog, HallwayToLivingRoomFog, KitchenFog, KitchenToHallwayFog;

	private void Awake()
	{
		DoorWallController[] dwcs = GameObject.FindObjectsOfType<DoorWallController>();
		HallwayToKitchenFog = dwcs.First(d => d.fogIdentifier == "HallwayToKitchenFog").gameObject;
		HallwayToLivingRoomFog = dwcs.First(d => d.fogIdentifier == "HallwayToLivingRoomFog").gameObject;
		KitchenFog = dwcs.First(d => d.fogIdentifier == "KitchenFog").gameObject;
		KitchenToHallwayFog = dwcs.First(d => d.fogIdentifier == "KitchenToHallwayFog").gameObject;
	}

	public void EnableHallwayToKitchenFog()
	{
		HallwayToKitchenFog.SetActive(true);
	}
	public void EnableKitchenToHallwayFog()
	{
		KitchenToHallwayFog.SetActive(true);
	}
	public void EnableHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.SetActive(true);
	}
	public void EnableKitchenFog()
	{
		KitchenFog.SetActive(true);
	}

	
	public void DisableHallwayToKitchenFog()
	{
		HallwayToKitchenFog.SetActive(false);
	}
	public void DisableHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.SetActive(false);
	}
	public void DisableKitchenToHallwayFog()
	{
		KitchenToHallwayFog.SetActive(false);
	}
	public void DisableKitchenFog()
	{
		KitchenFog.SetActive(false);
	}

	// Toggle
	public void ToogleHallwayToKitchenFog()
	{
		HallwayToKitchenFog.SetActive(!HallwayToKitchenFog.activeInHierarchy);
	}
	public void ToggleKitchenToHallwayFog()
	{
		KitchenToHallwayFog.SetActive(!KitchenToHallwayFog.activeInHierarchy);
	}
	public void ToogleHallwayToLivingRoomFog()
	{
		HallwayToLivingRoomFog.SetActive(!HallwayToLivingRoomFog.activeInHierarchy);
	}
	public void ToogleKitchenFog()
	{
		KitchenFog.SetActive(!KitchenFog.activeInHierarchy);
	}
}
