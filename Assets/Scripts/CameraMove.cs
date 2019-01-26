using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool cookMode = true;

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

    public void ChangeMode()
    {
        if (cameraMoveCoroutine == null)
            cameraMoveCoroutine = StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        if (!cookMode && washManager.WashStarted())
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
        GameManager.Instance.DirtRate = cookMode ? 1f : 0f;

        GameManager.Instance.WaitRate = cookMode ? 1f : 0.25f;
    }
}
