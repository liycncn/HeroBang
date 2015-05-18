using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.CardExpansion;

namespace HeroBang.Game
{
  public class SinglePlayerResponseArgs : EventArgs
  {
    ICard cardTriggering;
    public ICard CardTriggering { get { return cardTriggering; } }
    List<ICard> miniCardList;
    public List<ICard> MiniCardList { get { return miniCardList; } }
    ICard cardFirst;
    public ICard CardFirst { get { return cardFirst; } }
    // TODO Add the list of players to request a card from 

    public SinglePlayerResponseArgs(ICard cardT, List<ICard> miniCards, ICard cardF)
    {
      this.cardTriggering = cardT;
      this.miniCardList = miniCards;
      this.cardFirst = cardF;
    }
  }

  public class PlayerMessenger
  {
    // Define the delegate and its signature
    public delegate void PlayerTurnStartsEventHandler(PlayerMessenger messenger);
    // Define the event using the delegate
    public event PlayerTurnStartsEventHandler PlayerTurnStarts;

    // Define the delegate and its signature
    public delegate void PlayAnotheCardEventHandler(PlayerMessenger messenger);
    // Define the event using the delegate
    public event PlayAnotheCardEventHandler PlayAnotheCard;

    // Define the delegate and its signature
    public delegate void ResponseNeededEventHandler(PlayerMessenger messenger, SinglePlayerResponseArgs e);
    // Define the event using the delegate
    public event ResponseNeededEventHandler ResponseNeeded;

    // Define the delegate and its signature
    public delegate void PlayerTurnEndsEventHandler(PlayerMessenger messenger);
    // Define the event using the delegate
    public event PlayerTurnEndsEventHandler PlayerTurnEnds;

    public virtual void OnPlayerTurnStarts()
    {
      if (PlayerTurnStarts != null)
        PlayerTurnStarts(this);
    }

    public virtual void OnPlayAnotheCard()
    {
      if (PlayAnotheCard != null)
        PlayAnotheCard(this);
    }

    public virtual void OnResponseNeeded(SinglePlayerResponseArgs responseArgs)
    {
      if (ResponseNeeded != null)
        ResponseNeeded(this, responseArgs);
    }

    public virtual void OnPlayerTurnEnds()
    {
      if (PlayerTurnEnds != null)
        PlayerTurnEnds(this);
    }

  }
}
