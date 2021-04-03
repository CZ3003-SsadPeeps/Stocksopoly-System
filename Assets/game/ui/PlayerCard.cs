using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a base class for UI widgets that display the player's name & credit.
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public abstract class PlayerCard : MonoBehaviour
{
    /// <summary>
    /// The text widget that displays the player's name.
    /// </summary>
    public Text nameText;

    /// <summary>
    /// The text widget that displays the player's credit.
    /// </summary>
    public Text creditText;

    /// <summary>
    /// The widget that displays the card's background image
    /// </summary>
    public RawImage cardBackground;

    internal void SetPlayerDetails(Player player, string imageRes)
    {
        nameText.text = player.Name;
        creditText.text = $"{player.Credit}C";

        cardBackground.texture = Resources.Load<Texture2D>(imageRes);
    }

    internal void SetCredit(int credit)
    {
        creditText.text = $"{credit}C";
    }
}
