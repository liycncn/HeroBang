using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  public interface ICard
  {
    Suit CardSuit { get; set; }
    Rank CardRank { get; set; }
    Func CardFunc { get; set; }
    IPlayer PlayerPlaying { get; set; }
    IPlayer PlayerReceiving { get; set; }
    CarriedType CardCarryingType { get; set; }
    EquippedType CardEquippingType { get; set; }
    bool IsAnnulling { get; set; }
   CAction CardAction { get; set; } 
    bool IsSameAs(ICard card);
  }

  public class Card : ICard, IDisposable
  {
    public Suit CardSuit { get; set; }
    public Rank CardRank { get; set; }
    public Func CardFunc { get; set; }
    public IPlayer PlayerPlaying { get; set; }
    public IPlayer PlayerReceiving { get; set; }
    public CarriedType CardCarryingType { get; set; }
    public EquippedType CardEquippingType { get; set; }
    public bool IsAnnulling { get; set; }
    public CAction CardAction { get; set; }

    public Card() { }

    public Card(Func func)
    {
      this.CardSuit = Suit.None;
      this.CardRank = Rank.None;
      this.CardFunc = func;
    }

    public Card(Suit suit, Rank rank, Func func)
    {
      this.CardSuit = suit;
      this.CardRank = rank;
      this.CardFunc = func;
      this.CardAction = CAction.TakeEffect;
    }

    public bool IsSameAs(ICard card)
    {
      return card.CardCarryingType == this.CardCarryingType && card.CardEquippingType == this.CardEquippingType &&
        card.CardFunc == this.CardFunc && card.CardRank == this.CardRank && card.CardSuit == this.CardSuit;
    }

    public void Dispose()
    {
      // TODO Do we need do something here?
    }

  }
}
