using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Instance;

    private List<BaseEntity> entities;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        entities = new List<BaseEntity>();
    }

    public void AddEntity(BaseEntity entity)
    {
        entities.Add(entity);
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
}
