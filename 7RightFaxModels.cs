// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.DocumentResponse
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.Collections.Generic;

namespace RightFaxSenderService
{
  public class DocumentResponse
  {
    public int TotalCount { get; set; }

    public int Skipped { get; set; }

    public int Top { get; set; }

    public List<Item> Items { get; set; }

    public List<Link> Links { get; set; }
  }
}
