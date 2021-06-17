namespace RogueLike.Interfaces {

    /// <summary>
    /// This interface is implemented by every elements that are
    /// animated periodically.
    /// </summary>
    public interface IAnimated {

        char AlternateSymbol1 { get; set; }
        char AlternateSymbol2 { get; set; }

        char Symbol{get;set;}

        /// <summary>
        /// Change the symbol of the element by switching to AlternateSymbol1
        /// or AlternateSymbol2.
        /// </summary>
        void ChangeSymbolAlternative();



    }
}
