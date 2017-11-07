using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

#pragma warning disable 649

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Manager class that watches and keeps track of touch events and sends commands to the appropiate objects
    /// when OnTouchDown, OnTouchHold, OnTouchUp, OnTap, and swipe is being detected.
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        public bool isTouchingObject;

        [SerializeField]
        private InputHandlerSettings settings;

        private LayerMask touchInputMask;
        private float tapTimeThreshold = 1f;
        private float swipeSquaredDistanceThreshold = 1f;
        private float swipeStraightness = 1f;
        private float swipeTimeThreshold = 1f;

        private Camera mainCamera;
        private readonly Dictionary<GameObject, TouchState> frameTouches = new Dictionary<GameObject, TouchState>();
        private readonly Dictionary<GameObject, TouchState> objectOnTouchDownState = new Dictionary<GameObject, TouchState>();

        private Vector2 lastMousePos;
        private string debugLastInput;

        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;
            touchInputMask = settings._TouchInputMask;
            tapTimeThreshold = settings._TapTimeThreshold;
            swipeSquaredDistanceThreshold =
                settings._SwipeSquaredDistanceThreshold;
            swipeStraightness = settings._SwipeStraightness;
            swipeTimeThreshold = settings._SwipeTimeThreshold;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isRemoteConnected)
            {
                CheckTouch();
            }
            else
            {
                CheckMouse();
            }
#else
        CheckTouch();
