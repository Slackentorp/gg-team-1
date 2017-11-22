using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubStylizer : MonoBehaviour {
    private bool firstTry = false;
    void OnEnable()
    {
        SubToolXML.OnShowSubs += ChangeRotation;
    }
    void OnDisable()
    {
        SubToolXML.OnShowSubs -= ChangeRotation;
    }
	// Update is called once per frame
	void ChangeRotation () {
        float min = 0f;
        float max = 5f;
        float random = Random.Range(min, max);
      
       
        if(firstTry == true)
        { 
            transform.Rotate(0 ,0 , random);
            firstTry = false;
        }
        else
        {
            transform.Rotate(0, 0, -random);
            firstTry = true;
        }
    }
}
    