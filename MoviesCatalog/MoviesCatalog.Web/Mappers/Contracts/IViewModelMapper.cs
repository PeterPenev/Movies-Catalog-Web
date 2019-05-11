namespace MoviesCatalog.Web.Mappers.Contracts
{
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
    }
}
