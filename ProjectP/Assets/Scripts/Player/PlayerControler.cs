using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour, IDataPersistence
{
    public PlayerControls playerControls;

    private Rigidbody2D playerRB;

    public Stat speed;

    private Vector2 movement;

    public Animator animator;

    private float moveSpeed;
    [SerializeField] private float DefaultmoveSpeed = 5f;

    bool isBeingSlowed = false;

    private float slowAmount;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRB = GetComponent<Rigidbody2D>();
        moveSpeed = DefaultmoveSpeed;

        Physics2D.IgnoreLayerCollision(6,7);
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
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
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
        if(!isBeingSlowed)
            playerRB.velocity = directions * speed.currentValue;
        else
            playerRB.velocity = directions * (speed.currentValue / slowAmount);
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