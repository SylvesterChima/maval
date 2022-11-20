namespace Marval.HelperInterface
{
    public interface IFileHelper
    {
        Task<string> UploadCSV(IFormFile file, string userName);
    }
}
