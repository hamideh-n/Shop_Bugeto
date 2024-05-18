using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Domain.Catalogs;

namespace Infrastructure.MappingProfile
{
    public class CatalogMappingProfile:Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();
            //اگر تعداد فرزند های  اینم هایی که برگشت می دهد میلیونها رکورد باشد به حالت طبیعی کد نویسی کنیم این ساب تایم کانت رو بگیریم همه رو تو حافظه لود میکند و کانت رو نمایش می دهد
            // ولی این کار رو انجام می دهیم بهینه ترین کار رو انجام می دهد کانت رو نمایش می دهد ن اینکه کل رکورد هارو بیاورد روی رم حافظه داخلی سرور و بعد انجا کانت رو بگیرد  )
            CreateMap<CatalogType, CatalogTypeListDto>().ForMember(x => x.SubTypeCount, x => x.MapFrom(src=>src.SubType.Count));

        }
    }
}
