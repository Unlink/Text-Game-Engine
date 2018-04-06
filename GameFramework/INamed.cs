namespace GameFramework
{
    /// <summary>
    ///     Interface pomenovaných objektov
    /// </summary>
    public interface INamed
    {
        /// <summary>
        ///     Názov Objektu
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Popis Objektu
        /// </summary>
        string Description { get; }
    }
}