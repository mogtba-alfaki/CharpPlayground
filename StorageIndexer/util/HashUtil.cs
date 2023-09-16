namespace StorageIndexer; 

public class HashUtil {
    public static string GenerateHash(string text) {
        var hashCode = Math.Abs(text.GetHashCode()).ToString();
        return hashCode; 
    }
}