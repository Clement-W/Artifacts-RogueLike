namespace testRogueSharp.Interfaces{
    public interface IActor{
        string Name{get;set;}
        int Awareness{get;set;} //pour calculer le champ de vision
    }
}