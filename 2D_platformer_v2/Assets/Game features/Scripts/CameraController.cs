using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Uppercase "Transform" - Refers to the transform component of another object
    // Lowercase "transform" - Refers to the transform component of the object which the script is on (the camera)
    [SerializeField] private Transform player;
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 2, transform.position.z);
    }
}
