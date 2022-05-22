using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject portal;

    private void Awake()
    {
        EntityManager.AllEnemiesKilled += ActivatePortal;
    }

    private void OnDestroy()
    {
        EntityManager.AllEnemiesKilled -= ActivatePortal;
    }

    private void ActivatePortal()
    {
        portal.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseEntity>().isPlayer)
        {
            MoveToNextLevel();
        }
    }

    private void MoveToNextLevel()
    {
        Debug.Log("Moving to next level");
        SceneManager.LoadScene(0);
    }
}
