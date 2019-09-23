using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CharacterHorizontal : MonoBehaviour
{
    public float speed;
    public CameraHorizontal CameraController;

    private Vector3 movement;
    
        
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraController.moveUp == true)
        {
            transform.position += movement * speed;
        }
        else if (CameraController.moveDown == true)
        {
            transform.position -= movement * speed;
        }
    }
}
