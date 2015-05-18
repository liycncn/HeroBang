using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.CardExpansion;
using HeroBang.Game;

namespace HeroBang
{
  static class Util
  {
    public static void Shuffle<T>(this IList<T> list)
    {
      Random rng = new Random();
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    public static bool IsOffensive(this ICard card)
    {
      return (card.CardFunc == Func.BarbariansAtGate || card.CardFunc == Func.Duel || card.CardFunc == Func.Kill || card.CardFunc == Func.PickAndDrop ||
        card.CardFunc == Func.PocketPicking || card.CardFunc == Func.TenKArrows);
    }

    public static bool IsDefensive(this ICard card)
    {
      return (card.CardFunc == Func.Dodge || card.CardFunc == Func.ParryAll);
    }

    public static bool IsEquippable(this ICard card)
    {
      return (card.CardFunc == Func.BroadSword || card.CardFunc == Func.ChainHammer || card.CardFunc == Func.DefensiveHorse ||
        card.CardFunc == Func.Jade || card.CardFunc == Func.LongBow || card.CardFunc == Func.LongStick || card.CardFunc == Func.NailHammer ||
        card.CardFunc == Func.OffensiveHorse || card.CardFunc == Func.ShortSword || card.CardFunc == Func.Spear || card.CardFunc == Func.Tiger);
    }

    public static bool IsBeneficial(this ICard card)
    {
      return (card.CardFunc == Func.FreeCardForAll || card.CardFunc == Func.HealingAll || card.CardFunc == Func.TwoCardsFromOne);
    }

    public static bool CannotBeInterrupted(this ICard card)
    {
      return (card.CardFunc == Func.Portion || card.CardFunc == Func.Bomb || card.CardFunc == Func.Prison);
    }

    public static bool IsBomb(this ICard card)
    {
      return (card.CardFunc == Func.Bomb);
    }

    public static bool IsJade(this ICard card)
    {
      return (card.CardFunc == Func.Jade);
    }

    public static bool IsPrison(this ICard card)
    {
      return (card.CardFunc == Func.Prison);
    }

    public static void PrintPlayerAction(IPlayer player, ICard cardPlayed, PlayerAction action)
    {
      PrintPlayerAction(player, cardPlayed, null, action);
    }

    public static void PrintPlayerAction(IPlayer player, ICard cardPlayed, List<ICard> cardsExtra, PlayerAction action)
    {
      switch (action)
      {
        case PlayerAction.Have:
          Console.WriteLine(string.Format("Player {0} currently holds the following cards:", player.PlayerFigure.FigureName));
          foreach (ICard cardHeld in player.CardsHeld)
          {
            Console.WriteLine(string.Format("Card : {0} {1} {2}", cardHeld.CardSuit, cardHeld.CardRank, cardHeld.CardFunc));
          }
          break;
        case PlayerAction.Play:
          Console.WriteLine(string.Format("Player {0} just played the following card:", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Card : {0} {1} {2}", cardPlayed.CardSuit, cardPlayed.CardRank, cardPlayed.CardFunc));
          break;
        case PlayerAction.NoResponse:
          Console.WriteLine(string.Format("Player {0} did not respond to the original card", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.NoResponseFromAll:
          Console.WriteLine(string.Format("No player respond to the original card", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.Judge:
          Console.WriteLine(string.Format("The central card deck just used the following card to judge for Player {0}:", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Card : {0} {1} {2}", cardPlayed.CardSuit, cardPlayed.CardRank, cardPlayed.CardFunc));
          break;
        case PlayerAction.Counter:
          Console.WriteLine(string.Format("Player {0} just played the following card to counter:", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Card : {0} {1} {2}", cardPlayed.CardSuit, cardPlayed.CardRank, cardPlayed.CardFunc));
          break;
        case PlayerAction.Deployed:
          Console.WriteLine(string.Format("The following card just gets deployed on Player {0}:", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Card : {0} {1} {2}", cardPlayed.CardSuit, cardPlayed.CardRank, cardPlayed.CardFunc));
          break;
        case PlayerAction.Annulled:
          Console.WriteLine(string.Format("The following original card just gets annulled:", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Card : {0} {1} {2}", cardPlayed.CardSuit, cardPlayed.CardRank, cardPlayed.CardFunc));
          break;
        case PlayerAction.Drawn:
          Console.WriteLine(string.Format("The central card deck just deals the following cards to Player {0}:", player.PlayerFigure.FigureName));
          foreach (ICard cardExtra in cardsExtra)
          {
            Console.WriteLine(string.Format("Card : {0} {1} {2}", cardExtra.CardSuit, cardExtra.CardRank, cardExtra.CardFunc));
          }
          break;
        case PlayerAction.PrisonTakeEffect:
          Console.WriteLine(string.Format("Prison on Player {0} takes effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Player {0} cannot play any card due to being in prison", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.PrisonNotTakeEffect:
          Console.WriteLine(string.Format("Prison on Player {0} does not take effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Prison on Player {0} disappears", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.BombTakeEffect:
          Console.WriteLine(string.Format("Bomb on Player {0} takes effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Player {0} takes damage as the bomb just exploded", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.BombNotTakeEffect:
          Console.WriteLine(string.Format("Bomb on Player {0} does not take effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Bomb on Player {0} passes to the next player", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.JadeTakeEffect:
          Console.WriteLine(string.Format("Jade for Player {0} takes effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Player {0} does not respond further", player.PlayerFigure.FigureName));
          break;
        case PlayerAction.JadeNotTakeEffect:
          Console.WriteLine(string.Format("Jade for Player {0} does not take effect", player.PlayerFigure.FigureName));
          Console.WriteLine(string.Format("Player {0} needs to play a dodge card or will take damage", player.PlayerFigure.FigureName));
          break;
        default:
          throw new NotImplementedException("Unknown player action");
      }
    }

    public static ICard FindASpecificCard(this IPlayer player, Func func)
    {
      return player.CardsHeld.Find(c => c.CardFunc == func);
    }

    public static ICard FindASpecificCard(this IPlayer player, List<Func> funcsCanCounter)
    {
      // TODO Decide what card to play based on their priority, e.g. Dodge or ParryAll
      foreach (Func func1 in funcsCanCounter)
      {
        ICard card1 = player.CardsHeld.Find(c => c.CardFunc == func1);
        if (card1 != null)
          return card1;
      }

      return null;
    }

    // TODO Soon enough we will need a list version of FindASpecificCard()

    public static ICard PickARandomCard(this IPlayer player, List<ICard> miniCardList)
    {
      Random rng = new Random();
      int k = rng.Next(miniCardList.Count + 1);
      return miniCardList[k];
    }

    public static void SwapPlayers(ref IPlayer playerA, ref IPlayer playerB)
    {
      IPlayer playerTemp;

      ResetCardsPlayed(playerA);
      SwapCards(playerB);

      playerTemp = playerA;
      playerA = playerB;
      playerB = playerTemp;
    }

    static void ResetCardsPlayed(IPlayer player)
    {
      player.CardPlayed = player.CardResponded = null;
    }

    static void SwapCards(IPlayer player)
    {
      ICard cardTemp;

      cardTemp = player.CardPlayed;
      player.CardPlayed = player.CardResponded;
      player.CardResponded = cardTemp;
    }

    public static int GetNextPlayerIndex(int numOfTotalPlayers, int indexCurrentPlayer)
    {
      return ++indexCurrentPlayer % numOfTotalPlayers;
    }

    public static bool RankBetween(this ICard card, Rank rankLower, Rank rankUpper)
    {
      return card.CardRank >= rankLower && card.CardRank <= rankUpper;
    }

    public static Type GetActualCardType(ICard card)
    {
      switch (card.CardFunc)
      {
        case Func.Kill:
          return typeof(KillCard);
        case Func.Portion:
          return typeof(PortionCard);
        case Func.TenKArrows:
          return typeof(TenKArrowsCard);
        case Func.Duel:
          return typeof(DuelCard);
        case Func.Bomb:
          return typeof(BombCard);
        case Func.ParryAll:
          return typeof(ParryAllCard);
        case Func.Prison:
          return typeof(PrisonCard);
        case Func.FreeCardForAll:
          return typeof(FreeCardForAllCard);
        case Func.TwoCardsFromOne:
          return typeof(TwoCardsFromOneCard);
        case Func.HealingAll:
          return typeof(HealingAllCard);
        case Func.BarbariansAtGate:
          return typeof(BarbariansAtGateCard);
        case Func.PocketPicking:
          return typeof(PocketPickingCard);
        case Func.PickAndDrop:
          return typeof(PickAndDropCard);
        case Func.Tiger:
          return typeof(TigerCard);
        case Func.Jade:
          return typeof(JadeCard);
        default:
          throw new NotImplementedException("Undefined card type");
      }
    }

    public static void RemoveCard(this List<ICard> cardList, ICard card)
    {
      cardList.RemoveAll(c => c.IsSameAs(card));
    }

    public static void ReplaceEquippable(this IPlayer player, ICard cardNew)
    {
      ICard cardEquipped = player.CardEquipped.Find(c => c.CardEquippingType == cardNew.CardEquippingType);
      if (cardEquipped != null)
        player.CardEquipped.RemoveCard(cardEquipped);
      player.CardEquipped.Add(cardNew);
    }

    public static void AfterEquipped(this ICard cardJustEquipped)
    {
      cardJustEquipped.CardAction = CAction.TakeEffect;
    }
  }
}
