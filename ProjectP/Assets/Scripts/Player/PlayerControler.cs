using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour, IDataPersistence
{
    public PlayerControls playerControls;

    private Rigidbody2D playerRB;

    public Stat speed;

    private float moveSpeed;
    [SerializeField] private float DefaultmoveSpeed = 5f;

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
        playerRB.velocity = directions * speed.currentValue;
    }

    public void ReduceSpeed(float byAmount)
    {
        moveSpeed = DefaultmoveSpeed / byAmount;
    }

    public void ReduceSpeed()
    {
        moveSpeed = DefaultmoveSpeed;
    }

}
