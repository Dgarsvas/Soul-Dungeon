using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    public delegate void OnPlayerChangedEvent(BaseEntity entity);
    public static event OnPlayerChangedEvent PlayerChanged;

    public delegate void OnEntityKilledEvent(BaseEntity entity);
    public static event OnEntityKilledEvent EntityKilled;

    public delegate void OnAllEnemiesKilled();
    public static event OnAllEnemiesKilled AllEnemiesKilled;

    public delegate void OnPlayerDiedEvent();
    public static event OnPlayerDiedEvent PlayerDied;

    [SerializeField]
    private GameObject currentPlayerTargetIdentifier;
    private BaseEntity curPlayer;
    internal List<BaseEntity> entities;
    private float curClosestDistance = float.MaxValue;
    private BaseEntity curClosestEnemy;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        if (PlayerController.Instance != null)
        {
            PlayerControllerChanged(PlayerController.Instance);
        }
        PlayerController.PlayerControllerChanged += PlayerControllerChanged;

        entities = new List<BaseEntity>();
    }

    private void OnDestroy()
    {
        PlayerController.PlayerControllerChanged -= PlayerControllerChanged;
    }

    public void UpdateClosestEnemy(float distance, BaseEntity enemy)
    {
        if (enemy == curClosestEnemy)
        {
            curClosestDistance = distance;
        }
        else if (distance < curClosestDistance)
        {
            curClosestDistance = distance;
            curClosestEnemy = enemy;
            currentPlayerTargetIdentifier.SetActive(true);
            PlayerController.Instance.attackController.target = curClosestEnemy.healthController;
        }
    }

    private void Update()
    {
        if (curClosestEnemy != null)
        {
            currentPlayerTargetIdentifier.transform.position = new Vector3(curClosestEnemy.transform.position.x, -0.5f, curClosestEnemy.transform.position.z);
        }
        else
        {
            currentPlayerTargetIdentifier.SetActive(false);
        }
    }

    private void PlayerControllerChanged(PlayerController player)
    {
        curPlayer = player.GetComponent<BaseEntity>();
        PlayerChanged?.Invoke(curPlayer);
    }

    public void AddEntity(BaseEntity entity)
    {
        entities.Add(entity);
        entity.ActivateEntity(true);
    }

    public void RemoveEntity(BaseEntity entity)
    {
        entities.Remove(entity);
        if (entity.isPlayer)
        {
            PlayerHasDied();
        }
        else
        {
            CurrentStatistics.EnemyKilled();
            EntityKilled?.Invoke(entity);
            if (entities.Count == 1 && entities[0].isPlayer)
            {
                AllEnemiesKilled?.Invoke();
                PlayerController.Instance.EnableAttack(false);
            }
        }

        if (entity == curClosestEnemy)
        {
            FindNewClosestEntityForPlayer();
        }
    }

    public void FindNewClosestEntityForPlayer()
    {
        curClosestDistance = float.MaxValue;
        curClosestEnemy = null;
        foreach (var entity in entities)
        {
            if (!entity.isPlayer)
            {
                entity.UpdateDistanceToPlayer();
            }
        }
    }

    public void GetAvailableEntities(out List<BaseEntity> availableEntities, out int index)
    {
        availableEntities = entities;
        index = availableEntities.IndexOf(PlayerController.Instance.GetComponent<BaseEntity>());
    }

    public BaseEntity GetPlayer()
    {
        return curPlayer;
    }

    internal void PlayerHasDied()
    {
        foreach (BaseEntity entity in entities)
        {
            entity.ActivateEntity(false);
        }
        PlayerDied?.Invoke();
    }
}
