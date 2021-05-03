namespace RogueLike.Core
{
    public class Fist : AttackEquipment
    {

        public Fist(){
            Name = "Fist";
            AttackBonus=0;
            AttackRange.Add(1,1);
            Symbol = Icons.fistSymbol;
        }

    }
}