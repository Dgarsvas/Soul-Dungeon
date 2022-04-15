using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulShiftManager : MonoBehaviour
{
    [SerializeField]
    private float slowDownDuration = 1f;
    [SerializeField]
    private AnimationCurve slowDownCurve;

    [Header("References")]
    [SerializeField]
    private Image vignette;
    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject shiftControls;
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private GameObject choiceRing;

    private const float FLOOR_Y = -0.5f;

    private bool hasChosen;
    private bool shiftWasStarted;

    private Coroutine shiftCoroutine;
    private BaseEntity chosenEntity;

    private List<BaseEntity> availableEntities;
    private int index;

    public void StartSoulShift()
    {
        if (shiftWasStarted)
        {
            choiceRing.SetActive(false);
            StopCoroutine(shiftCoroutine);
            ActivateShiftControls(false);
            StartCoroutine(SlowDownTime(false));
            joystick.SetActive(true);
            shiftWasStarted = false;
            hasChosen = false;
            return;
        }
        EntityManager.Instance.GetAvailableEntities(out availableEntities, out index);
        chosenEntity = availableEntities[index];
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
        ActivateShiftControls(false);
        yield return ShiftSoulToChosenEntity();
        yield return SlowDownTime(false);

        joystick.SetActive(true);
        shiftWasStarted = false;
        hasChosen = false;
        choiceRing.SetActive(false);
    }

    private IEnumerator ShiftSoulToChosenEntity()
    {
        yield return null;
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
        choiceRing.transform.position = new Vector3(chosenEntity.transform.position.x, FLOOR_Y, chosenEntity.transform.position.z);
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
}
