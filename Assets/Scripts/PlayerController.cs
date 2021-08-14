using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text debugMessage;

    public float playerSpeed = 1f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector2 inputDirection)
    {
        Vector3 moveInput = inputDirection;//new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude != 0;
        anim.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
            transform.position += moveDir * Time.deltaTime * playerSpeed;
            debugMessage.text = transform.position.ToString();
        }
    }
}
