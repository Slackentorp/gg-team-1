using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour, ITouchInput
{
    public void OnTap(Touch finger)
    {
        print("On tap");
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
  //      print("On touch down");
    }

    public void OnTouchUp(Touch finger)
    {
        print("On touch up");
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {
   //     print("On touch hold");
    }

    public void OnTouchExit()
    {
        print("On touch exit");
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {

        print("Swiped: " + direction);
        //   print("On touch swipe");
    }
}