using GameStop.Models;

namespace GameStop.DAL.Interface;

public interface IProduct
{
    public Task addProduct(ProductModel product);
    public void updateProduct(ProductModel product);
    public ProductModel deleteProduct(in int id);
    
    public bool checkProduct(int id);

    public Task<List<ProductModel>> getProducts();
    public ProductModel getProduct(int? id);

    public IQueryable<ProductModel> getAll(); 
}