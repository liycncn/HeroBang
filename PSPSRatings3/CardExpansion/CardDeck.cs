using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.General;

namespace HeroBang.CardExpansion
{
  public class CardDeck
  {
    SpecialQueue<ICard> CurrentDeck { get; set; }

    public CardDeck()
    {
      List<ICard> cardList = InitialAllCards();
      cardList.Shuffle();
      CurrentDeck = new SpecialQueue<ICard>(cardList);
    }

    public void PrintCardDeck()
    {
      foreach (ICard card in CurrentDeck.ConvertToList())
      {
        Console.WriteLine(string.Format("ICard: {0} {1} {2}", card.CardSuit, card.CardRank, card.CardFunc));
      }
      Console.WriteLine(string.Format("{0} cards in the deck", CurrentDeck.Count));
      Console.ReadLine();
    }

    public List<ICard> InitialAllCards()
    {
      return new List<ICard>() {
        // Rank Ace
        new Card (Suit.Diamond, Rank.Ace, Func.TenKArrows), 
        new Card (Suit.Diamond, Rank.Ace, Func.HealingAll), 
        new Card (Suit.Heart, Rank.Ace, Func.Duel), 
        new Card (Suit.Heart, Rank.Ace, Func.Tiger), 
        new Card (Suit.Club, Rank.Ace, Func.Bomb), 
        new Card (Suit.Club, Rank.Ace, Func.Duel), 
        new Card (Suit.Spade, Rank.Ace, Func.Tiger), 
        new Card (Suit.Spade, Rank.Ace, Func.Duel), 
        // Rank Two
        new Card (Suit.Diamond, Rank.Two, Func.Dodge), 
        new Card (Suit.Diamond, Rank.Two, Func.Dodge), 
        new Card (Suit.Heart, Rank.Two, Func.Dodge), 
        new Card (Suit.Heart, Rank.Two, Func.Dodge), 
        new Card (Suit.Club, Rank.Two, Func.Jade), 
        new Card (Suit.Club, Rank.Two, Func.BroadSword), 
        new Card (Suit.Spade, Rank.Two, Func.Kill), 
        new Card (Suit.Spade, Rank.Two, Func.Jade), 
        // Rank Three
        new Card (Suit.Diamond, Rank.Three, Func.Portion), 
        new Card (Suit.Diamond, Rank.Three, Func.FreeCardForAll), 
        new Card (Suit.Heart, Rank.Three, Func.Dodge), 
        new Card (Suit.Heart, Rank.Three, Func.PocketPicking), 
        new Card (Suit.Club, Rank.Three, Func.PocketPicking), 
        new Card (Suit.Club, Rank.Three, Func.PickAndDrop), 
        new Card (Suit.Spade, Rank.Three, Func.PickAndDrop), 
        new Card (Suit.Spade, Rank.Three, Func.Kill), 
        // Rank Four
        new Card (Suit.Diamond, Rank.Four, Func.FreeCardForAll), 
        new Card (Suit.Diamond, Rank.Four, Func.Portion), 
        new Card (Suit.Heart, Rank.Four, Func.PocketPicking), 
        new Card (Suit.Heart, Rank.Four, Func.Dodge), 
        new Card (Suit.Club, Rank.Four, Func.PickAndDrop), 
        new Card (Suit.Club, Rank.Four, Func.PocketPicking), 
        new Card (Suit.Spade, Rank.Four, Func.PickAndDrop), 
        new Card (Suit.Spade, Rank.Four, Func.Kill), 
        // Rank Five
        new Card (Suit.Diamond, Rank.Five, Func.DefensiveHorse), 
        new Card (Suit.Diamond, Rank.Five, Func.LongBow), 
        new Card (Suit.Heart, Rank.Five, Func.ChainHammer), 
        new Card (Suit.Heart, Rank.Five, Func.Dodge), 
        new Card (Suit.Club, Rank.Five, Func.DefensiveHorse), 
        new Card (Suit.Club, Rank.Five, Func.LongStick),         
        new Card (Suit.Spade, Rank.Five, Func.Kill), 
        new Card (Suit.Spade, Rank.Five, Func.DefensiveHorse), 
        // Rank Six
        new Card (Suit.Diamond, Rank.Six, Func.Prison), 
        new Card (Suit.Diamond, Rank.Six, Func.Portion),         
        new Card (Suit.Heart, Rank.Six, Func.Dodge), 
        new Card (Suit.Heart, Rank.Six, Func.Kill), 
        new Card (Suit.Club, Rank.Six, Func.ShortSword), 
        new Card (Suit.Club, Rank.Six, Func.Prison), 
        new Card (Suit.Spade, Rank.Six, Func.Kill), 
        new Card (Suit.Spade, Rank.Six, Func.Prison), 
        // Rank Seven
        new Card (Suit.Diamond, Rank.Seven, Func.TwoCardsFromOne), 
        new Card (Suit.Diamond, Rank.Seven, Func.Portion),         
        new Card (Suit.Heart, Rank.Seven, Func.Dodge), 
        new Card (Suit.Heart, Rank.Seven, Func.Kill), 
        new Card (Suit.Club, Rank.Seven, Func.BarbariansAtGate), 
        new Card (Suit.Club, Rank.Seven, Func.Kill), 
        new Card (Suit.Spade, Rank.Seven, Func.Kill), 
        new Card (Suit.Spade, Rank.Seven, Func.BarbariansAtGate), 
        // Rank Eight
        new Card (Suit.Diamond, Rank.Eight, Func.TwoCardsFromOne), 
        new Card (Suit.Diamond, Rank.Eight, Func.Portion), 
        new Card (Suit.Heart, Rank.Eight, Func.Kill), 
        new Card (Suit.Heart, Rank.Eight, Func.Dodge), 
        new Card (Suit.Club, Rank.Eight, Func.Kill), 
        new Card (Suit.Club, Rank.Eight, Func.Kill), 
        new Card (Suit.Spade, Rank.Eight, Func.Kill), 
        new Card (Suit.Spade, Rank.Eight, Func.Kill), 
        // Rank Nine
        new Card (Suit.Diamond, Rank.Nine, Func.Portion), 
        new Card (Suit.Diamond, Rank.Nine, Func.TwoCardsFromOne), 
        new Card (Suit.Heart, Rank.Nine, Func.Dodge), 
        new Card (Suit.Heart, Rank.Nine, Func.Kill), 
        new Card (Suit.Club, Rank.Nine, Func.Kill), 
        new Card (Suit.Club, Rank.Nine, Func.Kill), 
        new Card (Suit.Spade, Rank.Nine, Func.Kill), 
        new Card (Suit.Spade, Rank.Nine, Func.Kill), 
        // Rank Ten
        new Card (Suit.Diamond, Rank.Ten, Func.Kill), 
        new Card (Suit.Diamond, Rank.Ten, Func.Kill), 
        new Card (Suit.Heart, Rank.Ten, Func.Kill), 
        new Card (Suit.Heart, Rank.Ten, Func.Dodge), 
        new Card (Suit.Club, Rank.Ten, Func.Kill), 
        new Card (Suit.Club, Rank.Ten, Func.Kill), 
        new Card (Suit.Spade, Rank.Ten, Func.Kill), 
        new Card (Suit.Spade, Rank.Ten, Func.Kill), 
        // Rank Jack
        new Card (Suit.Diamond, Rank.Jack, Func.TwoCardsFromOne), 
        new Card (Suit.Diamond, Rank.Jack, Func.Kill), 
        new Card (Suit.Heart, Rank.Jack, Func.Dodge), 
        new Card (Suit.Heart, Rank.Jack, Func.Dodge), 
        new Card (Suit.Club, Rank.Jack, Func.ParryAll), 
        new Card (Suit.Club, Rank.Jack, Func.PocketPicking), 
        new Card (Suit.Spade, Rank.Jack, Func.Kill), 
        new Card (Suit.Spade, Rank.Jack, Func.Kill), 
        // Rank Queen
        new Card (Suit.Diamond, Rank.Queen, Func.PickAndDrop), 
        new Card (Suit.Diamond, Rank.Queen, Func.Bomb), 
        new Card (Suit.Heart, Rank.Queen, Func.NailHammer), 
        new Card (Suit.Heart, Rank.Queen, Func.ParryAll), 
        new Card (Suit.Club, Rank.Queen, Func.Spear), 
        new Card (Suit.Club, Rank.Queen, Func.PickAndDrop), 
        new Card (Suit.Spade, Rank.Queen, Func.ParryAll), 
        new Card (Suit.Spade, Rank.Queen, Func.BorrowingWeapon), 
        // Rank King
        new Card (Suit.Diamond, Rank.King, Func.Dodge), 
        // No card here
        new Card (Suit.Heart, Rank.King, Func.OffensiveHorse), 
        new Card (Suit.Heart, Rank.King, Func.Kill), 
        new Card (Suit.Club, Rank.King, Func.BarbariansAtGate), 
        new Card (Suit.Club, Rank.King, Func.OffensiveHorse), 
        new Card (Suit.Spade, Rank.King, Func.BorrowingWeapon), 
        new Card (Suit.Spade, Rank.King, Func.ParryAll), 
        // Duplicate
        new Card (Suit.Diamond, Rank.Five, Func.OffensiveHorse), 
        new Card (Suit.Heart, Rank.Queen, Func.Portion), 
        new Card (Suit.Diamond, Rank.Queen, Func.Portion)
      };
    }

    public void PutCardBackToDeck(ICard card)
    {
      CurrentDeck.Enqueue(card);
    }

    public List<ICard> DealCards(int numOfCards)
    {
      List<ICard> cardDealt = new List<ICard>();
      for (int i = 0; i < numOfCards; i++)
      {
        cardDealt.Add(CurrentDeck.Dequeue());
      }

      return cardDealt;
    }

    public ICard DrawSpecificCard(Func cardFunc)
    {
      return CurrentDeck.FirstAndRemove(c => c.CardFunc == cardFunc);
    }

    public ICard DrawACard()
    {
      return CurrentDeck.Dequeue();
    }

    public List<ICard> DrawCards(int numOfCards)
    {
      return DealCards(numOfCards);
    }

  }
}
