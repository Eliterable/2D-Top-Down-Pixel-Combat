using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagment.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            FadeingScene.Instance.FadetoClear();
        }
    }
}
