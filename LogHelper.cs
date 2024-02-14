// Decompiled with JetBrains decompiler
// Type: RightFaxSenderService.LogHelper
// Assembly: RightFaxSenderService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7CE44107-192E-4CB8-98C4-CCABA0255BED
// Assembly location: C:\ZeroRefillFax\FaxSenderService\FaxSenderService\RightFaxSenderService.exe

using RestSharp;
using RightFaxSenderService.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace RightFaxSenderService
{
  public class LogHelper
  {
    public static void InsertLog(FaxSenderQueue Fax, string message)
    {
      try
      {
        if (ConfigurationManager.AppSettings["LogInDB"] == "true")
        {
          MHData mhData = new MHData();
          FaxSenderErrorLog entity = new FaxSenderErrorLog()
          {
            ErrorMessage = message,
            FaxID = Fax.FaxID,
            CreatedOn = DateTime.UtcNow,
            FaxRefID = Fax.FaxRefID,
            RFJobID = Fax.RFJobID,
            RFUserID = Fax.RFUserID
          };
          mhData.FaxSenderErrorLogs.Add(entity);
          mhData.SaveChanges();
        }
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() => FAXID: " + (object) Fax.FaxID + "  ErrorMessage: " + message);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    public static void InsertLog(FaxSenderQueue Fax, Exception exception)
    {
      try
      {
        MHData mhData = new MHData();
        StringBuilder sb = new StringBuilder();
        LogHelper.FormatException(sb, exception);
        string str = sb.ToString();
        if (ConfigurationManager.AppSettings["LogInDB"] == "true")
        {
          FaxSenderErrorLog entity = new FaxSenderErrorLog()
          {
            ErrorMessage = str,
            FaxID = Fax.FaxID,
            CreatedOn = DateTime.UtcNow,
            FaxRefID = Fax.FaxRefID,
            RFJobID = Fax.RFJobID,
            RFUserID = Fax.RFUserID
          };
          mhData.FaxSenderErrorLogs.Add(entity);
          mhData.SaveChanges();
        }
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() => FAXID: " + (object) Fax.FaxID + "  ErrorMessage: " + str);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    public static void LogError(Exception exception)
    {
      try
      {
        MHData mhData = new MHData();
        StringBuilder sb = new StringBuilder();
        LogHelper.FormatException(sb, exception);
        string str = sb.ToString();
        if (ConfigurationManager.AppSettings["LogInDB"] == "true")
        {
          FaxSenderErrorLog entity = new FaxSenderErrorLog()
          {
            ErrorMessage = str,
            FaxID = -1,
            CreatedOn = DateTime.UtcNow
          };
          mhData.FaxSenderErrorLogs.Add(entity);
          mhData.SaveChanges();
        }
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() =>   ErrorMessage: " + str);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(DateTime.Now.ToString() + " ##Error## : LogHelper:InsertLog() => " + ex.Message);
        Trace.TraceError(ex.ToString());
      }
    }

    public static void LogRequestResponse(IRestRequest request, IRestResponse response)
    {
      var data1 = new
      {
        resource = request.Resource,
        parameters = request.Parameters.Select(parameter => new
        {
          name = parameter.Name,
          value = parameter.Value,
          type = parameter.Type.ToString()
        }),
        method = request.Method.ToString()
      };
      var data2 = new
      {
        statusCode = response.StatusCode,
        content = response.Content,
        headers = response.Headers,
        responseUri = response.ResponseUri,
        errorMessage = response.ErrorMessage
      };
      Trace.Write(string.Format(DateTime.Now.ToString() + " ##REQUEST/RESPONSE## : Request: {0}, Response: {1}", (object) request.JsonSerializer.Serialize((object) data1), (object) request.JsonSerializer.Serialize((object) data2)));
    }

    public static void FormatException(StringBuilder sb, Exception ex)
    {
      sb.AppendLine("<b>Message</b>: <b>" + ex.Message + "</b><br />");
      sb.AppendLine("<b>Exception</b>: " + (object) ex.GetType() + "<br />");
      if (ex.TargetSite != (MethodBase) null)
        sb.AppendLine("<b>Targetsite</b>: " + (object) ex.TargetSite + "<br />");
      sb.AppendLine("<b>Source</b>: " + ex.Source + "<br />");
      if (!string.IsNullOrEmpty(ex.StackTrace))
        sb.AppendLine("<b>StackTrace</b>: " + ex.StackTrace.Replace(Environment.NewLine, "<br />") + "<br />");
      sb.AppendLine("<b>Data count</b>: " + (object) ex.Data.Count + "<br />");
      if (ex.Data.Count > 0)
      {
        HtmlTable htmlTable = new HtmlTable() { Border = 1 };
        HtmlTableRow row1 = new HtmlTableRow();
        HtmlTableCell cell1 = new HtmlTableCell();
        HtmlTableCell cell2 = new HtmlTableCell();
        HtmlTableCell cell3 = new HtmlTableCell();
        HtmlTableCell cell4 = new HtmlTableCell();
        cell1.InnerHtml = "<b>Key</b>";
        cell2.InnerHtml = "<b>Value</b>";
        cell3.InnerHtml = "Key Type";
        cell4.InnerHtml = "Value Type";
        row1.Cells.Add(cell1);
        row1.Cells.Add(cell2);
        row1.Cells.Add(cell3);
        row1.Cells.Add(cell4);
        htmlTable.Rows.Add(row1);
        foreach (DictionaryEntry dictionaryEntry in ex.Data)
        {
          HtmlTableRow row2 = new HtmlTableRow();
          HtmlTableCell cell5 = new HtmlTableCell();
          HtmlTableCell cell6 = new HtmlTableCell();
          HtmlTableCell cell7 = new HtmlTableCell();
          HtmlTableCell cell8 = new HtmlTableCell();
          cell5.InnerHtml = "<b>" + dictionaryEntry.Key + "</b>";
          cell6.InnerHtml = "<b>" + dictionaryEntry.Value + "</b>";
          cell7.InnerHtml = dictionaryEntry.Key.GetType().Name;
          cell8.InnerHtml = dictionaryEntry.Value.GetType().Name;
          cell7.Align = "center";
          cell8.Align = "center";
          row2.Cells.Add(cell5);
          row2.Cells.Add(cell6);
          row2.Cells.Add(cell7);
          row2.Cells.Add(cell8);
          htmlTable.Rows.Add(row2);
        }
        StringBuilder sb1 = new StringBuilder();
        HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter(sb1));
        htmlTable.RenderControl(writer);
        sb.AppendLine(sb1.ToString());
      }
      sb.AppendLine("<br />");
      sb.AppendLine("<b>Exception</b>: " + ex.ToString().Replace(Environment.NewLine, "<br />") + "<br />");
      if (ex.InnerException != null)
      {
        sb.Append("<br/><br/><b>Inner Excception:</b><br/><br/>");
        LogHelper.FormatException(sb, ex.InnerException);
      }
      if (!(ex is DbEntityValidationException))
        return;
      string str = ((DbEntityValidationException) ex).EntityValidationErrors.Aggregate<DbEntityValidationResult, string>("", (Func<string, DbEntityValidationResult, string>) ((current1, validationErrors) => validationErrors.ValidationErrors.Aggregate<DbValidationError, string>(current1, (Func<string, DbValidationError, string>) ((current, validationError) => current + string.Format(" Property: {0} Error: {1} ", (object) validationError.PropertyName, (object) validationError.ErrorMessage)))));
      sb.Append("<br/><br/><b>EntityValidationErrors:</b><br/><br/>");
      sb.Append(str);
    }
  }
}
