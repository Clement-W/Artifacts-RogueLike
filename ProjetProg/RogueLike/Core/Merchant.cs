using System.Collections.Generic;
using RogueLike.Interfaces;
namespace RogueLike.Core
{
    public abstract class Merchant : Character
    {
        public char AlternateSymbol1{get;set;}
        public char AlternateSymbol2{get;set;}

        public int MerchantLevel{get;set;} // equal to the number of artefacts found by the player

        public List<ISellable> Stall{get;set;} // The sellable items proposed by the merchant

        public Merchant(int merchantLevel){
            Stall = new List<ISellable>();
            MerchantLevel=merchantLevel;
            GenerateStall();
        }

        public void ChangeSymbol(){
            Symbol = (Symbol==AlternateSymbol1) ? AlternateSymbol2 : AlternateSymbol1;
        }

        public abstract void GenerateStall();





        

        
    }
}