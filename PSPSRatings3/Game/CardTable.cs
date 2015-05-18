using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.CardExpansion;

namespace HeroBang.Game
{
  public class CardTable
  {
    public List<IPlayer> PlayerList { get; set; }
    public CardDeck CentralCardDeck { get; set; }

    public CardTable()
    {
      PlayerList = new List<IPlayer>(); ;
      CentralCardDeck = new CardDeck();
    }

    public void InitialCardsDealing()
    {
      foreach (IPlayer player in PlayerList)
      {
        player.CardsHeld = CentralCardDeck.DealCards(player.PlayerFigure.MaxHitPoint);
      }
    }

    public void DealingSpecificCards(List<List<ICard>> cardsListToDeal)
    {
      int counter = 0;

      foreach (IPlayer player in PlayerList)
      {
        player.CardsHeld = cardsListToDeal[counter++];
      }
    }

    //public void AllPlayersPlayARandomCard()
    //{
    //  foreach (IPlayer player in PlayerList)
    //  {
    //    if (player.CardsHeld.Count > 0)
    //    {
    //      Card card = player.CardsHeld[0];
    //      player.PlayACard(card);
    //      player.PrintPlayedCard(card);
    //    }
    //  }
    //  Console.ReadLine();
    //}

    public void PrintAllPlayersCards()
    {
      foreach (IPlayer player in PlayerList)
      {
        Util.PrintPlayerAction(player, null, PlayerAction.Have);
      }
      Console.ReadLine();
    }

    public void StartGame()
    {
      // Start a game
      while (!IsGameOver())
      {
        // Start a round
        foreach (IPlayer player in PlayerList)
        {
          // Judge the bomb for the player before the player starts his round
          JudgeIndividualBomb(PlayerList, player);

          // Check if the player needs to be put in prison before the player starts his round
          JudgeIndividualPrison(player);

          // Start a player's turn 
          // Raise the PlayerTurnStarts event or the PlayAnotheCard event
          for (player.Messenger.OnPlayerTurnStarts(); player.CardNeedResponse != null; player.Messenger.OnPlayAnotheCard())
          {
            // Start a card's life cycle
            // TODO This behaviour is likely to be changed

            if (player.CardNeedResponse.IsBomb())
            {
              StartACardLifeCycle(player, player);
            }
            else if (player.CardNeedResponse.IsJade())
            {
              if (player.CardNeedResponse.CardAction == CAction.GetEquipped)
                Stop here
                player.CardNeedResponse.CardAction = CAction.TakeEffect;
              else
              // TODO Enable a player to play the jade card for someone else in the future
              // STOP HERE Need to distinguish beteen the jade card being equipped and being played
                JudgeIndividualJade(player.CardNeedResponse, player, player);
            }
            else
            {
              AffectedPlayer numOfAffectedPlayers = CardJudge.CardAffectNumOfPlayers(player.CardNeedResponse);
              if (numOfAffectedPlayers == AffectedPlayer.One)
              {
                StartACardLifeCycle(player, player.PlayerToKill);
              }
              else
              {
                foreach (IPlayer playerReceiving in PlayerList)
                {
                  if (numOfAffectedPlayers == AffectedPlayer.All ||
                      numOfAffectedPlayers == AffectedPlayer.AllButMyself && player != playerReceiving)
                    StartACardLifeCycle(player, playerReceiving);
                }
              }
            }

          }
          // Raise the PlayerTurnEnds event
          player.Messenger.OnPlayerTurnEnds();
        }
      }

      Console.ReadLine();
    }

    void JudgeIndividualPrison(IPlayer playerCarrying)
    {
      ICard cardPrisonCarrying = playerCarrying.CardCarrying.Find(c => c.CardCarryingType == CarriedType.Prison);
      if (cardPrisonCarrying == null)
        // Early out if no prison card found
        return;

      ICard cardUsedToJudge = null;

      //IPlayer playerResponding;
      // Raise the ResponseNeeded event
      ICard cardResponded = AllPlayersCanRespond(cardPrisonCarrying, null);

      if (cardResponded != null)
      {
        Util.PrintPlayerAction(cardResponded.PlayerPlaying, cardResponded, PlayerAction.Counter);
        CardJudge.CardNotTakeEffect(null, null, playerCarrying, cardPrisonCarrying);
        Util.PrintPlayerAction(playerCarrying, null, PlayerAction.PrisonNotTakeEffect);
      }
      else
      {
        Util.PrintPlayerAction(playerCarrying, null, PlayerAction.NoResponseFromAll);
        cardUsedToJudge = CentralCardDeck.DrawACard();
        Util.PrintPlayerAction(playerCarrying, cardUsedToJudge, PlayerAction.Judge);

        if (CardJudge.CardJudgingResult(cardPrisonCarrying, cardUsedToJudge))
        {
          CardJudge.CardTakeEffect(null, playerCarrying, cardPrisonCarrying, null);
          Util.PrintPlayerAction(playerCarrying, null, PlayerAction.PrisonTakeEffect);
        }
        else
        {
          CardJudge.CardNotTakeEffect(null, null, playerCarrying, cardPrisonCarrying);
          Util.PrintPlayerAction(playerCarrying, null, PlayerAction.PrisonNotTakeEffect);
        }
      }
    }

