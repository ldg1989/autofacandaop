using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoFacAop.Common
{
  public static class EnumExtensions
  {
    public static string GetDescription(this Enum value)
    {
      return value.GetType()
          .GetMember(value.ToString())
          .FirstOrDefault()?
          .GetCustomAttribute<DescriptionAttribute>()?
          .Description;
    }
  }
}
