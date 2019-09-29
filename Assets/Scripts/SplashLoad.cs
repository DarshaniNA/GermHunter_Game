using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadLevelAfterDelay(2.0f));
    }

    //to load Start scene after small delay
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Constant.SCENE_START);
    }
}
