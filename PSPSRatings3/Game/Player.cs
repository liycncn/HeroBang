using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.CardExpansion;

namespace HeroBang.Game
{
  public class SinglePlayerResponseArgs : EventArgs
  {
    ICard cardTriggering;
    public ICard CardTriggering { get { return cardTriggering; } }
    List<ICard> miniCardList;
    public List<ICard> MiniCardList { get { return miniCardList; } }
    ICard cardFirst;
    public ICard CardFirst { get { return cardFirst; } }
    // TODO Add the list of players to request a card from 

    public SinglePlayerResponseArgs(ICard cardT, List<ICard> miniCards, ICard cardF)
    {
      this.cardTriggering = cardT;
      this.miniCardList = miniCards;
      this.cardFirst = cardF;
    }
  }

  public interface IPlayer
  {
    IFigure PlayerFigure { get; set; }

    List<ICard> CardCarrying { get; set; }
    bool InPrison { get; set; }
    List<ICard> CardEquipped { get; set; }

    ICard CardPlayed { get; set; }
    ICard CardResponded { get; set; }
    ICard CardNeedResponse { get; set; }

    List<ICard> CardsHeld { get; set; }
    List<ICard> CardsCanBePlayedInThisRound { get; set; }
    List<ICard> CardsPlayedInThisRound { get; set; }

    IPlayer PlayerToKill { get; set; }
    IPlayer PlayerToProtect { get; set; }

    PlayerMessenger Messenger { get; set; }

    void PrintPlayedCard(ICard card);
  }

  public class FacelessPlayer : IPlayer
  {
    public IFigure PlayerFigure { get; set; }

    public List<ICard> CardCarrying { get; set; }
    public bool InPrison { get; set; }
    public List<ICard> CardEquipped { get; set; }

    public ICard CardPlayed { get; set; }
    public ICard CardResponded { get; set; }
    public ICard CardNeedResponse { get; set; }

    public List<ICard> CardsHeld { get; set; }
    public List<ICard> CardsCanBePlayedInThisRound { get; set; }
    public List<ICard> CardsPlayedInThisRound { get; set; }

    // TODO Both these variables need to become lists
    public IPlayer PlayerToKill { get; set; }
    public IPlayer PlayerToProtect { get; set; }

    public PlayerMessenger Messenger { get; set; }

    public FacelessPlayer(FacelessFigure initialFigure, CardTable initialTable)
    {
      PlayerFigure = initialFigure;

      Messenger = new PlayerMessenger();
      Messenger.PlayerTurnStarts += new PlayerMessenger.PlayerTurnStartsEventHandler(Messenger_PlayerTurnStarts);
      Messenger.PlayAnotheCard += new PlayerMessenger.PlayAnotheCardEventHandler(Messenger_PlayAnotheCard);
      Messenger.ResponseNeeded += new PlayerMessenger.ResponseNeededEventHandler(Messenger_ResponseNeeded);
      Messenger.PlayerTurnEnds += new PlayerMessenger.PlayerTurnEndsEventHandler(Messenger_PlayerTurnEnds);

      CardCarrying = new List<ICard>();
      CardEquipped = new List<ICard>();

      CardsPlayedInThisRound = new List<ICard>();
      CardsCanBePlayedInThisRound = new List<ICard>();
    }

    ICard PlayACard(ICard card)
    {
      return PlayACard(card, null, false);
    }

    ICard PlayACard(ICard card, IPlayer playerReceiving, bool isAnnulling)
    {
      if (card == null)
        return null;

      CardsHeld.Remove(card);
      CardsPlayedInThisRound.Add(card);
      card.PlayerPlaying = this;
      card.PlayerReceiving = playerReceiving ?? this;
      card.IsAnnulling = isAnnulling;

      return card;
    }

    public void PrintPlayedCard(ICard card)
    {
      Util.PrintPlayerAction(this, card, PlayerAction.Play);
    }

