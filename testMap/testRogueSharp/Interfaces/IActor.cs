namespace testRogueSharp.Interfaces
{
    public interface IActor
    {
        string Name { get; set; }
        int Awareness { get; set; } //pour calculer le champ de vision
        int Attack { get; set; }
        int AttackChance { get; set; }
        int Defense { get; set; }
        int DefenseChance { get; set; }
        int Gold { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        int Speed { get; set; }
    }
}