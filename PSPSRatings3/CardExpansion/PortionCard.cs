using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class PortionCard : PlayedCard
  {
    public PortionCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Portion;
    }

    public override bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      return playerReceiving.PlayerFigure.HitPoint < playerReceiving.PlayerFigure.MaxHitPoint;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.PlayerFigure.HitPoint++;
    }

    //  // Default the action of a player using portion to the player himself
    //  public void ActionResult(IPlayer playerPlaying)
    //  {
    //    playerPlaying.PlayerFigure.HitPoint++;
    //  }

    //  public void ActionResult(IPlayer playerPlaying, IPlayer playerReceiving)
    //  {
    //    playerReceiving.PlayerFigure.HitPoint++;
    //  }
  }
}
