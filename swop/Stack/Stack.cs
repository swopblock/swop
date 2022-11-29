﻿// Copywrite (c) 2022 Swopblock LLC
// Modified November 29, 2022 3:51 PM ET by Jeff Hilde, jeff@swopblock.org

namespace Swopblock.Stack
{

    public interface IAPP : IUser, IAuto
    {
        ICORE[] CORE { get; set; }
    }

    public interface ICORE : IUser, IAuto, ILayer
    {
        ICARRIER[] CARRIER { get; set; }
    }

    public interface ICARRIER : IUser, IAuto, ILayer
    {
        ILayer[] Layers { get; set; }
    }

    public interface IUser
    {
        IOrderQueue OrderingQueue { get; }

        void Order(IOrder order)
        {
            OrderingQueue.Enqueue(order);
        }
    }

    public interface IAuto
    {
        IOrderQueue ConfirmingQueue { get; }

        void Confirm(IOrder order)
        {
            ConfirmingQueue.Enqueue(order);
        }
    }

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

        bool ProcessOrder(IOrder order)
        {
            return order == null ? false : true;
        }

        bool ProcessConfirmation(IOrder confirmation)
        {
            return confirmation == null ? false : true;
        }
    }

    public interface IOrder
    {

    }

    public interface IBroadcasting : IUser
    {
        IUser[] User { get; set; }

        void Broadcast(IOrder order)
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

        void Validate(IOrder order)
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
        void Enqueue(IOrder order);

        IOrder Dequeue();
    }
}
