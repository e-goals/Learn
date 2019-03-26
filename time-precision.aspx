<%@ Page Language="C#" AutoEventWireup="true" CodeFile="time-precision.aspx.cs" Inherits="TimePrecision" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Time Precision</title>
  <style type="text/css">
    body { line-height: 1.5em; }
    form { color: #F00; font-family: Consolas, SimSun, sans-serif; font-size: 20px; }
    p.note { color: #90F; font-family: Verdana, SimSun, sans-serif; font-size: 16px; }
  </style>
</head>
<body>
  <form id="_form">
    <div>
      <%= "EZGoal.PreciseCounter.Frequency (System.Diagnostics.Stopwatch.Frequency): " + EZGoal.PreciseCounter.Frequency %><br />
      <p class="note">Cases might exist where QueryPerformanceFrequency doesn't return the actual frequency of the hardware tick generator. For example, in many cases, QueryPerformanceFrequency returns the TSC frequency divided by 1024; and on Hyper-V, the performance counter frequency is always 10 MHz when the guest virtual machine runs under a hypervisor that implements the hypervisor version 1.0 interface. As a result, don't assume that QueryPerformanceFrequency will return the precise TSC frequency.</p>
      <%= outputBuilder.ToString() %>
      <p class="note">A single tick represents one hundred nanoseconds or one ten-millionth of a second. There are 10,000 ticks in a millisecond, or 10 million ticks in a second. </p>
      <hr />
      <p class="note">System.DateTime的范围</p>
      <%= DateTime.MinValue.ToString("yyyyMMdd HH:mm:ss.fffffff") %><br />
      <%= DateTime.MaxValue.ToString("yyyyMMdd HH:mm:ss.fffffff") %><br />
      <%= "DateTime.MinValue.Ticks : " + DateTime.MinValue.Ticks %><br />
      <%= "DateTime.MaxValue.Ticks : " + DateTime.MaxValue.Ticks %><br />
      <%= "Int64.MaxValue : "+Int64.MaxValue %><br />
      <hr />
      <script runat="server">
        DateTime dtime0 = EZGoal.Datetime.Now, dtime1 = EZGoal.Datetime.UtcNow;
        DateTime dtime2 = DateTime.Now, dtime3 = DateTime.UtcNow;
      </script>
      <p class="note">以下的4个时间值，前两个来自EZGoal.Datetime连续获取，后两个来自System.DateTime连续获取</p>
      <%= dtime0.ToString("yyyyMMdd HH:mm:ss.fffffff") + ", " + dtime0.Ticks + ", " + dtime0.Kind %><br />
      <%= dtime1.ToString("yyyyMMdd HH:mm:ss.fffffff") + ", " + dtime1.Ticks + ", " + dtime1.Kind %><br />
      <%= dtime2.ToString("yyyyMMdd HH:mm:ss.fffffff") + ", " + dtime2.Ticks + ", " + dtime2.Kind %><br />
      <%= dtime3.ToString("yyyyMMdd HH:mm:ss.fffffff") + ", " + dtime3.Ticks + ", " + dtime3.Kind %><br />
      <br />
      <%= EZGoal.Database.Version %>
      <br />
    </div>
  </form>
</body>
</html>
