// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.FaxProcessingService
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using RightFaxSenderService.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Timers;

namespace RightFaxSenderService
{
  public class FaxProcessingService
  {
    private Timer _timer;

    public void OnStart()
    {
      try
      {
        this._timer = new Timer();
        this._timer.Elapsed += new ElapsedEventHandler(this.WorkerFunction);
        this._timer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["WaitTime"]);
        this._timer.Enabled = true;
        this._timer.AutoReset = false;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:OnStart() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    private void WorkerFunction(object sender, ElapsedEventArgs e)
    {
      this._timer.Stop();
      try
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##INFO## : FaxProcessingService:WorkerFunction() =>  Starting the Work");
        ConfigurationManager.RefreshSection("appSettings");
        MHData mhData = new MHData();
        List<FaxSenderQueue> list = mhData.FaxSenderQueues.Where<FaxSenderQueue>((Expression<Func<FaxSenderQueue, bool>>) (t => t.SentOn == new DateTime?())).ToList<FaxSenderQueue>();
        RightFaxSenderConfig rightFaxSenderConfig = mhData.GetFaxConfigByID(new int?(1)).FirstOrDefault<RightFaxSenderConfig>();
        foreach (FaxSenderQueue fax in list)
        {
          RightFaxSenderConfig config = (RightFaxSenderConfig) null;
          if (fax.ConfigID.HasValue)
            config = mhData.GetFaxConfigByID(fax.ConfigID).FirstOrDefault<RightFaxSenderConfig>();
          if (config == null)
            config = rightFaxSenderConfig;
          this.SendFaxUsingRightFaxAPI(fax, config);
          mhData.SaveChanges();
        }
        this.CheckForFaxStatuses();
      }
      catch (Exception ex)
      {
        LogHelper.LogError(ex);
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:WorkerFunction() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
      finally
      {
        this._timer.Start();
      }
    }

    private void CheckForFaxStatuses()
    {
      try
      {
        List<string> statuses = new List<string>()
        {
          "Failed",
          "Cancelled",
          "Succeeded"
        };
        MHData mhData = new MHData();
        List<FaxSenderQueue> list = mhData.FaxSenderQueues.Where<FaxSenderQueue>((Expression<Func<FaxSenderQueue, bool>>) (t => t.SentOn != new DateTime?() && !statuses.Contains(t.SentStatus))).ToList<FaxSenderQueue>();
        RightFaxSenderConfig rightFaxSenderConfig = mhData.GetFaxConfigByID(new int?(1)).FirstOrDefault<RightFaxSenderConfig>();
        foreach (FaxSenderQueue faxSenderQueue in list)
        {
          try
          {
            RightFaxSenderConfig config1 = (RightFaxSenderConfig) null;
            if (faxSenderQueue.ConfigID.HasValue)
              config1 = mhData.GetFaxConfigByID(faxSenderQueue.ConfigID).FirstOrDefault<RightFaxSenderConfig>();
            if (config1 == null)
              config1 = rightFaxSenderConfig;
            new RightFaxHelper(faxSenderQueue, config1).CheckFaxStatus(faxSenderQueue.RFJobID, faxSenderQueue.RFUserID);
            mhData.SaveChanges();
          }
          catch (Exception ex)
          {
            faxSenderQueue.ErrorMessage = ex.ToString();
            LogHelper.InsertLog(faxSenderQueue, ex);
            Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:CheckForFaxStatuses() => " + ex.Message);
            Trace.TraceError(ex.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogError(ex);
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:CheckForFaxStatuses() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    private void SendFaxUsingRightFaxAPI(FaxSenderQueue fax, RightFaxSenderConfig config)
    {
      try
      {
        if (string.IsNullOrEmpty(fax.FaxTo))
          fax.ErrorMessage = "FaxTo is required.";
        else if (string.IsNullOrEmpty(fax.HTMLBody) && string.IsNullOrEmpty(fax.AttachmentPath))
          fax.ErrorMessage = "Both Massage or AttachmentPath should not be null.";
        else
          new RightFaxHelper(fax, config).SendFaxUsingRightFaxAPI();
      }
      catch (Exception ex)
      {
        fax.ErrorMessage = ex.ToString();
        LogHelper.InsertLog(fax, ex);
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:SendFaxUsingRightFaxAPI() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    public void OnStop()
    {
      try
      {
        this._timer.Enabled = false;
        this._timer.Stop();
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : FaxProcessingService:OnStop() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }
  }
}
