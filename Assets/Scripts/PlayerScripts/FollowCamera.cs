using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void LateUpdate() {
        transform.position = player.transform.position;
    }
}
