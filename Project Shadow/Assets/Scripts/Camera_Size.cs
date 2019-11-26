using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Size : MonoBehaviour
{
    public SpriteRenderer target;
    void Start()
    {
        float orthoSize = target.bounds.size.y/2;

        Camera.main.orthographicSize = orthoSize;
    }

}
