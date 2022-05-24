using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoadManager.Instance.LoadScene("Level1");
    }
}
