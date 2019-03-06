using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControllerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetButton("Fire"))
            Load("home");
        if (Input.GetButton("Cancel"))
            Quit();*/
    }

    public void Load(string level)//, int spawnPointNum)
    {
        //lmb.Load(level);
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
