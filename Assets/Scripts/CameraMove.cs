using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool cookMode = true;

    private Coroutine cameraMoveCoroutine = null;

    [SerializeField]
    private WashManager washManager;

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

            yield return new WaitForSeconds(3f);
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

        cameraMoveCoroutine = null;
    }

    private void SetTimeRates()
    {
        GameManager.Instance.DirtRate = cookMode ? 1f : 0f;

        GameManager.Instance.WaitRate = cookMode ? 1f : 0.25f;
    }
}
