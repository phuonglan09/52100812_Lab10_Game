using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    float smooth = 5.0f;
    float tiltAngle = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float halfW = Screen.width / 2;
        float halfH = Screen.height / 3;
        float x = (Input.mousePosition.x - halfW) / halfW;
        float z = (Input.mousePosition.y - halfH) / halfH;
        Vector3 newPosition = new Vector3(x, 0, z);
        transform.position = newPosition;

        float tiltAroundZ = Input.GetAxis("Mouse X") * tiltAngle * 2;
        float tiltAroundX = Input.GetAxis("Mouse Y") * tiltAngle * -2;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
