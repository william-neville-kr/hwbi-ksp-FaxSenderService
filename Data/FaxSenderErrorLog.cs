// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Data.FaxSenderErrorLog
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System;

namespace RightFaxSenderService.Data
{
  public class FaxSenderErrorLog
  {
    public long ID { get; set; }

    public long FaxID { get; set; }

    public string ErrorMessage { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? FaxRefID { get; set; }

    public string RFJobID { get; set; }

    public string RFUserID { get; set; }
  }
}
