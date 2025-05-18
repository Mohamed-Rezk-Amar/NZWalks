namespace NZWalks.API.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRegionRepository Region { get; }
        IWalkRepository Walk { get; }
        IDifficultRepository Difficult { get; }
    }
}
