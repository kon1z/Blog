using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Meowv.Blog.Extensions;

public static class AlipayExtensions
{
    public static string Sign(this Dictionary<string, string> dic, string privateKey)
    {
        var sortedDic = new SortedDictionary<string, string>();

        foreach (var item in dic.Where(item => !item.Value.IsNullOrEmpty())) sortedDic.Add(item.Key, item.Value);

        var signStr = string.Join("&", sortedDic.Select(x => $"{x.Key}={x.Value}").ToArray());

        return RSASign(signStr, privateKey);
    }

    private static string RSASign(string data, string privatekey)
    {
        var rsaCsp = DecodeRSAPrivateKey(Convert.FromBase64String(privatekey));
        var signatureBytes = rsaCsp.SignData(data.GetBytes(), "SHA256");
        return Convert.ToBase64String(signatureBytes);
    }

    private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
    {
        byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

        var mem = new MemoryStream(privkey);
        var binr = new BinaryReader(mem);
        try
        {
            var twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130)
                binr.ReadByte();
            else if (twobytes == 0x8230)
                binr.ReadInt16();
            else
                return null;

            twobytes = binr.ReadUInt16();
            if (twobytes != 0x0102)
                return null;
            var bt = binr.ReadByte();
            if (bt != 0x00)
                return null;

            var elems = GetIntegerSize(binr);
            MODULUS = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            E = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            D = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            P = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            Q = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DP = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DQ = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            IQ = binr.ReadBytes(elems);

            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters
            {
                Modulus = MODULUS,
                Exponent = E,
                D = D,
                P = P,
                Q = Q,
                DP = DP,
                DQ = DQ,
                InverseQ = IQ
            };
            RSA.ImportParameters(RSAparams);
            return RSA;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
        finally
        {
            binr.Close();
        }
    }

    private static int GetIntegerSize(BinaryReader binr)
    {
        var bt = binr.ReadByte();
        if (bt != 0x02)
            return 0;
        bt = binr.ReadByte();

        int count;
        if (bt == 0x81)
        {
            count = binr.ReadByte();
        }
        else if (bt == 0x82)
        {
            var highbyte = binr.ReadByte();
            var lowbyte = binr.ReadByte();
            byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
            count = BitConverter.ToInt32(modint, 0);
        }
        else
        {
            count = bt;
        }

        while (binr.ReadByte() == 0x00) count -= 1;
        binr.BaseStream.Seek(-1, SeekOrigin.Current);
        return count;
    }
}