using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoder : MonoBehaviour {
    void Start() {
        StartCoroutine(LoadMainScene());
    }


    private IEnumerator LoadMainScene() {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f) {
            Debug.Log(operation.progress * 100f + "%読み込み完了");
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        operation.allowSceneActivation = true;
    }
    
    
}
