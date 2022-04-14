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

    private bool hasChosen;
    private bool shiftWasStarted;

    private Coroutine shiftCoroutine;

    public void StartSoulShift()
    {
        if (shiftWasStarted)
        {
            StopCoroutine(shiftCoroutine);
            ActivateShiftControls(false);
            StartCoroutine(SlowDownTime(false));
            joystick.SetActive(true);
            shiftWasStarted = false;
            hasChosen = false;
            return;
        }
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
    }

    private IEnumerator ShiftSoulToChosenEntity()
    {
        yield return null;
    }

    private void ActivateShiftControls(bool state)
    {
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
            vignette.color = new Color(1, 1, 1, 1-val);
        }
        val = state ? 0 : 1;
        Time.timeScale = val;
        vignette.color = new Color(1, 1, 1, 1-val);
    }
}
