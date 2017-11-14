using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour, ITouchInput
{
    public void OnTap()
    {
        print("On tap");
    }

    public void OnTouchDown(Vector3 worldPos)
    {
  //      print("On touch down");
    }

    public void OnTouchUp()
    {
        print("On touch up");
    }

    public void OnToucHold(Vector3 worldPos)
    {
   //     print("On touch hold");
    }

    public void OnTouchExit()
    {
        print("On touch exit");
    }

    public void OnSwipe(TouchDirection direction)
    {

        print("Swiped: " + direction);
        //   print("On touch swipe");
    }
}