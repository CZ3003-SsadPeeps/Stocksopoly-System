using UnityEngine;
using UnityEngine.SceneManagement;

public class StockRow : MonoBehaviour
{   
    // used by the ui to display the stocks in Stock scene
    public int RowPos { get; set; }

   // when button is clicked, loads up the Tesla Scene
    public void OnTradeButtonClick()
    {
        StockStore.SelectedStockPos = RowPos;
        SceneManager.LoadScene("Tesla");
    }
}
