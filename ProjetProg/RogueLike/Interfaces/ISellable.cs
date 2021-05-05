using RogueLike.Core.Merchants;
namespace RogueLike.Interfaces
{
    /// <summary>
    /// This interface is implemented by every elements that can be sold by a 
    /// merchant.
    /// </summary>
    public interface ISellable
    {

        /// <value>The cost of the element</value>
        int Cost { get; set; }


        /// <value> A reference to the merchant that sells the element</value>
        Merchant SoldByMerchant { get; set; }


        /// <value> The x position of the sellable on the map</value>
        int PosX { get; set; }

        /// <value> The y position of the sellable on the map </value>
        int PosY { get; set; }
    }
}