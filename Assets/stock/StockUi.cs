using UnityEngine;
using UnityEngine.SceneManagement;

public class StockUi : MonoBehaviour
{
    public StockListUi stockList;

    void Start()
    {
        stockList.SetItemClickListener(pos => {
            StockStore.SelectedStockPos = pos;
            SceneManager.LoadScene("Tesla");
        });
    }

    public void OnBackButtonClick()
    {

    }
}
