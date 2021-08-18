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
    private Text debugMessage;

    RaycastHit hit;
    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * 20);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, groundLayerMask))
        {
            Debug.Log("Ground Hit!");
            debugMessage.text = hit.point.ToString();

            player.Move(hit.point);
        }
    }
}
