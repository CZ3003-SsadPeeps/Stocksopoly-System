using UnityEngine;
using UnityEngine.UI;

///<summary>Concrete class which inherits PlayerCard. Contains the properties and methods of the small player card displayed besides the big player card
///<br></br>
///Done by: Khairuddin and Yi Shen
///</summary>
public class PlayerCardSmall : PlayerCard
{
    ///<summary>References the background image of the small player card</summary>
    public Image selectionBackground;

    ///<summary>Method that sets the position of the player card</summary>
    /// <param name="pos">Vector2 which defines the new position of the player card</param>
    internal void SetPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }

    ///<summary>Method that sets the selected player card</summary>
    /// <param name="isSelected">When isSelected is true, the player card will be active, when isSelected is false, the player card will be inactive</param>
    internal void SetSelected(bool isSelected)
    {
        selectionBackground.gameObject.SetActive(isSelected);
    }
}