    void JudgeIndividualBomb(List<IPlayer> playerList, IPlayer playerCarrying)
    {
      ICard cardBombCarrying = playerCarrying.CardCarrying.Find(c => c.CardCarryingType == CarriedType.Bomb);
      if (cardBombCarrying == null)
        // Early out if no bomb found
        return;

      ICard cardUsedToJudge = null;

      //IPlayer playerResponding;
      // Raise the ResponseNeeded event
      ICard cardResponded = AllPlayersCanRespond(cardBombCarrying, null);

      if (cardResponded != null)
      {
        Util.PrintPlayerAction(cardResponded.PlayerPlaying, cardResponded, PlayerAction.Counter);
        CardJudge.CardNotTakeEffect(PlayerList, null, playerCarrying, cardBombCarrying);
        Util.PrintPlayerAction(playerCarrying, null, PlayerAction.BombNotTakeEffect);
      }
      else
      {
        Util.PrintPlayerAction(playerCarrying, null, PlayerAction.NoResponseFromAll);
        cardUsedToJudge = CentralCardDeck.DrawACard();
        Util.PrintPlayerAction(playerCarrying, cardUsedToJudge, PlayerAction.Judge);

        if (CardJudge.CardJudgingResult(cardBombCarrying, cardUsedToJudge))
        {
          CardJudge.CardTakeEffect(null, playerCarrying, cardBombCarrying, null);
          Util.PrintPlayerAction(playerCarrying, null, PlayerAction.BombTakeEffect);
        }
        else
        {
          CardJudge.CardNotTakeEffect(PlayerList, null, playerCarrying, cardBombCarrying);
          Util.PrintPlayerAction(playerCarrying, null, PlayerAction.BombNotTakeEffect);
        }
      }
    }

    void JudgeIndividualJade(ICard cardJade, IPlayer playerPlaying, IPlayer playerReceiving)
    {
      // TODO Do we need to do this everywhere for jade, prison and bomb? Seriously?
      ICard cardUsedToJudge = CentralCardDeck.DrawACard();
      Util.PrintPlayerAction(playerReceiving, cardUsedToJudge, PlayerAction.Judge);

      if (CardJudge.CardJudgingResult(cardJade, cardUsedToJudge))
      {
        CardJudge.CardTakeEffect(playerPlaying, playerReceiving , cardJade, null);
        Util.PrintPlayerAction(playerReceiving, cardJade, PlayerAction.JadeTakeEffect);
      }
      else
      {
        CardJudge.CardNotTakeEffect(null, playerPlaying, playerReceiving, cardJade);
        Util.PrintPlayerAction(playerReceiving, cardJade, PlayerAction.JadeNotTakeEffect);
      }
    }

    void StartACardLifeCycle(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      if (playerPlaying.CardPlayed != null)
        Util.PrintPlayerAction(playerPlaying, playerPlaying.CardNeedResponse, PlayerAction.Play);

      ICard cardTriggering;
      ICard cardFirst;
      cardFirst = cardTriggering = playerPlaying.CardNeedResponse;

      if (!CardJudge.CardHasImmediateEffect(cardFirst))
      {
        // The card played has no immediate effect
        CardJudge.CardGetDeployed(playerReceiving, cardFirst);
        Util.PrintPlayerAction(playerReceiving, cardFirst, PlayerAction.Deployed);
        return;
      }

      List<Func> funcsCanCounter;
      List<Func> funcsCanAnnul;
      bool cardFirstAnnulled = false;

      MiniRoundTimingOption timingCountering = CardJudge.CardCanBeCounteredByAnyPlayer(playerPlaying, cardFirst, cardTriggering, out funcsCanCounter);
      MiniRoundTimingOption timingAnnulling = CardJudge.CardCanBeAnnulled(playerPlaying, cardFirst, cardTriggering, out funcsCanAnnul);

      // TODO This last option varies from card to card
      if (timingCountering != MiniRoundTimingOption.None || timingAnnulling != MiniRoundTimingOption.None)
        cardFirstAnnulled = EnterCardCounteringArena(playerPlaying, playerReceiving, cardTriggering, cardFirst,
          timingCountering, timingAnnulling);

      if (cardFirstAnnulled)
      {
        Util.PrintPlayerAction(playerReceiving, cardFirst, PlayerAction.Annulled);
        return;
      }

      // The card requires drawing more cards from the central card deck
      List<ICard> cardsExtraDrawn = null;
      if (CardJudge.CardNeedExtraCard(cardFirst))
      {
        cardsExtraDrawn = CentralCardDeck.DrawCards(CardJudge.CardNeedNumOfExtraCards(cardFirst, cardFirst));
        Util.PrintPlayerAction(playerReceiving, cardFirst, cardsExtraDrawn, PlayerAction.Drawn);
      }

      CardJudge.CardTakeEffect(playerPlaying, playerReceiving, cardFirst, cardsExtraDrawn);
    }

