// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.ProjectInstaller
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace RightFaxSenderService
{
  [RunInstaller(true)]
  public class ProjectInstaller : Installer
  {
    private IContainer components = (IContainer) null;
    private ServiceProcessInstaller serviceProcessInstaller1;
    private ServiceInstaller serviceInstaller1;

    public ProjectInstaller() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.serviceProcessInstaller1 = new ServiceProcessInstaller();
      this.serviceInstaller1 = new ServiceInstaller();
      this.serviceProcessInstaller1.Account = ServiceAccount.NetworkService;
      this.serviceProcessInstaller1.Password = (string) null;
      this.serviceProcessInstaller1.Username = (string) null;
      this.serviceInstaller1.Description = "Kroger Faxing Service for sending faxes from Db using the RightFax";
      this.serviceInstaller1.DisplayName = "Kroger.FaxingService";
      this.serviceInstaller1.ServiceName = "Kroger.FaxingService";
      this.serviceInstaller1.StartType = ServiceStartMode.Automatic;
      this.Installers.AddRange(new Installer[2]
      {
        (Installer) this.serviceProcessInstaller1,
        (Installer) this.serviceInstaller1
      });
    }
  }
}
