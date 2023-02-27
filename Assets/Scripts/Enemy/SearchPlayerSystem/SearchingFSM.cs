using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingFSM : MonoBehaviour
{
    public enum ZOMBIE_TARGETINGSTATE
    {
        Lookaround, Tracking, LostTarget
    }

    private ZOMBIE_TARGETINGSTATE _currentState;

    private void Start()
    {
        _currentState = ZOMBIE_TARGETINGSTATE.Lookaround;
    }

    private void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (_currentState)
        {
            case ZOMBIE_TARGETINGSTATE.Lookaround:
                // Do look around behavior
                // If target is detected, transition to Tracking state
                break;
            case ZOMBIE_TARGETINGSTATE.Tracking:
                // Do tracking behavior
                // If target is lost, transition to LostTarget state
                break;
            case ZOMBIE_TARGETINGSTATE.LostTarget:
                // Do lost target behavior
                // If target is found again, transition to Tracking state
                break;
        }
    }

    public void ChangerState(ZOMBIE_TARGETINGSTATE newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case ZOMBIE_TARGETINGSTATE.Lookaround:
                // Do look around behavior
                // If target is detected, transition to Tracking state
                break;
            case ZOMBIE_TARGETINGSTATE.Tracking:
                // Do tracking behavior
                // If target is lost, transition to LostTarget state
                break;
            case ZOMBIE_TARGETINGSTATE.LostTarget:
                // Do lost target behavior
                // If target is found again, transition to Tracking state
                break;
        }
    }

}
