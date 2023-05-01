using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAttack : MonoBehaviour, IHasAttack
{
    [Header("Setup data")]
    public GameObject CrossbowShotPrefab;
    public Stat playerDamageStat;
    public Transform weaponPoint;
    [SerializeField]
    private AudioSource crossbowDrawAudioSource;
    [SerializeField]
    private AudioSource crossbowShootAudioSource;

    [Header("Weapon values")]
    [SerializeField]
    private int weaponDamage = 20;
    [SerializeField]
    private float attackCooldown = 2f;
    [SerializeField]
    private float attackChargeTime = 1f;
    [SerializeField]
    private float shotSpeed = 100f;

    public bool canAttack;

    public bool attackCharging;
    public bool attackOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        attackOnCooldown = false;
        attackCharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

	public void Attack()
	{
        if (!canAttack)
        {
            return;
        }
        if (!AttackButtonPressed())
        {
            return;
        }
        if (attackOnCooldown)
        {
            return;
        }
        if (attackCharging)
        {
            return;
        }

        StartCoroutine(PerformChargeAttack());
        StartCoroutine(PerformCooldownPeriod());
    }

    public bool AttackButtonPressed()
    {
        if (Time.timeScale == 0)
        {
            return false;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        return true;
    }

    private IEnumerator PerformChargeAttack()
    {
        attackCharging = true;
        crossbowDrawAudioSource.Play();
        yield return new WaitForSeconds(attackChargeTime);

        yield return new WaitForSeconds(0.01f);
        ShootBeam();
        attackCharging = false;
    }

    private IEnumerator PerformCooldownPeriod()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);

        yield return new WaitForSeconds(0.1f);
        attackOnCooldown = false;
    }

    private void ShootBeam() 
    {
        crossbowShootAudioSource.Play();
        float angle = Utility.AngleTowardsMouse(weaponPoint.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));

        var shot = Instantiate(
            CrossbowShotPrefab,
            weaponPoint.position,
            rot
        );

        var shotScript = shot.GetComponent<CrossbowShot>();
        shotScript.SetDamage(weaponDamage * playerDamageStat.currentValue);
        shotScript.SetProjectileSpeed(shotSpeed);
    }

    public void DisableAttack()
	{
        canAttack = false;
	}

	public void EnableAttack()
	{
        canAttack = true;
	}
}
