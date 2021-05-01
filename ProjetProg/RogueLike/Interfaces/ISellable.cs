using RogueLike.Core;
namespace RogueLike.Interfaces
{
    public interface ISellable 
    {
        int Cost{get;set;}

        Merchant SoldByMerchant{get;set;}

        int PosX { get; set; }

        int PosY { get; set; }
    }
}