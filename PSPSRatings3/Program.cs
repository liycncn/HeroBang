using System;
using System.Collections.Generic;

using System.Linq;

using HeroBang.CardExpansion;
using HeroBang.Game;

namespace HeroBang
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      CardTable mainTable = new CardTable();

      List<IPlayer> initialPlayerList = new List<IPlayer>() {
        new FacelessPlayer(new FacelessFigure("FacelessPlayer 1", 4), mainTable), 
        new FacelessPlayer(new FacelessFigure("FacelessPlayer 2", 4), mainTable)};

      mainTable.PlayerList = initialPlayerList;

      //mainTable.InitialCardsDealing();
      mainTable.DealingSpecificCards(new List<List<ICard>>(){
        new List<ICard>() { 
          mainTable.CentralCardDeck.DrawSpecificCard(Func.Jade)},
        new List<ICard>() {
          mainTable.CentralCardDeck.DrawSpecificCard(Func.Kill)}
      });
      mainTable.PrintAllPlayersCards();

      mainTable.PlayerList[0].PlayerToKill = mainTable.PlayerList[1];
      mainTable.PlayerList[0].PlayerToProtect = mainTable.PlayerList[0];
      mainTable.PlayerList[1].PlayerToKill = mainTable.PlayerList[0];
      mainTable.PlayerList[1].PlayerToProtect = mainTable.PlayerList[1];

      mainTable.StartGame();
    }
  }
}
