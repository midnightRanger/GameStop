using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.DAL.Repository;

public class ProductInfoRepository : IProductInfo
{
    private ApplicationContext _db = new();

    public ProductInfoRepository(ApplicationContext db)
    {
        _db = db; 
    }

    
    public async Task addProductInfo(ProductInfoModel productInfo)
    {
        _db.ProductInfo.Add(productInfo);
        await _db.SaveChangesAsync();
    }

    public void updateProductInfo(ProductInfoModel productInfo)
    {
        _db.Entry(productInfo).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public ProductInfoModel deleteProductInfo(in int id)
    {
        ProductInfoModel? productInfo = _db.ProductInfo.Find(id);

        if (productInfo != null)
        {
            _db.ProductInfo.Remove(productInfo);
            _db.SaveChanges();
            return productInfo;
        }

        throw new ArgumentNullException();
    }
    
    public bool checkProductInfo(int id)
    {
        return _db.ProductInfo.Any(e => e.Id == id);
    }

    public async Task<List<ProductInfoModel>> getProductInfos()
    {
        return await _db.ProductInfo.ToListAsync();
    }

    public async Task<ProductInfoModel> getProductInfo(int id)
    {
        ProductInfoModel? productInfo = await _db.ProductInfo.FindAsync(id);

        if (productInfo != null)
        {
            return productInfo;
        }

        throw new ArgumentNullException();
    }

    public IQueryable<ProductInfoModel> getAll()
    {
        return _db.ProductInfo; 
    }
}