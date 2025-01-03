using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public void OnPlayButtonClick()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
    }

    public void OnQuitButtonClick()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                    Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif

        #if (UNITY_EDITOR)
                    UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
                    Application.Quit();
        #endif
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadSceneAsync("MainMenuScene");
        }
    }

}