    void DecideCardsCanBePlayedInYourTurn()
    {
      CardsCanBePlayedInThisRound.Clear();

      // Cannot play any card if the player is in prison
      if (InPrison)
        return;

      foreach (ICard card in CardsHeld)
      {
        if (CardJudge.CanPlayerPlayCard(this, this.PlayerToKill, card))
          CardsCanBePlayedInThisRound.Add(card);
      }
    }

    ICard DecideWhatCardToPlayInYourTurn()
    {
      ICard card;

      // TODO Think about the big picture - Advanced 
      card = PlayCardBasedOnBigPicture();
      if (card != null)
      {
        return PlayACard(card);
      }

      // Heal yourself and your alliance
      card = PlayCardToHeal();
      if (card != null)
      {
        // TODO The player does not always heal himself
        return PlayACard(card);
      }

      // Play beneficial cards for yourself and your alliance
      card = PlayBeneficialCard();
      if (card != null)
      {
        return PlayACard(card);
      }

      // Equip yourself and your alliance
      card = PlayEquippableCard();
      if (card != null)
      {
        return PlayACard(card);
      }

      // No player to kill
      if (PlayerToKill == null)
        return null;

      card = PlayOffensiveCard();
      if (card != null)
      {
        return PlayACard(card);
      }

      // Use the bomb
      card = PlayBombCard();
      if (card != null)
      {
        return PlayACard(card);
      }

      // Use the prison
      card = PlayPrisonCard();
      if (card != null)
      {
        return PlayACard(card, PlayerToKill, false); // TODO The player to kill is not always the same as the player to prison
      }

      // TODO No player to protect

      return null;
    }

    void Messenger_PlayerTurnStarts(PlayerMessenger messenger)
    {
      Messenger_PlayAnotheCard(messenger);
    }

    void Messenger_PlayAnotheCard(PlayerMessenger messenger)
    {
      DecideCardsCanBePlayedInYourTurn();
      this.CardPlayed = this.CardNeedResponse = DecideWhatCardToPlayInYourTurn();
    }

    void Messenger_ResponseNeeded(PlayerMessenger messenger, SinglePlayerResponseArgs e)
    {
      this.CardResponded = DecideWhatCardToPlayOutOfYourTurn(e);
    }

    void Messenger_PlayerTurnEnds(PlayerMessenger messenger)
    {
      FinishYourRound();
    }

    ICard DecideWhatCardToPlayOutOfYourTurn(SinglePlayerResponseArgs e)
    {
      ICard cardTriggering = e.CardTriggering;
      List<ICard> miniCardList = e.MiniCardList;
      ICard cardFirst = e.CardFirst;

      List<Func> funcsCanCounter;
      List<Func> funcsCanAnnul;
      ICard cardResponded= null;

      if (CardJudge.CardCanBeCounteredByAnyPlayer(null, cardFirst, cardTriggering, out funcsCanCounter) != MiniRoundTimingOption.None)
        cardResponded = PlayACard(this.FindASpecificCard(funcsCanCounter), PlayerToKill, false);

      if (cardResponded != null)
        return cardResponded;

      if (CardJudge.CardCanBeAnnulled(null, cardFirst, cardTriggering, out funcsCanAnnul) != MiniRoundTimingOption.None)
        cardResponded = PlayACard(this.FindASpecificCard(funcsCanAnnul), PlayerToKill, true);

      if (cardResponded != null)
        return cardResponded;

      return null; // Make no response
    }

    //ICard DecideWhatCardToPlayOutOfYourTurn(SinglePlayerResponseArgs e)
    //{
    //  ICard cardTriggering = e.CardTriggering;
    //  List<ICard> miniCardList = e.MiniCardList;
    //  ICard cardFirst = e.CardFirst;

    //  if (cardFirst != null && cardFirst != cardTriggering)
    //  {
    //    if (cardFirst.CardFunc == Func.Duel && cardTriggering.CardFunc == Func.Kill)
    //      return PlayACard(this.FindASpecificCard(Func.Kill), PlayerToKill);
    //  }