#endif
        }

        void OnGUI()
        {
            GUI.contentColor = Color.cyan;
            GUI.Label(new Rect(0, 0, 200, 200), debugLastInput);
        }

        void CheckTouch()
        {
            Dictionary<GameObject, TouchState> previousFrameTouches = new Dictionary<GameObject, TouchState>(frameTouches);
            frameTouches.Clear();
            isTouchingObject = false;

            foreach (Touch t in Input.touches)
            {
                Ray ray = mainCamera.ScreenPointToRay(t.position);
                RaycastHit hit;
               
                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    Debug.DrawLine(mainCamera.ScreenToWorldPoint(t.position), hit.point);
                    GameObject touchObject = hit.transform.gameObject;
                    ITouchInput touchInput =
                        touchObject.GetComponent<ITouchInput>();
                    if (touchInput == null)
                    {
                        continue;
                    }
                    isTouchingObject = true;

                    frameTouches[touchObject] = new TouchState(Time.time, t);

                    TouchState ts;
                    objectOnTouchDownState.TryGetValue(touchObject,
                        out ts);

                    // Tap
                    if (t.phase == TouchPhase.Ended && ts.onTouchTime > 0 &&
                        Time.time - ts.onTouchTime <= tapTimeThreshold)
                    {
                        touchInput.OnTap(t);
                        debugLastInput = "Tap";
                        objectOnTouchDownState.Remove(touchObject);
                    }
                    else
                    {
                        switch (t.phase) {
                            case TouchPhase.Began:
                                debugLastInput = "Down";
                                touchInput.OnTouchDown(t, hit.point);
                                objectOnTouchDownState[touchObject] = new TouchState(Time.time, t);
                                break;
                            case TouchPhase.Moved:
                                debugLastInput = "Hold";
                                touchInput.OnToucHold(t, hit.point);
                                break;
                            case TouchPhase.Ended:
                                debugLastInput = "Ended";
                                HandleOnTouchExit(touchObject, touchInput, new TouchState(Time.time, t));
                                break;
                            case TouchPhase.Stationary:
                                debugLastInput = "Hold";
                                touchInput.OnToucHold(t, hit.point);
                                break;
                            case TouchPhase.Canceled:
                                debugLastInput = "Exit";
                                HandleOnTouchExit(touchObject, touchInput, new TouchState(Time.time, t));
                                break;
                        }
                    }
                }
            }

            // Check whether more objects were touched in the previous frame
            // indicating that a touch was canceled
            foreach (var oldObject in previousFrameTouches)
            {
                TouchState ts;
                if (!frameTouches.TryGetValue(oldObject.Key, out ts))
                {
                    // On Touch up should always have been called at this point, but the object still lingers
                    if (oldObject.Value.onTouchObject.phase != TouchPhase.Ended)
                    {
                        HandleOnTouchExit(oldObject.Key,
                            oldObject.Key.GetComponent<ITouchInput>(),
                            oldObject.Value);
                    }
                }
            }
        }

        void CheckMouse()
        {
            Dictionary<GameObject, TouchState> previousFrameTouches =
                new Dictionary<GameObject, TouchState>(frameTouches);
            frameTouches.Clear();
            isTouchingObject = false;

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

                Ray ray = mainCamera.ScreenPointToRay(t.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject touchObject = hit.transform.gameObject;
                    ITouchInput touchInput =
                        touchObject.GetComponent<ITouchInput>();
                    if (touchInput == null)
                    {
                        return;
                    }
                    isTouchingObject = true;


                    frameTouches.Add(touchObject, new TouchState(Time.time, t));

                    TouchState ts;
                    objectOnTouchDownState.TryGetValue(touchObject,
                        out ts);

                    // Tap
                    if (t.phase == TouchPhase.Ended && ts.onTouchTime > 0 &&
                        Time.time - ts.onTouchTime <= tapTimeThreshold)
                    {
                        touchInput.OnTap(t);
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
                                    touchInput.OnTouchDown(t, hit.point);
                                    objectOnTouchDownState.Add(touchObject,
                                        new TouchState(Time.time, t));
                                }
                                else
                                {
                                    RefreshDictionary(touchObject);
                                }
                                break;
                            case TouchPhase.Moved:
                                touchInput.OnToucHold(t, hit.point);
                                break;
                            case TouchPhase.Ended:
                                HandleOnTouchExit(touchObject, touchInput,
                                    new TouchState(Time.time, t));
                                break;
                            case TouchPhase.Stationary:
                                touchInput.OnToucHold(t, hit.point);
                                break;
                            case TouchPhase.Canceled:
                                HandleOnTouchExit(touchObject, touchInput,
                                    new TouchState(Time.time, t));
                                break;
                        }
                    }
                }
            }

            // Check whether more objects were touched in the previous frame
            // indicating that a touch was canceled
            foreach (var oldObject in previousFrameTouches)
            {
                TouchState ts;
                if (!frameTouches.TryGetValue(oldObject.Key, out ts))
                {
                    // On Touch up should always have been called at this point, but the object still lingers
                    if (oldObject.Value.onTouchObject.phase != TouchPhase.Ended)
                    {
                        HandleOnTouchExit(oldObject.Key,
                            oldObject.Key.GetComponent<ITouchInput>(),
                            oldObject.Value);
                    }
                }
            }
        }

        void HandleOnTouchExit(GameObject touchObject, ITouchInput touchInput, TouchState touchState)
        {
            TouchState onTapState;
            if (!objectOnTouchDownState.TryGetValue(touchObject, out onTapState))
            {
                objectOnTouchDownState.Remove(touchObject);
            }

            // Detect a swipe
            // Accounts for how straight the swipe is, and the magnitude^2 of the swipe vector
            if (FixTouchDelta(touchState.onTouchObject).sqrMagnitude >
                swipeSquaredDistanceThreshold && touchState.onTouchTime - onTapState.onTouchTime < swipeTimeThreshold)
            {
                Vector2 normalizedDelta = touchState.onTouchObject.deltaPosition.normalized;
                if (normalizedDelta.y >= swipeStraightness)
                {
                    touchInput.OnSwipe(touchState.onTouchObject, TouchDirection.Up);
                }
                else if (normalizedDelta.x >= swipeStraightness)
                {
                    touchInput.OnSwipe(touchState.onTouchObject, TouchDirection.Right);
                }
                else if (normalizedDelta.y <= -1 * swipeStraightness)
                {
                    touchInput.OnSwipe(touchState.onTouchObject, TouchDirection.Down);
                }
                else if (normalizedDelta.x <= -1 * swipeStraightness)
                {
                    touchInput.OnSwipe(touchState.onTouchObject, TouchDirection.Left);
                }
            }
            else
            {
                if (touchState.onTouchObject.phase == TouchPhase.Ended)
                {
                    touchInput.OnTouchUp(touchState.onTouchObject);
                }
                else
                {
                    if (touchInput != null)
                    {
                        touchInput.OnTouchExit();
                    }
                    
                }
                
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
    }
}