using Xend_Finance.Models;

namespace Xend_Finance.Interface
{
    public interface IEventBus
    {
        void publish(TransactionsModel model);
    }
}
