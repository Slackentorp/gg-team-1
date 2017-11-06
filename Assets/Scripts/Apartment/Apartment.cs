using Gamelogic.Extensions;
using UnityEngine;


#pragma warning disable 0414
public class Apartment : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private string currentStateLiteral;

    private ApartmentState currentState;

    // Use this for initialization
    void Start()
    {
        SetState(new LivingRoomState(this));
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }

    public void SetState(ApartmentState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = state;
        currentStateLiteral = state.ToString();

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}