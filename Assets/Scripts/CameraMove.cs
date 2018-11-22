using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public void MoveCamera()
    {
        var newPosition = transform.position;

        newPosition.x = -newPosition.x;

        iTween.MoveTo(gameObject, iTween.Hash("position", newPosition,
                                              "easetype", iTween.EaseType.easeInOutExpo,
                                              "time", 1f));
    }
}
