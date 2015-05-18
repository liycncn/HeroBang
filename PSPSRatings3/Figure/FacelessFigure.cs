using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBang
{
  public class FacelessFigure : IFigure
  {
    public string FigureName { get; set; }
    public int HitPoint { get; set; }
    public int MaxHitPoint { get; set; }
    public Gender FigureGender { get { return Gender.None; } set { } }

    public FacelessFigure(string name, int maxHitPoint)
    {
      this.FigureName = name;
      this.HitPoint = this.MaxHitPoint = maxHitPoint;
    }

    //public bool NeedHealing()
    //{
    //  // Simplify the need to be healed to black and white scenario for the time being
    //  if (HitPoint < MaxHitPoint)
    //    return true;

    //  return false;
    //}
  }
}
