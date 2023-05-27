using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    private float length, startpos_x, startpos_y, cam_startpos_x, cam_startpos_y;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private GameObject cam;

    private void Start()
    {
        startpos_x = transform.position.x;
        startpos_y = transform.position.y;
        cam_startpos_x = cam.transform.position.x;
        cam_startpos_y = cam.transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // This makes sure that the paarallax is calculated only after the camera is repositioneds
    private void Update()
    {
        float cam_x = cam.transform.position.x - cam_startpos_x;
        float cam_y = cam.transform.position.y - cam_startpos_y;
        
        // Find how much the background moved relative to the camera
        float temp_x = (cam_x * (1 - parallaxEffect));
        float dist_x = (cam_x * parallaxEffect);
        float dist_y = (cam_y * parallaxEffect);

        transform.position = new Vector3(startpos_x + dist_x, startpos_y + dist_y, transform.position.z);

        // 10 acts as a buffer to move the background before the edge of the camera reaches the end 
        // (Esp with CineMachine cameras since they have a greater length)
        if (temp_x > startpos_x + length - 10)
            startpos_x += length;

        else if (temp_x < startpos_x - length + 10)
            startpos_x -= length;
    }
}