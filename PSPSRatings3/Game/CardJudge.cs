using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using HeroBang.CardExpansion;

namespace HeroBang.Game
{
  public class CardJudge
  {
    static object InvokeActualCardMethod(string methodName, ICard card, object[] methodParams)
    {
      // Use reflection to invoke the method of the actual card type, i.e. individual class inheriting the class PlayedCard
      Type cardToPlayType = Util.GetActualCardType(card);
      MethodInfo methodInfo = cardToPlayType.GetMethod(methodName);
      var actualCardToPlay = Activator.CreateInstance(cardToPlayType, new object[] { card });
      return methodInfo.Invoke(actualCardToPlay, methodParams);
    }

    public static bool CanPlayerPlayCard(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardToPlay)
    {
      // Use reflection to invoke the method CanBePlayed()
      //Type cardToPlayType = Util.GetActualCardType(cardToPlay);
      //MethodInfo methodInfo = cardToPlayType.GetMethod("CanBePlayed");
      //var actualCardToPlay = Activator.CreateInstance(cardToPlayType, new object[] { cardToPlay });
      //return (bool)methodInfo.Invoke(actualCardToPlay, new object[] { playerPlaying });
      return (bool)InvokeActualCardMethod("CanBePlayed", cardToPlay, new object[] { playerPlaying, playerReceiving });
    }

    public static bool CardHasImmediateEffect(ICard cardToPlay)
    {
      return (bool)InvokeActualCardMethod("HasImmediateEffect", cardToPlay, new object[] { });
    }

    public static void CardGetDeployed(IPlayer playerReceiving, ICard cardPlayed)
    {
      //IPlayer playerR= playerReceiving ;
      //object[] args = new object[] { playerR, cardPlayed };
      //InvokeActualCardMethod("GetDeployed",args  );
      //playerReceiving = playerR;
      InvokeActualCardMethod("GetDeployed", cardPlayed, new object[] { playerReceiving });
    }

    public static MiniRoundTimingOption CardCanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, ICard cardPlayed, out List<Func> funcsCanCounter)
    {
      // c# reflection: How can I invoke a method with an out parameter?
      // http://stackoverflow.com/questions/2438065/c-sharp-reflection-how-can-i-invoke-a-method-with-an-out-parameter
      funcsCanCounter = new List<Func>();
      object[] args = new object[] { playerPlaying, cardFirst, funcsCanCounter };
      MiniRoundTimingOption result = (MiniRoundTimingOption)InvokeActualCardMethod("CanBeCounteredByAnyPlayer", cardPlayed, args);
      funcsCanCounter = (List<Func>)args[2];
      return result;
    }

    public static MiniRoundTimingOption CardCanBeAnnulled(IPlayer playerPlaying, ICard cardFirst, ICard cardPlayed, out List<Func> funcsCanAnnul)
    {
      // c# reflection: How can I invoke a method with an out parameter?
      // http://stackoverflow.com/questions/2438065/c-sharp-reflection-how-can-i-invoke-a-method-with-an-out-parameter
      funcsCanAnnul = new List<Func>();
      object[] args = new object[] { playerPlaying, cardFirst, funcsCanAnnul };
      MiniRoundTimingOption result = (MiniRoundTimingOption)InvokeActualCardMethod("CanBeAnnulled", cardPlayed, args);
      funcsCanAnnul = (List<Func>)args[2];
      return result;
    }

    public static MiniRoundTimingOption CardCanBeAnnulledByEquippable(IPlayer playerPlaying, ICard cardFirst, ICard cardPlayed, out List<Func> funcsCanAnnul)
    {
      // c# reflection: How can I invoke a method with an out parameter?
      // http://stackoverflow.com/questions/2438065/c-sharp-reflection-how-can-i-invoke-a-method-with-an-out-parameter
      funcsCanAnnul = new List<Func>();
      object[] args = new object[] { playerPlaying, cardFirst, funcsCanAnnul };
      MiniRoundTimingOption result = (MiniRoundTimingOption)InvokeActualCardMethod("CanBeAnnulledByEquippable", cardPlayed, args);
      funcsCanAnnul = (List<Func>)args[2];
      return result;
    }

    public static AffectedPlayer CardAffectNumOfPlayers(ICard cardPlayed)
    {
      return (AffectedPlayer)InvokeActualCardMethod("AffectNumOfPlayers", cardPlayed, new object[] { });
    }

    public static bool CardNeedJudgingCard(ICard cardPlayed)
    {
      return (bool)InvokeActualCardMethod("NeedJudgingCard", cardPlayed, new object[] { });
    }

    public static bool CardJudgingResult(ICard cardPlayed, ICard cardUsedToJudge)
    {
      return (bool)InvokeActualCardMethod("JudgingResult", cardPlayed, new object[] { cardUsedToJudge });
    }

    public static bool CardNeedExtraCard(ICard cardPlayed)
    {
      return (bool)InvokeActualCardMethod("NeedExtraCard", cardPlayed, new object[] { });
    }

    public static int CardNeedNumOfExtraCards(ICard cardPlayed, ICard cardUsedToJudge)
    {
      return (int)InvokeActualCardMethod("NumOfExtraCardsNeeded", cardPlayed, new object[] { });
    }

    public static bool CardTriggerOneOnOne(ICard cardPlayed)
    {
      return (bool)InvokeActualCardMethod("TriggerOneOnOne", cardPlayed, new object[] { });
    }

    public static void CardTakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, ICard cardPlayed, List<ICard> cardsExtra)
    {
      InvokeActualCardMethod("TakeEffect", cardPlayed, new object[] { playerPlaying, playerReceiving, cardsExtra ?? new List<ICard>() });
    }

    public static void CardNotTakeEffect(List<IPlayer> playerList, IPlayer playerPlaying, IPlayer playerReceiving, ICard cardPlayed)
    {
      InvokeActualCardMethod("NotTakeEffect", cardPlayed, new object[] { playerList, playerPlaying, playerReceiving });
    }

    //public static bool Judge(ICard cardPlayed, ICard cardResponded, ICard cardFirst)
    //{
    //  IPlayer playerPlaying = cardPlayed.PlayerPlaying;
    //  IPlayer playerReceiving = cardPlayed.PlayerReceiving;
    //  bool newResponseNeeded = false;

    //  switch (cardPlayed.CardFunc)
    //  {
    //    case Func.Kill:
    //      newResponseNeeded = (new KillCard(cardPlayed)).ActionResult(playerPlaying, playerReceiving, cardResponded, cardFirst);
    //      break;
    //    case Func.Portion:
    //      (new PortionCard(cardPlayed)).ActionResult(playerPlaying);
    //      break;
    //    case Func.TenKArrows:
    //      (new TenKArrowsCard(cardPlayed)).ActionResult(playerPlaying, playerReceiving, cardResponded);
    //      break;
    //    case Func.Duel:
    //      newResponseNeeded = (new DuelCard(cardPlayed)).ActionResult(playerPlaying, playerReceiving, cardResponded);
    //      break;
    //    case Func.Bomb:
    //    case Func.Prison:
    //      newResponseNeeded = false;
    //      break;
    //    default:
    //      throw new NotImplementedException("Undefined card behaviour");
    //  }

    //  return newResponseNeeded;
    //}

  }
}
