using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool FirstWash = true;
    private bool cookMode = false;

    public bool CookMode
    {
        get
        {
            return cookMode;
        }
    }

    private Coroutine cameraMoveCoroutine = null;

    [SerializeField]
    private WashManager washManager;
    [SerializeField]
    private Sink sink;

    public delegate void ModeChangeAction(bool mode);
    public static event ModeChangeAction OnModeChange;

    public delegate void FirstWashAction();
    public static event FirstWashAction OnFirstWashFinished;

    public void ChangeMode()
    {
        if (washManager.HandsObject.WashCoroutine != null)
            return;

        if (FirstWash)
        {
            if (washManager.HygieneGauge >= 100f)
            {
                FirstWash = false;

                if (OnFirstWashFinished != null)
                    OnFirstWashFinished();
            }
        }

        if (cameraMoveCoroutine == null)
            cameraMoveCoroutine = StartCoroutine(MoveCamera());
    }

    public void TutorialChangeMode()
    {
        if (washManager.HygieneGauge >= 100f)
            ChangeMode();
    }

    private IEnumerator MoveCamera()
    {
        if (!cookMode && washManager.WashStarted)
        {
            washManager.HandsObject.FinishWash();

            yield return new WaitForSeconds(2.5f);

            sink.CloseWater();

            yield return new WaitForSeconds(0.5f);
        }

        cookMode = !cookMode;

        var newPosition = transform.position;

        newPosition.x = -newPosition.x;

        washManager.WashSetup();

        iTween.MoveTo(gameObject, iTween.Hash("position", newPosition,
                                              "easetype", iTween.EaseType.easeInOutExpo,
                                              "time", 1f,
                                              "oncomplete", "SetTimeRates"));

        yield return new WaitForSeconds(1f);

        if (OnModeChange != null)
            OnModeChange(cookMode);

        cameraMoveCoroutine = null;
    }

    private void SetTimeRates()
    {
        if (!GameManager.Instance.TutorialOn)
        {
            GameManager.Instance.DirtRate = cookMode ? 1f : 0f;

            GameManager.Instance.WaitRate = cookMode ? 1f : 0.25f;
        }
    }
}
