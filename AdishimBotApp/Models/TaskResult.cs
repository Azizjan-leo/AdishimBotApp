
namespace AdishimBotApp.Models
{
    public class TaskResult
    {
        public TaskResult(bool isSuccess, string msg)
        {
            IsSuccess = isSuccess;
            Msg = msg;
        }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
    }
}
