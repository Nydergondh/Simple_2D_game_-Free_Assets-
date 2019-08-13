using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform transformFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        //checks if the player is moving next to a checkpóint (so that the camera stops)
        transform.position = new Vector3(transformFollow.position.x, transformFollow.position.y + 0.5f, -10);                   
    }
}
