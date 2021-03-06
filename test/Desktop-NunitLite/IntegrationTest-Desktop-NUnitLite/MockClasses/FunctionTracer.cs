﻿
namespace Sitecore.MobileSDK.MockObjects
{
  using System;

  public class FunctionTracer : IDisposable
  {
    public FunctionTracer( string functionName, Action<string> logger )
    {
      this.logger = logger;
      this.functionName = functionName;

      string message = "[BEGIN] " + functionName;
      logger(message);
    }

    public void Dispose()
    {
      string message = "[END] " + this.functionName;
      this.logger(message);
    }


    private string functionName;
    private Action<string> logger;
  }
}

