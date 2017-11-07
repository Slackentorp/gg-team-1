using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapTouch : MonoBehaviour, ITouchInput
{
	[SerializeField]
	private WorldMap woldMap;

	public void OnSwipe(Touch finger, TouchDirection direction)
	{
	}

	public void OnTap(Touch finger)
	{
		woldMap.CheckSolution(gameObject);
	}

	public void OnTouchDown(Touch finger, Vector3 worldPos)
	{
	}

	public void OnTouchExit()
	{
	}

	public void OnToucHold(Touch finger, Vector3 worldPos)
	{
	}

	public void OnTouchUp(Touch finger)
	{
	}
}
