using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void StartExperience()
    {
        SceneManager.LoadScene("Wilhelm_Tell_Scene");
    }

    public void ShowInformation()
    {
        SceneManager.LoadScene("Info");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
