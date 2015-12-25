using AspNet5.Microservice.Utils;

namespace AspNet5.Microservice
{
    public class MicroserviceConfiguration
    {
        /// <summary>
        /// Specifies the IP address range that is allowed to access the actuator endpoints. Default allows all IPs
        /// </summary>
        public static IpAddressRange AllowedIpAddresses;
    }
}
