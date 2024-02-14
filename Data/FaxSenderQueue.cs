// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Data.FaxSenderQueue
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System;

namespace RightFaxSenderService.Data
{
  public class FaxSenderQueue
  {
    public long FaxID { get; set; }

    public string ApplicationID { get; set; }

    public string FaxTo { get; set; }

    public string FaxToName { get; set; }

    public string HTMLBody { get; set; }

    public string AttachmentPath { get; set; }

    public string Details { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public string SentStatus { get; set; }

    public DateTime? SentOn { get; set; }

    public string ErrorMessage { get; set; }

    public int? SiteID { get; set; }

    public int? FaxRefID { get; set; }

    public string RFJobID { get; set; }

    public string RFUserID { get; set; }

    public int? ConfigID { get; set; }
  }
}
