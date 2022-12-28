using System.Text;
using VideoMonitoring.Models;
using VideoMonitoring.ModelViewer;

namespace VideoMonitoring.Services.VideoServices;

public class VideoFileHandler : IVideoFileHandler
{
    public enum EVideoFileHandlerResponse
    {
        Ok,
        Error
    }

    public EVideoFileHandlerResponse CreateVideoFile(VideoModelViewer model, Guid serverId)
    {
        var sb = new StringBuilder();
        sb.AppendLine(model.Id.ToString());
        sb.AppendLine(model.Description);
        sb.AppendLine(model.Data);

        byte[] byteArray = Encoding.ASCII.GetBytes(sb.ToString());

        if (!Directory.Exists($"Files/{serverId.ToString()}"))
        {
            Directory.CreateDirectory($"Files/{serverId.ToString()}");
        }

        try
        {
            using (FileStream fs = File.Create($"Files/{serverId.ToString()}/{model.Id}.txt"))
            {
                fs.Write(byteArray, 0, byteArray.Length);
            }

            return EVideoFileHandlerResponse.Ok;
        } catch (Exception e)
        {
            return EVideoFileHandlerResponse.Error;
        }
    }

    public bool DeleteVideoFile(Video video)
    {
        throw new NotImplementedException();
    }

    public string GetVideoContent(Guid serverId, Guid videoId)
    {
        var content = File.ReadAllLines($"Files/{serverId.ToString()}/{videoId.ToString()}.txt");

        var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(content[2]));

        return base64;
    }
}
