using SnowLeopard.Lynx.Extensions;
using Xunit;
using static SnowLeopard.Lynx.Extensions.HashExtension;

namespace SnowLeopard.Lynx.UnitTest
{
    public class HashExtensionTest
    {
        public const string INPUT_STR = "9C55DBE1-5FB6-4EE9-B956-574999DB023A";

        #region expected 期望值

        public const string INPUT_STR_MD5 = "d747f8c6fad17ec3c38e2ba835d41415";
        public const string INPUT_STR_SHA1 = "1ac9bcb33b93ce6b6afa35f8502025eee87c9abb";
        public const string INPUT_STR_SHA256 = "f1e77390a50f687c543e0b1c1c6014294c3008eb0a9a182667d914f4df7843ba";
        public const string INPUT_STR_SHA384 = "804e3e0573c15f88f31c211f495ce8de9f24690a41535f9e380de750f254652d2d192114668f6314d1adb23219d08b14";
        public const string INPUT_STR_SHA512 = "f65bccda780b65f72cfb69e3145e9348c239072249a77725c9ce6ab22e10309d7e1f5ba3378d3e67945a93d38b97734302dfa51838f574380787e6523b0cec6e";
        public const string INPUT_STR_HMACMD5 = "306e306482e0c890bdee1778271c3c30";
        public const string INPUT_STR_HMACSHA1 = "574d3b86d9ff1bab484e856e0a4a6ad5101c4a67";
        public const string INPUT_STR_HMACSHA256 = "604c1e992ea09b852166babc923eb827b2cf922e9e43553a59be768b2e6632ea";
        public const string INPUT_STR_HMACSHA384 = "3dfc1b7ecdf5757b02019db4f45d8d40377db1c2420ec333841d1648c0bb21400b6f6fdb84a0492d81a63aed28b70245";
        public const string INPUT_STR_HMACSHA512 = "2b2487326956b8781b753c2942a8bf0b8a64757de3985b87b75c35c1b1ed1e66be81484274b4af23f35b1b0d8a8fd91b7f5bb39142c1665a47fc5e6cc99ca87e";


        public const string INPUT_STR_MD5_BASE64 = "10f4xvrRfsPDjiuoNdQUFQ==";
        public const string INPUT_STR_SHA1_BASE64 = "Gsm8szuTzmtq+jX4UCAl7uh8mrs=";
        public const string INPUT_STR_SHA256_BASE64 = "8edzkKUPaHxUPgscHGAUKUwwCOsKmhgmZ9kU9N94Q7o=";
        public const string INPUT_STR_SHA384_BASE64 = "gE4+BXPBX4jzHCEfSVzo3p8kaQpBU1+eOA3nUPJUZS0tGSEUZo9jFNGtsjIZ0IsU";
        public const string INPUT_STR_SHA512_BASE64 = "9lvM2ngLZfcs+2njFF6TSMI5ByJJp3clyc5qsi4QMJ1+H1ujN40+Z5Rak9OLl3NDAt+lGDj1dDgHh+ZSOwzsbg==";
        public const string INPUT_STR_HMACMD5_BASE64 = "10f4xvrRfsPDjiuoNdQUFQ==";
        public const string INPUT_STR_HMACSHA1_BASE64 = "Gsm8szuTzmtq+jX4UCAl7uh8mrs=";
        public const string INPUT_STR_HMACSHA256_BASE64 = "8edzkKUPaHxUPgscHGAUKUwwCOsKmhgmZ9kU9N94Q7o=";
        public const string INPUT_STR_HMACSHA384_BASE64 = "gE4+BXPBX4jzHCEfSVzo3p8kaQpBU1+eOA3nUPJUZS0tGSEUZo9jFNGtsjIZ0IsU";
        public const string INPUT_STR_HMACSHA512_BASE64 = "9lvM2ngLZfcs+2njFF6TSMI5ByJJp3clyc5qsi4QMJ1+H1ujN40+Z5Rak9OLl3NDAt+lGDj1dDgHh+ZSOwzsbg==";

        #endregion

        #region x2Str

        [Fact]
        public void Md5Str()
        {
            Assert.Equal(INPUT_STR_MD5, INPUT_STR.Md5());
        }
        [Fact]
        public void SHA1Str()
        {
            Assert.Equal(INPUT_STR_SHA1, INPUT_STR.Sha1());
        }
        [Fact]
        public void SHA256Str()
        {
            Assert.Equal(INPUT_STR_SHA256, INPUT_STR.Sha256());
        }
        [Fact]
        public void SHA384Str()
        {
            Assert.Equal(INPUT_STR_SHA384, INPUT_STR.Sha384());
        }
        [Fact]
        public void SHA512Str()
        {
            Assert.Equal(INPUT_STR_SHA512, INPUT_STR.Sha512());
        }

        [Fact]
        public void HMACMd5Str()
        {
            Assert.Equal(INPUT_STR_HMACMD5, INPUT_STR.Hmac(AlgorithmNameEnum.HMACMD5));
        }
        [Fact]
        public void HMACSHA1Str()
        {
            Assert.Equal(INPUT_STR_HMACSHA1, INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA1));
        }
        [Fact]
        public void HMACSHA256Str()
        {
            Assert.Equal(INPUT_STR_HMACSHA256, INPUT_STR.Hmac());
        }
        [Fact]
        public void HMACSHA384Str()
        {
            Assert.Equal(INPUT_STR_HMACSHA384, INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA384));
        }
        [Fact]
        public void HMACSHA512Str()
        {
            Assert.Equal(INPUT_STR_HMACSHA512, INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA512));
        }

        #endregion

        #region Md5Base64

        [Fact]
        public void Md5Base64()
        {
            Assert.Equal(INPUT_STR_MD5_BASE64, INPUT_STR.Md5());
        }
        [Fact]
        public void SHA1Base64()
        {
            Assert.Equal(INPUT_STR_SHA1_BASE64, INPUT_STR.Sha1());
        }
        [Fact]
        public void SHA256Base64()
        {
            Assert.Equal(INPUT_STR_SHA256_BASE64, INPUT_STR.Sha256());
        }
        [Fact]
        public void SHA384Base64()
        {
            Assert.Equal(INPUT_STR_SHA384_BASE64, INPUT_STR.Sha384());
        }
        [Fact]
        public void SHA512Base64()
        {
            Assert.Equal(INPUT_STR_SHA512_BASE64, INPUT_STR.Sha512());
        }

        #endregion
    }
}
