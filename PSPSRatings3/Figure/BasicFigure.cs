using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBang
{
  public interface IFigure
  {
    string FigureName { get; set; }    
    int HitPoint { get; set; }
    int MaxHitPoint { get; set; }
    Gender FigureGender { get; set; }

    //bool NeedHealing();
  }
}
