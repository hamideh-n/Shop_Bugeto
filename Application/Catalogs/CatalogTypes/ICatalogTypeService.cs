using Application.Dtos;
using Application.Interface.DbContext;
using AutoMapper;

namespace Application.Catalogs.CatalogTypes
{
    public interface ICatalogTypeService
    {
        BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogTypeDto);
        BaseDto Remove(int Id);
        BaseDto<CatalogTypeDto> Edite(CatalogTypeDto catalogTypeDto);
        BaseDto<CatalogTypeDto> FindById(int Id);

        PaginatedItemsDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize);
    }
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public CatalogTypeService(IDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }
        public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogTypeDto)
        {
            throw new NotImplementedException();
        }

        public BaseDto<CatalogTypeDto> Edite(CatalogTypeDto catalogTypeDto)
        {
            throw new NotImplementedException();
        }

        public BaseDto<CatalogTypeDto> FindById(int Id)
        {
            var result = _databaseContext.CatalogTypes.Find(Id);
            _mapper.Map<CatalogTypeDto>(result);
            throw new NotImplementedException();
        }

        public PaginatedItemsDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public BaseDto Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
    public class CatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }
    }

    public class CatalogTypeListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int SubTypeCount { get; set; }
    }
}
