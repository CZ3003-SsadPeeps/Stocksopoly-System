using UnityEngine;
using UnityEngine.UI;

///<summary>Abstract PlayerCard class which is inherited by PlayerCardBig and PlayerCardSmall
///<br></br>
///Done by: Khairuddin
///</summary>
public abstract class PlayerCard : MonoBehaviour
{
    ///<summary>References the Text objects on the Player Card</summary>
    public Text nameText, creditText;

    ///<summary>References the Image object on the Player Card</summary>
    public RawImage cardBackground;

    ///<summary>Sets the name and credit text on the Player Card</summary>
    /// <param name="player">The player object whose details are to be set on the Player Card</param>
    /// <param name="imageRes">The image to be loaded</param>
    internal void SetPlayerDetails(Player player, string imageRes)
    {
        nameText.text = player.Name;
        creditText.text = $"{player.Credit}C";

        cardBackground.texture = Resources.Load<Texture2D>(imageRes);
    }

    ///<summary>Sets the credit text on the Player Card</summary>
    /// <param name="credit">The credits to be set on the Player Card</param>
    internal void SetCredit(int credit)
    {
        creditText.text = $"{credit}C";
    }
}
