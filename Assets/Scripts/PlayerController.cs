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

    [Header("SPEEDS")]
    public float playerSpeed = 1f;

    [Header("SHOOTING")]
    public float fireRate = 0.08f;
    public GameObject bullet;
    public Transform bulletSpawn;

    [Header("AUDIO")]
    public AudioClip shootSFX;

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
}
