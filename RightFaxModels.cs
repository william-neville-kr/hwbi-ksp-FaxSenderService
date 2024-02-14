// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.SendJobResponse
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System;
using System.Collections.Generic;

namespace RightFaxSenderService
{
  public class SendJobResponse
  {
    public string Id { get; set; }

    public string UserId { get; set; }

    public List<Recipient> Recipients { get; set; }

    public List<string> IncludedDocumentIds { get; set; }

    public List<string> AttachmentUrls { get; set; }

    public List<string> LibraryDocumentIds { get; set; }

    public DateTime SendAfter { get; set; }

    public bool HoldForPreview { get; set; }

    public string Priority { get; set; }

    public string DiagnosticMode { get; set; }

    public string Resolution { get; set; }

    public string CoversheetTemplateId { get; set; }

    public List<Tag> Tags { get; set; }

    public string Condition { get; set; }

    public string Status { get; set; }

    public int DocumentCount { get; set; }

    public DateTime CreateTime { get; set; }

    public List<Notification> Notifications { get; set; }

    public List<Link> Links { get; set; }
  }
}
