using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeroBang.CardExpansion;

namespace HeroBang.Game
{
  public interface IPlayer
  {
    IFigure PlayerFigure { get; set; }

    List<ICard> CardCarrying { get; set; }
    bool InPrison { get; set; }
    List<ICard> CardEquipped { get; set; }

    ICard CardPlayed { get; set; }
    ICard CardResponded { get; set; }
    ICard CardNeedResponse { get; set; }

    List<ICard> CardsHeld { get; set; }
    List<ICard> CardsCanBePlayedInThisRound { get; set; }
    List<ICard> CardsPlayedInThisRound { get; set; }

    IPlayer PlayerToKill { get; set; }
    IPlayer PlayerToProtect { get; set; }

    PlayerMessenger Messenger { get; }

    void PrintPlayedCard(ICard card);
    ICard PlayACardFromEquipped(ICard card, IPlayer playerReceiving, bool isAnnulling);
  }
}
