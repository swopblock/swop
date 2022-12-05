// Copywrite (c) 2022 Swopblock LLC
// Created November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

using Swopblock.API.Data;

namespace Swopblock.API.Process
{
    public interface ILayer : IBroadcasting, IValidating
    {
        void Run()
        {
            var order = SigningQueue.Dequeue();

            while (ProcessOrder(order))
            {
                Broadcast(order);

                order = SigningQueue.Dequeue();
            }

            var confirmation = ConfirmingQueue.Dequeue();

            while (ProcessConfirmation(confirmation))
            {
                Confirm(confirmation);

                confirmation = ConfirmingQueue.Dequeue();
            }
        }

        bool ProcessOrder(IMessage order)
        {
            return order == null ? false : true;
        }

        bool ProcessConfirmation(IMessage confirmation)
        {
            return confirmation == null ? false : true;
        }
    }


    public interface IBroadcasting : IUser
    {
        IUser[] User { get; set; }

        void Broadcast(IMessage Call)
        {
            foreach (var user in User)
            {
                user.Sign(Call);
            }
        }
    }

    public interface IValidating : IAuto
    {
        IAuto Auto { get; set; }

        void Validate(IMessage Return)
        {
            Auto.Confirm(Return);
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

    public interface IMessageQueue
    {
        void Enqueue(IMessage order);

        IMessage Dequeue();
    }
}
