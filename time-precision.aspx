<%@ Page Language="C#" AutoEventWireup="true" CodeFile="time-precision.aspx.cs" Inherits="TimePrecision" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <style type="text/css">
    body { line-height: 1.5em; }
    form { color: #F00; font-family: Consolas, SimSun, sans-serif; font-size: 20px; }
    p { color: #F0F; font-family: Verdana, SimSun, sans-serif; font-size: 16px; }
  </style>
</head>
<body>
  <form id="_form">
    <div>
      <%= "System.Diagnostics.Stopwatch.Frequency: " + System.Diagnostics.Stopwatch.Frequency %><br />
      <p>Cases might exist where QueryPerformanceFrequency doesn't return the actual frequency of the hardware tick generator. For example, in many cases, QueryPerformanceFrequency returns the TSC frequency divided by 1024; and on Hyper-V, the performance counter frequency is always 10 MHz when the guest virtual machine runs under a hypervisor that implements the hypervisor version 1.0 interface. As a result, don't assume that QueryPerformanceFrequency will return the precise TSC frequency.</p>
      <%= output %>
    </div>
  </form>
</body>
</html>
