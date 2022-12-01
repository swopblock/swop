// Copywrite (c) 2022 Swopblock LLC
// Created December 1, 2022 12:13 AM ET by Jeff Hilde, jeff@swopblock.org


namespace Swopblock.API.Data
{

    public interface IOrdering
    {
        IOffer Offer { get; set; }

        IOrder Order { get; set; }

        IDeed Deed { get; set; }

        IExpiration Expiration { get; set; }
    }

    public interface IOffer : IAsset 
    {  
        IValue OfferAtLeast { get; set; }

        IValue OfferAtMost { get; set; }
    }

    public interface ICashOffer : IOffer { }

    public interface IAssetOffer : IOffer { }

    public interface IBtcOffer : IAssetOffer { }

    public interface IEthOffer : IAssetOffer { }


    public interface IOrder 
    { 
        IValue OrderAtLeast { get; set; }

        IValue OrderAtMost { get; set; }
    }

    public interface ICashOrder : IOrder { }

    public interface IAssetOrder : IOrder { }

    public interface IBtcOrder : IAssetOrder { }

    public interface IEthOrder : IAssetOrder { }


    public interface IDeed : IAsset 
    { 
        IValue Deed { get; set; }
    }

    public interface ICashDeed : IDeed { }

    public interface IAssetDeed : IDeed { }

    public interface IBtcDeed : IAssetDeed { }

    public interface IEthDeed : IAssetDeed { }


    public interface IExpiration 
    {
        IValue LockUpCash { get; set; }

        IValue LockInVolume { get; set; }

        IValue LockOutVolume { get; set; }
    }

    public interface ICashExpiration : IExpiration { }

    public interface IAssetExpiration : IExpiration { }

    public interface IBtcExpiration : IAssetExpiration { }

    public interface IEthExpiration : IAssetExpiration { }

    public interface IAsset : IValue
    {
        IAsset Input { get; set; }

        IFaceValue FaceValue { get; set; }

        ICashValue CashValue { get; set; }

        IRoute Route { get; set; }

        IAddress Address { get; set; }

        ISignature Signature { get; set; }
    }

    public interface IValue 
    {
        decimal Value { get; set; } 
    } 

    public interface IFaceValue : IValue { }

    public interface ICashValue : IValue { }

    public interface IRoute { }

    public interface IAddress { }

    public interface ISignature { }
}
