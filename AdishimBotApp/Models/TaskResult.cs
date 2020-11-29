
namespace AdishimBotApp.Models
{
    public class TaskResult
    {
        public TaskResult(bool isSuccess, string msg, Special spec = Special.NoSpec)
        {
            IsSuccess = isSuccess;
            Msg = msg;
            Spec = spec;       
        }

        public Special Spec { get; set; }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
    }
}
