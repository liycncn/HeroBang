using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class BarbariansAtGateCard : PlayedCard
  {
    public BarbariansAtGateCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.BarbariansAtGate;
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return  MiniRoundTimingOption.Always;
    }

    public override MiniRoundTimingOption CanBeAnnulled(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanAnnul)
    {
      funcsCanAnnul = new List<Func>() { Func.Kill };
      return  MiniRoundTimingOption.FirstMiniRound;
    }

    public override AffectedPlayer AffectNumOfPlayers() { return AffectedPlayer.AllButMyself; }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.PlayerFigure.HitPoint--;
    }

  }
}