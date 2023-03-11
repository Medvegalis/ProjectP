using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    bool controlsEnabled;
    private PlayerControls playerControls;
    private GameObject player;

    private Rigidbody2D playerRB;

    [SerializeField] private float moveSpeed = 2f;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
        player = GetComponent<GameObject>();
        playerRB = GetComponent<Rigidbody2D>();
        controlsEnabled = true;
    }

    private void OnEnable()
    {
        controlsEnabled = true;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        controlsEnabled = false;
        playerControls.Disable();
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            LookAtMouse();
        }
        Vector2 movementDirection = playerControls.Player.Move.ReadValue<Vector2>().normalized;
        MovePlayer(movementDirection);
    }

    private void MovePlayer(Vector2 directions)
    {
        playerRB.velocity = directions * moveSpeed;
    }

    private void LookAtMouse() 
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


}
