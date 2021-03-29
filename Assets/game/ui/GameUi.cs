using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Database;

public class GameUi : MonoBehaviour
{
    static readonly Vector2 POPUP_HIDDEN_POS = new Vector2(0, -800);
    static readonly string PLAYER_CARDS_ROOT = "Cards";
    static readonly string[] SMALL_CARD_RES = new string[4];
    static readonly string[] BIG_CARD_RES = new string[4];

    static GameUi()
    {
        for (int i = 0; i < 4; i++)
        {
            SMALL_CARD_RES[i] = $"{PLAYER_CARDS_ROOT}/Small/player_{i + 1}_card_small";
            BIG_CARD_RES[i] = $"{PLAYER_CARDS_ROOT}/Big/player_{i + 1}_card_big";
        }
    }

    public Board board;
    public Canvas canvas;
    public Button rollDiceButton, endTurnButton;
    public Text endGameText;
    public Image endGameBackground;
    public PlayerCardBig bigPlayerCard;
    public GameObject passedGoPopup, PlayerCardSmallPrefab;

    public GameObject[] startGameObjects;
    public GameObject[] endGameObjects;

    GameController controller;
    readonly List<PlayerCardSmall> smallPlayerCards = new List<PlayerCardSmall>(4);

    void Start()
    {
        // Uncomment when testing Game UI only
        controller = new GameController(new StockTrader(), new PlayerRecordDAO());
        GeneratePlayerCards();

        // Ensures popup is displayed on top of everything else. Must be done after player cards are generated
        passedGoPopup.transform.SetAsLastSibling();

        LoadCurrentPlayerDetails();
    }

    void Update()
    {
        int currentPlayerCredit = controller.CurrentPlayer.Credit;
        smallPlayerCards[controller.CurrentPlayerPos].SetCredit(currentPlayerCredit);
        bigPlayerCard.SetCredit(currentPlayerCredit);

        // This operation is pretty expensive, so will only be done when while user is in stock market UI
        if (controller.ShouldUpdatePlayerStock)
        {
            bigPlayerCard.SetStockDetails(controller.GetPlayerStocks());
        }
    }

    public void RollDice()
    {
        StartCoroutine(PerformDiceRoll());
    }

    public void EndTurn()
    {
        int numLaps = board.GetNumLapsForCurrentPiece();
        if (controller.HasReachedMaxLaps(numLaps))
        {
            DisplayFinalScores();
            return;
        }

        bool shouldShowNews = controller.NextTurn();
        if (shouldShowNews) DisplayNews();

        LoadCurrentPlayerDetails();
        rollDiceButton.interactable = true;
    }

    public void ShowStockMarket()
    {
        SceneManager.LoadScene("StockList", LoadSceneMode.Additive);
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("Home");
    }

    public void ShowLeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }

    public void ShowNewsList()
    {
        SceneManager.LoadScene("NewsList", LoadSceneMode.Additive);
    }

    void LoadCurrentPlayerDetails()
    {
        int prevPos = controller.PrevPlayerPos;
        int currentPos = controller.CurrentPlayerPos;
        Player currentPlayer = controller.CurrentPlayer;
        List<PlayerStock> stocks = controller.GetPlayerStocks();

        smallPlayerCards[prevPos].SetSelected(false);
        smallPlayerCards[currentPos].SetSelected(true);

        bigPlayerCard.SetTextColor(currentPos == 0);
        bigPlayerCard.SetPlayerDetails(currentPlayer, BIG_CARD_RES[currentPos]);
        bigPlayerCard.SetStockDetails(stocks);

        // Select player's piece
        board.SetSelectedPiece(currentPos);
        endTurnButton.interactable = false;
    }

    void OnQuizTileActivated()
    {
        SceneManager.LoadScene("DifficultySelection", LoadSceneMode.Additive);
    }

    void OnEventTileActivated()
    {
        SceneManager.LoadScene("EventPopup", LoadSceneMode.Additive);
    }

    void DisplayNews()
    {
        SceneManager.LoadScene("News", LoadSceneMode.Additive);
    }

    void DisplayFinalScores()
    {
        bigPlayerCard.SetVisible(false);

        // Disable all buttons except leaderboard & back
        foreach (GameObject gameObject in startGameObjects)
        {
            gameObject.SetActive(false);
        }

        foreach (GameObject gameObject in endGameObjects)
        {
            gameObject.SetActive(true);
        }

        // Sell all stocks
        controller.SavePlayerScores();

        // Update player card details & move to center of screen
        Player[] players = controller.Players;

        Player player;
        PlayerCardSmall smallCard;
        for (int i = 0; i < smallPlayerCards.Count; i++)
        {
            player = players[i];
            smallCard = smallPlayerCards[i];

            smallCard.SetPosition(new Vector3(-300 + (200 * i), -180, 0f));
            smallCard.SetSelected(false);
            smallCard.SetCredit(player.Credit);
        }
    }

    // Must be performed in coroutine to wait for piece to move before performing additional operations
    IEnumerator PerformDiceRoll()
    {
        rollDiceButton.interactable = false;

        int diceValue = controller.GenerateDiceValue();
        rollDiceButton.GetComponentInChildren<Text>().text = diceValue.ToString();

        yield return StartCoroutine(board.MovePiece(diceValue));

        // Check if piece landed or passed through GO tile
        int currentPiecePos = board.GetCurrentPiecePos();
        if (currentPiecePos < diceValue)
        {
            controller.IssueGoPayout();

            // Display passed GO popup
            RectTransform goPopupTransform = passedGoPopup.GetComponent<RectTransform>();

            yield return StartCoroutine(MovePopupToPos(goPopupTransform, Vector2.zero));
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(MovePopupToPos(goPopupTransform, POPUP_HIDDEN_POS));
        }

        // Launch tile event if needed
        Tile currentTile = board.tiles[currentPiecePos];
        // No need check for GO tile. Already handled by code above
        switch (currentTile.Type)
        {
            case TileType.Quiz:
                OnQuizTileActivated();
                break;
            case TileType.Event:
                OnEventTileActivated();
                break;
        }

        rollDiceButton.GetComponentInChildren<Text>().text = string.Empty;
        endTurnButton.interactable = true;

        yield break;
    }

    void GeneratePlayerCards()
    {
        Player[] players = controller.Players;

        GameObject cardObject;
        Player player;
        PlayerCardSmall smallPlayerCard;
        for (int i = 0; i < players.Length; i++)
        {
            player = players[i];

            // Create small player card
            cardObject = Instantiate(PlayerCardSmallPrefab);
            cardObject.transform.SetParent(canvas.transform, false);

            smallPlayerCard = cardObject.GetComponent<PlayerCardSmall>();
            smallPlayerCard.SetPosition(new Vector3(-400f, -90 * (i + 1), 0f));
            smallPlayerCard.SetPlayerDetails(player, SMALL_CARD_RES[i]);
            smallPlayerCards.Add(smallPlayerCard);
        }
    }

    IEnumerator MovePopupToPos(RectTransform popupTransform, Vector2 targetPos)
    {
        do
        {
            // Current position must be passed using RectTransform property. Otherwise popup will keep jumping back & forth between current & target positions
            popupTransform.anchoredPosition = Vector2.MoveTowards(popupTransform.anchoredPosition, targetPos, 10f);
            yield return null;
        } while (popupTransform.anchoredPosition != targetPos);
        yield break;
    }
}
