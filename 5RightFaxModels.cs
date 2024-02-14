// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Recipient
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.Collections.Generic;

namespace RightFaxSenderService
{
  public class Recipient
  {
    public string Name { get; set; }

    public string Destination { get; set; }

    public string Company { get; set; }

    public string PhoneNumber { get; set; }

    public string ContactId { get; set; }

    public List<Tag> Tags { get; set; }
  }
}
