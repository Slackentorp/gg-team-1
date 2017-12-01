using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CinemachineSound {

public GameObject source;
public string wwiseEvent;

}


public class EasyWwiseCinemachineSounds : MonoBehaviour {


	public CinemachineSound[] sounds;


	public void playSound (int index)
	{
		AkSoundEngine.PostEvent( sounds[index].wwiseEvent, sounds[index].source);
		
	}


}
