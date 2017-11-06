using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
	[SerializeField]
	private GameObject[] lights;
	[SerializeField]
	private Color colorOff;
	[SerializeField]
	private Color colorOn;
	ITouchInput iTouchInput;
	private Renderer[] rend;
	private Dictionary<GameObject, int> dictionary = new Dictionary<GameObject, int>();
	private GameObject objectHit;
	private int lampsLit = 1;

	void Start()
	{
		rend = new Renderer[lights.Length];
		AddToDictionary();
		for (int i = 0; i < lights.Length; i++)
		{
			rend[i] = lights[i].GetComponent<Renderer>();
		}
		rend[0].material.color = colorOn;
	}

	public void CheckSolution(GameObject sender)
	{
		
		if (lampsLit < lights.Length)
		{
			objectHit = sender;
			CheckCorrectOrder(objectHit);
		}
		if (lampsLit == lights.Length)
		{
			Debug.Log("Puzzle Done");
		}
	}

	void AddToDictionary()
	{
		for (int i = 1; i < lights.Length; i++)
		{
			dictionary.Add(lights[i], i + 1);
		}
	}

	void CheckCorrectOrder(GameObject objClicked)
	{
		int value;
		if (dictionary.TryGetValue(objClicked, out value))
		{
			if (value != lampsLit+1)
			{
				lampsLit = 1;
				TurnColorsOff(lampsLit);
				rend[lampsLit - 1].material.color = colorOn;
			}
			else
			{
				lampsLit++;
				rend[lampsLit - 1].material.color = colorOn;
			}
		}
	}

	void TestExistingKey(GameObject objClicked, int value)
	{
		Debug.Assert(value != -1000);
	}

	void TurnColorsOff(int j)
	{
		foreach(Renderer r in rend)
		{
			r.material.color = colorOff;
		}
	}
}
