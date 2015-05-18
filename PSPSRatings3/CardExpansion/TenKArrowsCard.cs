using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class TenKArrowsCard : Card
  {
    public TenKArrowsCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.TenKArrows;
    }

    public bool CanBePlayed(IPlayer playerPlaying)
    {
      return true;
    }

    public void ActionResult(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardResponded)
    {
      if (cardResponded != null && (cardResponded.CardFunc == Func.Dodge || cardResponded.CardFunc == Func.ParryAll))
        return;

      playerReceiving.PlayerFigure.HitPoint--;
    }
  }
}
