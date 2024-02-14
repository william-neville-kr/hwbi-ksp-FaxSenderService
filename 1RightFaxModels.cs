// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Tag
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.Collections.Generic;

namespace RightFaxSenderService
{
  public class Tag
  {
    public string Id { get; set; }

    public string RelatedType { get; set; }

    public string RelatedId { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public string ETag { get; set; }

    public List<Link> Links { get; set; }
  }
}
