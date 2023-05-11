using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public PlayerControls playerControls;
    public PlayerScript playerScript;

    private Rigidbody2D playerRB;

    public Stat speed;

    public Animator animator;

    [SerializeField] private float DefaultmoveSpeed = 5f;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 22f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private WalkingSoundController walkingSoundScript;

    bool isBeingSlowed = false;

    private float slowAmount;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRB = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6,7);
        walkingSoundScript = GetComponentInChildren<WalkingSoundController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        Vector2 movementDirection = playerControls.Player.Move.ReadValue<Vector2>().normalized;
        MovePlayer(movementDirection);

        if(playerControls.Player.Dash.WasPerformedThisFrame() && canDash)
            StartCoroutine(Dash(movementDirection));

        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");


        if (Time.timeScale != 0)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
            animator.SetFloat("Speed", movementDirection.sqrMagnitude);
        }

        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            if (Time.timeScale != 0)
            {
                playWalkingSoundEffect();
            }
        }
    }

    private void playWalkingSoundEffect() 
    {
		if (walkingSoundScript == null)
		{
            return;
		}
        if (walkingSoundScript.stepSoundIsPlaying())
        {
            return;
        }
        walkingSoundScript.playStepSound();
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    private void MovePlayer(Vector2 directions)
    {
        if (!isDashing)
        {
            if (!isBeingSlowed)
                playerRB.velocity = directions * speed.currentValue;
            else
                playerRB.velocity = directions * (speed.currentValue / slowAmount);
        }
    }


    private IEnumerator Dash(Vector2 directions)
    {
        canDash = false;
        isDashing = true;
        playerRB.velocity = directions * dashingPower;
        //Makes the player invincible for dash duration
        playerScript.BecomeInvicible(dashingTime*2);
        //Turns off collision between player(7) and enemy(11) layers
        Physics2D.IgnoreLayerCollision(7, 11, true);
        yield return new WaitForSeconds(dashingTime);
        Physics2D.IgnoreLayerCollision(7, 11, false);

        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void ReduceSpeed(float byAmount)
    {
        isBeingSlowed = true;
        slowAmount = byAmount;
    }

    public void ReduceSpeed()
    {
        isBeingSlowed = false;
        slowAmount = 1;
    }

}