using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class ParryAllCard : PlayedCard
  {
    public ParryAllCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.ParryAll;
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return  MiniRoundTimingOption.Always;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra) { }

  }
}
