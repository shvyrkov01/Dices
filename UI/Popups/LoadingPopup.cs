using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingPopup : Popup
{
    [SerializeField] private Text _loadingSceneField;



    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void LoadingScene(string sceneName)
    {
        _loadingSceneField.text = sceneName;

        StartCoroutine(Loading(sceneName));
    }


    private IEnumerator Loading(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        Close();
    }
}
