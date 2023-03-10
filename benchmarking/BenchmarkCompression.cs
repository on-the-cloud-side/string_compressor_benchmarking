using System.Text;
using BenchmarkDotNet.Attributes;

namespace gzipstream;

// must be public class and cannot be sealed
[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchmarkCompression
{
    static readonly string originalString = string.Empty;
    static BenchmarkCompression ()
    {
#if USE_RANDOM_STRING_SPECIMEN
        using (var specimen_ = new RandomStringSpecimen())
        {
            originalString = specimen_.UrlEncodedRandomString;
        }
#else
        originalString = new StringSpecimen().Payload;
#endif
    }

    [Benchmark]
    public void GZipCompress()
    {
        byte[] dataToCompress = Encoding.UTF8.GetBytes(originalString);
        var compressedData = GZipCompressor.Compress(dataToCompress);
    }

    [Benchmark]
    public void BrotliCompress()
    {
        byte[] dataToCompress = Encoding.UTF8.GetBytes(originalString);
        var compressedData = Brotli.Compress(dataToCompress);
    }

    [Benchmark]
    public void DeflatorCompress()
    {
        byte[] dataToCompress = Encoding.UTF8.GetBytes(originalString);
        var compressedData = Brotli.Compress(dataToCompress);
    }
}