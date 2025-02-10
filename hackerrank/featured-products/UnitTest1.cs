using System.Runtime.InteropServices;

namespace featured_products;

public class UnitTest1
{
    public static string featuredProduct(List<string> products)
    {
        Dictionary<string, int> productsRank = new Dictionary<string, int>();
        foreach (var product in products) {
            productsRank.TryGetValue(product, out int times);
            times++;
            productsRank[product] = times;
        }

        int candidateRank = 0;
        string candidateProduct = "";
        foreach (var keyValue in productsRank) {
            if (keyValue.Value > candidateRank) {
                candidateRank = keyValue.Value;
                candidateProduct = keyValue.Key;
            } else if (keyValue.Value == candidateRank && string.Compare(keyValue.Key, candidateProduct) > 0) {
                candidateProduct = keyValue.Key;
            }
        }
        
        return candidateProduct;
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(
            "yellowShirt",
            featuredProduct(
                new List<string> { "yellowShirt","redHat","blackShirt","bluePants","redHat","pinkHat","blackShirt","yellowShirt","greenPants","greenPants" }
            )
        );
        // Assert.Equal(
        //     "redShirt",
        //     featuredProduct(
        //         new List<string> { "redShirt", "greenPants", "redShirt", "orangeShoes", "blackPants", "blackPants" }
        //     )
        // );
    }
}