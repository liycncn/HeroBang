using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class PrisonCard : PlayedCard
  {
    public PrisonCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Prison;
      this.CardCarryingType = CarriedType.Prison;
    }

    public override bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      // TODO return false if there the receiving player already carries a prison card
      return true;
    }

    public override bool HasImmediateEffect() { return false; }

    public override void GetDeployed(IPlayer playerReceiving)
    {
      playerReceiving.CardCarrying.Add(this);
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return MiniRoundTimingOption.Always;
    }

    public override bool NeedJudgingCard() { return true; }

    public override bool JudgingResult(ICard cardUsedToJudge)
    {
      return cardUsedToJudge.CardSuit != Suit.Heart;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.CardCarrying.RemoveCard(this);
      playerReceiving.InPrison = true;
    }

    public override void NotTakeEffect(List<IPlayer> playerList, IPlayer playerPlaying, IPlayer playerReceiving)
    {
      playerReceiving.CardCarrying.RemoveCard(this);
    }

    //public bool ActionResult(IPlayer playerCarrying, ICard cardResponded, ICard cardUsedToJudge)
    //{
    //  if (cardResponded != null && cardResponded.CardFunc == Func.ParryAll)
    //    return false;

    //  playerCarrying.PrisonCarrying = null;
    //  return playerCarrying.InPrison = WillPrisonWork(cardUsedToJudge);
    //}

  }
}
