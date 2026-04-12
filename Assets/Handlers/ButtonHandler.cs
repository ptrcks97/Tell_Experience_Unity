using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void StartExperience()
    {
        SceneManager.LoadScene("Wilhelm_Tell_Scene");
    }
}
