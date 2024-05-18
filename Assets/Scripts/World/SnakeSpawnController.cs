using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawnController : MonoBehaviour
{

    public static SnakeSpawnController Instance;

    public float spawnInterval = 2.5f;
    public int snakeCap = 25;
    float timeSinceLastSpawn;

    [SerializeField] Transform activeSnakesParent;
    public Transform ActiveSnakesParent { get { return activeSnakesParent; } private set { } }
    [SerializeField] Transform inactiveSnakesParent;
    public Transform InactiveSnakesParent { get { return inactiveSnakesParent; } private set { } }
    [SerializeField] Transform evilSnakesParent;
    public Transform EvilSnakesParent { get { return evilSnakesParent; } private set { } }
    public Transform FollowerQueue;
    [SerializeField] float distanceToDespawn = 25;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Update()
    {
        SpawnSnake();
        EnsureEvilSnake();
        RefillScreen();
    }

    void RefillScreen()
    {
        Transform player = FollowerQueue.parent;
        if (Vector3.Distance(activeSnakesParent.position, player.position) < 20 ||
           Vector3.Distance(evilSnakesParent.position, player.position) < 20)
            return;
        for (int i = activeSnakesParent.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(activeSnakesParent.GetChild(i).position, player.position) > 15)
                ActivateSnake(activeSnakesParent.GetChild(i), activeSnakesParent);
        }
        for (int i = evilSnakesParent.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(evilSnakesParent.GetChild(i).position, player.position) > 15)
                ActivateSnake(evilSnakesParent.GetChild(i), evilSnakesParent);
        }
    }

    public void DisableSnake(Transform snake)
    {
        snake.gameObject.SetActive(false);
        snake.parent = inactiveSnakesParent;
    }

    private void EnsureEvilSnake()
    {
        if (evilSnakesParent.childCount > 0)
            return;

        Transform snake = null;

        for (int i = inactiveSnakesParent.childCount - 1; i >= 0; i--)
        {
            if (inactiveSnakesParent.GetChild(i).CompareTag(Tags.EVILSNAKE))
            {
                snake = inactiveSnakesParent.GetChild(i);
                break;
            }
        }

        if (snake == null)
            return;

        ActivateSnake(snake, evilSnakesParent);
    }

    void Start()
    {
        EventManager.Instance.OnPotFill.AddListener(ArrangeInactiveSnakes);
        InitialSort();
    }

    void InitialSort()
    {
        for (int i = inactiveSnakesParent.childCount - 1; i >= 0; i--)
        {
            ActivateSnake(inactiveSnakesParent.GetChild(i), activeSnakesParent);
        }
        for (int i = evilSnakesParent.childCount - 1; i >= 0; i--)
        {
            ActivateSnake(evilSnakesParent.GetChild(i), evilSnakesParent);
        }
    }

    void SpawnSnake()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (activeSnakesParent.childCount >= snakeCap || inactiveSnakesParent.childCount <= 0 || timeSinceLastSpawn < spawnInterval)
            return;

        Transform snake = inactiveSnakesParent.GetChild(inactiveSnakesParent.childCount - 1);
        Transform parent = activeSnakesParent;

        if (snake.CompareTag(Tags.EVILSNAKE))
            parent = evilSnakesParent;

        ActivateSnake(snake, parent);
        DisableFarAway();
    }

    void ActivateSnake(Transform snake, Transform parent)
    {
        snake.transform.position = FollowerQueue.parent.transform.position - MathLib.RandomRestrictedV3(4, 20);
        snake.gameObject.SetActive(true);
        snake.transform.SetParent(parent);
        timeSinceLastSpawn = 0;
    }

    void ArrangeInactiveSnakes()
    {
        for (int i = inactiveSnakesParent.childCount - 1; i >= 0; i--)
        {
            inactiveSnakesParent.GetChild(i).transform.localPosition = Vector3.zero;
        }
    }

    void DisableFarAway()
    {
        for (int i = activeSnakesParent.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(activeSnakesParent.GetChild(i).transform.position, FollowerQueue.parent.transform.position) > distanceToDespawn)
            {
                activeSnakesParent.GetChild(i).gameObject.SetActive(false);
                activeSnakesParent.GetChild(i).parent = inactiveSnakesParent;
            }
        }
        for (int i = evilSnakesParent.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(evilSnakesParent.GetChild(i).transform.position, FollowerQueue.parent.transform.position) > distanceToDespawn)
            {
                evilSnakesParent.GetChild(i).gameObject.SetActive(false);
                evilSnakesParent.GetChild(i).parent = inactiveSnakesParent;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnPotFill.RemoveListener(ArrangeInactiveSnakes);
    }
}
