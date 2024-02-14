// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.RightFaxHelper
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using RestSharp;
using RestSharp.Authenticators;
using RightFaxSenderService.Data;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace RightFaxSenderService
{
  internal class RightFaxHelper
  {
    private readonly RightFaxSenderConfig Config;
    private readonly FaxSenderQueue Fax;

    public RightFaxHelper(FaxSenderQueue fax1, RightFaxSenderConfig config1)
    {
      this.Fax = fax1;
      this.Config = config1;
      ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    }

    public void SendFaxUsingRightFaxAPI()
    {
      RestClient authenticatedClient = this.GetAuthenticatedClient(this.Config);
      string str1 = this.UploadAttachments(authenticatedClient, this.Fax.AttachmentPath, this.Fax.HTMLBody);
      if (string.IsNullOrEmpty(str1))
      {
        this.Fax.ErrorMessage = "Unable to upload the Attachment to the Server.";
      }
      else
      {
        RestRequest request = new RestRequest("SendJobs", Method.POST)
        {
          RequestFormat = DataFormat.Json
        };
        string str2 = ("{\r\n                                'Recipients':[\r\n                                    {\r\n                                        'Name':'" + this.Fax.FaxToName + "',\r\n                                        'Destination':'" + this.Fax.FaxTo + "',\r\n                                        'PhoneNumber':'" + this.Fax.FaxTo + "',\r\n                                    }\r\n                                ],\r\n                                    'AttachmentUrls': ['" + str1 + "'],\r\n                                    \r\n                           }").Replace("'", "\"");
        request.AddParameter("application/json", (object) str2, ParameterType.RequestBody);
        IRestResponse<SendJobResponse> response = authenticatedClient.Execute<SendJobResponse>((IRestRequest) request);
        if (response.ErrorException != null)
        {
          LogHelper.InsertLog(this.Fax, response.ErrorException);
          LogHelper.LogRequestResponse((IRestRequest) request, (IRestResponse) response);
        }
        if (response.StatusCode == HttpStatusCode.Created)
        {
          this.Fax.SentOn = new DateTime?(DateTime.UtcNow);
          this.Fax.RFJobID = response.Data.Id;
          this.Fax.RFUserID = response.Data.UserId;
          this.Fax.SentStatus = response.Data.Condition;
        }
        else
        {
          this.Fax.ErrorMessage = response.Content;
          LogHelper.InsertLog(this.Fax, response.Content);
          LogHelper.LogRequestResponse((IRestRequest) request, (IRestResponse) response);
        }
      }
    }

    public void CheckFaxStatus(string jobID, string userID)
    {
      RestClient authenticatedClient = this.GetAuthenticatedClient(this.Config);
      RestRequest request = new RestRequest("Documents?userId=" + userID + "&filter=job&jobid=" + jobID, Method.GET)
      {
        RequestFormat = DataFormat.Json
      };
      IRestResponse<DocumentResponse> response = authenticatedClient.Execute<DocumentResponse>((IRestRequest) request);
      if (response.ErrorException != null)
      {
        LogHelper.InsertLog(this.Fax, response.ErrorException);
        LogHelper.LogRequestResponse((IRestRequest) request, (IRestResponse) response);
      }
      if (response.StatusCode == HttpStatusCode.OK)
      {
        if (response.Data.Items.Count <= 0)
          return;
        this.Fax.SentStatus = response.Data.Items[0].Condition;
        LogHelper.InsertLog(this.Fax, "New Fax Status: " + response.Data.Items[0].Condition);
        if (this.Fax.SentStatus == "Failed" || this.Fax.SentStatus == "Warning")
        {
          this.Fax.ErrorMessage = "Status Code: " + response.Data.Items[0].Status + " Status Text:" + response.Data.Items[0].StatusText;
          LogHelper.InsertLog(this.Fax, "Failed Fax Status: Status Code: " + response.Data.Items[0].Status + " Status Text:" + response.Data.Items[0].StatusText);
        }
      }
      else
      {
        this.Fax.ErrorMessage = response.Content;
        LogHelper.InsertLog(this.Fax, response.Content);
        LogHelper.LogRequestResponse((IRestRequest) request, (IRestResponse) response);
      }
    }

    private string UploadAttachments(RestClient client, string filePath, string rawData)
    {
      RestRequest request = new RestRequest("Attachments", Method.POST);
      if (!string.IsNullOrEmpty(filePath))
        request.AddFile("Attachment", filePath, (string) null);
      else if (!string.IsNullOrEmpty(rawData))
        request.AddFileBytes("Attachment", Encoding.ASCII.GetBytes(rawData), "Attachment.txt", "text/plain");
      IRestResponse response = client.Execute((IRestRequest) request);
      if (response.ErrorException != null)
      {
        LogHelper.InsertLog(this.Fax, response.ErrorException);
        LogHelper.LogRequestResponse((IRestRequest) request, response);
      }
      if (response.StatusCode == HttpStatusCode.Created)
      {
        Parameter parameter = response.Headers.FirstOrDefault<Parameter>((Func<Parameter, bool>) (t => t.Name == "Location"));
        if (parameter != null)
          return parameter.Value.ToString();
      }
      else
      {
        this.Fax.ErrorMessage = response.Content;
        LogHelper.InsertLog(this.Fax, response.Content);
        LogHelper.LogRequestResponse((IRestRequest) request, response);
      }
      return "";
    }

    private RestClient GetAuthenticatedClient(RightFaxSenderConfig config)
    {
      string serverUrl = config.ServerURL;
      string userId = config.UserID;
      string password = config.Password;
      return new RestClient(serverUrl)
      {
        Authenticator = (IAuthenticator) new HttpBasicAuthenticator(userId, password)
      };
    }
  }
}
