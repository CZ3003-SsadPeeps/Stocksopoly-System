using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The user interface that provides the option to start a game of Stocksopoly. This user interface is
/// the first one that runs when the game first launches.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class HomeUi : MonoBehaviour
{
    /// <summary>
    /// Launches <c>NameInputUi</c> class.
    /// </summary>
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Name Input");
    }
}
