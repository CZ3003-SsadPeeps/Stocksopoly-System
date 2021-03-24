using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("StockList");
    }
}
