using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.ZFB
{
    /// <summary>
    /// 支付宝支付配置
    /// </summary>
    public class ZFBPayConfig
    {
        /// <summary>
        /// 公钥
        /// </summary>
        private static string publicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsfUPw3psCYxTKuANAf4t5SoSX9rmwbwn9LB4QtrNqYWVtCpuRkC9ZyOqvD3j5kz3QlQvjgx2vyI16zO/wnQOLwve74oiq8Nk8yfGg84dQ0Oc0aL+q93DtZfHjeAoK2j/309MzmiotorGs3kNYlZFwtD/+r9RzN+Il0X9KR8oEe2DhtcvLy4iiYMOHNENfg20ATGZIZXR2INGBcQIeP3PHNTgDHh5E2asumILZHRg4my2T1Bphc57DDlxNSkwMFOnxYkvMiBu6oy0klnxXM5vdP6Bvt3xbZD+CjgMTzCtohnW7jRfjBA+Pt7KnCUC1SwUXmVHSUQmY6oSbocEg5WjrQIDAQAB";

        /// <summary>
        /// 私钥
        /// </summary>
        private static string privateKey = @"MIIEowIBAAKCAQEAulZj4swi41aFMMNREwYAXvk46arLP9HyBq97pDqPIz9RKTqr
tzqUfoh/HBl/OKzpYzqdmhRzCpyixIMXNiiQDGILvi/RK1+BGz9wDXvEFNZfg4DN
sm+QD3VljVpXkq5IqBLkJNj7vtcXk2F4Gg+u7JpiHCFg/OVLab+2SK7TC3IoqBGW
ipgdnVbZk+r9Zv2yBZFfz/wKHo7m9uTz9vlOxbk1crbyuhxvlBeRQLbA/hf8+DCJ
4l1FaJT9W4M5O1Y31YormWBHuu0qhgSzYvlZWB1jGhLR/Ht9X+4ijiGuNV13pxvR
aXS8hIKfs8gR0DgH4m407qThGx0ElYgN4HUTewIDAQABAoIBAEom1tkKI1gUtiwR
jdHkMYGZ6+wQ65EaGxZN/wX7x7pfGA0wK+VeinlQGEU2YEpNhRLX4J/QQ3eVnbBV
+oay3aAP5Fxq6vI3r0kIla7H8d/Y72mFeFXpz4pXTXJS4Uad1IwN+HwxXP0020zr
HMBCPXoqdaB8J/x7wubyQ7fo2lNTDL32Wo3UWB9Qx2fzp/pN3ZGX4AH45qQtVDPU
Bs50vwly2yBJFHroKvjquLHs5WxPlb3T1g41SBLJFuNEIyw30W6CEfsq6D1f7NDI
T9rj91rH08Ged17GXlG+LOcrtE0szXmDRMOLA5OtxJZbxPdu+eRVkdUo+K5c6ME+
nsiUEwECgYEA3pybKvj3/9y/DjvM31ewvkGRn4iJIOZePsxWtU3tnJ3LubuhHe+t
2cbu52tg8rqbnminLBV5GeRgDIhOZ9tnWYt6BzaXEoPGReQv/C3uv62ZrEJQkSas
NiBQ/8YRiKVaFLMOfiSCHuQWcWsw+EoWLKrvawL6Wk4yhG3FxY6msx0CgYEA1kj/
ch/Fb9s0eC0iC2PlYCOXm5asn26mNYErWkcWPkRWCYFUw3m9DhhVGB530IdxCo/B
tguaIqRkRsUexFXuQ17qwhivu0JhN7UZL3rkuliKKviPwBO2LZGhi/S9RBnlmTuB
P7pFKCmEwd/86tjclPsIx53RVyQG25ReZSWlRXcCgYBMxsBxerq14SF3vEI1iV3o
0F50IjgBQ9KfYARLtZvM11NrNH4mAAL6yDSfVhZywM9pO/jkKGfC+VPzHpPEGBS1
+nfUwwwpk48vjbrLA+CC9VG56ok09pyQcmbdQCfN9BF0cCkAcoD6PHpNFcYm7A5n
y3CVfMpxmOStbdq2/zMZNQKBgQC3/dXYWA9fDAjxZ09kGFJLSkRTA/0UqB1us4pV
6dPfgy2c27+8WDtMbvghzYdzNdiKCQV7Glsug5jWa0sPqfj7P/Sy1B+P2l8/RfBq
JEtg9cUtQF+tvT8fwXvEgFBCuTm7aKyB256l7YBgN8MiozJDeYkAXTHycKyyvaKP
ltNh4QKBgBlofo81i9t+aKcPPDUSSivX/rUCCzlbUer1q7mfHnmxBSCTkg8O5gjZ
3PRT+KTstnI/n5c1hwADNDxfpjxasBRhBoeiEbg6YPyo0HShbBYI94oDHqe62UME
OUskoR+TSdahAQP5hWig9F9XEvl20O7fnOm/MJQPwTsmvuJZQZ9n";

        /// <summary>
        /// 支付宝回调地址
        /// </summary>
        public static string NotifyUrl = "http://m.leadyssg.com/api/OrderApi/ZCallBack";

        /// <summary>
        /// 初始化支付宝支付配置
        /// </summary>
        public static ZPayCenterConfig config = new ZPayCenterConfig()
        {
            AppSource = "1",
            AppId = "2017050207084282",
            AppPrivateKey = privateKey,
            AppPublicKey = publicKey
        };

    }
}
