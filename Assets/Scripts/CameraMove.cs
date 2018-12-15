using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool cookMode = true;

    public void MoveCamera()
    {
        cookMode = !cookMode;

        var newPosition = transform.position;

        newPosition.x = -newPosition.x;

        iTween.MoveTo(gameObject, iTween.Hash("position", newPosition,
                                              "easetype", iTween.EaseType.easeInOutExpo,
                                              "time", 1f,
                                              "oncomplete", "SetTimeRates"));
    }

    private void SetTimeRates()
    {
        GameManager.Instance.DirtRate = cookMode ? 1f : 0f;

        GameManager.Instance.WaitRate = cookMode ? 1f : 0.4f;
    }
}
