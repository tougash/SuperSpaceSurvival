using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void LateUpdate() {
        if (PauseBehaviour.instance.GetIsPaused()) { return; }
        transform.position = player.transform.position;
    }
}
