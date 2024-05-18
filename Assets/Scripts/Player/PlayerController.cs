using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] CharmController charmController;
    float currentSpeed;
    float currentCharmRange;
    bool canMove = true;

    private void Start()
    {
        EventManager.Instance.OnApplyCharm.AddListener(AddStats);
        charmController.SetSize(stats.charmRange);
        currentSpeed = stats.moveSpeed;
        currentCharmRange = stats.charmRange;
    }

    void Update()
    {
        Move(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastCharm();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCharm();
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Move(Vector3 direction)
    {
        if (!canMove)
            return;
        direction = direction.normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * currentSpeed);
    }

    void CastCharm()
    {
        // Can't charm if followers queue is full
        if (charmController.followersQ.childCount >= stats.maxFollowers || charmController.isCharming)
            return;
        EventManager.Instance.OnStartCharm.Invoke(stats.charmTime, stats.maxFollowers);
    }

    void StopCharm()
    {
        if (!charmController.isCharming)
            return;
        EventManager.Instance.OnStopCharm.Invoke();
    }

    void LeaveSnakes()
    {
        int snakes = charmController.followersQ.childCount;
        AddEXP(snakes);
        EmptyCarryingSnakes(snakes);
        stats.totalSnakes += snakes;
        EventManager.Instance.OnPotFill.Invoke();
        EventManager.Instance.OnSetScore.Invoke(stats.totalSnakes);
    }

    void AddEXP(int amount)
    {
        stats.currentSnakes += amount;
        if (stats.currentSnakes >= stats.snakesToNextLvl)
        {
            stats.currentSnakes -= stats.snakesToNextLvl;
            stats.level++;
            float a = stats.lvlCoefficients[0];
            float b = stats.lvlCoefficients[1];
            stats.snakesToNextLvl = MathLib.ExpLogPoly(a, b, stats.level);
            EventManager.Instance.OnLevelUp.Invoke(stats.level);
        }
        if (stats.currentSnakes >= stats.snakesToNextLvl)
            AddEXP(0);
        else
            EventManager.Instance.OnIncreasedSnakeCount.Invoke(stats.currentSnakes, stats.snakesToNextLvl);
    }

    void Pause()
    {
        EventManager.Instance.OnPauseGame.Invoke();
    }

    void AddStats(CharmInfo charm)
    {
        if (charm.option == Charm.True_Love_Orb)
        {
            int snakesToAdd = stats.snakesToNextLvl - stats.currentSnakes;
            for (int i = 1; i < Info.GetRarityModifier(charm.rarity); i++)
            {
                snakesToAdd += MathLib.ExpLogPoly(stats.lvlCoefficients[0], stats.lvlCoefficients[1], stats.level + i);
            }
            AddEXP(snakesToAdd);
            return;
        }
        if (charm.option == Charm.Apollo_Teardrop)
        {
            stats.moveSpeed += (Info.GetRarityModifier(charm.rarity) * Constants.SPDMODIFIER);
            currentSpeed = stats.moveSpeed;
            return;
        }
        if (charm.option == Charm.Amplifier_Amulet)
        {
            stats.charmRange += (Info.GetRarityModifier(charm.rarity) * Constants.RANGEMODIFIER);
            currentCharmRange += (Info.GetRarityModifier(charm.rarity) * Constants.RANGEMODIFIER);
            charmController.SetSize(currentCharmRange);
            return;
        }
        if (charm.option == Charm.Smooth_Criminal_Ring)
        {
            stats.charmTime -= (Info.GetRarityModifier(charm.rarity) * Constants.CHARMTIMEMODIFIER);
            return;
        }
        if (charm.option == Charm.Venomstar_Locket)
        {
            stats.maxFollowers += (Info.GetRarityModifier(charm.rarity) * Constants.FOLLOWMODIFIER);
            return;
        }
    }

    void EmptyCarryingSnakes(int snakes)
    {
        for (int i = snakes - 1; i >= 0; i--)
        {
            var snake = charmController.followersQ.GetChild(i);
            snake.SetParent(SnakeSpawnController.Instance.InactiveSnakesParent);
            snake.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag(Tags.EVILSNAKE))
        {
            StopCharm();
            if (charmController.followersQ.childCount <= 0)
            {
                int option = (int)UnityEngine.Random.Range(0, 2);
                if (option == 0)
                {
                    stats.moveSpeed -= Constants.ONHITSPDREDUCTION;
                    currentSpeed = stats.moveSpeed;
                    print("reduced speed");
                }
                if (option == 1)
                {
                    stats.charmTime += Constants.ONHITCHARMTIMEPENALTY;
                    print("reduced charm");
                }
                return;
            }
            EmptyCarryingSnakes(charmController.followersQ.childCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.FXAMP))
        {
            currentCharmRange += Constants.FXAMPMOD;
            charmController.SetSize(currentCharmRange);
        }
        if (other.CompareTag(Tags.FXSPEED))
        {
            currentSpeed += Constants.FXSPEEDMOD;
        }
        if (other.CompareTag(Tags.FXSLOW))
        {
            currentSpeed -= Constants.FXSLOWMOD;
        }
        if (other.CompareTag(Tags.POT))
        {
            if (charmController.followersQ.childCount <= 0)
                return;
            LeaveSnakes();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tags.FXAMP))
        {
            currentCharmRange = stats.charmRange;
            charmController.SetSize(currentCharmRange);
        }
        if (other.CompareTag(Tags.FXSPEED) || other.CompareTag(Tags.FXSLOW))
        {
            currentSpeed = stats.moveSpeed;
        }
    }

}
