using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(
            Mathf.Clamp(targetToFollow.position.x,-1.45f,2.5f),
            Mathf.Clamp(targetToFollow.position.y,-3.0f,4.5f),
            transform.position.z
        );
    }
}
