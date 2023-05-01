using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour, IHasAttack, IHasProjectileAttack
{
    [Header("Setup data")]
    public GameObject ArrowPrefab;
    public Transform BowPoint;
    public Stat playerDamageStat;

    private AbilityControler abilityScript;
    private AudioSource audioSource;

    [Header("Weapon values")]
    [SerializeField]
    private int weaponDamage = 2;
    [SerializeField]
    private float baseWeaponProjectileSpeed = 10f;
    [SerializeField]
    private int baseProjectileCount = 1;
    [SerializeField]
    private float attackCooldown = .2f;
    [SerializeField]
    private float multipleProjectileAngleIncrement = 15f;
    [Header("Debug lookup for current values")]
    [SerializeField]
    private int projectileCount;
    [SerializeField]
    private float weaponProjectileSpeed;

    public bool canAttack;
    public bool attackOnCooldown;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        projectileCount = baseProjectileCount;
        weaponProjectileSpeed = baseWeaponProjectileSpeed;
        getPlayerAbilityScript();
    }

    void Update()
    {
		if (abilityScript == null)
		{
            getPlayerAbilityScript();
		}

        updateProjectileAmountFromAbility();
        updateProjectileSpeedFromAbility();

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

        audioSource.Play();
		for (int i = 1; i <= projectileCount; i++)
		{
            float angleIndexAlternating = -(i%2)*(i/2) + ((1-(i%2))*(i/2));
            float angle = Mathf.LerpUnclamped(0f, multipleProjectileAngleIncrement, angleIndexAlternating);
            ShootArrow(angle);
		}

        StartCoroutine(PerformCooldownPeriod());
    }
    private IEnumerator PerformCooldownPeriod()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);

        yield return new WaitForSeconds(0.1f);
        attackOnCooldown = false;
    }

    private void ShootArrow(float angleOffset) {
        //Gets the agle of the bow
        float angle = Utility.AngleTowardsMouse(BowPoint.position);
        //rotates 90 degress so it shots forward
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f + angleOffset));

        var arrow = Instantiate(
            ArrowPrefab,
            BowPoint.position,
            rot
        );

        var arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetDamage(weaponDamage * playerDamageStat.currentValue);
        arrowScript.SetProjectileSpeed(weaponProjectileSpeed);
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

    private void getPlayerAbilityScript() 
    {
        GameObject player = GameObject.Find("Player");
        abilityScript = player.GetComponent<AbilityControler>();
    }

    public void setProjectileAmount(int count)
    {
        baseProjectileCount = count;
    }

    public void updateProjectileAmountFromAbility()
    {
        if (abilityScript == null)
        {
            return;
        }

        int abilityIndex = checkForAbilityIdInEnabledAbilities(0);
        if (abilityIndex == -1)
        {
            return;
        }

        Ability projectileAbility = abilityScript.getEnabledAbility(abilityIndex);

        projectileCount = baseProjectileCount + ((int)projectileAbility.GetValuePerLevel() * projectileAbility.GetLevel());
    }

    public void setProjectileSpeed(float speed)
    {
        weaponProjectileSpeed = speed;
    }

    public void updateProjectileSpeedFromAbility()
    {
        if (abilityScript == null)
        {
            return;
        }

        int abilityIndex = checkForAbilityIdInEnabledAbilities(1);
        if (abilityIndex == -1)
        {
            return;
        }

        Ability projectileAbility = abilityScript.getEnabledAbility(abilityIndex);

        weaponProjectileSpeed = baseWeaponProjectileSpeed + (projectileAbility.GetValuePerLevel() * projectileAbility.GetLevel());
    }

    private int checkForAbilityIdInEnabledAbilities(int abilityId)
    {
        int abilityCount = abilityScript.enabledAbilityCount;
        int projectileAbilityIndex = -1;
        for (int i = 0; i < abilityCount; i++)
        {
            Ability ability = abilityScript.getEnabledAbility(i);
            if (ability.GetId() == abilityId)
            {
                projectileAbilityIndex = i;
                break;
            }
        }
        return projectileAbilityIndex;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
    }

	
}
