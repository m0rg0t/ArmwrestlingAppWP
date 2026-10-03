using System;
using ArmwrestlingAppWP;
using Microsoft.Devices.Sensors;
class Program {
 static int assertions;
 static void Equal(object expected, object actual) { assertions++; if (!Object.Equals(expected,actual)) throw new Exception("Expected " + expected + "; got " + actual); }
 static void Main() {
  var page=new MainPage(); page.ToggleForTest(); page.FirstPlayer=12; page.SecondPlayer=34; page.CurrentTime=DateTime.Now.AddSeconds(-10);
  var timestamp=page.CurrentTime; Accelerometer.Last.Emit(0.3); page.ToggleForTest(); page.Dispatcher.Flush();
  Equal(12.0,page.FirstPlayer); Equal(34.0,page.SecondPlayer); Equal(timestamp,page.CurrentTime); Equal(0.0,page.BallTransform.X);
  page.FirstPlayerTime.Text="12"; page.SecondPlayerTime.Text="34"; page.BallTransform.X=96;
  page.ToggleForTest(); Equal(0.0,page.FirstPlayer); Equal(0.0,page.SecondPlayer); Equal("0",page.FirstPlayerTime.Text); Equal("0",page.SecondPlayerTime.Text);
  page.CurrentTime=DateTime.Now.AddSeconds(-2); Accelerometer.Last.Emit(1); page.Dispatcher.Flush(); Equal(-160.0,page.BallTransform.X);
  if(page.FirstPlayer<1.9 || page.SecondPlayer!=0) throw new Exception("Positive reading failed"); assertions++;
  page.CurrentTime=DateTime.Now.AddSeconds(-2); Accelerometer.Last.Emit(-1); page.Dispatcher.Flush(); Equal(160.0,page.BallTransform.X);
  if(page.SecondPlayer<1.9) throw new Exception("Negative reading failed"); assertions++;
  Accelerometer.Last.Emit(0.4); page.ToggleForTest(); page.ToggleForTest(); page.CurrentTime=DateTime.Now.AddSeconds(-5); page.Dispatcher.Flush(); Equal(0.0,page.FirstPlayer); Equal(0.0,page.SecondPlayer);
  Console.WriteLine("PASS "+assertions+" stopped/active/restarted game assertions using synthetic queued readings");
 }
}
