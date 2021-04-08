using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerCard : MonoBehaviour
{
    public Text nameText, creditText;
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
