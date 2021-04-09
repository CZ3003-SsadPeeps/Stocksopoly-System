using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Places the stock into the Unity UI
/// <br></br>
/// Created by Chia Ze
/// </summary>
public class StockListUi : MonoBehaviour
{
    public StockList stockList;

    readonly StockListManager manager = new StockListManager();

    void Start()
    {
        stockList.SetItemClickListener(pos => {
            StockStore.SelectedStockPos = pos;
            SceneManager.LoadScene("StockDetails", LoadSceneMode.Additive);
        });

        stockList.SetList(manager.GetAllStocks());
    }
    /// <summary>
    /// To load the Stock List when the button is clicked
    /// </summary>
    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("StockList");
    }
}
