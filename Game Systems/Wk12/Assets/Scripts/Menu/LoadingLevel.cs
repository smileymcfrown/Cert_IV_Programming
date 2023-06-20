using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevel : MonoBehaviour
{
    public GameObject loadScreen;
    public Image progressBar;
    public Text progressText;

    public void LoadLevel(int sceneIndex)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
                StartCoroutine(LoadAsync(sceneIndex));
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
