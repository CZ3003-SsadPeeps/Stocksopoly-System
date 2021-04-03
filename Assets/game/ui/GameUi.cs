using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Database;

/// <summary>
/// This is the user interface that displays the gameplay and the end of game interface
/// <br/><br/>
/// Created by Khairuddin Bin Ali
/// </summary>
public class GameUi : MonoBehaviour
{
    private static readonly Vector2 POPUP_HIDDEN_POS = new Vector2(0, -800);
    private static readonly string PLAYER_CARDS_ROOT = "Cards";
    private static readonly string[] SMALL_CARD_RES = new string[4];
    private static readonly string[] BIG_CARD_RES = new string[4];

    static GameUi()
    {
        for (int i = 0; i < 4; i++)
        {
            SMALL_CARD_RES[i] = $"{PLAYER_CARDS_ROOT}/Small/player_{i + 1}_card_small";
            BIG_CARD_RES[i] = $"{PLAYER_CARDS_ROOT}/Big/player_{i + 1}_card_big";
        }
    }

    /// <summary>
    /// The game board that holds the player's piece and moves them throughout the game.
    /// </summary>
    public Board board;

    /// <summary>
    /// The root of the 2D user interface. Used to place the small player cards.
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// The button that generates the number of tiles the player's piece moves.
    /// </summary>
    public Button rollDiceButton;

    /// <summary>
    /// The button that passes control from the current player to the next player.
    /// </summary>
    public Button endTurnButton;

    /// <summary>
    /// The big card in the middle of the interface that displays the currently playing player's
    /// name, credit, & list of stocks they own.
    /// </summary>
    public PlayerCardBig bigPlayerCard;

    /// <summary>
    /// The popup that will be displayed when the player lands or passes the GO tile.
    /// </summary>
    public GameObject passedGoPopup;

    /// <summary>
    /// The template for creating the small player cards.
    /// </summary>
    public GameObject PlayerCardSmallPrefab;

    /// <summary>
    /// List of UI widgets that will be hidden when the game ends
    /// </summary>
    public GameObject[] startGameObjects;

    /// <summary>
    /// List of UI widgets that will be shown when the game ends
    /// </summary>
    public GameObject[] endGameObjects;

    private GameController controller;
    private readonly List<PlayerCardSmall> smallPlayerCards = new List<PlayerCardSmall>(4);

    private void Start()
    {
        // Uncomment when testing Game UI only
        controller = new GameController(new StockTrader(), new PlayerRecordDAO());
        GeneratePlayerCards();

        // Ensures popup is displayed on top of everything else. Must be done after player cards are generated
        passedGoPopup.transform.SetAsLastSibling();

        LoadCurrentPlayer();
    }

    private void Update()
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

    /// <summary>
    /// Generates the number of tiles that the player's piece will move, then calles the <c>Board</c> object to
    /// move the piece
    /// </summary>
    public void RollDice()
    {
        StartCoroutine(PerformDiceRoll());
    }

    /// <summary>
    /// Checks if the player has reached the maximum number of laps. If true, display the end of game interface.
    /// Otherwise passes control to the next player. If 4 turns have passed, <c>NewsUi</c> is launched.
    /// </summary>
    public void EndTurn()
    {
        int numLaps = board.GetNumLapsForCurrentPiece();
        if (controller.HasReachedMaxLaps(numLaps))
        {
            DisplayEndOfGame();
            return;
        }

        bool shouldShowNews = controller.NextTurn();
        if (shouldShowNews) DisplayNews();

        LoadCurrentPlayer();
        rollDiceButton.interactable = true;
    }

    /// <summary>
    /// Launch <c>NewsUi</c>.
    /// </summary>
    public void ShowNewsList()
    {
        SceneManager.LoadScene("NewsList", LoadSceneMode.Additive);
    }

    /// <summary>
    /// Launches <c>StockListUi</c>.
    /// </summary>
    public void ShowStockMarket()
    {
        SceneManager.LoadScene("StockList", LoadSceneMode.Additive);
    }

    /// <summary>
    /// Launches <c>LeaderboardUi</c>.
    /// </summary>
    public void ShowLeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }

    /// <summary>
    /// Launches <c>HomeUi</c>.
    /// </summary>
    public void ReturnHome()
    {
        SceneManager.LoadScene("Home");
    }

    private void LoadCurrentPlayer()
    {
        int currentPos = controller.CurrentPlayerPos;

        // Change selected player
        smallPlayerCards[controller.PrevPlayerPos].SetSelected(false);
        smallPlayerCards[currentPos].SetSelected(true);

        // Change big card details
        bigPlayerCard.SetTextColor(currentPos == 0);
        bigPlayerCard.SetPlayerDetails(controller.CurrentPlayer, BIG_CARD_RES[currentPos]);
        bigPlayerCard.SetStockDetails(controller.GetPlayerStocks());

        // Select player's piece
        board.SetSelectedPiece(currentPos);
        endTurnButton.interactable = false;
    }

    private void ActivateQuiz()
    {
        SceneManager.LoadScene("DifficultySelection", LoadSceneMode.Additive);
    }

    private void ActivateEvent()
    {
        SceneManager.LoadScene("EventPopup", LoadSceneMode.Additive);
    }

    private void DisplayNews()
    {
        SceneManager.LoadScene("News", LoadSceneMode.Additive);
    }

    private void DisplayEndOfGame()
    {
        bigPlayerCard.SetVisible(false);

        // Switch to end of game UI
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

    private void GeneratePlayerCards()
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

    // Must be performed in coroutine to wait for piece to move before performing additional operations
    private IEnumerator PerformDiceRoll()
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
                ActivateQuiz();
                break;
            case TileType.Event:
                ActivateEvent();
                break;
        }

        rollDiceButton.GetComponentInChildren<Text>().text = string.Empty;
        endTurnButton.interactable = true;

        yield break;
    }

    private IEnumerator MovePopupToPos(RectTransform popupTransform, Vector2 targetPos)
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
