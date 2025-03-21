﻿using DataModel;

namespace Web
{
    public class BasicConfiguration : BaseConfiguration
    {
        public string LongitudeHotel { get; private set; }
        public string LatitudeHotel { get; private set; }
        public string IdentityApiUrl { get; private set; }
        public string IdentityApiName { get; private set; }

        public BasicConfiguration(IConfiguration configuration) : base(configuration) { }
    }
}
