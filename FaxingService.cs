// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.FaxingService
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace RightFaxSenderService
{
  public class FaxingService : ServiceBase
  {
    private readonly FaxProcessingService _faxProcessingService = new FaxProcessingService();
    private IContainer components = (IContainer) null;

    public FaxingService() => this.InitializeComponent();

    protected override void OnStart(string[] args)
    {
      Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
      try
      {
        Trace.WriteLine(DateTime.Now.ToString() + " : Starting Services...");
        this._faxProcessingService.OnStart();
        Trace.WriteLine(DateTime.Now.ToString() + " : All Services started successfully");
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " : Error Starting service => " + ex.Message);
        this.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
        throw;
      }
    }

    protected override void OnStop()
    {
      try
      {
        Trace.WriteLine(DateTime.Now.ToString() + " : Stopping Services...");
        this._faxProcessingService.OnStop();
        Trace.WriteLine(DateTime.Now.ToString() + " : All Services stopped successfully");
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " : Error Stoping service => " + ex.Message);
        this.EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
        throw;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.ServiceName = "Kroger.FaxingService";
    }
  }
}
