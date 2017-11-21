using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

//[ExecuteInEditMode]
public class PictureFrameTouch : MonoBehaviour, ITouchInput
{
    //[HideInInspector]
    //public CombinationPuzzleController controller;
    //[HideInInspector]
    //public Vector3 originPosition;
    [HideInInspector]
    public bool isCorrect;

    public Vector3 correctPostion, correctRotation;

    //[SerializeField]
    //private Color gizmoColor = Color.black;

    [SerializeField]
    private string pickupWwiseEvent, placeWwiseEvent;
    //[SerializeField]
    //private Material gizmoMaterial;

    //private Material internalGizmoMaterial;

    [SerializeField, Tooltip("Allowed directions to move")]
    private BasePuzzle.DirectionsStruct Directions;

    private Vector3 distanceWorldPos;
    //private Renderer cachedRenderer;
    //private MeshFilter cachedMeshFilter;
    private float startY;

    //#if UNITY_EDITOR
    //    private void OnEnable() {
    //        UnityEditor.SceneView.onSceneGUIDelegate -= OnSceneGUI;
    //        UnityEditor.SceneView.onSceneGUIDelegate += OnSceneGUI;
    //        internalGizmoMaterial = new Material(gizmoMaterial);
    //    }

    //    private void OnDisable() {
    //        UnityEditor.SceneView.onSceneGUIDelegate -= OnSceneGUI;
    //    }

    //    private void OnSceneGUI(UnityEditor.SceneView sceneView) {

    //       // Draw(sceneView.camera);
    //    }
    //#endif



    private void Start()
    {
        startY = transform.position.y;
    }


    //private void Draw(Camera cam)
    //{
    //    if (cachedRenderer == null)
    //    {
    //        cachedRenderer = transform.GetComponent<Renderer>();
    //    }
    //    if (cachedMeshFilter == null)
    //    {
    //        cachedMeshFilter = transform.GetComponent<MeshFilter>();
    //    }
    //    if (originPosition == Vector3.zero)
    //    {
    //        originPosition = transform.parent.position;
    //    }
    //    if (gizmoMaterial == null)
    //    {
    //        return;
    //    }
    //    internalGizmoMaterial.SetPass(1);
    //    gizmoColor.a = Mathf.Clamp(gizmoColor.a, 0, .5f);
    //    internalGizmoMaterial.color = gizmoColor;
    //    Matrix4x4 rotationMatrix = Matrix4x4.TRS(correctPostion + originPosition, Quaternion.Euler(correctRotation) * transform.parent.rotation, transform.lossyScale);

    //    Graphics.DrawMesh(cachedMeshFilter.sharedMesh, rotationMatrix, internalGizmoMaterial, gameObject.layer, null);
    //}

    public void OnTap()
    {
    }

    public void OnTouchDown(Vector3 worldPos)
    {
        if (isCorrect)
            return;

        //if (controller != null)
        //{
        //    controller.OnBeginSolving();
        //}
        distanceWorldPos = worldPos - transform.position;


        PlayEvent(pickupWwiseEvent);
    }

    public void OnTouchUp()
    {
        if (isCorrect)
            return;

        PlayEvent(placeWwiseEvent);
        //if (controller != null)
        //{
        //    controller.CheckForSolution(this);
        //}
        //   AkSoundEngine.PostEvent(controller.onIncorrectPlacementWwiseEvent, gameObject);
    }

    public void OnToucHold(Vector3 worldPos)
    {
        if (isCorrect)
            return;

        Vector3 newPosition = worldPos - distanceWorldPos;

        if (!Directions.X)
        {
            newPosition.x = transform.position.x;
        }
        if (!Directions.Y)
        {
            newPosition.y = transform.position.y;
        }
        if (!Directions.Z)
        {
            newPosition.z = transform.position.z;
        }
        transform.position = newPosition;
    }

    public void OnTouchExit()
    {
        if (isCorrect)
            return;

        PlayEvent(placeWwiseEvent);
        transform.SetY(startY);
        //if (controller != null)
        //{
        //    controller.CheckForSolution(this);
        //}
        //AkSoundEngine.PostEvent(controller.onIncorrectPlacementWwiseEvent, gameObject);
    }

    public void OnSwipe(TouchDirection direction)
    {
    }

    private void PlayEvent(string wwiseevent)
    {
        if (string.IsNullOrEmpty(wwiseevent))
        {
            AkSoundEngine.PostEvent(wwiseevent, gameObject);
        }
    }
}