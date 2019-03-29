using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EZGoal
{
    public class IPRecord
    {
        public string IP { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
    }

    public class CZIPDB
    {
        private byte[] data;
        private Regex regex = new Regex(@"^(((25[0-5])|(2[0-4]\d)|(1\d{2})|([1-9]?\d))\.){3}((25[0-5])|(2[0-4]\d)|(1\d{2})|([1-9]?\d))$");
        private uint firstIndexOffset; //第一条索引绝对偏移
        private uint lastIndexOffset; //最后一条索引绝对偏移
        private uint ipCount; //索引数量，即IP记录数
        public UInt32 Count
        {
            get { return ipCount; }
        }

        public CZIPDB(string dataPath)
        {

            using (FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
            }
            //文件头共8字节，前4字节为第一条索引绝对偏移，后4字节为最后一条索引绝对偏移
            byte[] buffer = new byte[8];
            Array.Copy(data, 0, buffer, 0, 8);
            firstIndexOffset = Convert.ToUInt32(buffer[0]) + (Convert.ToUInt32(buffer[1]) << 8)
                + (Convert.ToUInt32(buffer[2]) << 16) + (Convert.ToUInt32(buffer[3]) << 24);
            lastIndexOffset = Convert.ToUInt32(buffer[4]) + (Convert.ToUInt32(buffer[5]) << 8)
                + (Convert.ToUInt32(buffer[6]) << 16) + (Convert.ToUInt32(buffer[7]) << 24);
            /*
             * 每条索引7字节，索引区最后一条记录为版本信息，不是IP记录
             * 因此，IP记录数为((lastIndexOffset - firstIndexOffset) / 7)
             */
            ipCount = Convert.ToUInt32((double)(lastIndexOffset - firstIndexOffset) / 7.0);

            if (ipCount < 1)
            {
                throw new ArgumentException("File Data Error");
            }
        }

        private static UInt32 IpStrToInt(String ipStr)
        {
            String[] ipSections = ipStr.Split('.');
            UInt32 num1 = UInt32.Parse(ipSections[0]) << 0x18;
            UInt32 num2 = UInt32.Parse(ipSections[1]) << 0x10;
            UInt32 num3 = UInt32.Parse(ipSections[2]) << 0x08;
            UInt32 num4 = UInt32.Parse(ipSections[3]);
            return num1 + num2 + num3 + num4;
        }

        private static String IpIntToStr(UInt32 ipInt)
        {
            UInt32 num1 = (ipInt >> 0x18) & 0xFF;
            UInt32 num2 = (ipInt >> 0x10) & 0xFF;
            UInt32 num3 = (ipInt >> 0x08) & 0xFF;
            UInt32 num4 = ipInt & 0xFF;
            return String.Format("{0}.{1}.{2}.{3}", num1, num2, num3, num4);
        }

        public IPRecord Query(string ip)
        {
            if (!regex.IsMatch(ip))
            {
                throw new ArgumentException("IP Address Format Error");
            }
            IPRecord ipLocation = new IPRecord() { IP = ip };
            UInt32 ipInt = IpStrToInt(ip);
            if ((ipInt >> 22) == 0x191)
            {
                //100.64.0.0 - 100.127.255.255
                ipLocation.Location1 = "Carrier-Grade NAT";
                ipLocation.Location2 = "";
            }
            else if ((ipInt >> 24) == 0x7F)
            {
                //127.0.0.0 - 127.255.255.255
                ipLocation.Location1 = "Loopback Network";
                ipLocation.Location2 = "";
            }
            else if ((ipInt >> 24) == 0x0A || (ipInt >> 20) == 0xAC1 || (ipInt >> 16) == 0xC0A8)
            {
                //10.0.0.0 - 10.255.255.255, 172.16.0.0 - 172.31.255.255, 192.168.0.0 - 192.168.255.255
                ipLocation.Location1 = "Local Area Network";
                ipLocation.Location2 = "";
            }
            else
            {
                uint lowerIndex = 0;
                uint upperIndex = ipCount;
                uint middle = 0;
                uint lowerIp = 0;
                uint upperIp = 0;
                uint upperIpOffset = 0;
                byte flag = 0;
                while (lowerIndex < (upperIndex - 1))
                {
                    middle = (upperIndex + lowerIndex) >> 1;
                    lowerIp = GetLowerIp(middle, out upperIpOffset);
                    if (ipInt == lowerIp)
                    {
                        lowerIndex = middle;
                        break;
                    }
                    if (ipInt > lowerIp)
                    {
                        lowerIndex = middle;
                    }
                    else
                    {
                        upperIndex = middle;
                    }
                }
                lowerIp = GetLowerIp(lowerIndex, out upperIpOffset);
                upperIp = GetUpperIp(upperIpOffset, out flag);
                if ((lowerIp <= ipInt) && (upperIp >= ipInt))
                {
                    string local;
                    ipLocation.Location1 = GetLocation(upperIpOffset, flag, out local);
                    ipLocation.Location2 = local;
                }
                else
                {
                    ipLocation.Location1 = "Unknown";
                    ipLocation.Location2 = "";
                }

            }
            return ipLocation;
        }

        private uint GetLowerIp(uint index, out uint upperIpOffset)
        {
            uint leftOffset = firstIndexOffset + (index << 3) - index;
            byte[] buffer = new byte[7];
            Array.Copy(data, leftOffset, buffer, 0, 7);
            upperIpOffset = Convert.ToUInt32(buffer[4]) + (Convert.ToUInt32(buffer[5]) << 8) + (Convert.ToUInt32(buffer[6]) << 16);
            return Convert.ToUInt32(buffer[0]) + (Convert.ToUInt32(buffer[1]) << 8)
                + (Convert.ToUInt32(buffer[2]) << 16) + (Convert.ToUInt32(buffer[3]) << 24);
        }

        private uint GetUpperIp(uint upperIpOffset, out byte flag)
        {
            byte[] buffer = new byte[5];
            Array.Copy(data, upperIpOffset, buffer, 0, 5);
            flag = buffer[4];
            return Convert.ToUInt32(buffer[0]) + (Convert.ToUInt32(buffer[1]) << 8)
                + (Convert.ToUInt32(buffer[2]) << 16) + (Convert.ToUInt32(buffer[3]) << 24);
        }

        private string GetLocation(uint upperIpOffset, byte flag, out string sLocation)
        {
            string location = "";
            uint offset = upperIpOffset + 4;
            switch (flag)
            {
                case 1:
                case 2:
                    location = GetRedirectedLocation(ref offset, ref flag, ref upperIpOffset);
                    offset = upperIpOffset + 8;
                    sLocation = (1 == flag) ? "" : GetRedirectedLocation(ref offset, ref flag, ref upperIpOffset);
                    break;
                default:
                    location = GetRedirectedLocation(ref offset, ref flag, ref upperIpOffset);
                    sLocation = GetRedirectedLocation(ref offset, ref flag, ref upperIpOffset);
                    break;
            }
            return location;
        }

        private string GetRedirectedLocation(ref uint offset, ref byte flag, ref uint endIpOff)
        {
            byte jumpFlag = 0;
            byte[] buffer = new byte[3];//实际偏移位置3字节
            while (true)
            {
                uint forwardOffset = offset;
                jumpFlag = data[forwardOffset++];
                if (jumpFlag != 1 && jumpFlag != 2)
                {
                    break;
                }
                Array.Copy(data, forwardOffset, buffer, 0, 3);
                forwardOffset += 3;
                if (jumpFlag == 2)
                {
                    flag = 2;
                    endIpOff = offset - 4;
                }
                offset = Convert.ToUInt32(buffer[0]) + (Convert.ToUInt32(buffer[1]) << 8) + (Convert.ToUInt32(buffer[2]) << 16);
            }
            if (offset < 12)
            {
                return "";
            }
            return GetLocation(ref offset);
        }

        private string GetLocation(ref uint offset)
        {
            byte lowerByte = 0;
            byte upperByte = 0;
            StringBuilder sBuilder = new StringBuilder();
            byte[] bytes = new byte[2];
            Encoding encoding = Encoding.GetEncoding("GB18030");
            while (true)
            {
                lowerByte = data[offset++];
                if (lowerByte == 0)
                {
                    return sBuilder.ToString();
                }
                if (lowerByte > 0x7f)
                {
                    upperByte = data[offset++];
                    bytes[0] = lowerByte;
                    bytes[1] = upperByte;
                    if (upperByte == 0)
                    {
                        return sBuilder.ToString();
                    }
                    sBuilder.Append(encoding.GetString(bytes));
                }
                else
                {
                    sBuilder.Append((char)lowerByte);
                }
            }
        }
    }
}
