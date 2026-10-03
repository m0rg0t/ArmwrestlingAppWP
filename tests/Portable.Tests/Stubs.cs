// Test-only surface substitutes. Never built by the Phone project; no real sensor/UI/share APIs.
using System;
using System.Collections.Generic;
namespace System.Windows { public class RoutedEventArgs : EventArgs {} }
namespace System.Windows.Controls { public class Control { public object Content; public string Text; public double Width; public double X; } }
namespace System.Windows.Documents { } namespace System.Windows.Input { } namespace System.Windows.Media { } namespace System.Windows.Media.Animation { } namespace System.Windows.Shapes { }
namespace Microsoft.Phone.Controls {
 public class PhoneApplicationPage { public TestDispatcher Dispatcher = new TestDispatcher(); }
 public class TestDispatcher { public Queue<Action> Queue = new Queue<Action>(); public void BeginInvoke(Action action) { Queue.Enqueue(action); } public void Flush() { while(Queue.Count > 0) Queue.Dequeue()(); } }
}
namespace Microsoft.Devices.Sensors {
 public class AccelerometerReadingEventArgs : EventArgs { public double X; }
 public class Accelerometer { public static Accelerometer Last; public Accelerometer() { Last=this; } public event EventHandler<AccelerometerReadingEventArgs> ReadingChanged; public void Start() {} public void Stop() {} public void Emit(double x) { ReadingChanged(this,new AccelerometerReadingEventArgs { X=x }); } }
}
namespace Microsoft.Phone.Tasks { public class ShareStatusTask { public string Status; public void Show() { throw new Exception("Sharing is outside test scope"); } } }
namespace ArmwrestlingAppWP {
 public partial class MainPage {
  public System.Windows.Controls.Control BallTransform = new System.Windows.Controls.Control();
  public System.Windows.Controls.Control Track = new System.Windows.Controls.Control { Width=400 };
  public System.Windows.Controls.Control Ball = new System.Windows.Controls.Control { Width=80 };
  public System.Windows.Controls.Control FirstPlayerTime = new System.Windows.Controls.Control { Text="0" };
  public System.Windows.Controls.Control SecondPlayerTime = new System.Windows.Controls.Control { Text="0" };
  public System.Windows.Controls.Control StartGameButton = new System.Windows.Controls.Control();
  void InitializeComponent() {} public void ToggleForTest() { StartGameButton_OnClick(null,null); }
 }
}
