using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class HealingAllCard : PlayedCard
  {
    public HealingAllCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.HealingAll;
    }

    // TODO If no player needs healing, the card should not be allowed to be played
    // public override bool CanBePlayed(IPlayer playerPlaying);

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return MiniRoundTimingOption.Always;
    }

    public override AffectedPlayer AffectNumOfPlayers() { return AffectedPlayer.All; }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      if (playerReceiving.PlayerFigure.HitPoint < playerReceiving.PlayerFigure.MaxHitPoint)
        playerReceiving.PlayerFigure.HitPoint++;
    }

  }
}
