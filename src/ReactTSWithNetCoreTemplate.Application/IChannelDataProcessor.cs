
namespace ReactTSWithNetCoreTemplate.Application
{
    public interface IChannelDataProcessor<T>
    {
        void CompletePublishing();
        Task PublishMessageAsync(T message);
        Task StartProcessingAsync(CancellationToken cancellationToken = default);
    }
}