    ICard AllPlayersCanRespond(ICard cardTriggering, ICard cardFirst)
    {
      foreach (IPlayer player in PlayerList)
      {
        if (cardFirst != null && player == cardFirst.PlayerPlaying ||
          cardTriggering != null && player == cardTriggering.PlayerPlaying)
          continue;

        // Raise the ResponseNeeded event
        player.Messenger.OnResponseNeeded(new SinglePlayerResponseArgs(cardTriggering, null, cardFirst));
        if (player.CardResponded != null)
          return player.CardResponded;
      }

      return null;
    }

    ICard SinglePlayerMustRespond(IPlayer playerReceiving, ICard cardTriggering, ICard cardFirst)
    {
      // Raise the ResponseNeeded event
      playerReceiving.Messenger.OnResponseNeeded(new SinglePlayerResponseArgs(cardTriggering, null, cardFirst));
      if (playerReceiving.CardResponded != null)
      {
        Util.PrintPlayerAction(playerReceiving, playerReceiving.CardResponded, PlayerAction.Counter);
        return playerReceiving.CardResponded;
      }
      else
      {
        Util.PrintPlayerAction(playerReceiving, null, PlayerAction.NoResponse);
      }

      return null;
    }

    bool EnterCardCounteringArena(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardTriggering, ICard cardFirst,
      MiniRoundTimingOption timingCountering, MiniRoundTimingOption timingAnnulling)
    {
      ICard cardResponded;
      bool cardFirstAnnulled = false;
      bool firstMiniRound = true;

      do
      {
        cardResponded = null;

        //IPlayer playerResponding;
        if (timingCountering == MiniRoundTimingOption.Always ||
            timingCountering == MiniRoundTimingOption.FirstMiniRound && firstMiniRound)
          cardResponded = AllPlayersCanRespond(cardTriggering, cardFirst);

        if (cardResponded == null)
          // No player has responded with a card as the third party
          cardResponded = SinglePlayerMustRespond(playerReceiving, cardTriggering, cardFirst);

        if (cardResponded == null)
          // There is no card played as the response to the card just been played
          break;

        Util.PrintPlayerAction(cardResponded.PlayerPlaying, cardResponded, PlayerAction.Counter);
        cardFirstAnnulled = !cardFirstAnnulled;

        // This card just being played is an annulling card and cannot be countered
        if (cardResponded.IsAnnulling)
          break;

        //// TODO Remove this paragraph
        //  // This card just been played cannot be countered
        //  if (CardJudge.CardCanBeCounteredByAnyPlayer(playerPlaying, cardFirst, cardResponded, out funcsCanCounter) == MiniRoundTimingOption.None)
        //  {
        //    // This card just been played cannot be annulled
        //    if (CardJudge.CardCanBeAnnulled(playerPlaying, cardFirst, cardResponded, out funcsCanAnnul) == MiniRoundTimingOption.None)
        //      break;
        //  }

        // Swap players so the player who plays card first in this mini-round needs to respond in the next mini-round
        Util.SwapPlayers(ref playerPlaying, ref playerReceiving);

        firstMiniRound = false;
      }
      while (cardResponded != null);

      return cardFirstAnnulled;
    }

    void EnterOneOnOneArena()
    {
      ;
    }

    bool IsGameOver()
    {
      // TODO This is going to be changed
      return !PlayerList.Exists(p => p.CardsHeld.Count > 0) && !PlayerList.Exists(p => p.CardCarrying != null && p.CardCarrying.Count > 0);
    }

  }
}
