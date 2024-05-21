using Application.Dtos;
using Application.Interface.DbContext;
using AutoMapper;
using Common;
using Domain.Catalogs;


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
            var data = _mapper.Map<CatalogType>(catalogTypeDto);
            var add = _databaseContext.CatalogTypes.Add(data);
            _databaseContext.SaveChanges();
            return  new BaseDto<CatalogTypeDto>(new List<string> { $"تایپ {catalogTypeDto.Type} با موفقیت در سیستم ثبت شد" },_mapper.Map<CatalogTypeDto>(data),true);
        }

        public BaseDto<CatalogTypeDto> Edite(CatalogTypeDto catalogTypeDto)
        {
             var data = _databaseContext.CatalogTypes.SingleOrDefault(x=>x.Id==catalogTypeDto.Id);
             var result= _mapper.Map<CatalogType>(data);
            _databaseContext.SaveChanges();
            return new BaseDto<CatalogTypeDto>(new List<string> { "دیتا با موفقیت آپدیت شد" },_mapper.Map<CatalogTypeDto>(result),true);
        }

        public BaseDto<CatalogTypeDto> FindById(int Id)
        {
            var data = _databaseContext.CatalogTypes.Find(Id);
            var result= _mapper.Map<CatalogTypeDto>(data);
            return new BaseDto<CatalogTypeDto>(new List<string> { "دیتا با موفقیت یافت شد " },result,true);
        }

        public PaginatedItemsDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {
            var totalCount = 0;
            var data = _databaseContext.CatalogTypes.AsQueryable().PagedResult(page, pageSize,out totalCount);
            var result=_mapper.ProjectTo<CatalogTypeListDto>(data).ToList();
            return new PaginatedItemsDto<CatalogTypeListDto> (page, pageSize,totalCount,result);
        }

        public BaseDto Remove(int Id)
        {
            var data = _databaseContext.CatalogTypes.SingleOrDefault(x => x.Id == Id);
            _databaseContext.CatalogTypes.Remove(data);//new catalogType{Id==Id}
            return new BaseDto(new List<string> {$"این پیام با موفقیت حذف شد "},true);
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
