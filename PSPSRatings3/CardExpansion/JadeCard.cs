using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  class JadeCard : PlayedCard
  {
    public JadeCard(ICard card)
    {
      this.CardSuit = card.CardSuit;
      this.CardRank = card.CardRank;
      this.CardFunc = Func.Jade;
      this.CardEquippingType = EquippedType.Shield;
    }

    public override bool HasImmediateEffect() { return false; }

    public override void GetDeployed(IPlayer playerReceiving)
    {
      this.CardAction = CAction.GetEquipped;
      playerReceiving.ReplaceEquippable(this);
    }
    
  public override bool NeedJudgingCard() { return true; }

    public override bool JudgingResult(ICard cardUsedToJudge)
    {
      return cardUsedToJudge.CardSuit == Suit.Heart || cardUsedToJudge.CardSuit == Suit.Diamond;
    }

    public override void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra)
    {
      // Is this enough?
      playerReceiving.CardResponded = new Card(Func.Dodge);
    }
  }
}
