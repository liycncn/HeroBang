using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.Game;

namespace HeroBang.CardExpansion
{
  abstract class PlayedCard : Card
  {
    public virtual bool CanBePlayed(IPlayer playerPlaying, IPlayer playerReceiving) { return true; }
    public virtual bool HasImmediateEffect() { return true; }
    public virtual void GetDeployed(IPlayer playerReceiving) { ; }

     // Countering and Annulling 
    // Potentially every player can counter a card. However it depends on the type of the first card being played
    public virtual MiniRoundTimingOption CanBeCounteredByAnyPlayer(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { };
      return MiniRoundTimingOption.None;
    }

    // Only the receiving player can annul a card being played
    public virtual MiniRoundTimingOption CanBeAnnulled(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanAnnul)
    {
      funcsCanAnnul = new List<Func>() { };
      return MiniRoundTimingOption.None;
    }

    // Only the receiving player can annul a card being played
    public virtual MiniRoundTimingOption CanBeAnnulledByEquippable(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanAnnul)
    {
      funcsCanAnnul = new List<Func>() { };
      return MiniRoundTimingOption.None;
    }

    public virtual AffectedPlayer AffectNumOfPlayers() { return AffectedPlayer.One; }

    // Extra cards
    public virtual bool NeedJudgingCard() { return false; }
    public virtual bool JudgingResult(ICard cardUsedToJudge) { return false; }
    public virtual bool NeedExtraCard() { return false; }
    public virtual int NumOfExtraCardsNeeded() { return 0; }

    
    //public virtual TimingOfRoundOption TimingOfAnyPlayerToCounter() { return  TimingOfRoundOption.None; }
    //public virtual TimingOfRoundOption TimingOfPlayerToAnnul() { return TimingOfRoundOption.None; }
    //public virtual NumOfRoundOption NumOfCounteringRound() { return  NumOfRoundOption.None; }    
    public virtual bool TriggerOneOnOne() { return false; }
    public virtual MiniRoundTimingOption CanBeCounteredInOneOnOne(IPlayer playerPlaying, ICard cardFirst, out List<Func> funcsCanCounter)
    {
      funcsCanCounter = new List<Func>() { };
      return MiniRoundTimingOption.None;
    }

    public abstract void TakeEffect(IPlayer playerPlaying, IPlayer playerReceiving, List<ICard> cardsExtra);
    public virtual void NotTakeEffect(List<IPlayer> playerList, IPlayer playerPlaying, IPlayer playerReceiving) { }
  }
}
