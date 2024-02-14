// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.Data.MHData
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace RightFaxSenderService.Data
{
  public class MHData : DbContext
  {
    public MHData()
      : base("name=MHData")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder) => throw new UnintentionalCodeFirstException();

    public virtual DbSet<FaxSenderQueue> FaxSenderQueues { get; set; }

    public virtual DbSet<FaxSenderErrorLog> FaxSenderErrorLogs { get; set; }

    public virtual ObjectResult<RightFaxSenderConfig> GetFaxConfigByID(int? configNo) => ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction<RightFaxSenderConfig>(nameof (GetFaxConfigByID), configNo.HasValue ? new ObjectParameter("ConfigNo", (object) configNo) : new ObjectParameter("ConfigNo", typeof (int)));

    public virtual int AddNewFaxConfig(
      int? configNo,
      string serverURL,
      string userID,
      string password)
    {
      return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction(nameof (AddNewFaxConfig), configNo.HasValue ? new ObjectParameter("ConfigNo", (object) configNo) : new ObjectParameter("ConfigNo", typeof (int)), serverURL != null ? new ObjectParameter("ServerURL", (object) serverURL) : new ObjectParameter("ServerURL", typeof (string)), userID != null ? new ObjectParameter("UserID", (object) userID) : new ObjectParameter("UserID", typeof (string)), password != null ? new ObjectParameter("Password", (object) password) : new ObjectParameter("Password", typeof (string)));
    }
  }
}
