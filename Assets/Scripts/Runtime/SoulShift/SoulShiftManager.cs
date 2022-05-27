using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoulShiftManager : MonoBehaviour
{
    [SerializeField]
    private float slowDownDuration = 1f;
    [SerializeField]
    private AnimationCurve slowDownCurve;

    [Header("References")]
    [SerializeField]
    private StatsPanel statsPanel;
    [SerializeField]
    private GameObject deadMenu;
    [SerializeField]
    private GameObject shiftStartControls;
    [SerializeField]
    private Image vignette;
    [SerializeField]
    private Image progressFill;
    [SerializeField]
    private Button soulShiftButton;
    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject shiftControls;
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private GameObject choiceRing;
    [SerializeField]
    private ParticleSystem choiceParticles;

    [Header("Soulshift properties")]
    [SerializeField]
    SoulShiftTypeScriptableObject currentSoulShiftUsed;

    private const float FLOOR_Y = -0.5f;

    private bool hasChosen;
    private bool shiftWasStarted;

    private Coroutine shiftCoroutine;
    private BaseEntity chosenEntity;
    private BaseEntity curPlayerEntity;

    private List<BaseEntity> availableEntities;
    private int index;

    private void Awake()
    {
        currentSoulShiftUsed = GameState.GetData(GameState.CHOSEN_SOUL_VARIANT_KEY, currentSoulShiftUsed) as SoulShiftTypeScriptableObject;
        EntityManager.PlayerDied += PlayerDied;
        if (Time.timeScale == 0)
        {
            StartCoroutine(SlowDownTime(false));
        }

        switch (currentSoulShiftUsed.Type)
        {
            case SoulShiftActivationType.Kills:
                EntityManager.EntityKilled += EntityGotKilled;
                break;
            case SoulShiftActivationType.LevelsPassed:
                break;
            case SoulShiftActivationType.DamageDealt:
                break;
            case SoulShiftActivationType.Time:
                break;
        }

        AllowSoulShift(currentSoulShiftUsed.GetSoulShiftProgress() >= 1);
    }

    private void OnDestroy()
    {
        EntityManager.PlayerDied -= PlayerDied;
        EntityManager.EntityKilled -= EntityGotKilled;
    }

    public void StartSoulShift()
    {
        if (shiftWasStarted)
        {
            choiceParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            StopCoroutine(shiftCoroutine);
            statsPanel.gameObject.SetActive(false);

            ActivateShiftControls(false);
            StartCoroutine(SlowDownTime(false));
            joystick.SetActive(true);
            shiftWasStarted = false;
            hasChosen = false;
            return;
        }
        EntityManager.Instance.GetAvailableEntities(out availableEntities, out index);
        chosenEntity = availableEntities[index];
        curPlayerEntity = chosenEntity;
        statsPanel.gameObject.SetActive(true);
        MoveSelectionRingToEntity(chosenEntity);
        shiftWasStarted = true;
        hasChosen = false;
        shiftCoroutine = StartCoroutine(ShiftCoroutine());
    }

    private IEnumerator ShiftCoroutine()
    {
        joystick.SetActive(false);

        yield return SlowDownTime(true);
        ActivateShiftControls(true);
        yield return new WaitUntil(() => { return hasChosen; });
        statsPanel.gameObject.SetActive(false);
        ActivateShiftControls(false);
        ShiftSoulToChosenEntity();
        joystick.SetActive(true);
        yield return SlowDownTime(false);
        shiftWasStarted = false;
        hasChosen = false;
        curPlayerEntity = null;
        choiceParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        EntityManager.Instance.FindNewClosestEntityForPlayer();
        GameState.SoulShiftUsed();
        UpdateSoulShiftProgress();
    }

    private void AllowSoulShift(bool state)
    {
        soulShiftButton.interactable = state;
    }

    private void ShiftSoulToChosenEntity()
    {
        if (curPlayerEntity != chosenEntity)
        {
            chosenEntity.gameObject.AddComponent<PlayerController>();
            chosenEntity.isPlayer = true;
            MakePreviousPlayerEnitityIntoEnemy(curPlayerEntity);
        }
    }

    private void MakePreviousPlayerEnitityIntoEnemy(BaseEntity curPlayerEntity)
    {
        ParticleMaster.Instance.SpawnParticles(new Vector3(curPlayerEntity.transform.position.x, 0f, curPlayerEntity.transform.position.z), ParticleType.MagicExplosion);
        Destroy(curPlayerEntity.gameObject.GetComponent<PlayerController>());

        curPlayerEntity.isPlayer = false;
    }

    private void ActivateShiftControls(bool state)
    {
        shiftControls.SetActive(state);
    }

    private IEnumerator SlowDownTime(bool state)
    {
        float timer = slowDownDuration;
        float val;

        while (timer > 0)
        {
            yield return null;
            timer -= Time.unscaledDeltaTime;
            val = state ? timer / slowDownDuration : 1 - timer / slowDownDuration;
            Time.timeScale = slowDownCurve.Evaluate(val);
            vignette.color = new Color(1, 1, 1, 1 - val);
        }
        val = state ? 0 : 1;
        Time.timeScale = val;
        vignette.color = new Color(1, 1, 1, 1 - val);
    }


    private void MoveCamToEntity(BaseEntity chosenEntity)
    {
        playerCam.Follow = chosenEntity.transform;
    }
    private void MoveSelectionRingToEntity(BaseEntity chosenEntity)
    {
        choiceRing.SetActive(true);
        choiceParticles.Play(true);
        choiceRing.transform.position = new Vector3(chosenEntity.transform.position.x, FLOOR_Y, chosenEntity.transform.position.z);
        statsPanel.SetStats(chosenEntity.GetStats());
    }

    public void ShiftNext()
    {
        if (index < availableEntities.Count - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        chosenEntity = availableEntities[index];
        MoveCamToEntity(chosenEntity);
        MoveSelectionRingToEntity(chosenEntity);
    }

    public void ShiftPrevious()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = availableEntities.Count-1;
        }

        chosenEntity = availableEntities[index];
        MoveCamToEntity(chosenEntity);
        MoveSelectionRingToEntity(chosenEntity);
    }

    public void ConfirmShift()
    {
        if (chosenEntity != null)
        {
            hasChosen = true;
        }
    }

    private void EntityGotKilled(BaseEntity entity)
    {
        UpdateSoulShiftProgress();
    }

    private void UpdateSoulShiftProgress()
    {
        float progress = currentSoulShiftUsed.GetSoulShiftProgress();
        progressFill.fillAmount = progress;
        AllowSoulShift(progress >= 1f);
    }

    private void PlayerDied()
    {
        StartCoroutine(ShowEndMenuCoroutine());
    }

    private IEnumerator ShowEndMenuCoroutine()
    {
        joystick.SetActive(false);
        shiftStartControls.SetActive(false);
        yield return SlowDownTime(true);
        deadMenu.SetActive(true);
    }

    public void RestartPressed()
    {
        SceneManager.LoadScene(0);
    }
}