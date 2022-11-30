using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IProductInfo
{
    public Task addProductInfo(ProductInfoModel ProductInfo);
    public void updateProductInfo(ProductInfoModel ProductInfo);
    public ProductInfoModel deleteProductInfo(in int id);
    
    public bool checkProductInfo(int id);

    public Task<List<ProductInfoModel>> getProductInfos();
    public Task<ProductInfoModel> getProductInfo(int id);

    public IQueryable<ProductInfoModel> getAll(); 
}