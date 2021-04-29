namespace RogueLike.Interfaces
{
    public interface ISellable 
    {
        int Cost{get;set;}

        bool InMerchantStall{get;set;}

        int PosX { get; set; }

        int PosY { get; set; }
    }
}