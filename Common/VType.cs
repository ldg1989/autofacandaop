using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFacAop.Common
{
  public enum VType
  {
    [Description("不考虑机会")]
    NotConsidering = 1,
    [Description("放弃面试 ")]
    QuitInterview = 2,
    [Description("放弃offer")]
    QuitOffer = 3,
    [Description("放弃入职")]
    QuitEntry = 4
  }
}