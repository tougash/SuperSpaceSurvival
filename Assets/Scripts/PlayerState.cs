using UnityEngine;

public enum CurrentState
{
    IDLE,
    WALKING,
    SHOOTING,
}

public class PlayerState : MonoBehaviour
{
    [SerializeField] CurrentState playerState = CurrentState.IDLE;


}
