using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class BombCard : PlayedCard
  {
    public BombCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Bomb;
      this.CardCarryingType = CarriedType.Bomb;
    }

    public override bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving)
    {
      // TODO return false if there are more bombs than alive players
      return true;
    }

    public override bool HasImmediateEffect() { return false; }

    public override void GetDeployed(IPlayer playerReceiving)
    {
      playerReceiving.CardCarrying.Add(this);
    }

    public override MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { Func.ParryAll};
      return  MiniRoundTimingOption.Always;
    }

    public override bool NeedJudgingCard() { return true; }

    public override bool JudgingResult(ICard cardUsedToJudge)
    {
      return cardUsedToJudge.CardSuit == Suit.Spade && cardUsedToJudge.RankBetween(Rank.Two, Rank.Nine);
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      playerReceiving.PlayerFigure.HitPoint -= 3;
      playerReceiving.CardCarrying.RemoveCard(this);
    }

    public override void NotTakeEffect(List<IPlayer> playerList, IPlayer playerPlaying, IPlayer playerReceiving)
    {
      int indexPlayerCarrying = playerList.IndexOf(playerReceiving);
      int indexPlayerNext = Util.GetNextPlayerIndex(playerList.Count, indexPlayerCarrying);

      IPlayer playerNext = playerList[indexPlayerNext];
      playerNext.CardCarrying.Add(this);
      playerReceiving.CardCarrying.RemoveCard(this);
    }

    //public void ActionResult(IPlayer playerCarrying, ICard cardResponded, ICard cardUsedToJudge)
    //{
    //  if (cardResponded != null && cardResponded.CardFunc == Func.ParryAll)
    //    return;

    //  if (WillBombExplode(cardUsedToJudge))
    //  {
    //    playerCarrying.PlayerFigure.HitPoint -= 3;
    //    playerCarrying.BombCarrying = null;
    //  }
    //}

  }
}