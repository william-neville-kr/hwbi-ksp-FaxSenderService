// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Program
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.ServiceProcess;

namespace RightFaxSenderService
{
  internal static class Program
  {
    private static void Main() => ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new FaxingService()
    });
  }
}
