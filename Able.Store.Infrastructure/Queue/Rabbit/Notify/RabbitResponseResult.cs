namespace Able.Store.Infrastructure.Queue.Rabbit.Notify
{
    public class RabbitResponseResult
    {

        private static readonly string Stuffix = ".r";
        public static string GetCorrelationId(string requestCorrelationId)
        {
            var correlationId= string.Concat(requestCorrelationId, Stuffix);

            return correlationId;
        }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public int ErrorCode { get; set; }

    }
}
