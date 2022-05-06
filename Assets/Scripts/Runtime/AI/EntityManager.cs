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

    public UnityEvent<Transform> OnPlayerChanged;

    private Transform curPlayerTransform;

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

    float curClosestDistance = float.MaxValue;
    Transform curClosestEnemyTransform;

    public void UpdateClosestEnemy(float distance, Transform transform)
    {
        if (transform == curClosestEnemyTransform)
        {
            curClosestDistance = distance;
        }
        else if (distance < curClosestDistance)
        {
            Debug.Log($"enemy {transform.name} is closer now");
            curClosestDistance = distance;
            curClosestEnemyTransform = transform;
            PlayerController.Instance.attackController.target = curClosestEnemyTransform;
        }
    }

    private void PlayerController_PlayerControllerChanged(PlayerController player)
    {
        Debug.Log($"player changed to {player.transform.name}");
        OnPlayerChanged?.Invoke(player.transform);
        curPlayerTransform = player.transform;
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

    public Transform GetPlayer()
    {
        return curPlayerTransform;
    }

    internal void PlayerDied()
    {
        foreach (BaseEntity entity in entities)
        {
            entity.ActivateEntity(false);
        }
    }
}
