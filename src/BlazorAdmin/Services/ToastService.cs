using System.Timers;
using Timer = System.Timers.Timer;
namespace BlazorAdmin.Services
{
    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
    // 显示一条消息，并在3秒后自动隐藏。
    public class ToastService
    {
        // 定义一个事件OnShow，当需要显示Toast消息时触发。
        public event Action<string, ToastLevel> OnShow;
        // 定义一个事件OnHide，当需要隐藏Toast消息时触发。
        public event Action OnHide;

        private Timer Countdown;
        // 定义一个方法ShowToast，用于触发OnShow事件，传递消息内容和消息级别。
        public void ShowToast(string messge, ToastLevel level)
        {
            OnShow?.Invoke(messge, level);  // 发出显示指令
            StartCountdown();              // 重置倒计时，3秒后自动隐藏
        }

        private void StartCountdown()
        {
            SetCountdown();
            // 处理连续多次点击的情况
            if (Countdown.Enabled)
            {
                Countdown.Stop();  // 如果已经在倒计时，强制先停掉
                Countdown.Start(); // 重新开始计算3秒
            }
            else
            {
                Countdown.Start(); // 如果没有在倒计时，直接开始
            }
        }
        private void SetCountdown()
        {
            // 第一次使用时，才创建对象
            if (Countdown == null)
            {
                Countdown = new Timer(3000);  // 设置计时器的间隔为3000毫秒（3秒）
                Countdown.Elapsed += HideToast;      // 当计时器的时间到达时，触发HideToast方法
                Countdown.AutoReset = false;        // 设置计时器为单次触发，即只触发一次后停止
            }
        }
        // 定义一个回调函数HideToast，当计时器到达3秒时触发，调用OnHide事件来隐藏Toast消息。
        // 为什么配置成(object source, ElapsedEventArgs args)这样？这是因为Timer.Elapsed是一个事件委托
        // 委托类型是ElapsedEventHandler（F12）查看定义，参数就是(object sender, ElapsedEventArgs e)，所以回调函数必须匹配这个签名。
        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();  
        }
    }
}
