using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public void SelectCharacter(int characterIndex)
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();
        //Back to menu
        SceneManager.LoadScene("Menu");
    }
}
