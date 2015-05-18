using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class DuelCard : PlayedCard
  {
    public DuelCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Duel;
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return MiniRoundTimingOption.Always;
    }

    public override bool TriggerOneOnOne() { return true; }

    public override MiniRoundTimingOption CanBeCounteredInOneOnOne(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.Kill };
      return MiniRoundTimingOption.Always;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.PlayerFigure.HitPoint--;
    }

    //public bool ActionResult(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardResponded)
    //{
    //  if (cardResponded != null && (cardResponded.CardFunc == Func.Kill || cardResponded.CardFunc == Func.ParryAll))
    //    {
    //      if (cardResponded.CardFunc == Func.ParryAll)
    //        return false;
    //      else
    //        return true;
    //  }

    //  playerReceiving.PlayerFigure.HitPoint--;
    //  return false;
    //}
  }
}
