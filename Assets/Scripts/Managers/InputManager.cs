using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Manager class that watches and keeps track of touch events and sends commands to the appropiate objects
    /// when OnTouchDown, OnTouchHold, OnTouchUp, OnTap, and swipe is being detected.
    /// </summary>
    public class InputManager
    {
        public static bool isTouchingObject;
        public static bool pauseShown;

        private readonly LayerMask touchInputMask;
        private readonly float tapTimeThreshold = 1f;
        private readonly float swipeSquaredDistanceThreshold = 1f;
        private readonly float swipeStraightness = 1f;
        private readonly float swipeTimeThreshold = 1f;

        private readonly Camera mainCamera;
        private readonly Dictionary<GameObject, TouchState> objectOnTouchDownState = new Dictionary<GameObject, TouchState>();

        private Vector2 lastMousePos;
        private string debugLastInput;

        // Use this for initialization

        public InputManager(InputHandlerSettings settings, Camera camera)
        {
            mainCamera = camera;
            touchInputMask = settings._TouchInputMask;
            tapTimeThreshold = settings._TapTimeThreshold;
            swipeSquaredDistanceThreshold =
                settings._SwipeSquaredDistanceThreshold;
            swipeStraightness = settings._SwipeStraightness;
            swipeTimeThreshold = settings._SwipeTimeThreshold;
        }

        // Update is called once per frame
        public InputEvent CheckInput()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isRemoteConnected)
            {
                return CheckTouch();
            }
            else
            {
                return CheckMouse();
            }
#else
            return CheckTouch();
