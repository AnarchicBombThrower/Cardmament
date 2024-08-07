using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardHand;
using static Player;

public class PlayersManager : MonoBehaviour
{
    private int nextPlayerIndex = 0;
    private Player currentPlayer = null;
    private Player[] players;
    private CardManager cardManager;
    public enum playerType { human, cpu }

    private void Start()
    {
        cardManager = GetComponent<CardManager>();
    }

    public void setupPlayers(playerType[] playersToSetup)
    {
        players = new Player[playersToSetup.Length];
        
        for (int i = 0; i < players.Length; i++)
        {
            switch (playersToSetup[i])
            {
                case playerType.human:
                    players[i] = new HumanPlayer(cardManager.getDeck());
                    break;
                case playerType.cpu:
                    break;
            }
            
        }
    }

    public void onGameBegan()
    {
        beginTurnOfNextPlayerIndex();
    }

    public Player getCurrentPlayer()
    {
        return currentPlayer;
    }

    public Player[] getAllPlayers()
    {
        return players;
    }

    public void beginTurnOfNextPlayerIndex()
    {
        currentPlayer = players[nextPlayerIndex];
        currentPlayer.ourTurnBegun();
        nextPlayerIndex++;

        if (nextPlayerIndex == players.Length)
        {
            nextPlayerIndex = 0;
        }
    }

    public void endTurn()
    {
        beginTurnOfNextPlayerIndex();
    }

    public void subcribeToHumanPlayerCallbacks(newCard newCardSubscribe, removedCard removedCardSubscribe, ourHandTurn ourHandTurnSubscribe)
    {
        foreach (Player player in players)
        {
            if (player.isHuman() == false)
            {
                continue;
            }

            CardHand hand = player.getOurHand();
            hand.subscribeToCardAddedCallback(newCardSubscribe);
            hand.subscribeToCardRemovedCallback(removedCardSubscribe);
            hand.subscribeToHandTurnCallback(ourHandTurnSubscribe);
        }
    }

    public void subscribeToHumanTurnOfPlayerCallback(turnOfPlayer turnOfPlayerSubscribe)
    {
        foreach (Player player in players)
        {
            if (player.isHuman() == false)
            {
                continue;
            }

            player.subscribeToTurnOfPlayer(turnOfPlayerSubscribe);
        }
    }
}
