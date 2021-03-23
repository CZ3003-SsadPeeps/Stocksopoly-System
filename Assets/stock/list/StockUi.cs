using UnityEngine;
using UnityEngine.SceneManagement;

public class StockUi : MonoBehaviour
{
    public StockListUi stockList;

    readonly StockListManager manager = new StockListManager();

    void Start()
    {
        stockList.SetItemClickListener(pos => {
            StockStore.SelectedStockPos = pos;
            SceneManager.LoadScene("Tesla", LoadSceneMode.Additive);
        });

        stockList.SetList(manager.GetAllStocks());
    }

    public void OnBackButtonClick()
    {
        SceneManager.UnloadSceneAsync("Stock");
    }
}
