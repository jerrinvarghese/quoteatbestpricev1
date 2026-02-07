namespace Core.Specifications;
 
 public class ProductSpecParams
 {
    public int? TypeId { get; set; }
public int? BrandId { get; set; }
public int? MakeId { get; set; }
public int? ModelId { get; set; }
    //public int? kilometers { get; set; }
public int? MinKilometers { get; set; }
public int? MaxKilometers { get; set; }

public int? MinYear { get; set; }
public int? MaxYear { get; set; }
public int? MinPrice { get; set; }
public int? MaxPrice { get; set; }
public int? OwnerNumber { get; set; }
public string? TransmissionType { get; set; }
public string? Location { get; set; }
public string? FuelType { get; set; }
     private const int MaxPageSize = 50;
     public int PageIndex { get; set; } = 1;
 
     private int _pageSize = 6;
     public int PageSize
     {
         get => _pageSize;
         set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
     }
     
 
     private List<string> _brands = [];
     public List<string> Brands
     {
         get => _brands;
         set
         {
             _brands = value.SelectMany(x => x.Split(',',
                 StringSplitOptions.RemoveEmptyEntries)).ToList();
         }
     }
 
     private List<string> _types = [];
     public List<string> Types
     {
         get => _types;
         set
         {
             _types = value.SelectMany(x => x.Split(',',
                 StringSplitOptions.RemoveEmptyEntries)).ToList();
         }
     }
 
     public string? Sort { get; set; }
 
     private string? _search;
     public string Search
     {
         get => _search ?? "";
         set => _search = value.ToLower();
     }
     
 
 }