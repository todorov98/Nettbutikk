namespace Nettbutikk.Models
{
    /// <summary>
    /// All entities implement this interface.
    /// </summary>
    public interface IEntity
    {
        public bool Commit();
        public bool Exists();
    }
}