#endif
        }

        public void OnGUI()
        {
            GUI.contentColor = Color.cyan;
            GUI.Label(new Rect(0, 0, 200, 200), debugLastInput);
        }

        InputEvent CheckTouch()
        {
            isTouchingObject = false;
            InputEvent ie = new InputEvent();
            if(pauseShown)
            {
                return ie;
            }

            foreach (Touch t in Input.touches)
            {
                Ray ray = mainCamera.ScreenPointToRay(t.position);
                RaycastHit hit;

                                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject touchObject = hit.transform.gameObject;
                    ie.GameObject = touchObject;
                    ie.TouchPosition = hit.point;
                    ie.RaycastHit = hit;

                    TouchState ts;
                    objectOnTouchDownState.TryGetValue(touchObject,
                        out ts);

                    // Tap
                    if (t.phase == TouchPhase.Ended && ts.onTouchTime > 0 &&
                        Time.time - ts.onTouchTime <= tapTimeThreshold)
                    {
                        ie.InputType = InputType.TAP;
                        objectOnTouchDownState.Remove(touchObject);
                    }
                    else
                    {
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                TouchState tmp;
                                if (!objectOnTouchDownState.TryGetValue(
                                        touchObject, out tmp))
                                {
                                    ie.InputType = InputType.TOUCH_DOWN;
                                    objectOnTouchDownState.Add(touchObject,
                                        new TouchState(Time.time, t));
                                }
                                else
                                {
                                    RefreshDictionary(touchObject);
                                }
                                break;
                            case TouchPhase.Moved:
                                ie.InputType = InputType.TOUCH_HOLD;
                                break;
                            case TouchPhase.Ended:
                                ie.InputType = HandleOnTouchExit(touchObject, new TouchState(Time.time, t));
                                break;
                            case TouchPhase.Stationary:
                                ie.InputType = InputType.TOUCH_HOLD;
                                break;
                            case TouchPhase.Canceled:
                                ie.InputType = HandleOnTouchExit(touchObject, new TouchState(Time.time, t));
                                break;
                        }
                    }
                }
            }

            return ie;
        }

        InputEvent CheckMouse()
        {
            isTouchingObject = false;
            InputEvent ie = new InputEvent();
            if(pauseShown)
            {
                return ie;
            }

            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonUp(0))
            {
                Touch t = new Touch
                {
                position = Input.mousePosition
                };
                if (Input.GetMouseButtonDown(0))
                {
                    t.phase = TouchPhase.Began;
                    lastMousePos = Input.mousePosition.To2DXY();
                }
                else if (Input.GetMouseButton(0))
                {
                    t.phase = TouchPhase.Stationary;
                    t.deltaPosition =
                        lastMousePos - Input.mousePosition.To2DXY();
                    lastMousePos = Input.mousePosition.To2DXY();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    t.phase = TouchPhase.Ended;
                    t.deltaPosition =
                        lastMousePos - Input.mousePosition.To2DXY();
                }

                if (EventSystem.current.IsPointerOverGameObject())
                {
                    ie.GameObject = null;
                    return ie;
                }

                Ray ray = mainCamera.ScreenPointToRay(t.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject touchObject = hit.transform.gameObject;
                    ie.GameObject = touchObject;
                    ie.TouchPosition = hit.point;
                    ie.RaycastHit = hit;

                    TouchState ts;
                    objectOnTouchDownState.TryGetValue(touchObject,
                        out ts);

                    // Tap
                    if (t.phase == TouchPhase.Ended && ts.onTouchTime > 0 &&
                        Time.time - ts.onTouchTime <= tapTimeThreshold)
                    {
                        ie.InputType = InputType.TAP;
                        objectOnTouchDownState.Remove(touchObject);
                    }
                    else
                    {
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                TouchState tmp;
                                if (!objectOnTouchDownState.TryGetValue(
                                        touchObject, out tmp))
                                {
                                    ie.InputType = InputType.TOUCH_DOWN;
                                    objectOnTouchDownState.Add(touchObject,
                                        new TouchState(Time.time, t));
                                }
                                else
                                {
                                    RefreshDictionary(touchObject);
                                }
                                break;
                            case TouchPhase.Moved:
                                ie.InputType = InputType.TOUCH_HOLD;
                                break;
                            case TouchPhase.Ended:
                                ie.InputType = HandleOnTouchExit(touchObject, new TouchState(Time.time, t));
                                break;
                            case TouchPhase.Stationary:
                                ie.InputType = InputType.TOUCH_HOLD;
                                break;
                            case TouchPhase.Canceled:
                                ie.InputType = HandleOnTouchExit(touchObject, new TouchState(Time.time, t));
                                break;
                        }
                    }
                }
            }

            return ie;
        }

        InputType HandleOnTouchExit(GameObject touchObject, TouchState touchState)
        {
            TouchState onTapState;
            if (!objectOnTouchDownState.TryGetValue(touchObject, out onTapState))
            {
                objectOnTouchDownState.Remove(touchObject);
            }
            objectOnTouchDownState.Clear();
            if (touchState.onTouchObject.phase == TouchPhase.Ended)
            {
                return InputType.TOUCH_UP;
            }
            else
            {
                return InputType.TOUCH_EXIT;
            }
        }

        public void RefreshDictionary(GameObject go)
        {
            objectOnTouchDownState.Remove(go);
        }

        public struct TouchState
        {
            public float onTouchTime;
            public Touch onTouchObject;

            public TouchState(float onTouchTime, Touch onTouchObject)
            {
                this.onTouchTime = onTouchTime;
                this.onTouchObject = onTouchObject;
            }
        }

        /// <summary>
        /// Used to account for different screen DPI
        /// </summary>
        /// <param name="input">The touch to correct</param>
        /// <returns>Normalized touch deltaPosition</returns>
        public static Vector2 FixTouchDelta(Touch input)
        {
            float dt = Time.deltaTime / input.deltaTime;
            if (float.IsNaN(dt) || float.IsInfinity(dt))
            {
                dt = 1.0f;
            }
            return input.deltaPosition * dt;
        }

        public bool GetHeadsetState()
        {
#if UNITY_EDITOR
            return false;
#endif
#if UNITY_ANDROID
            using(var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using(var context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            using(var AudioManager = context.Call<AndroidJavaObject>("getSystemService", "audio"))
            {

                if (AudioManager != null)
                {
                    return AudioManager.Call<bool>("isWiredHeadsetOn");
                }
            }
#endif
            return false;
        }
    }

    public enum InputType
    {
        TOUCH_DOWN,
        TOUCH_UP,
        TOUCH_HOLD,
        TOUCH_EXIT,
        TAP,
        SWIPE
    }
    public struct InputEvent
    {
        public GameObject GameObject;
        public InputType InputType;
        public TouchDirection TouchDirection;
        public Vector3 TouchPosition;
        public RaycastHit RaycastHit;

        public override string ToString()
        {
            return string.Format("GameObject: {0} - InputType: {1} - TouchDirection: {2} - TouchPosition: {3}", GameObject, InputType, TouchDirection, TouchPosition);
        }
    }
}