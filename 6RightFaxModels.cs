// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Item
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System;
using System.Collections.Generic;

namespace RightFaxSenderService
{
  public class Item
  {
    public string Id { get; set; }

    public string RootId { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public string Direction { get; set; }

    public int PageCount { get; set; }

    public string Condition { get; set; }

    public string Status { get; set; }

    public string StatusText { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public string Destination { get; set; }

    public string Company { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime TransmitCompleteTime { get; set; }

    public bool IsContentDeleted { get; set; }

    public bool HasCoversheet { get; set; }

    public bool IsArchived { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsSendJob { get; set; }

    public bool CanDelete { get; set; }

    public bool CanRetry { get; set; }

    public bool CanRetryToAltDestination { get; set; }

    public bool CanSendNow { get; set; }

    public bool CanCancelSend { get; set; }

    public bool CanPrint { get; set; }

    public bool CanSave { get; set; }

    public bool CanUndelete { get; set; }

    public bool CanView { get; set; }

    public bool CanViewProperties { get; set; }

    public bool CanRoute { get; set; }

    public bool CanResend { get; set; }

    public bool CanForwardInternally { get; set; }

    public bool CanForwardExternally { get; set; }

    public bool Viewed { get; set; }

    public bool IsHeldForPreview { get; set; }

    public bool HoldReleased { get; set; }

    public bool IsForwarded { get; set; }

    public bool IsAnnotated { get; set; }

    public List<object> Tags { get; set; }

    public string BillingCode1 { get; set; }

    public string BillingCode2 { get; set; }

    public List<object> ForwardedList { get; set; }

    public List<Link> Links { get; set; }
  }
}
