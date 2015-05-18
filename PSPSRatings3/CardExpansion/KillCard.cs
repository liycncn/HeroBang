using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class KillCard : PlayedCard
  {
    public KillCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Kill;
    }

    public override bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      return !playerPlaying.CardsPlayedInThisRound.Exists(c => c.CardFunc == Func.Kill);
    }

    public override MiniRoundTimingOption CanBeAnnulled(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanAnnul)
    {
      funcsCanAnnul = new List<Func>() { Func.Dodge };
      return MiniRoundTimingOption.FirstMiniRound;
    }

    public override MiniRoundTimingOption CanBeAnnulledByEquippable(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanAnnul)
    {
      funcsCanAnnul = new List<Func>() { Func.Jade };
      return MiniRoundTimingOption.FirstMiniRound;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.PlayerFigure.HitPoint--;
    }

    //public bool ActionResult(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardResponded, ICard cardFirst)
    //{
    //  if (cardResponded != null)
    //  {
    //    if (cardFirst != null && cardFirst.CardFunc == Func.Duel && cardResponded.CardFunc == Func.Kill)
    //      return true;

    //    if (cardFirst == null && cardResponded.CardFunc == Func.Dodge)
    //      return false;
    //  }

    //  playerReceiving.PlayerFigure.HitPoint--;
    //  return false;
    //}
  }
}
