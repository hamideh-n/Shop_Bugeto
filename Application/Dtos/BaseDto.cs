namespace Application.Dtos
{
    public class BaseDto<T>
    {
        public BaseDto(List<string> message, T data, bool isSuccess)
        {
            Message = message;
            Data = data;
            IsSuccess = isSuccess;
        }

        public List<string> Message { get; private set; }
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
    }
    public class BaseDto
    {
        public BaseDto(List<string> message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public List<string> Message { get; private set; }
        public bool IsSuccess { get; private set; }
    }
}
