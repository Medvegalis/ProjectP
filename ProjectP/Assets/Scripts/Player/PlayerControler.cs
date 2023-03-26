using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour, IDataPersistence
{
    bool controlsEnabled;
    private PlayerControls playerControls;
    [SerializeField] private GameObject weapon;

    private Rigidbody2D playerRB;

    private float moveSpeed;
    [SerializeField] private float DefaultmoveSpeed = 5f;

    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRB = GetComponent<Rigidbody2D>();
        controlsEnabled = true;
        moveSpeed = DefaultmoveSpeed;

        Physics2D.IgnoreLayerCollision(6,7);
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
        if (controlsEnabled && Time.timeScale != 0)
        {
            LookAtMouse();
        }
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
        playerRB.velocity = directions * moveSpeed;
    }

    public void ReduceSpeed(float byAmount)
    {
        moveSpeed = DefaultmoveSpeed / byAmount;
    }

      public void ReduceSpeed()
    {
        moveSpeed = DefaultmoveSpeed;
    }

    private void LookAtMouse() 
    {
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        //var angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        //weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float angle = Utility.AngleTowardsMouse(weapon.transform.position);
		weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }


}