    //  if (cardTriggering.IsOffensive())
    //  {
    //    if (cardTriggering.CardFunc == Func.BarbariansAtGate || cardTriggering.CardFunc == Func.Duel)
    //      return PlayACard(this.FindASpecificCard(Func.Kill, Func.ParryAll), PlayerToKill);
    //    else if (cardTriggering.CardFunc == Func.Kill || cardTriggering.CardFunc == Func.TenKArrows)
    //      return PlayACard(this.FindASpecificCard(Func.Dodge, Func.ParryAll));
    //    else if (cardTriggering.CardFunc == Func.PickAndDrop ||
    //      cardTriggering.CardFunc == Func.PocketPicking || cardTriggering.CardFunc == Func.Prison)
    //      return PlayACard(this.FindASpecificCard(Func.ParryAll));
    //    else
    //      throw new NotImplementedException("Unrecognised offensive card");
    //  }
    //  else if (cardTriggering.IsBeneficial())
    //  {
    //    if (cardTriggering.CardFunc == Func.FreeCardForAll)
    //      return PlayACard(this.PickARandomCard(miniCardList)); // TODO Need a more sophiscated picking card strategy
    //    else if (cardTriggering.CardFunc == Func.HealingAll || cardTriggering.CardFunc == Func.TwoCardsFromOne)
    //      return null; // Do nothing
    //    else
    //      throw new NotImplementedException("Unrecognised beneficial card");
    //  }
    //  else if (cardTriggering.IsEquippable())
    //  {
    //    return null; // Do nothing
    //  }
    //  else if (cardTriggering.CannotBeInterrupted())
    //  {
    //    if (cardTriggering.IsBomb())
    //      return PlayACard(this.FindASpecificCard(Func.ParryAll));
    //    else
    //      return null; // Do nothing
    //  }
    //  else
    //  {
    //    throw new NotImplementedException("Undefined action");
    //  }
    //}

    ICard PlayCardBasedOnBigPicture()
    {
      return null;
    }

    ICard PlayCardToHeal()
    {
      //if (!PlayerFigure.NeedHealing())
      //  return null;

      // TODO Need to consider if your opponent/alliance needs to be healed 
      // and if you have the card ParryAll
      ICard cardHealingAll = CardsCanBePlayedInThisRound.Find(c => c.CardFunc == Func.HealingAll);
      if (cardHealingAll != null)
        return cardHealingAll;

      ICard cardPortion = CardsCanBePlayedInThisRound.Find(c => c.CardFunc == Func.Portion);
      if (cardPortion != null)
        return cardPortion;

      return null;
    }

    ICard PlayBeneficialCard()
    {
      // TODO Make all Play***Card functions like this
      return CardsCanBePlayedInThisRound.Find(c => c.IsBeneficial());
    }

    ICard PlayEquippableCard()
    {
      ICard cardEquippable = CardsCanBePlayedInThisRound.Find(c => c.IsEquippable());
      if (cardEquippable != null)
        return cardEquippable;

      return null;
    }

    ICard PlayOffensiveCard()
    {
      ICard cardOffensive = CardsCanBePlayedInThisRound.Find(c => c.IsOffensive());
      if (cardOffensive != null)
        return cardOffensive;

      return null;
    }

    ICard PlayBombCard()
    {
      ICard cardBomb = CardsCanBePlayedInThisRound.Find(c => c.IsBomb());
      if (cardBomb != null)
        return cardBomb;

      return null;
    }

    ICard PlayPrisonCard()
    {
      ICard cardPrison = CardsCanBePlayedInThisRound.Find(c => c.IsPrison());
      if (cardPrison != null)
        return cardPrison;

      return null;
    }

    void FinishYourRound()
    {
      CardsPlayedInThisRound.Clear();
      CardsCanBePlayedInThisRound.Clear();
      CardPlayed = null;
      CardResponded = null;
      CardNeedResponse = null;
      InPrison = false;
    }

  }
}
