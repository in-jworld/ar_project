using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour
{
    public LayerMask groundLayerMask;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private VirtualJoystick joystick;
    [SerializeField]
    private BoxCollider plane;
    [SerializeField]
    private Text debugMessage;
    private void Start()
    {
        plane = GetComponent<BoxCollider>();
    }

    RaycastHit hit;
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 20);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, groundLayerMask))
        {
            Debug.Log("Ground Hit!");
            debugMessage.text = hit.point.ToString();
            
            player.Move(hit.point, hit.normal);
            if (joystick.isInput)
            {
                //Vector3 closestPos = plane.ClosestPoint(transform.position);
                //player.Rotate(hit.point, closestPos, joystick.inputDirection);

                Quaternion forwardDir = Quaternion.FromToRotation(plane.transform.forward, hit.point);
                Vector3 ang = forwardDir.eulerAngles;
                
                /*Vector3 joystickPos = new Vector3(joystick.inputDirection.x,
                    0f,
                    joystick.inputDirection.y);
                Quaternion forwardDir = Quaternion.FromToRotation(joystickPos.normalized);*/

                //Quaternion forwardDir = Quaternion.FromToRotation(hit.point.normalized, joystickPos.normalized);
                //Quaternion forwardDir = Quaternion.FromToRotation(plane.transform.forward, joystickPos.normalized);
                player.Rotate(hit.point);

            }
        }
    }

}
