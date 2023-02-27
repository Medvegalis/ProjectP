using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    private PlayerControls playerControls;
    private GameObject player;
    private Rigidbody2D playerRB;

    [SerializeField] private float moveSpeed = 15f;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
        player = GetComponent<GameObject>();
        playerRB = GetComponent<Rigidbody2D>();
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
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>().normalized;
        MovePlayer(move);
        Debug.Log(move);
    }

    private void MovePlayer(Vector2 directions)
    {
        playerRB.velocity = directions * moveSpeed;

    }

}
