﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Database;

public class GameUi : MonoBehaviour
{
    static readonly Vector2 popupHiddenPos = new Vector2(0, -760);
    static readonly Color32[] CARD_COLORS = new Color32[] {
        new Color32(240, 98, 146, 255),
        new Color32(186, 102, 199, 255),
        new Color32(125, 133, 201, 255),
        new Color32(145, 164, 174, 255)
    };

    public Board board;
    public Canvas canvas;
    public Button rollDiceButton, endTurnButton;
    public Text endGameText;
    public Image endGameBackground;
    public PlayerCardBig bigPlayerCard;
    public EventUi eventPopup;
    public GameObject passedGoPopup, PlayerCardSmallPrefab;

    public GameObject[] startGameObjects;
    public GameObject[] endGameObjects;

    GameController controller;
    readonly List<PlayerCardSmall> smallPlayerCards = new List<PlayerCardSmall>(4);

    void Start()
    {
        // Uncomment when testing Game UI only
        //GameStore.InitPlayers(new string[] { "Abu", "Banana", "Cherry", "Mewtwo" });

        controller = new GameController(new StockTrader(), new PlayerRecordDAO());
        GeneratePlayerCards();

        // Ensures popup is displayed on top of everything else. Must be done after player cards are generated
        passedGoPopup.transform.SetAsLastSibling();
        eventPopup.transform.SetAsLastSibling();

        LoadCurrentPlayerDetails();
    }

    void Update()
    {
        int currentPlayerCredit = GameStore.CurrentPlayer.Credit;
        smallPlayerCards[GameStore.CurrentPlayerPos].SetCredit(currentPlayerCredit);

        bigPlayerCard.SetCredit(currentPlayerCredit);
        // This operation is pretty expensive, so will only be done when while user is in stock market UI
        if (GameStore.ShouldUpdatePlayerStock)
        {
            List<PlayerStock> stocks = controller.GetPlayerStocks();
            bigPlayerCard.SetStockDetails(stocks);
        }
    }

    public void RollDice()
    {
        StartCoroutine(PerformDiceRoll());
    }

    public void EndTurn()
    {
        if (board.HasReachedMaxLaps())
        {
            bigPlayerCard.SetVisible(false);

            for (int i = 0; i < smallPlayerCards.Count; i++)
            {
                smallPlayerCards[i].SetPosition(new Vector3(-300 + (200 * i), -180, 0f));
            }

            // Disable all buttons except leaderboard & back
            foreach(GameObject gameObject in startGameObjects)
            {
                gameObject.SetActive(false);
            }

            foreach (GameObject gameObject in endGameObjects)
            {
                gameObject.SetActive(true);
            }

            controller.SavePlayerScores();
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

    // [NOTE] This function corresponds to clicking the end game button. This is NOT the same as
    // the first player reaching 14 laps
    public void OnEndGameButtonClick()
    {
        // TODO: Display confirmation message, then end game
        Debug.Log("Ending game...");
    }

    public void OnEventConfirmButtonClick()
    {
        StartCoroutine(MovePopupToPos(eventPopup.PosTransform, popupHiddenPos));
    }

    public void ShowNewsList()
    {
        SceneManager.LoadScene("NewsList", LoadSceneMode.Additive);
    }

    void LoadCurrentPlayerDetails()
    {
        Player currentPlayer = GameStore.CurrentPlayer;
        List<PlayerStock> stocks = controller.GetPlayerStocks();
        Debug.Log($"Player[{currentPlayer.Name}, ${currentPlayer.Credit}]");

        smallPlayerCards[GameStore.PrevPlayerPos].SetSelected(false);
        smallPlayerCards[GameStore.CurrentPlayerPos].SetSelected(true);

        bigPlayerCard.SetPlayerDetails(currentPlayer, CARD_COLORS[GameStore.CurrentPlayerPos]);
        bigPlayerCard.SetStockDetails(stocks);

        // Select player's piece
        board.SetSelectedPiece(GameStore.CurrentPlayerPos);
        endTurnButton.interactable = false;
    }

    void OnQuizTileActivated()
    {
        Debug.Log("Launching quiz UI...");
        SceneManager.LoadScene("DifficultySelection", LoadSceneMode.Additive);
    }

    void OnEventTileActivated()
    {
        Debug.Log("Launching event UI...");
        eventPopup.LoadNewEvent();
        StartCoroutine(MovePopupToPos(eventPopup.PosTransform, Vector2.zero));
    }

    void DisplayNews()
    {
        SceneManager.LoadScene("News", LoadSceneMode.Additive);
    }

    void DisplayFinalScores()
    {
        // TODO: Display all players score
        Player[] players = GameStore.Players;
        foreach (Player player in players)
        {
            Debug.Log($"Player[{player.Name}, ${player.Credit}]");
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
            yield return StartCoroutine(MovePopupToPos(goPopupTransform, popupHiddenPos));

            Debug.Log("Received GO payout");
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

        rollDiceButton.GetComponentInChildren<Text>().text = "Roll Dice";
        endTurnButton.interactable = true;

        yield break;
    }

    void GeneratePlayerCards()
    {
        GameObject cardObject;
        Player player;
        PlayerCardSmall smallPlayerCard;
        for (int i = 0; i < GameStore.Players.Length; i++)
        {
            player = GameStore.Players[i];

            // Create small player card
            cardObject = Instantiate(PlayerCardSmallPrefab);
            cardObject.transform.SetParent(canvas.transform, false);

            smallPlayerCard = cardObject.GetComponent<PlayerCardSmall>();
            smallPlayerCard.SetPosition(new Vector3(-400f, -90 * (i + 1), 0f));
            smallPlayerCard.SetPlayerDetails(player, CARD_COLORS[i]);
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
