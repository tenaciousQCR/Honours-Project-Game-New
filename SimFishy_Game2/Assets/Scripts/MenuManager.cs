using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
    public void Continue()
    {
        SceneManager.LoadScene("Scenes/GridTest");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Scenes/GridTest");
    }

}
