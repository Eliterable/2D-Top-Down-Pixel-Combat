using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(WaitToFade());
        }

    }

    IEnumerator WaitToFade()
    {
        FadeingScene.Instance.FadetoBlack();

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
        SceneManagment.Instance.SetTransitionName(sceneTransitionName);
    }




}
