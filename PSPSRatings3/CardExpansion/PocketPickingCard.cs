using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class PocketPickingCard : PlayedCard
  {
    public PocketPickingCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.PocketPicking;
    }

    public override bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      if (playerReceiving.CardsHeld.Count == 0 && playerReceiving.CardCarrying.Count == 0 && playerReceiving.CardEquipped.Count == 0)
        return false;

      return true;
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll };
      return MiniRoundTimingOption.Always;
    }

    // TODO This needs to be done interactively
    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      ICard cardToPick = playerReceiving.CardsHeld[0];
      playerReceiving.CardsHeld.RemoveAt(0);
      playerPlaying.CardsHeld.Add(cardToPick);
    }
  }
}
