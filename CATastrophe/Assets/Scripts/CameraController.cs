using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*    void LateUpdate()
        {
            if(transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z);

                targetPosition.x = Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x);
                targetPosition.y = Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }*/

    private void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

    }
}
