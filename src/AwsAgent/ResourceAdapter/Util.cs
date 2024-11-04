namespace AwsAgent.ResourceAdapter;

public class Util
{
   public static ResourceTag[] ConvertTag(dynamic tags)
    {
        List<ResourceTag> result = [];
        try
        {
            foreach (var tag in tags)
            {
                result.Add(new ResourceTag()
                {
                    Key = tag.Key,
                    Value = tag.Value,
                });
            }
        }
        catch (Exception)
        {
        }

        return [.. result];
    }

    public static string ConvertArnToAccountId(string arn)
    {
        Regex regex = new Regex(Const.AWS_ARN_Pattern);
        Match match = regex.Match(arn);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        return "";
    } 
}