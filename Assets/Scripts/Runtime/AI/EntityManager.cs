using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    private List<BaseEntity> entities;

    public UnityEvent<BaseEntity> OnPlayerChanged;

    private BaseEntity curPlayer;

    float curClosestDistance = float.MaxValue;
    BaseEntity curClosestEnemy;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        if (PlayerController.Instance != null)
        {
            PlayerController_PlayerControllerChanged(PlayerController.Instance);
        }
        PlayerController.PlayerControllerChanged += PlayerController_PlayerControllerChanged;

        entities = new List<BaseEntity>();
    }

    private void OnDestroy()
    {
        PlayerController.PlayerControllerChanged -= PlayerController_PlayerControllerChanged;
    }



    public void UpdateClosestEnemy(float distance, BaseEntity enemy)
    {
        if (enemy == curClosestEnemy)
        {
            curClosestDistance = distance;
        }
        else if (distance < curClosestDistance)
        {
            Debug.Log($"enemy {enemy.name} is closer now");
            curClosestDistance = distance;
            curClosestEnemy = enemy;
            PlayerController.Instance.attackController.target = curClosestEnemy.healthController;
        }
    }

    private void PlayerController_PlayerControllerChanged(PlayerController player)
    {
        Debug.Log($"player changed to {player.transform.name}");
        curPlayer = player.GetComponent<BaseEntity>();
        OnPlayerChanged?.Invoke(curPlayer);
        
    }

    public void AddEntity(BaseEntity entity)
    {
        entities.Add(entity);
        entity.ActivateEntity(true);
    }

    public void RemoveEntity(BaseEntity entity)
    {
        entities.Remove(entity);
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

    internal void PlayerDied()
    {
        foreach (BaseEntity entity in entities)
        {
            entity.ActivateEntity(false);
        }
    }
}
