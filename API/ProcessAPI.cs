// Copywrite (c) 2022 Swopblock LLC
// Created November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;

namespace Swopblock.API.Process
{
    public interface ILayer : IBroadcasting, IValidating
    {
        void Run()
        {
            var order = OrderingQueue.Dequeue();

            while (ProcessOrder(order))
            {
                Broadcast(order);

                order = OrderingQueue.Dequeue();
            }

            var confirmation = ConfirmingQueue.Dequeue();

            while (ProcessConfirmation(confirmation))
            {
                Confirm(confirmation);

                confirmation = ConfirmingQueue.Dequeue();
            }
        }

        bool ProcessOrder(IOrdering order)
        {
            return order == null ? false : true;
        }

        bool ProcessConfirmation(IOrdering confirmation)
        {
            return confirmation == null ? false : true;
        }
    }


    public interface IBroadcasting : IUser
    {
        IUser[] User { get; set; }

        void Broadcast(IOrdering order)
        {
            foreach (var user in User)
            {
                user.Order(order);
            }
        }
    }

    public interface IValidating : IAuto
    {
        IAuto Auto { get; set; }

        void Validate(IOrdering order)
        {
            Auto.Confirm(order);
        }
    }

    public interface IInternetConnectionLayer : ILayer
    {
        IValidating ValidatingConnection { get; set; }

        IBroadcasting BroadcastingConnection { get; set; }

        void SetConnections(IValidating validator, IBroadcasting broadcaster);
    }

    public interface IApplicationLayer : ILayer
    {
        IInternetConnectionLayer IncentiveConnection { get; set; }

        void SetIncentiveLayer(IIncentiveLayer IncentiveLayer);
    }

    public interface IIncentiveLayer : ILayer
    {
        IInternetConnectionLayer ApplicationConnection { get; set; }

        IInternetConnectionLayer ConsensusConnection { get; set; }

        void SetConsensusLayer(IConsensusLayer ConsensusLayer);
    }

    public interface IConsensusLayer : ILayer
    {
        IInternetConnectionLayer IncentiveLayerConnection { get; set; }

        IInternetConnectionLayer NetworkLayerConnection { get; set; }

        void SetNetworkLayer(INetworkLayer NetworkLayer);
    }

    public interface INetworkLayer : ILayer
    {
        IInternetConnectionLayer OrderingConsensusLayer { get; set; }

        IInternetConnectionLayer ConfirmingConsensusLayer { get; set; }

        void SetInternetConnection(IInternetConnectionLayer InternetConnection);
    }

    public interface IOrderQueue
    {
        void Enqueue(IOrdering order);

        IOrdering Dequeue();
    }
}
