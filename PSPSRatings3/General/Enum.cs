using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBang
{
  public enum Suit
  {
    None,
    Club,
    Diamond,
    Heart,
    Spade
  }

  public enum Rank
  {
    None = 0,
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
  }

  public enum Func
  {
    Kill,
    Dodge,
    Portion,
    Duel,
    Bomb,
    ParryAll,
    Prison,
    BorrowingWeapon, // TODO Three-party card
    FreeCardForAll, // TODO A card affecting all players
    TwoCardsFromOne,
    HealingAll,
    BarbariansAtGate,
    TenKArrows,
    PocketPicking,
    PickAndDrop,
    Tiger,
    Jade,
    OffensiveHorse,
    DefensiveHorse,
    BroadSword,
    LongStick,
    ShortSword,
    ChainHammer,
    NailHammer,
    LongBow,
    Spear
  }

  public enum FuncType
  {
    Basic,
    TrickInSleeve,
    OffensiveWeapon,
    DefensiveWeapon,
    OffensiveHorse,
    DefensiveHorse,
  }

  public enum Gender
  {
    None,
    Female,
    Male
  }

  public enum PlayerAction
  {
    Have,
    Play,
    NoResponse,
    NoResponseFromAll,
    Judge,
    Counter,
    Deployed,
    Annulled,
    Drawn,
    PrisonTakeEffect,
    PrisonNotTakeEffect,
    BombTakeEffect,
    BombNotTakeEffect,
    JadeTakeEffect,
    JadeNotTakeEffect
  }

  public enum CAction
  {
    TakeEffect,
    GetEquipped
  }

  public enum CarriedType
  {
    None,
    Bomb,
    Prison
  }

  public enum EquippedType
  {
    None,
    Arm,
    Shield,
    OffensiveHorse,
    DefensiveHorse
  }

  //public enum NumOfRoundOption
  //{
  //  None,
  //  OneMiniRound,
  //  Unlimited
  //}

  public enum MiniRoundTimingOption
  {
    None,
    FirstMiniRound,
    Always
  }

  public enum AffectedPlayer
  {
    One, 
    Two,
    Three,
    All,
    AllButMyself
  }
}
