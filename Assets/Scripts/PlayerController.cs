using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Text debugMessage;
    [SerializeField]
    private VirtualJoystick joystick;

    [Header("SPEEDS")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 200f;

    [Header("SHOOTING")]
    public float fireRate = 0.08f;
    public GameObject bullet;
    public Transform bulletSpawn;

    [Header("AUDIO")]
    public AudioClip shootSFX;

    public bool left = false;
    public bool right = false;
    public bool isRotating = false;
    public bool isMoving = false;
    bool isShooting = false;
    bool canShoot = true;
    Animator anim;
    AudioSource playerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack
        if(isShooting && canShoot)
        {
            StartCoroutine("Attack");
        }
        if (isRotating)
            RotatePlayer();
    }

    public void Move(Vector3 pos, Vector3 normal)
    {
        
        Vector3 moveInput = pos;
        isMoving = moveInput.magnitude != 0;
        anim.SetBool("isMoving", isMoving);
        if (isMoving && !isRotating)
        {
            // Position
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * movementSpeed);

            // Rotation
            Vector3 direction = pos - transform.position;
            Quaternion toRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRot, Time.deltaTime * rotationSpeed);
        }


        // movement(Legacy)
        /*Vector3 moveInput = inputDirection;//new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude != 0;
        anim.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
            transform.position += moveDir * Time.deltaTime * playerSpeed;
            debugMessage.text = transform.position.ToString();
        }*/
    }

    //public void Rotate(Vector3 rayPos, Vector3 closestPos, Vector2 inputDirection)
    public void Rotate(Vector3 pos)
    {
        Vector3 rotationInput = pos;
        bool isRotating = rotationInput.magnitude != 0;
        if (isRotating)
        {
            Vector3 direction = pos - transform.position;
            Quaternion toRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRot, 1000f* Time.deltaTime * rotationSpeed);
        }
        

        /*Vector3 joystickPos = new Vector3(joystick.inputDirection.x,
                    0f,
                    joystick.inputDirection.y);
        transform.rotation = Quaternion.LookRotation(transform)*/

        /*Quaternion rot = Quaternion.RotateTowards(transform.rotation, forwardDirection, 180f*//*Time.deltaTime * rotationSpeed*//*);
        transform.rotation = rot;*/

        /*Vector3 direction = rayPos - closestPos;
        Quaternion toRot = Quaternion.LookRotation(direction);
        Quaternion fromRot = Quaternion.LookRotation(new Vector3(inputDirection.x, 0f, inputDirection.y));
        transform.rotation = Quaternion.Lerp(fromRot, toRot, Time.deltaTime * rotationSpeed);*/
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRot, Time.deltaTime * rotationSpeed);
        //Quaternion toRot = Quaternion.RotateTowards(direction, new Vector3(inputDirection.x, 0f, inputDirection.y));
        //transform.rotation = toRot;
    }

    public void AttackBtnDown()
    {
        isShooting = true;
    }
    public void AttackBtnUp()
    {
        isShooting = false;
    }

    IEnumerator Attack()
    {
        // Set shot cooldown as false
        canShoot = false;

        // Play shot sound
        playerAudio.PlayOneShot(shootSFX);

        // Spawn the bullet
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        // Wait for the cooldown
        yield return new WaitForSeconds(fireRate);

        //anim.SetTrigger("doShot");
        Debug.Log("Attack!");

        // Set shot cooldown as true
        canShoot = true;
    }

    public void LeftDown()
    {
        isRotating = true;
        left = true;
        right = !left;
    }
    public void LeftUp()
    {
        isRotating = false;
        left = false;
    }

    public void RightDown()
    {
        isRotating = true;
        right = true;
        left = !right;
    }
    public void RightUp()
    {
        isRotating = false;
        right = false;
    }

    public void RotatePlayer()
    {
        if (left)
        {
            transform.Rotate(new Vector3(0f, -Time.deltaTime * rotationSpeed, 0f));
        }
        else
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * rotationSpeed, 0f));
        }
    }
}
